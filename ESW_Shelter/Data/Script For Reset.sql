/*********** LIMPEZA DA BASE DE DADOS ****************/
/*
	NOTA! Mudar onde Diz ESWShelterDB para ShelterDB
*/
DELETE FROM AnimalUsers
DBCC CHECKIDENT ('ShelterDB.dbo.AnimalUsers',RESEED, 0)

DELETE FROM AnimalProduct
DBCC CHECKIDENT ('ShelterDB.dbo.AnimalProduct',RESEED, 0)

DELETE FROM Animal
DBCC CHECKIDENT ('ShelterDB.dbo.Animal',RESEED, 0)

DELETE FROM AnimalRace
DBCC CHECKIDENT ('ShelterDB.dbo.AnimalRace',RESEED, 0)

DELETE FROM DonationProduct
DBCC CHECKIDENT ('ShelterDB.dbo.DonationProduct',RESEED, 0)

DELETE FROM Donation
DBCC CHECKIDENT ('ShelterDB.dbo.Donation',RESEED, 0)

DELETE FROM Products
DBCC CHECKIDENT ('ShelterDB.dbo.Products',RESEED, 0)

DELETE FROM AnimalTypes
DBCC CHECKIDENT ('ShelterDB.dbo.AnimalTypes',RESEED, 0)

DELETE FROM ProductTypes
DBCC CHECKIDENT ('ShelterDB.dbo.ProductTypes',RESEED, 0)

DELETE FROM Images
DBCC CHECKIDENT ('ShelterDB.dbo.Images',RESEED, 0)

DELETE FROM Users
DBCC CHECKIDENT ('ShelterDB.dbo.Users',RESEED, 0)

DELETE FROM Roles
DBCC CHECKIDENT ('ShelterDB.dbo.Roles',RESEED, 0)
/********Para o Novo Update**********/
Select *
From INFORMATION_SCHEMA.TABLES

Select *
From __EFMigrationsHistory

Delete From __EFMigrationsHistory WHERE MigrationId = '20190122101921_AnimalTables'

DROP TABLE AnimalUsers
DROP TABLE AnimalProduct
DROP TABLE Animal
DROP TABLE AnimalRace
DROP TABLE Images