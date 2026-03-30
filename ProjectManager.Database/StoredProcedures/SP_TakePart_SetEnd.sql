CREATE PROCEDURE [dbo].[SP_TakePart_SetEnd]
	@employeeId UNIQUEIDENTIFIER,
	@projectId UNIQUEIDENTIFIER,
	@endDate DATETIME2
AS
BEGIN
	UPDATE [TakePart] 
		SET [EndDate] = @endDate
		WHERE [EmployeeId] = @employeeId
			AND	[ProjectId] = @projectId
END
