using System;
using System.Drawing;
using System.Drawing.Printing;
using System.Windows.Forms;

namespace PrinterToolApp
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            LoadPrinters();
            UpdateAdminStatus();
        }

        /// <summary>
        /// Update status admin dan enable/disable checkbox
        /// </summary>
        private void UpdateAdminStatus()
        {
            bool isAdmin = PaperSizeManager.IsRunningAsAdministrator();
            string status = isAdmin ? "Running as Administrator ✓" : "Not Admin (permanent save disabled)";
            this.Text = $"Printer Management Tool - {status}";
            
            // Enable checkbox hanya jika running as admin
            chkPermanent.Enabled = isAdmin;
            chkEditPermanent.Enabled = isAdmin;
            
            if (!isAdmin)
            {
                chkPermanent.Checked = false;
                chkEditPermanent.Checked = false;
            }
        }

        /// <summary>
        /// Load daftar printer yang terinstall (hanya target printer)
        /// </summary>
        private void LoadPrinters()
        {
            try
            {
                listBoxPrinters.Items.Clear();
                
                // Cek apakah target printer terinstall
                if (PrinterManager.IsTargetPrinterInstalled())
                {
                    string targetPrinter = PrinterManager.GetTargetPrinterName();
                    listBoxPrinters.Items.Add(targetPrinter);
                    listBoxPrinters.SelectedIndex = 0;
                    
                    labelPrinterCount.Text = $"Printer ditemukan: {PrinterManager.TARGET_PRINTER_NAME}";
                    labelPrinterCount.ForeColor = System.Drawing.Color.Green;
                }
                else
                {
                    // Printer tidak ditemukan - tampilkan instruksi install
                    labelPrinterCount.Text = "Printer tidak ditemukan!";
                    labelPrinterCount.ForeColor = System.Drawing.Color.Red;
                    
                    ShowDriverInstallInstructions();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading printers: {ex.Message}", "Error", 
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// Tampilkan instruksi install driver printer
        /// </summary>
        private void ShowDriverInstallInstructions()
        {
            string instructions = $"Printer {PrinterManager.TARGET_PRINTER_NAME} tidak ditemukan!\n\n" +
                                 "Langkah-langkah install driver:\n\n" +
                                 "1. Download driver dari website vendor\n" +
                                 "2. Extract file driver\n" +
                                 "3. Jalankan installer sebagai Administrator\n" +
                                 "4. Restart aplikasi ini setelah install selesai\n\n" +
                                 "Atau install manual:\n" +
                                 "- Buka Devices and Printers (Control Panel)\n" +
                                 "- Add a printer → Select manually\n" +
                                 "- Pilih port (USB/Network)\n" +
                                 $"- Pilih driver: {PrinterManager.TARGET_PRINTER_NAME}\n\n" +
                                 "Buka halaman download driver?";

            DialogResult result = MessageBox.Show(instructions, "Driver Printer Tidak Ditemukan",
                MessageBoxButtons.YesNo, MessageBoxIcon.Warning);

            if (result == DialogResult.Yes)
            {
                try
                {
                    System.Diagnostics.Process.Start(PrinterManager.DRIVER_DOWNLOAD_URL);
                }
                catch
                {
                    MessageBox.Show($"Tidak dapat membuka browser.\n\nSilakan buka manual:\n{PrinterManager.DRIVER_DOWNLOAD_URL}",
                        "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
        }

        /// <summary>
        /// Load paper sizes untuk printer yang dipilih
        /// </summary>
        private void LoadPaperSizes()
        {
            listBoxPaperSizes.Items.Clear();

            if (listBoxPrinters.SelectedItem == null)
            {
                return;
            }

            try
            {
                string printerName = listBoxPrinters.SelectedItem.ToString();
                var paperSizes = PrinterManager.GetPaperSizes(printerName);

                foreach (var paperSize in paperSizes)
                {
                    // Konversi dari 1/100 inch ke mm
                    double widthMM = paperSize.Width / 100.0 * 25.4;
                    double heightMM = paperSize.Height / 100.0 * 25.4;
                    
                    string displayText = $"{paperSize.PaperName} ({widthMM:F0} x {heightMM:F0} mm)";
                    listBoxPaperSizes.Items.Add(displayText);
                }

                labelPaperSizeCount.Text = $"Total: {paperSizes.Count} paper size(s)";
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading paper sizes: {ex.Message}", "Error", 
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void listBoxPrinters_SelectedIndexChanged(object sender, EventArgs e)
        {
            LoadPaperSizes();
        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            LoadPrinters();
        }

        private void btnAddPaperSize_Click(object sender, EventArgs e)
        {
            if (listBoxPrinters.SelectedItem == null)
            {
                MessageBox.Show("Silakan pilih printer terlebih dahulu.", "Informasi", 
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            string paperName = txtPaperName.Text.Trim();
            
            if (string.IsNullOrWhiteSpace(paperName))
            {
                MessageBox.Show("Nama ukuran kertas tidak boleh kosong.", "Validasi", 
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtPaperName.Focus();
                return;
            }

            double width = (double)numWidth.Value;
            double height = (double)numHeight.Value;

            if (!PaperSizeManager.ValidatePaperSize(width, height))
            {
                MessageBox.Show("Ukuran kertas tidak valid. Ukuran harus antara 10mm - 1000mm.", "Validasi", 
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                string printerName = listBoxPrinters.SelectedItem.ToString();
                bool usePermanent = chkPermanent.Checked;
                bool success = false;

                if (usePermanent)
                {
                    // Simpan ke registry (permanen)
                    success = PaperSizeManager.AddPermanentPaperSize(paperName, width, height);
                    
                    if (success)
                    {
                        MessageBox.Show(
                            $"Ukuran kertas '{paperName}' berhasil ditambahkan secara permanen!\n\n" +
                            $"Ukuran: {width:F1} x {height:F1} mm\n\n" +
                            $"Paper size telah disimpan di Windows Registry dan akan tersedia\n" +
                            $"untuk semua printer setelah restart Print Spooler atau reboot.",
                            "Sukses",
                            MessageBoxButtons.OK,
                            MessageBoxIcon.Information);
                    }
                }
                else
                {
                    // Simpan session-based
                    success = PaperSizeManager.AddCustomPaperSize(printerName, paperName, width, height);
                    
                    if (success)
                    {
                        MessageBox.Show(
                            $"Ukuran kertas '{paperName}' berhasil ditambahkan!\n\n" +
                            $"Catatan: Untuk membuat ukuran kertas permanen,\n" +
                            $"centang opsi 'Simpan permanen' dan jalankan sebagai Administrator.",
                            "Sukses",
                            MessageBoxButtons.OK,
                            MessageBoxIcon.Information);
                    }
                }

                if (success)
                {
                    // Clear input fields
                    txtPaperName.Clear();
                    numWidth.Value = 100;
                    numHeight.Value = 100;

                    // Refresh paper sizes
                    LoadPaperSizes();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Gagal menambahkan ukuran kertas:\n{ex.Message}", "Error", 
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnScanRegistry_Click(object sender, EventArgs e)
        {
            if (listBoxPrinters.SelectedItem == null)
            {
                MessageBox.Show("Silakan pilih printer terlebih dahulu.", "Informasi",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            try
            {
                string printerName = listBoxPrinters.SelectedItem.ToString();
                
                // Scan registry locations
                string scanResult = RegistryScanner.ScanPrinterRegistryLocations(printerName);
                
                // Get driver name
                string driverName = RegistryScanner.GetPrinterDriverName(printerName);
                if (!string.IsNullOrEmpty(driverName))
                {
                    scanResult += "\n" + RegistryScanner.ScanDriverSpecificLocations(driverName);
                }
                
                // Show results
                Form scanForm = new Form
                {
                    Text = "Registry Scan Results",
                    Width = 800,
                    Height = 600,
                    StartPosition = FormStartPosition.CenterParent
                };
                
                TextBox resultBox = new TextBox
                {
                    Multiline = true,
                    ScrollBars = ScrollBars.Both,
                    Dock = DockStyle.Fill,
                    Font = new System.Drawing.Font("Consolas", 9),
                    Text = scanResult,
                    WordWrap = false
                };
                
                scanForm.Controls.Add(resultBox);
                scanForm.ShowDialog();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error scanning registry: {ex.Message}", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnShowDevMode_Click(object sender, EventArgs e)
        {
            if (listBoxPrinters.SelectedItem == null)
            {
                MessageBox.Show("Silakan pilih printer terlebih dahulu.", "Informasi", 
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            try
            {
                string printerName = listBoxPrinters.SelectedItem.ToString();
                var devMode = PaperSizeManager.GetDevMode(printerName);

                if (devMode.HasValue)
                {
                    string info = $"DEVMODE Information for: {printerName}\n\n" +
                                  $"Device Name: {devMode.Value.dmDeviceName}\n" +
                                  $"Form Name: {devMode.Value.dmFormName}\n" +
                                  $"Driver Version: {devMode.Value.dmDriverVersion}\n" +
                                  $"Color: {devMode.Value.dmColor}\n" +
                                  $"Duplex: {devMode.Value.dmDuplex}";

                    MessageBox.Show(info, "Printer Information", 
                        MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    MessageBox.Show("Tidak dapat mengambil DEVMODE untuk printer ini.", "Error", 
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error: {ex.Message}", "Error", 
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnEditPaperSize_Click(object sender, EventArgs e)
        {
            if (listBoxPrinters.SelectedItem == null)
            {
                MessageBox.Show("Silakan pilih printer terlebih dahulu.", "Informasi", 
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            if (listBoxPaperSizes.SelectedItem == null)
            {
                MessageBox.Show("Silakan pilih ukuran kertas yang ingin diedit dari daftar.", "Informasi", 
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            try
            {
                string printerName = listBoxPrinters.SelectedItem.ToString();
                string selectedText = listBoxPaperSizes.SelectedItem.ToString();
                
                // Parse paper size dari format "Name (width x height mm)"
                int startIndex = selectedText.LastIndexOf('(');
                int endIndex = selectedText.LastIndexOf(')');
                
                if (startIndex > 0 && endIndex > startIndex)
                {
                    string paperName = selectedText.Substring(0, startIndex).Trim();
                    string dimensionText = selectedText.Substring(startIndex + 1, endIndex - startIndex - 1);
                    
                    // Parse dimensions "width x height mm"
                    string[] parts = dimensionText.Replace("mm", "").Split('x');
                    if (parts.Length == 2)
                    {
                        double width = double.Parse(parts[0].Trim());
                        double height = double.Parse(parts[1].Trim());
                        
                        // Populate edit form
                        txtEditPaperName.Text = paperName;
                        numEditWidth.Value = (decimal)width;
                        numEditHeight.Value = (decimal)height;
                        
                        // Show edit panel, hide add panel
                        groupBoxEditPaperSize.Visible = true;
                        groupBoxAddPaperSize.Visible = false;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error parsing paper size: {ex.Message}", "Error", 
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnUpdatePaperSize_Click(object sender, EventArgs e)
        {
            if (listBoxPrinters.SelectedItem == null)
            {
                MessageBox.Show("Silakan pilih printer terlebih dahulu.", "Informasi", 
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            string paperName = txtEditPaperName.Text.Trim();
            double width = (double)numEditWidth.Value;
            double height = (double)numEditHeight.Value;

            if (!PaperSizeManager.ValidatePaperSize(width, height))
            {
                MessageBox.Show("Ukuran kertas tidak valid. Ukuran harus antara 10mm - 1000mm.", "Validasi", 
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                string printerName = listBoxPrinters.SelectedItem.ToString();
                bool usePermanent = chkEditPermanent.Checked;
                bool success = false;

                if (usePermanent)
                {
                    // Update di registry (permanen)
                    success = PaperSizeManager.UpdatePermanentPaperSize(paperName, width, height);
                    
                    if (success)
                    {
                        MessageBox.Show(
                            $"Ukuran kertas '{paperName}' berhasil diperbarui secara permanen!\n\n" +
                            $"Ukuran baru: {width:F1} x {height:F1} mm\n\n" +
                            $"Perubahan telah disimpan di Windows Registry.",
                            "Sukses",
                            MessageBoxButtons.OK,
                            MessageBoxIcon.Information);
                    }
                }
                else
                {
                    // Update session-based
                    success = PaperSizeManager.AddCustomPaperSize(printerName, paperName, width, height);
                    
                    if (success)
                    {
                        MessageBox.Show(
                            $"Ukuran kertas '{paperName}' berhasil diperbarui!\n\n" +
                            $"Ukuran baru: {width:F1} x {height:F1} mm\n\n" +
                            $"Catatan: Perubahan ini berlaku untuk session saat ini.\n" +
                            $"Untuk perubahan permanen, centang opsi dan jalankan sebagai Administrator.",
                            "Sukses",
                            MessageBoxButtons.OK,
                            MessageBoxIcon.Information);
                    }
                }

                if (success)
                {
                    // Hide edit panel, show add panel
                    groupBoxEditPaperSize.Visible = false;
                    groupBoxAddPaperSize.Visible = true;

                    // Refresh paper sizes
                    LoadPaperSizes();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Gagal memperbarui ukuran kertas:\n{ex.Message}", "Error", 
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnCancelEdit_Click(object sender, EventArgs e)
        {
            // Hide edit panel, show add panel
            groupBoxEditPaperSize.Visible = false;
            groupBoxAddPaperSize.Visible = true;
        }

    }
}
