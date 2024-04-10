FROM node:20-alpine AS client-builder
ENV TZ="America/Sao_Paulo"
WORKDIR /app
COPY /src/PrevisaoTempo.AngularApp/package.json ./
COPY /src/PrevisaoTempo.AngularApp/package-lock.json ./
COPY /src/PrevisaoTempo.AngularApp/tsconfig.json ./
RUN npm install
COPY /src/PrevisaoTempo.AngularApp/ .
RUN npm run build-prod

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS server-builder
WORKDIR /server
COPY ["/src/PrevisaoTempo.Api/PrevisaoTempo.Api.csproj", "/server/src/PrevisaoTempo.Api/PrevisaoTempo.Api.csproj"]
COPY ["/src/PrevisaoTempo.Application/PrevisaoTempo.Application.csproj", "/server/src/PrevisaoTempo.Application/PrevisaoTempo.Application.csproj"]
COPY ["/src/PrevisaoTempo.Domain/PrevisaoTempo.Domain.csproj", "/server/src/PrevisaoTempo.Domain/PrevisaoTempo.Domain.csproj"]
COPY ["/src/PrevisaoTempo.Infraestructure/PrevisaoTempo.Infraestructure.csproj", "/server/src/PrevisaoTempo.Infraestructure/PrevisaoTempo.Infraestructure.csproj"]
RUN dotnet restore /server/src/PrevisaoTempo.Api/PrevisaoTempo.Api.csproj
COPY . .
RUN dotnet build
RUN dotnet publish -c Release ./src/PrevisaoTempo.Api/PrevisaoTempo.Api.csproj -o /publish/

FROM mcr.microsoft.com/dotnet/aspnet:8.0
RUN DEBIAN_FRONTEND=noninteractive TZ=America/Sao_Paulo apt-get update && apt-get install -y tzdata
ENV TZ=America/Sao_Paulo
RUN cat /usr/share/zoneinfo/$TZ > /etc/localtime \
&& cat /usr/share/zoneinfo/$TZ > /etc/timezone
RUN sed -i 's/TLSV1.2/TLSv1.1/g' /etc/ssl/openssl.cnf
RUN sed -i 's/DEFAULT@SECLEVEL=2/DEFAULT@SECLEVEL=0/g' /etc/ssl/openssl.cnf
EXPOSE 8080 
ENV ASPNETCORE_URLS http://+:8080
WORKDIR /app
RUN mkdir wwwroot
COPY --from=server-builder /publish .
COPY --from=client-builder /app/dist/browser ./wwwroot
ENTRYPOINT ["dotnet", "PrevisaoTempo.Api.dll"]