# TODO update to open silently when running on Appveyor.

function Start-Site($port){
	$path = (Resolve-path ./FluentAutomation.testApplication)
	Write-host "Starting IIS on port: $port"
	$params = "/port:$port /path:$path"
	$iis = '"c:\Program Files (x86)\IIS Express\iisexpress.exe"'
	$command = "$iis $params"
	cmd /c start cmd /k "$command"
}

Start-Site 38043
