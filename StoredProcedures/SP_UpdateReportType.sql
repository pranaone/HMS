SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Ahamed Azeem
-- Create date: 21-12-2019
-- Description:	SP To UPDATE A Report Type
-- =============================================
CREATE PROCEDURE SP_UpdateReportType
	-- Add the parameters for the stored procedure here
	@ReportType VARCHAR(50),
	@ReportTypeID int
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    UPDATE ReportType SET [Name] = @ReportType WHERE ID = @ReportTypeID
END 
GO
