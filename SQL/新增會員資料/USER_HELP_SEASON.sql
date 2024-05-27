create database iTastyDB01

use iTastyDB01
--會員表之後記得加 NOT NULL  和UNIQUE
create table userInfo
(
	id		int primary key identity(1,1),
	name	nvarchar(20) not null,
	email	nvarchar(20) not null UNIQUE,  
	password nvarchar(20) not null,
	photo	varbinary(MAX),
	banner	varbinary(MAX),	
	intro	nvarchar(50),
	userPermissions int not null check (userPermissions BETWEEN 1 AND 3) ,--1管理員 2一般會員 3停權  狀態
	createTime smalldatetime not null default GETDATE() 
)
select * from userInfo
go
create table helpForm(
	formId	int primary key identity(1,1),
	userId int not null, 
	questionType nvarchar(200) not null, 
	questionContent nvarchar(50)  not null,
    questionImage varbinary(MAX),   
	constraint FK_Questionnaire_User foreign key (userid) references userInfo(id)
	
)

go
create table seasonalIngredients(
	id	int primary key identity(1,1),
	monthId int not null check (monthId BETWEEN 1 AND 12),
	ingredientsName  nvarchar(50)  not null,
	ingredientsImg   varbinary(MAX)  
)
select*from seasonalIngredients
select*from userInfo
select*from helpForm