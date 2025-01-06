using System;
using System.Collections.Generic;
using System.Linq;
using System.Management;
using System.Text;
using System.Threading.Tasks;

namespace SimplePrintServer.Models
{
    public class Printer
    {

        public string Id { get; set; }
        public string Name { get; set; }
        public bool IsDefault { get; set; }
        public bool IsLocalPrinter { get; set; }
        public bool IsNetworkPrinter { get; set; }
        public List<String> PaperSizes { get; set; } = new List<string>();

        public string Label => GetLabel();

        public Printer(ManagementObject mo)
        {
            Id = mo.GetPropertyValue("DeviceID").ToString();
            Name = mo.GetPropertyValue("Name").ToString();
            IsDefault = (bool)mo.GetPropertyValue("Default");
            IsLocalPrinter = (bool)mo.GetPropertyValue("Local");
            IsNetworkPrinter = (bool)mo.GetPropertyValue("Network");
            if (mo["PrinterPaperNames"] is string[] paperSizes)
            {
                foreach (string size in paperSizes)
                {
                    PaperSizes.Add(size);
                }
            }
        }

        public string GetLabel()
        {
            var defaultText = IsDefault ? " (Default)" : "";
            var connectionType = "Unknown";
            if (IsLocalPrinter)
            {
                connectionType = "- Local";
            }
            else if (IsNetworkPrinter)
            {
                connectionType = "- Network";
            }


            return $"{Name} {connectionType} {defaultText}";
        }

    }
}
