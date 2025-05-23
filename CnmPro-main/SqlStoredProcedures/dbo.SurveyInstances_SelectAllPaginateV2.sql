USE [CnmPro]
GO
/****** Object:  StoredProcedure [dbo].[SurveyInstances_SelectAllPaginate]    Script Date: 9/6/2022 12:59:06 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
ALTER PROC [dbo].[SurveyInstances_SelectAllPaginateV2]
-- =============================================
-- Author: Michael Shepard
-- Create date: 27 August 2022
-- Description: selects data for all instances
--		from SurveyInstances, SurveyQuestions,
--		 SurveyQuestionsAnswerOptions,
--		SurveyAnswers, Surveys, Users tables
--		in paginated form
-- Code Reviewer: Rebekah Hoskins

-- MODIFIED BY: author
-- MODIFIED DATE:
-- Code Reviewer:
-- Note:
-- ==============================================
		 @PageIndex int 
		,@PageSize int
as
/*
DECLARE @PageIndex int = 0
		,@PageSize int = 3
EXECUTE dbo.SurveyInstances_SelectAllPaginateV2 @PageIndex, @PageSize
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
	ORDER BY DateTaken DESC

OFFSET @offSet Rows
FETCH NEXT @PageSize Rows ONLY

END