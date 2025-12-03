<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class Form1
    Inherits System.Windows.Forms.Form

    <System.Diagnostics.DebuggerNonUserCode()>
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        Try
            If disposing AndAlso components IsNot Nothing Then
                components.Dispose()
            End If
        Finally
            MyBase.Dispose(disposing)
        End Try
    End Sub

    Private components As System.ComponentModel.IContainer

    <System.Diagnostics.DebuggerStepThrough()>
    Private Sub InitializeComponent()
        Me.groupBox2 = New System.Windows.Forms.GroupBox()
        Me.btn_PrinterStatus = New System.Windows.Forms.Button()
        Me.btn_BitMap = New System.Windows.Forms.Button()
        Me.btn_Qrcode = New System.Windows.Forms.Button()
        Me.btn_Barcode = New System.Windows.Forms.Button()
        Me.btn_Text = New System.Windows.Forms.Button()
        Me.tb_Msg = New System.Windows.Forms.TextBox()
        Me.GroupBox3 = New System.Windows.Forms.GroupBox()
        Me.lptCb = New System.Windows.Forms.ComboBox()
        Me.lptRb = New System.Windows.Forms.RadioButton()
        Me.btn_Close = New System.Windows.Forms.Button()
        Me.btn_Open = New System.Windows.Forms.Button()
        Me.tb_IP = New System.Windows.Forms.TextBox()
        Me.cb_BaudRate = New System.Windows.Forms.ComboBox()
        Me.cb_COMName = New System.Windows.Forms.ComboBox()
        Me.rb_NET = New System.Windows.Forms.RadioButton()
        Me.rb_COM = New System.Windows.Forms.RadioButton()
        Me.rb_USB = New System.Windows.Forms.RadioButton()
        Me.groupBox2.SuspendLayout()
        Me.GroupBox3.SuspendLayout()
        Me.SuspendLayout()
        '
        'groupBox2
        '
        Me.groupBox2.Controls.Add(Me.btn_PrinterStatus)
        Me.groupBox2.Controls.Add(Me.btn_BitMap)
        Me.groupBox2.Controls.Add(Me.btn_Qrcode)
        Me.groupBox2.Controls.Add(Me.btn_Barcode)
        Me.groupBox2.Controls.Add(Me.btn_Text)
        Me.groupBox2.Font = New System.Drawing.Font("Arial", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.groupBox2.Location = New System.Drawing.Point(13, 13)
        Me.groupBox2.Name = "groupBox2"
        Me.groupBox2.Size = New System.Drawing.Size(293, 203)
        Me.groupBox2.TabIndex = 22
        Me.groupBox2.TabStop = False
        Me.groupBox2.Text = "Print"
        '
        'btn_PrinterStatus
        '
        Me.btn_PrinterStatus.Enabled = False
        Me.btn_PrinterStatus.Font = New System.Drawing.Font("Arial", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btn_PrinterStatus.Location = New System.Drawing.Point(6, 117)
        Me.btn_PrinterStatus.Name = "btn_PrinterStatus"
        Me.btn_PrinterStatus.Size = New System.Drawing.Size(117, 30)
        Me.btn_PrinterStatus.TabIndex = 15
        Me.btn_PrinterStatus.Text = "PrinterStatus"
        Me.btn_PrinterStatus.UseVisualStyleBackColor = True
        '
        'btn_BitMap
        '
        Me.btn_BitMap.Enabled = False
        Me.btn_BitMap.Font = New System.Drawing.Font("Arial", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btn_BitMap.Location = New System.Drawing.Point(145, 68)
        Me.btn_BitMap.Name = "btn_BitMap"
        Me.btn_BitMap.Size = New System.Drawing.Size(117, 30)
        Me.btn_BitMap.TabIndex = 13
        Me.btn_BitMap.Text = "Print Image"
        Me.btn_BitMap.UseVisualStyleBackColor = True
        '
        'btn_Qrcode
        '
        Me.btn_Qrcode.Enabled = False
        Me.btn_Qrcode.Font = New System.Drawing.Font("Arial", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btn_Qrcode.Location = New System.Drawing.Point(6, 68)
        Me.btn_Qrcode.Name = "btn_Qrcode"
        Me.btn_Qrcode.Size = New System.Drawing.Size(117, 30)
        Me.btn_Qrcode.TabIndex = 9
        Me.btn_Qrcode.Text = "Qrcode"
        Me.btn_Qrcode.UseVisualStyleBackColor = True
        '
        'btn_Barcode
        '
        Me.btn_Barcode.Enabled = False
        Me.btn_Barcode.Font = New System.Drawing.Font("Arial", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btn_Barcode.Location = New System.Drawing.Point(145, 20)
        Me.btn_Barcode.Name = "btn_Barcode"
        Me.btn_Barcode.Size = New System.Drawing.Size(118, 30)
        Me.btn_Barcode.TabIndex = 8
        Me.btn_Barcode.Text = "Barcode"
        Me.btn_Barcode.UseVisualStyleBackColor = True
        '
        'btn_Text
        '
        Me.btn_Text.Enabled = False
        Me.btn_Text.Font = New System.Drawing.Font("Arial", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btn_Text.Location = New System.Drawing.Point(6, 20)
        Me.btn_Text.Name = "btn_Text"
        Me.btn_Text.Size = New System.Drawing.Size(117, 30)
        Me.btn_Text.TabIndex = 7
        Me.btn_Text.Text = "Print Sample"
        Me.btn_Text.UseVisualStyleBackColor = True
        '
        'tb_Msg
        '
        Me.tb_Msg.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.tb_Msg.Location = New System.Drawing.Point(12, 225)
        Me.tb_Msg.Multiline = True
        Me.tb_Msg.Name = "tb_Msg"
        Me.tb_Msg.ScrollBars = System.Windows.Forms.ScrollBars.Both
        Me.tb_Msg.Size = New System.Drawing.Size(294, 186)
        Me.tb_Msg.TabIndex = 21
        '
        'GroupBox3
        '
        Me.GroupBox3.Controls.Add(Me.lptCb)
        Me.GroupBox3.Controls.Add(Me.lptRb)
        Me.GroupBox3.Controls.Add(Me.btn_Close)
        Me.GroupBox3.Controls.Add(Me.btn_Open)
        Me.GroupBox3.Controls.Add(Me.tb_IP)
        Me.GroupBox3.Controls.Add(Me.cb_BaudRate)
        Me.GroupBox3.Controls.Add(Me.cb_COMName)
        Me.GroupBox3.Controls.Add(Me.rb_NET)
        Me.GroupBox3.Controls.Add(Me.rb_COM)
        Me.GroupBox3.Controls.Add(Me.rb_USB)
        Me.GroupBox3.Font = New System.Drawing.Font("Arial", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.GroupBox3.Location = New System.Drawing.Point(315, 13)
        Me.GroupBox3.Margin = New System.Windows.Forms.Padding(5, 4, 5, 4)
        Me.GroupBox3.Name = "GroupBox3"
        Me.GroupBox3.Padding = New System.Windows.Forms.Padding(5, 4, 5, 4)
        Me.GroupBox3.Size = New System.Drawing.Size(268, 398)
        Me.GroupBox3.TabIndex = 20
        Me.GroupBox3.TabStop = False
        Me.GroupBox3.Text = "Select Port"
        '
        'lptCb
        '
        Me.lptCb.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lptCb.FormattingEnabled = True
        Me.lptCb.Location = New System.Drawing.Point(82, 172)
        Me.lptCb.Name = "lptCb"
        Me.lptCb.Size = New System.Drawing.Size(180, 24)
        Me.lptCb.TabIndex = 8
        '
        'lptRb
        '
        Me.lptRb.AutoSize = True
        Me.lptRb.Font = New System.Drawing.Font("Arial", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lptRb.Location = New System.Drawing.Point(16, 172)
        Me.lptRb.Margin = New System.Windows.Forms.Padding(5, 4, 5, 4)
        Me.lptRb.Name = "lptRb"
        Me.lptRb.Size = New System.Drawing.Size(53, 21)
        Me.lptRb.TabIndex = 7
        Me.lptRb.Text = "LPT"
        Me.lptRb.UseVisualStyleBackColor = True
        '
        'btn_Close
        '
        Me.btn_Close.Enabled = False
        Me.btn_Close.Font = New System.Drawing.Font("Arial", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btn_Close.Location = New System.Drawing.Point(171, 255)
        Me.btn_Close.Name = "btn_Close"
        Me.btn_Close.Size = New System.Drawing.Size(75, 30)
        Me.btn_Close.TabIndex = 6
        Me.btn_Close.Text = "close"
        Me.btn_Close.UseVisualStyleBackColor = True
        '
        'btn_Open
        '
        Me.btn_Open.Font = New System.Drawing.Font("Arial", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btn_Open.Location = New System.Drawing.Point(30, 255)
        Me.btn_Open.Name = "btn_Open"
        Me.btn_Open.Size = New System.Drawing.Size(75, 30)
        Me.btn_Open.TabIndex = 6
        Me.btn_Open.Text = "open"
        Me.btn_Open.UseVisualStyleBackColor = True
        '
        'tb_IP
        '
        Me.tb_IP.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.tb_IP.Location = New System.Drawing.Point(81, 126)
        Me.tb_IP.Name = "tb_IP"
        Me.tb_IP.Size = New System.Drawing.Size(181, 22)
        Me.tb_IP.TabIndex = 5
        '
        'cb_BaudRate
        '
        Me.cb_BaudRate.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cb_BaudRate.FormattingEnabled = True
        Me.cb_BaudRate.Items.AddRange(New Object() {"9600", "19200", "38400", "57600", "115200"})
        Me.cb_BaudRate.Location = New System.Drawing.Point(171, 76)
        Me.cb_BaudRate.Name = "cb_BaudRate"
        Me.cb_BaudRate.Size = New System.Drawing.Size(91, 24)
        Me.cb_BaudRate.TabIndex = 4
        '
        'cb_COMName
        '
        Me.cb_COMName.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cb_COMName.FormattingEnabled = True
        Me.cb_COMName.Location = New System.Drawing.Point(82, 76)
        Me.cb_COMName.Name = "cb_COMName"
        Me.cb_COMName.Size = New System.Drawing.Size(83, 24)
        Me.cb_COMName.TabIndex = 3
        '
        'rb_NET
        '
        Me.rb_NET.AutoSize = True
        Me.rb_NET.Font = New System.Drawing.Font("Arial", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.rb_NET.Location = New System.Drawing.Point(14, 127)
        Me.rb_NET.Margin = New System.Windows.Forms.Padding(5, 4, 5, 4)
        Me.rb_NET.Name = "rb_NET"
        Me.rb_NET.Size = New System.Drawing.Size(55, 21)
        Me.rb_NET.TabIndex = 2
        Me.rb_NET.Text = "NET"
        Me.rb_NET.UseVisualStyleBackColor = True
        '
        'rb_COM
        '
        Me.rb_COM.AutoSize = True
        Me.rb_COM.Font = New System.Drawing.Font("Arial", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.rb_COM.Location = New System.Drawing.Point(16, 78)
        Me.rb_COM.Margin = New System.Windows.Forms.Padding(5, 4, 5, 4)
        Me.rb_COM.Name = "rb_COM"
        Me.rb_COM.Size = New System.Drawing.Size(60, 21)
        Me.rb_COM.TabIndex = 1
        Me.rb_COM.Text = "COM"
        Me.rb_COM.UseVisualStyleBackColor = True
        '
        'rb_USB
        '
        Me.rb_USB.AutoSize = True
        Me.rb_USB.Checked = True
        Me.rb_USB.Font = New System.Drawing.Font("Arial", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.rb_USB.Location = New System.Drawing.Point(15, 34)
        Me.rb_USB.Margin = New System.Windows.Forms.Padding(5, 4, 5, 4)
        Me.rb_USB.Name = "rb_USB"
        Me.rb_USB.Size = New System.Drawing.Size(56, 21)
        Me.rb_USB.TabIndex = 0
        Me.rb_USB.TabStop = True
        Me.rb_USB.Text = "USB"
        Me.rb_USB.UseVisualStyleBackColor = True
        '
        'Form1
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 12.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(598, 420)
        Me.Controls.Add(Me.groupBox2)
        Me.Controls.Add(Me.tb_Msg)
        Me.Controls.Add(Me.GroupBox3)
        Me.Name = "Form1"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "WinSDKDemo"
        Me.groupBox2.ResumeLayout(False)
        Me.GroupBox3.ResumeLayout(False)
        Me.GroupBox3.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Private WithEvents groupBox2 As GroupBox
    Private WithEvents btn_PrinterStatus As Button
    Private WithEvents btn_BitMap As Button
    Private WithEvents btn_Qrcode As Button
    Private WithEvents btn_Barcode As Button
    Private WithEvents btn_Text As Button
    Private WithEvents tb_Msg As TextBox
    Private WithEvents GroupBox3 As GroupBox
    Private WithEvents btn_Close As Button
    Private WithEvents btn_Open As Button
    Private WithEvents tb_IP As TextBox
    Private WithEvents cb_BaudRate As ComboBox
    Private WithEvents cb_COMName As ComboBox
    Private WithEvents rb_NET As RadioButton
    Private WithEvents rb_COM As RadioButton
    Private WithEvents rb_USB As RadioButton
    Private WithEvents lptRb As RadioButton
    Private WithEvents lptCb As ComboBox
End Class
