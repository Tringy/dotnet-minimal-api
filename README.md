# How to run app

```
docker-compose up
```

## Clear environment 

```
docker-compose down --rmi all --remove-orphans
```

## how to run migrations (using at folder path /src):

add new migration
```
dotnet ef migrations add MIGRATION_NAME -p Infrastructure/Infrastructure.csproj -s Web.Api/Web.Api.csproj -o Database/Migrations
```

apply migrations:

```
dotnet ef database update -p Infrastructure/Infrastructure.csproj -s Web.Api/Web.Api.csproj
```