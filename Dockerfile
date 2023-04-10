#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["AppointmentService_API/AppointmentService_API.csproj", "AppointmentService_API/"]
COPY ["APBusinessLogic/APBusinessLogic.csproj", "APBusinessLogic/"]
COPY ["APDataLayer/APDataLayer.csproj", "APDataLayer/"]
COPY ["APModels/APModels.csproj", "APModels/"]
RUN dotnet restore "AppointmentService_API/AppointmentService_API.csproj"
COPY . .
WORKDIR "/src/AppointmentService_API"
RUN dotnet build "AppointmentService_API.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "AppointmentService_API.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "AppointmentService_API.dll"]