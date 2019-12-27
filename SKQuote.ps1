# Get the ID and security principal of the current user account
$myWindowsID=[System.Security.Principal.WindowsIdentity]::GetCurrent()
$myWindowsPrincipal=new-object System.Security.Principal.WindowsPrincipal($myWindowsID)
# Get the security principal for the Administrator role
Write-Host $myWindowsID
try
{
    Write-Host "Stopping process"
    Get-Process -Name "SKQuote"
    Get-Process -Name "SKQuote" | stop-process -Force
    
}
catch [System.Exception]
{
    Write-Host "Process not found"
    "Error: $Error" >> errorlog.txt
}
finally
{
    Write-Host "cleaning up ..."
    Start-Process -FilePath "C:\TradeSoft\SKQuote\SKQuote.exe"
}