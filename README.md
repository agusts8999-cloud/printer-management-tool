# 4BARCODE 4B-2082A - Paper Size Management Tool

Aplikasi khusus untuk mengelola ukuran kertas printer **4BARCODE 4B-2082A**.

## Fitur Utama

✅ **Deteksi Printer**: Otomatis detect printer 4BARCODE 4B-2082A
✅ **Driver Installation Guide**: Instruksi install jika printer tidak ditemukan
✅ **View Paper Sizes**: Tampilkan ukuran kertas yang tersedia
✅ **Add Custom Paper Size**: Tambahkan ukuran kertas custom
✅ **Edit Paper Size**: Ubah dimensi ukuran kertas existing
✅ **Permanent Storage**: Simpan ke Windows Registry (requires Admin)

## Persyaratan

- Windows 10 atau lebih tinggi
- .NET Framework 4.7.2+
- Printer **4BARCODE 4B-2082A** sudah terinstall
- Hak Administrator (untuk penyimpanan permanen)

## Quick Start

### 1. Install Printer Driver (Jika Belum)

Jika printer belum terinstall, aplikasi akan menampilkan instruksi:

**Manual Installation:**
1. Download driver 4BARCODE 4B-2082A dari vendor
2. Extract dan jalankan installer sebagai Administrator  
3. Restart aplikasi setelah install selesai

**Atau Add Printer Manual:**
- Control Panel → Devices and Printers
- Add a printer → Add manually
- Pilih port (USB/Network)
- Pilih driver: 4BARCODE 4B-2082A

### 2. Jalankan Aplikasi

**Normal Mode** (Session-based):
```
Double-click PrinterToolApp.exe
```

**Administrator Mode** (Permanent registry storage):
```
Right-click PrinterToolApp.exe → Run as administrator
```

## Penggunaan

### Add Custom Paper Size

1. Aplikasi akan otomatis detect printer 4BARCODE 4B-2082A
2. Isi form:
   - **Paper Name**: Nama ukuran kertas (contoh: "Label 100x50")
   - **Width**: Lebar dalam mm (10-1000)
   - **Height**: Tinggi dalam mm (10-1000)
3. **Optional**: Centang "Simpan permanen" (butuh run as admin)
4. Klik **Add Paper Size**

### Edit Existing Paper Size

1. Pilih paper size dari list
2. Klik **Edit Selected**
3. Ubah Width/Height sesuai kebutuhan
4. **Optional**: Centang "Simpan permanen"
5. Klik **Update Paper Size**

## Penyimpanan Permanen

### Registry Storage

Untuk menyimpan ukuran kertas **permanen** ke Windows Registry:

**Requirements:**
- ✅ Running as Administrator
- ✅ Centang checkbox "Simpan permanen ke registry"

**Lokasi Registry:**
```
HKEY_LOCAL_MACHINE\SYSTEM\CurrentControlSet\Control\Print\Forms\[PaperName]
```

**Benefits:**
- Tersedia untuk SEMUA printer
- Persisten (tidak hilang setelah restart)
- System-wide (semua user)

### Troubleshooting Registry Error

Jika muncul error **"tidak dapat membuka registry form"**:

**Solusi:**

1. **Pastikan Running as Administrator**
   - Tutup aplikasi
   - Klik kanan exe → Run as administrator
   - Verify title bar: "Running as Administrator ✓"

2. **Check UAC Settings**
   - Control Panel → User Accounts → UAC Settings
   - Jangan set ke level tertinggi
   - Restart aplikasi sebagai admin

3. **Verify Registry Permissions**
   - Buka `regedit` sebagai Administrator
   - Navigate: `HKLM\SYSTEM\CurrentControlSet\Control\Print\Forms`
   - Klik kanan → Permissions
   - Pastikan Administrators group = Full Control

4. **Restart Print Spooler**
   ```powershell
   Restart-Service -Name Spooler -Force
   ```

## Error Messages & Solutions

| Error | Cause | Solution |
|-------|-------|----------|
| "Printer tidak ditemukan" | Driver not installed | Install driver 4BARCODE 4B-2082A |
| "Akses registry ditolak" | Not running as admin | Run as administrator |
| "Tidak dapat membuka registry Forms" | UAC blocking / Permissions | Check UAC, registry permissions |
| Checkbox disabled (grayed) | Not admin mode | Run as administrator |

## Technical Details

### Supported Printer
- **Model**: 4BARCODE 4B-2082A only
- Other printers will not be shown

### Paper Size Limits
- **Minimum**: 10mm x 10mm
- **Maximum**: 1000mm x 1000mm
- **Unit**: Millimeters (mm)

### Registry Structure
```
HKLM\SYSTEM\CurrentControlSet\Control\Print\Forms\
  └── CustomPaperName\
      ├── Flags = 0x1 (DWORD, user-defined)
      ├── Size = width + height in 1/1000 mm (Binary, 8 bytes)
      └── ImageableArea = left,top,right,bottom (Binary, 16 bytes)
```

## Tips & Best Practices

> [!TIP]
> **Recommended Workflow**
> 1. Test dengan session-based mode terlebih dahulu
> 2. Jika sudah yakin, gunakan permanent mode
> 3. Restart Print Spooler after menambahkan paper size
> 4. Verify di Printer Properties

> [!IMPORTANT]
> **Before Running as Administrator**
> - Tutup semua instance aplikasi yang running
> - Pastikan file exe tidak corrupted
> - Backup registry jika akan banyak testing

> [!WARNING]
> **Registry Modifications**
> - Bersifat permanen dan system-wide
> - Affects all users on the system
> - Restart Print Spooler mungkin required
> - Test dengan non-critical paper sizes first

## File Locations

- **Executable**: `PrinterToolApp\bin\Debug\PrinterToolApp.exe`
- **Source Code**: `PrinterToolApp\*.cs`
- **Documentation**: `README.md`, `TROUBLESHOOTING.md`

## Support

Untuk troubleshooting lebih lanjut, lihat:
- [TROUBLESHOOTING.md](TROUBLESHOOTING.md) - Detailed debugging guide
- [walkthrough.md](C:\Users\agai\.gemini\antigravity\brain\aebb748c-eef6-46a7-b617-6f5e6663b2e3\walkthrough.md) - Technical walkthrough

## Version History

- **v1.3** - Customized for 4BARCODE 4B-2082A + Registry error improvements
- **v1.2** - Added registry-based permanent storage
- **v1.1** - Added edit paper size feature
- **v1.0** - Initial release with add/view features
