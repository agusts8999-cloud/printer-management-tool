Imports System.IO.Ports

Public Class Form1

    Dim isOpen = False

    Dim printer = IntPtr.Zero
    Private Sub addMsg(msg)
        tb_Msg.Text += DateTime.Now.ToString("MM-dd HH:mm:ss") + " " + msg + vbCrLf
    End Sub

    Private Sub btn_Open_Click(sender As Object, e As EventArgs) Handles btn_Open.Click
        If isOpen Then
            PrinterDemo.ClosePort(printer)
        End If
        PrinterCreator(printer, "")
        Dim info = ""
        If rb_USB.Checked Then
            info = "USB,"
        ElseIf rb_COM.Checked Then
            info = $"{cb_COMName.Text},{cb_BaudRate.Text}"
        ElseIf rb_NET.Checked Then
            info = $"NET,{tb_IP.Text}"
        ElseIf lptRb.Checked Then
            info = lptCb.Text
        End If
        Dim openStatus = PrinterDemo.OpenPort(printer, info)
        isOpen = openStatus = 0
        If isOpen Then
            addMsg("Open port success!")
            RefreshBtnStatus()
        Else
            addMsg($"Open port fail, Error is {openStatus}")
        End If

    End Sub
    Private Sub btn_Close_Click(sender As Object, e As EventArgs) Handles btn_Close.Click
        If isOpen Then
            addMsg("Close port!")
            PrinterDemo.ClosePort(printer)
            isOpen = False
            RefreshBtnStatus()
        End If

    End Sub
    Private Sub RefreshBtnStatus()
        btn_PrinterStatus.Enabled = isOpen And (Not lptRb.Checked)
        btn_Qrcode.Enabled = isOpen
        btn_Barcode.Enabled = isOpen
        btn_BitMap.Enabled = isOpen
        btn_Text.Enabled = isOpen
        btn_Close.Enabled = isOpen
    End Sub

    Private Sub Form1_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Dim ports As String() = SerialPort.GetPortNames()
        Dim port As String
        For Each port In ports
            cb_COMName.Items.Add(port)
        Next port
        lptCb.Items.Add("LPT1")
        lptCb.Items.Add("LPT2")
        lptCb.Items.Add("LPT3")
    End Sub

    Private Sub Form1_FormClosed(sender As Object, e As FormClosedEventArgs) Handles MyBase.FormClosed
        PrinterDemo.ReleasePrinter(printer)
    End Sub


    Private Sub btn_Barcode_Click(sender As Object, e As EventArgs) Handles btn_Barcode.Click
        addMsg("Print Barcode")
        PrinterDemo.PrintBarCode(printer)
    End Sub

    Private Sub btn_Qrcode_Click(sender As Object, e As EventArgs) Handles btn_Qrcode.Click
        addMsg("Print Qrcode")
        PrinterDemo.PrintQRCode(printer)
    End Sub

    Private Sub btn_Sample_Click(sender As Object, e As EventArgs) Handles btn_Text.Click
        addMsg("Print Sample")
        PrinterDemo.PrintSample(printer)
    End Sub
    Private Sub btn_BitMap_Click(sender As Object, e As EventArgs) Handles btn_BitMap.Click
        Dim fileDialog As New OpenFileDialog()
        fileDialog.InitialDirectory = AppDomain.CurrentDomain.BaseDirectory
        fileDialog.Filter = "Image Files|*.jpg;*.jpeg;*.png;*.bmp"
        fileDialog.FilterIndex = 0
        fileDialog.RestoreDirectory = True
        fileDialog.Title = "Select file"
        If fileDialog.ShowDialog = DialogResult.OK Then
            addMsg("Print Image")
            PrinterDemo.PrintImage(printer, fileDialog.FileName)
        End If
    End Sub

    Private Sub btn_PrinterStatus_Click(sender As Object, e As EventArgs) Handles btn_PrinterStatus.Click
        PrinterDemo.GetStatus(printer, tb_Msg)
    End Sub

    Private Sub tb_Msg_TextChanged(sender As Object, e As EventArgs) Handles tb_Msg.TextChanged
        tb_Msg.SelectionStart = tb_Msg.TextLength
        tb_Msg.ScrollToCaret()
    End Sub

End Class
