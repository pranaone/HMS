SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Ahamed Azeem
-- Create date: 19-12-2019
-- Description:	SP To Add A New User Role
-- =============================================
CREATE PROCEDURE SP_AddNewReportType
	-- Add the parameters for the stored procedure here
	@ReportType VARCHAR(50),
	@DateAdded DateTime
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    INSERT INTO ReportType([Name],DateAdded) VALUES(@ReportType, @DateAdded)
END 
GO
