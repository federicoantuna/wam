# Generic
$SolutionFileName = "WAM.sln"
$CoverageReportDir = ".coverage-report"
$HtmlReportDir = "html"
$HtmlReportIndexFileName = "index.html"
$CoverageXMLFileName = "coverage.cobertura.xml"
$ReportGenerator_ReportsSeparator = ";"
$TestResultsDir = "TestResults"
$TestsDir = "tests"

# Coverlet and Report Generator
$CoverletOutput = ".\TestResults\"
$CollectCoverage = "true"
$CoverletOutputFormat = "cobertura"
$ReportTypes = "HTML;"

# Arrange
$SolutionDir = (Get-Item $PSScriptRoot).Parent.FullName
$Solution = [IO.Path]::Combine($SolutionDir, $SolutionFileName)
$ReportHtmlFile = [IO.Path]::Combine($SolutionDir, $CoverageReportDir, $HtmlReportDir, $HtmlReportIndexFileName)
$TargetDir = [IO.Path]::Combine($CoverageReportDir, $HtmlReportDir)

# Build and Test
dotnet.exe build $Solution
dotnet.exe test $Solution /p:CollectCoverage=$CollectCoverage /p:CoverletOutputFormat=$CoverletOutputFormat /p:CoverletOutput=$CoverletOutput

# Arrange
$TestResultsDirs = Join-Path $SolutionDir -ChildPath $TestsDir\*\$TestResultsDir\*
$CoverageXMLFiles = (Split-Path $TestResultsDirs -Resolve | ForEach-Object -Process {[IO.Path]::Combine($_, $CoverageXMLFileName)}) -join $ReportGenerator_ReportsSeparator

# Generate Report
reportgenerator.exe "-reports:$CoverageXMLFiles" "-targetdir:$TargetDir" -reporttypes:$ReportTypes

# Open Report
Invoke-Item $ReportHtmlFile