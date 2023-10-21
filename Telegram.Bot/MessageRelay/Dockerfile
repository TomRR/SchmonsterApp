FROM mcr.microsoft.com/dotnet/runtime:7.0 AS base
WORKDIR /app

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["MessageRelay/MessageRelay.csproj", "MessageRelay/"]
RUN dotnet restore "MessageRelay/MessageRelay.csproj"
COPY . .
WORKDIR "/src/MessageRelay"
RUN dotnet build "MessageRelay.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "MessageRelay.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .

# copy appsettings.json
COPY ["MessageRelay/appsettings.json", "/app/"]

ENTRYPOINT ["dotnet", "MessageRelay.dll"]
