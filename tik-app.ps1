
 Param([string]$func)

 function build-dockers()
 {
    & dotnet publish ./TIK/Computation/AkkaSeed/TIK.Computation.AkkaSeed.csproj -c Release -o ./obj/Docker/publish
    
    & dotnet publish ./TIK/ProcessService/Online/TIK.ProcessService.Online.csproj -c Release -o ./obj/Docker/publish
    
    & dotnet publish ./TIK/ProcessService/Identity/TIK.ProcessService.Identity.csproj -c Release -o ./obj/Docker/publish
    
    & dotnet publish ./TIK/TIK.WebPortal/TIK.WebPortal.csproj -c Release -o ./obj/Docker/publish
    
    & dotnet publish ./TIK/TIK.WebSignalR/TIK.WebSignalR.csproj -c Release -o ./obj/Docker/publish
 }
function publish-dockers($outputversion)
{
    Write-Output "taging version: $outputversion" 

    & docker tag tik.computation.akkaseed sripirom/tik.computation.akkaseed:$outputversion
    
    & docker tag tik.processservice.online sripirom/tik.processservice.online:$outputversion
    
    & docker tag tik.processservice.identity sripirom/tik.processservice.identity:$outputversion
    
    & docker tag tik.webportal sripirom/tik.webportal:$outputversion
    
    & docker tag tik.websignalr sripirom/tik.websignalr:$outputversion

    & docker tag tik.elasticsearch sripirom/tik.elasticsearch:$outputversion
    
    Write-Output "pushing version: $outputversion" 

    $ "docker push sripirom/tik.computation.akkaseed:$outputversion"
    $ "docker push sripirom/tik.processservice.online:$outputversion"
    $ "docker push sripirom/tik.processservice.identity:$outputversion"
    $ "docker push sripirom/tik.webportal:$outputversion"
    $ "docker push sripirom/tik.websignalr:$outputversion"
    $ "docker push sripirom/tik.elasticsearch:$outputversion"
}

switch ($func) 
{ 
    "publish-dockers" {publish-dockers $args[0]} 
    "build-dockers" {build-dockers}

    default { Write-Output "function not found." }
}

