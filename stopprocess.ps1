

$adminRole=[System.Security.Principal.WindowsBuiltInRole]::Administrator
 
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
}