# Use the official .NET SDK image as the build image
# Verify the SDK and runtime versions match the ones in global.json
FROM --platform=$BUILDPLATFORM mcr.microsoft.com/dotnet/sdk:9.0.300-bookworm-slim AS build
WORKDIR /src

# Declare build arguments for target platform
ARG TARGETPLATFORM
ARG BUILDPLATFORM
ARG TARGETOS
ARG TARGETARCH

COPY . .

# Publish the application
FROM build AS publish

# Map Docker platform to .NET RID (Runtime Identifier)
RUN case ${TARGETPLATFORM} in \
        "linux/amd64")  DOTNET_RID=linux-x64  ;; \
        "linux/arm64")  DOTNET_RID=linux-arm64  ;; \
        "linux/arm/v7") DOTNET_RID=linux-arm  ;; \
        *) echo "Unsupported platform: ${TARGETPLATFORM}" && exit 1 ;; \
    esac && \
    dotnet publish src/AzureMcp.csproj --runtime "${DOTNET_RID}" -c Release --output "/app/publish" --self-contained

# Build the runtime image
FROM mcr.microsoft.com/dotnet/aspnet:9.0.5-bookworm-slim AS runtime
WORKDIR /app

# Install Azure CLI and required dependencies
RUN apt-get update && apt-get install -y \
    curl \
    ca-certificates \
    apt-transport-https \
    lsb-release \
    gnupg \
    && curl -sL https://aka.ms/InstallAzureCLIDeb | bash \
    && rm -rf /var/lib/apt/lists/*

# Copy the published application
COPY --from=publish "/app/publish" .

ENTRYPOINT ["dotnet", "azmcp.dll", "server", "start"]
