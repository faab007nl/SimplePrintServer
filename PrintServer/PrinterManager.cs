using System.Management;
using System.Diagnostics;
using System.Reflection;
using SimplePrintServer.Models;
using IronPdf;
using IronPdf.Rendering;
using IronSoftware;
using IronPdf.Pages;
using System.Windows.Forms;

namespace SimplePrintServer
{
    public partial class PrinterManager
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

        public void PrintHtml(string printerId, string html)
        {
            var renderer = new ChromePdfRenderer();
            var doc = renderer.RenderHtmlAsPdf(html);

            _ = doc.Print(printerId);
        }

        public void PrintPdfFromFile(string printerId, string pdfPath)
        {
            int selectedPageOrientionModifierIndex = Properties.Settings.Default.PageOrientionModifier;
            PdfPageRotation selectedPageOrientionModifier = PdfPageRotation.None;
            switch (selectedPageOrientionModifierIndex)
            {
                case 0:
                    selectedPageOrientionModifier = PdfPageRotation.None;
                    break;
                case 1:
                    selectedPageOrientionModifier = PdfPageRotation.Clockwise90;
                    break;
                case 2:
                    selectedPageOrientionModifier = PdfPageRotation.Clockwise180;
                    break;
                case 3:
                    selectedPageOrientionModifier = PdfPageRotation.Clockwise270;
                    break;
                default:
                    selectedPageOrientionModifier = PdfPageRotation.None;
                    break;
            }

            var doc = PdfDocument.FromFile(pdfPath);
            foreach (PdfPage page in doc.Pages)
            {
                page.PageRotation = selectedPageOrientionModifier;
            }
            _ = doc.Print(printerId);
        }

    }
}
