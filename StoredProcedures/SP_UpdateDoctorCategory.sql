SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Ahamed Azeem
-- Create date: 19-12-2019
-- Description:	SP To Update A Doctor Category
-- =============================================
CREATE PROCEDURE SP_UpdateDoctorCategory
	-- Add the parameters for the stored procedure here
	@DoctorCategory VARCHAR(50),
	@DoctorCategoryID int
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    UPDATE Doctor_Category SET Name = @DoctorCategory WHERE ID = @DoctorCategoryID
END 
GO
