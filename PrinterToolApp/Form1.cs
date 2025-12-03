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
                    
                    DialogResult result = MessageBox.Show(
                        $"Printer '{PrinterManager.TARGET_PRINTER_NAME}' tidak ditemukan.\n\n" +
                        "Apakah Anda ingin menginstall driver printer sekarang?",
                        "Driver Tidak Ditemukan",
                        MessageBoxButtons.YesNo,
                        MessageBoxIcon.Question);

                    if (result == DialogResult.Yes)
                    {
                        InstallDriver();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading printers: {ex.Message}", "Error", 
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void InstallDriver()
        {
            try
            {
                // Resource names can be tricky. Default namespace + folder (if any) + filename
                // Since file is in root of project, it should be PrinterToolApp.4barcode.driver.2024.11.14.1.exe
                // But sometimes underscores are used for numbers or special chars.
                
                string tempPath = System.IO.Path.Combine(System.IO.Path.GetTempPath(), "4barcode.driver.2024.11.14.1.exe");
                System.IO.Stream stream = null;
                string foundResourceName = null;

                // Get all resources
                string[] resources = System.Reflection.Assembly.GetExecutingAssembly().GetManifestResourceNames();
                
                // Find resource that ends with our filename
                foreach (string res in resources)
                {
                    if (res.EndsWith("4barcode.driver.2024.11.14.1.exe", StringComparison.OrdinalIgnoreCase))
                    {
                        foundResourceName = res;
                        break;
                    }
                }

                if (foundResourceName != null)
                {
                    stream = System.Reflection.Assembly.GetExecutingAssembly().GetManifestResourceStream(foundResourceName);
                }

                if (stream == null)
                {
                    // List all resources to debug
                    string availableResources = string.Join("\n", resources);
                    throw new Exception($"Embedded driver resource not found.\nAvailable resources:\n{availableResources}");
                }

                using (stream)
                {
                    using (System.IO.FileStream fileStream = new System.IO.FileStream(tempPath, System.IO.FileMode.Create))
                    {
                        stream.CopyTo(fileStream);
                    }
                }

                System.Diagnostics.Process.Start(tempPath);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Gagal mengekstrak atau menjalankan installer driver:\n{ex.Message}", "Error", 
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
                
                // Reset UI
                btnEditPaperSize.Enabled = false;
                btnDeletePaperSize.Visible = false;
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

        private void listBoxPaperSizes_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (listBoxPaperSizes.SelectedItem == null)
            {
                btnEditPaperSize.Enabled = false;
                btnDeletePaperSize.Visible = false;
                return;
            }

            // Enable edit button
            btnEditPaperSize.Enabled = true;
            
            // Show delete button
            btnDeletePaperSize.Visible = true;

            // Set tag for delete button
            string selectedPaper = listBoxPaperSizes.SelectedItem.ToString();
            
            // Extract paper name for Tag (assuming format "Name" or "Name (details)")
            string paperName = selectedPaper;
            int parenIndex = selectedPaper.LastIndexOf('(');
            if (parenIndex > 0)
            {
                paperName = selectedPaper.Substring(0, parenIndex).Trim();
            }
            
            btnDeletePaperSize.Tag = paperName;
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

        private void btnDeletePaperSize_Click(object sender, EventArgs e)
        {
            if (btnDeletePaperSize.Tag == null) return;

            string paperName = btnDeletePaperSize.Tag.ToString();
            
            DialogResult result = MessageBox.Show(
                $"Apakah Anda yakin ingin menghapus ukuran kertas '{paperName}' secara PERMANEN?",
                "Konfirmasi Hapus",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Warning);

            if (result == DialogResult.Yes)
            {
                try
                {
                    bool success = PaperSizeManager.DeletePermanentPaperSize(paperName);
                    
                    if (success)
                    {
                        MessageBox.Show($"Ukuran kertas '{paperName}' berhasil dihapus!", "Sukses", 
                            MessageBoxButtons.OK, MessageBoxIcon.Information);
                        
                        // Refresh list
                        LoadPaperSizes();
                        
                        // Reset UI
                        groupBoxEditPaperSize.Visible = false;
                        groupBoxAddPaperSize.Visible = true;
                        btnDeletePaperSize.Visible = false;
                    }
                    else
                    {
                        MessageBox.Show("Gagal menghapus ukuran kertas. Pastikan running as Administrator.", "Gagal", 
                            MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error: {ex.Message}", "Error", 
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void btnAbout_Click(object sender, EventArgs e)
        {
            string version = System.Reflection.Assembly.GetExecutingAssembly().GetName().Version.ToString();
            string changelog = 
                $"Printer Management Tool v{version}\n\n" +
                "Changelog:\n" +
                "- v1.1.0: Added 'Delete Paper Size' feature\n" +
                "- v1.1.0: Fixed registry binary structure for 4BARCODE printer\n" +
                "- v1.1.0: Added permanent registry storage support\n" +
                "- v1.0.0: Initial release";

            MessageBox.Show(changelog, "About", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void btnCancelEdit_Click(object sender, EventArgs e)
        {
            // Hide edit panel, show add panel
            groupBoxEditPaperSize.Visible = false;
            groupBoxAddPaperSize.Visible = true;
        }

    }
}
