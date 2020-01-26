SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Ahamed Azeem
-- Create date: 18-12-2019
-- Description:	SP To Check If the User Role Exists
-- =============================================
CREATE PROCEDURE SP_IsExistingUserRole
	-- Add the parameters for the stored procedure here
	@UserRole VARCHAR(10)
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    SELECT * from User_Role WHERE Name = @UserRole
END
GO
