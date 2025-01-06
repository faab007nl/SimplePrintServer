using System.Net;
using System.Text;
using Newtonsoft.Json;

namespace SimplePrintServer
{
    public class HttpServerManager
    {

        private readonly HttpListener _listener;
        private readonly PrinterManager _printerManager;

        private string _printerId = "";
        private string _paperSize = "";
        private List<string> _printQueue = new();

        public HttpServerManager(PrinterManager printerManager)
        {
            _printerManager = printerManager;

            _listener = new HttpListener();
            _listener.Prefixes.Add("http://localhost:54321/");

            _listener.Start();

            Console.WriteLine(@"Listening on port 54321...");

            var thread = new Thread(HandleServerRequests);
            thread.Start();
        }

        private void HandleServerRequests()
        {
            while (true)
            {
                var ctx = _listener.GetContext();
                using var resp = ctx.Response;

                Router(ctx, resp);
            }
        }

        private void Router(HttpListenerContext ctx, HttpListenerResponse resp)
        {
            var httpMethod = ctx.Request.HttpMethod;
            var httpPath = ctx.Request.Url?.AbsolutePath;

            resp.ContentType = "application/json";
            resp.Headers.Add("Access-Control-Allow-Origin", "*"); // For CORS
            resp.Headers.Add("Access-Control-Allow-Methods", "GET, POST");
            resp.Headers.Add("Access-Control-Allow-Headers", "Content-Type");

            var printerId = Properties.Settings.Default.SelectedPrinterId;
            if (string.IsNullOrEmpty(printerId))
            {
                resp.StatusCode = (int)HttpStatusCode.NoContent;
                string errResponse = JsonConvert.SerializeObject(new
                {
                    success = false,
                    message = "No printer selected. Please open the settings window and select a printer."
                });
                byte[] errBuffer = Encoding.UTF8.GetBytes(errResponse);

                resp.ContentLength64 = errBuffer.Length;
                resp.OutputStream.Write(errBuffer, 0, errBuffer.Length);
                return;
            }
            _printerId = printerId;

            var paperSize = Properties.Settings.Default.SelectedPaperSize;
            if (string.IsNullOrEmpty(paperSize))
            {
                resp.StatusCode = (int)HttpStatusCode.NoContent;
                string errResponse = JsonConvert.SerializeObject(new
                {
                    success = false,
                    message = "No paper size selected. Please open the settings window and select a paper size."
                });
                byte[] errBuffer = Encoding.UTF8.GetBytes(errResponse);

                resp.ContentLength64 = errBuffer.Length;
                resp.OutputStream.Write(errBuffer, 0, errBuffer.Length);
                return;
            }
            _paperSize = paperSize;

            if (httpMethod == "GET")
            {
                if (httpPath == "/")
                {
                    HandleStatusRequest(resp);
                }else if (httpPath == "/queue")
                {
                    HandlePrinterStatusRequest(resp);
                }
                else
                {
                    resp.StatusCode = (int)HttpStatusCode.NotFound;
                }
            }
            else if(httpMethod == "POST")
            {
                if (httpPath == "/print")
                {
                    HandlePrintRequest(ctx, resp);
                }
                else
                {
                    resp.StatusCode = (int)HttpStatusCode.NotFound;
                }
            }else if(httpMethod == "OPTIONS")
            {
                resp.StatusCode = (int)HttpStatusCode.OK;
            }
            else
            {
                resp.StatusCode = (int)HttpStatusCode.MethodNotAllowed;
            }
        }

        private static void HandleStatusRequest(HttpListenerResponse resp)
        {
            resp.StatusCode = (int)HttpStatusCode.OK;

            var response = JsonConvert.SerializeObject(new
            {
                success = true,
                message = "Server is running",
            });
            var buffer = Encoding.UTF8.GetBytes(response);

            resp.ContentLength64 = buffer.Length;
            resp.OutputStream.Write(buffer, 0, buffer.Length);
        }

        private void HandlePrinterStatusRequest(HttpListenerResponse resp)
        {
            try
            {
                resp.StatusCode = (int)HttpStatusCode.OK;

                var printStatusList = _printerManager.GetPrintQueue();

                // check if items in _printQueue are still in print queue
                foreach (var jobId in _printQueue.ToList())
                {
                    var job = printStatusList.Find(j => j.DocumentNumber == jobId);
                    if (job != null) continue;

                    _printQueue.Remove(jobId);

                    // Delete file
                    var filePath = "temp/" + jobId + ".pdf";
                    if (File.Exists(filePath))
                    {
                        File.Delete(filePath);
                    }
                }

                var response = JsonConvert.SerializeObject(new
                {
                    success = true,
                    jobs = printStatusList
                });
                var buffer = Encoding.UTF8.GetBytes(response);

                resp.ContentLength64 = buffer.Length;
                resp.OutputStream.Write(buffer, 0, buffer.Length);
            }catch(Exception e)
            {
                resp.StatusCode = (int)HttpStatusCode.InternalServerError;
                var errBuffer = Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(new
                {
                    success = false,
                    message = e.Message
                }));

                resp.ContentLength64 = errBuffer.Length;
                resp.OutputStream.Write(errBuffer, 0, errBuffer.Length);
            }
        }

        private void HandlePrintRequest(HttpListenerContext ctx, HttpListenerResponse resp)
        {
            try
            {
                // get form data
                var formData = ctx.Request.InputStream;
                var encoding = ctx.Request.ContentEncoding;
                var reader = new StreamReader(formData, encoding);

                var base64data = reader.ReadToEnd();
                if (base64data.Length == 0)
                {
                    resp.StatusCode = (int)HttpStatusCode.NoContent;
                    var errBuffer = Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(new
                    {
                        success = false,
                        message = "No data provided"
                    }));

                    resp.ContentLength64 = errBuffer.Length;
                    resp.OutputStream.Write(errBuffer, 0, errBuffer.Length);
                    return;
                }

                // Generate a unique file name
                var uniqueId = Guid.NewGuid().ToString(); // You can also use a timestamp if preferred

                // Get base64 data and save to file
                var fileBytes = Convert.FromBase64String(base64data);
                Directory.CreateDirectory("temp");
                var filePath = "temp/"+ uniqueId +".pdf";
                File.WriteAllBytes(filePath, fileBytes);

                // Rotate the document 90 deg
                var rotationAngleIndex = Properties.Settings.Default.PageOrientionModifier;
                _printerManager.RotatePdf(filePath, rotationAngleIndex);

                // Print the file
                _printerManager.PrintPdfFromFile(_printerId, _paperSize, filePath);
                _printQueue.Add(uniqueId);

                resp.StatusCode = (int)HttpStatusCode.OK;
                var buffer = Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(new
                {
                    success = true,
                    jobId = uniqueId
                }));
                resp.ContentLength64 = buffer.Length;
                resp.OutputStream.Write(buffer, 0, buffer.Length);
            }catch(Exception e)
            {
                resp.StatusCode = (int)HttpStatusCode.InternalServerError;
                var errBuffer = Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(new
                {
                    success = false,
                    message = e.Message
                }));

                resp.ContentLength64 = errBuffer.Length;
                resp.OutputStream.Write(errBuffer, 0, errBuffer.Length);
            }
        }

    }
}
