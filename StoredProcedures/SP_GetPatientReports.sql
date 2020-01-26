SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Ahamed Azeem
-- Create date: 19-12-2019
-- Description:	SP To Get User Roles
-- =============================================
CREATE PROCEDURE SP_GetPatientReports
	-- Add the parameters for the stored procedure here
	@PatientID int
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    SELECT rpt.[ID]
      ,p.[Name]
      ,rt.[Name]
      ,rpt.[Results]
      ,rpt.[SampleDate]
      ,rpt.[TestedDate]
      ,rpt.[Remarks]
      ,rpt.[Fee]
      ,rpt.[ReportHtml] from [Report] rpt
	  join Patient p on p.ID = rpt.[PatientID]
	  join ReportType rt on rt.ID = rpt.[ReportType]
	  WHERE p.ID = @PatientID
	  ORDER BY rpt.ID desc
END	   
GO
