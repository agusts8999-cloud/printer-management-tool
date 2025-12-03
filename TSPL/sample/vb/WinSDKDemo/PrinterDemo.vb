Imports System.IO
Imports System.Runtime.InteropServices

Module PrinterDemo

    Public Const POSPRINTERR As String = "printer.sdk.dll"

    <DllImport(POSPRINTERR, CharSet:=CharSet.Unicode, CallingConvention:=CallingConvention.Cdecl)>
    Public Function PrinterCreator(ByRef printer As IntPtr, model As String) As Integer
    End Function

    <DllImport(POSPRINTERR, CharSet:=CharSet.Unicode, CallingConvention:=CallingConvention.Cdecl)>
    Public Function OpenPort(printer As IntPtr, setting As String) As Integer

    End Function
    <DllImport(POSPRINTERR, CharSet:=CharSet.Unicode, CallingConvention:=CallingConvention.Cdecl)>
    Public Function ClosePort(printer As IntPtr) As Integer
    End Function

    <DllImport(POSPRINTERR, CharSet:=CharSet.Ansi, CallingConvention:=CallingConvention.Cdecl)>
    Public Function ReleasePrinter(printer As IntPtr) As Integer
    End Function

    <DllImport(POSPRINTERR, CharSet:=CharSet.Ansi, CallingConvention:=CallingConvention.Cdecl)>
    Public Function ReadData(printer As IntPtr, buffer As Byte(), size As Integer) As Integer
    End Function

    <DllImport(POSPRINTERR, CharSet:=CharSet.Ansi, CallingConvention:=CallingConvention.Cdecl)>
    Public Function WriteData(printer As IntPtr, buffer As Byte(), size As Integer) As Integer
    End Function

    <DllImport(POSPRINTERR, CharSet:=CharSet.Ansi, CallingConvention:=CallingConvention.Cdecl)>
    Public Function TSPL_Text(printer As IntPtr, x As Integer, y As Integer, fontName As String, content As String, rotation As Integer, x_multiplication As Integer, y_multiplication As Integer, alignment As Integer) As Integer
    End Function
    <DllImport(POSPRINTERR, CharSet:=CharSet.Ansi, CallingConvention:=CallingConvention.Cdecl)>
    Public Function TSPL_Print(printer As IntPtr, num As Integer, copies As Integer) As Integer
    End Function

    <DllImport(POSPRINTERR, CharSet:=CharSet.Ansi, CallingConvention:=CallingConvention.Cdecl)>
    Public Function TSPL_Direction(printer As IntPtr, direction As Integer, mirror As Integer) As Integer
    End Function
    <DllImport(POSPRINTERR, CharSet:=CharSet.Ansi, CallingConvention:=CallingConvention.Cdecl)>
    Public Function TSPL_Bar(printer As IntPtr, x As Integer, y As Integer, width As Integer, height As Integer) As Integer
    End Function

    <DllImport(POSPRINTERR, CharSet:=CharSet.Ansi, CallingConvention:=CallingConvention.Cdecl)>
    Public Function TSPL_BarCode(printer As IntPtr, x As Integer, y As Integer, barcodeType As Integer, data As String, height As Integer, showText As Integer, rotation As Integer, narrow As Integer, wide As Integer) As Integer
    End Function

    <DllImport(POSPRINTERR, CharSet:=CharSet.Unicode, CallingConvention:=CallingConvention.Cdecl)>
    Public Function TSPL_ImageW(printer As IntPtr, x As Integer, y As Integer, mode As Integer, filePath As String) As Integer
    End Function

    <DllImport(POSPRINTERR, CharSet:=CharSet.Ansi, CallingConvention:=CallingConvention.Cdecl)>
    Public Function TSPL_Setup(printer As IntPtr, printSpeed As Integer, printDensity As Integer, labelWidth As Integer, labelHeight As Integer, labelType As Integer, gapHight As Integer, offset As Integer) As Integer
    End Function

    <DllImport(POSPRINTERR, CharSet:=CharSet.Ansi, CallingConvention:=CallingConvention.Cdecl)>
    Public Function TSPL_ClearBuffer(printer As IntPtr) As Integer
    End Function

    <DllImport(POSPRINTERR, CharSet:=CharSet.Ansi, CallingConvention:=CallingConvention.Cdecl)>
    Public Function TSPL_Box(printer As IntPtr, x As Integer, y As Integer, x_end As Integer, y_end As Integer, thickness As Integer, radius As Integer) As Integer
    End Function

    <DllImport(POSPRINTERR, CharSet:=CharSet.Ansi, CallingConvention:=CallingConvention.Cdecl)>
    Public Function TSPL_QrCode(printer As IntPtr, x As Integer, y As Integer, width As Integer, eccLevel As Integer, mode As Integer, rotate As Integer, model As Integer, mask As Integer, data As String) As Integer
    End Function

    <DllImport(POSPRINTERR, CharSet:=CharSet.Ansi, CallingConvention:=CallingConvention.Cdecl)>
    Public Function TSPL_Home(printer As IntPtr) As Integer
    End Function

    <DllImport(POSPRINTERR, CharSet:=CharSet.Ansi, CallingConvention:=CallingConvention.Cdecl)>
    Public Function TSPL_GetPrinterStatus(printer As IntPtr, ByRef printerStatus As Integer) As Integer
    End Function

    <DllImport(POSPRINTERR, CharSet:=CharSet.Ansi, CallingConvention:=CallingConvention.Cdecl)>
    Public Function TSPL_PDF417(printer As IntPtr, x As Integer, y As Integer, width As Integer, height As Integer, rotate As Integer, [option] As String, data As String) As Integer
    End Function
    <DllImport(POSPRINTERR, CharSet:=CharSet.Ansi, CallingConvention:=CallingConvention.Cdecl)>
    Public Function TSPL_Block(printer As IntPtr, x As Integer, y As Integer, width As Integer, height As Integer, fontName As String, data As String, rotation As Integer, x_multiplication As Integer, y_multiplication As Integer, alignment As Integer) As Integer
    End Function

    <DllImport(POSPRINTERR, CharSet:=CharSet.Ansi, CallingConvention:=CallingConvention.Cdecl)>
    Public Function TSPL_Dmatrix(printer As IntPtr, x As Integer, y As Integer, width As Integer, height As Integer, data As String, blockSize As Integer, row As Integer, col As Integer) As Integer
    End Function

    Private Function ParseStatus(status) As String
        If status = 0 Then
            Return "Normal!"
        ElseIf (status And &B1) > 0 Then
            Return "Head opened!"
        ElseIf (status And &B10) > 0 Then
            Return "Paper jam!"
        ElseIf (status And &B100) > 0 Then
            Return "Out of paper!"
        ElseIf (status And &B1000) > 0 Then
            Return "Out of ribbon!"
        ElseIf (status And &B10000) > 0 Then
            Return "Pause!"
        ElseIf (status And &B100000) > 0 Then
            Return "Printing!"
        ElseIf (status And &B1000000) > 0 Then
            Return "Cover opened!"
        Else
            Return "Other error!"
        End If
    End Function

    Public Sub GetStatus(printer, tb_Msg)
        Dim status As Integer
        Dim ret = TSPL_GetPrinterStatus(printer, status)
        If ret = 0 Then
            tb_Msg.Text += DateTime.Now.ToString("MM-dd HH:mm:ss") + " The printer status is " + ParseStatus(status) + vbCrLf
        Else
            tb_Msg.Text += DateTime.Now.ToString("MM-dd HH:mm:ss") + " " + "Get Error, Code is: " + ret + vbCrLf
        End If
    End Sub
    Public Sub PrintSample(printer)
        TSPL_Setup(printer, 4, 8, 76, 30, 1, 2, 4)
        TSPL_ClearBuffer(printer)
        TSPL_Direction(printer, 0, 0)
        TSPL_Box(printer, 6, 6, 384, 235, 5, 0)
        TSPL_Box(printer, 16, 16, 376, 225, 5, 0)
        TSPL_BarCode(printer, 30, 30, 7, "ABCDEFGH", 100, 0, 0, 2, 2)
        TSPL_QrCode(printer, 265, 30, 4, 1, 0, 0, 1, 2, "test qrcode")
        TSPL_Text(printer, 200, 144, "3", "Test EN", 0, 1, 1, 0)
        TSPL_Text(printer, 38, 165, "3", "Test EN", 0, 1, 2, 0)
        TSPL_Bar(printer, 200, 183, 166, 30)
        TSPL_Bar(printer, 334, 145, 30, 30)
        TSPL_Print(printer, 1, 1)
    End Sub
    Public Sub PrintQRCode(printer)
        TSPL_Setup(printer, 4, 8, 76, 30, 1, 0, 0)
        TSPL_ClearBuffer(printer)
        TSPL_QrCode(printer, 265, 30, 8, 1, 0, 0, 1, 2, "test qrcode")
        TSPL_Print(printer, 1, 1)
    End Sub
    Public Sub PrintBarCode(printer)
        TSPL_Setup(printer, 4, 8, 76, 30, 1, 0, 0)
        TSPL_ClearBuffer(printer)
        TSPL_BarCode(printer, 30, 30, 7, "ABCDEFGH", 100, 2, 0, 2, 2)
        TSPL_Print(printer, 1, 1)
    End Sub
    Public Sub PrintImage(printer, path)
        TSPL_Setup(printer, 4, 8, 76, 80, 1, 2, 0)
        TSPL_ClearBuffer(printer)
        TSPL_ImageW(printer, 10, 50, 0, path)
        TSPL_Print(printer, 1, 1)
    End Sub

End Module
