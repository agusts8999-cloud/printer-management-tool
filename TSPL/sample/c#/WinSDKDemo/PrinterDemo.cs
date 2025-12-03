using System;
using System.Runtime.InteropServices;
using System.Text;

namespace WinSDKDemo
{
    public class PrinterDemo
    {
        [DllImport("printer.sdk.dll", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
        public static extern IntPtr PrinterCreator(ref IntPtr printer, string model);

        [DllImport("printer.sdk.dll", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
        public static extern int ReleasePrinter(IntPtr intPtr);

        [DllImport("printer.sdk.dll", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Auto)]
        public static extern int OpenPort(IntPtr intPtr, string usb);
        [DllImport("printer.sdk.dll", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Auto)]
        public static extern int ClosePort(IntPtr intPtr);
        [DllImport("printer.sdk.dll", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
        public static extern int WriteData(IntPtr intPtr, byte[] buffer, int size);

        [DllImport("printer.sdk.dll", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
        public static extern int ReadData(IntPtr intPtr, byte[] buffer, int size);
        [DllImport("printer.sdk.dll", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
        public static extern int TSPL_Text(IntPtr intPtr, int x, int y, string fontName, byte[] content, int rotation = 0, int x_multiplication = 1, int y_multiplication = 1, int alignment = 0);

        [DllImport("printer.sdk.dll", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
        public static extern int TSPL_Print(IntPtr intPtr, int num, int copies);
        [DllImport("printer.sdk.dll", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
        public static extern int TSPL_Direction(IntPtr intPtr, int direction, int mirror);

        [DllImport("printer.sdk.dll", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
        public static extern int TSPL_Bar(IntPtr intPtr, int x, int y, int width, int height);
        [DllImport("printer.sdk.dll", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
        public static extern int TSPL_BarCode(IntPtr intPtr, int x, int y, int barcodeType, byte[] data, int height, int showText, int rotation, int narrow, int wide);

        [DllImport("printer.sdk.dll", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Unicode)]
        public static extern int TSPL_ImageW(IntPtr intPtr, int x, int y, int mode, string filePath);
        [DllImport("printer.sdk.dll", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
        public static extern int TSPL_Setup(IntPtr intPtr, int printSpeed, int printDensity, int labelWidth, int labelHeight, int labelType, int gapHight, int offset);
        [DllImport("printer.sdk.dll", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
        public static extern int TSPL_ClearBuffer(IntPtr intPtr);
        [DllImport("printer.sdk.dll", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
        public static extern int TSPL_Box(IntPtr intPtr, int x, int y, int x_end, int y_end, int thickness = 1, int radius = 0);

        [DllImport("printer.sdk.dll", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
        public static extern int TSPL_QrCode(IntPtr intPtr, int x, int y, int width, int eccLevel, int mode, int rotate, int model, int mask, byte[] date);

        [DllImport("printer.sdk.dll", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
        public static extern int TSPL_Home(IntPtr intPtr);

        [DllImport("printer.sdk.dll", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
        public static extern int TSPL_GetPrinterStatus(IntPtr intPtr, out int printerStatus);

        [DllImport("printer.sdk.dll", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
        public static extern int TSPL_PDF417(IntPtr intPtr, int x, int y, int width, int height, int rotate, string option, byte[] data);
        [DllImport("printer.sdk.dll", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
        public static extern int TSPL_Block(IntPtr intPtr, int x, int y, int width, int height, string fontName, byte[] data, int rotation, int x_multiplication = 1, int y_multiplication = 1, int alignment = 0);

        [DllImport("printer.sdk.dll", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
        public static extern int TSPL_Dmatrix(IntPtr intPtr, int x, int y, int width, int height, byte[] data, int blockSize = 0, int row = 10, int col = 10);
        public delegate void MsgCallback(string message);

        private static string ParseStatus(int status)
        {
            if (status == 0)
            {
                return "Normal!";
            }
            else if ((status & 0b1) > 0)
            {
                return "Head opened!";
            }
            else if ((status & 0b10) > 0)
            {
                return "Paper jam!";
            }
            else if ((status & 0b100) > 0)
            {
                return "Out of paper!";
            }
            else if ((status & 0b1000) > 0)
            {
                return "Out of ribbon!";
            }
            else if ((status & 0b10000) > 0)
            {
                return "Pause!";
            }
            else if ((status & 0b100000) > 0)
            {
                return "Printing!";
            }
            else if ((status & 0b1000000) > 0)
            {
                return "Cover opened!";
            }
            else
            {
                return "Other error!";
            }
        }

        public static void GetStatus(IntPtr printer, MsgCallback callback)
        {
            int ret = PrinterDemo.TSPL_GetPrinterStatus(printer, out int status);
            if (ret == 0)
            {
                callback($"The printer status is {ParseStatus(status)}");
            }
            else
            {
                callback($"Get Error, Code is: {ret}");
            }
        }

        private static byte[] encodeingString(string str)
        {
            return Encoding.Default.GetBytes(str);
        }
        public static void PrintSample(IntPtr printer)
        {
            TSPL_Setup(printer, 4, 8, 76, 30, 1, 2, 4);
            TSPL_ClearBuffer(printer);
            TSPL_Direction(printer, 0, 0);
            TSPL_Box(printer, 6, 6, 384, 235, 5);
            TSPL_Box(printer, 16, 16, 376, 225, 5);
            TSPL_BarCode(printer, 30, 30, 7, encodeingString("ABCDEFGH"), 100, 0, 0, 2, 2);
            TSPL_QrCode(printer, 265, 30, 4, 1, 0, 0, 1, 2, encodeingString("test qrcode"));
            TSPL_Text(printer, 200, 144, "3", encodeingString("Test EN"));
            TSPL_Text(printer, 38, 165, "3", encodeingString("Test EN"), 0, 1, 2);
            TSPL_Bar(printer, 200, 183, 166, 30);
            TSPL_Bar(printer, 334, 145, 30, 30);
            TSPL_Print(printer, 1, 1);
        }

        public static void PrintQRCode(IntPtr printer)
        {
            TSPL_Setup(printer, 4, 8, 76, 30, 1, 0, 0);
            TSPL_ClearBuffer(printer);
            TSPL_QrCode(printer, 265, 30, 8, 1, 0, 0, 1, 2, encodeingString("test qrcode"));
            TSPL_Print(printer, 1, 1);
        }

        public static void PrintBarCode(IntPtr printer)
        {
            TSPL_Setup(printer, 4, 8, 76, 30, 1, 0, 0);
            TSPL_ClearBuffer(printer);
            TSPL_BarCode(printer, 30, 30, 7, encodeingString("ABCDEFGH"), 100, 2, 0, 2, 2);
            TSPL_Print(printer, 1, 1);
        }

        public static void PrintImage(IntPtr printer, string path)
        {
            TSPL_Setup(printer, 4, 8, 76, 80, 1, 2, 0);
            TSPL_ClearBuffer(printer);
            TSPL_ImageW(printer, 10, 50, 0, path);
            TSPL_Print(printer, 1, 1);
        }
    }
}
