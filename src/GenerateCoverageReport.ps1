$loc = Get-Location

dotnet test /p:CollectCoverage=true /p:CoverletOutputFormat=cobertura /p:CoverletOutput=TestResults/Coverage/

dotnet reportgenerator -reports:$loc/**/coverage.cobertura.xml -targetdir:../TestResults/Report -reporttypes:"HtmlInline_AzurePipelines;Cobertura" -assemblyfilters:"-*.Extensions;" -classfilters:"-*.Program;-*.Startup"