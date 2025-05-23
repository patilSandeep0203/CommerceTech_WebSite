USE [CnmPro]
GO
/****** Object:  StoredProcedure [dbo].[OrgAdminDashboard_GetProsposalStats]    Script Date: 9/6/2022 1:17:27 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO


CREATE proc [dbo].[OrgAdminDashboard_GetProsposalStats]
	@OrganizationId int,
	@TimezoneOffset int

as

/* TestCode

	Declare @OrganizationId int = 175
	Declare @TimezoneOffset int = 420

	Execute dbo.OrgAdminDashBoard_GetProsposalStats
		@OrganizationId,
		@TimezoneOffset

*/

Begin
	CREATE TABLE countofdays (
		date_created datetime2(7),
		count_of_proposals int
	)

	DECLARE @CurrentDate datetime2(7) = GETUTCDATE()
	DECLARE @Counter int
	SET @Counter = 0
	WHILE(@Counter < 7)
		Begin
			Declare @DateCreated datetime2(7) = CAST(DATEADD(minute, -@TimezoneOffset, DATEADD(day, -@Counter, @CurrentDate)) as DATE)
			Declare @TotalCount int = 0
			
			SELECT @TotalCount = COUNT(1) OVER()
			FROM dbo.Proposals as p inner join dbo.Jobs as j
			on p.JobId = j.Id
			inner join dbo.Organizations as o
			on j.OrganizationId = o.Id
			Where j.OrganizationId = @OrganizationId AND CAST(p.DateCreated AS DATE) = CAST(DATEADD(minute, -@TimezoneOffset, DATEADD(day, -@Counter, @CurrentDate)) as DATE)

			INSERT into countofdays
			SELECT @DateCreated
				  ,@TotalCount

			

			SET @Counter = @Counter + 1
	End
	

	SELECT date_created,
			count_of_proposals
	From countofdays

	DROP TABLE countofdays
End
GO
