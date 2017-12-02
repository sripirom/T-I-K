& dotnet publish ./Computation/AkkaSeed/TIK.Computation.AkkaSeed.csproj -c Release -o ./obj/Docker/publish

& dotnet publish ./ProcessService/Online/TIK.ProcessService.Online.csproj -c Release -o ./obj/Docker/publish

& dotnet publish ./ProcessService/Identity/TIK.ProcessService.Identity.csproj -c Release -o ./obj/Docker/publish

& dotnet publish ./TIK.WebPortal/TIK.WebPortal.csproj -c Release -o ./obj/Docker/publish
 
