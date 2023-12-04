# jcf.estacionamento

## Executar o projeto:

1 - Baixar/clonar: 

```
git clone https://github.com/joaocarias/jcf.estacionamento.git
```

O projeto encontra-se divido em duas partes:

1.1 - Api 

Encontra-se em: Jcf.Estacionamento/

1.2 - Frontend

Encontra-se em: jcf-frontend-estacionamento/

2 - Configs

A Api foi desenvolvido em .net core 7.0 e  MySQL Server como banco de dados. É necessário configurar a conexão do banco nos appsettings.

```
  "ConnectionStrings": {
    "DefaultConnection": "server=localhost;user=btztransportsuser;password=btztransports@1827;database=db_estacionamento_prod"
  },
```

2.1 - O frontend

O frontend foi desenvolvido em Angular Framework utilizando a versão 16.2.0.

3 - Executar o projeto

```
cd jcf.estacionamento/
dotnet run --project Jcf.Estacionamento/Jcf.Estacionamento.Api/

```

A base de dados é criado via migration, quando executar a aplicação o banco é criado/gerado.

Logo após de complilado e executado, acesse o swagger, como exemplo [no link](https://localhost:7020/swagger/index.html):  

## Coleção do Postman

Segue o link: [coleção](/temp/ApiEstacionamento.postman_collection.json)

