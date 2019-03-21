# find the latest chromedriver version by scraping the link on the chromedriver webpage
# get the latest chromedriver version number from the end of the url 
# e.g. 2.37 from -> https://chromedriver.storage.googleapis.com/index.html?path=2.37/
# ------------------------------------------------------------------------------------

$res = Invoke-WebRequest "https://sites.google.com/a/chromium.org/chromedriver/downloads" -UseBasicParsing
$url = ($res.Links | Where-Object -property href -match "^https://chromedriver.storage.googleapis.com/index.html\?path=" | select -property href)[0].href

$seperator = @("?path=")

# hard coding version for now because chrome is ver 73 and website says ver 74! mmm, that just happened.
# $ver = $url.Split($seperator, [System.StringSplitOptions]::RemoveEmptyEntries)[1]
$ver = "73.0.3683.68/"


$file = "https://chromedriver.storage.googleapis.com/" + $ver + "chromedriver_win32.zip"
write-host "downloading $file"

$dir = "Draki.SeleniumWebDriver\3rdPartyLib"
$zip = "$dir\chromedriver_win32.zip"
$zipFolder = $dir + "\chromedriver"
Invoke-WebRequest $file -OutFile $zip
Expand-Archive $zip -DestinationPath $zipFolder

Copy-Item  ".\$zipfolder\chromedriver.exe" "$dir\chromedriver.exe"
Remove-Item $zip 
remove-item $zipFolder -Recurse

write-host "done."
write-host "--------------------"
write-host "Remember to update your path to include the chromedriver location."
write-host "see https://sites.google.com/a/chromium.org/chromedriver/getting-started"

                  