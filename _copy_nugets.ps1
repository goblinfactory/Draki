# requires that the folder already exists.
$dir = "C:\src\nuget\test-packages\Draki"
Copy-Item "Draki.Core\bin\Release\*.nupkg" -Destination $dir