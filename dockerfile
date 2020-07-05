FROM mcr.microsoft.com/dotnet/core/sdk:3.1-alpine AS build
WORKDIR /app
# copy sln and csproj files into the image
COPY *.sln .
COPY src/Repo.App/*.csproj ./src/Repo.App/
COPY tests/Repo.App.Test/*.csproj ./tests/Repo.App.Test/
# restore package dependencies for the solution
RUN dotnet restore

# copy full solution over
COPY . .
RUN dotnet build

FROM build AS testrunner
WORKDIR /app/tests/Repo.App.Test
CMD ["dotnet", "test", "--logger:trx"]
# run the unit tests
FROM build AS test
WORKDIR /app/tests/Repo.App.Test
RUN dotnet test --logger:trx
# publish the API
FROM build AS publish
WORKDIR /app/src/Repo.App
RUN dotnet publish -c Release -o out
# run the api
FROM mcr.microsoft.com/dotnet/core/aspnet:3.1-alpine AS runtime
WORKDIR /app
COPY --from=publish /app/src/Repo.App/out ./
EXPOSE 80
ENTRYPOINT ["dotnet", "Repo.App.dll"]