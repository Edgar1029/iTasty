create database iTastyDB

use iTastyDB

create table user_info
(
	id		int primary key identity(1,1),
	name	nvarchar(20) not null,
	photo	varbinary(MAX)
)

create table user_follower
(
	userID			int not null,
	followerID		int not null,
	followDate		DATETIME not null,
	unfollowDate	DATETIME,
	primary key (userID, followerID),
	FOREIGN KEY (userID) REFERENCES user_info(id),
	FOREIGN KEY (followerID) REFERENCES user_info(id) 
)



select * from user_info
select * from user_follower
where userID = 1 and followerID = 3


