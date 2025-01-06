using Newtonsoft.Json;

namespace SimplePrintServer.Models;

public class PrintStatus
{

    public string PrinterName { get; private set; }
    public string DocumentNumber { get; private set; }
    public List<String> PrinterStatuses { get; private set; }
    public bool IsPrinting => IsJobPrinting();
    public bool IsError => IsJobError();
    public bool IsPaused => IsJobPaused();

    public PrintStatus(string printerName, string documentNumber, string printerStatusString)
    {
        PrinterName = printerName;
        DocumentNumber = documentNumber;
        PrinterStatuses = printerStatusString
            .Split('|')
            .Select(status => status.Trim())
            .ToList();
    }

    public bool IsJobPrinting()
    {
        return PrinterStatuses.Contains("Printing");
    }

    public bool IsJobError()
    {
        return PrinterStatuses.Contains("Error");
    }

    public bool IsJobPaused()
    {
        return PrinterStatuses.Contains("Paused");
    }

    public string ToJson()
    {
        return JsonConvert.SerializeObject(this);
    }

}