using System;
using System.Collections.Generic;
using Microsoft.Win32;
using System.Text;

namespace PrinterToolApp
{
    /// <summary>
    /// Utility untuk scan registry dan cari lokasi storage printer-specific
    /// </summary>
    public class RegistryScanner
    {
        /// <summary>
        /// Scan semua lokasi registry yang potensial menyimpan paper size untuk printer
        /// </summary>
        public static string ScanPrinterRegistryLocations(string printerName)
        {
            StringBuilder result = new StringBuilder();
            result.AppendLine($"=== REGISTRY SCAN FOR: {printerName} ===\n");

            // Location 1: Global Forms
            result.AppendLine("1. GLOBAL FORMS:");
            result.AppendLine(@"   HKLM\SYSTEM\CurrentControlSet\Control\Print\Forms");
            ScanRegistryKey(@"SYSTEM\CurrentControlSet\Control\Print\Forms", result);

            // Location 2: Printer-specific registry
            result.AppendLine("\n2. PRINTER-SPECIFIC:");
            string printerPath = $@"SYSTEM\CurrentControlSet\Control\Print\Printers\{printerName}";
            result.AppendLine($"   HKLM\\{printerPath}");
            ScanRegistryKey(printerPath, result);

            // Location 3: Driver-specific
            result.AppendLine("\n3. PRINTER DRIVERS:");
            ScanRegistryKey(@"SYSTEM\CurrentControlSet\Control\Print\Environments\Windows x64\Drivers", result, 1);

            // Location 4: Current user preferences
            result.AppendLine("\n4. USER-SPECIFIC SETTINGS:");
            ScanCurrentUserPrinterSettings(printerName, result);

            // Location 5: DevModes storage
            result.AppendLine("\n5. DEVMODE STORAGE:");
            string devModePath = $@"SYSTEM\CurrentControlSet\Control\Print\Printers\{printerName}\PrinterDriverData";
            result.AppendLine($"   HKLM\\{devModePath}");
            ScanRegistryKey(devModePath, result);

            return result.ToString();
        }

        private static void ScanRegistryKey(string path, StringBuilder result, int maxDepth = 0)
        {
            try
            {
                using (RegistryKey key = Registry.LocalMachine.OpenSubKey(path))
                {
                    if (key == null)
                    {
                        result.AppendLine("   [Not Found]");
                        return;
                    }

                    result.AppendLine("   [EXISTS]");
                    
                    // List values
                    string[] valueNames = key.GetValueNames();
                    if (valueNames.Length > 0)
                    {
                        result.AppendLine("   Values:");
                        foreach (string valueName in valueNames)
                        {
                            object value = key.GetValue(valueName);
                            string valueType = key.GetValueKind(valueName).ToString();
                            result.AppendLine($"     - {valueName} ({valueType})");
                        }
                    }

                    // List subkeys
                    string[] subKeyNames = key.GetSubKeyNames();
                    if (subKeyNames.Length > 0 && maxDepth > 0)
                    {
                        result.AppendLine("   Subkeys:");
                        foreach (string subKeyName in subKeyNames)
                        {
                            result.AppendLine($"     - {subKeyName}");
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                result.AppendLine($"   [ERROR: {ex.Message}]");
            }
        }

        private static void ScanCurrentUserPrinterSettings(string printerName, StringBuilder result)
        {
            try
            {
                string userPath = $@"Software\Microsoft\Windows NT\CurrentVersion\PrinterPorts";
                result.AppendLine($"   HKCU\\{userPath}");
                
                using (RegistryKey key = Registry.CurrentUser.OpenSubKey(userPath))
                {
                    if (key != null)
                    {
                        object value = key.GetValue(printerName);
                        if (value != null)
                        {
                            result.AppendLine($"     {printerName} = {value}");
                        }
                        else
                        {
                            result.AppendLine("     [Printer not found in user settings]");
                        }
                    }
                    else
                    {
                        result.AppendLine("     [Key not found]");
                    }
                }
            }
            catch (Exception ex)
            {
                result.AppendLine($"   [ERROR: {ex.Message}]");
            }
        }

        /// <summary>
        /// Cari semua forms/paper sizes yang ada di registry
        /// </summary>
        public static List<string> FindAllPaperSizesInRegistry()
        {
            List<string> paperSizes = new List<string>();

            try
            {
                using (RegistryKey formsKey = Registry.LocalMachine.OpenSubKey(@"SYSTEM\CurrentControlSet\Control\Print\Forms"))
                {
                    if (formsKey != null)
                    {
                        string[] subKeyNames = formsKey.GetSubKeyNames();
                        paperSizes.AddRange(subKeyNames);
                    }
                }
            }
            catch { }

            return paperSizes;
        }

        /// <summary>
        /// Get driver name untuk printer tertentu
        /// </summary>
        public static string GetPrinterDriverName(string printerName)
        {
            try
            {
                string printerPath = $@"SYSTEM\CurrentControlSet\Control\Print\Printers\{printerName}";
                using (RegistryKey key = Registry.LocalMachine.OpenSubKey(printerPath))
                {
                    if (key != null)
                    {
                        object driverValue = key.GetValue("Printer Driver");
                        if (driverValue != null)
                        {
                            return driverValue.ToString();
                        }
                    }
                }
            }
            catch { }

            return null;
        }

        /// <summary>
        /// Scan lokasi driver-specific registry
        /// </summary>
        public static string ScanDriverSpecificLocations(string driverName)
        {
            StringBuilder result = new StringBuilder();
            result.AppendLine($"=== DRIVER-SPECIFIC SCAN: {driverName} ===\n");

            // Driver Version 3 (modern drivers)
            string driverPath = $@"SYSTEM\CurrentControlSet\Control\Print\Environments\Windows x64\Drivers\Version-3\{driverName}";
            result.AppendLine($"Driver Path: HKLM\\{driverPath}");
            ScanRegistryKey(driverPath, result);

            return result.ToString();
        }
    }
}
