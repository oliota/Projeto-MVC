# Projeto-MVC
Projeto C# de exemplo utilizando MVC 5

 <img src="https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcRLDFinUslL6IeWUb1zxGBseg7EXWR1VNkxxij4-hpUtdr7ezwe&s" title="FVCproductions" alt="FVCproductions"> 
 
# Configurações do banco de dados
No Sql Server é necessário criar a base de dados, favor executar os script abaixo
## Scripts

```sql
create database BDProjetoMVC
go

use BDProjetoMVC
go

create table Empresas
(
  Id int primary key identity,
  NomeFantasia varchar(60) not null ,
  UF varchar(2) not null ,
  CNPJ varchar(60) not null 
)
 

create table Fornecedores
(
  Id int primary key identity,
  Empresa int	not null ,
  Nome varchar(60) not null ,
  CpfCnpj varchar(60) not null ,
  DataCadastro datetime not null ,
  RG varchar(60),
  DataNascimento datetime,
  FOREIGN KEY (Empresa) REFERENCES Empresas(Id)
)

create table Telefones
(
  Id int primary key identity,
  Fornecedor int not null ,
  Numero varchar(60) not null 
  FOREIGN KEY (Fornecedor) REFERENCES Fornecedores(Id)
)
```

>No visual studio localize a solution do projeto

>No projeto aberto localize o arquivo WebConfig

>É necessario alterar o connectionString com as definições da maquina
Substituir o "LAPTOP-URIUHHVG\SQLEXPRESS"
```
<connectionStrings>
  <add name="BDProjetoMVC" connectionString="Data Source=LAPTOP-URIUHHVG\SQLEXPRESS; Initial Catalog=BDProjetoMVC; Integrated Security=True; MultipleActiveResultSets=True;"
    providerName="System.Data.SqlClient" />
</connectionStrings>
```


***Recompile a solução no modo debug***

[![INSERT YOUR GRAPHIC HERE](https://i.imgur.com/hjWUYTy.png)]()

***Execute a solução no modo release***

[![INSERT YOUR GRAPHIC HERE]( https://i.imgur.com/UfAruvX.png)]()

 
***Layout do sistema***
  
[![INSERT YOUR GRAPHIC HERE](https://i.imgur.com/R2znkcd.png)]()

[![INSERT YOUR GRAPHIC HERE](https://i.imgur.com/RwuEoQO.png)]()


[![INSERT YOUR GRAPHIC HERE](https://i.imgur.com/uHbCdfk.png)]()


[![INSERT YOUR GRAPHIC HERE](https://i.imgur.com/InNhqHi.png)]()


[![INSERT YOUR GRAPHIC HERE](https://i.imgur.com/bRu9mKb.png)]()

 
