
IF EXISTS (SELECT * FROM INFORMATION_SCHEMA.TABLES 
           WHERE TABLE_NAME = N'Users')
BEGIN
DROP TABLE Users
END

CREATE TABLE Users
(
	Id UNIQUEIDENTIFIER DEFAULT NEWSEQUENTIALID() PRIMARY KEY,
	Email varchar(255) UNIQUE not null,
	[Password] varchar(255) not null,
	CreatedDate DATETIMEOFFSET not null,
	LastUpdatedDate DATETIMEOFFSET,
)
GO

IF EXISTS (SELECT * FROM sys.objects WHERE type = 'P' AND name = 'CheckEmailInUse')
DROP PROCEDURE CheckEmailInUse
GO

CREATE PROCEDURE CheckEmailInUse
@Email varchar(255),
@Exists bit output
AS
SET NOCOUNT ON;  
    SELECT @Exists = 1
    FROM Users
    WHERE Email = @Email

GO

IF EXISTS (SELECT * FROM sys.objects WHERE type = 'P' AND name = 'CreateAcount')
DROP PROCEDURE CreateAcount
GO

CREATE PROCEDURE CreateAcount
@Email varchar(255),
@Password varchar(255),
@CreatedDate DATETIMEOFFSET
AS
Insert into Users (Email, Password, CreatedDate)
values (@Email, @Password, @CreatedDate)
GO