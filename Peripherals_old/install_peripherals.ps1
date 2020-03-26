

if (!([Security.Principal.WindowsPrincipal][Security.Principal.WindowsIdentity]::GetCurrent()).IsInRole([Security.Principal.WindowsBuiltInRole] "Administrator")) { Start-Process powershell.exe "-NoProfile -ExecutionPolicy Bypass -File `"$PSCommandPath`"" -Verb RunAs; exit }

$path = (Get-Item "Env:ProgramFiles").Value + "\infonet-pos"
$stubDllPath = $path + "\PeripheralsProxyStub.dll"


# this script needs to be run by the x86 version of powershell
# if it's run by the x64 version, relaunch as x86
if ($env:Processor_Architecture -ne "x86") { 
  write-warning 'Launching x86 PowerShell'
  &"$env:windir\syswow64\windowspowershell\v1.0\powershell.exe" -noninteractive -noprofile -file $myinvocation.Mycommand.path -executionpolicy bypass -verb runas
  exit
}




# stop the dllhost process if it's running in order to unlock the DLLs
$proxyStubModule = "PeripheralsProxyStub.dll"

Write-Output "Stopping DLLHost processes with $testModule loaded"
function check-process ([System.Diagnostics.Process]$process, [string]$moduleName) {
  ($process.Modules | ?{$_.ModuleName -eq $moduleName}).count -gt 0
}
get-process | ? { $_.ProcessName -eq "dllhost"} | ?{ check-process $_ $proxyStubModule} | %{ stop-process $_ }


# copy Brokered WinRT component files to install path
Write-Output "copying brokered component files to $path"


if (!(test-path $path)) { 
  New-Item -ItemType Directory -Force -Path "$path"
}

#save the last write date of the proxy/stub module so we can later see if it changed
$psPath = join-path $path $proxyStubModule
if (test-path $psPath) {
  $psOldLastWrite = (get-childitem $psPath).LastWriteTime
}

#copy relevant files 
copy-item "$PSScriptRoot\PeripheralsComponent.winmd" $path 
copy-item "$PSScriptRoot\PeripheralsProxyStub.dll" $path 
copy-item "$PSScriptRoot\copyright.bmp" $path 


#setup ACLs correctly for the brokered WinRT component install folder 
$ignore = icacls $path /T  /grant "ALL APPLICATION PACKAGES:RX"

#save the updated last write date of the proxy/stub module 
$psNewLastWrite = (get-childitem $psPath).LastWriteTime

#if the proxy/stub module has been updated, reregister it via regsvr32
#if (true or $psNewLastWrite -gt $psOldLastWrite) {
  Write-warning "Proxy updated. Launching elevated command shell to re-register proxy"
  
  Start-Process powershell.exe -argumentlist "-command regsvr32 '$stubDllPath'" -verb runas
#}

Write-Host "Press any key to continue ..."

$x = $host.UI.RawUI.ReadKey("NoEcho,IncludeKeyDown")
