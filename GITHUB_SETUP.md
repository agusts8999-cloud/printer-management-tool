# Push Project ke GitHub - Panduan Lengkap

## Problem
Git tidak terinstall di sistem Anda.

## Solusi: Install Git dan Push ke GitHub

### Step 1: Install Git

**Download Git:**
1. Buka: https://git-scm.com/download/win
2. Download "64-bit Git for Windows Setup"
3. Run installer
4. **PENTING**: Pada "Adjusting your PATH environment":
   - Pilih **"Git from the command line and also from 3rd-party software"**
5. Install dengan default settings lainnya
6. Restart PowerShell/Command Prompt

**Verify Installation:**
```powershell
git --version
# Output: git version 2.x.x
```

### Step 2: Configure Git (First Time)

```powershell
# Set your name and email
git config --global user.name "Your Name"
git config --global user.email "your.email@example.com"

# Verify
git config --list
```

### Step 3: Initialize Repository

```powershell
cd C:\project\IM-583X

# Initialize git repo
git init

# Add all files
git add .

# Commit
git commit -m "Initial commit: 4BARCODE 4B-2082A Printer Management Tool"
```

### Step 4: Create GitHub Repository

**Option A: Via GitHub Website**

1. Login ke https://github.com
2. Click **"+"** → **"New repository"**
3. Repository name: `printer-management-tool` (atau nama lain)
4. Description: "Paper size management tool for 4BARCODE 4B-2082A printer"
5. **JANGAN** check "Initialize with README" (kita sudah punya files)
6. Click **"Create repository"**

**Copy repository URL** yang muncul (e.g., `https://github.com/username/printer-management-tool.git`)

**Option B: Via GitHub CLI (jika sudah install)**

```powershell
gh repo create printer-management-tool --public --source=. --remote=origin --push
```

### Step 5: Connect Local to GitHub

```powershell
# Add remote
git remote add origin https://github.com/YOUR_USERNAME/REPO_NAME.git

# Verify
git remote -v
```

### Step 6: Push to GitHub

```powershell
# Push to main branch
git branch -M main
git push -u origin main
```

**If prompted for credentials:**
- Username: your GitHub username
- Password: use **Personal Access Token** (NOT your password)

### Step 7: Create Personal Access Token (if needed)

1. GitHub → Settings → Developer settings
2. Personal access tokens → Tokens (classic)
3. Generate new token (classic)
4. Select scopes: **repo** (all)
5. Generate token
6. **COPY TOKEN** (won't show again!)
7. Use this token as password when pushing

---

## Alternative: Use GitHub Desktop

**Easier for beginners:**

1. Download: https://desktop.github.com/
2. Install and login
3. File → Add Local Repository → Select `C:\project\IM-583X`
4. Publish repository
5. Done!

---

## Quick Commands Reference

```powershell
# Check status
git status

# Add new files
git add .

# Commit changes
git commit -m "Description of changes"

# Push to GitHub
git push

# Pull latest changes
git pull

# View commit history
git log --oneline

# Create new branch
git checkout -b feature-name

# Switch branch
git checkout main
```

---

## What Files Will Be Published

Based on `.gitignore`, these will be pushed:

✅ **Source Code:**
- `*.cs` files
- `*.csproj` files
- `*.sln` files

✅ **Documentation:**
- `README.md`
- `TROUBLESHOOTING.md`
- `MANUAL_INVESTIGATION.md`
- `REG_FILE_METHOD.md`
- etc.

❌ **NOT Pushed (ignored):**
- `bin/` and `obj/` folders
- `.exe` and `.dll` files
- `.vs/` folder
- User-specific files

---

## After First Push

### Clone di komputer lain:
```powershell
git clone https://github.com/YOUR_USERNAME/REPO_NAME.git
cd REPO_NAME
```

### Update after changes:
```powershell
git add .
git commit -m "Updated paper size algorithm"
git push
```

### Collaborate:
1. Fork repository
2. Create feature branch
3. Make changes
4. Create Pull Request

---

## Common Issues

### Issue 1: Git not found

**Solution**: Install Git for Windows, restart terminal

### Issue 2: Permission denied (publickey)

**Solution**: Use HTTPS instead of SSH, or setup SSH keys

### Issue 3: Push rejected

**Solution**: 
```powershell
git pull --rebase
git push
```

### Issue 4: Merge conflicts

**Solution**: 
```powershell
git status  # See conflicted files
# Edit files manually
git add <resolved-file>
git commit
```

---

## Project Structure on GitHub

```
printer-management-tool/
├── README.md
├── PrinterToolApp/
│   ├── Form1.cs
│   ├── PaperSizeManager.cs
│   ├── PrinterManager.cs
│   └── ...
├── TROUBLESHOOTING.md
├── MANUAL_INVESTIGATION.md
├── REG_FILE_METHOD.md
└── .gitignore
```

---

## Next Steps

1. ✅ Install Git
2. ✅ Configure user name/email
3. ✅ Initialize repo
4. ✅ Create GitHub repo
5. ✅ Push code
6. 🎉 Share link!

---

## GitHub Repository Recommendations

### README.md Enhancement

Add badges:
```markdown
# 4BARCODE 4B-2082A Printer Management Tool

![.NET Framework](https://img.shields.io/badge/.NET%20Framework-4.7.2-blue)
![Windows](https://img.shields.io/badge/OS-Windows%2010-blue)
![License](https://img.shields.io/badge/license-MIT-green)
```

### Add License

Create `LICENSE` file:
- MIT License (permissive)
- Apache 2.0 (patent protection)
- GPL (copyleft)

### Add Contributing Guide

Create `CONTRIBUTING.md`:
- How to report bugs
- How to request features
- Code style guidelines

---

## Need Help?

Jika ada error atau pertanyaan, paste error message ke chat dan saya bantu troubleshoot!
