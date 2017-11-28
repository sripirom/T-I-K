FROM microsoft/aspnetcore:2.0
ARG source
WORKDIR /app
EXPOSE 80/tcp
COPY ${source:-obj/Docker/publish} .
ENTRYPOINT ["dotnet", "TIK.ProcessService.Online.dll"]
