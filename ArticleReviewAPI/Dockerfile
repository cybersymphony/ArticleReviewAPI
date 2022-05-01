#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:5.0 AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
WORKDIR /src
COPY ["ArticleReviewAPI/ArticleReviewAPI.csproj", "ArticleReviewAPI/"]
COPY ["ArticleReviewAPI.Shared/ArticleReviewAPI.Shared.csproj", "ArticleReviewAPI.Shared/"]
RUN dotnet restore "ArticleReviewAPI/ArticleReviewAPI.csproj"
COPY . .
WORKDIR "/src/ArticleReviewAPI"
RUN dotnet build "ArticleReviewAPI.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "ArticleReviewAPI.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "ArticleReviewAPI.dll"]