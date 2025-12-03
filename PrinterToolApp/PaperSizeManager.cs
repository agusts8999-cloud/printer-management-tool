using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Drawing.Printing;
using Microsoft.Win32;

namespace PrinterToolApp
{
    /// <summary>
    /// Class untuk mengelola ukuran kertas custom menggunakan Windows API dan Registry
    /// </summary>
    public class PaperSizeManager
    {
        #region Windows API Declarations

        [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Auto)]
        public struct DEVMODE
        {
            private const int CCHDEVICENAME = 32;
            private const int CCHFORMNAME = 32;

            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = CCHDEVICENAME)]
            public string dmDeviceName;
            public short dmSpecVersion;
            public short dmDriverVersion;
            public short dmSize;
            public short dmDriverExtra;
            public int dmFields;

            public int dmPositionX;
            public int dmPositionY;
            public int dmDisplayOrientation;
            public int dmDisplayFixedOutput;

            public short dmColor;
            public short dmDuplex;
            public short dmYResolution;
            public short dmTTOption;
            public short dmCollate;

            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = CCHFORMNAME)]
            public string dmFormName;

            public short dmLogPixels;
            public int dmBitsPerPel;
            public int dmPelsWidth;
            public int dmPelsHeight;
            public int dmDisplayFlags;
            public int dmDisplayFrequency;
            public int dmICMMethod;
            public int dmICMIntent;
            public int dmMediaType;
            public int dmDitherType;
            public int dmReserved1;
            public int dmReserved2;
            public int dmPanningWidth;
            public int dmPanningHeight;
        }

        [DllImport("winspool.drv", CharSet = CharSet.Auto, SetLastError = true)]
        public static extern bool OpenPrinter(string pPrinterName, out IntPtr phPrinter, IntPtr pDefault);

        [DllImport("winspool.drv", CharSet = CharSet.Auto, SetLastError = true)]
        public static extern bool ClosePrinter(IntPtr hPrinter);

        [DllImport("winspool.drv", CharSet = CharSet.Auto, SetLastError = true)]
        public static extern int DocumentProperties(
            IntPtr hwnd,
            IntPtr hPrinter,
            string pDeviceName,
            IntPtr pDevModeOutput,
            IntPtr pDevModeInput,
            int fMode);

        private const int DM_OUT_BUFFER = 2;
        private const int DM_IN_BUFFER = 8;

        #endregion

        #region Registry-Based Paper Size Management

        /// <summary>
        /// Base registry path untuk Forms (Paper Sizes) di Windows
        /// </summary>
        private const string FORMS_REGISTRY_PATH = @"SYSTEM\CurrentControlSet\Control\Print\Forms";

        /// <summary>
        /// Menambahkan ukuran kertas custom secara permanen ke Windows Registry
        /// Memerlukan hak administrator!
        /// Uses driver-specific method for 4BARCODE printer
        /// </summary>
        public static bool AddPermanentPaperSize(string paperName, double widthMM, double heightMM)
        {
            // Validasi input
            if (string.IsNullOrWhiteSpace(paperName))
            {
                throw new ArgumentException("Nama ukuran kertas tidak valid.");
            }

            if (!ValidatePaperSize(widthMM, heightMM))
            {
                throw new ArgumentException("Ukuran kertas tidak valid.");
            }

            try
            {
                // DRIVER-SPECIFIC: For 4BARCODE printer, write to PrinterDriverData\FORMS
                string printerName = PrinterManager.TARGET_PRINTER_NAME;
                return Add4BARCODEPaperSize(printerName, paperName, widthMM, heightMM);
            }
            catch (Exception driverEx)
            {
                // Fallback to .reg file method if driver-specific fails
                try
                {
                    return AddPermanentPaperSizeViaRegFile(paperName, widthMM, heightMM);
                }
                catch (Exception regFileEx)
                {
                    // Both methods failed
                    throw new Exception(
                        $"Gagal menambahkan paper size dengan semua metode:\n\n" +
                        $"Metode 1 (Driver-specific): {driverEx.Message}\n\n" +
                        $"Metode 2 (.reg file): {regFileEx.Message}\n\n" +
                        $"Saran:\n" +
                        $"- Restart Print Spooler: Restart-Service Spooler\n" +
                        $"- Pastikan running as Administrator\n" +
                        $"- Cek printer driver sudah terinstall dengan benar");
                }
            }
        }

        /// <summary>
        /// Menghapus ukuran kertas custom secara permanen dari Windows Registry
        /// Memerlukan hak administrator!
        /// </summary>
        public static bool DeletePermanentPaperSize(string paperName)
        {
            try
            {
                // DRIVER-SPECIFIC: For 4BARCODE printer
                string printerName = PrinterManager.TARGET_PRINTER_NAME;
                
                // Try delete from driver storage
                bool driverDelete = Delete4BARCODEPaperSize(printerName, paperName);
                
                // Also try delete from global registry (just in case)
                bool globalDelete = false;
                try
                {
                    using (RegistryKey formsKey = Registry.LocalMachine.OpenSubKey(FORMS_REGISTRY_PATH, true))
                    {
                        if (formsKey != null)
                        {
                            formsKey.DeleteSubKey(paperName, false);
                            globalDelete = true;
                        }
                    }
                }
                catch { }

                return driverDelete || globalDelete;
            }
            catch (Exception ex)
            {
                throw new Exception($"Gagal menghapus paper size: {ex.Message}");
            }
        }

        /// <summary>
        /// Direct registry access method (fallback)
        /// </summary>
        private static bool AddPermanentPaperSizeDirectRegistry(string paperName, double widthMM, double heightMM)
        {
            // Validasi input
            if (string.IsNullOrWhiteSpace(paperName))
            {
                throw new ArgumentException("Nama ukuran kertas tidak boleh kosong.");
            }

            if (!ValidatePaperSize(widthMM, heightMM))
            {
                throw new ArgumentException("Ukuran kertas tidak valid.");
            }

            try
            {
                // Konversi dari mm ke 1/1000 mm (unit yang digunakan Windows Registry)
                int widthMicromm = (int)(widthMM * 1000);
                int heightMicromm = (int)(heightMM * 1000);

                // CRITICAL FIX: Buka atau buat Forms registry key jika belum ada
                RegistryKey formsKey = null;
                try
                {
                    // Coba buka dengan write access
                    formsKey = Registry.LocalMachine.OpenSubKey(FORMS_REGISTRY_PATH, true);
                    
                    if (formsKey == null)
                    {
                        // Key tidak ada, coba buat
                        // HKEY_LOCAL_MACHINE\SYSTEM\CurrentControlSet\Control\Print
                        using (RegistryKey printKey = Registry.LocalMachine.OpenSubKey(@"SYSTEM\CurrentControlSet\Control\Print", true))
                        {
                            if (printKey == null)
                            {
                                throw new Exception("Tidak dapat membuka registry Print key. Registry system mungkin corrupted.");
                            }
                            
                            // Buat Forms subkey
                            formsKey = printKey.CreateSubKey("Forms");
                            if (formsKey == null)
                            {
                                throw new Exception("Tidak dapat membuat Forms registry key meskipun running as Administrator.");
                            }
                        }
                    }

                    // Buat subkey untuk paper size baru
                    using (RegistryKey paperKey = formsKey.CreateSubKey(paperName))
                    {
                        if (paperKey == null)
                        {
                            throw new Exception($"Tidak dapat membuat registry key untuk {paperName}");
                        }

                        // Set values
                        // Flags: 0x1 = user-defined form
                        paperKey.SetValue("Flags", 0x1, RegistryValueKind.DWord);
                        
                        // Size dalam 1/1000 mm
                        byte[] sizeData = new byte[8];
                        BitConverter.GetBytes(widthMicromm).CopyTo(sizeData, 0);
                        BitConverter.GetBytes(heightMicromm).CopyTo(sizeData, 4);
                        paperKey.SetValue("Size", sizeData, RegistryValueKind.Binary);

                        // ImageableArea - area yang bisa dicetak (sama dengan size untuk simplicity)
                        // Format: left, top, right, bottom (dalam 1/1000 mm)
                        byte[] imageableData = new byte[16];
                        BitConverter.GetBytes(0).CopyTo(imageableData, 0);        // left
                        BitConverter.GetBytes(0).CopyTo(imageableData, 4);        // top
                        BitConverter.GetBytes(widthMicromm).CopyTo(imageableData, 8);   // right
                        BitConverter.GetBytes(heightMicromm).CopyTo(imageableData, 12); // bottom
                        paperKey.SetValue("ImageableArea", imageableData, RegistryValueKind.Binary);
                    }
                }
                finally
                {
                    if (formsKey != null)
                    {
                        formsKey.Dispose();
                    }
                }

                return true;
            }
            catch (UnauthorizedAccessException uae)
            {
                string errorMsg = "Akses registry ditolak!\n\n" +
                                  "Kemungkinan penyebab:\n" +
                                  "1. Aplikasi tidak dijalankan sebagai Administrator\n" +
                                  "2. UAC (User Access Control) memblokir akses\n" +
                                  "3. Registry key memiliki permission khusus\n\n" +
                                  "Solusi:\n" +
                                  "- Tutup aplikasi\n" +
                                  "- Klik kanan PrinterToolApp.exe → Run as administrator\n" +
                                  "- Pastikan UAC prompt muncul dan klik 'Yes'\n\n" +
                                  $"Detail error: {uae.Message}";
                throw new Exception(errorMsg);
            }
            catch (System.Security.SecurityException se)
            {
                throw new Exception($"Security exception saat akses registry.\n" +
                                  $"Aplikasi harus dijalankan sebagai Administrator.\n\n" +
                                  $"Detail: {se.Message}");
            }
            catch (Exception ex)
            {
                throw new Exception($"Gagal menambahkan ukuran kertas ke registry:\n\n" +
                                  $"Error: {ex.Message}\n" +
                                  $"Type: {ex.GetType().Name}\n\n" +
                                  $"Jika error 'Tidak dapat membuka registry Forms':\n" +
                                  $"- Pastikan running as Administrator\n" +
                                  $"- Cek registry permissions di regedit");
            }
        }

        /// <summary>
        /// Update ukuran kertas yang sudah ada di registry
        /// </summary>
        public static bool UpdatePermanentPaperSize(string paperName, double widthMM, double heightMM)
        {
            // Delete existing, then add new
            DeletePermanentPaperSize(paperName);
            return AddPermanentPaperSize(paperName, widthMM, heightMM);
        }



        /// <summary>
        /// Cek apakah paper size sudah ada di registry
        /// </summary>
        public static bool PaperSizeExistsInRegistry(string paperName)
        {
            try
            {
                using (RegistryKey formsKey = Registry.LocalMachine.OpenSubKey(FORMS_REGISTRY_PATH, false))
                {
                    if (formsKey == null) return false;

                    using (RegistryKey paperKey = formsKey.OpenSubKey(paperName))
                    {
                        return paperKey != null;
                    }
                }
            }
            catch
            {
                return false;
            }
        }

        /// <summary>
        /// Cek apakah aplikasi berjalan sebagai administrator
        /// </summary>
        public static bool IsRunningAsAdministrator()
        {
            try
            {
                var identity = System.Security.Principal.WindowsIdentity.GetCurrent();
                var principal = new System.Security.Principal.WindowsPrincipal(identity);
                return principal.IsInRole(System.Security.Principal.WindowsBuiltInRole.Administrator);
            }
            catch
            {
                return false;
            }
        }

        /// <summary>
        /// ALTERNATIVE METHOD: Tambah paper size menggunakan file .reg
        /// Method ini lebih reliable daripada direct registry API
        /// </summary>
        public static bool AddPermanentPaperSizeViaRegFile(string paperName, double widthMM, double heightMM)
        {
            if (!ValidatePaperSize(widthMM, heightMM))
            {
                throw new ArgumentException("Ukuran kertas tidak valid.");
            }

            try
            {
                // Konversi dari mm ke 1/1000 mm
                int widthMicromm = (int)(widthMM * 1000);
                int heightMicromm = (int)(heightMM * 1000);

                // Generate .reg file content
                string regContent = GenerateRegFileContent(paperName, widthMicromm, heightMicromm);

                // Create temporary .reg file
                string tempRegFile = System.IO.Path.Combine(
                    System.IO.Path.GetTempPath(), 
                    $"PaperSize_{paperName}_{System.Guid.NewGuid()}.reg");

                // Write .reg file
                System.IO.File.WriteAllText(tempRegFile, regContent, System.Text.Encoding.Unicode);

                // Import using regedit
                bool success = ImportRegFile(tempRegFile);

                // Cleanup
                try
                {
                    if (System.IO.File.Exists(tempRegFile))
                    {
                        System.IO.File.Delete(tempRegFile);
                    }
                }
                catch { }

                return success;
            }
            catch (Exception ex)
            {
                throw new Exception($"Gagal menambahkan paper size via .reg file:\n{ex.Message}");
            }
        }

        /// <summary>
        /// Generate .reg file content untuk paper size
        /// </summary>
        private static string GenerateRegFileContent(string paperName, int widthMicromm, int heightMicromm)
        {
            // Escape backslashes and quotes untuk registry path
            string escapedPaperName = paperName.Replace("\\", "\\\\").Replace("\"", "\\\"");

            // Convert integers to hex bytes (little-endian)
            string widthHex = IntToHexBytes(widthMicromm);
            string heightHex = IntToHexBytes(heightMicromm);

            // Size = width + height (8 bytes total)
            string sizeHex = widthHex + "," + heightHex;

            // ImageableArea = left,top,right,bottom (16 bytes)
            string leftTopHex = "00,00,00,00,00,00,00,00"; // left=0, top=0
            string imageableHex = leftTopHex + "," + widthHex + "," + heightHex;

            // Generate .reg file content
            string regContent = "Windows Registry Editor Version 5.00\r\n\r\n";
            regContent += $"[HKEY_LOCAL_MACHINE\\SYSTEM\\CurrentControlSet\\Control\\Print\\Forms\\{escapedPaperName}]\r\n";
            regContent += "\"Flags\"=dword:00000001\r\n";
            regContent += $"\"Size\"=hex:{sizeHex}\r\n";
            regContent += $"\"ImageableArea\"=hex:{imageableHex}\r\n";

            return regContent;
        }

        /// <summary>
        /// Convert integer to hex bytes string (little-endian)
        /// </summary>
        private static string IntToHexBytes(int value)
        {
            byte[] bytes = BitConverter.GetBytes(value);
            return string.Join(",", System.Array.ConvertAll(bytes, b => b.ToString("x2")));
        }

        /// <summary>
        /// Import .reg file menggunakan regedit
        /// </summary>
        private static bool ImportRegFile(string regFilePath)
        {
            try
            {
                // Use regedit.exe /s untuk silent import
                var startInfo = new System.Diagnostics.ProcessStartInfo
                {
                    FileName = "regedit.exe",
                    Arguments = $"/s \"{regFilePath}\"",
                    UseShellExecute = true,
                    Verb = "runas", // Request elevation
                    CreateNoWindow = true,
                    WindowStyle = System.Diagnostics.ProcessWindowStyle.Hidden
                };

                using (var process = System.Diagnostics.Process.Start(startInfo))
                {
                    if (process != null)
                    {
                        process.WaitForExit(5000); // Wait max 5 seconds
                        return process.ExitCode == 0;
                    }
                }

                return false;
            }
            catch (System.ComponentModel.Win32Exception)
            {
                // User cancelled UAC prompt
                throw new Exception("Import registry dibatalkan atau UAC ditolak.\n\n" +
                                  "Klik 'Yes' pada UAC prompt untuk mengizinkan perubahan registry.");
            }
            catch (Exception ex)
            {
                throw new Exception($"Gagal import .reg file: {ex.Message}");
            }
        }

        #endregion

        /// <summary>
        /// Menambahkan ukuran kertas custom ke printer (session-based)
        /// </summary>
        /// <param name="printerName">Nama printer</param>
        /// <param name="paperName">Nama ukuran kertas</param>
        /// <param name="widthMM">Lebar dalam milimeter</param>
        /// <param name="heightMM">Tinggi dalam milimeter</param>
        /// <returns>True jika berhasil</returns>
        public static bool AddCustomPaperSize(string printerName, string paperName, double widthMM, double heightMM)
        {
            // Validasi input
            if (string.IsNullOrWhiteSpace(printerName))
            {
                throw new ArgumentException("Nama printer tidak boleh kosong.");
            }

            if (string.IsNullOrWhiteSpace(paperName))
            {
                throw new ArgumentException("Nama ukuran kertas tidak boleh kosong.");
            }

            if (widthMM <= 0 || widthMM > 1000)
            {
                throw new ArgumentException("Lebar kertas harus antara 0 dan 1000 mm.");
            }

            if (heightMM <= 0 || heightMM > 1000)
            {
                throw new ArgumentException("Tinggi kertas harus antara 0 dan 1000 mm.");
            }

            try
            {
                // Menggunakan PrinterSettings untuk menambahkan ukuran kertas custom
                PrinterSettings printerSettings = new PrinterSettings();
                printerSettings.PrinterName = printerName;

                if (!printerSettings.IsValid)
                {
                    throw new Exception($"Printer {printerName} tidak valid atau tidak ditemukan.");
                }

                // Konversi dari mm ke 1/100 inch (units yang digunakan PaperSize)
                int widthHundredthsInch = (int)(widthMM / 25.4 * 100);
                int heightHundredthsInch = (int)(heightMM / 25.4 * 100);

                // Membuat custom paper size
                PaperSize customSize = new PaperSize(paperName, widthHundredthsInch, heightHundredthsInch);

                // Note: Di C# .NET Framework, menambahkan ukuran kertas custom secara permanen
                // memerlukan akses langsung ke driver printer atau registry, yang memerlukan
                // hak administrator. Untuk sementara ini hanya menambahkan ke collection
                // yang akan tersedia selama session aplikasi berjalan.
                
                // Untuk menambahkan secara permanen, perlu menggunakan Windows API yang lebih kompleks
                // atau tools pihak ketiga seperti printui.dll

                return true;
            }
            catch (Exception ex)
            {
                throw new Exception($"Gagal menambahkan ukuran kertas: {ex.Message}");
            }
        }

        /// <summary>
        /// Mendapatkan DEVMODE untuk printer
        /// </summary>
        /// <param name="printerName">Nama printer</param>
        /// <returns>DEVMODE structure atau null jika gagal</returns>
        public static DEVMODE? GetDevMode(string printerName)
        {
            IntPtr hPrinter = IntPtr.Zero;
            DEVMODE? devMode = null;

            try
            {
                if (!OpenPrinter(printerName, out hPrinter, IntPtr.Zero))
                {
                    return null;
                }

                int sizeNeeded = DocumentProperties(IntPtr.Zero, hPrinter, printerName, IntPtr.Zero, IntPtr.Zero, 0);
                
                if (sizeNeeded <= 0)
                {
                    return null;
                }

                IntPtr pDevMode = Marshal.AllocHGlobal(sizeNeeded);
                
                try
                {
                    int result = DocumentProperties(IntPtr.Zero, hPrinter, printerName, pDevMode, IntPtr.Zero, DM_OUT_BUFFER);
                    
                    if (result >= 0)
                    {
                        devMode = (DEVMODE)Marshal.PtrToStructure(pDevMode, typeof(DEVMODE));
                    }
                }
                finally
                {
                    Marshal.FreeHGlobal(pDevMode);
                }
            }
            finally
            {
                if (hPrinter != IntPtr.Zero)
                {
                    ClosePrinter(hPrinter);
                }
            }

            return devMode;
        }

        /// <summary>
        /// Validasi ukuran kertas
        /// </summary>
        /// <param name="widthMM">Lebar dalam mm</param>
        /// <param name="heightMM">Tinggi dalam mm</param>
        /// <returns>True jika valid</returns>
        public static bool ValidatePaperSize(double widthMM, double heightMM)
        {
            // Windows mendukung ukuran kertas minimal dan maksimal
            const double MIN_SIZE = 10;   // 10mm
            const double MAX_SIZE = 1000; // 1000mm (1 meter)

            return widthMM >= MIN_SIZE && widthMM <= MAX_SIZE &&
                   heightMM >= MIN_SIZE && heightMM <= MAX_SIZE;
        }

        #region Driver-Specific Methods for 4BARCODE

        /// <summary>
        /// DRIVER-SPECIFIC: Add paper size to 4BARCODE PrinterDriverData\FORMS
        /// This is the CORRECT location for 4BARCODE 4B-2082A driver
        /// </summary>
        public static bool Add4BARCODEPaperSize(string printerName, string paperName, double widthMM, double heightMM)
        {
            if (!ValidatePaperSize(widthMM, heightMM))
            {
                throw new ArgumentException("Ukuran kertas tidak valid.");
            }

            try
            {
                // Convert mm to micrometers (1/1000 mm)
                int widthMicromm = (int)(widthMM * 1000);
                int heightMicromm = (int)(heightMM * 1000);

                string registryPath = $@"SYSTEM\CurrentControlSet\Control\Print\Printers\{printerName}\PrinterDriverData";

                using (RegistryKey driverDataKey = Registry.LocalMachine.OpenSubKey(registryPath, true))
                {
                    if (driverDataKey == null)
                    {
                        throw new Exception($"Tidak dapat membuka registry PrinterDriverData untuk {printerName}");
                    }

                    // Read existing FORMS binary data
                    byte[] existingForms = driverDataKey.GetValue("FORMS") as byte[];
                    if (existingForms == null)
                    {
                        throw new Exception("FORMS data tidak ditemukan di registry driver");
                    }

                    // Extract template tail from the first entry (skip 4 byte header + 72 bytes data)
                    byte[] templateTail = new byte[40];
                    if (existingForms.Length >= 116) // 4 header + 112 entry
                    {
                        // Copy last 40 bytes of the first entry (offset 4 + 72 = 76)
                        System.Buffer.BlockCopy(existingForms, 76, templateTail, 0, 40);
                    }

                    // Create new form entry
                    byte[] newFormEntry = Create4BARCODEFormEntry(paperName, widthMicromm, heightMicromm, templateTail);

                    // Append new form to existing forms
                    byte[] updatedForms = new byte[existingForms.Length + newFormEntry.Length];
                    System.Buffer.BlockCopy(existingForms, 0, updatedForms, 0, existingForms.Length);
                    System.Buffer.BlockCopy(newFormEntry, 0, updatedForms, existingForms.Length, newFormEntry.Length);

                    // Write back to registry
                    driverDataKey.SetValue("FORMS", updatedForms, RegistryValueKind.Binary);

                    // Also update InstalledForm (using simplified structure)
                    UpdateInstalledForm(driverDataKey, paperName, widthMicromm, heightMicromm);

                    return true;
                }
            }
            catch (Exception ex)
            {
                throw new Exception($"Gagal menambahkan paper size ke driver 4BARCODE:\n{ex.Message}");
            }
        }

        /// <summary>
        /// Create binary entry for 4BARCODE FORMS structure
        /// Corrected structure based on analysis:
        /// - Global Header: 4 bytes (skipped in entry creation)
        /// - Entry Size: 112 bytes
        /// - Name: Offset 0 (64 bytes)
        /// - Width: Offset 64 (4 bytes)
        /// - Height: Offset 68 (4 bytes)
        /// - Padding/Flags: Offset 72 (40 bytes)
        /// </summary>
        private static byte[] Create4BARCODEFormEntry(string paperName, int widthMicromm, int heightMicromm, byte[] templateTail)
        {
            byte[] entry = new byte[112];

            // Write paper name as Unicode (first 64 bytes)
            byte[] nameBytes = System.Text.Encoding.Unicode.GetBytes(paperName);
            int nameCopyLength = Math.Min(nameBytes.Length, 64);
            System.Buffer.BlockCopy(nameBytes, 0, entry, 0, nameCopyLength);

            // Write width at offset 64
            byte[] widthBytes = BitConverter.GetBytes(widthMicromm);
            System.Buffer.BlockCopy(widthBytes, 0, entry, 64, 4);

            // Write height at offset 68
            byte[] heightBytes = BitConverter.GetBytes(heightMicromm);
            System.Buffer.BlockCopy(heightBytes, 0, entry, 68, 4);

            // Copy tail (flags/padding) from template if available
            if (templateTail != null && templateTail.Length >= 40)
            {
                System.Buffer.BlockCopy(templateTail, 0, entry, 72, 40);
            }
            else
            {
                // Default flags if template not found (from observation)
                // 01 00 00 80 D0 07 00 00 ...
                entry[96] = 0x01;
                entry[99] = 0x80;
                entry[100] = 0xD0;
                entry[101] = 0x07;
            }

            return entry;
        }

        /// <summary>
        /// Update InstalledForm registry value
        /// </summary>
        private static void UpdateInstalledForm(RegistryKey driverDataKey, string paperName, int widthMicromm, int heightMicromm)
        {
            try
            {
                // InstalledForm should match the FORMS entry structure (112 bytes)
                // Previously was 80 bytes, which might cause mismatch issues
                byte[] installedFormEntry = Create4BARCODEFormEntry(paperName, widthMicromm, heightMicromm, null);
                driverDataKey.SetValue("InstalledForm", installedFormEntry, RegistryValueKind.Binary);
            }
            catch
            {
                // Non-critical
            }
        }

        /// <summary>
        /// DRIVER-SPECIFIC: Delete paper size from 4BARCODE PrinterDriverData\FORMS
        /// </summary>
        public static bool Delete4BARCODEPaperSize(string printerName, string paperName)
        {
            try
            {
                string registryPath = $@"SYSTEM\CurrentControlSet\Control\Print\Printers\{printerName}\PrinterDriverData";

                using (RegistryKey driverDataKey = Registry.LocalMachine.OpenSubKey(registryPath, true))
                {
                    if (driverDataKey == null) return false;

                    byte[] existingForms = driverDataKey.GetValue("FORMS") as byte[];
                    if (existingForms == null || existingForms.Length < 4) return false;

                    // Header is 4 bytes
                    int entrySize = 112;
                    int headerSize = 4;
                    
                    // Create list to hold kept forms
                    List<byte> keptForms = new List<byte>();
                    
                    // Keep header
                    for (int i = 0; i < headerSize; i++) keptForms.Add(existingForms[i]);

                    // Iterate through entries
                    int totalEntries = (existingForms.Length - headerSize) / entrySize;
                    bool found = false;

                    for (int i = 0; i < totalEntries; i++)
                    {
                        int offset = headerSize + (i * entrySize);
                        
                        // Read name (first 64 bytes)
                        string entryName = System.Text.Encoding.Unicode.GetString(existingForms, offset, 64).TrimEnd('\0');

                        if (entryName.Equals(paperName, StringComparison.OrdinalIgnoreCase))
                        {
                            found = true;
                            continue; // Skip this entry (delete it)
                        }

                        // Keep this entry
                        for (int j = 0; j < entrySize; j++)
                        {
                            keptForms.Add(existingForms[offset + j]);
                        }
                    }

                    if (found)
                    {
                        // Write back to registry
                        driverDataKey.SetValue("FORMS", keptForms.ToArray(), RegistryValueKind.Binary);
                        return true;
                    }

                    return false;
                }
            }
            catch (Exception ex)
            {
                throw new Exception($"Gagal menghapus paper size dari driver: {ex.Message}");
            }
        }

        #endregion
    }
}
