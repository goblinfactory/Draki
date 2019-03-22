# requires that the folder already exists.
$dir = "C:\src\nuget\test-packages\Draki"
Copy-Item "Draki.Nuget\bin\Debug\*.nupkg" -Destination $dir
Copy-Item "Draki.Core\bin\Debug\*.nupkg" -Destination $dir
Copy-Item "Draki.SeleniumWebDriver\bin\Debug\*.nupkg" -Destination $dir