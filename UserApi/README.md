### Keycloak
O Projeto está utilizando o Keycloak para gerenciar/autorizar usuários e os endpoints da PeopleController.

Para utilizar os endpoints da PeopleController você deverá seguir o tutorial [Clique aqui](https://www.youtube.com/watch?v=LphlwLonTwA)

Ou simplesmente retirar o campo `[Authorize]` da Controller

### Migrations
Para utilizar os comandos do migrations, instale o dotnet-ef no terminal: 
`dotnet tool install --global dotnet-ef --version 8.*` .

Depois de criar/atualizar um Model, rode o comando: `dotnet ef migrations add {nome_migration}`

Para atualizar no banco, rode o comando: `dotnet ef database update`.
*Obs:* Sempre que rodar o projeto ele executará automaticamente qualquer migration que ainda não tenha sido aplicada.

### Dependencias do projeto
Nesse projeto, foram instaladas as seguintes dependencias:

- Microsoft.EntityFrameworkCore
- Microsoft.EntityFrameworkCore.Design
- Npgsql
- Npgsql.EntityFrameworkCore.PostgreSQL
- Dapper
- Keycloak.AuthServices.Authentication
- Keycloak.AuthServices.Authorization
- Keycloak.AuthServices.Sdk