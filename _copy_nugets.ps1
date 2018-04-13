$dir = "C:\src\.nuget\test-packages"
Copy-Item "Draki.Core\bin\Release\Draki.Core*.nupkg" -Destination $dir
Copy-Item "Draki.SeleniumWebDriver\bin\Release\Draki.SeleniumWebDriver*.nupkg" -Destination $dir