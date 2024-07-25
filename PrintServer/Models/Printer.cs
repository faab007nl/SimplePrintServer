using System;
using System.Collections.Generic;
using System.Linq;
using System.Management;
using System.Text;
using System.Threading.Tasks;

namespace SimplePrintServer.Models
{
    public partial class Printer
    {

        public string Id { get; set; }
        public string Name { get; set; }
        public bool IsDefault { get; set; }
        public bool IsLocalPrinter { get; set; }
        public bool IsNetworkPrinter { get; set; }

        public string Label => GetLabel();

        public Printer(string id, string name, bool isDefault, bool isLocalPrinter, bool isNetworkPrinter)
        {
            Id = id;
            Name = name;
            IsDefault = isDefault;
            IsLocalPrinter = isLocalPrinter;
            IsNetworkPrinter = isNetworkPrinter;
        }

        public Printer(ManagementObject mo)
        {
            Id = mo.GetPropertyValue("DeviceID").ToString();
            Name = mo.GetPropertyValue("Name").ToString();
            IsDefault = (bool)mo.GetPropertyValue("Default");
            IsLocalPrinter = (bool)mo.GetPropertyValue("Local");
            IsNetworkPrinter = (bool)mo.GetPropertyValue("Network");
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
