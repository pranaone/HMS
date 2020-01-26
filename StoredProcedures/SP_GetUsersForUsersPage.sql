SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Ahamed Azeem
-- Create date: 19-12-2019
-- Description:	SP To Get User Roles
-- =============================================
CREATE PROCEDURE SP_GetUsersForUsersPage
	-- Add the parameters for the stored procedure here
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    SELECT u.ID
      ,u.Firstname
      ,u.Lastname
      ,u.Address_line_1
      ,u.Address_line_2
      ,u.PostalCode
      ,u.Email
      ,u.Password
      ,u.Salt
      ,u.Gender
      ,u.MobileNo
      ,ur.Name AS [Role]
     ,IIF(u.[Doctor_Category] = 0, 'Not A Doctor', (SELECT dc.Name FROM Doctor_Category dc WHERE dc.ID = u.[Doctor_Category])) AS [Doctor_Category]
      ,u.RegisteredDate
      ,IIF(u.ActiveStatus = 0 , 'Inactive', 'Active') AS ActiveStatus from [User] u
	  join User_Role ur on ur.ID = u.RoleID
END 
GO
