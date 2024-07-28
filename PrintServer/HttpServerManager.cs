using System.Diagnostics;
using System.Net;
using System.Text;

namespace SimplePrintServer
{
    public partial class HttpServerManager
    {

        private HttpListener listener;
        private PrinterManager printerManager;

        public HttpServerManager(PrinterManager printerManager)
        {
            this.printerManager = printerManager;

            listener = new HttpListener();
            listener.Prefixes.Add("http://localhost:54321/");

            listener.Start();

            Console.WriteLine("Listening on port 54321...");

            Thread thread = new Thread(new ThreadStart(this.HandleServerRequests));
            thread.Start();
        }

        private void HandleServerRequests()
        {
            while (true)
            {
                HttpListenerContext ctx = listener.GetContext();
                using HttpListenerResponse resp = ctx.Response;

                this.Router(ctx, resp);
            }
        }

        private void Router(HttpListenerContext ctx, HttpListenerResponse resp)
        {
            var httpMethod = ctx.Request.HttpMethod;
            var httpPath = ctx.Request.Url.AbsolutePath;

            Debug.WriteLine($"Request: {httpMethod} {httpPath}");

            resp.ContentType = "application/json";
            resp.Headers.Add("Access-Control-Allow-Origin", "*"); // For CORS
            resp.Headers.Add("Access-Control-Allow-Methods", "GET, POST");
            resp.Headers.Add("Access-Control-Allow-Headers", "Content-Type");

            if (httpMethod == "GET")
            {
                if (httpPath == "/")
                {
                    this.handleStatusRequest(ctx, resp);
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
                    this.handlePrintRequest(ctx, resp);
                }
                else
                {
                    resp.StatusCode = (int)HttpStatusCode.NotFound;
                }
            }
            else
            {
                resp.StatusCode = (int)HttpStatusCode.MethodNotAllowed;
            }
        }

        private void handleStatusRequest(HttpListenerContext ctx, HttpListenerResponse resp)
        {
            resp.StatusCode = (int)HttpStatusCode.OK;

            string response = "{\"status\": \"ok\"}";
            byte[] buffer = Encoding.UTF8.GetBytes(response);

            resp.ContentLength64 = buffer.Length;
            resp.OutputStream.Write(buffer, 0, buffer.Length);
        }

        private void handlePrintRequest(HttpListenerContext ctx, HttpListenerResponse resp)
        {
            try
            {
                // get form data
                var formData = ctx.Request.InputStream;
                var encoding = ctx.Request.ContentEncoding;
                var reader = new System.IO.StreamReader(formData, encoding);

                string base64data = reader.ReadToEnd();
                if (base64data.Length == 0)
                {
                    resp.StatusCode = (int)HttpStatusCode.NoContent;
                    string errResponse = "{\"status\": \"No data provided\"}";
                    byte[] errBuffer = Encoding.UTF8.GetBytes(errResponse);

                    resp.ContentLength64 = errBuffer.Length;
                    resp.OutputStream.Write(errBuffer, 0, errBuffer.Length);
                    return;
                }

                var printerId = Properties.Settings.Default.SelectedPrinterId;
                Debug.WriteLine($"Selected printer: {printerId}");
                if (printerId == null || printerId.Length == 0)
                {
                    resp.StatusCode = (int)HttpStatusCode.NoContent;
                    string errResponse = "{\"status\": \"No printer selected\"}";
                    byte[] errBuffer = Encoding.UTF8.GetBytes(errResponse);

                    resp.ContentLength64 = errBuffer.Length;
                    resp.OutputStream.Write(errBuffer, 0, errBuffer.Length);
                    return;
                }

                byte[] fileBytes = Convert.FromBase64String(base64data);

                Directory.CreateDirectory("temp");
                string filePath = "temp/document.pdf";
                File.WriteAllBytes(filePath, fileBytes);
                Debug.WriteLine($"File saved to {filePath}");

                this.printerManager.PrintPdfFromFile(Properties.Settings.Default.SelectedPrinterId, filePath);

                resp.StatusCode = (int)HttpStatusCode.OK;

                string response = "{\"status\": \"printing\"}";
                byte[] buffer = Encoding.UTF8.GetBytes(response);

                resp.ContentLength64 = buffer.Length;
                resp.OutputStream.Write(buffer, 0, buffer.Length);
            }catch(Exception e)
            {
                resp.StatusCode = (int)HttpStatusCode.InternalServerError;
                string errResponse = "{\"status\": \"error\", \"message\": \"" + e.Message + "\"}";
                byte[] errBuffer = Encoding.UTF8.GetBytes(errResponse);

                resp.ContentLength64 = errBuffer.Length;
                resp.OutputStream.Write(errBuffer, 0, errBuffer.Length);
            }
        }
    }
}
