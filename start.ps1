# Kredi Hesaplama Uygulaması Başlatma Scripti

$currentDir = Get-Location

# Eski surecleri temizle (varsa)
Stop-Process -Name "KrediHesaplama.Api" -ErrorAction SilentlyContinue

# API'yi dotnet watch ile başlat (Yeni bir pencerede)
Write-Host "API baslatiliyor (dotnet watch)..." -ForegroundColor Green
Start-Process powershell -ArgumentList "-NoExit", "-Command", "cd '$currentDir\KrediHesaplama.Api'; dotnet watch run"

# UI'yi npm start ile başlat (Yeni bir pencerede)
Write-Host "UI baslatiliyor (npm start)..." -ForegroundColor Cyan
Start-Process powershell -ArgumentList "-NoExit", "-Command", "cd '$currentDir\kredi-hesaplama-ui'; npm start"

Write-Host "Her iki uygulama da ayri pencerelerde baslatildi." -ForegroundColor Yellow
Write-Host "API: https://localhost:7177"
Write-Host "UI: http://localhost:5200"
