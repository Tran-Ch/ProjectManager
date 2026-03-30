CREATE PROCEDURE [dbo].[SP_Project_Get_FromProjectManagerId]
	@projectManagerId UNIQUEIDENTIFIER
AS
BEGIN
	SELECT	[ProjectId],
			[Name],
			[Description],
			[CreationDate],
			[ProjectManagerId]
		FROM [Project]
		WHERE [ProjectManagerId] = @projectManagerId
END
