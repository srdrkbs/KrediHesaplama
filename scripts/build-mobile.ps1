# Kredi Hesaplama - Mobil Build ve Sync Scripti

Write-Host "--- 1. Angular Projesi Build Ediliyor (Production) ---" -ForegroundColor Cyan
Set-Location kredi-hesaplama-ui
npm run build

if ($LASTEXITCODE -ne 0) {
    Write-Host "!!! Angular build hatası oluştu. İşlem durduruldu. !!!" -ForegroundColor Red
    exit $LASTEXITCODE
}

Write-Host "`n--- 2. Capacitor ile Android Senkronizasyonu Yapılıyor ---" -ForegroundColor Cyan
npx cap sync android

if ($LASTEXITCODE -ne 0) {
    Write-Host "!!! Capacitor sync hatası oluştu. !!!" -ForegroundColor Red
    exit $LASTEXITCODE
}

Write-Host "`n=== İŞLEM BAŞARIYLA TAMAMLANDI ===" -ForegroundColor Green
Write-Host "Şimdi 'npm run mobile:open' komutu ile Android Studio'yu açıp APK alabilirsiniz." -ForegroundColor Yellow
Set-Location ..
