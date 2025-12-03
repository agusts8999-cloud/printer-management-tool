# Troubleshooting Guide - Registry Update Failures

## Masalah yang Dilaporkan
Aplikasi sudah dijalankan sebagai administrator tapi masih gagal update ke registry.

## Penyebab Umum & Solusi

### 1. Verifikasi Status Administrator

**Cek:**
- Title bar aplikasi harus menampilkan: **"Running as Administrator ✓"**
- Jika tidak, aplikasi tidak dijalankan dengan hak administrator yang benar

**Solusi:**
```
1. Tutup aplikasi  
2. Klik kanan pada PrinterToolApp.exe
3. Pilih "Run as administrator"
4. Jika muncul UAC prompt, klik "Yes"
```

### 2. Checkbox Tidak Tercentang

**Cek:**
- Pastikan checkbox **"Simpan permanen ke registry (Admin required)"** tercentang
- Checkbox ini harus di-centang SEBELUM klik Add/Update

**Catatan:** Jika tidak running as admin, checkbox akan disabled (grayed out)

### 3. UAC (User Access Control) Settings

Windows UAC mungkin memblokir akses registry meskipun running as admin.

**Solusi:**
1. Buka **Control Panel** → **User Accounts**
2. Klik **Change User Account Control settings**
3. Pastikan tidak di level tertinggi ("Always notify")
4. Restart aplikasi sebagai administrator

### 4. Registry Permissions

Registry key mungkin memiliki permission khusus.

**Verifikasi Permission:**
1. Tekan `Win + R`, ketik `regedit`
2. Navigate ke: `HKEY_LOCAL_MACHINE\SYSTEM\CurrentControlSet\Control\Print\Forms`
3. Klik kanan → **Permissions**
4. Pastikan **Administrators** group memiliki **Full Control**

### 5. Print Spooler Service

Print Spooler service mungkin perlu di-restart.

**Restart Spooler:**
```powershell
# Buka PowerShell sebagai Administrator
Restart-Service -Name Spooler -Force
```

### 6. Error Message Spesifik

Jika muncul error message, catat detailnya:

**Error Message**| **Penyebab** | **Solusi**
---|---|---
"Akses ditolak" | Tidak running as admin | Run as administrator
"Tidak dapat membuka registry Forms" | Permission issue | Cek registry permissions
"UnauthorizedAccessException" | UAC blocking | Adjust UAC settings

### 7. Test dengan Paper Size Sederhana

Coba dengan data simple dulu untuk testing:

```
Paper Name: TestPaper
Width: 100 mm
Height: 150 mm
☑ Simpan permanen ke registry
```

Klik "Add Paper Size" dan lihat hasilnya.

### 8. Verifikasi di Registry Manual

Setelah klik Add/Update, cek manual di registry:

1. Buka **regedit** (as administrator)
2. Navigate: `HKEY_LOCAL_MACHINE\SYSTEM\CurrentControlSet\Control\Print\Forms`
3. Cari subkey dengan nama paper size yang ditambahkan
4. Jika ada, berarti berhasil disimpan

**Expected Values:**
- `Flags` (DWORD) = 1
- `Size` (Binary) = 8 bytes
- `ImageableArea` (Binary) = 16 bytes

### 9. Windows Version Compatibility

**Minimum Requirements:**
- Windows 10 atau lebih tinggi
- .NET Framework 4.7.2+

**Cek Windows Version:**
```
Win + R → winver
```

### 10. Rebuild Aplikasi

Jika ada update code terbaru:

```powershell
cd C:\project\IM-583X
& "C:\Program Files\Microsoft Visual Studio\2022\Enterprise\MSBuild\Current\Bin\MSBuild.exe" PrinterToolApp.sln /t:Rebuild
```

## Debugging Steps

Jika masih gagal, lakukan steps ini secara berurutan:

1. ✅ **Close semua instance aplikasi**
2. ✅ **Right-click exe → Run as administrator**
3. ✅ **Verify title bar shows "Running as Administrator ✓"**
4. ✅ **Verify checkbox enabled (not grayed out)**
5. ✅ **Centang checkbox** "Simpan permanen ke registry"
6. ✅ **Fill form** dengan data valid (10-1000 mm)
7. ✅ **Click Add/Update**
8. ✅ **Watch for success/error message**
9. ✅ **Check registry** di regedit
10. ✅ **Restart Print Spooler** jika paper size tidak muncul

## Informasi untuk Developer

### Code Changes
File yang telah diperbaiki:
- `Form1.cs` - Removed duplicate success messages
- Error handling sudah ditingkatkan

### Registry Path
```
HKEY_LOCAL_MACHINE\SYSTEM\CurrentControlSet\Control\Print\Forms\[PaperName]
```

### Required Admin Rights
Modifying `HKLM\SYSTEM` requires elevated privileges. Check:
```csharp
PaperSizeManager.IsRunningAsAdministrator()
```

## Jika Masalah Persists

Silakan berikan informasi berikut:
1. **Exact error message** yang muncul
2. **Screenshot** title bar aplikasi
3. **Status checkbox** (enabled/disabled, checked/unchecked)
4. **Windows version** (winver output)
5. **UAC settings level**
6. **Registry permissions** untuk Forms key

Dengan informasi ini, saya bisa memberikan solusi yang lebih spesifik.
