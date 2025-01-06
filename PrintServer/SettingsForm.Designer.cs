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
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SettingsForm));
            PrinterListInput = new System.Windows.Forms.ComboBox();
            label1 = new System.Windows.Forms.Label();
            label2 = new System.Windows.Forms.Label();
            RefreshPrintersBtn = new System.Windows.Forms.Button();
            CurrentPrinterLabel = new System.Windows.Forms.Label();
            SaveBtn = new System.Windows.Forms.Button();
            printTestPageBtn = new System.Windows.Forms.Button();
            pageOrientionModifierSelect = new System.Windows.Forms.ComboBox();
            PrinterPaperSizesInput = new System.Windows.Forms.ComboBox();
            CurrentPaperSizeLabel = new System.Windows.Forms.Label();
            ErrorLabel = new System.Windows.Forms.Label();
            SuspendLayout();
            //
            // PrinterListInput
            //
            PrinterListInput.Font = new System.Drawing.Font("Segoe UI", 9.6F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)0));
            PrinterListInput.FormattingEnabled = true;
            PrinterListInput.Location = new System.Drawing.Point(16, 51);
            PrinterListInput.Name = "PrinterListInput";
            PrinterListInput.Size = new System.Drawing.Size(289, 29);
            PrinterListInput.TabIndex = 0;
            PrinterListInput.SelectedIndexChanged += PrinterListInput_SelectedIndexChanged;
            //
            // label1
            //
            label1.AutoSize = true;
            label1.Font = new System.Drawing.Font("Segoe UI", 9.6F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)0));
            label1.Location = new System.Drawing.Point(11, 14);
            label1.Name = "label1";
            label1.Size = new System.Drawing.Size(178, 21);
            label1.TabIndex = 2;
            label1.Text = "Select Default Printer:";
            //
            // label2
            //
            label2.AutoSize = true;
            label2.Location = new System.Drawing.Point(16, 227);
            label2.Name = "label2";
            label2.Size = new System.Drawing.Size(184, 20);
            label2.TabIndex = 3;
            label2.Text = "Page Orientation Modifier:";
            //
            // RefreshPrintersBtn
            //
            RefreshPrintersBtn.Location = new System.Drawing.Point(16, 117);
            RefreshPrintersBtn.Name = "RefreshPrintersBtn";
            RefreshPrintersBtn.Size = new System.Drawing.Size(81, 25);
            RefreshPrintersBtn.TabIndex = 4;
            RefreshPrintersBtn.Text = "Refresh List";
            RefreshPrintersBtn.UseVisualStyleBackColor = true;
            RefreshPrintersBtn.Click += RefreshPrintersBtn_Click;
            //
            // CurrentPrinterLabel
            //
            CurrentPrinterLabel.AutoSize = true;
            CurrentPrinterLabel.Location = new System.Drawing.Point(16, 175);
            CurrentPrinterLabel.Name = "CurrentPrinterLabel";
            CurrentPrinterLabel.Size = new System.Drawing.Size(144, 20);
            CurrentPrinterLabel.TabIndex = 5;
            CurrentPrinterLabel.Text = "Current Printer: none";
            //
            // SaveBtn
            //
            SaveBtn.Location = new System.Drawing.Point(224, 117);
            SaveBtn.Name = "SaveBtn";
            SaveBtn.Size = new System.Drawing.Size(81, 25);
            SaveBtn.TabIndex = 6;
            SaveBtn.Text = "Save";
            SaveBtn.UseVisualStyleBackColor = true;
            SaveBtn.Click += SaveBtn_Click;
            //
            // printTestPageBtn
            //
            printTestPageBtn.Location = new System.Drawing.Point(16, 285);
            printTestPageBtn.Name = "printTestPageBtn";
            printTestPageBtn.Size = new System.Drawing.Size(289, 53);
            printTestPageBtn.TabIndex = 8;
            printTestPageBtn.Text = "Print Test Page";
            printTestPageBtn.UseVisualStyleBackColor = true;
            printTestPageBtn.Click += printTestPagePressed;
            //
            // pageOrientionModifierSelect
            //
            pageOrientionModifierSelect.Font = new System.Drawing.Font("Segoe UI", 9.6F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)0));
            pageOrientionModifierSelect.FormattingEnabled = true;
            pageOrientionModifierSelect.Items.AddRange(new object[] { "None", "Rotate 90 deg", "Rotate 180 deg", "Rotate 270 deg" });
            pageOrientionModifierSelect.Location = new System.Drawing.Point(16, 245);
            pageOrientionModifierSelect.Name = "pageOrientionModifierSelect";
            pageOrientionModifierSelect.Size = new System.Drawing.Size(289, 29);
            pageOrientionModifierSelect.TabIndex = 9;
            pageOrientionModifierSelect.UseWaitCursor = true;
            pageOrientionModifierSelect.SelectedIndexChanged += pageOrientionModifierSelect_SelectedIndexChanged;
            //
            // PrinterPaperSizesInput
            //
            PrinterPaperSizesInput.Enabled = false;
            PrinterPaperSizesInput.Font = new System.Drawing.Font("Segoe UI", 9.6F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)0));
            PrinterPaperSizesInput.FormattingEnabled = true;
            PrinterPaperSizesInput.Location = new System.Drawing.Point(16, 86);
            PrinterPaperSizesInput.Name = "PrinterPaperSizesInput";
            PrinterPaperSizesInput.Size = new System.Drawing.Size(289, 21);
            PrinterPaperSizesInput.TabIndex = 10;
            //
            // CurrentPaperSizeLabel
            //
            CurrentPaperSizeLabel.AutoSize = true;
            CurrentPaperSizeLabel.Location = new System.Drawing.Point(16, 195);
            CurrentPaperSizeLabel.Name = "CurrentPaperSizeLabel";
            CurrentPaperSizeLabel.Size = new System.Drawing.Size(169, 20);
            CurrentPaperSizeLabel.TabIndex = 11;
            CurrentPaperSizeLabel.Text = "Current Paper Size: none";
            //
            // ErrorLabel
            //
            ErrorLabel.AutoSize = true;
            ErrorLabel.ForeColor = System.Drawing.Color.Red;
            ErrorLabel.Location = new System.Drawing.Point(16, 145);
            ErrorLabel.Name = "ErrorLabel";
            ErrorLabel.Size = new System.Drawing.Size(0, 20);
            ErrorLabel.TabIndex = 12;
            //
            // SettingsForm
            //
            AutoScaleDimensions = new System.Drawing.SizeF(8F, 20F);
            AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            ClientSize = new System.Drawing.Size(337, 382);
            Controls.Add(ErrorLabel);
            Controls.Add(CurrentPaperSizeLabel);
            Controls.Add(PrinterPaperSizesInput);
            Controls.Add(pageOrientionModifierSelect);
            Controls.Add(printTestPageBtn);
            Controls.Add(SaveBtn);
            Controls.Add(CurrentPrinterLabel);
            Controls.Add(RefreshPrintersBtn);
            Controls.Add(label2);
            Controls.Add(label1);
            Controls.Add(PrinterListInput);
            FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            Icon = ((System.Drawing.Icon)resources.GetObject("$this.Icon"));
            Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            Text = "Settings - Simple Print Server";
            FormClosing += SettingsForm_FormClosing;
            ResumeLayout(false);
            PerformLayout();
        }

        private System.Windows.Forms.Label ErrorLabel;

        private System.Windows.Forms.Label CurrentPaperSizeLabel;

        private System.Windows.Forms.ComboBox PrinterPaperSizesInput;

        #endregion

        private System.Windows.Forms.ComboBox PrinterListInput;
        private Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button RefreshPrintersBtn;
        private System.Windows.Forms.Label CurrentPrinterLabel;
        private System.Windows.Forms.Button SaveBtn;
        private System.Windows.Forms.Button printTestPageBtn;
        private System.Windows.Forms.ComboBox pageOrientionModifierSelect;
    }
}
