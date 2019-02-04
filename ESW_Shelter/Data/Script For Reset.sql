/*********** LIMPEZA DA BASE DE DADOS ****************/
/*
	NOTA! Mudar onde Diz ESWShelterDB para ShelterDB
*/
DELETE FROM AnimalUsers
DBCC CHECKIDENT ('ESWShelterDB.dbo.AnimalUsers',RESEED, 0)

DELETE FROM AnimalProduct
DBCC CHECKIDENT ('ESWShelterDB.dbo.AnimalProduct',RESEED, 0)

DELETE FROM Animal
DBCC CHECKIDENT ('ESWShelterDB.dbo.Animal',RESEED, 0)

DELETE FROM AnimalRace
DBCC CHECKIDENT ('ESWShelterDB.dbo.AnimalRace',RESEED, 0)

DELETE FROM DonationProduct
DBCC CHECKIDENT ('ESWShelterDB.dbo.DonationProduct',RESEED, 0)

DELETE FROM Donation
DBCC CHECKIDENT ('ESWShelterDB.dbo.Donation',RESEED, 0)

DELETE FROM Products
DBCC CHECKIDENT ('ESWShelterDB.dbo.Products',RESEED, 0)

DELETE FROM AnimalTypes
DBCC CHECKIDENT ('ESWShelterDB.dbo.AnimalTypes',RESEED, 0)

DELETE FROM ProductTypes
DBCC CHECKIDENT ('ESWShelterDB.dbo.ProductTypes',RESEED, 0)

DELETE FROM Images
DBCC CHECKIDENT ('ESWShelterDB.dbo.Images',RESEED, 0)

DELETE FROM Users
DBCC CHECKIDENT ('ESWShelterDB.dbo.Users',RESEED, 0)

DELETE FROM Roles
DBCC CHECKIDENT ('ESWShelterDB.dbo.Roles',RESEED, 0)
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