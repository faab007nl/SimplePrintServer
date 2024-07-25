using SimplePrintServer.Models;

namespace SimplePrintServer
{
    public partial class SettingsForm : Form
    {
        private readonly PrinterManager printerManager;

        public SettingsForm(PrinterManager printerManager)
        {
            InitializeComponent();
            this.printerManager = printerManager;

            FillPrintersList();
            LoadSelectedPrinter();
            LoadPageOrientionModifier();
        }

        private void SettingsForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (e.CloseReason == CloseReason.WindowsShutDown) return;
            e.Cancel = true;

            this.Hide();
        }

        public void RefreshPrintersBtn_Click(object sender, EventArgs e)
        {
            FillPrintersList();
        }

        private void SaveBtn_Click(object sender, EventArgs e)
        {
            SaveSelectedPrinter();

            PrinterListInput.SelectedItem = null;
        }

        // 

        private void FillPrintersList()
        {
            var printers = printerManager.GetAvailablePrinters();


            var datasource = new BindingSource();
            datasource.DataSource = printers;

            PrinterListInput.DataSource = datasource;
            PrinterListInput.DisplayMember = "Label";
            PrinterListInput.SelectedItem = null;
        }

        private void LoadSelectedPrinter()
        {
            var selectedPrinterId = Properties.Settings.Default.SelectedPrinterId;
            if (selectedPrinterId == null)
            {
                CurrentPrinterLabel.Text = "Current Printer: none";
                return;
            }

            var selectedPrinter = printerManager.GetAvailablePrinters().Find(p => p.Id == selectedPrinterId);
            if (selectedPrinter == null)
            {
                CurrentPrinterLabel.Text = "Current Printer: Not present";
                return;
            }

            CurrentPrinterLabel.Text = "Current Printer: " + selectedPrinter.Name;
        }

        private void SaveSelectedPrinter()
        {
            if (PrinterListInput.SelectedItem is not Printer selectedPrinter)
            {
                return;
            }
            CurrentPrinterLabel.Text = "Current Printer: " + selectedPrinter.Name;


            Properties.Settings.Default.SelectedPrinterId = selectedPrinter.Id;
            Properties.Settings.Default.Save();
        }

        private void printTestPagePressed(object sender, EventArgs e)
        {
            var printerId = Properties.Settings.Default.SelectedPrinterId;
            if (printerId == null)
            {
                MessageBox.Show("No printer selected");
                return;
            }

            string html = @"<h1>Test Page</h1> <p>This is sample test print created to test the connection of the computer and printer.</p>";
            printerManager.PrintHtml(Properties.Settings.Default.SelectedPrinterId, html);
        }

        private void LoadPageOrientionModifier()
        {
            var selectedPageOrientionModifierIndex = Properties.Settings.Default.PageOrientionModifier;
            pageOrientionModifierSelect.SelectedIndex = selectedPageOrientionModifierIndex;
        }

        private void pageOrientionModifierSelect_SelectedIndexChanged(object sender, EventArgs e)
        {
            Properties.Settings.Default.PageOrientionModifier = pageOrientionModifierSelect.SelectedIndex;
            Properties.Settings.Default.Save();
        }

    }
}
