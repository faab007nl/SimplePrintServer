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

            Hide();
        }

        public void RefreshPrintersBtn_Click(object sender, EventArgs e)
        {
            FillPrintersList();
        }

        private void SaveBtn_Click(object sender, EventArgs e)
        {
            SaveSelectedPrinter();

            PrinterListInput.SelectedItem = null;
            PrinterPaperSizesInput.SelectedItem = null;
            PrinterPaperSizesInput.Items.Clear();
            PrinterPaperSizesInput.Enabled = false;
        }

        private void FillPrintersList()
        {
            var printers = printerManager.GetAvailablePrinters();

            var datasource = new BindingSource();
            datasource.DataSource = printers;

            PrinterListInput.DataSource = datasource;
            PrinterListInput.DisplayMember = "Label";
            PrinterListInput.SelectedItem = null;

            PrinterPaperSizesInput.Items.Clear();
            PrinterPaperSizesInput.Enabled = false;
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

            var selectedPaperSize = Properties.Settings.Default.SelectedPaperSize;
            if (selectedPaperSize == null)
            {
                CurrentPaperSizeLabel.Text = "Current Paper Size: none";
                return;
            }

            CurrentPrinterLabel.Text = "Current Printer: " + selectedPrinter.Name;
            CurrentPaperSizeLabel.Text = "Current Paper Size: " + selectedPaperSize;
        }

        private void SaveSelectedPrinter()
        {
            if (PrinterListInput.SelectedItem is not Printer selectedPrinter)
            {
                return;
            }
            CurrentPrinterLabel.Text = "Current Printer: " + selectedPrinter.Name;

            if (PrinterPaperSizesInput.SelectedItem is not string paperSize)
            {
                return;
            }
            CurrentPaperSizeLabel.Text = "Current Paper Size: " + paperSize;

            Properties.Settings.Default.SelectedPrinterId = selectedPrinter.Id;
            Properties.Settings.Default.SelectedPaperSize = paperSize;
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

            printerManager.PrintTestDoc(Properties.Settings.Default.SelectedPrinterId);
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

        private void PrinterListInput_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (PrinterListInput.SelectedItem is not Printer selectedPrinter)
            {
                return;
            }

            PrinterPaperSizesInput.Items.Clear();
            PrinterPaperSizesInput.Enabled = true;
            foreach (var paperSize in selectedPrinter.PaperSizes)
            {
                PrinterPaperSizesInput.Items.Add(paperSize);
            }
        }

    }
}
