SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Ahamed Azeem
-- Create date: 19-12-2019
-- Description:	SP To Add A New User Role
-- =============================================
CREATE PROCEDURE SP_UpdateUserActiveStatus
	-- Add the parameters for the stored procedure here
	@ActiveStatus bit = 0,
	@RoleID int
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    UPDATE [User] SET ActiveStatus = @ActiveStatus WHERE ID = @RoleID
END 
GO
