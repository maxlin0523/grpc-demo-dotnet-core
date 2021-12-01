### Build Stage
FROM mcr.microsoft.com/dotnet/core/sdk:3.1 AS dotnet-build-env
COPY ./src /src
WORKDIR /src
RUN dotnet publish GrpcDemo.WebApplication -o /publish -c Release

### Publish Stage
FROM mcr.microsoft.com/dotnet/core/aspnet:3.1
WORKDIR /app
COPY --from=dotnet-build-env /publish .
ENTRYPOINT dotnet GrpcDemo.WebApplication.dll