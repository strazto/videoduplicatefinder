FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build-env

WORKDIR /app
COPY VideoDuplicateFinder.Web/*.csproj  VideoDuplicateFinder.Web/
COPY DuplicateFinderEngine/*.csproj  DuplicateFinderEngine/

RUN dotnet restore DuplicateFinderEngine && \
	dotnet restore VideoDuplicateFinder.Web 
 
COPY VideoDuplicateFinder.Web/  VideoDuplicateFinder.Web/
COPY DuplicateFinderEngine/  DuplicateFinderEngine/
RUN dotnet publish -c Release --no-self-contained -o out VideoDuplicateFinder.Web


FROM mcr.microsoft.com/dotnet/aspnet:3.1-alpine
RUN apk add --no-cache --repository=https://dl-cdn.alpinelinux.org/alpine/edge/main ffmpeg && \
	apk add --no-cache --repository=https://dl-cdn.alpinelinux.org/alpine/edge/testing libgdiplus
WORKDIR /app
COPY --from=build-env /app/out .
ENTRYPOINT [ "dotnet", "VideoDuplicateFinder.Web.dll" ]
