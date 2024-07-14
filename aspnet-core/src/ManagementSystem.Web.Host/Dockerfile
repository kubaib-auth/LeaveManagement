FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build

WORKDIR /src
COPY ["src/ManagementSystem.Web.Host/ManagementSystem.Web.Host.csproj", "src/ManagementSystem.Web.Host/"]
COPY ["src/ManagementSystem.Web.Core/ManagementSystem.Web.Core.csproj", "src/ManagementSystem.Web.Core/"]
COPY ["src/ManagementSystem.Application/ManagementSystem.Application.csproj", "src/ManagementSystem.Application/"]
COPY ["src/ManagementSystem.Core/ManagementSystem.Core.csproj", "src/ManagementSystem.Core/"]
COPY ["src/ManagementSystem.EntityFrameworkCore/ManagementSystem.EntityFrameworkCore.csproj", "src/ManagementSystem.EntityFrameworkCore/"]
WORKDIR "/src/src/ManagementSystem.Web.Host"
RUN dotnet restore 

WORKDIR /src
COPY ["src/ManagementSystem.Web.Host", "src/ManagementSystem.Web.Host"]
COPY ["src/ManagementSystem.Web.Core", "src/ManagementSystem.Web.Core"]
COPY ["src/ManagementSystem.Application", "src/ManagementSystem.Application"]
COPY ["src/ManagementSystem.Core", "src/ManagementSystem.Core"]
COPY ["src/ManagementSystem.EntityFrameworkCore", "src/ManagementSystem.EntityFrameworkCore"]
WORKDIR "/src/src/ManagementSystem.Web.Host"
RUN dotnet publish -c Release -o /publish --no-restore

FROM mcr.microsoft.com/dotnet/aspnet:7.0
EXPOSE 80
WORKDIR /app
COPY --from=build /publish .
ENTRYPOINT ["dotnet", "ManagementSystem.Web.Host.dll"]
