SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Ahamed Azeem
-- Create date: 18-12-2019
-- Description:	SP To Check If the Doctor Category Exists
-- =============================================
CREATE PROCEDURE [dbo].[SP_IsExistingDoctorCategory]
	-- Add the parameters for the stored procedure here
	@DoctorCategory VARCHAR(10)
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    SELECT * from Doctor_Category WHERE Name = @DoctorCategory
END
