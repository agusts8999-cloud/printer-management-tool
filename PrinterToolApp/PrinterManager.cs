using System;
using System.Collections.Generic;
using System.Drawing.Printing;
using System.Linq;

namespace PrinterToolApp
{
    /// <summary>
    /// Class untuk mengelola operasi terkait printer
    /// </summary>
    public class PrinterManager
    {
        /// <summary>
        /// Nama printer target yang didukung aplikasi
        /// </summary>
        public const string TARGET_PRINTER_NAME = "4BARCODE 4B-2082A";
        
        /// <summary>
        /// URL driver printer (sesuaikan dengan vendor)
        /// </summary>
        public const string DRIVER_DOWNLOAD_URL = "https://www.google.com/search?q=4BARCODE+4B-2082A+driver+download";

        /// <summary>
        /// Mendapatkan daftar semua printer yang terinstall di sistem
        /// </summary>
        /// <returns>List nama printer</returns>
        public static List<string> GetInstalledPrinters()
        {
            List<string> printers = new List<string>();
            
            foreach (string printer in PrinterSettings.InstalledPrinters)
            {
                printers.Add(printer);
            }
            
            return printers;
        }

        /// <summary>
        /// Cek apakah target printer sudah terinstall
        /// </summary>
        /// <returns>True jika printer target ditemukan</returns>
        public static bool IsTargetPrinterInstalled()
        {
            foreach (string printer in PrinterSettings.InstalledPrinters)
            {
                if (printer.Contains(TARGET_PRINTER_NAME))
                {
                    return true;
                }
            }
            return false;
        }

        /// <summary>
        /// Get nama printer target jika ditemukan
        /// </summary>
        /// <returns>Nama printer atau null jika tidak ditemukan</returns>
        public static string GetTargetPrinterName()
        {
            foreach (string printer in PrinterSettings.InstalledPrinters)
            {
                if (printer.Contains(TARGET_PRINTER_NAME))
                {
                    return printer;
                }
            }
            return null;
        }

        /// <summary>
        /// Mendapatkan daftar ukuran kertas yang didukung oleh printer tertentu
        /// </summary>
        /// <param name="printerName">Nama printer</param>
        /// <returns>List ukuran kertas</returns>
        public static List<PaperSize> GetPaperSizes(string printerName)
        {
            List<PaperSize> paperSizes = new List<PaperSize>();
            
            try
            {
                PrinterSettings printerSettings = new PrinterSettings();
                printerSettings.PrinterName = printerName;
                
                foreach (PaperSize paperSize in printerSettings.PaperSizes)
                {
                    paperSizes.Add(paperSize);
                }
            }
            catch (Exception ex)
            {
                throw new Exception($"Error mendapatkan ukuran kertas untuk printer {printerName}: {ex.Message}");
            }
            
            return paperSizes;
        }

        /// <summary>
        /// Mendapatkan informasi detail printer
        /// </summary>
        /// <param name="printerName">Nama printer</param>
        /// <returns>PrinterSettings object</returns>
        public static PrinterSettings GetPrinterSettings(string printerName)
        {
            PrinterSettings settings = new PrinterSettings();
            settings.PrinterName = printerName;
            
            if (!settings.IsValid)
            {
                throw new Exception($"Printer {printerName} tidak valid atau tidak tersedia.");
            }
            
            return settings;
        }

        /// <summary>
        /// Mendapatkan nama default printer
        /// </summary>
        /// <returns>Nama default printer</returns>
        public static string GetDefaultPrinter()
        {
            PrinterSettings settings = new PrinterSettings();
            return settings.PrinterName;
        }

        /// <summary>
        /// Mengecek apakah printer tersedia
        /// </summary>
        /// <param name="printerName">Nama printer</param>
        /// <returns>True jika printer tersedia</returns>
        public static bool IsPrinterAvailable(string printerName)
        {
            PrinterSettings settings = new PrinterSettings();
            settings.PrinterName = printerName;
            return settings.IsValid;
        }
    }
}
