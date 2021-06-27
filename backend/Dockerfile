FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
WORKDIR /app

COPY src/ .
RUN dotnet publish -c Release -o out ./LetsCode.Kanban.WebApi

FROM mcr.microsoft.com/dotnet/aspnet:5.0
WORKDIR /app
COPY --from=build /app/out .
ENTRYPOINT ["dotnet", "LetsCode.Kanban.WebApi.dll"]