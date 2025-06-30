Para utilizar os comandos do migrations, instale o dotnet-ef no terminal: 
`dotnet tool install --global dotnet-ef --version 8.*` .

Depois de criar/atualizar um Model, rode o comando: `dotnet ef migrations add {nome_migration}`

Para atualizar no banco, rode o comando: `dotnet ef database update` 

Nesse projeto, foram instaladas as seguintes dependencias:

- Microsoft.EntityFrameworkCore
- Microsoft.EntityFrameworkCore.Design
- Npgsql
- Npgsql.EntityFrameworkCore.PostgreSQL
- Dapper