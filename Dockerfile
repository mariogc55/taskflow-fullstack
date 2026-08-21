FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
WORKDIR /src

COPY ["backend/TaskFlow.API/TaskFlow.API.csproj", "TaskFlow.API/"]
COPY ["backend/TaskFlow.Application/TaskFlow.Application.csproj", "TaskFlow.Application/"]
COPY ["backend/TaskFlow.Domain/TaskFlow.Domain.csproj", "TaskFlow.Domain/"]
COPY ["backend/TaskFlow.Infrastructure/TaskFlow.Infrastructure.csproj", "TaskFlow.Infrastructure/"]

RUN dotnet restore "TaskFlow.API/TaskFlow.API.csproj"

COPY backend/ .
WORKDIR /src/TaskFlow.API
RUN dotnet publish -c Release -o /app/publish

FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS runtime
WORKDIR /app
COPY --from=build /app/publish .

ENV ASPNETCORE_URLS=http://+:$PORT
EXPOSE 8080

ENTRYPOINT ["dotnet", "TaskFlow.API.dll"]