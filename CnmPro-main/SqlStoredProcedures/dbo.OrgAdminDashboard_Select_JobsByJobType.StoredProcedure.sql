USE [CnmPro]
GO
/****** Object:  StoredProcedure [dbo].[OrgAdminDashboard_Select_JobsByJobType]    Script Date: 9/6/2022 1:17:27 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE proc [dbo].[OrgAdminDashboard_Select_JobsByJobType]
	@Id int

as

/* Test Code 
	
	Declare  @Id int = 1

	Execute dbo.OrgAdminDashboard_Select_JobsByJobType
		@Id

*/

Begin 

	Declare @PartTimeCount int,
			@FullTimeCount int,
			@ContractCount int

	
	SELECT @PartTimeCount = COUNT(1)
	From dbo.Jobs as j inner join dbo.JobTypes as jt
	on j.JobTypeId = jt.Id
	Where j.OrganizationId = @Id AND jt.Name = 'Part Time'


	SELECT @FullTimeCount = COUNT(1)
	From dbo.Jobs as j inner join dbo.JobTypes as jt
	on j.JobTypeId = jt.Id
	Where j.OrganizationId = @Id AND jt.Name = 'Full Time'

	SELECT @ContractCount = COUNT(1)
	From dbo.Jobs as j inner join dbo.JobTypes as jt
	on j.JobTypeId = jt.Id
	Where j.OrganizationId = @Id AND jt.Name = 'Contract'

	SELECT PartTimeCount = @PartTimeCount,
		   FullTimeCount = @FullTimeCount,
		   ContractCount = @ContractCount

End
GO
