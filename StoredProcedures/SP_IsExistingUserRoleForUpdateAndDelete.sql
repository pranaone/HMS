SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Ahamed Azeem
-- Create date: 19-12-2019
-- Description:	SP To Check If the User Role Exists For Update And Delete
-- =============================================
CREATE PROCEDURE SP_IsExistingUserRoleForUpdateAndDelete
	-- Add the parameters for the stored procedure here
	@UserRoleID INT
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    SELECT * from User_Role WHERE ID = @UserRoleID
END
GO
