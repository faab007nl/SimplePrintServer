using System.Diagnostics;
using System.Net;

namespace SimplePrintServer
{
    internal partial class SimplePrintServer: ApplicationContext
    {

        private readonly SettingsForm settingsForm;
        private readonly NotifyIcon trayIcon;
        private PrinterManager printerManager;
        private HttpServerManager httpServerManager;

        public SimplePrintServer() {

            trayIcon = new()
            {
                Icon = new Icon("Resources/logo.ico"),
                Text = "MyParcel Print Server",
                Visible = true
            };

            ContextMenuStrip contextMenu = new();

            ToolStripMenuItem openSettings = new()
            {
                Text = "Settings"
            };
            openSettings.Click += new EventHandler(openSettingsMenu);
            contextMenu.Items.Add(openSettings);

            ToolStripMenuItem exitApplication = new()
            {
                Text = "Close"
            };
            exitApplication.Click += new EventHandler(closeApplication);
            contextMenu.Items.Add(exitApplication);

            trayIcon.ContextMenuStrip = contextMenu;

            
            // Setup printer manager
            printerManager = new PrinterManager();

            // Setup settings form
            settingsForm = new SettingsForm(printerManager);

            // Setup http server manager
            httpServerManager = new HttpServerManager(printerManager);
        }

        public void openSettingsMenu(object sender, EventArgs e)
        {
            settingsForm.Show();
        }

        public void closeApplication(object sender, EventArgs e)
        {
            DialogResult messageBox = MessageBox.Show("Are you sure you want to close Simple Print Server", "Close App?", MessageBoxButtons.YesNo);
            if(messageBox.Equals(DialogResult.Yes))
            {
                settingsForm.Dispose();
                this.Dispose();
                System.Environment.Exit(1);
                Application.Exit();
            }
        }
        

    }
}
