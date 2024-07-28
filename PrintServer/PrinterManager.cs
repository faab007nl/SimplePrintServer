using System.Diagnostics;
using System.Drawing.Printing;
using System.Management;
using System.Threading;
using SimplePrintServer.Models;

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

        public void PrintTestDoc(string printerId)
        {
            var pdfPath = "Resources\\label-test-file.pdf";
            Process print = new Process();
            print.StartInfo.FileName = "Libraries\\sumatrapdf.exe";
            print.StartInfo.UseShellExecute = true;
            print.StartInfo.CreateNoWindow = true;
            print.StartInfo.WindowStyle = ProcessWindowStyle.Hidden;
            print.StartInfo.Arguments = "-print-to \"" + printerId + "\" -exit-when-done \"" + pdfPath + "\"";
            print.Start();
        }

        public void PrintPdfFromFile(string printerId, string pdfPath)
        {
            Process print = new Process();
            print.StartInfo.FileName = "Libraries\\sumatrapdf.exe";
            print.StartInfo.UseShellExecute = true;
            print.StartInfo.CreateNoWindow = true;
            print.StartInfo.WindowStyle = ProcessWindowStyle.Hidden;
            print.StartInfo.Arguments = "-print-to \"" + printerId + "\" -exit-when-done \"" + pdfPath + "\"";
            print.Start();
        }

    }
}
