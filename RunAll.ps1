Write-Host "Challenger Api"
cd Challenger.Api
$location = Get-Location 
Write-Host $location
Start-Process -FilePath "cmd" -ArgumentList "/c dotnet run"

Write-Host ""
Write-Host "Challenger Web"
cd ..\Challenger.Web
$location = Get-Location 
Write-Host $location
Start-Process -FilePath "cmd" -ArgumentList "/c ng serve"

#Read-Host -Prompt '.'