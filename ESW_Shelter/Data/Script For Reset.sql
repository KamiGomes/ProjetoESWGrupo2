
DELETE FROM Users
DBCC CHECKIDENT ('ShelterDB.dbo.Users',RESEED, 0)

Drop Table RoleAuthorization

DELETE FROM Roles
DBCC CHECKIDENT ('ShelterDB.dbo.Roles',RESEED, 0)

Drop Table Components