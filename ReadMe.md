# ReadMe

## Database Migration

- EF tool much much match dotnet version for project
  - Current dotnet version in this project ( 6.0.6)
- Install ef tool if not installed
  - Test script: dotnet ef (if not installed it will throw an error)
  - Install script: dotnet tool install --global dotnet-ef
- Update ef tool if out of date
  - Update script: dotnet tool update —global dotnet-ef
- Create new migration
  - cd into root folder
  - Migration Script: dotnet ef migrations add initialCreate -p ReviewApi/ -s ReviewApi/ -o Persistence/Migrations/
    - “InitialCreate” name of the migration. It can be anything
    - “-p” the project which contains the “DbContext”
    - “-s” the project which contains the “ConnectionStrings” class
    - “-s” the project which contains the “ConnectionStrings” class
    - "-o" the output folder in which the migration scripts will be placed
- Run migration
  - cd into project which contains the DbContext
  - Migration update script: dotnet ef database update
