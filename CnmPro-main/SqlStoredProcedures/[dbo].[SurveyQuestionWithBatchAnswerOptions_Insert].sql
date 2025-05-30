USE [CnmPro]
GO
/****** Object:  StoredProcedure [dbo].[SurveyQuestionWithBatchAnswerOptions_Insert]    Script Date: 10/6/2022 1:14:15 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
ALTER PROC [dbo].[SurveyQuestionWithBatchAnswerOptions_Insert]
-- =============================================
-- Author:		Michael Shepard
-- Create date: 5 October 2022
-- Description:	Inserts a question into the surveyquestions table
--			and then inserts a batch of answer options into the
--			surveyquestionansweroptions table
-- Code Reviewer: Steven Tran
-- ==============================================

	@SurveyId int
	,@UserId int
	,@Question nvarchar(500)
	,@HelpText nvarchar(255) NULL
	,@IsRequired bit
	,@isMultipleAllowed bit
	,@QuestionTypeId int
	,@StatusId int
	,@SortOrder int

	,@batchAnswerOptions dbo.BatchAnswerOptions READONLY
	
	,@Id int OUTPUT



as

/* TEST CODE

	DECLARE @SurveyId int = 13
			,@UserId int = 118
			,@Question nvarchar(500) = 'What animal'
			,@HelpText nvarchar(255) = 'animal'
			,@IsRequired bit = 1
			,@isMultipleAllowed bit = 0
			,@QuestionTypeId int = 2
			,@StatusId int = 1
			,@SortOrder int = 1
			,@Id int


	DECLARE @batchAnswerOptions dbo.BatchAnswerOptions

INSERT INTO @batchAnswerOptions 
						(
						[Text]
						,[Value]
						,AdditionalInfo
						,SortOrder
						)
		VALUES
						(
						'Cats'
						,NULL
						,'additional info'
						,1
						)
						,(
						'Dogs'
						,NULL
						,'more info'
						,2
						)

	EXECUTE dbo.SurveyQuestionWithBatchAnswerOptions_Insert
			@SurveyId
			,@UserId
			,@Question
			,@HelpText
			,@IsRequired
			,@isMultipleAllowed
			,@QuestionTypeId
			,@StatusId
			,@SortOrder
			,@batchAnswerOptions
			,@Id 

*/

BEGIN

INSERT INTO dbo.SurveyQuestions
			(
			SurveyId
			,UserId
			,Question
			,HelpText
			,IsRequired
			,isMultipleAllowed
			,QuestionTypeId
			,StatusId
			,SortOrder
			)
VALUES		
			(
			@SurveyId
			,@UserId
			,@Question
			,@HelpText
			,@IsRequired
			,@isMultipleAllowed
			,@QuestionTypeId
			,@StatusId
			,@SortOrder
			)
	SET @Id = SCOPE_IDENTITY()

INSERT INTO dbo.SurveyQuestionAnswerOptions
			(
			QuestionId
			,[Text]
			,[Value]
			,AdditionalInfo
			,CreatedBy
			,SortOrder
			)
	SELECT @Id
			,b.[Text]
			,b.[Value]
			,b.AdditionalInfo
			,@UserId
			,b.SortOrder
	FROM @batchAnswerOptions as b

END