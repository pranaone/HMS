SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Ahamed Azeem
-- Create date: 19-12-2019
-- Description:	SP To Check If the User Role Exists For Update And Delete
-- =============================================
CREATE PROCEDURE SP_IsExistingReportTypeForUpdateAndDelete
	-- Add the parameters for the stored procedure here
	@ReportTypeID INT
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    SELECT * from ReportType WHERE ID = @ReportTypeID
END
GO
