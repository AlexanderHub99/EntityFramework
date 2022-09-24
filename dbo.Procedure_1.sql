CREATE PROCEDURE [dbo].[GetUserWithMaxAge]
    @name varchar(50) OUTPUT
AS
    SELECT @name = [Name] FROM Users WHERE Age = (SELECT MAX(Age) FROM Users)
RETURN 0
