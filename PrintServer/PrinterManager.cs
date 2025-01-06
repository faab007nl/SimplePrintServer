using System.Diagnostics;
using System.Management;
using PdfSharp.Pdf;
using PdfSharp.Pdf.IO;
using SimplePrintServer.Models;

namespace SimplePrintServer
{
    public class PrinterManager
    {

        public PrinterManager()
        {
            // Constructor
        }

        public List<Printer> GetAvailablePrinters()
        {
            var printers = new List<Printer>();

            ManagementScope objScope = new ManagementScope(ManagementPath.DefaultPath); //For the local Access
            objScope.Connect();

            SelectQuery selectQuery = new SelectQuery();
            selectQuery.QueryString = "Select * from win32_Printer";
            ManagementObjectSearcher MOS = new ManagementObjectSearcher(objScope, selectQuery);
            ManagementObjectCollection MOC = MOS.Get();
            foreach (ManagementObject mo in MOC)
            {
                printers.Add(new Printer(mo));
            }
            
            return printers;
        }

        public List<String> GetPrinterPaperSizes(string printerId)
        {
            List<String> availableSizes = new List<String>();

            // Query the Win32_Printer class for the specific printer
            string query = $"SELECT * FROM Win32_Printer WHERE Name = '{printerId.Replace("\\", "\\\\")}'";
            ManagementObjectSearcher searcher = new ManagementObjectSearcher(query);

            foreach (ManagementObject printer in searcher.Get())
            {
                // Retrieve the capabilities, including paper sizes
                if (printer["PrinterPaperNames"] is string[] paperSizes)
                {
                    foreach (string size in paperSizes)
                    {
                        availableSizes.Add(size);
                    }
                }
            }

            return availableSizes;
        }

        public Printer? GetPrinterById(string id)
        {
            ManagementScope objScope = new ManagementScope(ManagementPath.DefaultPath); //For the local Access
            objScope.Connect();

            SelectQuery selectQuery = new SelectQuery();
            selectQuery.QueryString = "Select * from win32_Printer WHERE DeviceID = '" + id + "'";
            ManagementObjectSearcher MOS = new ManagementObjectSearcher(objScope, selectQuery);
            ManagementObjectCollection MOC = MOS.Get();
            foreach (ManagementObject mo in MOC)
            {
                return new Printer(mo);
            }
            return null;
        }

        public void PrintTestDoc(string printerId)
        {
            var pdfPath = "Resources\\label-test-file.pdf";
            Process print = new Process();
            print.StartInfo.FileName = "Libraries\\sumatrapdf.exe";
            print.StartInfo.UseShellExecute = true;
            print.StartInfo.CreateNoWindow = true;
            print.StartInfo.WindowStyle = ProcessWindowStyle.Hidden;
            print.StartInfo.Arguments = "-print-to \"" + printerId + "\" -print-settings \"noscale,paper=1744907 4 in x 6 in\" -exit-when-done \"" + pdfPath + "\"";
            print.Start();
        }

        public void PrintPdfFromFile(string printerId, string paperSize, string pdfPath)
        {
            Process print = new Process();
            print.StartInfo.FileName = "Libraries\\sumatrapdf.exe";
            print.StartInfo.UseShellExecute = true;
            print.StartInfo.CreateNoWindow = true;
            print.StartInfo.WindowStyle = ProcessWindowStyle.Hidden;
            print.StartInfo.Arguments = "-print-to \"" + printerId + "\" -print-settings \"noscale,paper="+paperSize+"\" -exit-when-done \"" + pdfPath + "\"";
            print.Start();
        }

        public List<PrintStatus> GetPrintQueue()
        {
            List<PrintStatus> printStatusList = new List<PrintStatus>();

            // Query to get all print jobs
            string query = "SELECT * FROM Win32_PrintJob";

            // Initialize the ManagementObjectSearcher with the query
            using (ManagementObjectSearcher searcher = new ManagementObjectSearcher(query))
            {
                // Execute the query and get the results
                foreach (ManagementObject printJob in searcher.Get())
                {
                    string printerName = printJob["Name"].ToString();
                    string documentName = printJob["Document"].ToString();
                    string jobStatus = printJob["JobStatus"].ToString();

                    if(printerName == null || documentName == null || jobStatus == null)
                    {
                        continue;
                    }

                    string fileName = documentName.Split('\\').Last();
                    string documentId = fileName.Split('.').First();

                    printStatusList.Add(new PrintStatus(
                        printerName,
                        documentId,
                        jobStatus
                    ));
                }
            }

            return printStatusList;
        }

        public void RotatePdf(string tempDocumentPdf, int rotationAngleIndex)
        {
            var rotationAngle = 0;
            if (rotationAngleIndex == 1)
            {
                rotationAngle = 90;
            }else if (rotationAngleIndex == 2)
            {
                rotationAngle = 180;
            }else if (rotationAngleIndex == 3)
            {
                rotationAngle = 270;
            }

            // Open the PDF document
            PdfDocument document = PdfReader.Open(tempDocumentPdf, PdfDocumentOpenMode.Modify);

            foreach (PdfPage page in document.Pages)
            {
                // Rotate each page
                page.Rotate = (page.Rotate + rotationAngle) % 360;
            }

            // Save the rotated document
            document.Save(tempDocumentPdf);
        }
    }
}
