& dotnet publish ./TIK/Computation/AkkaSeed/TIK.Computation.AkkaSeed.csproj -c Release -o ./obj/Docker/publish

& dotnet publish ./TIK/ProcessService/Online/TIK.ProcessService.Online.csproj -c Release -o ./obj/Docker/publish

& dotnet publish ./TIK/ProcessService/Identity/TIK.ProcessService.Identity.csproj -c Release -o ./obj/Docker/publish

& dotnet publish ./TIK/TIK.WebPortal/TIK.WebPortal.csproj -c Release -o ./obj/Docker/publish
