USE [CnmPro]
GO
/****** Object:  StoredProcedure [dbo].[SurveyInstances_SelectByInstanceId]    Script Date: 9/12/2022 4:02:10 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
ALTER PROC [dbo].[SurveyInstances_SelectByInstanceIdV2]
-- =============================================
-- Author: Michael Shepard
-- Create date: 26 August 2022
-- Description: selects data for one survey instance 
--      from SurveyInstances, SurveyQuestions, 
--		SurveyQuestionsAnswerOptions,
--		SurveyAnswers, Surveys, Users tables
-- Code Reviewer: Rebekah Hoskins

-- MODIFIED BY: author
-- MODIFIED DATE:
-- Code Reviewer:
-- Note:
-- ==============================================
		@Id int
as
/*
DECLARE @Id int = 2
EXECUTE dbo.SurveyInstances_SelectByInstanceIdV2 @Id
*/
BEGIN

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

WHERE @Id = si.Id


END