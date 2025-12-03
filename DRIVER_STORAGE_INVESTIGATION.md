# Investigation: Driver-Specific Paper Size Storage

## Problem Report
Registry berhasil diubah (Forms global), tapi paper size tidak muncul di printer properties.

## Why This Happens

Banyak printer drivers **TIDAK MEMBACA** dari Forms global registry. Mereka punya storage sendiri:

### Common Storage Locations

1. **Global Forms** (yang kita sudah ubah):
   ```
   HKLM\SYSTEM\CurrentControlSet\Control\Print\Forms
   ```
   ❌ 4BARCODE driver mungkin TIDAK baca dari sini

2. **Printer-Specific Registry**:
   ```
   HKLM\SYSTEM\CurrentControlSet\Control\Print\Printers\[PrinterName]\PrinterDriverData
   ```
   ✅ Driver-specific settings, MIGHT store paper sizes here

3. **Driver Version-3 Location**:
   ```
   HKLM\SYSTEM\CurrentControlSet\Control\Print\Environments\Windows x64\Drivers\Version-3\[DriverName]
   ```
   ✅ Driver configuration, possible paper size storage

4. **User-Specific DevMode**:
   ```
   HKCU\Software\Microsoft\Windows NT\CurrentVersion\Devices
   HKCU\Printers\DevModePerUser\[PrinterName]
   ```
   ⚠️ User preferences, not global

5. **INF/GPD Files** (Driver Files):
   ```
   C:\Windows\System32\DriverStore\FileRepository\
   ```
   ✅ Some drivers read paper sizes from GPD/PPD files

## Investigation Steps

### Step 1: Use Registry Scanner Tool

Saya sudah tambahkan tool untuk scan registry. Cara pakai:

1. **Rebuild aplikasi** (sudah running)
2. **Run app**, pilih printer
3. **Klik button "Scan Registry"** (perlu ditambahkan di UI)
4. **Lihat hasil scan** - cari dimana driver menyimpan data

### Step 2: Manual Registry Investigation

```powershell
# Cari printer name
reg query "HKLM\SYSTEM\CurrentControlSet\Control\Print\Printers"

# Cari driver name
reg query "HKLM\SYSTEM\CurrentControlSet\Control\Print\Printers\4BARCODE 4B-2082A" /v "Printer Driver"

# Scan driver location
reg query "HKLM\SYSTEM\CurrentControlSet\Control\Print\Environments\Windows x64\Drivers\Version-3\[DriverName]" /s
```

### Step 3: Check Driver Files

```powershell
# Find driver INF/GPD files
Get-ChildItem "C:\Windows\System32\DriverStore\FileRepository\" -Recurse -Filter "*4barcode*.gpd"
Get-ChildItem "C:\Windows\System32\DriverStore\FileRepository\" -Recurse -Filter "*4barcode*.inf"
```

## Possible Solutions

### Solution 1: Restart Print Spooler

Driver mungkin perlu reload configuration:

```powershell
Restart-Service -Name Spooler -Force
```

### Solution 2: AddForm Windows API

Instead of registry, use Windows API `AddForm()`:

```csharp
[DllImport("winspool.drv")]
static extern bool AddForm(IntPtr hPrinter, int Level, ref FORM_INFO_1 pForm);

struct FORM_INFO_1 {
    uint Flags;
    string pName;
    SIZEL Size;
    RECTL ImageableArea;
}
```

This directly adds to Print Spooler's form database.

### Solution 3: Modify Driver INF/GPD

Some drivers read paper sizes from GPD files:

```
*Feature: PaperSize
{
    *Option: CUSTOMSIZE_100x150
    {
        *Name: "Custom 100x150mm"
        *PageDimensions: PAIR(100000, 150000)
    }
}
```

But this requires driver reinstall.

### Solution 4: Use Printer Properties API

```csharp
[DllImport("winspool.drv")]
static extern bool SetPrinter(IntPtr hPrinter, int Level, IntPtr pPrinter, int Command);
```

Directly modify printer configuration via API.

## Next Steps

1. ✅ **Run Registry Scanner** tool yang baru dibuat
2. **PASTE HASIL SCAN** ke chat untuk analisis
3. Berdasarkan hasil scan, saya akan:
   - Identifikasi exact location driver menyimpan paper sizes
   - Implement solution untuk write ke location tersebut
   - Test dan verify

## What to Send Me

Setelah running registry scanner, kirim hasil yang menunjukkan:

1. **Driver name** yang digunakan printer
2. **Registry keys** yang exist untuk printer ini
3. **Any paper size data** yang ditemukan di registry
4. **Error messages** jika ada

Dengan info ini, saya bisa pinpoint exact storage location dan implement fix yang tepat.

## Common Driver Behaviors

### Generic/Microsoft Drivers
- ✅ Read from Forms global
- ✅ Respect AddForm() API
- Easy to customize

### Vendor-Specific Drivers (like 4BARCODE)
- ❌ Often ignore Forms global
- ❌ Have proprietary storage
- ✅ Use GPD/INF files
- ⚠️ May require vendor tools

## Temporary Workaround

While investigating, you can:

1. **Open Printer Properties manually**
2. **Go to Paper Size settings**
3. **Add custom size through UI**
4. **Note where Windows stores it**
5. **Inspect registry after adding**
6. **Compare before/after registry state**

This manual test will reveal exact storage location!
