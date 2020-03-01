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

***Execute a solução no modo release***

[![INSERT YOUR GRAPHIC HERE]( https://i.imgur.com/UfAruvX.png)]()

 
***Layout do sistema***

[![INSERT YOUR GRAPHIC HERE](https://i.imgur.com/eh6YomJ.png)]()

 
