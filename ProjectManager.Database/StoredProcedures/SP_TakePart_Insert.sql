CREATE PROCEDURE [dbo].[SP_TakePart_Insert]
	@employeeId UNIQUEIDENTIFIER,
	@projectId UNIQUEIDENTIFIER,
	@startDate DATETIME2
AS
BEGIN
	INSERT [TakePart] ([EmployeeId],[ProjectId],[StartDate])
	VALUES (@employeeId,@projectId,@startDate)
END
