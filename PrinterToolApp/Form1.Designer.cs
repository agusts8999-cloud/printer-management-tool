namespace PrinterToolApp
{
    partial class Form1
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
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
            this.groupBoxPrinters = new System.Windows.Forms.GroupBox();
            this.labelPrinterCount = new System.Windows.Forms.Label();
            this.btnRefresh = new System.Windows.Forms.Button();
            this.listBoxPrinters = new System.Windows.Forms.ListBox();
            this.groupBoxPaperSizes = new System.Windows.Forms.GroupBox();
            this.labelPaperSizeCount = new System.Windows.Forms.Label();
            this.btnShowDevMode = new System.Windows.Forms.Button();
            this.btnEditPaperSize = new System.Windows.Forms.Button();
            this.listBoxPaperSizes = new System.Windows.Forms.ListBox();
            this.groupBoxAddPaperSize = new System.Windows.Forms.GroupBox();
            this.numHeight = new System.Windows.Forms.NumericUpDown();
            this.numWidth = new System.Windows.Forms.NumericUpDown();
            this.btnAddPaperSize = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.txtPaperName = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.chkPermanent = new System.Windows.Forms.CheckBox();
            this.groupBoxEditPaperSize = new System.Windows.Forms.GroupBox();
            this.numEditHeight = new System.Windows.Forms.NumericUpDown();
            this.numEditWidth = new System.Windows.Forms.NumericUpDown();
            this.btnUpdatePaperSize = new System.Windows.Forms.Button();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.txtEditPaperName = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.btnCancelEdit = new System.Windows.Forms.Button();
            this.chkEditPermanent = new System.Windows.Forms.CheckBox();
            this.groupBoxPrinters.SuspendLayout();
            this.groupBoxPaperSizes.SuspendLayout();
            this.groupBoxAddPaperSize.SuspendLayout();
            this.groupBoxEditPaperSize.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numHeight)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numWidth)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numEditHeight)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numEditWidth)).BeginInit();
            this.SuspendLayout();
            // 
            // groupBoxPrinters
            // 
            this.groupBoxPrinters.Controls.Add(this.labelPrinterCount);
            this.groupBoxPrinters.Controls.Add(this.btnRefresh);
            this.groupBoxPrinters.Controls.Add(this.listBoxPrinters);
            this.groupBoxPrinters.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBoxPrinters.Location = new System.Drawing.Point(12, 12);
            this.groupBoxPrinters.Name = "groupBoxPrinters";
            this.groupBoxPrinters.Size = new System.Drawing.Size(380, 250);
            this.groupBoxPrinters.TabIndex = 0;
            this.groupBoxPrinters.TabStop = false;
            this.groupBoxPrinters.Text = "Installed Printers";
            // 
            // labelPrinterCount
            // 
            this.labelPrinterCount.AutoSize = true;
            this.labelPrinterCount.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelPrinterCount.ForeColor = System.Drawing.SystemColors.ControlDarkDark;
            this.labelPrinterCount.Location = new System.Drawing.Point(6, 225);
            this.labelPrinterCount.Name = "labelPrinterCount";
            this.labelPrinterCount.Size = new System.Drawing.Size(86, 13);
            this.labelPrinterCount.TabIndex = 2;
            this.labelPrinterCount.Text = "Total: 0 printer(s)";
            // 
            // btnRefresh
            // 
            this.btnRefresh.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnRefresh.Location = new System.Drawing.Point(283, 217);
            this.btnRefresh.Name = "btnRefresh";
            this.btnRefresh.Size = new System.Drawing.Size(85, 27);
            this.btnRefresh.TabIndex = 1;
            this.btnRefresh.Text = "Refresh";
            this.btnRefresh.UseVisualStyleBackColor = true;
            this.btnRefresh.Click += new System.EventHandler(this.btnRefresh_Click);
            // 
            // listBoxPrinters
            // 
            this.listBoxPrinters.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.listBoxPrinters.FormattingEnabled = true;
            this.listBoxPrinters.ItemHeight = 15;
            this.listBoxPrinters.Location = new System.Drawing.Point(9, 22);
            this.listBoxPrinters.Name = "listBoxPrinters";
            this.listBoxPrinters.Size = new System.Drawing.Size(359, 184);
            this.listBoxPrinters.TabIndex = 0;
            this.listBoxPrinters.SelectedIndexChanged += new System.EventHandler(this.listBoxPrinters_SelectedIndexChanged);
            // 
            // groupBoxPaperSizes
            // 
            this.groupBoxPaperSizes.Controls.Add(this.labelPaperSizeCount);
            this.groupBoxPaperSizes.Controls.Add(this.btnShowDevMode);
            this.groupBoxPaperSizes.Controls.Add(this.btnEditPaperSize);
            this.groupBoxPaperSizes.Controls.Add(this.listBoxPaperSizes);
            this.groupBoxPaperSizes.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBoxPaperSizes.Location = new System.Drawing.Point(12, 268);
            this.groupBoxPaperSizes.Name = "groupBoxPaperSizes";
            this.groupBoxPaperSizes.Size = new System.Drawing.Size(380, 280);
            this.groupBoxPaperSizes.TabIndex = 1;
            this.groupBoxPaperSizes.TabStop = false;
            this.groupBoxPaperSizes.Text = "Paper Sizes for Selected Printer";
            // 
            // labelPaperSizeCount
            // 
            this.labelPaperSizeCount.AutoSize = true;
            this.labelPaperSizeCount.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelPaperSizeCount.ForeColor = System.Drawing.SystemColors.ControlDarkDark;
            this.labelPaperSizeCount.Location = new System.Drawing.Point(6, 255);
            this.labelPaperSizeCount.Name = "labelPaperSizeCount";
            this.labelPaperSizeCount.Size = new System.Drawing.Size(110, 13);
            this.labelPaperSizeCount.TabIndex = 3;
            this.labelPaperSizeCount.Text = "Total: 0 paper size(s)";
            // 
            // btnShowDevMode
            // 
            this.btnShowDevMode.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnShowDevMode.Location = new System.Drawing.Point(247, 247);
            this.btnShowDevMode.Name = "btnShowDevMode";
            this.btnShowDevMode.Size = new System.Drawing.Size(121, 27);
            this.btnShowDevMode.TabIndex = 2;
            this.btnShowDevMode.Text = "Show DevMode";
            this.btnShowDevMode.UseVisualStyleBackColor = true;
            this.btnShowDevMode.Click += new System.EventHandler(this.btnShowDevMode_Click);
            // 
            // btnEditPaperSize
            // 
            this.btnEditPaperSize.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnEditPaperSize.Location = new System.Drawing.Point(127, 247);
            this.btnEditPaperSize.Name = "btnEditPaperSize";
            this.btnEditPaperSize.Size = new System.Drawing.Size(114, 27);
            this.btnEditPaperSize.TabIndex = 4;
            this.btnEditPaperSize.Text = "Edit Selected";
            this.btnEditPaperSize.UseVisualStyleBackColor = true;
            this.btnEditPaperSize.Click += new System.EventHandler(this.btnEditPaperSize_Click);
            // 
            // listBoxPaperSizes
            // 
            this.listBoxPaperSizes.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.listBoxPaperSizes.FormattingEnabled = true;
            this.listBoxPaperSizes.ItemHeight = 15;
            this.listBoxPaperSizes.Location = new System.Drawing.Point(9, 22);
            this.listBoxPaperSizes.Name = "listBoxPaperSizes";
            this.listBoxPaperSizes.Size = new System.Drawing.Size(359, 214);
            this.listBoxPaperSizes.TabIndex = 0;
            // 
            // groupBoxAddPaperSize
            // 
            this.groupBoxAddPaperSize.Controls.Add(this.numHeight);
            this.groupBoxAddPaperSize.Controls.Add(this.numWidth);
            this.groupBoxAddPaperSize.Controls.Add(this.btnAddPaperSize);
            this.groupBoxAddPaperSize.Controls.Add(this.chkPermanent);
            this.groupBoxAddPaperSize.Controls.Add(this.label3);
            this.groupBoxAddPaperSize.Controls.Add(this.label2);
            this.groupBoxAddPaperSize.Controls.Add(this.txtPaperName);
            this.groupBoxAddPaperSize.Controls.Add(this.label1);
            this.groupBoxAddPaperSize.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBoxAddPaperSize.Location = new System.Drawing.Point(398, 12);
            this.groupBoxAddPaperSize.Name = "groupBoxAddPaperSize";
            this.groupBoxAddPaperSize.Size = new System.Drawing.Size(334, 250);
            this.groupBoxAddPaperSize.TabIndex = 2;
            this.groupBoxAddPaperSize.TabStop = false;
            this.groupBoxAddPaperSize.Text = "Add Custom Paper Size";
            // 
            // numHeight
            // 
            this.numHeight.DecimalPlaces = 1;
            this.numHeight.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.numHeight.Location = new System.Drawing.Point(127, 111);
            this.numHeight.Maximum = new decimal(new int[] {
            1000,
            0,
            0,
            0});
            this.numHeight.Minimum = new decimal(new int[] {
            10,
            0,
            0,
            0});
            this.numHeight.Name = "numHeight";
            this.numHeight.Size = new System.Drawing.Size(190, 23);
            this.numHeight.TabIndex = 6;
            this.numHeight.Value = new decimal(new int[] {
            100,
            0,
            0,
            0});
            // 
            // numWidth
            // 
            this.numWidth.DecimalPlaces = 1;
            this.numWidth.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.numWidth.Location = new System.Drawing.Point(127, 73);
            this.numWidth.Maximum = new decimal(new int[] {
            1000,
            0,
            0,
            0});
            this.numWidth.Minimum = new decimal(new int[] {
            10,
            0,
            0,
            0});
            this.numWidth.Name = "numWidth";
            this.numWidth.Size = new System.Drawing.Size(190, 23);
            this.numWidth.TabIndex = 5;
            this.numWidth.Value = new decimal(new int[] {
            100,
            0,
            0,
            0});
            // 
            // btnAddPaperSize
            // 
            this.btnAddPaperSize.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnAddPaperSize.Location = new System.Drawing.Point(127, 156);
            this.btnAddPaperSize.Name = "btnAddPaperSize";
            this.btnAddPaperSize.Size = new System.Drawing.Size(190, 35);
            this.btnAddPaperSize.TabIndex = 4;
            this.btnAddPaperSize.Text = "Add Paper Size";
            this.btnAddPaperSize.UseVisualStyleBackColor = true;
            this.btnAddPaperSize.Click += new System.EventHandler(this.btnAddPaperSize_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(15, 113);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(78, 15);
            this.label3.TabIndex = 3;
            this.label3.Text = "Height (mm):";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(15, 75);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(75, 15);
            this.label2.TabIndex = 2;
            this.label2.Text = "Width (mm):";
            // 
            // txtPaperName
            // 
            this.txtPaperName.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtPaperName.Location = new System.Drawing.Point(127, 35);
            this.txtPaperName.Name = "txtPaperName";
            this.txtPaperName.Size = new System.Drawing.Size(190, 23);
            this.txtPaperName.TabIndex = 1;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(15, 38);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(70, 15);
            this.label1.TabIndex = 0;
            this.label1.Text = "Paper Name:";
            // 
            // chkPermanent
            // 
            this.chkPermanent.AutoSize = true;
            this.chkPermanent.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chkPermanent.Location = new System.Drawing.Point(127, 140);
            this.chkPermanent.Name = "chkPermanent";
            this.chkPermanent.Size = new System.Drawing.Size(234, 17);
            this.chkPermanent.TabIndex = 7;
            this.chkPermanent.Text = "Simpan permanen ke registry (Admin required)";
            this.chkPermanent.UseVisualStyleBackColor = true;
            // 
            // groupBoxEditPaperSize
            // 
            this.groupBoxEditPaperSize.Controls.Add(this.btnCancelEdit);
            this.groupBoxEditPaperSize.Controls.Add(this.numEditHeight);
            this.groupBoxEditPaperSize.Controls.Add(this.numEditWidth);
            this.groupBoxEditPaperSize.Controls.Add(this.btnUpdatePaperSize);
            this.groupBoxEditPaperSize.Controls.Add(this.chkEditPermanent);
            this.groupBoxEditPaperSize.Controls.Add(this.label4);
            this.groupBoxEditPaperSize.Controls.Add(this.label5);
            this.groupBoxEditPaperSize.Controls.Add(this.txtEditPaperName);
            this.groupBoxEditPaperSize.Controls.Add(this.label6);
            this.groupBoxEditPaperSize.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBoxEditPaperSize.Location = new System.Drawing.Point(398, 268);
            this.groupBoxEditPaperSize.Name = "groupBoxEditPaperSize";
            this.groupBoxEditPaperSize.Size = new System.Drawing.Size(334, 280);
            this.groupBoxEditPaperSize.TabIndex = 3;
            this.groupBoxEditPaperSize.TabStop = false;
            this.groupBoxEditPaperSize.Text = "Edit Paper Size";
            this.groupBoxEditPaperSize.Visible = false;
            // 
            // numEditHeight
            // 
            this.numEditHeight.DecimalPlaces = 1;
            this.numEditHeight.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.numEditHeight.Location = new System.Drawing.Point(127, 111);
            this.numEditHeight.Maximum = new decimal(new int[] {
            1000,
            0,
            0,
            0});
            this.numEditHeight.Minimum = new decimal(new int[] {
            10,
            0,
            0,
            0});
            this.numEditHeight.Name = "numEditHeight";
            this.numEditHeight.Size = new System.Drawing.Size(190, 23);
            this.numEditHeight.TabIndex = 6;
            this.numEditHeight.Value = new decimal(new int[] {
            100,
            0,
            0,
            0});
            // 
            // numEditWidth
            // 
            this.numEditWidth.DecimalPlaces = 1;
            this.numEditWidth.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.numEditWidth.Location = new System.Drawing.Point(127, 73);
            this.numEditWidth.Maximum = new decimal(new int[] {
            1000,
            0,
            0,
            0});
            this.numEditWidth.Minimum = new decimal(new int[] {
            10,
            0,
            0,
            0});
            this.numEditWidth.Name = "numEditWidth";
            this.numEditWidth.Size = new System.Drawing.Size(190, 23);
            this.numEditWidth.TabIndex = 5;
            this.numEditWidth.Value = new decimal(new int[] {
            100,
            0,
            0,
            0});
            // 
            // btnUpdatePaperSize
            // 
            this.btnUpdatePaperSize.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnUpdatePaperSize.Location = new System.Drawing.Point(127, 156);
            this.btnUpdatePaperSize.Name = "btnUpdatePaperSize";
            this.btnUpdatePaperSize.Size = new System.Drawing.Size(190, 35);
            this.btnUpdatePaperSize.TabIndex = 4;
            this.btnUpdatePaperSize.Text = "Update Paper Size";
            this.btnUpdatePaperSize.UseVisualStyleBackColor = true;
            this.btnUpdatePaperSize.Click += new System.EventHandler(this.btnUpdatePaperSize_Click);
            // 
            // btnCancelEdit
            // 
            this.btnCancelEdit.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnCancelEdit.Location = new System.Drawing.Point(127, 197);
            this.btnCancelEdit.Name = "btnCancelEdit";
            this.btnCancelEdit.Size = new System.Drawing.Size(190, 30);
            this.btnCancelEdit.TabIndex = 7;
            this.btnCancelEdit.Text = "Cancel";
            this.btnCancelEdit.UseVisualStyleBackColor = true;
            this.btnCancelEdit.Click += new System.EventHandler(this.btnCancelEdit_Click);
            // 
            // chkEditPermanent
            // 
            this.chkEditPermanent.AutoSize = true;
            this.chkEditPermanent.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chkEditPermanent.Location = new System.Drawing.Point(127, 140);
            this.chkEditPermanent.Name = "chkEditPermanent";
            this.chkEditPermanent.Size = new System.Drawing.Size(234, 17);
            this.chkEditPermanent.TabIndex = 8;
            this.chkEditPermanent.Text = "Simpan permanen ke registry (Admin required)";
            this.chkEditPermanent.UseVisualStyleBackColor = true;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(15, 113);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(78, 15);
            this.label4.TabIndex = 3;
            this.label4.Text = "Height (mm):";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.Location = new System.Drawing.Point(15, 75);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(75, 15);
            this.label5.TabIndex = 2;
            this.label5.Text = "Width (mm):";
            // 
            // txtEditPaperName
            // 
            this.txtEditPaperName.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtEditPaperName.Location = new System.Drawing.Point(127, 35);
            this.txtEditPaperName.Name = "txtEditPaperName";
            this.txtEditPaperName.ReadOnly = true;
            this.txtEditPaperName.Size = new System.Drawing.Size(190, 23);
            this.txtEditPaperName.TabIndex = 1;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.Location = new System.Drawing.Point(15, 38);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(70, 15);
            this.label6.TabIndex = 0;
            this.label6.Text = "Paper Name:";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(744, 561);
            this.Controls.Add(this.groupBoxEditPaperSize);
            this.Controls.Add(this.groupBoxAddPaperSize);
            this.Controls.Add(this.groupBoxPaperSizes);
            this.Controls.Add(this.groupBoxPrinters);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.Name = "Form1";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Printer Management Tool - Detect Printers & Add Paper Sizes";
            this.groupBoxPrinters.ResumeLayout(false);
            this.groupBoxPrinters.PerformLayout();
            this.groupBoxPaperSizes.ResumeLayout(false);
            this.groupBoxPaperSizes.PerformLayout();
            this.groupBoxAddPaperSize.ResumeLayout(false);
            this.groupBoxAddPaperSize.PerformLayout();
            this.groupBoxEditPaperSize.ResumeLayout(false);
            this.groupBoxEditPaperSize.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numHeight)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numWidth)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numEditHeight)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numEditWidth)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBoxPrinters;
        private System.Windows.Forms.ListBox listBoxPrinters;
        private System.Windows.Forms.Button btnRefresh;
        private System.Windows.Forms.GroupBox groupBoxPaperSizes;
        private System.Windows.Forms.ListBox listBoxPaperSizes;
        private System.Windows.Forms.GroupBox groupBoxAddPaperSize;
        private System.Windows.Forms.TextBox txtPaperName;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btnAddPaperSize;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.NumericUpDown numWidth;
        private System.Windows.Forms.NumericUpDown numHeight;
        private System.Windows.Forms.Label labelPrinterCount;
        private System.Windows.Forms.Label labelPaperSizeCount;
        private System.Windows.Forms.Button btnShowDevMode;
        private System.Windows.Forms.Button btnEditPaperSize;
        private System.Windows.Forms.GroupBox groupBoxEditPaperSize;
        private System.Windows.Forms.NumericUpDown numEditHeight;
        private System.Windows.Forms.NumericUpDown numEditWidth;
        private System.Windows.Forms.Button btnUpdatePaperSize;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox txtEditPaperName;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Button btnCancelEdit;
        private System.Windows.Forms.CheckBox chkPermanent;
        private System.Windows.Forms.CheckBox chkEditPermanent;
    }
}
