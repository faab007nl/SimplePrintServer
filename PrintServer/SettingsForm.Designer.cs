namespace SimplePrintServer
{
    partial class SettingsForm
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SettingsForm));
            PrinterListInput = new ComboBox();
            label1 = new Label();
            label2 = new Label();
            RefreshPrintersBtn = new Button();
            CurrentPrinterLabel = new Label();
            SaveBtn = new Button();
            label3 = new Label();
            printTestPageBtn = new Button();
            pageOrientionModifierSelect = new ComboBox();
            SuspendLayout();
            // 
            // PrinterListInput
            // 
            PrinterListInput.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point, 0);
            PrinterListInput.FormattingEnabled = true;
            PrinterListInput.Location = new Point(16, 42);
            PrinterListInput.Name = "PrinterListInput";
            PrinterListInput.Size = new Size(289, 29);
            PrinterListInput.TabIndex = 0;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new Font("Segoe UI", 14.25F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label1.Location = new Point(11, 14);
            label1.Name = "label1";
            label1.Size = new Size(207, 25);
            label1.TabIndex = 2;
            label1.Text = "Select Default Printer:";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(16, 151);
            label2.Name = "label2";
            label2.Size = new Size(147, 15);
            label2.TabIndex = 3;
            label2.Text = "Page Orientation Modifier:";
            // 
            // RefreshPrintersBtn
            // 
            RefreshPrintersBtn.Location = new Point(16, 77);
            RefreshPrintersBtn.Name = "RefreshPrintersBtn";
            RefreshPrintersBtn.Size = new Size(81, 25);
            RefreshPrintersBtn.TabIndex = 4;
            RefreshPrintersBtn.Text = "Refresh List";
            RefreshPrintersBtn.UseVisualStyleBackColor = true;
            RefreshPrintersBtn.Click += RefreshPrintersBtn_Click;
            // 
            // CurrentPrinterLabel
            // 
            CurrentPrinterLabel.AutoSize = true;
            CurrentPrinterLabel.Location = new Point(16, 110);
            CurrentPrinterLabel.Name = "CurrentPrinterLabel";
            CurrentPrinterLabel.Size = new Size(118, 15);
            CurrentPrinterLabel.TabIndex = 5;
            CurrentPrinterLabel.Text = "Current Printer: none";
            // 
            // SaveBtn
            // 
            SaveBtn.Location = new Point(224, 77);
            SaveBtn.Name = "SaveBtn";
            SaveBtn.Size = new Size(81, 25);
            SaveBtn.TabIndex = 6;
            SaveBtn.Text = "Save";
            SaveBtn.UseVisualStyleBackColor = true;
            SaveBtn.Click += SaveBtn_Click;
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Font = new Font("Segoe UI", 9F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label3.Location = new Point(16, 130);
            label3.Name = "label3";
            label3.Size = new Size(292, 15);
            label3.TabIndex = 7;
            label3.Text = "---------------------------------------------------------";
            // 
            // printTestPageBtn
            // 
            printTestPageBtn.Location = new Point(16, 224);
            printTestPageBtn.Name = "printTestPageBtn";
            printTestPageBtn.Size = new Size(289, 53);
            printTestPageBtn.TabIndex = 8;
            printTestPageBtn.Text = "Print Test Page";
            printTestPageBtn.UseVisualStyleBackColor = true;
            printTestPageBtn.Click += printTestPagePressed;
            // 
            // pageOrientionModifierSelect
            // 
            pageOrientionModifierSelect.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point, 0);
            pageOrientionModifierSelect.FormattingEnabled = true;
            pageOrientionModifierSelect.Items.AddRange(new object[] { "None", "Rotate 90 deg", "Rotate 180 deg", "Rotate 270 deg" });
            pageOrientionModifierSelect.Location = new Point(16, 169);
            pageOrientionModifierSelect.Name = "pageOrientionModifierSelect";
            pageOrientionModifierSelect.Size = new Size(289, 29);
            pageOrientionModifierSelect.TabIndex = 9;
            pageOrientionModifierSelect.UseWaitCursor = true;
            pageOrientionModifierSelect.SelectedIndexChanged += pageOrientionModifierSelect_SelectedIndexChanged;
            // 
            // SettingsForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(323, 289);
            Controls.Add(pageOrientionModifierSelect);
            Controls.Add(printTestPageBtn);
            Controls.Add(label3);
            Controls.Add(SaveBtn);
            Controls.Add(CurrentPrinterLabel);
            Controls.Add(RefreshPrintersBtn);
            Controls.Add(label2);
            Controls.Add(label1);
            Controls.Add(PrinterListInput);
            FormBorderStyle = FormBorderStyle.FixedToolWindow;
            Icon = (Icon)resources.GetObject("$this.Icon");
            Name = "SettingsForm";
            Text = "Settings - Simple Print Server";
            FormClosing += SettingsForm_FormClosing;
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private ComboBox PrinterListInput;
        private Label label1;
        private Label label2;
        private Button RefreshPrintersBtn;
        private Label CurrentPrinterLabel;
        private Button SaveBtn;
        private Label label3;
        private Button printTestPageBtn;
        private ComboBox pageOrientionModifierSelect;
    }
}
