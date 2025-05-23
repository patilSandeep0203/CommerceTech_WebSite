USE [CnmPro]
GO
/****** Object:  StoredProcedure [dbo].[SurveyInstances_SearchPaginate]    Script Date: 9/12/2022 3:58:24 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
ALTER PROC [dbo].[SurveyInstances_SearchPaginateV2]
-- =============================================
-- Author: Michael Shepard
-- Create date: 29 August 2022
-- Description: selects data for all instances
--				of a survey that match a 
--				given query
-- Code Reviewer: Rebekah Hoskins

-- MODIFIED BY: author
-- MODIFIED DATE:
-- Code Reviewer:
-- Note:
-- ==============================================
		 @PageIndex int 
		,@PageSize int
		,@Query nvarchar(255)
as
/*

DECLARE @PageIndex int = 0
		,@PageSize int = 7
		--,@Query nvarchar(255) = ''
		--,@Query nvarchar(255) = 'Food'
		--,@Query nvarchar(255) = 'Playstation'
		,@Query nvarchar(255) = 'a'
EXECUTE dbo.[SurveyInstances_SearchPaginateV2] @PageIndex, @PageSize, @Query

*/
BEGIN
	DECLARE @offset int = @PageIndex * @PageSize

	DECLARE @tempTable TABLE (
		Id int,
		UserId int,
		FirstName nvarchar(100),
		LastName nvarchar(100),
		SurveyId int,
		SurveyName nvarchar(100),
		CompanyLogo nvarchar(255),
		SurveyTypeId int,
		SurveyType nvarchar(100),
		InstanceStatusId int,
		InstanceStatus nvarchar(100),
		DateTaken datetime2(7),
		SurveyData nvarchar(max)
		)
				
	INSERT INTO @tempTable
SELECT DISTINCT
		si.Id
		,u.Id as UserId
		,up.FirstName as FirstName
		,up.LastName as LastName
		,si.SurveyId as SurveyId
		,s.[Name] as SurveyName
		,s.CompanyLogo
		,st.Id as SurveyTypeId
		,st.[Name] as SurveyType
		,stat.Id as InstanceStatusId
		,stat.[Name] as InstanceStatus
		,si.DateCreated as DateTaken
		,SurveyData=
				(
				SELECT	
					Question = 
						(
						SELECT 
							sq.Id as Id
							,sq.Question as Question
							,qt.Id as QuestionTypeId
							,qt.[Name] as QuestionTypeName
						WHERE si.Id = sa.InstanceId
						FOR JSON PATH
						)
					,sa.Answer AS OpenEndedAnswer
					,sa.AnswerNumber AS BinaryAnswer
					,AnswerOptions.[Text] AS AnswerOptionText
					,AnswerOptions.[Value] AS AnswerOptionValue
				FROM dbo.SurveyQuestions as sq
				inner join dbo.QuestionTypes as qt on sq.QuestionTypeId = qt.Id
				left outer join dbo.SurveyAnswers as sa on sq.id = sa.QuestionId
				left outer join dbo.SurveyQuestionAnswerOptions as AnswerOptions on sa.AnswerOptionId = AnswerOptions.Id
				WHERE si.Id = sa.InstanceId
				FOR JSON AUTO
					)

FROM dbo.SurveyInstances as si
	inner join dbo.Users as u on si.UserId = u.Id
	inner join dbo.UserProfiles as up on u.Id = up.UserId
	inner join dbo.Surveys as s on si.SurveyId = s.Id
	inner join dbo.SurveyStatus as stat on si.StatusId = stat.Id
	inner join dbo.SurveyTypes as st on s.SurveyTypeId = st.Id
	inner join dbo.SurveyQuestions as sq on sq.SurveyId = s.Id
	inner join dbo.SurveyAnswers as sa on sq.id = sa.QuestionId
	inner join dbo.SurveyQuestionAnswerOptions as AnswerOptions on sa.AnswerOptionId = AnswerOptions.Id
			WHERE si.Id = sa.InstanceId  AND
			(
			sq.Question LIKE '%' + @Query + '%'
			OR sa.Answer LIKE '%' + @Query + '%'
			OR AnswerOptions.[Text] LIKE '%' + @Query + '%'
			OR AnswerOptions.[Value] LIKE '%' + @Query + '%'
			OR st.[Name] LIKE '%' + @Query + '%'
			OR s.[Name] LIKE '%' + @Query + '%'
			OR stat.[Name] LIKE '%' + @Query + '%'
			)
	ORDER BY si.SurveyId

	SELECT
		Id
		,UserId
		,FirstName
		,LastName
		,SurveyId
		,SurveyName
		,CompanyLogo
		,SurveyTypeId
		,SurveyType
		,InstanceStatusId
		,InstanceStatus
		,DateTaken
		,SurveyData
		,TotalCount = COUNT(1) OVER()
	FROM @tempTable
	ORDER BY Id

	OFFSET @offSet Rows
	FETCH NEXT @PageSize Rows ONLY

END