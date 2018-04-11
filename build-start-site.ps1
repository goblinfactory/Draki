start "" http://localhost:52734
push cd
try
{
	Write-Host cd
	exit 1
	
	cd "C:\Program Files (x86)\IIS Express"
	iisexpress /config:"c:\tfs\git\MyApp\.vs\config\applicationhost.config" /siteid:2
}
finally
{
	pop cd
}
