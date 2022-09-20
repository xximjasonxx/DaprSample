FROM mcr.microsoft.com/dotnet/sdk:6.0 as build
WORKDIR /code

COPY . .
RUN dotnet publish --configuration Release --output /publish

FROM mcr.microsoft.com/dotnet/aspnet:6.0 as final
WORKDIR /app

COPY --from=build /publish .
EXPOSE 80

CMD [ "dotnet", "SecretApi.dll" ]