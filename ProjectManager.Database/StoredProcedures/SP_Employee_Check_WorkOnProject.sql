CREATE PROCEDURE [dbo].[SP_Employee_Check_WorkOnProject]
	@employeeId UNIQUEIDENTIFIER,
	@projectId UNIQUEIDENTIFIER
AS
BEGIN
	SELECT	[EmployeeId]
		FROM	[TakePart]
		WHERE	[StartDate] <= GETDATE()
			AND ([EndDate] IS NULL OR [EndDate] > GETDATE())
			AND [EmployeeId] = @employeeId
			AND [ProjectId] = @projectId
	UNION
	SELECT	[ProjectManagerId]
		FROM	[Project]
		WHERE	[ProjectId] = @projectId
			AND [ProjectManagerId] = @employeeId
END
