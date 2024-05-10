insert into ServiceProvider values
	('Viettel'),
	('Mobifone'),
	('Vinaphone')
go
insert into RechargePlans values
	(1,'Data Viettel ST30K',7,30000,7),
	(1,'Data Viettel SD90',30,90000,45),
	(1,'Data Viettel ST120K',30,120000,60),
	(2,'Data Mobifone MBF30',7,30000,30),
	(2,'Data Mobifone V90',30,90000,30),
	(2,'Data Mobifone F120',30,120000,12),
	(3,'Data Vinaphone ES7',7,30000,7),
	(3,'Data Vinaphone DT90',30,90000,15),
	(3,'Data Vinaphone DT120',30,120000,20)
go

insert into Role values
	('admin','2024-01-15 23:00:00'),
	('user','2024-01-16 01:00:00')
go

insert into Users values
	(1,1,'John Doe','john@gmail.com','0987654321',LOWER(CONVERT(varchar(32),HASHBYTES('md5','12345678'),2)),1),
	(2,4,'Michael','michael@gmail.com','0987654322',LOWER(CONVERT(varchar(32),HASHBYTES('md5','123456789'),2)),2)
go

insert into Wallet values
	(1,50000),
	(2,100000)
go

insert into RechargeLogs values
	(1,2,'2024-01-02 14:00:00','2024-02-02 14:00:00')
go
-- select
select * from ServiceProvider
select * from RechargePlans
select * from Role
select * from Users -- 0987654323 && 11111111
select * from Wallet
select * from RechargeLogs
select * from Contact
select * from RechargeReport
