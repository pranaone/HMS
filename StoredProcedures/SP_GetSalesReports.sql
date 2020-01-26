SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Ahamed Azeem
-- Create date: 19-12-2019
-- Description:	SP To Get User Roles
-- =============================================
CREATE PROCEDURE SP_GetSalesReports
	-- Add the parameters for the stored procedure here
	@PatientID int,
	@ProductID int,
	@SearchFromDate datetime,
	@SearchToDate datetime
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;


	IF @SearchFromDate = '' AND @SearchToDate = ''
	BEGIN
		IF @productID = 0
			BEGIN
				SELECT DISTINCT s.ID
				, (SELECT [Name] from Patient Where ID = s.PatientID) AS [NAME]
				, s.TotalPrice
				, s.TotalBill, 
				s.SalesDate
				 from Sales s
				join CartDetails cd on cd.CartHeaderID = s.CartHeaderID
				WHERE (@patientID =0 OR s.PatientID = @patientID) AND (@productID =0 OR cd.ProductID = @productID)
			END
		ELSE 
			BEGIN
				SELECT DISTINCT s.ID
				, (SELECT [Name] from Patient Where ID = s.PatientID) AS [NAME]
				, s.TotalPrice
				, cd.TotalPrice AS TotalBill
				, s.SalesDate
				 from Sales s
				join CartDetails cd on cd.CartHeaderID = s.CartHeaderID
				WHERE (@patientID =0 OR s.PatientID = @patientID) AND (@productID =0 OR cd.ProductID = @productID)
			END
	END
ELSE
	BEGIN
		IF @productID = 0
			BEGIN
				SELECT DISTINCT s.ID
				, (SELECT [Name] from Patient Where ID = s.PatientID) AS [NAME]
				, s.TotalPrice
				, s.TotalBill, 
				s.SalesDate
				 from Sales s
				join CartDetails cd on cd.CartHeaderID = s.CartHeaderID
				WHERE (@patientID =0 OR s.PatientID = @patientID) AND (@productID =0 OR cd.ProductID = @productID)
				AND ((CONVERT(date, s.SalesDate ) BETWEEN  CONVERT(date, @SearchFromDate )  AND CONVERT(date, @SearchToDate)))
			END
		ELSE 
			BEGIN
				SELECT DISTINCT s.ID
				, (SELECT [Name] from Patient Where ID = s.PatientID) AS [NAME]
				, s.TotalPrice
				, cd.TotalPrice AS TotalBill
				, s.SalesDate
				 from Sales s
				join CartDetails cd on cd.CartHeaderID = s.CartHeaderID
				WHERE (@patientID =0 OR s.PatientID = @patientID) AND (@productID =0 OR cd.ProductID = @productID)
				AND ((CONVERT(date, s.SalesDate ) BETWEEN  CONVERT(date, @SearchFromDate )  AND CONVERT(date, @SearchToDate)))
			END
	END

END	   
GO
