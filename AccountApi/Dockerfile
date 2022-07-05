FROM mcr.microsoft.com/dotnet/sdk:6.0 as builder
WORKDIR /code

COPY . .
RUN dotnet publish -c Release -o /output

FROM mcr.microsoft.com/dotnet/aspnet:6.0 as runner
WORKDIR /app

COPY --from=builder /output .

EXPOSE 443
EXPOSE 80

CMD [ "dotnet", "AccountApi.dll" ]