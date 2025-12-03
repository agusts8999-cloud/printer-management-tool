namespace WinSDKDemo
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
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.cb_lpt = new System.Windows.Forms.ComboBox();
            this.rb_lpt = new System.Windows.Forms.RadioButton();
            this.btn_Close = new System.Windows.Forms.Button();
            this.btn_Open = new System.Windows.Forms.Button();
            this.tb_IP = new System.Windows.Forms.TextBox();
            this.cb_BaudRate = new System.Windows.Forms.ComboBox();
            this.cb_COMName = new System.Windows.Forms.ComboBox();
            this.rb_NET = new System.Windows.Forms.RadioButton();
            this.rb_COM = new System.Windows.Forms.RadioButton();
            this.rb_USB = new System.Windows.Forms.RadioButton();
            this.tb_Msg = new System.Windows.Forms.TextBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.btn_PrinterStatus = new System.Windows.Forms.Button();
            this.btn_Image = new System.Windows.Forms.Button();
            this.btn_Qrcode = new System.Windows.Forms.Button();
            this.btn_Barcode = new System.Windows.Forms.Button();
            this.btn_Sample = new System.Windows.Forms.Button();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.cb_lpt);
            this.groupBox1.Controls.Add(this.rb_lpt);
            this.groupBox1.Controls.Add(this.btn_Close);
            this.groupBox1.Controls.Add(this.btn_Open);
            this.groupBox1.Controls.Add(this.tb_IP);
            this.groupBox1.Controls.Add(this.cb_BaudRate);
            this.groupBox1.Controls.Add(this.cb_COMName);
            this.groupBox1.Controls.Add(this.rb_NET);
            this.groupBox1.Controls.Add(this.rb_COM);
            this.groupBox1.Controls.Add(this.rb_USB);
            this.groupBox1.Font = new System.Drawing.Font("Arial", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox1.Location = new System.Drawing.Point(306, 13);
            this.groupBox1.Margin = new System.Windows.Forms.Padding(5, 4, 5, 4);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Padding = new System.Windows.Forms.Padding(5, 4, 5, 4);
            this.groupBox1.Size = new System.Drawing.Size(276, 398);
            this.groupBox1.TabIndex = 17;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Select Port";
            // 
            // cb_lpt
            // 
            this.cb_lpt.FormattingEnabled = true;
            this.cb_lpt.Location = new System.Drawing.Point(90, 169);
            this.cb_lpt.Name = "cb_lpt";
            this.cb_lpt.Size = new System.Drawing.Size(176, 25);
            this.cb_lpt.TabIndex = 8;
            // 
            // rb_lpt
            // 
            this.rb_lpt.AutoSize = true;
            this.rb_lpt.Font = new System.Drawing.Font("Arial", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rb_lpt.Location = new System.Drawing.Point(24, 173);
            this.rb_lpt.Margin = new System.Windows.Forms.Padding(5, 4, 5, 4);
            this.rb_lpt.Name = "rb_lpt";
            this.rb_lpt.Size = new System.Drawing.Size(53, 21);
            this.rb_lpt.TabIndex = 7;
            this.rb_lpt.Tag = "3";
            this.rb_lpt.Text = "LPT";
            this.rb_lpt.UseVisualStyleBackColor = true;
            this.rb_lpt.CheckedChanged += new System.EventHandler(this.rb_CheckedChanged);
            // 
            // btn_Close
            // 
            this.btn_Close.Enabled = false;
            this.btn_Close.Font = new System.Drawing.Font("Arial", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_Close.Location = new System.Drawing.Point(175, 266);
            this.btn_Close.Name = "btn_Close";
            this.btn_Close.Size = new System.Drawing.Size(91, 30);
            this.btn_Close.TabIndex = 6;
            this.btn_Close.Text = "close";
            this.btn_Close.UseVisualStyleBackColor = true;
            this.btn_Close.Click += new System.EventHandler(this.btn_Close_Click);
            // 
            // btn_Open
            // 
            this.btn_Open.Font = new System.Drawing.Font("Arial", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_Open.Location = new System.Drawing.Point(24, 266);
            this.btn_Open.Name = "btn_Open";
            this.btn_Open.Size = new System.Drawing.Size(87, 30);
            this.btn_Open.TabIndex = 6;
            this.btn_Open.Text = "open";
            this.btn_Open.UseVisualStyleBackColor = true;
            this.btn_Open.Click += new System.EventHandler(this.btn_Open_Click);
            // 
            // tb_IP
            // 
            this.tb_IP.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tb_IP.Location = new System.Drawing.Point(90, 123);
            this.tb_IP.Name = "tb_IP";
            this.tb_IP.Size = new System.Drawing.Size(176, 22);
            this.tb_IP.TabIndex = 5;
            // 
            // cb_BaudRate
            // 
            this.cb_BaudRate.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cb_BaudRate.FormattingEnabled = true;
            this.cb_BaudRate.Items.AddRange(new object[] {
            "9600",
            "19200",
            "38400",
            "57600",
            "115200"});
            this.cb_BaudRate.Location = new System.Drawing.Point(175, 74);
            this.cb_BaudRate.Name = "cb_BaudRate";
            this.cb_BaudRate.Size = new System.Drawing.Size(91, 24);
            this.cb_BaudRate.TabIndex = 4;
            // 
            // cb_COMName
            // 
            this.cb_COMName.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cb_COMName.FormattingEnabled = true;
            this.cb_COMName.Location = new System.Drawing.Point(90, 74);
            this.cb_COMName.Name = "cb_COMName";
            this.cb_COMName.Size = new System.Drawing.Size(68, 24);
            this.cb_COMName.TabIndex = 3;
            // 
            // rb_NET
            // 
            this.rb_NET.AutoSize = true;
            this.rb_NET.Font = new System.Drawing.Font("Arial", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rb_NET.Location = new System.Drawing.Point(24, 124);
            this.rb_NET.Margin = new System.Windows.Forms.Padding(5, 4, 5, 4);
            this.rb_NET.Name = "rb_NET";
            this.rb_NET.Size = new System.Drawing.Size(55, 21);
            this.rb_NET.TabIndex = 2;
            this.rb_NET.Tag = "2";
            this.rb_NET.Text = "NET";
            this.rb_NET.UseVisualStyleBackColor = true;
            this.rb_NET.CheckedChanged += new System.EventHandler(this.rb_CheckedChanged);
            // 
            // rb_COM
            // 
            this.rb_COM.AutoSize = true;
            this.rb_COM.Font = new System.Drawing.Font("Arial", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rb_COM.Location = new System.Drawing.Point(24, 76);
            this.rb_COM.Margin = new System.Windows.Forms.Padding(5, 4, 5, 4);
            this.rb_COM.Name = "rb_COM";
            this.rb_COM.Size = new System.Drawing.Size(60, 21);
            this.rb_COM.TabIndex = 1;
            this.rb_COM.Tag = "1";
            this.rb_COM.Text = "COM";
            this.rb_COM.UseVisualStyleBackColor = true;
            this.rb_COM.CheckedChanged += new System.EventHandler(this.rb_CheckedChanged);
            // 
            // rb_USB
            // 
            this.rb_USB.AutoSize = true;
            this.rb_USB.Checked = true;
            this.rb_USB.Font = new System.Drawing.Font("Arial", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rb_USB.Location = new System.Drawing.Point(24, 34);
            this.rb_USB.Margin = new System.Windows.Forms.Padding(5, 4, 5, 4);
            this.rb_USB.Name = "rb_USB";
            this.rb_USB.Size = new System.Drawing.Size(56, 21);
            this.rb_USB.TabIndex = 0;
            this.rb_USB.TabStop = true;
            this.rb_USB.Tag = "0";
            this.rb_USB.Text = "USB";
            this.rb_USB.UseVisualStyleBackColor = true;
            this.rb_USB.CheckedChanged += new System.EventHandler(this.rb_CheckedChanged);
            // 
            // tb_Msg
            // 
            this.tb_Msg.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tb_Msg.Location = new System.Drawing.Point(12, 225);
            this.tb_Msg.Multiline = true;
            this.tb_Msg.Name = "tb_Msg";
            this.tb_Msg.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.tb_Msg.Size = new System.Drawing.Size(286, 186);
            this.tb_Msg.TabIndex = 18;
            this.tb_Msg.TextChanged += new System.EventHandler(this.tb_Msg_TextChanged);
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.btn_PrinterStatus);
            this.groupBox2.Controls.Add(this.btn_Image);
            this.groupBox2.Controls.Add(this.btn_Qrcode);
            this.groupBox2.Controls.Add(this.btn_Barcode);
            this.groupBox2.Controls.Add(this.btn_Sample);
            this.groupBox2.Font = new System.Drawing.Font("Arial", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox2.Location = new System.Drawing.Point(12, 13);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(286, 205);
            this.groupBox2.TabIndex = 19;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Print";
            // 
            // btn_PrinterStatus
            // 
            this.btn_PrinterStatus.Enabled = false;
            this.btn_PrinterStatus.Font = new System.Drawing.Font("Arial", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_PrinterStatus.Location = new System.Drawing.Point(6, 115);
            this.btn_PrinterStatus.Name = "btn_PrinterStatus";
            this.btn_PrinterStatus.Size = new System.Drawing.Size(117, 30);
            this.btn_PrinterStatus.TabIndex = 15;
            this.btn_PrinterStatus.Text = "Printer Status";
            this.btn_PrinterStatus.UseVisualStyleBackColor = true;
            this.btn_PrinterStatus.Click += new System.EventHandler(this.btn_PrinterStatus_Click);
            // 
            // btn_Image
            // 
            this.btn_Image.Enabled = false;
            this.btn_Image.Font = new System.Drawing.Font("Arial", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_Image.Location = new System.Drawing.Point(135, 68);
            this.btn_Image.Name = "btn_Image";
            this.btn_Image.Size = new System.Drawing.Size(118, 30);
            this.btn_Image.TabIndex = 10;
            this.btn_Image.Text = "Print Image";
            this.btn_Image.UseVisualStyleBackColor = true;
            this.btn_Image.Click += new System.EventHandler(this.btn_Image_Click);
            // 
            // btn_Qrcode
            // 
            this.btn_Qrcode.Enabled = false;
            this.btn_Qrcode.Font = new System.Drawing.Font("Arial", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_Qrcode.Location = new System.Drawing.Point(6, 68);
            this.btn_Qrcode.Name = "btn_Qrcode";
            this.btn_Qrcode.Size = new System.Drawing.Size(117, 30);
            this.btn_Qrcode.TabIndex = 9;
            this.btn_Qrcode.Text = "Qrcode";
            this.btn_Qrcode.UseVisualStyleBackColor = true;
            this.btn_Qrcode.Click += new System.EventHandler(this.btn_Qrcode_Click);
            // 
            // btn_Barcode
            // 
            this.btn_Barcode.Enabled = false;
            this.btn_Barcode.Font = new System.Drawing.Font("Arial", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_Barcode.Location = new System.Drawing.Point(135, 20);
            this.btn_Barcode.Name = "btn_Barcode";
            this.btn_Barcode.Size = new System.Drawing.Size(118, 30);
            this.btn_Barcode.TabIndex = 8;
            this.btn_Barcode.Text = "Barcode";
            this.btn_Barcode.UseVisualStyleBackColor = true;
            this.btn_Barcode.Click += new System.EventHandler(this.btn_Barcode_Click);
            // 
            // btn_Sample
            // 
            this.btn_Sample.Enabled = false;
            this.btn_Sample.Font = new System.Drawing.Font("Arial", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_Sample.Location = new System.Drawing.Point(6, 20);
            this.btn_Sample.Name = "btn_Sample";
            this.btn_Sample.Size = new System.Drawing.Size(117, 30);
            this.btn_Sample.TabIndex = 7;
            this.btn_Sample.Text = "Print Sample";
            this.btn_Sample.UseVisualStyleBackColor = true;
            this.btn_Sample.Click += new System.EventHandler(this.btn_Sample_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(596, 427);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.tb_Msg);
            this.Controls.Add(this.groupBox1);
            this.Name = "Form1";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "WindowsSDK_Demo";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.Form2_FormClosed);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button btn_Close;
        private System.Windows.Forms.Button btn_Open;
        private System.Windows.Forms.TextBox tb_IP;
        private System.Windows.Forms.ComboBox cb_BaudRate;
        private System.Windows.Forms.ComboBox cb_COMName;
        private System.Windows.Forms.RadioButton rb_NET;
        private System.Windows.Forms.RadioButton rb_COM;
        private System.Windows.Forms.RadioButton rb_USB;
        private System.Windows.Forms.TextBox tb_Msg;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Button btn_PrinterStatus;
        private System.Windows.Forms.Button btn_Image;
        private System.Windows.Forms.Button btn_Qrcode;
        private System.Windows.Forms.Button btn_Barcode;
        private System.Windows.Forms.Button btn_Sample;
        private System.Windows.Forms.RadioButton rb_lpt;
        private System.Windows.Forms.ComboBox cb_lpt;
    }
}