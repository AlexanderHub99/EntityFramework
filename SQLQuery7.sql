USE [12345helloappdb]
GO

DECLARE	@return_value Int

EXEC	@return_value = [dbo].[GetUsersByCompany]
		@name = N'Bob'

SELECT	@return_value as 'Return Value'

GO
