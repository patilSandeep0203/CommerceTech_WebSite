USE [CnmPro]
GO
/****** Object:  StoredProcedure [dbo].[OrgAdminDashboard_Select_JobsByActiveStatus]    Script Date: 9/6/2022 1:17:27 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE proc [dbo].[OrgAdminDashboard_Select_JobsByActiveStatus]
	@Id int

as

/* Test Code

	Declare @Id int = 1

	Execute dbo.OrgAdminDashboard_Select_JobsByActiveStatus
		@Id

*/

Begin
	Declare @ActiveJobCount int,
			@InActiveJobCount int

	Select @ActiveJobCount = COUNT(1)
	FROM dbo.Jobs as j
	Where j.OrganizationId = @Id AND j.IsActive = 1

	Select @InActiveJobCount = COUNT(1)
	FROM dbo.Jobs as j
	Where j.OrganizationId = @Id AND j.IsActive = 0

	Select ActiveJobCount = @ActiveJobCount,
		   InActiveJobCount = @InActiveJobCount

End
GO
