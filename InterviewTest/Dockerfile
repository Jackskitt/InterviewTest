FROM mcr.microsoft.com/dotnet/core/aspnet:2.2-stretch-slim AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/core/sdk:2.2-stretch AS build
WORKDIR /src
COPY ["InterviewTest/InterviewTest.csproj", "InterviewTest/"]
RUN dotnet restore "InterviewTest/InterviewTest.csproj"
COPY . .
WORKDIR "/src/InterviewTest"
RUN dotnet build "InterviewTest.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "InterviewTest.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "InterviewTest.dll"]