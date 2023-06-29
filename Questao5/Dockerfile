FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 5072

ENV ASPNETCORE_URLS=http://+:5072
ENV DOTNET_NOLOGO=true
ENV DOTNET_CLI_TELEMETRY_OPTOUT=true

# Creates a non-root user with an explicit UID and adds permission to access the /app folder
# For more info, please refer to https://aka.ms/vscode-docker-dotnet-configure-containers
RUN adduser -u 5678 --disabled-password --gecos "" appuser && chown -R appuser /app
USER appuser

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src

COPY Questao5/Questao5.csproj Questao5/

RUN dotnet restore Questao5/Questao5.csproj

COPY . .
WORKDIR /src/Questao5
RUN dotnet build Questao5.csproj -c Release -o /app/build

FROM build AS publish
RUN dotnet publish Questao5.csproj -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Questao5.dll"]