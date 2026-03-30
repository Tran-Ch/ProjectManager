CREATE PROCEDURE [dbo].[SP_Project_Insert]
	@projectManagerId UNIQUEIDENTIFIER,
	@name NVARCHAR(256),
	@description NVARCHAR(MAX)
AS
BEGIN
	INSERT INTO [Project] ([Name], [Description], [ProjectManagerId])
		OUTPUT [inserted].[ProjectId]
		VALUES (@name, @description, @projectManagerId);		
END
