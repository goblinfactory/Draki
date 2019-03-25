# Knowledge base

##  process cannot access the file

> Message: System.IO.IOException : The process cannot access the file 'C:\Users\Alan\AppData\Local\Temp\chromedriver.exe' because it is being used by another process.

### fix

You probably have stopped a test during debugging. Or run a .NET Core test that did not shut down the chrome driver. Run the following powershell command to stop the chromedriver then re-run your test.

> get-process | where { $_.name -eq 'chromedriver' } | stop-process


