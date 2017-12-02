FROM microsoft/aspnetcore:2.0
ARG source
WORKDIR /app
EXPOSE 80 5301/tcp
COPY ${source:-obj/Docker/publish} .
ENTRYPOINT ["dotnet", "TIK.Computation.AkkaSeed.dll"]
