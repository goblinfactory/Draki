function MAKE_NUGET($projectPath, $dest) {  

	# -------------------------------------------------------------------
	function Split([string] $src,[string] $seperator) {
		$ret = $src.Split(@($seperator), [System.StringSplitOptions]::RemoveEmptyEntries)
		(,$ret) # force result to be an array even if there was only 1 string
	}
	# -------------------------------------------------------------------
	function checkBuild() {
		$file = get-item $dir\bin\Release\$name.dll
		Write-Host "Checking age of $file"
		CheckAge $file $minutes "Please make a recent RELEASE build before running this script. It's quite possible the last build you made was DEBUG."
		exit 1
	}
	# -------------------------------------------------------------------
	function CheckAge($file, $minutes, $message) {
		$age = (get-date).Subtract($file.LastAccessTime).TotalMinutes
		$name = $file.Name
		if ($age -gt $minutes) { 
			write-Host ("$name is {0:0} minutes old. exiting script" -f $age)
			write-Host $message
			exit 1
		}	
	}
	# -------------------------------------------------------------------
	function MostRecent($wildcard) {
		$latest = gci $dir\$wildcar | sort LastWriteTime | select -first 1
		CheckAge $latest 
	}

	# ===================================================================
	#					          make-nuget.ps1
	# ===================================================================
	
	# AAARGH need simpler names for all this shit!!!
	# projectFilePath, projectFileObject, projectFileNameWithExt, projectFileNameOnly

	$projectFile = Get-Item $projectPath
	$projectName = $projectFile.Name
	$name = (Split $projectName ".csproj")
	$dir = $projectFile.Directory.FullName   
	
	# number of minutes old (age) that I allow the release build to be, before the script will tell me I have forgotten to make a release build, because yknow i forget that me and visualstudio live perpetually in DEBUG mode!
	$minutes = 2										

	Write-Host "Building nuget package : projectPath:$projectPath, projectName:$projectName, name:$name, $dir:dir"
	checkBuild
	nuget pack $projectPath -Prop Configuration=Release -IncludeReferencedProjects -OutputDirectory $dest


	# copy-items MostRecent "*.nupkg" "c:\"

	# see https://stackoverflow.com/questions/26111710/nuget-dependencies-not-getting-installed?utm_medium=organic&utm_source=google_rich_qa&utm_campaign=google_rich_qa
	# extra notes on getting the nuget package to automatically include the correct related dependency packages
	

}

MAKE_NUGET .\Draki.Core\Draki.Core.csproj "C:\src\.nuget\test-packages"