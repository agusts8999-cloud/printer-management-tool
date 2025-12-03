# Manual Investigation Guide - Driver Paper Size Storage

## Problem
✅ Registry Forms successfully updated  
❌ Paper size NOT appearing in printer properties

## Root Cause
Driver **4BARCODE 4B-2082A** likely has proprietary storage and does NOT read from global Forms registry.

---

## INVESTIGATION STEPS

### Step 1: Find Printer Registry Location

```powershell
# Run this in PowerShell as Administrator:
reg query "HKLM\SYSTEM\CurrentControlSet\Control\Print\Printers" | findstr "4BARCODE"
```

**Expected output**: Full printer key path

### Step 2: Get Driver Name

```powershell
# Replace [PrinterName] with exact name from Step 1:
reg query "HKLM\SYSTEM\CurrentControlSet\Control\Print\Printers\[PrinterName]" /v "Printer Driver"
```

**Expected output**: Driver name (e.g., "4BARCODE 4B-2082A" or similar)

### Step 3: Check PrinterDriverData

```powershell
# Check if driver stores data here:
reg query "HKLM\SYSTEM\CurrentControlSet\Control\Print\Printers\[PrinterName]\PrinterDriverData" /s
```

**Look for**: Any values related to paper sizes, forms, or media

### Step 4: Check Driver-Specific Registry

```powershell
# Check driver Version-3 location:
reg query "HKLM\SYSTEM\CurrentControlSet\Control\Print\Environments\Windows x64\Drivers\Version-3" /s /f "4BARCODE"
```

**Look for**: Driver configuration keys

### Step 5: Compare Before/After Manual Add

**Test procedure**:
1. Open **Devices and Printers**
2. Right-click printer → **Printing Preferences**
3. Find **Paper Size** settings
4. **Manually add** a custom size (e.g., "TestSize 50x80mm")
5. Click **OK** to save

**Then immediately run**:
```powershell
# Export current state:
reg export "HKLM\SYSTEM\CurrentControlSet\Control\Print" C:\before.reg /y

# After adding manually, export again:
reg export "HKLM\SYSTEM\CurrentControlSet\Control\Print" C:\after.reg /y

# Compare:
fc C:\before.reg C:\after.reg > C:\differences.txt
notepad C:\differences.txt
```

**This will show EXACTLY where driver stores paper sizes!**

---

## ALTERNATIVE: Use Windows API AddForm

Instead of registry approach, use Windows Spooler API.

### Create PowerShell Script

Save as `AddPaperSize.ps1`:

```powershell
Add-Type @"
using System;
using System.Runtime.InteropServices;

public class PrinterAPI {
    [DllImport("winspool.drv", CharSet = CharSet.Auto, SetLastError = true)]
    public static extern bool OpenPrinter(string pPrinterName, out IntPtr phPrinter, IntPtr pDefault);
    
    [DllImport("winspool.drv", CharSet = CharSet.Auto, SetLastError = true)]
    public static extern bool ClosePrinter(IntPtr hPrinter);
    
    [DllImport("winspool.drv", CharSet = CharSet.Auto, SetLastError = true)]
    public static extern bool AddForm(IntPtr hPrinter, int Level, ref FORM_INFO_1 pForm);
    
    [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Auto)]
    public struct FORM_INFO_1 {
        public uint Flags;
        [MarshalAs(UnmanagedType.LPTStr)]
        public string pName;
        public SIZEL Size;
        public RECTL ImageableArea;
    }
    
    [StructLayout(LayoutKind.Sequential)]
    public struct SIZEL {
        public int cx;
        public int cy;
    }
    
    [StructLayout(LayoutKind.Sequential)]
    public struct RECTL {
        public int left;
        public int top;
        public int right;
        public int bottom;
    }
}
"@

# Usage:
$printerName = "4BARCODE 4B-2082A"
$hPrinter = [IntPtr]::Zero

if ([PrinterAPI]::OpenPrinter($printerName, [ref]$hPrinter, [IntPtr]::Zero)) {
    
    $form = New-Object PrinterAPI+FORM_INFO_1
    $form.Flags = 1  # FORM_USER
    $form.pName = "Custom 100x150"
    $form.Size.cx = 100000  # 100mm in micrometers
    $form.Size.cy = 150000  # 150mm in micrometers
    $form.ImageableArea.left = 0
    $form.ImageableArea.top = 0
    $form.ImageableArea.right = 100000
    $form.ImageableArea.bottom = 150000
    
    if ([PrinterAPI]::AddForm($hPrinter, 1, [ref]$form)) {
        Write-Host "SUCCESS: Paper size added!" -ForegroundColor Green
    } else {
        Write-Host "FAILED: $([System.Runtime.InteropServices.Marshal]::GetLastWin32Error())" -ForegroundColor Red
    }
    
    [PrinterAPI]::ClosePrinter($hPrinter)
} else {
    Write-Host "FAILED to open printer" -ForegroundColor Red
}
```

Run as Administrator:
```powershell
PowerShell -ExecutionPolicy Bypass -File AddPaperSize.ps1
```

---

## WHAT TO SEND ME

After running investigation steps, please provide:

1. **Output from Step 2** (driver name)
2. **Output from Step 3** (PrinterDriverData contents)
3. **Output from Step 5** (differences.txt file)

With this information, I can:
- Identify exact storage location
- Implement proper write method
- Update application to use correct API

---

## Quick Test

Meanwhile, test if Print Spooler restart helps:

```powershell
Restart-Service -Name Spooler -Force
```

Then check printer properties - sometimes driver caches forms list.

---

## Expected Results

Based on investigation, we'll likely find one of:

1. **PrinterDriverData** - Driver stores settings here
2. **Custom INI file** - Some drivers use config files
3. **GPD/PPD file** - Driver definition file
4. **AddForm API required** - Driver needs API call, not registry

I'll implement the correct solution once we know the storage location!
