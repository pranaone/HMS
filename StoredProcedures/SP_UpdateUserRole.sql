SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Ahamed Azeem
-- Create date: 19-12-2019
-- Description:	SP To Add A New User Role
-- =============================================
CREATE PROCEDURE SP_UpdateUserRole
	-- Add the parameters for the stored procedure here
	@UserRole VARCHAR(50),
	@RoleID int
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    UPDATE User_Role SET Name = @UserRole WHERE ID = @RoleID
END 
GO
