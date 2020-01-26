SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Ahamed Azeem
-- Create date: 19-12-2019
-- Description:	SP To Delete A User Role
-- =============================================
CREATE PROCEDURE SP_DeleteReportType
	-- Add the parameters for the stored procedure here
	@ReportType VARCHAR(50),
	@ReportTypeID int
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    DELETE FROM ReportType WHERE Name = @ReportType AND ID = @ReportTypeID
END 
GO
