Proyect in .net 6

Steps for run this proyect:

1) Create a database named Marketplace

2) Run the command: dotnet ef migrations add <name-of-migration>

2) Run the command: dotnet ef database update

The next command depends if you want run seed or not
3) Run the command: dotnet run or Run the command: dotnet run seeddata

In this case everything is commented and prepared in case you want to use the states by database

Soon this project could be built with docker