FROM mcr.microsoft.com/dotnet/sdk:6.0.100 AS build

# Work around for broken dotnet restore
ADD http://ftp.us.debian.org/debian/pool/main/c/ca-certificates/ca-certificates_20210119_all.deb .
RUN dpkg -i ca-certificates_20210119_all.deb

RUN curl https://deb.nodesource.com/setup_16.x | bash
RUN curl https://dl.yarnpkg.com/debian/pubkey.gpg | apt-key add -
RUN echo "deb https://dl.yarnpkg.com/debian/ stable main" | tee /etc/apt/sources.list.d/yarn.list
RUN apt-get update && apt-get install -y nodejs yarn

WORKDIR /app
COPY ./HBD.StaticApp .

WORKDIR /app
RUN dotnet publish ./HBD.StaticApp.csproj -r linux-x64 -c Release --self-contained true -p:PublishReadyToRun=true -p:PublishTrimmed=true -p:IncludeNativeLibrariesForSelfExtract=true -o out

FROM alpine:latest AS runtime

# RUN useradd -u 1000 user
# USER user

WORKDIR /app
COPY --from=build /app/out ./

ENTRYPOINT ["HBD.StaticApp"]