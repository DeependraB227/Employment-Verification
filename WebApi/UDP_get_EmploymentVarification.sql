-- EXEC UDP_get_EmploymentVarification 1
CREATE Procedure UDP_get_EmploymentVarification
(
	@EmployeeID int,
	@CompanyName nvarchar(max)=null,
	@VarificationCode nvarchar(max)= null
)
as
Begin
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

	declare @RESULT varchar(20) = 'Not Verified';
	SELECT @RESULT = CASE WHEN emp.EmpId > 0 THEN 'Verified' END
	FROM Employee emp
	WHERE emp.EmpId = @EmployeeID and emp.CompName = isnull(@CompanyName,emp.CompName) and 
		emp.VCode = isnull(@VarificationCode,emp.VCode);
	SELECT @RESULT RESULT;	
End