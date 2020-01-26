SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Ahamed Azeem
-- Create date: 19-12-2019
-- Description:	SP To Delete A Doctor Category
-- =============================================
CREATE PROCEDURE SP_DeleteDoctorCategory
	-- Add the parameters for the stored procedure here
	@DoctorCategory VARCHAR(50),
	@DoctorCategoryID int
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    DELETE FROM Doctor_Category WHERE Name = @DoctorCategory AND ID = @DoctorCategoryID
END 
GO
