#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["VacancyAnalyzerHH/VacancyAnalyzerHH.Api.csproj", "VacancyAnalyzerHH.Api/"]
#COPY ["VacancyAnalyzerHH/VacancyAnalyzerHH.Application.csproj", "VacancyAnalyzerHH.Application/"]
#COPY ["VacancyAnalyzerHH/VacancyAnalyzerHH.Contracts.csproj", "VacancyAnalyzerHH.Contracts/"]
#COPY ["VacancyAnalyzerHH/VacancyAnalyzerHH.Core.csproj", "VacancyAnalyzerHH.Core/"]
#COPY ["VacancyAnalyzerHH/VacancyAnalyzerHH.Infrastructure.csproj", "VacancyAnalyzerHH.Infrastructure/"]

RUN dotnet restore "VacancyAnalyzerHH/VacancyAnalyzerHH.Api.csproj"
COPY . .
WORKDIR "/src/VacancyAnalyzerHH"
RUN dotnet build "VacancyAnalyzerHH.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "VacancyAnalyzerHH.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "VacancyAnalyzerHH.Api.dll"]