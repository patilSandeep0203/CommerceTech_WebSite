USE [CnmPro]
GO
/****** Object:  StoredProcedure [dbo].[Events_Select_ByOrganization]    Script Date: 9/19/2022 1:40:52 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author: <Victor Gonzalez>
-- Create date: <09/16/2022>
-- Description: <Events_slect by organization>
-- Code Reviewer:

-- MODIFIED BY: N/A
-- MODIFIED DATE:N/A
-- Code Reviewer:
-- Note:
-- =============================================

CREATE proc [dbo].[Events_Select_ByOrganization]
	@OrganizationId int

as

/*  Test Code

	Declare @OrganizationId int = 46

	Execute dbo.Events_Select_ByOrganization
		@OrganizationId


*/

Begin 
	Select e.Id,
		   e.Title,
		   e.Description,
		   e.IsMultiDay,
		   e.ImageUrl
	FROM dbo.[Events] as e
	Where e.OrganizationId = @OrganizationId

End
GO
