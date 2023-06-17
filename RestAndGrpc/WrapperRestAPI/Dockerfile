FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443
EXPOSE 5018
EXPOSE 7030


FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["WrapperRestAPI/WrapperRestAPI.csproj", "WrapperRestAPI/"]
RUN dotnet restore "WrapperRestAPI/WrapperRestAPI.csproj"
COPY . .
WORKDIR "/src/WrapperRestAPI"
RUN dotnet build "WrapperRestAPI.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "WrapperRestAPI.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "WrapperRestAPI.dll"]
