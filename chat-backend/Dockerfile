
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build

WORKDIR /server

#COPY PROJECT FILES
COPY *.csproj ./
RUN dotnet restore

#COPY CODE AND PUBLISH APP
COPY . .
RUN dotnet publish -o /app

#RUNTIME
FROM mcr.microsoft.com/dotnet/aspnet:8.0
EXPOSE 5154
WORKDIR /
COPY --link --from=build /app .

CMD ["dotnet", "chat-backend.dll"]
