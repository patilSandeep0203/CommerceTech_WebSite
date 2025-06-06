USE [master]
GO
/****** Object:  Database [CnmPro]    Script Date: 12/2/2022 3:13:21 PM ******/
CREATE DATABASE [CnmPro]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'CnmPro', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL13.MSSQLSERVER\MSSQL\DATA\CnmPro.mdf' , SIZE = 73728KB , MAXSIZE = UNLIMITED, FILEGROWTH = 65536KB )
 LOG ON 
( NAME = N'CnmPro_log', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL13.MSSQLSERVER\MSSQL\DATA\CnmPro_log.ldf' , SIZE = 466944KB , MAXSIZE = 2048GB , FILEGROWTH = 65536KB )
GO
ALTER DATABASE [CnmPro] SET COMPATIBILITY_LEVEL = 130
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [CnmPro].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [CnmPro] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [CnmPro] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [CnmPro] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [CnmPro] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [CnmPro] SET ARITHABORT OFF 
GO
ALTER DATABASE [CnmPro] SET AUTO_CLOSE OFF 
GO
ALTER DATABASE [CnmPro] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [CnmPro] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [CnmPro] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [CnmPro] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [CnmPro] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [CnmPro] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [CnmPro] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [CnmPro] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [CnmPro] SET  DISABLE_BROKER 
GO
ALTER DATABASE [CnmPro] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [CnmPro] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [CnmPro] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [CnmPro] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [CnmPro] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [CnmPro] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [CnmPro] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [CnmPro] SET RECOVERY FULL 
GO
ALTER DATABASE [CnmPro] SET  MULTI_USER 
GO
ALTER DATABASE [CnmPro] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [CnmPro] SET DB_CHAINING OFF 
GO
ALTER DATABASE [CnmPro] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [CnmPro] SET TARGET_RECOVERY_TIME = 60 SECONDS 
GO
ALTER DATABASE [CnmPro] SET DELAYED_DURABILITY = DISABLED 
GO
EXEC sys.sp_db_vardecimal_storage_format N'CnmPro', N'ON'
GO
ALTER DATABASE [CnmPro] SET QUERY_STORE = OFF
GO
USE [CnmPro]
GO
ALTER DATABASE SCOPED CONFIGURATION SET LEGACY_CARDINALITY_ESTIMATION = OFF;
GO
ALTER DATABASE SCOPED CONFIGURATION SET MAXDOP = 0;
GO
ALTER DATABASE SCOPED CONFIGURATION SET PARAMETER_SNIFFING = ON;
GO
ALTER DATABASE SCOPED CONFIGURATION SET QUERY_OPTIMIZER_HOTFIXES = OFF;
GO
USE [CnmPro]
GO
/****** Object:  UserDefinedTableType [dbo].[ActivityUdt]    Script Date: 12/2/2022 3:13:22 PM ******/
CREATE TYPE [dbo].[ActivityUdt] AS TABLE(
	[activityName] [nvarchar](50) NOT NULL
)
GO
/****** Object:  UserDefinedTableType [dbo].[BatchAnswerOptions]    Script Date: 12/2/2022 3:13:22 PM ******/
CREATE TYPE [dbo].[BatchAnswerOptions] AS TABLE(
	[Text] [nvarchar](500) NULL,
	[Value] [nvarchar](100) NULL,
	[AdditionalInfo] [nvarchar](200) NULL,
	[SortOrder] [int] NULL
)
GO
/****** Object:  UserDefinedTableType [dbo].[BatchQuestionAnswerOptions]    Script Date: 12/2/2022 3:13:22 PM ******/
CREATE TYPE [dbo].[BatchQuestionAnswerOptions] AS TABLE(
	[QuestionId] [int] NOT NULL,
	[Text] [nvarchar](500) NULL,
	[Value] [nvarchar](100) NULL,
	[AdditionalInfo] [nvarchar](200) NULL,
	[SortOrder] [int] NULL
)
GO
/****** Object:  UserDefinedTableType [dbo].[BatchSurveyAnswers]    Script Date: 12/2/2022 3:13:22 PM ******/
CREATE TYPE [dbo].[BatchSurveyAnswers] AS TABLE(
	[QuestionId] [int] NOT NULL,
	[AnswerOptionId] [int] NULL,
	[Answer] [nvarchar](500) NULL,
	[AnswerNumber] [int] NULL
)
GO
/****** Object:  UserDefinedTableType [dbo].[BatchSurveyQuestions]    Script Date: 12/2/2022 3:13:22 PM ******/
CREATE TYPE [dbo].[BatchSurveyQuestions] AS TABLE(
	[Question] [nvarchar](500) NOT NULL,
	[HelpText] [nvarchar](255) NULL,
	[IsRequired] [bit] NOT NULL,
	[isMultipleAllowed] [bit] NOT NULL,
	[QuestionTypeId] [int] NOT NULL,
	[StatusId] [int] NOT NULL,
	[SortOrder] [int] NOT NULL
)
GO
/****** Object:  UserDefinedTableType [dbo].[Education]    Script Date: 12/2/2022 3:13:22 PM ******/
CREATE TYPE [dbo].[Education] AS TABLE(
	[InstitutionId] [int] NULL,
	[EdProgramTypeId] [int] NULL,
	[SpecializationTypeId] [int] NULL,
	[DateStart] [datetime2](7) NULL,
	[DateEnd] [datetime2](7) NULL,
	[Description] [nvarchar](200) NULL,
	[IsGraduated] [bit] NULL,
	[CreatedBy] [int] NULL,
	[ModifiedBy] [int] NULL
)
GO
/****** Object:  UserDefinedTableType [dbo].[Experience]    Script Date: 12/2/2022 3:13:22 PM ******/
CREATE TYPE [dbo].[Experience] AS TABLE(
	[Title] [nvarchar](50) NOT NULL,
	[EmploymentTypeId] [int] NOT NULL,
	[CompanyName] [nvarchar](50) NOT NULL,
	[LocationId] [int] NOT NULL,
	[IsCurrent] [bit] NOT NULL,
	[DateStart] [datetime2](7) NOT NULL,
	[DateEnd] [datetime2](7) NOT NULL,
	[Description] [nvarchar](200) NOT NULL,
	[CreatedBy] [int] NOT NULL,
	[ModifiedBy] [int] NOT NULL
)
GO
/****** Object:  UserDefinedTableType [dbo].[ExperienceV2]    Script Date: 12/2/2022 3:13:22 PM ******/
CREATE TYPE [dbo].[ExperienceV2] AS TABLE(
	[Title] [nvarchar](50) NOT NULL,
	[EmploymentTypeId] [int] NOT NULL,
	[CompanyName] [nvarchar](50) NOT NULL,
	[LocationId] [int] NOT NULL,
	[IsCurrent] [bit] NOT NULL,
	[DateStart] [datetime2](7) NOT NULL,
	[DateEnd] [datetime2](7) NOT NULL,
	[Description] [nvarchar](200) NOT NULL,
	[CreatedBy] [int] NOT NULL,
	[ModifiedBy] [int] NOT NULL,
	[GUID] [int] NOT NULL
)
GO
/****** Object:  UserDefinedTableType [dbo].[FreelanceGoal]    Script Date: 12/2/2022 3:13:22 PM ******/
CREATE TYPE [dbo].[FreelanceGoal] AS TABLE(
	[Name] [nvarchar](30) NOT NULL
)
GO
/****** Object:  UserDefinedTableType [dbo].[IntTable]    Script Date: 12/2/2022 3:13:22 PM ******/
CREATE TYPE [dbo].[IntTable] AS TABLE(
	[Data] [int] NOT NULL
)
GO
/****** Object:  UserDefinedTableType [dbo].[JobDayOfWeek]    Script Date: 12/2/2022 3:13:22 PM ******/
CREATE TYPE [dbo].[JobDayOfWeek] AS TABLE(
	[DayOfWeek] [nvarchar](50) NULL
)
GO
/****** Object:  UserDefinedTableType [dbo].[JobScheduleDayOfWeek]    Script Date: 12/2/2022 3:13:22 PM ******/
CREATE TYPE [dbo].[JobScheduleDayOfWeek] AS TABLE(
	[DayOfWeek] [int] NOT NULL
)
GO
/****** Object:  UserDefinedTableType [dbo].[JobScheduleDayOfWeekv2]    Script Date: 12/2/2022 3:13:22 PM ******/
CREATE TYPE [dbo].[JobScheduleDayOfWeekv2] AS TABLE(
	[DayOfWeek] [nvarchar](1) NOT NULL
)
GO
/****** Object:  UserDefinedTableType [dbo].[Licences]    Script Date: 12/2/2022 3:13:22 PM ******/
CREATE TYPE [dbo].[Licences] AS TABLE(
	[LicenseStateId] [int] NOT NULL,
	[LicenseTypeId] [int] NOT NULL,
	[LicenseNumber] [nvarchar](50) NOT NULL,
	[DateExpires] [date] NOT NULL
)
GO
/****** Object:  UserDefinedTableType [dbo].[LicenseV2]    Script Date: 12/2/2022 3:13:22 PM ******/
CREATE TYPE [dbo].[LicenseV2] AS TABLE(
	[LicenseStateId] [int] NOT NULL,
	[LicenseTypeId] [int] NOT NULL,
	[LicenseNumber] [nvarchar](50) NOT NULL,
	[DateExpires] [date] NOT NULL,
	[CreateBy] [int] NOT NULL
)
GO
/****** Object:  UserDefinedTableType [dbo].[Role]    Script Date: 12/2/2022 3:13:22 PM ******/
CREATE TYPE [dbo].[Role] AS TABLE(
	[Name] [nvarchar](50) NULL
)
GO
/****** Object:  UserDefinedTableType [dbo].[Skills]    Script Date: 12/2/2022 3:13:22 PM ******/
CREATE TYPE [dbo].[Skills] AS TABLE(
	[Name] [nvarchar](32) NOT NULL
)
GO
/****** Object:  UserDefinedTableType [dbo].[UserProfessionTypes]    Script Date: 12/2/2022 3:13:22 PM ******/
CREATE TYPE [dbo].[UserProfessionTypes] AS TABLE(
	[Name] [nvarchar](100) NULL
)
GO
/****** Object:  Table [dbo].[Blogs]    Script Date: 12/2/2022 3:13:22 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Blogs](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[BlogTypeId] [int] NOT NULL,
	[AuthorId] [int] NOT NULL,
	[Title] [nvarchar](50) NOT NULL,
	[Subject] [nvarchar](50) NOT NULL,
	[Content] [nvarchar](255) NULL,
	[IsPublished] [bit] NOT NULL,
	[ImageUrl] [nvarchar](255) NULL,
	[DateCreated] [datetime2](7) NOT NULL,
	[DateModified] [datetime2](7) NOT NULL,
	[DatePublish] [datetime2](7) NULL,
 CONSTRAINT [PK_Blogs] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[BlogTypes]    Script Date: 12/2/2022 3:13:23 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[BlogTypes](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](100) NOT NULL,
 CONSTRAINT [PK_BlogTypes] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[CnmProSubscriptions]    Script Date: 12/2/2022 3:13:23 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[CnmProSubscriptions](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](100) NOT NULL,
	[isActive] [bit] NOT NULL,
	[isArchived] [bit] NOT NULL,
	[Price] [smallmoney] NOT NULL,
	[StripeProdId] [nvarchar](50) NULL,
	[StripePriceId] [nvarchar](50) NULL,
	[Description] [nvarchar](500) NOT NULL,
	[DateCreated] [datetime2](7) NOT NULL,
	[DateModified] [datetime2](7) NULL,
	[DateArchived] [datetime2](7) NULL,
	[CreatedBy] [int] NOT NULL,
	[ModifiedBy] [int] NULL,
 CONSTRAINT [PK_CnmProSubscriptions] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Comments]    Script Date: 12/2/2022 3:13:23 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Comments](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Subject] [nvarchar](50) NULL,
	[Text] [nvarchar](3000) NOT NULL,
	[ParentId] [int] NULL,
	[EntityTypeId] [int] NOT NULL,
	[EntityId] [int] NOT NULL,
	[DateCreated] [datetime2](7) NOT NULL,
	[DateModified] [datetime2](7) NOT NULL,
	[CreatedBy] [int] NOT NULL,
	[IsDeleted] [bit] NOT NULL,
 CONSTRAINT [PK_Comments] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[DailyLinks]    Script Date: 12/2/2022 3:13:23 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[DailyLinks](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[DailyLink] [nvarchar](255) NOT NULL,
	[CreatedBy] [int] NOT NULL,
	[DateCreated] [datetime2](7) NOT NULL,
 CONSTRAINT [PK_DailyLinks] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[DailyUser]    Script Date: 12/2/2022 3:13:23 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[DailyUser](
	[DailyLinkId] [int] NOT NULL,
	[RecipientId] [int] NOT NULL
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[DaysOfWeeks]    Script Date: 12/2/2022 3:13:23 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[DaysOfWeeks](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](50) NOT NULL,
 CONSTRAINT [PK_DaysOfWeeks] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[EdProgramTypes]    Script Date: 12/2/2022 3:13:23 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[EdProgramTypes](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](30) NOT NULL,
 CONSTRAINT [PK_EdProgramTypes] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Education]    Script Date: 12/2/2022 3:13:23 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Education](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[InstitutionId] [int] NOT NULL,
	[EdProgramTypeId] [int] NOT NULL,
	[SpecializationTypeId] [int] NOT NULL,
	[DateStart] [datetime2](7) NOT NULL,
	[DateEnd] [datetime2](7) NOT NULL,
	[Description] [nvarchar](200) NOT NULL,
	[IsGraduated] [bit] NOT NULL,
	[CreatedBy] [int] NOT NULL,
	[ModifiedBy] [int] NOT NULL,
	[DateCreated] [datetime2](7) NOT NULL,
	[DateModified] [datetime2](7) NOT NULL,
 CONSTRAINT [PK_Education] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[EmploymentTypes]    Script Date: 12/2/2022 3:13:23 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[EmploymentTypes](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](30) NOT NULL,
 CONSTRAINT [PK_EmploymentTypes] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[EntityTypes]    Script Date: 12/2/2022 3:13:23 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[EntityTypes](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](100) NOT NULL,
 CONSTRAINT [PK_EntityTypes] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Events]    Script Date: 12/2/2022 3:13:23 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Events](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[EventTypeId] [int] NOT NULL,
	[LocationId] [int] NOT NULL,
	[OrganizationId] [int] NOT NULL,
	[Title] [nvarchar](128) NOT NULL,
	[Description] [nvarchar](500) NULL,
	[ImageUrl] [nvarchar](255) NULL,
	[IsMultiDay] [bit] NOT NULL,
	[StartDate] [datetime2](7) NOT NULL,
	[EndDate] [datetime2](7) NOT NULL,
	[CreatedBy] [int] NOT NULL,
	[ModifiedBy] [int] NOT NULL,
	[DateCreated] [datetime2](7) NOT NULL,
	[DateModified] [datetime2](7) NOT NULL,
 CONSTRAINT [PK_Events] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[EventTypes]    Script Date: 12/2/2022 3:13:23 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[EventTypes](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](100) NOT NULL,
 CONSTRAINT [PK_EventTypes] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Experience]    Script Date: 12/2/2022 3:13:23 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Experience](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Title] [nvarchar](50) NOT NULL,
	[EmploymentTypeId] [int] NOT NULL,
	[CompanyName] [nvarchar](50) NOT NULL,
	[LocationId] [int] NOT NULL,
	[IsCurrent] [bit] NOT NULL,
	[DateStart] [datetime2](7) NOT NULL,
	[DateEnd] [datetime2](7) NOT NULL,
	[Description] [nvarchar](200) NOT NULL,
	[CreatedBy] [int] NOT NULL,
	[ModifiedBy] [int] NOT NULL,
	[DateCreated] [datetime2](7) NOT NULL,
	[DateModified] [datetime2](7) NOT NULL,
 CONSTRAINT [PK_Experience] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[ExternalLinks]    Script Date: 12/2/2022 3:13:23 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ExternalLinks](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[UserId] [int] NOT NULL,
	[UrlTypeId] [int] NOT NULL,
	[Url] [nvarchar](255) NOT NULL,
	[EntityId] [int] NOT NULL,
	[EntityTypeId] [int] NOT NULL,
	[DateCreated] [datetime2](7) NOT NULL,
	[DateModified] [datetime2](7) NOT NULL,
 CONSTRAINT [PK_ExternalLinks] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[FAQs]    Script Date: 12/2/2022 3:13:23 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[FAQs](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Question] [nvarchar](255) NOT NULL,
	[Answer] [nvarchar](2000) NOT NULL,
	[CategoryId] [int] NOT NULL,
	[SortOrder] [int] NOT NULL,
	[DateCreated] [datetime2](7) NOT NULL,
	[DateModified] [datetime2](7) NOT NULL,
	[CreatedBy] [int] NOT NULL,
	[ModifiedBy] [int] NOT NULL,
 CONSTRAINT [PK_FAQs] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[FAQsCategories]    Script Date: 12/2/2022 3:13:23 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[FAQsCategories](
	[Id] [int] NOT NULL,
	[Name] [nvarchar](50) NOT NULL,
 CONSTRAINT [PK_FAQsCategories] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Files]    Script Date: 12/2/2022 3:13:23 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Files](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](80) NOT NULL,
	[Url] [nvarchar](255) NOT NULL,
	[FileTypeId] [int] NOT NULL,
	[CreatedBy] [int] NOT NULL,
	[DateCreated] [datetime2](7) NOT NULL,
	[IsDeleted] [bit] NOT NULL,
 CONSTRAINT [PK_Files] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[FileTypes]    Script Date: 12/2/2022 3:13:23 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[FileTypes](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](100) NOT NULL,
 CONSTRAINT [PK_FileTypes] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[FreelanceGoalTypes]    Script Date: 12/2/2022 3:13:23 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[FreelanceGoalTypes](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](30) NOT NULL,
 CONSTRAINT [PK_FreelanceGoalTypes] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Institution]    Script Date: 12/2/2022 3:13:23 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Institution](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](150) NOT NULL,
 CONSTRAINT [PK_Institution] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Jobs]    Script Date: 12/2/2022 3:13:23 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Jobs](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[JobTypeId] [int] NOT NULL,
	[LocationId] [int] NOT NULL,
	[OrganizationId] [int] NOT NULL,
	[Title] [nvarchar](200) NOT NULL,
	[Description] [nvarchar](4000) NOT NULL,
	[Requirements] [nvarchar](3000) NOT NULL,
	[IsActive] [bit] NOT NULL,
	[ContactName] [nvarchar](100) NOT NULL,
	[ContactPhone] [nvarchar](20) NULL,
	[ContactEmail] [nvarchar](200) NULL,
	[CreatedBy] [int] NOT NULL,
	[ModifiedBy] [int] NOT NULL,
	[DateCreated] [datetime2](7) NOT NULL,
	[DateModified] [datetime2](7) NOT NULL,
 CONSTRAINT [PK_Jobs] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[JobScheduleDaysOfWeek]    Script Date: 12/2/2022 3:13:23 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[JobScheduleDaysOfWeek](
	[JobScheduleId] [int] NOT NULL,
	[DayOfWeekId] [int] NOT NULL,
 CONSTRAINT [PK_JobScheduleDaysOfWeek] PRIMARY KEY CLUSTERED 
(
	[JobScheduleId] ASC,
	[DayOfWeekId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[JobSchedules]    Script Date: 12/2/2022 3:13:23 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[JobSchedules](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[ScheduleId] [int] NOT NULL,
	[StartDate] [datetime2](7) NOT NULL,
	[EndDate] [datetime2](7) NOT NULL,
	[DailyStartTime] [time](7) NULL,
	[DailyEndTime] [time](7) NULL,
	[DateCreated] [datetime2](7) NOT NULL,
	[DateModified] [datetime2](7) NOT NULL,
	[CreatedBy] [int] NOT NULL,
	[ModifiedBy] [int] NOT NULL,
	[JobId] [int] NOT NULL,
 CONSTRAINT [PK_JobSchedules] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[JobTypes]    Script Date: 12/2/2022 3:13:23 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[JobTypes](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](100) NOT NULL,
 CONSTRAINT [PK_JobType] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Languages]    Script Date: 12/2/2022 3:13:23 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Languages](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Code] [varchar](64) NOT NULL,
	[Name] [varchar](64) NOT NULL,
 CONSTRAINT [PK_Languages] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Licenses]    Script Date: 12/2/2022 3:13:23 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Licenses](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[LicenseStateId] [int] NOT NULL,
	[LicenseTypeId] [int] NOT NULL,
	[LicenseNumber] [nvarchar](50) NOT NULL,
	[DateExpires] [date] NULL,
	[CreatedBy] [int] NOT NULL,
	[DateCreated] [datetime2](7) NOT NULL,
 CONSTRAINT [PK_UserProfileLicenses] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[LicenseTypes]    Script Date: 12/2/2022 3:13:23 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[LicenseTypes](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](max) NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Locations]    Script Date: 12/2/2022 3:13:23 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Locations](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[LocationTypeId] [int] NOT NULL,
	[LineOne] [nvarchar](50) NOT NULL,
	[LineTwo] [nvarchar](50) NOT NULL,
	[City] [nvarchar](50) NOT NULL,
	[Zip] [nvarchar](50) NULL,
	[StateId] [int] NOT NULL,
	[Latitude] [float] NOT NULL,
	[Longitude] [float] NOT NULL,
	[DateCreated] [datetime2](7) NOT NULL,
	[DateModified] [datetime2](7) NOT NULL,
	[CreatedBy] [int] NOT NULL,
	[ModifiedBy] [int] NULL,
 CONSTRAINT [PK_Locations] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[LocationTypes]    Script Date: 12/2/2022 3:13:23 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[LocationTypes](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](50) NOT NULL,
 CONSTRAINT [PK_LocationTypes] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Messages]    Script Date: 12/2/2022 3:13:23 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Messages](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Message] [nvarchar](1000) NOT NULL,
	[Subject] [nvarchar](100) NULL,
	[RecipientId] [int] NOT NULL,
	[SenderId] [int] NOT NULL,
	[DateSent] [datetime2](7) NULL,
	[DateRead] [datetime2](7) NULL,
	[DateModified] [datetime2](7) NOT NULL,
	[DateCreated] [datetime2](7) NOT NULL,
 CONSTRAINT [PK_Messages] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[NewsletterContent]    Script Date: 12/2/2022 3:13:23 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[NewsletterContent](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[TemplateKeyId] [int] NOT NULL,
	[PageId] [int] NOT NULL,
	[Value] [nvarchar](max) NOT NULL,
	[DateCreated] [datetime2](7) NOT NULL,
	[DateModified] [datetime2](7) NOT NULL,
	[CreatedBy] [int] NOT NULL,
 CONSTRAINT [PK_Newsletter] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[NewsletterKeyTypes]    Script Date: 12/2/2022 3:13:23 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[NewsletterKeyTypes](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](50) NOT NULL,
 CONSTRAINT [PK_NewsletterKeyTypes] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Newsletters]    Script Date: 12/2/2022 3:13:23 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Newsletters](
	[Id] [int] NOT NULL,
	[NewsletterTemplateId] [int] NOT NULL,
	[CreatedBy] [int] NOT NULL,
	[IsPublished] [int] NOT NULL,
	[PublishDate] [datetime2](7) NOT NULL,
 CONSTRAINT [PK_Newsletters] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[NewsletterSubscriptions]    Script Date: 12/2/2022 3:13:23 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[NewsletterSubscriptions](
	[Email] [nvarchar](255) NOT NULL,
	[isSubscribed] [bit] NOT NULL,
	[DateCreated] [datetime2](7) NOT NULL,
	[DateModified] [datetime2](7) NOT NULL,
 CONSTRAINT [PK_NewsletterSubscriptions] PRIMARY KEY CLUSTERED 
(
	[Email] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[NewsletterTemplateKeys]    Script Date: 12/2/2022 3:13:23 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[NewsletterTemplateKeys](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[KeyTypeId] [int] NOT NULL,
	[TemplateId] [int] NOT NULL,
	[KeyName] [nvarchar](50) NOT NULL,
 CONSTRAINT [PK_NewsletterTemplateKeys] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[NewsletterTemplates]    Script Date: 12/2/2022 3:13:23 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[NewsletterTemplates](
	[Id] [int] NOT NULL,
	[Name] [nvarchar](100) NOT NULL,
	[Description] [nvarchar](275) NOT NULL,
	[ExampleUrl] [nvarchar](100) NOT NULL,
	[CreatedBy] [int] NOT NULL,
	[ModifiedBy] [int] NOT NULL,
 CONSTRAINT [PK_NewsletterTemplates] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[OrganizationFollowers]    Script Date: 12/2/2022 3:13:23 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[OrganizationFollowers](
	[OrganizationId] [int] NOT NULL,
	[FollowerId] [int] NOT NULL,
 CONSTRAINT [PK_OrganizationFollowers] PRIMARY KEY CLUSTERED 
(
	[OrganizationId] ASC,
	[FollowerId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Organizations]    Script Date: 12/2/2022 3:13:23 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Organizations](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[OrganizationTypeId] [int] NOT NULL,
	[Name] [nvarchar](200) NOT NULL,
	[Headline] [nvarchar](200) NULL,
	[Description] [nvarchar](max) NULL,
	[Logo] [nvarchar](255) NULL,
	[LocationId] [int] NOT NULL,
	[Phone] [nvarchar](50) NULL,
	[SiteUrl] [nvarchar](255) NULL,
	[DateCreated] [datetime2](7) NOT NULL,
	[DateModified] [datetime2](7) NOT NULL,
	[CreatedBy] [int] NOT NULL,
 CONSTRAINT [PK_Organizations] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[OrganizationTypes]    Script Date: 12/2/2022 3:13:23 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[OrganizationTypes](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](50) NULL,
 CONSTRAINT [PK_OrganizationTypes] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[PaymentAccounts]    Script Date: 12/2/2022 3:13:23 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[PaymentAccounts](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[VendorId] [int] NOT NULL,
	[AccountId] [nvarchar](250) NOT NULL,
	[PaymentTypeId] [int] NOT NULL,
	[DateCreated] [datetime2](7) NOT NULL,
	[CreatedBy] [int] NOT NULL,
	[DateModified] [datetime2](7) NULL,
	[ModifiedBy] [int] NULL,
 CONSTRAINT [PK_PaymentAccounts] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[PaymentTypes]    Script Date: 12/2/2022 3:13:23 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[PaymentTypes](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[PaymentType] [nvarchar](100) NOT NULL,
 CONSTRAINT [PK_PaymentTypes] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[ProfessionTypes]    Script Date: 12/2/2022 3:13:23 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ProfessionTypes](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](100) NOT NULL,
 CONSTRAINT [PK_ProfessionType] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Proposals]    Script Date: 12/2/2022 3:13:23 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Proposals](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[JobId] [int] NOT NULL,
	[Bid] [nvarchar](50) NOT NULL,
	[Duration] [nvarchar](50) NOT NULL,
	[Details] [nvarchar](4000) NOT NULL,
	[ProposalStatusId] [int] NOT NULL,
	[CreatedBy] [int] NOT NULL,
	[ModifiedBy] [int] NOT NULL,
	[DateCreated] [datetime2](7) NOT NULL,
	[DateModified] [datetime2](7) NOT NULL,
 CONSTRAINT [PK_Proposals] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[ProposalStatus]    Script Date: 12/2/2022 3:13:23 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ProposalStatus](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](30) NOT NULL,
 CONSTRAINT [PK_ProposalStatus] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[QuestionTypes]    Script Date: 12/2/2022 3:13:23 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[QuestionTypes](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](100) NOT NULL,
 CONSTRAINT [PK_QuestionTypes] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Ratings]    Script Date: 12/2/2022 3:13:23 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Ratings](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Rating] [tinyint] NOT NULL,
	[CommentId] [int] NULL,
	[EntityTypeId] [int] NOT NULL,
	[EntityId] [int] NOT NULL,
	[DateCreated] [datetime2](7) NOT NULL,
	[DateModified] [datetime2](7) NOT NULL,
	[CreatedBy] [int] NOT NULL,
	[IsDeleted] [bit] NOT NULL,
 CONSTRAINT [PK_Ratings] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[ReferenceTypes]    Script Date: 12/2/2022 3:13:23 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ReferenceTypes](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](30) NOT NULL,
 CONSTRAINT [PK_ReferenceTypes] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[ResourceCategories]    Script Date: 12/2/2022 3:13:23 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ResourceCategories](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Category] [nvarchar](120) NULL,
 CONSTRAINT [PK_ResourceCategories] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Resources]    Script Date: 12/2/2022 3:13:23 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Resources](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](200) NOT NULL,
	[Headline] [nvarchar](200) NULL,
	[Description] [nvarchar](1000) NULL,
	[Logo] [nvarchar](255) NULL,
	[ResourceCategoryId] [int] NOT NULL,
	[OrgId] [int] NOT NULL,
	[ContactName] [nvarchar](200) NULL,
	[ContactEmail] [nvarchar](255) NULL,
	[Phone] [nvarchar](50) NULL,
	[SiteUrl] [nvarchar](255) NULL,
	[DateCreated] [datetime2](7) NOT NULL,
	[DateModified] [datetime2](7) NOT NULL,
	[CreatedBy] [int] NOT NULL,
	[ModifiedBy] [int] NULL,
 CONSTRAINT [PK_Resources] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[ResumeContact]    Script Date: 12/2/2022 3:13:23 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ResumeContact](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](200) NOT NULL,
	[Email] [nvarchar](255) NOT NULL,
	[Phone] [nvarchar](20) NOT NULL,
	[Notes] [nvarchar](500) NOT NULL,
	[DateCreated] [datetime2](7) NOT NULL,
	[DateModified] [datetime2](7) NOT NULL,
	[CreatedBy] [int] NOT NULL,
	[ModifiedBy] [int] NOT NULL,
 CONSTRAINT [PK_ResumeContact] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[ResumeEducation]    Script Date: 12/2/2022 3:13:23 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ResumeEducation](
	[ResumeId] [int] NOT NULL,
	[EducationId] [int] NOT NULL
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[ResumeExperience]    Script Date: 12/2/2022 3:13:23 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ResumeExperience](
	[ResumeId] [int] NOT NULL,
	[ExperienceId] [int] NOT NULL
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[ResumeFreelanceGoalType]    Script Date: 12/2/2022 3:13:23 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ResumeFreelanceGoalType](
	[ResumeId] [int] NOT NULL,
	[FreelanceGoalTypeId] [int] NOT NULL
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Resumes]    Script Date: 12/2/2022 3:13:23 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Resumes](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Description] [nvarchar](200) NOT NULL,
	[ResumeContactId] [int] NOT NULL,
	[ResumeFileId] [int] NOT NULL,
	[CreatedBy] [int] NOT NULL,
	[DateCreated] [datetime2](7) NOT NULL,
	[DateModified] [datetime2](7) NOT NULL,
 CONSTRAINT [PK_Resumes] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[ResumeSkills]    Script Date: 12/2/2022 3:13:23 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ResumeSkills](
	[ResumeId] [int] NOT NULL,
	[SkillId] [int] NOT NULL
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[ResumeViews]    Script Date: 12/2/2022 3:13:23 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ResumeViews](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[ResumeId] [int] NOT NULL,
	[ViewerId] [int] NOT NULL,
	[DateCreated] [datetime2](7) NOT NULL,
 CONSTRAINT [PK_ResumeViews] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Roles]    Script Date: 12/2/2022 3:13:23 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Roles](
	[Id] [int] NOT NULL,
	[Name] [nvarchar](50) NOT NULL,
 CONSTRAINT [PK_Roles] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[ScheduleAvailability]    Script Date: 12/2/2022 3:13:23 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ScheduleAvailability](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[ScheduleId] [int] NOT NULL,
	[DayOfWeek] [int] NOT NULL,
	[StartDate] [datetime2](7) NOT NULL,
	[EndDate] [datetime2](7) NOT NULL,
	[DateCreated] [datetime2](7) NOT NULL,
	[DateModified] [datetime2](7) NOT NULL,
	[CreatedBy] [int] NOT NULL,
	[ModifiedBy] [int] NOT NULL,
 CONSTRAINT [PK_ScheduleAvailability] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Schedules]    Script Date: 12/2/2022 3:13:23 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Schedules](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[OrgId] [int] NOT NULL,
	[CreatedBy] [int] NOT NULL,
	[ModifiedBy] [int] NOT NULL,
	[DateCreated] [datetime2](7) NOT NULL,
	[DateModified] [datetime2](7) NOT NULL,
 CONSTRAINT [PK_Schedules] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[ShareStory]    Script Date: 12/2/2022 3:13:23 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ShareStory](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](50) NOT NULL,
	[Email] [nvarchar](50) NOT NULL,
	[Story] [nvarchar](3000) NOT NULL,
	[CreatedBy] [int] NOT NULL,
	[DateCreated] [datetime2](7) NOT NULL,
	[DateModified] [datetime2](7) NOT NULL,
 CONSTRAINT [PK_ShareStory] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[ShareStoryFile]    Script Date: 12/2/2022 3:13:23 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ShareStoryFile](
	[ShareStoryId] [int] NOT NULL,
	[FileId] [int] NOT NULL,
 CONSTRAINT [PK_ShareStoryFile] PRIMARY KEY CLUSTERED 
(
	[ShareStoryId] ASC,
	[FileId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[SiteReferences]    Script Date: 12/2/2022 3:13:23 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[SiteReferences](
	[ReferenceTypeId] [int] NOT NULL,
	[UserId] [int] NOT NULL,
 CONSTRAINT [PK_SiteReferences] PRIMARY KEY CLUSTERED 
(
	[ReferenceTypeId] ASC,
	[UserId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Skills]    Script Date: 12/2/2022 3:13:23 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Skills](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](32) NOT NULL,
 CONSTRAINT [PK_Skills] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[SpecializationTypes]    Script Date: 12/2/2022 3:13:23 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[SpecializationTypes](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](30) NOT NULL,
 CONSTRAINT [PK_SpecializationTypes] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[States]    Script Date: 12/2/2022 3:13:23 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[States](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Code] [nvarchar](50) NOT NULL,
	[Name] [nvarchar](50) NOT NULL,
 CONSTRAINT [PK_States] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[SubscriptionTransactions]    Script Date: 12/2/2022 3:13:23 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[SubscriptionTransactions](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[SubscriptionId] [int] NOT NULL,
	[PaymentAcctId] [int] NOT NULL,
	[UserId] [int] NOT NULL,
	[PurchaseAmount] [smallmoney] NULL,
	[PurchaseDate] [datetime2](7) NULL,
	[CancellationDate] [datetime2](7) NULL,
	[RefundedAmount] [smallmoney] NULL,
	[StripeInvoiceId] [nvarchar](100) NOT NULL,
	[StripeInvoiceUrl] [nvarchar](200) NOT NULL,
	[StripeSubscriptionId] [nvarchar](100) NOT NULL,
 CONSTRAINT [PK_SubscriptionTransactions] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[SubscriptionVendors]    Script Date: 12/2/2022 3:13:23 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[SubscriptionVendors](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[VendorType] [nvarchar](100) NOT NULL,
 CONSTRAINT [PK_SubscriptionVendors] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[SurveyAnswers]    Script Date: 12/2/2022 3:13:23 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[SurveyAnswers](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[InstanceId] [int] NOT NULL,
	[QuestionId] [int] NOT NULL,
	[AnswerOptionId] [int] NULL,
	[Answer] [nvarchar](500) NULL,
	[AnswerNumber] [int] NULL,
	[DateCreated] [datetime2](7) NOT NULL,
	[DateModified] [datetime2](7) NOT NULL,
 CONSTRAINT [PK_SurveyAnswers] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[SurveyInstances]    Script Date: 12/2/2022 3:13:23 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[SurveyInstances](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[SurveyId] [int] NOT NULL,
	[UserId] [int] NOT NULL,
	[DateCreated] [datetime2](7) NOT NULL,
	[DateModified] [datetime2](7) NOT NULL,
	[StatusId] [int] NOT NULL,
 CONSTRAINT [PK_SurveyInstances] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[SurveyQuestionAnswerOptions]    Script Date: 12/2/2022 3:13:23 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[SurveyQuestionAnswerOptions](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[QuestionId] [int] NOT NULL,
	[Text] [nvarchar](500) NULL,
	[Value] [nvarchar](100) NULL,
	[AdditionalInfo] [nvarchar](200) NULL,
	[CreatedBy] [int] NOT NULL,
	[SortOrder] [int] NULL,
 CONSTRAINT [PK_SurveyQuestionAnswerOptions] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[SurveyQuestions]    Script Date: 12/2/2022 3:13:23 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[SurveyQuestions](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[SurveyId] [int] NOT NULL,
	[UserId] [int] NOT NULL,
	[Question] [nvarchar](500) NOT NULL,
	[HelpText] [nvarchar](255) NULL,
	[IsRequired] [bit] NOT NULL,
	[isMultipleAllowed] [bit] NOT NULL,
	[QuestionTypeId] [int] NOT NULL,
	[StatusId] [int] NOT NULL,
	[SortOrder] [int] NOT NULL,
	[DateCreated] [datetime2](7) NOT NULL,
	[DateModified] [datetime2](7) NOT NULL,
 CONSTRAINT [PK_SurveyQuestions] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Surveys]    Script Date: 12/2/2022 3:13:23 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Surveys](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](100) NOT NULL,
	[Description] [nvarchar](2000) NOT NULL,
	[StatusId] [int] NOT NULL,
	[SurveyTypeId] [int] NOT NULL,
	[CreatedBy] [int] NOT NULL,
	[CompanyLogo] [nvarchar](255) NULL,
	[DateCreated] [datetime2](7) NOT NULL,
	[DateModified] [datetime2](7) NOT NULL,
 CONSTRAINT [PK_Surveys] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[SurveyStatus]    Script Date: 12/2/2022 3:13:23 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[SurveyStatus](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](100) NOT NULL,
 CONSTRAINT [PK_SurveyStatus] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[SurveyTypes]    Script Date: 12/2/2022 3:13:23 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[SurveyTypes](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](100) NOT NULL,
 CONSTRAINT [PK_SurveyTypes] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Timesheet]    Script Date: 12/2/2022 3:13:23 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Timesheet](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Date] [datetime2](7) NOT NULL,
	[TimeFrom] [nvarchar](10) NOT NULL,
	[TimeTo] [nvarchar](10) NOT NULL,
	[OrgId] [int] NOT NULL,
	[Comment] [nvarchar](800) NULL,
	[DateSubmmitted] [datetime2](7) NOT NULL,
	[CreatedBy] [int] NOT NULL,
	[ModifiedBy] [int] NOT NULL,
	[JobId] [int] NOT NULL,
	[TotalHrs] [int] NULL,
 CONSTRAINT [PK_Timesheet] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[TokenTypes]    Script Date: 12/2/2022 3:13:23 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[TokenTypes](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](100) NOT NULL,
 CONSTRAINT [PK_TokenTypes] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[UrlTypes]    Script Date: 12/2/2022 3:13:23 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[UrlTypes](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](100) NOT NULL,
 CONSTRAINT [PK_UrlTypes] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[UserJobs]    Script Date: 12/2/2022 3:13:23 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[UserJobs](
	[UserId] [int] NOT NULL,
	[JobId] [int] NOT NULL,
 CONSTRAINT [PK_UserJobs] PRIMARY KEY CLUSTERED 
(
	[UserId] ASC,
	[JobId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[UserLicense]    Script Date: 12/2/2022 3:13:23 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[UserLicense](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[LicenseTypesId] [int] NOT NULL,
	[UserId] [int] NULL,
	[LocationsId] [int] NOT NULL,
	[LicenseStateId] [int] NOT NULL,
	[Url] [nvarchar](max) NOT NULL,
	[DateExpires] [int] NOT NULL,
	[CreatedBy] [int] NULL,
	[ModifiedBy] [int] NULL,
	[DateCreated] [datetime2](7) NOT NULL,
	[DateModified] [datetime2](7) NOT NULL
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[UserProfessionTypes]    Script Date: 12/2/2022 3:13:23 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[UserProfessionTypes](
	[UserId] [int] NOT NULL,
	[ProfessionTypeId] [int] NOT NULL,
 CONSTRAINT [PK_UserProfessionTypes_1] PRIMARY KEY CLUSTERED 
(
	[UserId] ASC,
	[ProfessionTypeId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[UserProfiles]    Script Date: 12/2/2022 3:13:23 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[UserProfiles](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[UserId] [int] NOT NULL,
	[LocationId] [int] NOT NULL,
	[FirstName] [nvarchar](100) NULL,
	[LastName] [nvarchar](100) NULL,
	[Mi] [nvarchar](2) NULL,
	[AvatarUrl] [nvarchar](255) NULL,
	[DOB] [datetime2](7) NOT NULL,
	[Email] [nvarchar](100) NOT NULL,
	[Phone] [nvarchar](20) NULL,
	[YearsOfExperience] [nvarchar](10) NOT NULL,
	[DesiredHourlyRate] [nvarchar](10) NOT NULL,
	[IsActive] [bit] NOT NULL,
	[DateCreated] [datetime2](7) NOT NULL,
	[DateModified] [datetime2](7) NOT NULL,
 CONSTRAINT [PK_UserProfiles] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[UserRoles]    Script Date: 12/2/2022 3:13:23 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[UserRoles](
	[UserId] [int] NOT NULL,
	[RoleId] [int] NOT NULL,
 CONSTRAINT [PK_UserRoles] PRIMARY KEY CLUSTERED 
(
	[UserId] ASC,
	[RoleId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Users]    Script Date: 12/2/2022 3:13:23 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Users](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Email] [nvarchar](100) NOT NULL,
	[Password] [nvarchar](100) NOT NULL,
	[IsConfirmed] [bit] NOT NULL,
	[UserStatusId] [int] NOT NULL,
	[DateCreated] [datetime2](7) NOT NULL,
	[DateModified] [datetime2](7) NOT NULL,
 CONSTRAINT [PK_Users] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[UsersLicenses]    Script Date: 12/2/2022 3:13:23 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[UsersLicenses](
	[UserId] [int] NOT NULL,
	[LicenseId] [int] NOT NULL,
 CONSTRAINT [PK_UserProfileLicenses_1] PRIMARY KEY CLUSTERED 
(
	[UserId] ASC,
	[LicenseId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[UserStatus]    Script Date: 12/2/2022 3:13:23 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[UserStatus](
	[Id] [int] NOT NULL,
	[Name] [nvarchar](30) NOT NULL,
 CONSTRAINT [PK_UserStatus] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[UserSubscriptionStatus]    Script Date: 12/2/2022 3:13:23 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[UserSubscriptionStatus](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[SubscriptionId] [int] NOT NULL,
	[UserId] [int] NOT NULL,
	[SubscriptionStartDate] [datetime2](7) NULL,
	[SubscriptionEndDate] [datetime2](7) NULL,
	[isSubscribed] [bit] NOT NULL,
 CONSTRAINT [PK_UserSubscriptionStatus] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[UserTokens]    Script Date: 12/2/2022 3:13:23 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[UserTokens](
	[Token] [varchar](200) NOT NULL,
	[UserId] [int] NOT NULL,
	[TokenType] [int] NOT NULL,
 CONSTRAINT [PK_UserTokens] PRIMARY KEY CLUSTERED 
(
	[Token] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Index [IX_UserProfiles]    Script Date: 12/2/2022 3:13:23 PM ******/
CREATE UNIQUE NONCLUSTERED INDEX [IX_UserProfiles] ON [dbo].[UserProfiles]
(
	[UserId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
ALTER TABLE [dbo].[Blogs] ADD  CONSTRAINT [DF_Blogs_IsPublished]  DEFAULT ((0)) FOR [IsPublished]
GO
ALTER TABLE [dbo].[Blogs] ADD  CONSTRAINT [DF_Blogs_DateCreated]  DEFAULT (getutcdate()) FOR [DateCreated]
GO
ALTER TABLE [dbo].[Blogs] ADD  CONSTRAINT [DF_Blogs_DateModified]  DEFAULT (getutcdate()) FOR [DateModified]
GO
ALTER TABLE [dbo].[CnmProSubscriptions] ADD  CONSTRAINT [DF_CnmProSubscriptions_DateCreated]  DEFAULT (getutcdate()) FOR [DateCreated]
GO
ALTER TABLE [dbo].[Comments] ADD  CONSTRAINT [DF_Comments_DateCreated]  DEFAULT (getutcdate()) FOR [DateCreated]
GO
ALTER TABLE [dbo].[Comments] ADD  CONSTRAINT [DF_Comments_DateModified]  DEFAULT (getutcdate()) FOR [DateModified]
GO
ALTER TABLE [dbo].[DailyLinks] ADD  CONSTRAINT [DF_DailyLinks_DateCreated]  DEFAULT (getutcdate()) FOR [DateCreated]
GO
ALTER TABLE [dbo].[Education] ADD  CONSTRAINT [DF_Education_DateStart]  DEFAULT (getutcdate()) FOR [DateStart]
GO
ALTER TABLE [dbo].[Education] ADD  CONSTRAINT [DF_Education_DateEnd]  DEFAULT (getutcdate()) FOR [DateEnd]
GO
ALTER TABLE [dbo].[Education] ADD  CONSTRAINT [DF_Education_DateCreated]  DEFAULT (getutcdate()) FOR [DateCreated]
GO
ALTER TABLE [dbo].[Education] ADD  CONSTRAINT [DF_Education_DateModified]  DEFAULT (getutcdate()) FOR [DateModified]
GO
ALTER TABLE [dbo].[Events] ADD  CONSTRAINT [DF_Events_StartDate]  DEFAULT (getutcdate()) FOR [StartDate]
GO
ALTER TABLE [dbo].[Events] ADD  CONSTRAINT [DF_Events_EndDate]  DEFAULT (getutcdate()) FOR [EndDate]
GO
ALTER TABLE [dbo].[Events] ADD  CONSTRAINT [DF_Events_DateCreated]  DEFAULT (getutcdate()) FOR [DateCreated]
GO
ALTER TABLE [dbo].[Events] ADD  CONSTRAINT [DF_Events_DateModified]  DEFAULT (getutcdate()) FOR [DateModified]
GO
ALTER TABLE [dbo].[Experience] ADD  CONSTRAINT [DF_Experience_DateStart]  DEFAULT (getutcdate()) FOR [DateStart]
GO
ALTER TABLE [dbo].[Experience] ADD  CONSTRAINT [DF_Experience_DateEnd]  DEFAULT (getutcdate()) FOR [DateEnd]
GO
ALTER TABLE [dbo].[Experience] ADD  CONSTRAINT [DF_Experience_DateCreated]  DEFAULT (getutcdate()) FOR [DateCreated]
GO
ALTER TABLE [dbo].[Experience] ADD  CONSTRAINT [DF_Experience_DateModified]  DEFAULT (getutcdate()) FOR [DateModified]
GO
ALTER TABLE [dbo].[ExternalLinks] ADD  CONSTRAINT [DF_ExternalLinks_DateCreated]  DEFAULT (getutcdate()) FOR [DateCreated]
GO
ALTER TABLE [dbo].[ExternalLinks] ADD  CONSTRAINT [DF_ExternalLinks_DateModified]  DEFAULT (getutcdate()) FOR [DateModified]
GO
ALTER TABLE [dbo].[FAQs] ADD  CONSTRAINT [DF_FAQ_DateCreated]  DEFAULT (getutcdate()) FOR [DateCreated]
GO
ALTER TABLE [dbo].[FAQs] ADD  CONSTRAINT [DF_FAQ_DateModified]  DEFAULT (getutcdate()) FOR [DateModified]
GO
ALTER TABLE [dbo].[Files] ADD  CONSTRAINT [DF_Files_DateCreated]  DEFAULT (getutcdate()) FOR [DateCreated]
GO
ALTER TABLE [dbo].[Files] ADD  CONSTRAINT [DF_Files_IsDeleted]  DEFAULT ((0)) FOR [IsDeleted]
GO
ALTER TABLE [dbo].[Jobs] ADD  CONSTRAINT [DF_Jobs_DateCreated]  DEFAULT (getutcdate()) FOR [DateCreated]
GO
ALTER TABLE [dbo].[Jobs] ADD  CONSTRAINT [DF_Jobs_DateModified]  DEFAULT (getutcdate()) FOR [DateModified]
GO
ALTER TABLE [dbo].[JobSchedules] ADD  CONSTRAINT [DF_JobSchedules_DateCreated]  DEFAULT (getutcdate()) FOR [DateCreated]
GO
ALTER TABLE [dbo].[JobSchedules] ADD  CONSTRAINT [DF_JobSchedules_DateModified]  DEFAULT (getutcdate()) FOR [DateModified]
GO
ALTER TABLE [dbo].[Licenses] ADD  CONSTRAINT [DF_UserProfileLicenses_DateCreated]  DEFAULT (getutcdate()) FOR [DateCreated]
GO
ALTER TABLE [dbo].[Locations] ADD  CONSTRAINT [DF_Locations_DateCreated]  DEFAULT (getutcdate()) FOR [DateCreated]
GO
ALTER TABLE [dbo].[Locations] ADD  CONSTRAINT [DF_Locations_DateModified]  DEFAULT (getutcdate()) FOR [DateModified]
GO
ALTER TABLE [dbo].[Messages] ADD  CONSTRAINT [DF_Messages_DateModified]  DEFAULT (getutcdate()) FOR [DateModified]
GO
ALTER TABLE [dbo].[Messages] ADD  CONSTRAINT [DF_Messages_DateCreated]  DEFAULT (getutcdate()) FOR [DateCreated]
GO
ALTER TABLE [dbo].[NewsletterContent] ADD  CONSTRAINT [DF_Newsletter_DateCreated]  DEFAULT (getutcdate()) FOR [DateCreated]
GO
ALTER TABLE [dbo].[NewsletterContent] ADD  CONSTRAINT [DF_Newsletter_DateModified]  DEFAULT (getutcdate()) FOR [DateModified]
GO
ALTER TABLE [dbo].[NewsletterSubscriptions] ADD  CONSTRAINT [DF_NewsletterSubscriptions_DateCreated]  DEFAULT (getutcdate()) FOR [DateCreated]
GO
ALTER TABLE [dbo].[NewsletterSubscriptions] ADD  CONSTRAINT [DF_NewsletterSubscriptions_DateModified]  DEFAULT (getutcdate()) FOR [DateModified]
GO
ALTER TABLE [dbo].[Organizations] ADD  CONSTRAINT [DF_Organizations_DateCreated]  DEFAULT (getutcdate()) FOR [DateCreated]
GO
ALTER TABLE [dbo].[Organizations] ADD  CONSTRAINT [DF_Organizations_DateModified]  DEFAULT (getutcdate()) FOR [DateModified]
GO
ALTER TABLE [dbo].[PaymentAccounts] ADD  CONSTRAINT [DF_PaymentAccounts_DateCreated]  DEFAULT (getutcdate()) FOR [DateCreated]
GO
ALTER TABLE [dbo].[PaymentAccounts] ADD  CONSTRAINT [DF_PaymentAccounts_DateModified]  DEFAULT (getutcdate()) FOR [DateModified]
GO
ALTER TABLE [dbo].[Proposals] ADD  CONSTRAINT [DF_Proposals_DateCreated]  DEFAULT (getutcdate()) FOR [DateCreated]
GO
ALTER TABLE [dbo].[Proposals] ADD  CONSTRAINT [DF_Proposals_DateModified]  DEFAULT (getutcdate()) FOR [DateModified]
GO
ALTER TABLE [dbo].[Ratings] ADD  CONSTRAINT [DF_Ratings_DateCreated]  DEFAULT (getutcdate()) FOR [DateCreated]
GO
ALTER TABLE [dbo].[Ratings] ADD  CONSTRAINT [DF_Ratings_DateModified]  DEFAULT (getutcdate()) FOR [DateModified]
GO
ALTER TABLE [dbo].[Resources] ADD  CONSTRAINT [DF_Resources_DateCreated]  DEFAULT (getutcdate()) FOR [DateCreated]
GO
ALTER TABLE [dbo].[Resources] ADD  CONSTRAINT [DF_Resources_DateModified]  DEFAULT (getutcdate()) FOR [DateModified]
GO
ALTER TABLE [dbo].[ResumeContact] ADD  CONSTRAINT [DF_ResumeContact_DateCreated]  DEFAULT (getutcdate()) FOR [DateCreated]
GO
ALTER TABLE [dbo].[ResumeContact] ADD  CONSTRAINT [DF_ResumeContact_DateModified]  DEFAULT (getutcdate()) FOR [DateModified]
GO
ALTER TABLE [dbo].[Resumes] ADD  CONSTRAINT [DF_Resumes_DateCreated]  DEFAULT (getutcdate()) FOR [DateCreated]
GO
ALTER TABLE [dbo].[Resumes] ADD  CONSTRAINT [DF_Resumes_DateModified]  DEFAULT (getutcdate()) FOR [DateModified]
GO
ALTER TABLE [dbo].[ResumeViews] ADD  CONSTRAINT [DF_ResumeViews_DateCreated]  DEFAULT (getutcdate()) FOR [DateCreated]
GO
ALTER TABLE [dbo].[ScheduleAvailability] ADD  CONSTRAINT [DF_ScheduleAvailability_DateCreated]  DEFAULT (getutcdate()) FOR [DateCreated]
GO
ALTER TABLE [dbo].[ScheduleAvailability] ADD  CONSTRAINT [DF_ScheduleAvailability_DateModified]  DEFAULT (getutcdate()) FOR [DateModified]
GO
ALTER TABLE [dbo].[Schedules] ADD  CONSTRAINT [DF_Schedules_DateCreated]  DEFAULT (getutcdate()) FOR [DateCreated]
GO
ALTER TABLE [dbo].[Schedules] ADD  CONSTRAINT [DF_Schedules_DateModified]  DEFAULT (getutcdate()) FOR [DateModified]
GO
ALTER TABLE [dbo].[ShareStory] ADD  CONSTRAINT [DF_ShareStory_DateCreated]  DEFAULT (getutcdate()) FOR [DateCreated]
GO
ALTER TABLE [dbo].[ShareStory] ADD  CONSTRAINT [DF_ShareStory_DateModified]  DEFAULT (getutcdate()) FOR [DateModified]
GO
ALTER TABLE [dbo].[SurveyAnswers] ADD  CONSTRAINT [DF_SurveyAnswers_DateCreated]  DEFAULT (getutcdate()) FOR [DateCreated]
GO
ALTER TABLE [dbo].[SurveyAnswers] ADD  CONSTRAINT [DF_SurveyAnswers_DateModified]  DEFAULT (getutcdate()) FOR [DateModified]
GO
ALTER TABLE [dbo].[SurveyInstances] ADD  CONSTRAINT [DF_SurveyInstances_DateCreated]  DEFAULT (getutcdate()) FOR [DateCreated]
GO
ALTER TABLE [dbo].[SurveyInstances] ADD  CONSTRAINT [DF_SurveyInstances_DateModified]  DEFAULT (getutcdate()) FOR [DateModified]
GO
ALTER TABLE [dbo].[SurveyQuestions] ADD  CONSTRAINT [DF_SurveyQuestions_IsRequired]  DEFAULT ((1)) FOR [IsRequired]
GO
ALTER TABLE [dbo].[SurveyQuestions] ADD  CONSTRAINT [DF_SurveyQuestions_isMultipleAllowed]  DEFAULT ((0)) FOR [isMultipleAllowed]
GO
ALTER TABLE [dbo].[SurveyQuestions] ADD  CONSTRAINT [DF_SurveyQuestions_StatusId]  DEFAULT ((1)) FOR [StatusId]
GO
ALTER TABLE [dbo].[SurveyQuestions] ADD  CONSTRAINT [DF_SurveyQuestions_DateCreated]  DEFAULT (getutcdate()) FOR [DateCreated]
GO
ALTER TABLE [dbo].[SurveyQuestions] ADD  CONSTRAINT [DF_SurveyQuestions_DateModified]  DEFAULT (getutcdate()) FOR [DateModified]
GO
ALTER TABLE [dbo].[Surveys] ADD  CONSTRAINT [DF_Surveys_DateCreated]  DEFAULT (getutcdate()) FOR [DateCreated]
GO
ALTER TABLE [dbo].[Surveys] ADD  CONSTRAINT [DF_Surveys_DateModified]  DEFAULT (getutcdate()) FOR [DateModified]
GO
ALTER TABLE [dbo].[Timesheet] ADD  CONSTRAINT [DF_Table_1_DateSunmmitted]  DEFAULT (getutcdate()) FOR [DateSubmmitted]
GO
ALTER TABLE [dbo].[UserLicense] ADD  CONSTRAINT [DF_UserLicense_DateCreated]  DEFAULT (getutcdate()) FOR [DateCreated]
GO
ALTER TABLE [dbo].[UserLicense] ADD  CONSTRAINT [DF_UserLicense_DateModified]  DEFAULT (getutcdate()) FOR [DateModified]
GO
ALTER TABLE [dbo].[UserProfiles] ADD  CONSTRAINT [DF_UserProfiles_DateCreated]  DEFAULT (getutcdate()) FOR [DateCreated]
GO
ALTER TABLE [dbo].[UserProfiles] ADD  CONSTRAINT [DF_UserProfiles_DateModified]  DEFAULT (getutcdate()) FOR [DateModified]
GO
ALTER TABLE [dbo].[Users] ADD  CONSTRAINT [DF_Users_IsConfirmed]  DEFAULT ((0)) FOR [IsConfirmed]
GO
ALTER TABLE [dbo].[Users] ADD  CONSTRAINT [DF_Users_DateCreated]  DEFAULT (getutcdate()) FOR [DateCreated]
GO
ALTER TABLE [dbo].[Users] ADD  CONSTRAINT [DF_Users_DateModified]  DEFAULT (getutcdate()) FOR [DateModified]
GO
ALTER TABLE [dbo].[Blogs]  WITH CHECK ADD  CONSTRAINT [FK_Blogs_BlogTypes] FOREIGN KEY([BlogTypeId])
REFERENCES [dbo].[BlogTypes] ([Id])
GO
ALTER TABLE [dbo].[Blogs] CHECK CONSTRAINT [FK_Blogs_BlogTypes]
GO
ALTER TABLE [dbo].[Comments]  WITH CHECK ADD  CONSTRAINT [FK_Comments_EntityTypes1] FOREIGN KEY([EntityTypeId])
REFERENCES [dbo].[EntityTypes] ([Id])
GO
ALTER TABLE [dbo].[Comments] CHECK CONSTRAINT [FK_Comments_EntityTypes1]
GO
ALTER TABLE [dbo].[Comments]  WITH CHECK ADD  CONSTRAINT [FK_Comments_Users] FOREIGN KEY([CreatedBy])
REFERENCES [dbo].[Users] ([Id])
GO
ALTER TABLE [dbo].[Comments] CHECK CONSTRAINT [FK_Comments_Users]
GO
ALTER TABLE [dbo].[DailyUser]  WITH CHECK ADD  CONSTRAINT [FK_DailyUser_DailyLinks] FOREIGN KEY([DailyLinkId])
REFERENCES [dbo].[DailyLinks] ([Id])
GO
ALTER TABLE [dbo].[DailyUser] CHECK CONSTRAINT [FK_DailyUser_DailyLinks]
GO
ALTER TABLE [dbo].[DailyUser]  WITH CHECK ADD  CONSTRAINT [FK_DailyUser_Users] FOREIGN KEY([RecipientId])
REFERENCES [dbo].[Users] ([Id])
GO
ALTER TABLE [dbo].[DailyUser] CHECK CONSTRAINT [FK_DailyUser_Users]
GO
ALTER TABLE [dbo].[Education]  WITH CHECK ADD  CONSTRAINT [FK_Education_EdProgramTypeId_on_EdProgramTypes_Id] FOREIGN KEY([EdProgramTypeId])
REFERENCES [dbo].[EdProgramTypes] ([Id])
GO
ALTER TABLE [dbo].[Education] CHECK CONSTRAINT [FK_Education_EdProgramTypeId_on_EdProgramTypes_Id]
GO
ALTER TABLE [dbo].[Education]  WITH CHECK ADD  CONSTRAINT [FK_Education_SpecializationTypeId_on_SpecializationTypes_Id] FOREIGN KEY([SpecializationTypeId])
REFERENCES [dbo].[SpecializationTypes] ([Id])
GO
ALTER TABLE [dbo].[Education] CHECK CONSTRAINT [FK_Education_SpecializationTypeId_on_SpecializationTypes_Id]
GO
ALTER TABLE [dbo].[Events]  WITH CHECK ADD  CONSTRAINT [FK_Events_CreatedBy_on_Users_id] FOREIGN KEY([CreatedBy])
REFERENCES [dbo].[Users] ([Id])
GO
ALTER TABLE [dbo].[Events] CHECK CONSTRAINT [FK_Events_CreatedBy_on_Users_id]
GO
ALTER TABLE [dbo].[Events]  WITH CHECK ADD  CONSTRAINT [FK_Events_EventTypes] FOREIGN KEY([EventTypeId])
REFERENCES [dbo].[EventTypes] ([Id])
GO
ALTER TABLE [dbo].[Events] CHECK CONSTRAINT [FK_Events_EventTypes]
GO
ALTER TABLE [dbo].[Events]  WITH CHECK ADD  CONSTRAINT [FK_Events_ModifiedBy_on_Users_Id] FOREIGN KEY([ModifiedBy])
REFERENCES [dbo].[Users] ([Id])
GO
ALTER TABLE [dbo].[Events] CHECK CONSTRAINT [FK_Events_ModifiedBy_on_Users_Id]
GO
ALTER TABLE [dbo].[Events]  WITH CHECK ADD  CONSTRAINT [FK_Events_Organizations] FOREIGN KEY([OrganizationId])
REFERENCES [dbo].[Organizations] ([Id])
GO
ALTER TABLE [dbo].[Events] CHECK CONSTRAINT [FK_Events_Organizations]
GO
ALTER TABLE [dbo].[ExternalLinks]  WITH CHECK ADD  CONSTRAINT [FK_ExternalLinks_EntityTypes] FOREIGN KEY([EntityTypeId])
REFERENCES [dbo].[EntityTypes] ([Id])
GO
ALTER TABLE [dbo].[ExternalLinks] CHECK CONSTRAINT [FK_ExternalLinks_EntityTypes]
GO
ALTER TABLE [dbo].[ExternalLinks]  WITH CHECK ADD  CONSTRAINT [FK_ExternalLinks_UrlTypes] FOREIGN KEY([UrlTypeId])
REFERENCES [dbo].[UrlTypes] ([Id])
GO
ALTER TABLE [dbo].[ExternalLinks] CHECK CONSTRAINT [FK_ExternalLinks_UrlTypes]
GO
ALTER TABLE [dbo].[ExternalLinks]  WITH CHECK ADD  CONSTRAINT [FK_ExternalLinks_Users] FOREIGN KEY([UserId])
REFERENCES [dbo].[Users] ([Id])
GO
ALTER TABLE [dbo].[ExternalLinks] CHECK CONSTRAINT [FK_ExternalLinks_Users]
GO
ALTER TABLE [dbo].[FAQs]  WITH CHECK ADD  CONSTRAINT [FK_FAQs_FAQsCategories] FOREIGN KEY([CategoryId])
REFERENCES [dbo].[FAQsCategories] ([Id])
GO
ALTER TABLE [dbo].[FAQs] CHECK CONSTRAINT [FK_FAQs_FAQsCategories]
GO
ALTER TABLE [dbo].[FAQs]  WITH CHECK ADD  CONSTRAINT [FK_FAQs_Users] FOREIGN KEY([CreatedBy])
REFERENCES [dbo].[Users] ([Id])
GO
ALTER TABLE [dbo].[FAQs] CHECK CONSTRAINT [FK_FAQs_Users]
GO
ALTER TABLE [dbo].[FAQs]  WITH CHECK ADD  CONSTRAINT [FK_FAQs_Users1] FOREIGN KEY([ModifiedBy])
REFERENCES [dbo].[Users] ([Id])
GO
ALTER TABLE [dbo].[FAQs] CHECK CONSTRAINT [FK_FAQs_Users1]
GO
ALTER TABLE [dbo].[Files]  WITH CHECK ADD  CONSTRAINT [FK_Files_FileTypeId_on_FileTypes_Id] FOREIGN KEY([FileTypeId])
REFERENCES [dbo].[FileTypes] ([Id])
GO
ALTER TABLE [dbo].[Files] CHECK CONSTRAINT [FK_Files_FileTypeId_on_FileTypes_Id]
GO
ALTER TABLE [dbo].[Jobs]  WITH CHECK ADD  CONSTRAINT [FK_Jobs_CreatedBy_on_Users_Id] FOREIGN KEY([CreatedBy])
REFERENCES [dbo].[Users] ([Id])
GO
ALTER TABLE [dbo].[Jobs] CHECK CONSTRAINT [FK_Jobs_CreatedBy_on_Users_Id]
GO
ALTER TABLE [dbo].[Jobs]  WITH CHECK ADD  CONSTRAINT [FK_Jobs_JobTypes] FOREIGN KEY([JobTypeId])
REFERENCES [dbo].[JobTypes] ([Id])
GO
ALTER TABLE [dbo].[Jobs] CHECK CONSTRAINT [FK_Jobs_JobTypes]
GO
ALTER TABLE [dbo].[Jobs]  WITH CHECK ADD  CONSTRAINT [FK_Jobs_ModifiedBy_Users_Id] FOREIGN KEY([ModifiedBy])
REFERENCES [dbo].[Users] ([Id])
GO
ALTER TABLE [dbo].[Jobs] CHECK CONSTRAINT [FK_Jobs_ModifiedBy_Users_Id]
GO
ALTER TABLE [dbo].[Jobs]  WITH CHECK ADD  CONSTRAINT [FK_Jobs_Organizations] FOREIGN KEY([OrganizationId])
REFERENCES [dbo].[Organizations] ([Id])
GO
ALTER TABLE [dbo].[Jobs] CHECK CONSTRAINT [FK_Jobs_Organizations]
GO
ALTER TABLE [dbo].[JobScheduleDaysOfWeek]  WITH CHECK ADD  CONSTRAINT [FK_JobScheduleDaysOfWeek_DayOfWeekId_on_DaysOfWeeks_Id] FOREIGN KEY([DayOfWeekId])
REFERENCES [dbo].[DaysOfWeeks] ([Id])
GO
ALTER TABLE [dbo].[JobScheduleDaysOfWeek] CHECK CONSTRAINT [FK_JobScheduleDaysOfWeek_DayOfWeekId_on_DaysOfWeeks_Id]
GO
ALTER TABLE [dbo].[JobScheduleDaysOfWeek]  WITH CHECK ADD  CONSTRAINT [FK_JobScheduleDaysOfWeek_JobScheduleId_on_JobSchedules_Id] FOREIGN KEY([JobScheduleId])
REFERENCES [dbo].[JobSchedules] ([Id])
GO
ALTER TABLE [dbo].[JobScheduleDaysOfWeek] CHECK CONSTRAINT [FK_JobScheduleDaysOfWeek_JobScheduleId_on_JobSchedules_Id]
GO
ALTER TABLE [dbo].[JobSchedules]  WITH CHECK ADD  CONSTRAINT [FK_JobSchedules_Jobs] FOREIGN KEY([JobId])
REFERENCES [dbo].[Jobs] ([Id])
GO
ALTER TABLE [dbo].[JobSchedules] CHECK CONSTRAINT [FK_JobSchedules_Jobs]
GO
ALTER TABLE [dbo].[JobSchedules]  WITH CHECK ADD  CONSTRAINT [FK_JobSchedules_Schedules] FOREIGN KEY([ScheduleId])
REFERENCES [dbo].[Schedules] ([Id])
GO
ALTER TABLE [dbo].[JobSchedules] CHECK CONSTRAINT [FK_JobSchedules_Schedules]
GO
ALTER TABLE [dbo].[JobSchedules]  WITH CHECK ADD  CONSTRAINT [FK_JobSchedules_Users] FOREIGN KEY([CreatedBy])
REFERENCES [dbo].[Users] ([Id])
GO
ALTER TABLE [dbo].[JobSchedules] CHECK CONSTRAINT [FK_JobSchedules_Users]
GO
ALTER TABLE [dbo].[JobSchedules]  WITH CHECK ADD  CONSTRAINT [FK_JobSchedules_Users1] FOREIGN KEY([ModifiedBy])
REFERENCES [dbo].[Users] ([Id])
GO
ALTER TABLE [dbo].[JobSchedules] CHECK CONSTRAINT [FK_JobSchedules_Users1]
GO
ALTER TABLE [dbo].[Licenses]  WITH CHECK ADD  CONSTRAINT [FK_UserProfileLicenses_LicenseTypes] FOREIGN KEY([LicenseTypeId])
REFERENCES [dbo].[LicenseTypes] ([Id])
GO
ALTER TABLE [dbo].[Licenses] CHECK CONSTRAINT [FK_UserProfileLicenses_LicenseTypes]
GO
ALTER TABLE [dbo].[Licenses]  WITH CHECK ADD  CONSTRAINT [FK_UserProfileLicenses_States] FOREIGN KEY([LicenseStateId])
REFERENCES [dbo].[States] ([Id])
GO
ALTER TABLE [dbo].[Licenses] CHECK CONSTRAINT [FK_UserProfileLicenses_States]
GO
ALTER TABLE [dbo].[Licenses]  WITH CHECK ADD  CONSTRAINT [FK_UserProfileLicenses_Users] FOREIGN KEY([CreatedBy])
REFERENCES [dbo].[Users] ([Id])
GO
ALTER TABLE [dbo].[Licenses] CHECK CONSTRAINT [FK_UserProfileLicenses_Users]
GO
ALTER TABLE [dbo].[Licenses]  WITH CHECK ADD  CONSTRAINT [FK_UserProfileLicenses_Users1] FOREIGN KEY([CreatedBy])
REFERENCES [dbo].[Users] ([Id])
GO
ALTER TABLE [dbo].[Licenses] CHECK CONSTRAINT [FK_UserProfileLicenses_Users1]
GO
ALTER TABLE [dbo].[Locations]  WITH CHECK ADD  CONSTRAINT [FK_Locations_LocationTypes] FOREIGN KEY([LocationTypeId])
REFERENCES [dbo].[LocationTypes] ([Id])
GO
ALTER TABLE [dbo].[Locations] CHECK CONSTRAINT [FK_Locations_LocationTypes]
GO
ALTER TABLE [dbo].[Locations]  WITH CHECK ADD  CONSTRAINT [FK_Locations_States] FOREIGN KEY([StateId])
REFERENCES [dbo].[States] ([Id])
GO
ALTER TABLE [dbo].[Locations] CHECK CONSTRAINT [FK_Locations_States]
GO
ALTER TABLE [dbo].[Newsletters]  WITH CHECK ADD  CONSTRAINT [FK_Newsletters_NewsletterTemplates] FOREIGN KEY([NewsletterTemplateId])
REFERENCES [dbo].[NewsletterTemplates] ([Id])
GO
ALTER TABLE [dbo].[Newsletters] CHECK CONSTRAINT [FK_Newsletters_NewsletterTemplates]
GO
ALTER TABLE [dbo].[NewsletterTemplateKeys]  WITH CHECK ADD  CONSTRAINT [FK_NewsletterTemplateKeys_NewsletterKeyTypes] FOREIGN KEY([KeyTypeId])
REFERENCES [dbo].[NewsletterKeyTypes] ([Id])
GO
ALTER TABLE [dbo].[NewsletterTemplateKeys] CHECK CONSTRAINT [FK_NewsletterTemplateKeys_NewsletterKeyTypes]
GO
ALTER TABLE [dbo].[NewsletterTemplateKeys]  WITH CHECK ADD  CONSTRAINT [FK_NewsletterTemplateKeys_NewsletterTemplates] FOREIGN KEY([TemplateId])
REFERENCES [dbo].[NewsletterTemplates] ([Id])
GO
ALTER TABLE [dbo].[NewsletterTemplateKeys] CHECK CONSTRAINT [FK_NewsletterTemplateKeys_NewsletterTemplates]
GO
ALTER TABLE [dbo].[OrganizationFollowers]  WITH CHECK ADD  CONSTRAINT [FK_OrganizationFollowers_Organizations] FOREIGN KEY([OrganizationId])
REFERENCES [dbo].[Organizations] ([Id])
GO
ALTER TABLE [dbo].[OrganizationFollowers] CHECK CONSTRAINT [FK_OrganizationFollowers_Organizations]
GO
ALTER TABLE [dbo].[OrganizationFollowers]  WITH CHECK ADD  CONSTRAINT [FK_OrganizationFollowers_Users] FOREIGN KEY([FollowerId])
REFERENCES [dbo].[Users] ([Id])
GO
ALTER TABLE [dbo].[OrganizationFollowers] CHECK CONSTRAINT [FK_OrganizationFollowers_Users]
GO
ALTER TABLE [dbo].[Organizations]  WITH CHECK ADD  CONSTRAINT [FK_Organizations_OrganizationTypes1] FOREIGN KEY([OrganizationTypeId])
REFERENCES [dbo].[OrganizationTypes] ([Id])
GO
ALTER TABLE [dbo].[Organizations] CHECK CONSTRAINT [FK_Organizations_OrganizationTypes1]
GO
ALTER TABLE [dbo].[PaymentAccounts]  WITH CHECK ADD  CONSTRAINT [FK_PaymentAccounts_PaymentTypes] FOREIGN KEY([PaymentTypeId])
REFERENCES [dbo].[PaymentTypes] ([Id])
GO
ALTER TABLE [dbo].[PaymentAccounts] CHECK CONSTRAINT [FK_PaymentAccounts_PaymentTypes]
GO
ALTER TABLE [dbo].[PaymentAccounts]  WITH CHECK ADD  CONSTRAINT [FK_PaymentAccounts_SubscriptionVendors] FOREIGN KEY([VendorId])
REFERENCES [dbo].[SubscriptionVendors] ([Id])
GO
ALTER TABLE [dbo].[PaymentAccounts] CHECK CONSTRAINT [FK_PaymentAccounts_SubscriptionVendors]
GO
ALTER TABLE [dbo].[Proposals]  WITH CHECK ADD  CONSTRAINT [FK_Proposals_Jobs] FOREIGN KEY([JobId])
REFERENCES [dbo].[Jobs] ([Id])
GO
ALTER TABLE [dbo].[Proposals] CHECK CONSTRAINT [FK_Proposals_Jobs]
GO
ALTER TABLE [dbo].[Proposals]  WITH CHECK ADD  CONSTRAINT [FK_Proposals_ProposalStatus] FOREIGN KEY([ProposalStatusId])
REFERENCES [dbo].[ProposalStatus] ([Id])
GO
ALTER TABLE [dbo].[Proposals] CHECK CONSTRAINT [FK_Proposals_ProposalStatus]
GO
ALTER TABLE [dbo].[Proposals]  WITH CHECK ADD  CONSTRAINT [FK_Proposals_Users] FOREIGN KEY([CreatedBy])
REFERENCES [dbo].[Users] ([Id])
GO
ALTER TABLE [dbo].[Proposals] CHECK CONSTRAINT [FK_Proposals_Users]
GO
ALTER TABLE [dbo].[Proposals]  WITH CHECK ADD  CONSTRAINT [FK_Proposals_Users1] FOREIGN KEY([ModifiedBy])
REFERENCES [dbo].[Users] ([Id])
GO
ALTER TABLE [dbo].[Proposals] CHECK CONSTRAINT [FK_Proposals_Users1]
GO
ALTER TABLE [dbo].[Ratings]  WITH CHECK ADD  CONSTRAINT [FK_Ratings_Comments] FOREIGN KEY([CommentId])
REFERENCES [dbo].[Comments] ([Id])
GO
ALTER TABLE [dbo].[Ratings] CHECK CONSTRAINT [FK_Ratings_Comments]
GO
ALTER TABLE [dbo].[Ratings]  WITH CHECK ADD  CONSTRAINT [FK_Ratings_CreatedBy1] FOREIGN KEY([CreatedBy])
REFERENCES [dbo].[Users] ([Id])
GO
ALTER TABLE [dbo].[Ratings] CHECK CONSTRAINT [FK_Ratings_CreatedBy1]
GO
ALTER TABLE [dbo].[Ratings]  WITH CHECK ADD  CONSTRAINT [FK_Ratings_EntityTypes1] FOREIGN KEY([EntityTypeId])
REFERENCES [dbo].[EntityTypes] ([Id])
GO
ALTER TABLE [dbo].[Ratings] CHECK CONSTRAINT [FK_Ratings_EntityTypes1]
GO
ALTER TABLE [dbo].[Resources]  WITH CHECK ADD  CONSTRAINT [FK_Resources_Organizations] FOREIGN KEY([OrgId])
REFERENCES [dbo].[Organizations] ([Id])
GO
ALTER TABLE [dbo].[Resources] CHECK CONSTRAINT [FK_Resources_Organizations]
GO
ALTER TABLE [dbo].[Resources]  WITH CHECK ADD  CONSTRAINT [FK_Resources_ResourceCategories] FOREIGN KEY([ResourceCategoryId])
REFERENCES [dbo].[ResourceCategories] ([Id])
GO
ALTER TABLE [dbo].[Resources] CHECK CONSTRAINT [FK_Resources_ResourceCategories]
GO
ALTER TABLE [dbo].[ResumeContact]  WITH CHECK ADD  CONSTRAINT [FK_ResumeContact_CreatedBy_on_Users_Id] FOREIGN KEY([CreatedBy])
REFERENCES [dbo].[Users] ([Id])
GO
ALTER TABLE [dbo].[ResumeContact] CHECK CONSTRAINT [FK_ResumeContact_CreatedBy_on_Users_Id]
GO
ALTER TABLE [dbo].[ResumeContact]  WITH CHECK ADD  CONSTRAINT [FK_ResumeContact_ModifiedBy_on_Users_Id] FOREIGN KEY([ModifiedBy])
REFERENCES [dbo].[Users] ([Id])
GO
ALTER TABLE [dbo].[ResumeContact] CHECK CONSTRAINT [FK_ResumeContact_ModifiedBy_on_Users_Id]
GO
ALTER TABLE [dbo].[ResumeContact]  WITH CHECK ADD  CONSTRAINT [FK_ResumeContact_Users] FOREIGN KEY([CreatedBy])
REFERENCES [dbo].[Users] ([Id])
GO
ALTER TABLE [dbo].[ResumeContact] CHECK CONSTRAINT [FK_ResumeContact_Users]
GO
ALTER TABLE [dbo].[ResumeEducation]  WITH CHECK ADD  CONSTRAINT [FK_ResumeEducation_EducationId_on_Education_Id] FOREIGN KEY([EducationId])
REFERENCES [dbo].[Education] ([Id])
GO
ALTER TABLE [dbo].[ResumeEducation] CHECK CONSTRAINT [FK_ResumeEducation_EducationId_on_Education_Id]
GO
ALTER TABLE [dbo].[ResumeExperience]  WITH CHECK ADD  CONSTRAINT [FK_ResumeExperience_ExperienceId_on_Experience_Id] FOREIGN KEY([ExperienceId])
REFERENCES [dbo].[Experience] ([Id])
GO
ALTER TABLE [dbo].[ResumeExperience] CHECK CONSTRAINT [FK_ResumeExperience_ExperienceId_on_Experience_Id]
GO
ALTER TABLE [dbo].[ResumeFreelanceGoalType]  WITH CHECK ADD  CONSTRAINT [FK_ResumeFreelanceGoalType_FreelanceGoalTypeId_on_FreelanceGoalTypes_Id] FOREIGN KEY([FreelanceGoalTypeId])
REFERENCES [dbo].[FreelanceGoalTypes] ([Id])
GO
ALTER TABLE [dbo].[ResumeFreelanceGoalType] CHECK CONSTRAINT [FK_ResumeFreelanceGoalType_FreelanceGoalTypeId_on_FreelanceGoalTypes_Id]
GO
ALTER TABLE [dbo].[Resumes]  WITH CHECK ADD  CONSTRAINT [FK_Resumes_CreatedBy_on_Users_Id] FOREIGN KEY([CreatedBy])
REFERENCES [dbo].[Users] ([Id])
GO
ALTER TABLE [dbo].[Resumes] CHECK CONSTRAINT [FK_Resumes_CreatedBy_on_Users_Id]
GO
ALTER TABLE [dbo].[Resumes]  WITH CHECK ADD  CONSTRAINT [FK_Resumes_ResumeContact] FOREIGN KEY([ResumeContactId])
REFERENCES [dbo].[ResumeContact] ([Id])
GO
ALTER TABLE [dbo].[Resumes] CHECK CONSTRAINT [FK_Resumes_ResumeContact]
GO
ALTER TABLE [dbo].[Resumes]  WITH CHECK ADD  CONSTRAINT [FK_Resumes_ResumeFileId_on_Files_Id] FOREIGN KEY([ResumeFileId])
REFERENCES [dbo].[Files] ([Id])
GO
ALTER TABLE [dbo].[Resumes] CHECK CONSTRAINT [FK_Resumes_ResumeFileId_on_Files_Id]
GO
ALTER TABLE [dbo].[ResumeSkills]  WITH CHECK ADD  CONSTRAINT [FK_ResumeSkills_Skills] FOREIGN KEY([SkillId])
REFERENCES [dbo].[Skills] ([Id])
GO
ALTER TABLE [dbo].[ResumeSkills] CHECK CONSTRAINT [FK_ResumeSkills_Skills]
GO
ALTER TABLE [dbo].[ResumeViews]  WITH CHECK ADD  CONSTRAINT [FK_ResumeViews_Resumes] FOREIGN KEY([ResumeId])
REFERENCES [dbo].[Resumes] ([Id])
GO
ALTER TABLE [dbo].[ResumeViews] CHECK CONSTRAINT [FK_ResumeViews_Resumes]
GO
ALTER TABLE [dbo].[ResumeViews]  WITH CHECK ADD  CONSTRAINT [FK_ResumeViews_Users] FOREIGN KEY([ViewerId])
REFERENCES [dbo].[Users] ([Id])
GO
ALTER TABLE [dbo].[ResumeViews] CHECK CONSTRAINT [FK_ResumeViews_Users]
GO
ALTER TABLE [dbo].[Schedules]  WITH CHECK ADD  CONSTRAINT [FK_Schedules_Organizations] FOREIGN KEY([OrgId])
REFERENCES [dbo].[Organizations] ([Id])
GO
ALTER TABLE [dbo].[Schedules] CHECK CONSTRAINT [FK_Schedules_Organizations]
GO
ALTER TABLE [dbo].[Schedules]  WITH CHECK ADD  CONSTRAINT [FK_Schedules_Users] FOREIGN KEY([CreatedBy])
REFERENCES [dbo].[Users] ([Id])
GO
ALTER TABLE [dbo].[Schedules] CHECK CONSTRAINT [FK_Schedules_Users]
GO
ALTER TABLE [dbo].[Schedules]  WITH CHECK ADD  CONSTRAINT [FK_Schedules_Users1] FOREIGN KEY([ModifiedBy])
REFERENCES [dbo].[Users] ([Id])
GO
ALTER TABLE [dbo].[Schedules] CHECK CONSTRAINT [FK_Schedules_Users1]
GO
ALTER TABLE [dbo].[ShareStory]  WITH CHECK ADD  CONSTRAINT [FK_ShareStory_Users] FOREIGN KEY([CreatedBy])
REFERENCES [dbo].[Users] ([Id])
GO
ALTER TABLE [dbo].[ShareStory] CHECK CONSTRAINT [FK_ShareStory_Users]
GO
ALTER TABLE [dbo].[ShareStoryFile]  WITH CHECK ADD  CONSTRAINT [FK_ShareStoryFile_Files1] FOREIGN KEY([FileId])
REFERENCES [dbo].[Files] ([Id])
GO
ALTER TABLE [dbo].[ShareStoryFile] CHECK CONSTRAINT [FK_ShareStoryFile_Files1]
GO
ALTER TABLE [dbo].[ShareStoryFile]  WITH CHECK ADD  CONSTRAINT [FK_ShareStoryFile_ShareStory] FOREIGN KEY([ShareStoryId])
REFERENCES [dbo].[ShareStory] ([id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[ShareStoryFile] CHECK CONSTRAINT [FK_ShareStoryFile_ShareStory]
GO
ALTER TABLE [dbo].[SiteReferences]  WITH CHECK ADD  CONSTRAINT [FK_SiteReferences_ReferenceTypes1] FOREIGN KEY([ReferenceTypeId])
REFERENCES [dbo].[ReferenceTypes] ([Id])
GO
ALTER TABLE [dbo].[SiteReferences] CHECK CONSTRAINT [FK_SiteReferences_ReferenceTypes1]
GO
ALTER TABLE [dbo].[SiteReferences]  WITH CHECK ADD  CONSTRAINT [FK_SiteReferences_Users] FOREIGN KEY([UserId])
REFERENCES [dbo].[Users] ([Id])
GO
ALTER TABLE [dbo].[SiteReferences] CHECK CONSTRAINT [FK_SiteReferences_Users]
GO
ALTER TABLE [dbo].[SubscriptionTransactions]  WITH CHECK ADD  CONSTRAINT [FK_SubscriptionTransactions_CnmProSubscriptions] FOREIGN KEY([SubscriptionId])
REFERENCES [dbo].[CnmProSubscriptions] ([Id])
GO
ALTER TABLE [dbo].[SubscriptionTransactions] CHECK CONSTRAINT [FK_SubscriptionTransactions_CnmProSubscriptions]
GO
ALTER TABLE [dbo].[SubscriptionTransactions]  WITH CHECK ADD  CONSTRAINT [FK_SubscriptionTransactions_PaymentAccounts] FOREIGN KEY([PaymentAcctId])
REFERENCES [dbo].[PaymentAccounts] ([Id])
GO
ALTER TABLE [dbo].[SubscriptionTransactions] CHECK CONSTRAINT [FK_SubscriptionTransactions_PaymentAccounts]
GO
ALTER TABLE [dbo].[SurveyAnswers]  WITH CHECK ADD  CONSTRAINT [FK_SurveyAnswers_SurveyInstances] FOREIGN KEY([InstanceId])
REFERENCES [dbo].[SurveyInstances] ([Id])
GO
ALTER TABLE [dbo].[SurveyAnswers] CHECK CONSTRAINT [FK_SurveyAnswers_SurveyInstances]
GO
ALTER TABLE [dbo].[SurveyAnswers]  WITH CHECK ADD  CONSTRAINT [FK_SurveyAnswers_SurveyQuestionAnswerOptions] FOREIGN KEY([AnswerOptionId])
REFERENCES [dbo].[SurveyQuestionAnswerOptions] ([Id])
GO
ALTER TABLE [dbo].[SurveyAnswers] CHECK CONSTRAINT [FK_SurveyAnswers_SurveyQuestionAnswerOptions]
GO
ALTER TABLE [dbo].[SurveyAnswers]  WITH CHECK ADD  CONSTRAINT [FK_SurveyAnswers_SurveyQuestions] FOREIGN KEY([QuestionId])
REFERENCES [dbo].[SurveyQuestions] ([Id])
GO
ALTER TABLE [dbo].[SurveyAnswers] CHECK CONSTRAINT [FK_SurveyAnswers_SurveyQuestions]
GO
ALTER TABLE [dbo].[SurveyInstances]  WITH CHECK ADD  CONSTRAINT [FK_SurveyInstances_Surveys] FOREIGN KEY([SurveyId])
REFERENCES [dbo].[Surveys] ([Id])
GO
ALTER TABLE [dbo].[SurveyInstances] CHECK CONSTRAINT [FK_SurveyInstances_Surveys]
GO
ALTER TABLE [dbo].[SurveyInstances]  WITH CHECK ADD  CONSTRAINT [FK_SurveyInstances_SurveyStatus] FOREIGN KEY([StatusId])
REFERENCES [dbo].[SurveyStatus] ([Id])
GO
ALTER TABLE [dbo].[SurveyInstances] CHECK CONSTRAINT [FK_SurveyInstances_SurveyStatus]
GO
ALTER TABLE [dbo].[SurveyInstances]  WITH CHECK ADD  CONSTRAINT [FK_SurveyInstances_Users] FOREIGN KEY([UserId])
REFERENCES [dbo].[Users] ([Id])
GO
ALTER TABLE [dbo].[SurveyInstances] CHECK CONSTRAINT [FK_SurveyInstances_Users]
GO
ALTER TABLE [dbo].[SurveyQuestionAnswerOptions]  WITH CHECK ADD  CONSTRAINT [FK_SurveyQuestionAnswerOptions_SurveyQuestions] FOREIGN KEY([QuestionId])
REFERENCES [dbo].[SurveyQuestions] ([Id])
GO
ALTER TABLE [dbo].[SurveyQuestionAnswerOptions] CHECK CONSTRAINT [FK_SurveyQuestionAnswerOptions_SurveyQuestions]
GO
ALTER TABLE [dbo].[SurveyQuestionAnswerOptions]  WITH CHECK ADD  CONSTRAINT [FK_SurveyQuestionAnswerOptions_Users] FOREIGN KEY([CreatedBy])
REFERENCES [dbo].[Users] ([Id])
GO
ALTER TABLE [dbo].[SurveyQuestionAnswerOptions] CHECK CONSTRAINT [FK_SurveyQuestionAnswerOptions_Users]
GO
ALTER TABLE [dbo].[SurveyQuestions]  WITH CHECK ADD  CONSTRAINT [FK_SurveyQuestions_QuestionTypes1] FOREIGN KEY([QuestionTypeId])
REFERENCES [dbo].[QuestionTypes] ([Id])
GO
ALTER TABLE [dbo].[SurveyQuestions] CHECK CONSTRAINT [FK_SurveyQuestions_QuestionTypes1]
GO
ALTER TABLE [dbo].[SurveyQuestions]  WITH CHECK ADD  CONSTRAINT [FK_SurveyQuestions_Surveys] FOREIGN KEY([SurveyId])
REFERENCES [dbo].[Surveys] ([Id])
GO
ALTER TABLE [dbo].[SurveyQuestions] CHECK CONSTRAINT [FK_SurveyQuestions_Surveys]
GO
ALTER TABLE [dbo].[SurveyQuestions]  WITH CHECK ADD  CONSTRAINT [FK_SurveyQuestions_SurveyStatus] FOREIGN KEY([StatusId])
REFERENCES [dbo].[SurveyStatus] ([Id])
GO
ALTER TABLE [dbo].[SurveyQuestions] CHECK CONSTRAINT [FK_SurveyQuestions_SurveyStatus]
GO
ALTER TABLE [dbo].[SurveyQuestions]  WITH CHECK ADD  CONSTRAINT [FK_SurveyQuestions_Users] FOREIGN KEY([UserId])
REFERENCES [dbo].[Users] ([Id])
GO
ALTER TABLE [dbo].[SurveyQuestions] CHECK CONSTRAINT [FK_SurveyQuestions_Users]
GO
ALTER TABLE [dbo].[Surveys]  WITH CHECK ADD  CONSTRAINT [FK_Surveys_SurveyStatus] FOREIGN KEY([StatusId])
REFERENCES [dbo].[SurveyStatus] ([Id])
GO
ALTER TABLE [dbo].[Surveys] CHECK CONSTRAINT [FK_Surveys_SurveyStatus]
GO
ALTER TABLE [dbo].[Surveys]  WITH CHECK ADD  CONSTRAINT [FK_Surveys_SurveyTypes] FOREIGN KEY([SurveyTypeId])
REFERENCES [dbo].[SurveyTypes] ([Id])
GO
ALTER TABLE [dbo].[Surveys] CHECK CONSTRAINT [FK_Surveys_SurveyTypes]
GO
ALTER TABLE [dbo].[Surveys]  WITH CHECK ADD  CONSTRAINT [FK_Surveys_Users] FOREIGN KEY([CreatedBy])
REFERENCES [dbo].[Users] ([Id])
GO
ALTER TABLE [dbo].[Surveys] CHECK CONSTRAINT [FK_Surveys_Users]
GO
ALTER TABLE [dbo].[Timesheet]  WITH CHECK ADD  CONSTRAINT [FK_Timesheet_Organizations] FOREIGN KEY([OrgId])
REFERENCES [dbo].[Organizations] ([Id])
GO
ALTER TABLE [dbo].[Timesheet] CHECK CONSTRAINT [FK_Timesheet_Organizations]
GO
ALTER TABLE [dbo].[Timesheet]  WITH CHECK ADD  CONSTRAINT [FK_Timesheet_Users] FOREIGN KEY([CreatedBy])
REFERENCES [dbo].[Users] ([Id])
GO
ALTER TABLE [dbo].[Timesheet] CHECK CONSTRAINT [FK_Timesheet_Users]
GO
ALTER TABLE [dbo].[UserJobs]  WITH CHECK ADD  CONSTRAINT [FK_UserJobs_Jobs] FOREIGN KEY([JobId])
REFERENCES [dbo].[Jobs] ([Id])
GO
ALTER TABLE [dbo].[UserJobs] CHECK CONSTRAINT [FK_UserJobs_Jobs]
GO
ALTER TABLE [dbo].[UserJobs]  WITH CHECK ADD  CONSTRAINT [FK_UserJobs_Users] FOREIGN KEY([UserId])
REFERENCES [dbo].[Users] ([Id])
GO
ALTER TABLE [dbo].[UserJobs] CHECK CONSTRAINT [FK_UserJobs_Users]
GO
ALTER TABLE [dbo].[UserLicense]  WITH CHECK ADD  CONSTRAINT [FK_UserLicense_LicenseTypes] FOREIGN KEY([LicenseTypesId])
REFERENCES [dbo].[LicenseTypes] ([Id])
GO
ALTER TABLE [dbo].[UserLicense] CHECK CONSTRAINT [FK_UserLicense_LicenseTypes]
GO
ALTER TABLE [dbo].[UserLicense]  WITH CHECK ADD  CONSTRAINT [FK_UserLicense_Locations] FOREIGN KEY([LocationsId])
REFERENCES [dbo].[Locations] ([Id])
GO
ALTER TABLE [dbo].[UserLicense] CHECK CONSTRAINT [FK_UserLicense_Locations]
GO
ALTER TABLE [dbo].[UserLicense]  WITH CHECK ADD  CONSTRAINT [FK_UserLicense_States] FOREIGN KEY([LicenseStateId])
REFERENCES [dbo].[States] ([Id])
GO
ALTER TABLE [dbo].[UserLicense] CHECK CONSTRAINT [FK_UserLicense_States]
GO
ALTER TABLE [dbo].[UserLicense]  WITH CHECK ADD  CONSTRAINT [FK_UserLicense_UserProfiles] FOREIGN KEY([UserId])
REFERENCES [dbo].[UserProfiles] ([Id])
GO
ALTER TABLE [dbo].[UserLicense] CHECK CONSTRAINT [FK_UserLicense_UserProfiles]
GO
ALTER TABLE [dbo].[UserProfiles]  WITH CHECK ADD  CONSTRAINT [FK_UserProfiles_Locations] FOREIGN KEY([LocationId])
REFERENCES [dbo].[Locations] ([Id])
GO
ALTER TABLE [dbo].[UserProfiles] CHECK CONSTRAINT [FK_UserProfiles_Locations]
GO
ALTER TABLE [dbo].[UserProfiles]  WITH CHECK ADD  CONSTRAINT [FK_UserProfiles_Users] FOREIGN KEY([UserId])
REFERENCES [dbo].[Users] ([Id])
GO
ALTER TABLE [dbo].[UserProfiles] CHECK CONSTRAINT [FK_UserProfiles_Users]
GO
ALTER TABLE [dbo].[UserRoles]  WITH CHECK ADD  CONSTRAINT [FK_UserRoles_Roles] FOREIGN KEY([RoleId])
REFERENCES [dbo].[Roles] ([Id])
GO
ALTER TABLE [dbo].[UserRoles] CHECK CONSTRAINT [FK_UserRoles_Roles]
GO
ALTER TABLE [dbo].[UserRoles]  WITH CHECK ADD  CONSTRAINT [FK_UserRoles_Users] FOREIGN KEY([UserId])
REFERENCES [dbo].[Users] ([Id])
GO
ALTER TABLE [dbo].[UserRoles] CHECK CONSTRAINT [FK_UserRoles_Users]
GO
ALTER TABLE [dbo].[UsersLicenses]  WITH CHECK ADD  CONSTRAINT [FK_UserProfileLicenses_Licenses] FOREIGN KEY([LicenseId])
REFERENCES [dbo].[Licenses] ([Id])
GO
ALTER TABLE [dbo].[UsersLicenses] CHECK CONSTRAINT [FK_UserProfileLicenses_Licenses]
GO
ALTER TABLE [dbo].[UserSubscriptionStatus]  WITH CHECK ADD  CONSTRAINT [FK_UserSubscriptionStatus_SubscriptionTransactions] FOREIGN KEY([SubscriptionId])
REFERENCES [dbo].[SubscriptionTransactions] ([Id])
GO
ALTER TABLE [dbo].[UserSubscriptionStatus] CHECK CONSTRAINT [FK_UserSubscriptionStatus_SubscriptionTransactions]
GO
ALTER TABLE [dbo].[UserTokens]  WITH CHECK ADD  CONSTRAINT [FK_UserTokens_TokenTypes] FOREIGN KEY([TokenType])
REFERENCES [dbo].[TokenTypes] ([Id])
GO
ALTER TABLE [dbo].[UserTokens] CHECK CONSTRAINT [FK_UserTokens_TokenTypes]
GO
ALTER TABLE [dbo].[UserTokens]  WITH CHECK ADD  CONSTRAINT [FK_UserTokens_Users] FOREIGN KEY([UserId])
REFERENCES [dbo].[Users] ([Id])
GO
ALTER TABLE [dbo].[UserTokens] CHECK CONSTRAINT [FK_UserTokens_Users]
GO
/****** Object:  StoredProcedure [dbo].[AdminDashboard_Search_Organizations]    Script Date: 12/2/2022 3:13:23 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author: <Lucas Gil>
-- Create date: <8/3/22>
-- Description: [dbo].[AdminDashboard_Search_Organizations]>
-- Code Reviewer:

-- MODIFIED BY: <author>
-- MODIFIED DATE: 07/28/2022
-- Code Reviewer:
-- Note: none
-- =============================================

CREATE PROC [dbo].[AdminDashboard_Search_Organizations]
					@Query nvarchar(50)

AS

/* -/-/-/- TESTING CODE BEGIN -/-/-/-

Declare @Query nvarchar(50) = 'Nike'

Execute [dbo].[AdminDashboard_Search_Organizations]
			@Query

-/-/-/-/-/- TESTING CODE END -/-/-/-/-/-


SELECT *
FROM [dbo].[Organizations]

SELECT *
FROM [dbo].[Locations]

*/

BEGIN

SELECT 
		o.[Id],
		o.[Name],
		o.[Headline],
		o.[Logo],
		o.[SiteUrl],
		loc.[City],
		loc.[LineOne],
		loc.[LineTwo],
		s.[Code] as [StateCode],
		loc.[Zip],
		o.DateCreated

	FROM		[dbo].[Organizations] as o 
			inner join [dbo].[Locations] as loc				on o.LocationId = loc.Id
			inner join [dbo].States as s
				on loc.StateId = s.Id
	WHERE o.[Name] LIKE '%' + @Query + '%'
	OR loc.[City] LIKE '%' + @Query + '%'
	OR s.[Code] LIKE '%' + @Query + '%'
	ORDER BY o.Id

END
GO
/****** Object:  StoredProcedure [dbo].[AdminDashboard_Select_Info]    Script Date: 12/2/2022 3:13:23 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author: <Lucas Gil>
-- Create date: <12/07/2022>
-- Description: <Admin Dashboard Information]>
-- Code Reviewer:

-- MODIFIED BY: <Lucas Gil>
-- MODIFIED DATE: 18/07/2022
-- Code Reviewer: James Hughes 
-- Note:
-- =============================================

CREATE proc [dbo].[AdminDashboard_Select_Info]

AS
/*
	Execute [dbo].[AdminDashboard_Select_Info]
*/
BEGIN

Declare @totalOrg int,
		@totalUsers int,
		@activeUsers int;

SELECT @totalOrg=COUNT(1)
  FROM [dbo].[Organizations] as o 
			inner join [dbo].[Locations] as loc				on o.LocationId = loc.Id
			inner join [dbo].States as s
				on loc.StateId = s.Id

SELECT	@totalUsers=COUNT(1)
  FROM [dbo].[Users]

SELECT @activeUsers = count(1)
  FROM [dbo].[Users]
  Where UserStatusId = 1

/***** Info Table ****/

Select TotalOrg = @totalOrg,
		TotalUsers = @totalUsers,
		ActiveUsers = @activeUsers

END
GO
/****** Object:  StoredProcedure [dbo].[AdminDashboard_Select_InfoWeekly]    Script Date: 12/2/2022 3:13:23 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author: <Lucas Gil>
-- Create date: <13/07/2022>
-- Description: <Admin Dashboard Information]>
-- Code Reviewer:

-- MODIFIED BY: <Lucas Gil>
-- MODIFIED DATE: 18/07/2022
-- Code Reviewer: James Hughes
-- Note:
-- =============================================
CREATE proc [dbo].[AdminDashboard_Select_InfoWeekly]

AS

/*
	Execute [dbo].[AdminDashboard_Select_InfoWeekly]

	SELECT *
	FROM dbo.Users
		SELECT *
	FROM dbo.organizations
*/

BEGIN

DECLARE @Zero dec = 1;

/********** TOTAL ORGANIZATIONS **********/

DECLARE		
		@weekO			date = DATEADD(WEEK,-1,GETDATE()), 
		@weekTO			date = DATEADD(WEEK,-2,GETDATE()),
		@weeklyOrg		dec,
		@weeklyTOrg		dec, 
		@div			dec
DECLARE
		@orgInfo		table ([Id] int, 
								[Name] nvarchar(200), 
								[Headline] nvarchar(200),
								[Logo] nvarchar(255),
								[SiteUrl] nvarchar(255),
								[City] nvarchar(50),
								[LineOne] nvarchar(50),
								[LineTwo] nvarchar(50),
								[StateCode] nvarchar(50),
								[Zip] nvarchar(50),
								[DateCreated] datetime2(7))

INSERT @orgInfo EXEC [dbo].[AdminDashboard_Select_Organizations]

SELECT
		@weeklyOrg = COUNT(1)
		FROM @orgInfo
		WHERE DateCreated >= @weekO
SELECT	
		@weeklyTOrg = COUNT(1)
		FROM @orgInfo as o

		WHERE o.DateCreated >= @weekTO AND o.DateCreated <= @weekO
DECLARE 
		@SumOne dec = @weeklyTOrg + @weeklyOrg
DECLARE 
		@SubtractionOne dec = @SumOne - @weeklyTOrg
			IF @weeklyTOrg<=0
			SELECT @weeklyTOrg= @Zero
			IF @weeklyTOrg = @Zero
			Select @div = CAST(ROUND((@SubtractionOne/@weeklyTOrg)*100,2,1) AS DECIMAL(20,2))
			ELSE
			SELECT @div = CAST(ROUND((@SubtractionOne/@weeklyTOrg)*100,2,1) AS DECIMAL(20,2))

/********** TOTAL USERS ***********/

DECLARE		
		@week			date = DATEADD(WEEK,-1,GETDATE()), 
		@weekt			date = DATEADD(WEEK,-2,GETDATE()),
		@weeklyUsers	dec,
		@weeklyTUsers	dec, 
		@divTwo			dec;
SELECT
		@weeklyUsers = COUNT(1)
		FROM [dbo].[Users]
		WHERE DateCreated >= @week
SELECT	
		@weeklyTUsers = COUNT(1)
		FROM [dbo].[Users]
		WHERE DateCreated >= @weekT AND DateCreated <= @week
DECLARE 
		@SumTwo dec = @weeklyTUsers + @weeklyUsers
DECLARE 
		@SubtractionTwo dec = @SumTwo - @weeklyTUsers
			IF @weeklyUsers<=0
			SELECT @divTwo= @Zero
			ELSE
			SELECT @divTwo = CAST(ROUND((@SubtractionTwo/@weeklyTUsers)*100,2,1) AS DECIMAL(20,2))

 /********** ACTIVE USERS **********/

DECLARE		
		@weekA			date = DATEADD(WEEK,-1,GETDATE()), 
		@weekTA			date = DATEADD(WEEK,-2,GETDATE()),
		@weeklyAUsers	dec,
		@weeklyTAUsers	dec, 
		@divThree		dec;
SELECT
		@weeklyAUsers = COUNT(1)
		FROM [dbo].[Users]
		WHERE DateCreated >= @weekA AND UserStatusId = 1
SELECT	
		@weeklyTAUsers = COUNT(1)
		FROM [dbo].[Users]
		WHERE DateCreated >= @weekTA AND DateCreated <= @weekA AND UserStatusId = 1
DECLARE 
		@SumThree dec = @weeklyTAUsers + @weeklyAUsers
DECLARE 
		@SubtractionThree dec = @SumThree - @weeklyTAUsers
			IF @weeklyAUsers<=0
			SELECT @divThree= @Zero
			ELSE
			SELECT @divThree = CAST(ROUND((@SubtractionThree/@weeklyTUsers)*100,2,1) AS DECIMAL(20,2))

/********* Info Table **********/

SELECT OrganizationGrowth = @div,
		UsersGrowth = @divTwo,
		ActiveUsersGrowth = @divThree
END
GO
/****** Object:  StoredProcedure [dbo].[AdminDashboard_Select_Organizations]    Script Date: 12/2/2022 3:13:23 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author: <Lucas Gil>
-- Create date: <7/28/22>
-- Description: AdminDashboard_Select_OrganizationsV2>
-- Code Reviewer:

-- MODIFIED BY: <author>
-- MODIFIED DATE: 07/28/2022
-- Code Reviewer:
-- Note: none
-- =============================================

CREATE PROC [dbo].[AdminDashboard_Select_Organizations]
					

AS

/* ---- TESTING CODE BEGIN ----

Execute [dbo].[AdminDashboard_Select_Organizations]

SELECT *
FROM [dbo].[Organizations]

SELECT *
FROM [dbo].[Locations]

*/ ----  TESTING CODE END  ----

BEGIN

SELECT 
		o.[Id],
		o.[Name],
		o.[Headline],
		o.[Logo],
		o.[SiteUrl],
		loc.[City],
		loc.[LineOne],
		loc.[LineTwo],
		s.[Code] as [StateCode],
		loc.[Zip],
		o.DateCreated

	FROM		[dbo].[Organizations] as o 
			inner join [dbo].[Locations] as loc				on o.LocationId = loc.Id
			inner join [dbo].States as s
				on loc.StateId = s.Id

		ORDER BY o.Id

END
GO
/****** Object:  StoredProcedure [dbo].[AdminDashboard_Select_UserMonth]    Script Date: 12/2/2022 3:13:23 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROC [dbo].[AdminDashboard_Select_UserMonth]

AS 

/* ----- TESTING CODE BEGINING ------

EXECUTE [dbo].[AdminDashboard_Select_UserMonth]

////

select * from dbo.Users

*/ ----- TESTING CODE ENDING --------

BEGIN

DECLARE @month12 int,
		@month11 int,
		@month10 int,
		@month9 int,
		@month8 int,
		@month7 int,
		@month6 int,
		@month5 int,
		@month4 int,
		@month3 int,
		@month2 int,
		@month1 int,
		@week4 int,
		@week3 int,
		@week2 int,
		@week1 int
DECLARE 
		@today		nvarchar(50) = FORMAT(GETUTCDATE(),'MMM dd yyyy'),
		@theMonth12 nvarchar(50) = FORMAT(DATEADD(MONTH, -12, GETDATE()),'MMM dd yyyy'),
		@theMonth11 nvarchar(50) = FORMAT(DATEADD(MONTH, -11, GETDATE()),'MMM dd yyyy'),
		@theMonth10 nvarchar(50) = FORMAT(DATEADD(MONTH, -10, GETDATE()),'MMM dd yyyy'),
		@theMonth9	nvarchar(50) = FORMAT(DATEADD(MONTH, -9, GETDATE()),'MMM dd yyyy'),
		@theMonth8	nvarchar(50) = FORMAT(DATEADD(MONTH, -8, GETDATE()),'MMM dd yyyy'),
		@theMonth7	nvarchar(50) = FORMAT(DATEADD(MONTH, -7, GETDATE()),'MMM dd yyyy'),
		@theMonth6	nvarchar(50) = FORMAT(DATEADD(MONTH, -6, GETDATE()),'MMM dd yyyy'),
		@theMonth5	nvarchar(50) = FORMAT(DATEADD(MONTH, -5, GETDATE()),'MMM dd yyyy'),
		@theMonth4	nvarchar(50) = FORMAT(DATEADD(MONTH, -4, GETDATE()),'MMM dd yyyy'),
		@theMonth3	nvarchar(50) = FORMAT(DATEADD(MONTH, -3, GETDATE()),'MMM dd yyyy'),
		@theMonth2	nvarchar(50) = FORMAT(DATEADD(MONTH, -2, GETDATE()),'MMM dd yyyy'),
		@theMonth1	nvarchar(50) = FORMAT(DATEADD(MONTH, -1, GETDATE()),'MMM dd yyyy'),
		@theWeek4	nvarchar(50) = FORMAT(DATEADD(WEEK, -1, GETDATE()),'MMM dd yyyy'),
		@theWeek3	nvarchar(50) = FORMAT(DATEADD(WEEK, -2, GETDATE()),'MMM dd yyyy'),
		@theWeek2	nvarchar(50) = FORMAT(DATEADD(WEEK, -3, GETDATE()),'MMM dd yyyy'),
		@theWeek1	nvarchar(50) = FORMAT(DATEADD(WEEK, -4, GETDATE()),'MMM dd yyyy')

		SELECT @month12 = COUNT(1)
		FROM [dbo].[Users]
		WHERE DateCreated >= @theMonth12 AND DateCreated < DATEADD(MONTH,1,@theMonth12)

		SELECT @month11 = COUNT(1)
		FROM [dbo].[Users]
		WHERE DateCreated >= @theMonth11 AND DateCreated < DATEADD(MONTH,1,@theMonth11)

		SELECT @month10 = COUNT(1)
		FROM [dbo].[Users]
		WHERE DateCreated >= @theMonth10 AND DateCreated < DATEADD(MONTH,1,@theMonth10)

		SELECT @month9 = COUNT(1)
		FROM [dbo].[Users]
		WHERE DateCreated >= @theMonth9 AND DateCreated < DATEADD(MONTH,1,@theMonth9)

		SELECT @month8 = COUNT(1)
		FROM [dbo].[Users]
		WHERE DateCreated >= @theMonth8 AND DateCreated < DATEADD(MONTH,1,@theMonth8)

		SELECT @month7 = COUNT(1)
		FROM [dbo].[Users]
		WHERE DateCreated >= @theMonth7 AND DateCreated < DATEADD(MONTH,1,@theMonth7)

		SELECT @month6 = COUNT(1)
		FROM [dbo].[Users]
		WHERE DateCreated >= @theMonth6 AND DateCreated < DATEADD(MONTH,1,@theMonth6)

		SELECT @month5 = COUNT(1)
		FROM [dbo].[Users]
		WHERE DateCreated >= @theMonth5 AND DateCreated < DATEADD(MONTH,1,@theMonth5)

		SELECT @month4 = COUNT(1)
		FROM [dbo].[Users]
		WHERE DateCreated >= @theMonth4 AND DateCreated < DATEADD(MONTH,1,@theMonth4)

		SELECT @month3 = COUNT(1)
		FROM [dbo].[Users]
		WHERE DateCreated >= @theMonth3 AND DateCreated < DATEADD(MONTH,1,@theMonth3)

		SELECT @month2 = COUNT(1)
		FROM [dbo].[Users]
		WHERE DateCreated >= @theMonth2 AND DateCreated < DATEADD(MONTH,1,@theMonth2)

		SELECT @month1 = COUNT(1)
		FROM [dbo].[Users]
		WHERE DateCreated >= @theMonth1 AND DateCreated < DATEADD(MONTH,1,@theMonth1)

		SELECT @week4 = COUNT(1)
		FROM [dbo].[Users]
		WHERE DateCreated >= @theWeek4 AND DateCreated < DATEADD(WEEK,1,@theWeek4)

		SELECT @week3 = COUNT(1)
		FROM [dbo].[Users]
		WHERE DateCreated >= @theWeek3 AND DateCreated < DATEADD(WEEK,1,@theWeek3)

		SELECT @week2 = COUNT(1)
		FROM [dbo].[Users]
		WHERE DateCreated >= @theWeek2 AND DateCreated < DATEADD(WEEK,1,@theWeek2)

		SELECT @week1 = COUNT(1)
		FROM [dbo].[Users]
		WHERE DateCreated >= @theWeek1 AND DateCreated < DATEADD(WEEK,1,@theWeek1)

SET @theWeek1 += ' - '
SET @theWeek1 += @theWeek2

SET @theWeek2 += ' - '
SET @theWeek2 += @theWeek3

SET @theWeek3 += ' - '
SET @theWeek3 += @theWeek4

SET @theWeek4 += ' - '
SET @theWeek4 += @today

DECLARE @tableMonth TABLE (TheWeeks INT, TheDate nvarchar(50))

INSERT INTO @tableMonth
VALUES
		(@week1, @theWeek1),
		(@week2, @theWeek2),
		(@week3, @theWeek3),
		(@week4, @theWeek4),
		(@month1, @theWeek3)
SELECT *
FROM @tableMonth
		
DECLARE @tableYear TABLE (TheMonths INT, TheDate nvarchar(50))

INSERT INTO @tableYear
VALUES		
		(@month12, @theMonth12),
		(@month11, @theMonth11),
		(@month10, @theMonth10),
		(@month9, @theMonth9),
		(@month8, @theMonth8),
		(@month7, @theMonth7),
		(@month6, @theMonth6),
		(@month5, @theMonth5),
		(@month4, @theMonth4),
		(@month3, @theMonth3),
		(@month2, @theMonth2),
		(@month1, @theMonth1)
		
SELECT *
FROM @tableYear

END
GO
/****** Object:  StoredProcedure [dbo].[AdminDashboard_Select_UserWeek]    Script Date: 12/2/2022 3:13:23 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author: <Lucas Gil>
-- Create date: <07/19/2022>
-- Description: <[dbo].[AdminDashboard_Select_UserWeek]>
-- Code Reviewer:

-- MODIFIED BY: author
-- MODIFIED DATE:07/21/2022
-- Code Reviewer:
-- Note:
-- =============================================

CREATE PROC [dbo].[AdminDashboard_Select_UserWeek]

AS

/* ----- TEST CODE -----
	
	EXECUTE [dbo].[AdminDashboard_Select_UserWeek]

	///////////////////////////

	select * from dbo.Users

*/ ----- END TEST CODE -----

BEGIN

DECLARE	@day14 int,
		@day13 int,
		@day12 int,
		@day11 int,
		@day10 int,
		@day9 int,
		@day8 int,
		@day7 int,
		@day6 int,
		@day5 int,
		@day4 int,
		@day3 int,
		@day2 int,
		@day1 int

DECLARE 
		@theDay14 NVARCHAR(50) = FORMAT(GETUTCDATE(),'MMM dd yyyy'),
		@theDay13 nvarchar(50) = FORMAT(DATEADD(DAY,-1,GETDATE()),'MMM dd yyyy'),
		@theDay12 nvarchar(50) = FORMAT(DATEADD(DAY,-2,GETDATE()),'MMM dd yyyy'),
		@theDay11 nvarchar(50) = FORMAT(DATEADD(DAY,-3,GETDATE()),'MMM dd yyyy'),
		@theDay10 nvarchar(50) = FORMAT(DATEADD(DAY,-4,GETDATE()),'MMM dd yyyy'),
		@theDay9 nvarchar(50) = FORMAT(DATEADD(DAY,-5,GETDATE()),'MMM dd yyyy'),
		@theDay8 nvarchar(50) = FORMAT(DATEADD(DAY,-6,GETDATE()),'MMM dd yyyy'),
		@theDay7 NVARCHAR(50) = FORMAT(DATEADD(DAY,-7,GETDATE()),'MMM dd yyyy'),
		@theDay6 nvarchar(50) = FORMAT(DATEADD(DAY,-8,GETDATE()),'MMM dd yyyy'),
		@theDay5 nvarchar(50) = FORMAT(DATEADD(DAY,-9,GETDATE()),'MMM dd yyyy'),
		@theDay4 nvarchar(50) = FORMAT(DATEADD(DAY,-10,GETDATE()),'MMM dd yyyy'),
		@theDay3 nvarchar(50) = FORMAT(DATEADD(DAY,-11,GETDATE()),'MMM dd yyyy'),
		@theDay2 nvarchar(50) = FORMAT(DATEADD(DAY,-12,GETDATE()),'MMM dd yyyy'),
		@theDay1 nvarchar(50) = FORMAT(DATEADD(DAY,-13,GETDATE()),'MMM dd yyyy')

		SELECT @day14 = COUNT(1)
		FROM [dbo].[Users]
		WHERE DateCreated >= @theDay14 AND DateCreated < DATEADD(DAY,1,@theDay14)

		SELECT @day13 = COUNT(1)
		FROM [dbo].[Users]
		WHERE DateCreated >= @theDay13 AND DateCreated < DATEADD(DAY,1,@theDay13)

		SELECT @day12 = COUNT(1)
		FROM [dbo].[Users]
		WHERE DateCreated >= @theDay12 AND DateCreated < DATEADD(DAY,1,@theDay12)

		SELECT @day11 = COUNT(1)
		FROM [dbo].[Users]
		WHERE DateCreated >= @theDay11 AND DateCreated < DATEADD(DAY,1,@theDay11)

		SELECT @day10 = COUNT(1)
		FROM [dbo].[Users]
		WHERE DateCreated >= @theDay10 AND DateCreated < DATEADD(DAY,1,@theDay10)

		SELECT @day9 = COUNT(1)
		FROM [dbo].[Users]
		WHERE DateCreated >= @theDay9 AND DateCreated < DATEADD(DAY,1,@theDay9)

		SELECT @day8 = COUNT(1)
		FROM [dbo].[Users]
		WHERE DateCreated >= @theDay8 AND DateCreated < DATEADD(DAY,1,@theDay8)

		SELECT @day7 = COUNT(1)
		FROM [dbo].[Users]
		WHERE DateCreated >= @theDay7 AND DateCreated < DATEADD(DAY,1,@theDay7)

		SELECT @day6 = COUNT(1)
		FROM [dbo].[Users]
		WHERE DateCreated >= @theDay6 AND DateCreated < DATEADD(DAY,1,@theDay6)

		SELECT @day5 = COUNT(1)
		FROM [dbo].[Users]
		WHERE DateCreated >= @theDay5 AND DateCreated < DATEADD(DAY,1,@theDay5)

		SELECT @day4 = COUNT(1)
		FROM [dbo].[Users]
		WHERE DateCreated >= @theDay4 AND DateCreated < DATEADD(DAY,1,@theDay4)

		SELECT @day3 = COUNT(1)
		FROM [dbo].[Users]
		WHERE DateCreated >= @theDay3 AND DateCreated < DATEADD(DAY,1,@theDay3)

		SELECT @day2 = COUNT(1)
		FROM [dbo].[Users]
		WHERE DateCreated >= @theDay2 AND DateCreated < DATEADD(DAY,1,@theDay2)

		SELECT @day1 = COUNT(1)
		FROM [dbo].[Users]
		WHERE DateCreated >= @theDay1 AND DateCreated < DATEADD(DAY,1,@theDay1)

		DECLARE @tableWeek TABLE (TheDays INT, TheDate nvarchar(50))

		INSERT INTO @tableWeek
		VALUES 
				(@day8, @theDay8),
				(@day9, @theDay9),
				(@day10, @theDay10),
				(@day11, @theDay11),
				(@day12, @theDay12),
				(@day13, @theDay13),
				(@day14, @theDay14)
		SELECT *
		FROM @tableWeek

		DECLARE @tableTwoWeek TABLE (TheDays INT, TheDate nvarchar(50))

		INSERT INTO @tableTwoWeek
		VALUES (@day1, @theDay1),
				(@day2, @theDay2),
				(@day3, @theDay3),
				(@day4, @theDay4),
				(@day5, @theDay5),
				(@day6, @theDay6),
				(@day7, @theDay7),
				(@day8, @theDay8),
				(@day9, @theDay9),
				(@day10, @theDay10),
				(@day11, @theDay11),
				(@day12, @theDay12),
				(@day13, @theDay13),
				(@day14, @theDay14)
		SELECT *
		FROM @tableTwoWeek
END
GO
/****** Object:  StoredProcedure [dbo].[Blogs_Delete_ById]    Script Date: 12/2/2022 3:13:23 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author: <Morgan Frost>
-- Create date: <07/01/2022>
-- Description: <Blogs_Delete_ById>
-- Code Reviewer:Jordan Poole 07/06/2022

-- MODIFIED BY: author
-- MODIFIED DATE:07/01/2020
-- Code Reviewer:
-- Note:
-- =============================================

CREATE proc [dbo].[Blogs_Delete_ById]
	@Id int

/*------TEST CODE------

		Declare @Id int = 10

		Select *
		From dbo.Blogs
		Where Id = @Id

		Execute dbo.Blogs_Delete_ById
			@Id

		Select *
		From dbo.Blogs
		Where Id = @Id
*/

AS

BEGIN

DELETE FROM [dbo].[Blogs]
	Where Id = @Id

END
GO
/****** Object:  StoredProcedure [dbo].[Blogs_Insert]    Script Date: 12/2/2022 3:13:23 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author: <Morgan Frost>
-- Create date: <07/01/2022>
-- Description: <Blogs_Insert>
-- Code Reviewer:Jordan Poole 07/06/2022

-- MODIFIED BY: author
-- MODIFIED DATE:07/01/2022
-- Code Reviewer:
-- Note:
-- =============================================

CREATE proc [dbo].[Blogs_Insert]
	@BlogTypeId int
	,@AuthorId int
	,@Title nvarchar(50)
	,@Subject nvarchar(50)
	,@Content nvarchar(4000)
	,@IsPublished bit
	,@ImageUrl nvarchar(255)
	,@DatePublish datetime2(7)
	,@Id int OUTPUT

/*------TEST CODE------
Declare @Id int = 0;


Declare @BlogTypeId int = 1
		,@AuthorId int = 4
		,@Title nvarchar(50) = 'Civil Engineering 101'
		,@Subject nvarchar(50) = 'Introduction'
		,@Content nvarchar(4000) = 'Content goes here'
		,@IsPublished bit = 0
		,@ImageUrl nvarchar(255) = 'image url placeholder'
		,@DatePublish datetime2(7) = 'July 1, 2022'

Execute dbo.Blogs_Insert
		@BlogTypeId
		,@AuthorId 
		,@Title 
		,@Subject 
		,@Content 
		,@IsPublished 
		,@ImageUrl
		,@DatePublish
		,@Id OUTPUT

	Select @Id

Select *
From dbo.Blogs
	


*/

AS

BEGIN




INSERT INTO

[dbo].[Blogs]
		([BlogTypeId] 
		,[AuthorId]
		,[Title]
		,[Subject] 
		,[Content]
		,[IsPublished] 
		,[ImageUrl] 
		,[DatePublish]) 

	VALUES
		(@BlogTypeId
		,@AuthorId
		,@Title
		,@Subject
		,@Content
		,@IsPublished
		,@ImageUrl
		,@DatePublish)

	SET @Id = SCOPE_IDENTITY()



END
GO
/****** Object:  StoredProcedure [dbo].[Blogs_Select_BlogCategory]    Script Date: 12/2/2022 3:13:23 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author: <Morgan Frost>
-- Create date: <07/01/2022>
-- Description: <Blogs_Select_BlogCategory>
-- Code Reviewer:Jordan Poole 07/06/2022

-- MODIFIED BY: author
-- MODIFIED DATE:07/01/2020
-- Code Reviewer:
-- Note:
-- =============================================

CREATE proc [dbo].[Blogs_Select_BlogCategory]
	@Query nvarchar(50)
	,@PageIndex int
	,@PageSize int

/*------TEST CODE------

		Declare @Query nvarchar(50) = 'Introduction'
				,@PageIndex int = 0
				,@PageSize int = 5
				

		Execute dbo.Blogs_Select_BlogCategory
			@Query
			,@PageIndex
			,@PageSize

*/

AS

BEGIN
	DECLARE @offset int = @PageIndex * @PageSize

SELECT	b.[Id]
		,b.[Title]
		,b.[Subject]
		,b.[Content]
		,b.[IsPublished]
		,b.[ImageUrl]
		,b.[DateCreated]
		,b.[DateModified]
		,b.[DatePublish]
		,bt.[Id]
		,bt.[Name] as BlogType
		,b.AuthorId
		,up.[FirstName]
		,up.[LastName]
		,up.[Mi]
		,up.[AvatarUrl]
		,TotalCount = COUNT(1) OVER()

	
	FROM dbo.Blogs as b 
						inner join dbo.BlogTypes as bt

						on b.BlogTypeId = bt.Id

						inner join dbo.UserProfiles as up
						
						on b.AuthorId = up.Id
	
	WHERE (Subject LIKE '%' + @Query + '%')

	ORDER BY b.Id

	OFFSET @offSet Rows
	Fetch Next @PageSize Rows ONLY

END
GO
/****** Object:  StoredProcedure [dbo].[Blogs_Select_ByCreatedBy]    Script Date: 12/2/2022 3:13:23 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author: <Morgan Frost>
-- Create date: <07/01/2022>
-- Description: <Blogs_Select_ByCreatedBy>
-- Code Reviewer:Jordan Poole 07/06/2022

-- MODIFIED BY: author
-- MODIFIED DATE:07/01/2022
-- Code Reviewer:
-- Note:
-- =============================================

CREATE proc [dbo].[Blogs_Select_ByCreatedBy]
	@AuthorId int
	,@PageIndex int
	,@PageSize int

/*------TEST CODE------

		Declare @AuthorId int = 4
				,@PageIndex int = 0
				,@PageSize int = 100

		Execute dbo.Blogs_Select_ByCreatedBy
			@AuthorId
			,@PageIndex
			,@PageSize
*/

AS

BEGIN
	Declare @offset int = @PageIndex * @PageSize

SELECT	b.[Id]
		,b.[Title]
		,b.[Subject]
		,b.[Content]
		,b.[IsPublished]
		,b.[ImageUrl]
		,b.[DateCreated]
		,b.[DateModified]
		,b.[DatePublish]
		,bt.[Id]
		,bt.[Name] as BlogType
		,b.AuthorId
		,up.[FirstName]
		,up.[LastName]
		,up.[Mi]
		,up.[AvatarUrl]
		,TotalCount = COUNT(1) OVER()

	
	FROM dbo.Blogs as b 
						inner join dbo.BlogTypes as bt

						on b.BlogTypeId = bt.Id

						inner join dbo.UserProfiles as up
						
						on b.AuthorId = up.Id

	Where AuthorId = @AuthorId
	ORDER BY b.Id

	OFFSET @offSet Rows
	--Fetch Next @PageSize Rows ONLY

END
GO
/****** Object:  StoredProcedure [dbo].[Blogs_Select_ByCreatedByV2]    Script Date: 12/2/2022 3:13:23 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE Proc [dbo].[Blogs_Select_ByCreatedByV2]
	@PageIndex int
	,@PageSize int

as
/*
		Declare @PageIndex int = 0
				,@PageSize int = 10


		Execute dbo.Blogs_Select_ByCreatedByV2
				@PageIndex
				,@PageSize

*/
BEGIN
	Declare @offset int = @PageIndex * @PageSize


SELECT	
		b.[Id]
		,b.[Title]
		,b.[Subject]
		,b.[Content]
		,b.[IsPublished]
		,b.[ImageUrl]
		,b.[DateCreated]
		,b.[DateModified]
		,b.[DatePublish]
		,bt.[Id]
		,bt.[Name] as BlogType
		,b.AuthorId
		,up.[FirstName]
		,up.[LastName]
		,up.[Mi]
		,up.[AvatarUrl]
		,TotalCount = COUNT(1) OVER()


	FROM dbo.Blogs as b inner join dbo.BlogTypes as bt
						on bt.Id = b.BlogTypeId
						inner join dbo.UserProfiles as up
						on up.UserId = b.AuthorId		

	ORDER BY b.DateCreated DESC

	OFFSET @offSet Rows


END
GO
/****** Object:  StoredProcedure [dbo].[Blogs_Select_ById]    Script Date: 12/2/2022 3:13:23 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author: <Morgan Frost>
-- Create date: <07/01/2022>
-- Description: <Blogs_Select_ById>
-- Code Reviewer: Jordan Poole 07/06/2022

-- MODIFIED BY: author
-- MODIFIED DATE:07/01/2022
-- Code Reviewer:
-- Note:
-- =============================================

CREATE proc [dbo].[Blogs_Select_ById]
	@Id int

/*------TEST CODE------

		Declare @Id int = 30;
		Execute dbo.Blogs_Select_ById
			@Id
*/

AS

BEGIN

SELECT		b.[Id]
		,b.[Title]
		,b.[Subject]
		,b.[Content]
		,b.[IsPublished]
		,b.[ImageUrl]
		,b.[DateCreated]
		,b.[DateModified]
		,b.[DatePublish]
		,bt.[Id]
		,bt.[Name] as BlogType
		,b.[AuthorId]
		,up.[UserId]
		,up.[FirstName]
		,up.[LastName]
		,up.[Mi]
		,up.[AvatarUrl]


	FROM dbo.Blogs as b 
						inner join dbo.BlogTypes as bt

						on b.BlogTypeId = bt.Id

						inner join dbo.UserProfiles as up
						
						on b.AuthorId = up.Id


	WHERE b.Id = @Id

END
GO
/****** Object:  StoredProcedure [dbo].[Blogs_SelectAll]    Script Date: 12/2/2022 3:13:23 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author: <Morgan Frost>
-- Create date: <07/01/2022>
-- Description: <Blogs_SelectAll>
-- Code Reviewer:Jordan Poole 07/06/2022

-- MODIFIED BY: author
-- MODIFIED DATE:07/01/2020
-- Code Reviewer:
-- Note:
-- =============================================

CREATE proc [dbo].[Blogs_SelectAll]
	@PageIndex int
	,@PageSize int

/*------TEST CODE------

		Declare @PageIndex int = 0
				,@PageSize int = 15

		Execute dbo.Blogs_SelectAll
			@PageIndex
			,@PageSize

*/

AS

BEGIN
	Declare @offset int = @PageIndex * @PageSize

SELECT	b.[Id]
		,b.[Title]
		,b.[Subject]
		,b.[Content]
		,b.[IsPublished]
		,b.[ImageUrl]
		,b.[DateCreated]
		,b.[DateModified]
		,b.[DatePublish]
		,bt.[Id]
		,bt.[Name] as BlogType
		,b.[AuthorId]
		,up.[UserId] as Author
		,up.[FirstName]
		,up.[LastName]
		,up.[Mi]
		,up.[AvatarUrl]
		,TotalCount = COUNT(1) OVER()

	
	FROM dbo.Blogs as b 
						inner join dbo.BlogTypes as bt

						on b.BlogTypeId = bt.Id

						inner join dbo.UserProfiles as up
						
						on b.AuthorId = up.UserId
	

	ORDER BY b.DateCreated DESC

	OFFSET @offSet Rows
   FETCH Next @PageSize Rows ONLY

END

GO
/****** Object:  StoredProcedure [dbo].[Blogs_SelectAll_V2]    Script Date: 12/2/2022 3:13:23 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author: <Morgan Frost>
-- Create date: <07/01/2022>
-- Description: <Blogs_SelectAll>
-- Code Reviewer:Jordan Poole 07/06/2022

-- MODIFIED BY: Ramiro Diaz Bravo
-- MODIFIED DATE:08/03/2022
-- Code Reviewer:
-- Note:
-- =============================================

CREATE proc [dbo].[Blogs_SelectAll_V2]
	@PageIndex int
	,@PageSize int

/*------TEST CODE------

		Declare @PageIndex int = 0
				,@PageSize int = 5

		Execute dbo.Blogs_SelectAll
			@PageIndex
			,@PageSize

*/

AS

BEGIN
	Declare @offset int = @PageIndex * @PageSize

SELECT	b.[Id]
		,b.[Title]
		,b.[Subject]
		,b.[Content]
		,b.[IsPublished]
		,b.[ImageUrl]
		,b.[DateCreated]
		,b.[DateModified]
		,b.[DatePublish]
		,bt.[Id] as BlogType
		,bt.[Name]
		,up.[Id] as AuthorId
		,up.[UserId] as Author
		,up.[FirstName]
		,up.[LastName]
		,up.[Mi]
		,up.[AvatarUrl]
		,TotalCount = COUNT(1) OVER()

	FROM dbo.Blogs as b inner join dbo.BlogTypes as bt
						on bt.Id = b.BlogTypeId
						inner join dbo.UserProfiles as up
						on up.UserId = b.AuthorId
	

	ORDER BY b.DateCreated DESC

	OFFSET @offSet Rows
    FETCH Next @PageSize Rows ONLY

END

GO
/****** Object:  StoredProcedure [dbo].[Blogs_SelectAll_V3]    Script Date: 12/2/2022 3:13:23 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author: <Morgan Frost>
-- Create date: <07/01/2022>
-- Description: <Blogs_SelectAll>
-- Code Reviewer:Jordan Poole 07/06/2022

-- MODIFIED BY: author
-- MODIFIED DATE:07/01/2020
-- Code Reviewer:
-- Note:
-- =============================================

CREATE proc [dbo].[Blogs_SelectAll_V3]
	@PageIndex int
	,@PageSize int

/*------TEST CODE------

		Declare @PageIndex int = 0
				,@PageSize int = 15

		Execute dbo.Blogs_SelectAll_V3
			@PageIndex
			,@PageSize

*/

AS

BEGIN
	Declare @offset int = @PageIndex * @PageSize

SELECT	b.[Id]
		,b.[Title]
		,b.[Subject]
		,b.[Content]
		,b.[IsPublished]
		,b.[ImageUrl]
		,b.[DateCreated]
		,b.[DateModified]
		,b.[DatePublish]
		,bt.[Id]
		,bt.[Name] as BlogType
		,b.[AuthorId]
		,up.[UserId] as Author
		,up.[FirstName]
		,up.[LastName]
		,up.[Mi]
		,up.[AvatarUrl]
		,TotalCount = COUNT(1) OVER()

	
	FROM dbo.Blogs as b 
						inner join dbo.BlogTypes as bt

						on b.BlogTypeId = bt.Id

						left outer join dbo.UserProfiles as up
						
						on b.AuthorId = up.UserId
	

	ORDER BY b.DateCreated DESC

	OFFSET @offSet Rows
   FETCH Next @PageSize Rows ONLY

END

GO
/****** Object:  StoredProcedure [dbo].[Blogs_Update]    Script Date: 12/2/2022 3:13:23 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE proc [dbo].[Blogs_Update]
	@Id int
	,@Content nvarchar(4000)


/*------TEST CODE------

		Declare @Id int = 67
			,@Content nvarchar(4000) = '<p>Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. Facilisi cras fermentum odio eu. Augue interdum velit euismod in pellentesque massa placerat duis ultricies.Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. Facilisi cras fermentum odio eu. Augue interdum velit euismod in pellentesque massa placerat duis ultricies.<p>'
		
		Execute dbo.Blogs_Update
			@Id
			,@Content 

		Select *
		From dbo.Blogs
		Where Id = @Id


*/

AS

BEGIN

UPDATE [dbo].[Blogs]
	SET	[Content] = @Content

	Where Id = @Id

END
GO
/****** Object:  StoredProcedure [dbo].[BlogTypes_SelectAll]    Script Date: 12/2/2022 3:13:23 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO


CREATE proc [dbo].[BlogTypes_SelectAll]

/*
		Execute dbo.BlogTypes_SelectAll
*/

AS

BEGIN

SELECT	[Id]
		,[Name]
From [dbo].[BlogTypes]

END
GO
/****** Object:  StoredProcedure [dbo].[Comments_Delete_ById]    Script Date: 12/2/2022 3:13:23 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author: <Joey Oh>
-- Create date: <07,28,22>
-- Description: <N/A>
-- Code Reviewer:

-- MODIFIED BY: author
-- MODIFIED DATE:12/1/2020
-- Code Reviewer:
-- Note:
-- =============================================
CREATE proc [dbo].[Comments_Delete_ById]
		@Id int
as
/*

	Select * from dbo.comments

	Declare @Id int = 30

	Select * 
	From dbo.comments
	WHERE Id = @Id 
	 
	Execute dbo.Comments_Delete_ById
	@Id

	Select * from dbo.Comments
*/

BEGIN

	DELETE FROM dbo.Ratings
	Where CommentId = @Id

	DELETE FROM dbo.comments
	WHERE Id = @Id

END 
GO
/****** Object:  StoredProcedure [dbo].[Comments_Insert]    Script Date: 12/2/2022 3:13:23 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author: <Joey Oh>
-- Create date: <07,28,22>
-- Description: <N/A>
-- Code Reviewer:

-- MODIFIED BY:
-- MODIFIED DATE:
-- Code Reviewer:
-- Note:
-- =============================================
CREATE proc [dbo].[Comments_Insert]
	@Text nvarchar(3000)
	,@ParentId int = null 
	,@EntityTypeId int
	,@EntityId int
	,@CreatedBy int
	,@IsDeleted bit
	,@Id int OUTPUT
as
/*
		Select * from dbo.comments

		Declare @Id int = 0

		Declare 
		,@Text nvarchar(3000) = 'insertNew'
		,@ParentId int = null
		,@EntityTypeId int = '7'
		,@EntityId int = '7'
		,@CreatedBy int = '16'
		,@IsDeleted bit = 'False'

		Execute dbo.Comments_Insert
		@Text
		,@ParentId  
		,@EntityTypeId 
		,@EntityId 
		,@CreatedBy 
		,@IsDeleted
		,@Id OUTPUT

		Select * from dbo.comments

*/
BEGIN
	
	INSERT INTO dbo.comments
	([Text]
	,[ParentId]
	,[EntityTypeId]
	,[EntityId]
	,[CreatedBy]
	,[IsDeleted])
	
    VALUES
        (@Text
		,@ParentId  
		,@EntityTypeId 
		,@EntityId 
		,@CreatedBy 
		,@IsDeleted)

    SET @Id = SCOPE_IDENTITY()
END
GO
/****** Object:  StoredProcedure [dbo].[Comments_Select_ByCreatedBy]    Script Date: 12/2/2022 3:13:23 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author: <Joey Oh>
-- Create date: <07,28,22>
-- Description: <N/A>
-- Code Reviewer:

-- MODIFIED BY: author
-- MODIFIED DATE:12/1/2020
-- Code Reviewer:
-- Note:
-- =============================================
CREATE proc [dbo].[Comments_Select_ByCreatedBy]
				@CreatedBy int,
				@PageIndex int,
				@PageSize int

as
/*
	Select * from dbo.comments

	Declare @CreatedBy int = 22,
	        @PageIndex int = 0,
        	@PageSize int = 10

	Execute dbo.Comments_Select_ByCreatedBy
		@CreatedBy
		,@PageIndex
		,@PageSize

*/
BEGIN
	Declare @offset int = @PageIndex * @PageSize
	
	SELECT C.Id
		,C.[Subject]
		,C.[Text]
		,C.ParentId
		,C.EntityTypeId
		,C.EntityId
		,C.DateCreated
		,C.DateModified
		,C.CreatedBy
		,UP.FirstName
		,UP.LastName
		,UP.AvatarUrl
		,C.IsDeleted
		,TotalCount = COUNT(1) OVER()
	FROM dbo.Comments C 
		inner join dbo.Users U on c.CreatedBy = U.Id
		inner join dbo.EntityTypes E on C.EntityTypeId = E.Id
		inner join dbo.UserProfiles UP on UP.UserId = U.Id 
	  Where CreatedBy = @CreatedBy
	  ORDER BY Id
	  OFFSET @offset ROWS
	  FETCH NEXT @PageSize Rows ONLY
END
GO
/****** Object:  StoredProcedure [dbo].[Comments_Select_ByEntityId]    Script Date: 12/2/2022 3:13:23 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author: <Joey Oh>
-- Create date: <07,28,22>
-- Description: <N/A>
-- Code Reviewer:

-- MODIFIED BY: Joey Oh
-- MODIFIED DATE:08/30/2020
-- Code Reviewer:
-- Note:
-- =============================================
CREATE proc [dbo].[Comments_Select_ByEntityId]
		@EntityId int,
		@EntityTypeId int,
		@PageIndex int,
		@PageSize int

as 
/*
	Declare @EntityId int = 68
		,@EntityTypeId int = 1
		,@PageIndex int = 0
        ,@PageSize int = 10

	Execute dbo.Comments_Select_ByEntityId
		@EntityId
		,@EntityTypeId
		,@PageIndex
        ,@PageSize 

	Select * from dbo.Comments
*/
BEGIN

	Declare @offset int = @PageIndex * @PageSize

	SELECT C.Id
		,C.[Subject]
		,C.[Text]
		,C.ParentId
		,C.EntityTypeId
		,C.EntityId
		,C.DateCreated
		,C.DateModified
		,C.CreatedBy
		,UP.FirstName
		,UP.LastName
		,UP.AvatarUrl
		,C.IsDeleted
		,Replies = (Select CM.Id
					,CM.[Subject]
					,CM.[Text]
					,CM.ParentId
					,CM.EntityTypeId
					,CM.EntityId
					,CM.DateCreated
					,CM.DateModified
					,CM.CreatedBy
					,UPP.FirstName 
					,UPP.LastName
					,UPP.AvatarUrl
					,CM.IsDeleted
					FROM dbo.Comments CM
					inner join dbo.UserProfiles UPP on CM.CreatedBy = UPP.UserId
					where UP.UserId = C.CreatedBy and CM.ParentId = C.Id 
					order by c.DateCreated
					For JSON PATH)
		,TotalCount = COUNT(1) OVER()
	FROM dbo.Comments C 
		inner join dbo.UserProfiles UP on c.CreatedBy = UP.UserId
	  Where EntityId = @EntityId AND EntityTypeId = @EntityTypeId AND parentId Is NULL
	  	  ORDER BY DateCreated
	  OFFSET @offset ROWS
	  FETCH NEXT @PageSize Rows ONLY
END
GO
/****** Object:  StoredProcedure [dbo].[Comments_Select_ById]    Script Date: 12/2/2022 3:13:23 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author: <Joey Oh>
-- Create date: <07,28,22>
-- Description: <N/A>
-- Code Reviewer:

-- MODIFIED BY: author
-- MODIFIED DATE:12/1/2020
-- Code Reviewer:
-- Note:
-- =============================================
CREATE proc [dbo].[Comments_Select_ById]
			@Id int
as
/*
	Declare @Id int = 17
	Execute dbo.Comments_Select_ById
		@Id

	Select *
	FROM dbo.Comments
*/
BEGIN
	SELECT C.Id
		,C.[Subject]
		,C.[Text]
		,C.ParentId
		,C.EntityTypeId
		,E.[Name] as EntityType
		,C.EntityId
		,C.DateCreated
		,C.DateModified
		,C.CreatedBy
		,UP.FirstName
		,UP.LastName
		,UP.AvatarUrl
		,C.IsDeleted
	
	FROM dbo.Comments C 
		inner join dbo.Users U on c.CreatedBy = U.Id
		inner join dbo.EntityTypes E on C.EntityTypeId = E.Id
		inner join dbo.UserProfiles UP on UP.UserId = U.Id 
	  Where C.Id = @Id
END
GO
/****** Object:  StoredProcedure [dbo].[Comments_Select_ByIdV2]    Script Date: 12/2/2022 3:13:23 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author: <Joey Oh>
-- Create date: <07,28,22>
-- Description: <N/A>
-- Code Reviewer:

-- MODIFIED BY: author
-- MODIFIED DATE:12/1/2020
-- Code Reviewer:
-- Note:
-- =============================================
CREATE   proc [dbo].[Comments_Select_ByIdV2]
			@Id int
as
/*
	Declare @Id int = 17
	Execute dbo.Comments_Select_ById
		@Id

	Select *
	FROM dbo.Comments
*/
BEGIN
	SELECT C.Id
		,C.[Subject]
		,C.[Text]
		,C.ParentId
		,C.EntityTypeId
		,E.[Name] as EntityType
		,C.EntityId
		,C.DateCreated
		,C.DateModified
		,C.CreatedBy
		,UP.FirstName
		,UP.LastName
		,UP.AvatarUrl
		,C.IsDeleted
		,Replies = (Select *
				from dbo.Comments
				where ParentId = @Id
				For JSON AUTO)
	
	FROM dbo.Comments C 
		inner join dbo.Users U on c.CreatedBy = U.Id
		inner join dbo.EntityTypes E on C.EntityTypeId = E.Id
		inner join dbo.UserProfiles UP on UP.UserId = U.Id 
	  Where C.Id = @Id
END
GO
/****** Object:  StoredProcedure [dbo].[Comments_Select_ByParentId]    Script Date: 12/2/2022 3:13:23 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE proc [dbo].[Comments_Select_ByParentId]
						@ParentId int
as 
/*

select * from dbo.Comments
	DECLARE @ParentId int = 57

	Execute dbo.Comments_Select_ByParentId
		@ParentId
*/

BEGIN
	SELECT C.Id
		,C.[Subject]
		,C.[Text]
		,C.ParentId
		,C.EntityTypeId
		,E.[Name] as EntityType
		,C.EntityId
		,C.DateCreated
		,C.DateModified
		,C.CreatedBy
		,UP.FirstName
		,UP.LastName
		,UP.AvatarUrl
		,C.IsDeleted
	FROM dbo.Comments C 
		inner join dbo.Users U on c.CreatedBy = U.Id
		inner join dbo.EntityTypes E on C.EntityTypeId = E.Id
		inner join dbo.UserProfiles UP on UP.UserId = U.Id 
	  Where ParentId = @ParentId
END
GO
/****** Object:  StoredProcedure [dbo].[Comments_Select_Replies]    Script Date: 12/2/2022 3:13:23 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE proc [dbo].[Comments_Select_Replies]
	        @PageIndex int 
			,@PageSize int
as 
/*
	Declare
        @PageIndex int = 0
        ,@PageSize int = 10
	Execute [dbo].[Comments_Select_Replies] @PageIndex, @PageSize
*/

BEGIN
	Declare @offset int = @PageIndex * @PageSize

	SELECT C.Id
		,C.[Subject]
		,C.[Text]
		,C.ParentId
		,C.EntityTypeId
		,E.[Name] as EntityType
		,C.EntityId
		,C.DateCreated
		,C.DateModified
		,C.CreatedBy
		,UP.FirstName
		,UP.LastName
		,UP.AvatarUrl
		,C.IsDeleted
		,TotalCount = COUNT(1) OVER()
	FROM dbo.Comments C 
		inner join dbo.Users U on c.CreatedBy = U.Id
		inner join dbo.EntityTypes E on C.EntityTypeId = E.Id
		inner join dbo.UserProfiles UP on UP.UserId = U.Id 
	WHERE UP.UserId = C.CreatedBy AND C.ParentId is not null
	ORDER BY C.Id
	OFFSET @offset ROWS
	FETCH NEXT @PageSize Rows ONLY
END
GO
/****** Object:  StoredProcedure [dbo].[Comments_SelectAll]    Script Date: 12/2/2022 3:13:23 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author: <Joey Oh>
-- Create date: <07,28,22>
-- Description: <N/A>
-- Code Reviewer:

-- MODIFIED BY: author
-- MODIFIED DATE:12/1/2020
-- Code Reviewer:
-- Note:
-- =============================================
CREATE proc [dbo].[Comments_SelectAll]
        @PageIndex int 
        ,@PageSize int
as 
/*
	Declare
        @PageIndex int = 0
        ,@PageSize int = 10
	Execute dbo.comments_SelectAll @PageIndex, @PageSize

	Select * from dbo.Comments
*/
BEGIN
	Declare @offset int = @PageIndex * @PageSize

	SELECT C.Id
		,C.[Subject]
		,C.[Text]
		,C.ParentId
		,C.EntityTypeId
		,E.[Name] as EntityType
		,C.EntityId
		,C.DateCreated
		,C.DateModified
		,C.CreatedBy
		,UP.FirstName
		,UP.LastName
		,UP.AvatarUrl
		,C.IsDeleted
		,Replies = (Select *
					FROM dbo.Comments 
				where ParentId = C.Id
				order by c.DateCreated
				For JSON AUTO)
		,TotalCount = COUNT(1) OVER()
	FROM dbo.Comments C 
		inner join dbo.Users U on c.CreatedBy = U.Id
		inner join dbo.EntityTypes E on C.EntityTypeId = E.Id
		inner join dbo.UserProfiles UP on UP.UserId = U.Id 
	WHERE UP.UserId = C.CreatedBy AND C.ParentId IS NULL
	ORDER BY C.Id
	OFFSET @offset ROWS
	FETCH NEXT @PageSize Rows ONLY
END
GO
/****** Object:  StoredProcedure [dbo].[Comments_SelectAllV2]    Script Date: 12/2/2022 3:13:23 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author: <Joey Oh>
-- Create date: <08,25,22>
-- Description: <N/A>
-- Code Reviewer:

-- MODIFIED BY: author
-- MODIFIED DATE:12/1/2020
-- Code Reviewer:
-- Note:
-- =============================================
CREATE proc [dbo].[Comments_SelectAllV2]
        @PageIndex int 
        ,@PageSize int
as 
/*
	Declare
        @PageIndex int = 0
        ,@PageSize int = 10
	Execute dbo.comments_SelectAllV2 @PageIndex, @PageSize

	Select * from dbo.Comments
*/
BEGIN
	Declare @offset int = @PageIndex * @PageSize

	SELECT C.Id
		,C.[Subject]
		,C.[Text]
		,C.ParentId
		,C.EntityTypeId
		,C.EntityId
		,C.DateCreated
		,C.DateModified
		,C.CreatedBy
		,UP.FirstName
		,UP.LastName
		,UP.AvatarUrl
		,C.IsDeleted
		,Replies = (Select CM.Id
					,CM.[Subject]
					,CM.[Text]
					,CM.ParentId
					,CM.EntityTypeId
					,CM.EntityId
					,CM.DateCreated
					,CM.DateModified
					,CM.CreatedBy
					,UPP.FirstName 
					,UPP.LastName
					,UPP.AvatarUrl
					,CM.IsDeleted
					FROM dbo.Comments CM
					inner join dbo.UserProfiles UPP on CM.CreatedBy = UPP.UserId
					where UP.UserId = C.CreatedBy and CM.ParentId = C.Id 
					order by c.DateCreated
					For JSON PATH)
		,TotalCount = COUNT(1) OVER()
	FROM dbo.Comments C 
		inner join dbo.UserProfiles UP on c.CreatedBy = UP.UserId
	WHERE UP.UserId = C.CreatedBy AND C.ParentId IS NULL
	ORDER BY C.Id
	OFFSET @offset ROWS
	FETCH NEXT @PageSize Rows ONLY
END
GO
/****** Object:  StoredProcedure [dbo].[Comments_Update]    Script Date: 12/2/2022 3:13:23 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author: <Joey Oh>
-- Create date: <07,28,22>
-- Description: <N/A>
-- Code Reviewer:

-- MODIFIED BY: author
-- MODIFIED DATE:12/1/2020
-- Code Reviewer:
-- Note:
-- =============================================
CREATE proc [dbo].[Comments_Update]
	@Id int
	,@Text nvarchar(3000)
	,@IsDeleted bit
	,@UserId int

as 
/*
	Declare @Id int = '132' 
		,@Text nvarchar(3000) = 'Updated3'
		,@IsDeleted bit = 'True'
		,@UserId int = '135'

	Select *
	From dbo.comments
	Where Id = @Id

	Execute dbo.Comments_Update
		@Id
		,@Text
		,@IsDeleted
		,@UserId

	Select * from dbo.comments
	Where Id = @Id
*/
BEGIN
	
	DECLARE @DateModified datetime2 = GETUTCDATE()

	UPDATE dbo.comments

	SET	[Text] = @Text 
	,[DateModified] = @DateModified
	,[IsDeleted] = @IsDeleted,
	[CreatedBy] = @UserId

	Where Id = @Id 
END
GO
/****** Object:  StoredProcedure [dbo].[Daily_GetUsers]    Script Date: 12/2/2022 3:13:23 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROC [dbo].[Daily_GetUsers] 
				@Id int 
					
AS 

/*
---- TEST CODE ---- 
DECLARE 
		@Id int = 1

EXECUTE [dbo].[Daily_GetUsers]
		@Id

*/
BEGIN 

		SELECT 
				L.DailyLink 

		
		FROM dbo.DailyUser as DU
		JOIN dbo.DailyLinks as L on DU.DailyLinkId = L.Id
		JOIN dbo.Users as U on DU.RecipientId = U.Id

		WHERE @Id = U.Id

END 
		
GO
/****** Object:  StoredProcedure [dbo].[Daily_Insert]    Script Date: 12/2/2022 3:13:23 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROC [dbo].[Daily_Insert] 
			@Id int OUTPUT, 
			@DailyLink nvarchar(255), 
			@CreatedBy int 

AS 

/*
----TEST CODE----
DECLARE @Id int = 0, 
		@DailyLink nvarchar(255) = 'For test pruposes', 
		@CreatedBy int = 1

EXECUTE [dbo].[Daily_Insert] 
		@Id, 
		@DailyLink, 
		@CreatedBy

SELECT *
FROM DailyLinks

----END TEST CODE----
*/

BEGIN 

INSERT INTO dbo.DailyLinks
			(
			DailyLink, 
			CreatedBy
			)

VALUES 
			(
			@DailyLink,
			@CreatedBy
			)

SET @Id = SCOPE_IDENTITY()

END 
GO
/****** Object:  StoredProcedure [dbo].[Daily_Select_Paginate]    Script Date: 12/2/2022 3:13:23 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
 CREATE PROC    [dbo].[Daily_Select_Paginate]
				@PageIndex int
				,@PageSize int
AS

/*

----TEST CODE----

DECLARE	@PageIndex int = 0,
		@PageSize int = 3

EXECUTE [dbo].[Daily_Select_Paginate]
		@PageIndex,
		@PageSize

----END TEST CODE----

*/

BEGIN

	Declare @offset int = @PageIndex * @PageSize

	SELECT 
			Id,
			DailyLink,
			CreatedBy, 
			DateCreated
	FROM dbo.DailyLinks 


	ORDER BY Id

	OFFSET @offSet Rows
	Fetch Next @PageSize Rows ONLY

END 
GO
/****** Object:  StoredProcedure [dbo].[DailyLink_SelectById]    Script Date: 12/2/2022 3:13:23 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROC [dbo].[DailyLink_SelectById] 
			@Id int 

AS 

BEGIN 

	SELECT 
			DailyLink

	FROM dbo.DailyLinks
	WHERE Id = @Id

END 
GO
/****** Object:  StoredProcedure [dbo].[DailyLinks_DeleteById]    Script Date: 12/2/2022 3:13:23 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROC [dbo].[DailyLinks_DeleteById]
			@Id int 


AS 

/*
----TEST CODE----

DECLARE @Id int = 2

EXECUTE [dbo].[DailyLinks_DeleteById] @Id

SELECT *
FROM dbo.DailyLinks

----END TEST CODE----
*/


BEGIN 

DELETE FROM dbo.DailyLinks 
WHERE Id = @Id 
END
GO
/****** Object:  StoredProcedure [dbo].[dbo.UserJobs_SelectById]    Script Date: 12/2/2022 3:13:23 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- Author: <Rebekah Hoskins>
-- Create date: <20220808>
-- Description: <Select job info based on UserId>
-- Code Reviewer:

-- MODIFIED BY: author
-- MODIFIED DATE:12/1/2020
-- Code Reviewer:
-- Note:

CREATE PROC [dbo].[dbo.UserJobs_SelectById]
	@UserId int

AS

/*-----TEST CODE-----

	DECLARE @UserId int = 22

	EXECUTE [dbo.UserJobs_SelectById]
		@UserId

-----END TEST CODE-----
*/

BEGIN 

	SELECT uj.[UserId]
			,uj.[JobId]
			,j.Title
			,o.Name as Organization
			,j.OrganizationId
	FROM [dbo].[UserJobs] AS uj INNER JOIN [dbo].[Jobs] AS j
			ON uj.JobId = j.Id
		INNER JOIN dbo.Organizations as o
			ON o.Id=j.OrganizationId
	WHERE @UserId = [UserId]

END 
GO
/****** Object:  StoredProcedure [dbo].[EdProgramTypes_Insert]    Script Date: 12/2/2022 3:13:23 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author: <Ledwin Batista>
-- Create date: <07/29/2022>
-- Description: <EdProgramTypes_Insert>
-- Code Reviewer:

-- MODIFIED BY: author
-- MODIFIED DATE:12/1/2020
-- Code Reviewer:
-- Note:
-- =============================================

CREATE PROC [dbo].[EdProgramTypes_Insert]
			@Id int
			,@Name nvarchar(30)

AS

/* -- TEST CODE --

	Declare
			 @Id int = 1
			,@Name nvarchar(30) = 'Masters'


	EXECUTE [dbo].[EdProgramTypes_Insert]
			 @Id
			,@Name


	Select * 
	from dbo.Education
	order by Id
			
	select * 
	from dbo.EdProgramTypes
		
*/ -- END TEST CODE --


BEGIN
	

	INSERT INTO [dbo].[EdProgramTypes] 
			([Id]
			,[Name])
		
		VALUES
			(@Id
			,@Name) 

END


GO
/****** Object:  StoredProcedure [dbo].[EdProgramTypes_SelectAll]    Script Date: 12/2/2022 3:13:23 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author: <Grant Lee>
-- Create date: <08/01/2022>
-- Description: <EdProgramTypes_SelectAll>
-- Code Reviewer: Morgan Frost 08/02/2022

-- MODIFIED BY: author
-- MODIFIED DATE: 8/16/2022
-- Code Reviewer: Joey Oh 8/16/2022
-- Note:
-- =============================================

CREATE proc [dbo].[EdProgramTypes_SelectAll]

as

/*------TEST CODE------


Execute [dbo].[EdProgramTypes_SelectAll]

*/

BEGIN

SELECT edTyp.[Id]
      ,edTyp.[Name]

  FROM [dbo].[EdProgramTypes] as edTyp
  ORDER BY edTyp.Id

END
GO
/****** Object:  StoredProcedure [dbo].[Education_Delete_ById]    Script Date: 12/2/2022 3:13:23 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

-- =============================================
-- Author: <Ledwin Batista>
-- Create date: <07/28/2022>
-- Description: <Education_Delete_ById>
-- Code Reviewer:

-- MODIFIED BY: author
-- MODIFIED DATE:12/1/2020
-- Code Reviewer:
-- Note:
-- =============================================

CREATE PROC [dbo].[Education_Delete_ById]
	@Id int

AS

/* -- TEST CODE --
	
	Declare @Id int = 1

	EXECUTE dbo.Education_Delete_ById @Id
	
*/ -- END TEST CODE --

BEGIN

	DELETE FROM dbo.Education
	WHERE @Id = Id

END
GO
/****** Object:  StoredProcedure [dbo].[Education_Insert]    Script Date: 12/2/2022 3:13:23 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

-- =============================================
-- Author: <Ledwin Batista>
-- Create date: <07/28/2022>
-- Description: <Education_Insert>
-- Code Reviewer:

-- MODIFIED BY: author
-- MODIFIED DATE:12/1/2020
-- Code Reviewer:
-- Note:
-- =============================================

CREATE PROC [dbo].[Education_Insert]
			 @InstitutionId int
			,@EdProgramTypeIdId int
			,@SpecializationTypeId int
			,@Description nvarchar(200)
			,@IsGraduated bit
			,@CreatedBy int
			,@ModifiedBy int
			,@Id int OUTPUT

AS

/* -- TEST CODE --

	DECLARE
			@InstitutionId int = 25
			,@EdProgramTypeIdId int = 14
			,@SpecializationTypeId int = 5
			,@Description nvarchar(200) = "Engineer"
			,@IsGraduated bit = 1
			,@CreatedBy int = 12
			,@ModifiedBy int = 12
			,@Id int = 0

	EXECUTE dbo.Education_Insert 
			@InstitutionId
			,@EdProgramTypeIdId
			,@SpecializationTypeId
			,@Description
			,@IsGraduated
			,@CreatedBy
			,@ModifiedBy
			,@Id 

*/ -- END TEST CODE --

BEGIN

	INSERT INTO [dbo].[Education]
			([InstitutionId]
			,[EdProgramTypeId]
			,[SpecializationTypeId]
			,[Description]
			,[IsGraduated]
			,[CreatedBy]
			,[ModifiedBy])

		 VALUES
			(@InstitutionId
			,@EdProgramTypeIdId
			,@SpecializationTypeId
			,@Description
			,@IsGraduated
			,@CreatedBy
			,@ModifiedBy)

		SET		@Id = SCOPE_IDENTITY()

END
GO
/****** Object:  StoredProcedure [dbo].[Education_Select_ByCreatedBy]    Script Date: 12/2/2022 3:13:23 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author: <Grant Lee>
-- Create date: <08/01/2022>
-- Description: <Education_Select_ByCreatedBy>
-- Code Reviewer: Morgan Frost 8/2/2022

-- MODIFIED BY: author
-- MODIFIED DATE: 08/01/2022
-- Code Reviewer:
-- Note:
-- =============================================

CREATE proc [dbo].[Education_Select_ByCreatedBy]
									 @PageIndex int
									,@PageSize int
									,@CreatedBy int

as

/* ----- TEST CODE -----

	Declare		 @PageIndex int = 0
				,@PageSize int = 2
				,@CreatedBy int = 4
	

	Execute [dbo].[Education_Select_ByCreatedBy] 
				 @PageIndex
				,@PageSize
				,@CreatedBy

*/ ----- END TEST CODE -----

BEGIN

Declare	   @offset int = @PageIndex * @PageSize

	SELECT   edu.[Id]
			,edu.[InstitutionId]
			,edu.[EdProgramTypeId]
			,edu.[SpecializationTypeId]
			,edu.[DateStart]
			,edu.[DateEnd]
			,edu.[Description]
			,edu.[IsGraduated]
			,edu.[CreatedBy]
			,TotalCount = COUNT(1) OVER()

	FROM [dbo].[Education] as edu

WHERE		edu.CreatedBy = @CreatedBy
ORDER BY	edu.Id
			OFFSET @offset ROWS
FETCH NEXT  @PageSize ROWS ONLY

END
GO
/****** Object:  StoredProcedure [dbo].[Education_Select_ById]    Script Date: 12/2/2022 3:13:23 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author: <Grant Lee>
-- Create date: <08/01/2022>
-- Description: <Education_Select_ById>
-- Code Reviewer: Morgan Frost 8/2/2022

-- MODIFIED BY: author
-- MODIFIED DATE:08/01/2022
-- Code Reviewer: 
-- Note:
-- =============================================

CREATE proc [dbo].[Education_Select_ById]
								@Id int
as

/*
	Declare @Id int = 1
	Execute dbo.Education_Select_ById
								  @Id

	Select *
	FROM dbo.Education
*/

BEGIN

SELECT edu.[Id]
      ,edu.[InstitutionId]
      ,edu.[EdProgramTypeId]
      ,edu.[SpecializationTypeId]
      ,edu.[DateStart]
      ,edu.[DateEnd]
      ,edu.[Description]
      ,edu.[IsGraduated]
      ,edu.[CreatedBy]
      ,edu.[ModifiedBy]
      ,edu.[DateCreated]
      ,edu.[DateModified]
  FROM [dbo].[Education] as edu
  WHERE edu.Id = @Id

END
GO
/****** Object:  StoredProcedure [dbo].[Education_SelectAll]    Script Date: 12/2/2022 3:13:23 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author: <Grant Lee>
-- Create date: <08/01/2022>
-- Description: <Education_SelectAll>
-- Code Reviewer: Morgan Frost 8/2/2022

-- MODIFIED BY: author
-- MODIFIED DATE: 08/01/2022
-- Code Reviewer:
-- Note:
-- =============================================

CREATE proc [dbo].[Education_SelectAll]
							@PageIndex int
						   ,@PageSize int
as

/*------TEST CODE------

Declare @PageIndex int = 0
	   ,@PageSize int = 1

Execute [dbo].[Education_SelectAll]
						  @PageIndex
						 ,@PageSize

*/

BEGIN

Declare @offset int = @PageIndex * @PageSize

SELECT edu.[Id]
      ,edu.[InstitutionId]
      ,edu.[EdProgramTypeId]
      ,edu.[SpecializationTypeId]
      ,edu.[DateStart]
      ,edu.[DateEnd]
      ,edu.[Description]
      ,edu.[CreatedBy]
      ,edu.[ModifiedBy]
      ,edu.[DateCreated]
      ,edu.[DateModified]
	  ,TotalCount = COUNT(1) OVER()

  FROM [dbo].[Education] as edu
  ORDER BY edu.Id

  OFFSET @offSet Rows
  FETCH NEXT @PageSize ROWS ONLY

END
GO
/****** Object:  StoredProcedure [dbo].[Education_Update]    Script Date: 12/2/2022 3:13:23 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

-- =============================================
-- Author: <Ledwin Batista>
-- Create date: <07/28/2022>
-- Description: <Education_Update>
-- Code Reviewer:

-- MODIFIED BY: author
-- MODIFIED DATE:12/1/2020
-- Code Reviewer:
-- Note:
-- =============================================

CREATE PROC [dbo].[Education_Update]
			@InstitutionId int
			,@EdProgramTypeIdId int
			,@SpecializationTypeId int
			,@Description nvarchar(200)
			,@IsGraduated bit
			,@CreatedBy int
			,@ModifiedBy int
			,@Id int
AS

BEGIN

	UPDATE [dbo].[Education]

	   SET [InstitutionId] = @InstitutionId
		  ,[EdProgramTypeId] = @EdProgramTypeIdId
		  ,[SpecializationTypeId] = @SpecializationTypeId
		  ,[Description] = @Description
		  ,[IsGraduated] = @IsGraduated
		  ,[CreatedBy] = @CreatedBy
		  ,[ModifiedBy] = @ModifiedBy
		  ,[DateModified] = GETUTCDATE()

	 WHERE @Id = Id

END


GO
/****** Object:  StoredProcedure [dbo].[EmploymentTypes_SelectAll]    Script Date: 12/2/2022 3:13:23 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author: <Grant Lee>
-- Create date: <08/10/2022>
-- Description: <EmploymentTypes_SelectAll>
-- Code Reviewer:

-- MODIFIED BY: author
-- MODIFIED DATE: 8/16/2022
-- Code Reviewer: Joey Oh 8/16/2022
-- Note:
-- =============================================

CREATE proc [dbo].[EmploymentTypes_SelectAll]

/*------TEST CODE------

Execute dbo.EmploymentTypes_SelectAll

*/
 

as

BEGIN

	SELECT 
			Id
			,[Name]

	
	FROM dbo.EmploymentTypes

END
GO
/****** Object:  StoredProcedure [dbo].[EntityTypes_SelectAll]    Script Date: 12/2/2022 3:13:23 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE proc [dbo].[EntityTypes_SelectAll]

As
/*...TEST CODE...

execute dbo.EntityTypes_SelectAll

*/

BEGIN

	SELECT Id
			,Name 
	FROM dbo.EntityTypes

END
GO
/****** Object:  StoredProcedure [dbo].[Events_Delete]    Script Date: 12/2/2022 3:13:23 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author: <Victor Gonzalez>
-- Create date: <09/16/2022>
-- Description: <Events_Delete>
-- Code Reviewer:

-- MODIFIED BY: N/A
-- MODIFIED DATE:N/A
-- Code Reviewer:
-- Note:
-- =============================================
CREATE PROC [dbo].[Events_Delete]
						@Id int

/*-----------Test Code-------------

DECLARE @Id int = 1;

SELECT *
FROM [dbo].[Events]
WHERE Id = @Id

EXEC [dbo].[Events_Delete]
					@Id

SELECT *
FROM [dbo].[Events]
WHERE Id = @Id

*/

AS

BEGIN
	DELETE FROM [dbo].[Events]
		WHERE Id = @Id
END


GO
/****** Object:  StoredProcedure [dbo].[Events_Insert]    Script Date: 12/2/2022 3:13:23 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author: <Victor Gonzalez>
-- Create date: <09/16/2022>
-- Description: <Events_Insert>
-- Code Reviewer:

-- MODIFIED BY: N/A
-- MODIFIED DATE:N/A
-- Code Reviewer:
-- Note:
-- =============================================
CREATE PROC [dbo].[Events_Insert]
					@EventTypeId int
					,@LocationId int
					,@OrganizationId int
					,@Title nvarchar(128)
					,@Description nvarchar(500)
					,@ImageUrl nvarchar(255)
					,@IsMultiDay bit
					,@UserId int
					,@Id int OUTPUT

/*---------------Test Code--------------


DECLARE @EventTypeId int = 1
		,@LocationId int = 1
		,@OrganizationId int = 1
		,@Title nvarchar(128) = 'test title'
		,@Description nvarchar(500) = 'test description'
		,@ImageUrl nvarchar(255)='image url'
		,@IsMultiDay bit = 0
		,@UserId int = 4
		,@Id int = 0
			

	EXEC [dbo].[Events_Insert]
					@EventTypeId
					,@LocationId
					,@OrganizationId 
					,@Title
					,@Description
					,@ImageUrl 
					,@IsMultiDay 
					,@UserId
					,@Id OUTPUT

	Select *
	From [dbo].[Events]
	Where Id = @Id
	
*/

AS

BEGIN


INSERT INTO [dbo].[Events]
           ([EventTypeId]
		   ,[LocationId]
		   ,[OrganizationId]
           ,[Title]
           ,[Description]
           ,[ImageUrl]
		   ,[IsMultiDay]
		   ,[CreatedBy]
		   ,[ModifiedBy]
		   ,[DateCreated]
           ,[DateModified])
     VALUES
           (@EventTypeId
		   ,@LocationId
		   ,@OrganizationId
			,@Title 
			,@Description 
			,@ImageUrl
			,@IsMultiDay
			,@UserId
			,@UserId
			,GETUTCDATE()
			,GETUTCDATE())

		SET @Id = SCOPE_IDENTITY()
					

END
GO
/****** Object:  StoredProcedure [dbo].[Events_InsertV2]    Script Date: 12/2/2022 3:13:23 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author: <Victor Gonzalez>
-- Create date: <09/16/2022>
-- Description: <Events_Insert>
-- Code Reviewer:

-- MODIFIED BY: N/A
-- MODIFIED DATE:N/A
-- Code Reviewer:
-- Note:
-- =============================================
CREATE PROC [dbo].[Events_InsertV2]
					@EventTypeId int
					,@LocationTypeId int
					,@OrganizationId int
					,@Title nvarchar(128)
					,@Description nvarchar(500)
					,@ImageUrl nvarchar(255)
					,@IsMultiDay bit
					,@LineOne nvarchar(255)
			        ,@LineTwo nvarchar(255)
			        ,@City nvarchar(225)
			        ,@Zip nvarchar(50)
		 	        ,@StateId int
			        ,@Latitude float 
          			,@Longitude float
					,@CreatedBy int
					,@Id int OUTPUT

/*---------------Test Code--------------


DECLARE @EventTypeId int = 1
		,@LocationId int = 1
		,@OrganizationId int = 1
		,@Title nvarchar(128) = 'test title'
		,@Description nvarchar(500) = 'test description'
		,@ImageUrl nvarchar(255)='image url'
		,@IsMultiDay bit = 0
		,@UserId int = 4
		,@Id int = 0
			

	EXEC [dbo].[Events_Insert]
					@EventTypeId
					,@LocationId
					,@OrganizationId 
					,@Title
					,@Description
					,@ImageUrl 
					,@IsMultiDay 
					,@UserId
					,@Id OUTPUT

	Select *
	From [dbo].[Events]
	Where Id = @Id
	
*/

AS

BEGIN

Declare @LocationId int
				,@DateModified datetime2 = GETUTCDATE()

INSERT INTO [dbo].[Locations]
				   ([LocationTypeId]
				   ,[LineOne]
				   ,[LineTwo]
				   ,[City]
				   ,[Zip]
				   ,[StateId]
				   ,[Latitude]
				   ,[Longitude]
				   ,[CreatedBy])
			 VALUES
				   (@LocationTypeId
				   ,@LineOne
				   ,@LineTwo
				   ,@City
				   ,@Zip
				   ,@StateId
				   ,@Latitude
				   ,@Longitude
				   ,@CreatedBy)

		SET @LocationId = SCOPE_IDENTITY()

INSERT INTO [dbo].[Events]
           ([EventTypeId]
		   ,[LocationId]
		   ,[OrganizationId]
           ,[Title]
           ,[Description]
           ,[ImageUrl]
		   ,[IsMultiDay]
		   ,[CreatedBy]
		   ,[ModifiedBy]
		   ,[DateCreated]
           ,[DateModified])
     VALUES
           (@EventTypeId
		   ,@LocationId
		   ,@OrganizationId
			,@Title 
			,@Description 
			,@ImageUrl
			,@IsMultiDay
			,@CreatedBy
			,@CreatedBy
			,@DateModified
			,@DateModified)

		SET @Id = SCOPE_IDENTITY()
					

END
GO
/****** Object:  StoredProcedure [dbo].[Events_Search_Pagination]    Script Date: 12/2/2022 3:13:23 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- Author: <Victor Gonzalez>
-- Create date: <09/16/2022>

-- Description: <Events_search pagination>

-- Code Reviewer: N/A

-- =============================================

CREATE PROC [dbo].[Events_Search_Pagination]   @PageIndex int ,@PageSize int ,@Query nvarchar(50)
	
AS
/*-----TEST CODE-----

Declare @PageIndex int = 0
		,@PageSize int = 10
		,@Query nvarchar(50) = 'hiring'



EXECUTE [dbo].[Events_Search_Pagination ]
		@PageIndex
		,@PageSize
		,@Query

	

		EXECUTE dbo.Events_SelectAll @PageIndex 
		,@PageSize

-----END TEST CODE-----
*/


BEGIN

Declare @Offset int = @PageIndex * @PageSize
	
		SELECT e.[Id] AS [EventId]
			  ,e.[EventTypeId] AS [EventType] 
			  ,et.[Name] AS [EventTypeName]
			  ,e.[Title] AS [EventTitle]
			  ,e.[Description]AS [EventDescription]
			  ,e.[LocationId] AS [LocationId]
			  ,e.[ImageUrl] AS [ImageUrl]
			  ,l.[LocationTypeId] AS [LocationTypeId]
			  ,lt.[Name]AS [LocationTypeName]
			  ,l.[LineOne]
			  ,l.[LineTwo]
			  ,l.[City]
			  ,l.[Zip]
			  ,l.[StateId] As [StateId]
			  ,s.[Name]AS [StateName]
			  ,s.[Code]AS [StateCode]
			  ,l.[Latitude]
			  ,l.[Longitude]
			  ,e.[OrganizationId] AS [Organization]
			  ,o.[OrganizationTypeId] AS [OrganizationType] 
			  ,ot.[Name]AS [OrgTypeName]
			  ,o.[Name]AS [OrgName]
			  ,o.[Headline]AS [OrgHeadline]
			  ,o.[Description]AS [OrgDescription]
			  ,o.[Logo]
			  ,o.[Phone]
			  ,o.[SiteUrl]
			  ,up.[Id] AS [UserId]
			  ,e.[CreatedBy]
			  ,up.FirstName
			  ,up.LastName
			  ,up.Mi
			  ,up.AvatarUrl
			  ,e.[DateCreated]
			  ,e.[DateModified]
			  ,TotalCount = COUNT(1) OVER()


			  

		FROM [dbo].[Events] AS e 
		left outer join dbo.Organizations as o on o.Id = e.OrganizationId
		left outer join dbo.Locations as l on l.id = e.LocationId
		inner join dbo.EventTypes as et on et.Id = e.EventTypeId
		left outer join dbo.LocationTypes as lt on lt.Id = l.LocationTypeId
		left outer join dbo.OrganizationTypes as ot on ot.Id = o.OrganizationTypeId
		left outer join dbo.UserProfiles as up on up.UserId = e.CreatedBy
		left outer join dbo.States as s on s.Id = l.StateId
	
		WHERE	e.Title LIKE  '%' + @Query + '%'
		OR  e.Description LIKE '%' + @Query + '%'
		OR  l.Zip LIKE '%' + @Query + '%'
		OR  l.City LIKE '%' + @Query + '%'

		ORDER BY e.Id

		OFFSET @Offset ROWS
		FETCH NEXT @PageSize ROWS ONLY

	END
GO
/****** Object:  StoredProcedure [dbo].[Events_Search_Pagination_By_Location]    Script Date: 12/2/2022 3:13:23 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- Author: <Victor Gonzalez>
-- Create date: <09/16/2022>

-- Description: <Events_search pagination by location>

-- Code Reviewer: N/A

-- =============================================

CREATE PROC [dbo].[Events_Search_Pagination_By_Location]   @PageIndex int ,@PageSize int ,@Query nvarchar(50)
	
AS
/*-----TEST CODE-----

Declare @PageIndex int = 0
		,@PageSize int = 10
		,@Query nvarchar(50) = '1234'



EXECUTE [dbo].[Events_Search_Pagination_By_Location]
		@PageIndex
		,@PageSize
		,@Query

	

		EXECUTE dbo.Events_SelectAll @PageIndex 
		,@PageSize

-----END TEST CODE-----
*/


BEGIN

Declare @Offset int = @PageIndex * @PageSize
	
		SELECT e.[Id]
			  ,e.[EventTypeId] AS [EventType] 
			  ,et.[Name]
			  ,e.[Title]
			  ,e.[Description]
			  ,e.[LocationId] AS [Location]
			  ,l.[LocationTypeId] AS [LocationType]
			  ,e.[ImageUrl]
			  ,lt.[Name]
			  ,l.[LineOne]
			  ,l.[LineTwo]
			  ,l.[City]
			  ,l.[Zip]
			  ,l.[StateId] As [StateId]
			  ,s.[Name]
			  ,s.[Code]
			  ,l.[Latitude]
			  ,l.[Longitude]
			  ,e.[OrganizationId] AS [Organization]
			  ,o.[OrganizationTypeId] AS [OrganizationType] 
			  ,ot.[Name]
			  ,o.[Name]
			  ,o.[Headline]
			  ,o.[Description]
			  ,o.[Logo]
			  ,o.[Phone]
			  ,o.[SiteUrl]
			  ,up.[Id]
			  ,e.[CreatedBy]
			  ,up.FirstName
			  ,up.LastName
			  ,up.Mi
			  ,up.AvatarUrl
			  ,e.[DateCreated]
			  ,e.[DateModified]
			  ,TotalCount = COUNT(1) OVER()


			  

		FROM [dbo].[Events] AS e 
		left outer join dbo.Organizations as o on o.Id = e.OrganizationId
inner join dbo.Locations as l on l.id = e.LocationId
inner join dbo.EventTypes as et on et.Id = e.EventTypeId
left outer join dbo.LocationTypes as lt on lt.Id = l.LocationTypeId
left outer join dbo.OrganizationTypes as ot on ot.Id = o.OrganizationTypeId
left outer join dbo.UserProfiles as up on up.UserId = e.CreatedBy
left outer join dbo.States as s on s.Id = l.StateId

	
		WHERE	l.Zip LIKE  '%' + @Query + '%'

		ORDER BY e.Id

		OFFSET @Offset ROWS
		FETCH NEXT @PageSize ROWS ONLY

	END
GO
/****** Object:  StoredProcedure [dbo].[Events_Search_Pagination_With_Location_Radius]    Script Date: 12/2/2022 3:13:23 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- Author: <Victor Gonzalez>
-- Create date: <09/16/2022>
-- Description: <Events_SearchPagination with location radius>
-- MODIFIED BY: <>
-- MODIFIED DATE:<>
-- Code Reviewer:
-- Note:  

-- =============================================
CREATE PROC [dbo].[Events_Search_Pagination_With_Location_Radius]
@PageIndex int
				,@PageSize int
				,@Query nvarchar(50)
				,@Lat decimal(10,7)
				,@Long decimal(10,7)
				,@Distance int
AS
/*-----TEST CODE-----

Declare @PageIndex int = 0
				,@PageSize int = 10
				,@Query nvarchar(50) = 'engineer'
				,@Lat decimal(10,7) = 32.7269669
				,@Long decimal(10,7) = -118.1647094
				,@Distance int = 100



EXECUTE [dbo].[Events_Search_Pagination_With_Location_Radius]
		@PageIndex
				,@PageSize
				,@Query
				,@Lat
				,@Long
				,@Distance

	

		

-----END TEST CODE-----
*/


BEGIN

Declare @Offset int = @PageIndex * @PageSize
		 ;WITH AllRecords AS (
		SELECT e.[Id] AS [EventId]
			  ,e.[EventTypeId] AS [EventType] 
			  ,et.[Name] AS [EventTypeName]
			  ,e.[Title] AS [EventTitle]
			  ,e.[Description]AS [EventDescription]
			  ,e.[LocationId] AS [LocationId]
			  ,l.[LocationTypeId] AS [LocationTypeId]
			  ,e.[ImageUrl] AS [ImageUrl]
			  ,lt.[Name]AS [LocationTypeName]
			  ,l.[LineOne]
			  ,l.[LineTwo]
			  ,l.[City]
			  ,l.[Zip]
			  ,l.[StateId] As [StateId]
			  ,s.[Name]AS [StateName]
			  ,s.[Code]AS [StateCode]
			  ,l.[Latitude]
			  ,l.[Longitude]
			  ,e.[OrganizationId] AS [Organization]
			  ,o.[OrganizationTypeId] AS [OrganizationType] 
			  ,ot.[Name]AS [OrgTypeName]
			  ,o.[Name]AS [OrgName]
			  ,o.[Headline]AS [OrgHeadline]
			  ,o.[Description]AS [OrgDescription]
			  ,o.[Logo]
			  ,o.[Phone]
			  ,o.[SiteUrl]
			  ,up.[Id] AS [UserId]
			  ,e.[CreatedBy]
			  ,up.FirstName
			  ,up.LastName
			  ,up.Mi
			  ,up.AvatarUrl
			  ,e.[DateCreated]
			  ,e.[DateModified]
			  ,TotalCount = COUNT(1) OVER()
			  ,( 3959 * acos( cos( radians(@lat) ) * cos( radians( l.Latitude ) ) * cos( radians(l.Longitude ) - radians(@Long) ) + sin( radians(@Lat) ) * sin( radians(l.Latitude) ) ) ) AS Distance
		

			  

		FROM [dbo].[Events] AS e 
		left outer join dbo.Organizations as o on o.Id = e.OrganizationId
inner join dbo.Locations as l on l.id = e.LocationId
inner join dbo.EventTypes as et on et.Id = e.EventTypeId
left outer join dbo.LocationTypes as lt on lt.Id = l.LocationTypeId
left outer join dbo.OrganizationTypes as ot on ot.Id = o.OrganizationTypeId
left outer join dbo.UserProfiles as up on up.UserId = e.CreatedBy
left outer join dbo.States as s on s.Id = l.StateId

ORDER BY e.Id

		OFFSET @Offset ROWS
		FETCH NEXT @PageSize ROWS ONLY
		)
	, filteredRecords as (
			SELECT  *
			FROM    AllRecords
			WHERE   distance <= @distance  
			AND	(AllRecords.EventTitle LIKE  '%' + @Query + '%'
			OR AllRecords.EventDescription LIKE '%' + @Query + '%')
			
			)
	SELECT
			filteredRecords.[EventId]
			,filteredRecords.[EventType]
			,filteredRecords.[EventTypeName]
			,filteredRecords.[EventTitle]
			,filteredRecords.[EventDescription]
			,filteredRecords.[ImageUrl]
			,filteredRecords.[LocationId]
			,filteredRecords.[LocationTypeId]
			,filteredRecords.[LocationTypeName]
			,filteredRecords.[LineOne]
			,filteredRecords.[LineTwo]
			,filteredRecords.[City]
			,filteredRecords.[Zip]
			,filteredRecords.[StateId]
			,filteredRecords.[StateName]
			,filteredRecords.[StateCode]
			,filteredRecords.[Latitude]
			,filteredRecords.[Longitude]
			,filteredRecords.[Organization]
			,filteredRecords.[OrganizationType]
			,filteredRecords.[OrgTypeName]
			,filteredRecords.[OrgName]
			,filteredRecords.[OrgHeadline]
			,filteredRecords.[OrgDescription]
			,filteredRecords.[Logo]
			,filteredRecords.[Phone]
			,filteredRecords.[SiteUrl]
			,filteredRecords.[UserId]
			,filteredRecords.[CreatedBy]
			,filteredRecords.[FirstName]
			,filteredRecords.[LastName]
			,filteredRecords.[Mi]
			,filteredRecords.[AvatarUrl]
			,filteredRecords.[DateCreated]
			,filteredRecords.[DateModified]
			,filteredRecords.[Distance]
			,TotalCount = COUNT(1) OVER()
    FROM    filteredRecords
    ORDER BY distance ASC;		
	END
GO
/****** Object:  StoredProcedure [dbo].[Events_Select_ByOrganization]    Script Date: 12/2/2022 3:13:23 PM ******/
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
/****** Object:  StoredProcedure [dbo].[Events_SelectAll]    Script Date: 12/2/2022 3:13:23 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
	-- =============================================
-- Author: <Victor Gonzalez>
-- Create date: <09/16/2022>

-- Description: <Events_SelectAll brings in all relevant data belonging to events and includes
-- joined tables such as Locations and Organizations to bring a comprehenensive amount of
-- information >

-- Code Reviewer: N/A

-- =============================================
	
	
	CREATE PROC [dbo].[Events_SelectAll]

				 @PageIndex int
				,@PageSize int

	AS

	/*


				DECLARE @PageIndex int = 0
				,@PageSize int = 50

		Execute dbo.Events_SelectAll 
												@PageIndex
												,@PageSize

	*/

	BEGIN

		Declare @Offset int = @PageIndex * @PageSize
		
		SELECT e.[Id] AS [EventId]
			  ,e.[EventTypeId] AS [EventType] 
			  ,et.[Name] AS [EventTypeName]
			  ,e.[Title] AS [EventTitle]
			  ,e.[Description]AS [EventDescription]
			  ,e.[LocationId] AS [LocationId]
			  ,e.[ImageUrl] AS [ImageUrl]
			  ,l.[LocationTypeId] AS [LocationTypeId]
			  ,lt.[Name]AS [LocationTypeName]
			  ,l.[LineOne]
			  ,l.[LineTwo]
			  ,l.[City]
			  ,l.[Zip]
			  ,l.[StateId] As [StateId]
			  ,s.[Name]AS [StateName]
			  ,s.[Code]AS [StateCode]
			  ,l.[Latitude]
			  ,l.[Longitude]
			  ,e.[OrganizationId] AS [Organization]
			  ,o.[OrganizationTypeId] AS [OrganizationType] 
			  ,ot.[Name]AS [OrgTypeName]
			  ,o.[Name]AS [OrgName]
			  ,o.[Headline]AS [OrgHeadline]
			  ,o.[Description]AS [OrgDescription]
			  ,o.[Logo]
			  ,o.[Phone]
			  ,o.[SiteUrl]
			  ,up.[Id] AS [UserId]
			  ,e.[CreatedBy]
			  ,up.FirstName
			  ,up.LastName
			  ,up.Mi
			  ,up.AvatarUrl
			  ,e.[DateCreated]
			  ,e.[DateModified]
			  ,TotalCount = COUNT(1) OVER()


			  

		FROM [dbo].[Events] AS e 
		left outer join dbo.Organizations as o on o.Id = e.OrganizationId
		left outer join dbo.Locations as l on l.id = e.LocationId
		inner join dbo.EventTypes as et on et.Id = e.EventTypeId
		left outer join dbo.LocationTypes as lt on lt.Id = l.LocationTypeId
		left outer join dbo.OrganizationTypes as ot on ot.Id = o.OrganizationTypeId
		left outer join dbo.UserProfiles as up on up.UserId = e.CreatedBy
		left outer join dbo.States as s on s.Id = l.StateId





		ORDER BY e.[Id]

		OFFSET @Offset ROWS
		FETCH NEXT @PageSize ROWS ONLY


	END
GO
/****** Object:  StoredProcedure [dbo].[Events_SelectByCreatedBy]    Script Date: 12/2/2022 3:13:23 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- Author: <Victor Gonzalez>
-- Create date: <09/16/2022>

-- Description: <Events_Select created by>

-- Code Reviewer: N/A

-- =============================================


CREATE PROC [dbo].[Events_SelectByCreatedBy]

				@CreatedBy int
				,@PageIndex int
				,@PageSize int

	AS

	/*

	Declare @CreatedBy int = 1
				,@PageIndex int = 0
				,@PageSize int = 50

		Execute dbo.Events_SelectByCreatedBy @CreatedBy
												,@PageIndex
												,@PageSize
											

	*/

BEGIN

Declare @Offset int = @PageIndex * @PageSize
	
		SELECT e.[Id]
			  ,e.[EventTypeId] AS [EventType] 
			  ,t.[Name]
			  ,e.[Title]
			  ,e.[Description]
			  ,e.[LocationId] AS [Location]
			  ,l.[LocationTypeId] AS [LocationType]
			  ,e.[ImageUrl]
			  ,lt.[Name]
			  ,l.[LineOne]
			  ,l.[LineTwo]
			  ,l.[City]
			  ,l.[Zip]
			  ,l.[StateId] As [StateId]
			  ,s.[Name]
			  ,s.[Code]
			  ,l.[Latitude]
			  ,l.[Longitude]
			  ,e.[OrganizationId] AS [Organization]
			  ,o.[OrganizationTypeId] AS [OrganizationType] 
			  ,ot.[Name]
			  ,o.[Name]
			  ,o.[Headline]
			  ,o.[Description]
			  ,o.[Logo]
			  ,o.[Phone]
			  ,o.[SiteUrl]
			  ,up.[Id]
			  ,e.[CreatedBy]
			  ,up.FirstName
			  ,up.LastName
			  ,up.Mi
			  ,up.AvatarUrl
			  ,e.[DateCreated]
			  ,e.[DateModified]
			  ,TotalCount = COUNT(1) OVER()


			  

		FROM [dbo].[Events] AS e INNER JOIN dbo.Locations as l
		ON l.LocationTypeId = e.LocationId INNER JOIN dbo.Organizations as o
		ON e.OrganizationId = o.OrganizationTypeId INNER JOIN dbo.States as s
		ON l.StateId = s.Id INNER JOIN dbo.EventTypes as t 
		ON e.EventTypeId = t.Id Inner join  dbo.UserProfiles as up 
		ON up.UserId = e.CreatedBy inner join dbo.LocationTypes as lt  
		ON lt.Id = l.LocationTypeId inner join dbo.OrganizationTypes as ot 
		ON ot.Id = e.OrganizationId

	
		WHERE e.CreatedBy = @CreatedBy

		ORDER BY e.CreatedBy

		OFFSET @Offset ROWS
		FETCH NEXT @PageSize ROWS ONLY

	END
GO
/****** Object:  StoredProcedure [dbo].[Events_SelectById]    Script Date: 12/2/2022 3:13:23 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- Author: <Victor Gonzalez>
-- Create date: <09/16/2022>

-- Description: <Events_Select by Id>

-- Code Reviewer: N/A

-- =============================================


CREATE PROC [dbo].[Events_SelectById]

				 @Id int

	AS

	/*


		DECLARE @Id int = 20

		Execute dbo.Events_SelectById
				@Id
											

	*/

BEGIN

	
		SELECT e.[Id] AS [EventId]
			  ,e.[EventTypeId] AS [EventType] 
			  ,et.[Name] AS [EventTypeName]
			  ,e.[Title] AS [EventTitle]
			  ,e.[Description]AS [EventDescription]
			  ,e.[LocationId] AS [LocationId]
			  ,e.[ImageUrl] AS [ImageUrl]
			  ,l.[LocationTypeId] AS [LocationTypeId]
			  ,lt.[Name]AS [LocationTypeName]
			  ,l.[LineOne]
			  ,l.[LineTwo]
			  ,l.[City]
			  ,l.[Zip]
			  ,l.[StateId] As [StateId]
			  ,s.[Name]AS [StateName]
			  ,s.[Code]AS [StateCode]
			  ,l.[Latitude]
			  ,l.[Longitude]
			  ,e.[OrganizationId] AS [Organization]
			  ,o.[OrganizationTypeId] AS [OrganizationType] 
			  ,ot.[Name]AS [OrgTypeName]
			  ,o.[Name]AS [OrgName]
			  ,o.[Headline]AS [OrgHeadline]
			  ,o.[Description]AS [OrgDescription]
			  ,o.[Logo]
			  ,o.[Phone]
			  ,o.[SiteUrl]
			  ,up.[Id] AS [UserId]
			  ,e.[CreatedBy]
			  ,up.FirstName
			  ,up.LastName
			  ,up.Mi
			  ,up.AvatarUrl
			  ,e.[DateCreated]
			  ,e.[DateModified]
			  ,TotalCount = COUNT(1) OVER()


			  

		FROM [dbo].[Events] AS e 
		left outer join dbo.Organizations as o on o.Id = e.OrganizationId
		left outer join dbo.Locations as l on l.id = e.LocationId
		inner join dbo.EventTypes as et on et.Id = e.EventTypeId
		left outer join dbo.LocationTypes as lt on lt.Id = l.LocationTypeId
		left outer join dbo.OrganizationTypes as ot on ot.Id = o.OrganizationTypeId
		left outer join dbo.UserProfiles as up on up.UserId = e.CreatedBy
		left outer join dbo.States as s on s.Id = l.StateId

	WHERE e.Id = @Id

	END
GO
/****** Object:  StoredProcedure [dbo].[Events_Update]    Script Date: 12/2/2022 3:13:23 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- Author: <Victor Gonzalez>
-- Create date: <09/16/2022>

-- Description: <Events_Update>

-- Code Reviewer: N/A

-- =============================================

CREATE PROC [dbo].[Events_Update]
					@EventTypeId int
					,@LocationId int
					,@OrganizationId int
					,@Title nvarchar(128)
					,@Description nvarchar(500)
					,@ImageUrl nvarchar(255)
					,@IsMultiDay bit
					,@UserId int
					,@Id int 

/*---------------Test Code--------------
DECLARE @EventTypeId int = 111
		,@LocationId int = 222
		,@OrganizationId int = 333
		,@Title nvarchar(128) = 'test title'
		,@Description nvarchar(500) = 'test description'
		,@ImageUrl nvarchar(255)='image url'
		,@IsMultiDay bit = 0
		,@UserId int = 999
		,@Id int = 1
			
SELECT *
FROM [dbo].[Events]
WHERE Id = @Id

	EXEC [dbo].[Events_Update]
					@EventTypeId
					,@LocationId
					,@OrganizationId 
					,@Title
					,@Description
					,@ImageUrl 
					,@IsMultiDay 
					,@UserId 
					,@Id 

	Select *
	From [dbo].[Events]
	Where Id = @Id

*/

AS

BEGIN

UPDATE [dbo].[Events]
   SET		[EventTypeId]= @EventTypeId
		   ,[LocationId]= @LocationId
		   ,[OrganizationId]= @OrganizationId
           ,[Title]=@Title
           ,[Description]= @Description
           ,[ImageUrl]= @ImageUrl
		   ,[IsMultiDay]= @IsMultiDay
		   ,[ModifiedBy] = @UserId
           ,[DateModified] = GETUTCDATE()
 WHERE Id = @Id
					
END
GO
/****** Object:  StoredProcedure [dbo].[EventTypes_SelectAll]    Script Date: 12/2/2022 3:13:23 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Victor Gonzalez>
-- Create date: <09/16/2022>
-- Description:	<Select All EventTypes>
-- Code Reviewer:

-- MODIFIED BY: n/a
-- MODIFIED DATE: n/a
-- Code Reviewer: 
-- Note: 
-- =============================================

CREATE PROC [dbo].[EventTypes_SelectAll] 

	AS

	/*------Test Code------
		
	Execute [dbo].[EventTypes_SelectAll]


	*/---------------------

	BEGIN


	SELECT [Id]
		  ,[Name]

	  FROM [dbo].[EventTypes]

	END
GO
/****** Object:  StoredProcedure [dbo].[Experience_Delete_ById]    Script Date: 12/2/2022 3:13:23 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

-- =============================================
-- Author: <Ledwin Batista>
-- Create date: <07/28/2022>
-- Description: <Experience_Delete_ById>
-- Code Reviewer:

-- MODIFIED BY: author
-- MODIFIED DATE:12/1/2020
-- Code Reviewer:
-- Note:
-- =============================================

CREATE PROC [dbo].[Experience_Delete_ById]
	@Id int

AS

/* -- TEST CODE --
	
	Declare @Id int = 1

	EXECUTE dbo.Experience_Delete_ById @Id
	
*/ -- END TEST CODE --

BEGIN

	DELETE FROM dbo.Experience
	WHERE @Id = Id

END
GO
/****** Object:  StoredProcedure [dbo].[Experience_Insert]    Script Date: 12/2/2022 3:13:23 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

-- =============================================
-- Author: <Ledwin Batista>
-- Create date: <07/28/2022>
-- Description: <Experience_Insert>
-- Code Reviewer:

-- MODIFIED BY: author
-- MODIFIED DATE:12/1/2020
-- Code Reviewer:
-- Note:
-- =============================================

CREATE PROC [dbo].[Experience_Insert]
			 @Title nvarchar(50)
			,@EmploymentTypeId int
			,@CompanyName nvarchar(50)
			,@LocationId int
			,@IsCurrent bit
			,@Description nvarchar(200)
			,@CreatedBy int
			,@ModifiedBy int
			,@Id int OUTPUT

AS

/* -- TEST CODE --

	DECLARE
			@Title nvarchar(50) = "Engineer"
			,@EmploymentTypeId int = 10
			,@CompanyName nvarchar(50) = "Engineering"
			,@LocationId int = 11
			,@IsCurrent bit = 0
			,@Description nvarchar(200) = "We Engineer" 
			,@CreatedBy int = 12
			,@ModifiedBy int = 12
			,@Id int = 0

	EXECUTE dbo.Experience_Insert 
			@Title
			,@EmploymentTypeId
			,@CompanyName
			,@LocationId
			,@IsCurrent
			,@Description
			,@CreatedBy
			,@ModifiedBy
			,@Id 

*/ -- END TEST CODE --

BEGIN

	INSERT INTO [dbo].[Experience]
			([Title]
			,[EmploymentTypeId]
			,[CompanyName]
			,[LocationId]
			,[IsCurrent]
			,[Description]
			,[CreatedBy]
			,[ModifiedBy])

		 VALUES
			(@Title
			,@EmploymentTypeId
			,@CompanyName
			,@LocationId
			,@IsCurrent
			,@Description
			,@CreatedBy
			,@ModifiedBy)

		SET		@Id = SCOPE_IDENTITY()

END
GO
/****** Object:  StoredProcedure [dbo].[Experience_Select_ByCreatedBy]    Script Date: 12/2/2022 3:13:23 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author: <Grant Lee>
-- Create date: <08/01/2022>
-- Description: <Experience_Select_ByCreatedBy>
-- Code Reviewer: Morgan Frost 8/2/2022

-- MODIFIED BY: author
-- MODIFIED DATE: 08/01/2022
-- Code Reviewer:
-- Note:
-- =============================================

CREATE proc [dbo].[Experience_Select_ByCreatedBy]
									 @PageIndex int
									,@PageSize int
									,@CreatedBy int

as

/* ----- TEST CODE -----

	Declare		 @PageIndex int = 0
				,@PageSize int = 1
				,@CreatedBy int = 4
	

	Execute [dbo].[Experience_Select_ByCreatedBy] 
				 @PageIndex
				,@PageSize
				,@CreatedBy

*/ ----- END TEST CODE -----

BEGIN

Declare	   @offset int = @PageIndex * @PageSize

	SELECT   ex.[Id]
			,ex.[Title]
			,ex.[EmploymentTypeId]
			,ex.[CompanyName]
			,ex.[LocationId]
			,ex.[IsCurrent]
			,ex.[DateStart]
			,ex.[DateEnd]
			,ex.[Description]
			,ex.[CreatedBy]
			,TotalCount = COUNT(1) OVER()

	FROM [dbo].[Experience] as ex

WHERE		ex.CreatedBy = @CreatedBy
ORDER BY	ex.Id
			OFFSET @offset ROWS
FETCH NEXT  @PageSize ROWS ONLY

END
GO
/****** Object:  StoredProcedure [dbo].[Experience_Select_ById]    Script Date: 12/2/2022 3:13:23 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author: <Grant Lee>
-- Create date: <08/01/2022>
-- Description: <Experience_Select_ById>
-- Code Reviewer: Morgan Frost 8/2/2022

-- MODIFIED BY: author
-- MODIFIED DATE:08/01/2022
-- Code Reviewer:
-- Note:
-- =============================================

CREATE proc [dbo].[Experience_Select_ById]
								@Id int
as

/*
	Declare @Id int = 1
	Execute dbo.Experience_Select_ById
								  @Id

	Select *
	FROM dbo.Experience
*/

BEGIN

SELECT [exp].[Id]
      ,[exp].[Title]
      ,[exp].[EmploymentTypeId]
      ,[exp].[CompanyName]
      ,[exp].[LocationId]
      ,[exp].[IsCurrent]
      ,[exp].[DateStart]
      ,[exp].[DateEnd]
      ,[exp].[Description]
      ,[exp].[CreatedBy]
      ,[exp].[ModifiedBy]
      ,[exp].[DateCreated]
      ,[exp].[DateModified]
  FROM [dbo].[Experience] as [exp]
  WHERE [exp].Id = @Id

END
GO
/****** Object:  StoredProcedure [dbo].[Experience_SelectAll]    Script Date: 12/2/2022 3:13:23 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author: <Grant Lee>
-- Create date: <08/01/2022>
-- Description: <Experience_SelectAll>
-- Code Reviewer: Morgan Frost 8/2/2022

-- MODIFIED BY: author
-- MODIFIED DATE: 08/01/2022
-- Code Reviewer:
-- Note:
-- =============================================

CREATE proc [dbo].[Experience_SelectAll]
							@PageIndex int
						   ,@PageSize int
as

/*------TEST CODE------

Declare @PageIndex int = 0
	   ,@PageSize int = 1

Execute [dbo].[Experience_SelectAll]
						  @PageIndex
						 ,@PageSize

*/

BEGIN

Declare @offset int = @PageIndex * @PageSize

SELECT ex.[Id]
      ,ex.[Title]
      ,ex.[EmploymentTypeId]
      ,ex.[CompanyName]
      ,ex.[LocationId]
      ,ex.[IsCurrent]
      ,ex.[DateStart]
      ,ex.[DateEnd]
      ,ex.[Description]
      ,ex.[CreatedBy]
      ,ex.[ModifiedBy]
      ,ex.[DateCreated]
      ,ex.[DateModified]
	  ,TotalCount = COUNT(1) OVER()

  FROM [dbo].[Experience] as ex
  ORDER BY ex.Id

  OFFSET @offSet Rows
  FETCH NEXT @PageSize ROWS ONLY

END
GO
/****** Object:  StoredProcedure [dbo].[Experience_Update]    Script Date: 12/2/2022 3:13:23 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

-- =============================================
-- Author: <Ledwin Batista>
-- Create date: <07/28/2022>
-- Description: <Experience_Update>
-- Code Reviewer:

-- MODIFIED BY: author
-- MODIFIED DATE:12/1/2020
-- Code Reviewer:
-- Note:
-- =============================================

CREATE PROC [dbo].[Experience_Update]
			@Title nvarchar(50)
			,@EmploymentTypeId int
			,@CompanyName nvarchar(50)
			,@LocationId int
			,@IsCurrent bit
			,@Description nvarchar(200)
			,@CreatedBy int
			,@ModifiedBy int
			,@Id int
AS

BEGIN

	UPDATE [dbo].[Experience]

	   SET [Title] = @Title
		  ,[EmploymentTypeId] = @CreatedBy
		  ,[CompanyName] = @ModifiedBy
		  ,[LocationId] = @LocationId
		  ,[IsCurrent] = @IsCurrent
		  ,[Description] = @Description
		  ,[CreatedBy] = @CreatedBy
		  ,[ModifiedBy] = @ModifiedBy
		  ,[DateModified] = GETUTCDATE()

	 WHERE @Id = Id

END


GO
/****** Object:  StoredProcedure [dbo].[ExternalLinks_DeleteById]    Script Date: 12/2/2022 3:13:23 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE proc [dbo].[ExternalLinks_DeleteById]
									@Id int
									,@UserId int


AS

/*...TEST CODE...

DECLARE @Id int = 5
		,@UserId int = 18

execute dbo.ExternalLinks_DeleteById
								@Id 
								,@UserId

*/


BEGIN

	DELETE FROM dbo.ExternalLinks
	WHERE Id = @Id
	AND UserId = @UserId 
	  

END
GO
/****** Object:  StoredProcedure [dbo].[ExternalLinks_Insert]    Script Date: 12/2/2022 3:13:23 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO



CREATE proc [dbo].[ExternalLinks_Insert]
									@UserId int
								   ,@UrlTypeId int
								   ,@Url nvarchar(255)
								   ,@EntityId int
								   ,@EntityTypeId int 
								   ,@Id int OUTPUT

as
/*...TEST CODE...

Declare @Id int = 0;

Declare @UserId int = 18
			,@UrlTypeId int = 2
			,@Url nvarchar(255) = 'www.news.com'
			,@EntityId int = 256
			,@EntityTypeId int = 2


execute dbo.ExternalLinks_Insert
								@UserId
								,@UrlTypeId
								,@Url
								,@EntityId
								,@EntityTypeId 
								,@Id OUTPUT
*/

BEGIN


	INSERT INTO [dbo].[ExternalLinks]
           ([UserId]
           ,[UrlTypeId]
           ,[Url]
           ,[EntityId]
           ,[EntityTypeId])
     VALUES
           (@UserId
           ,@UrlTypeId
           ,@Url
           ,@EntityId
           ,@EntityTypeId)
		
	SET @Id = SCOPE_IDENTITY();

END


GO
/****** Object:  StoredProcedure [dbo].[ExternalLinks_SelectByCreatedBy]    Script Date: 12/2/2022 3:13:23 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO



CREATE proc [dbo].[ExternalLinks_SelectByCreatedBy]
										@UserId int

as

/*...TEST CODE...


Declare @UserId int = 147;

execute dbo.ExternalLinks_SelectByCreatedBy
									@UserId 



*/


BEGIN

	SELECT 	l.Id
			,l.UserId
			,u.Id as UrlTypeId
			,u.Name as UrlType
			,l.Url
			,l.EntityId
			,e.Id as EntityTypeId
			,e.Name as EntityType
			,l.DateCreated
			,l.DateModified

	FROM dbo.ExternalLinks as l 
		inner join dbo.UrlTypes as u 
			on u.Id = l.UrlTypeId
		inner join dbo.EntityTypes as e 
			on e.Id = l.EntityTypeId

	WHERE UserId = @UserId


END
GO
/****** Object:  StoredProcedure [dbo].[ExternalLinks_Update]    Script Date: 12/2/2022 3:13:23 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO


CREATE proc [dbo].[ExternalLinks_Update] @Id int
									,@UserId int
								   ,@Url nvarchar(255)

as
/*...TEST CODE...

Declare	@Id int = 8
			,@UserId int = 147
			,@Url nvarchar(255) = 'www.updatedddddd.com'



execute dbo.ExternalLinks_Update @Id
								,@UserId
								,@Url

*/

BEGIN

	Declare @Date datetime2 = getutcdate();

		UPDATE [dbo].[ExternalLinks]

		   SET [Url] = @Url
			  ,[DateModified] = @Date
		 WHERE Id = @Id 
		 AND UserId = @UserId
END


GO
/****** Object:  StoredProcedure [dbo].[Facebook_Select_AuthData]    Script Date: 12/2/2022 3:13:23 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author: Gonzalez, Victor
-- Create date: 9/6/22
-- Description: Inserts Facebook Token properties
-- Code Reviewer:

-- MODIFIED BY: Gonzalez, Victor
-- MODIFIED DATE:9/6/2022
-- Code Reviewer:
-- Note:
-- ==============================================

CREATE PROC [dbo].[Facebook_Select_AuthData]
	@Email nvarchar(100)

AS

/* ----- TEST CODE -----

	DECLARE @Email nvarchar(100) = 'testuser7000@gmail.com'
	
	EXECUTE dbo.Facebook_Select_AuthData @Email

*/ ----- END TEST CODE -----


Declare @status INT
		,@confirm BIT
SELECT @status = U.UserStatusId
		,@confirm = U.IsConfirmed
FROM dbo.Users as U
WHERE @Email = Email

BEGIN
	IF @confirm = 1
		IF @status < 2
			SELECT [U].[Id]
				  ,[U].[Email]
				 
				  ,R.[Name]
				  ,U.IsConfirmed
			  FROM [dbo].[Users] AS U
				   INNER JOIN
				   dbo.UserRoles AS UR
				   ON U.Id = UR.UserId
				   INNER JOIN
				   dbo.Roles AS R
				   ON UR.RoleId = R.Id

			  WHERE [U].[Email] = @Email
		ELSE
			  THROW 60001, 'User is not active.', 16
	ELSE
		THROW 60002, 'User is not confirmed.', 16
END

GO
/****** Object:  StoredProcedure [dbo].[FAQs_Delete_ById]    Script Date: 12/2/2022 3:13:23 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author: Joshua Eslava
-- Create date: 7/19/2022
-- Description: Faq_Delete_ById
-- Code Reviewer: Thomas Sauer

-- MODIFIED BY: author
-- MODIFIED DATE:12/1/2020
-- Code Reviewer:
-- Note:
-- =============================================

CREATE PROC [dbo].[FAQs_Delete_ById]
@Id int 


as
/*
Declare @Id int = 3

Execute dbo.Faq_Delete_ById @Id

Select * from dbo.Faq
*/



BEGIN

DELETE FROM [dbo].[Faqs]
      WHERE @Id = Id

END


GO
/****** Object:  StoredProcedure [dbo].[FAQs_Insert]    Script Date: 12/2/2022 3:13:23 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author: Joshua Eslava
-- Create date: 7/19/2022
-- Description: <FAQ_insert>
-- Code Reviewer: Thomas Sauer

-- MODIFIED BY: author
-- MODIFIED DATE:12/1/2020
-- Code Reviewer:
-- Note:
-- =============================================

CREATE proc [dbo].[FAQs_Insert]

@Question nvarchar(255)
,@Answer nvarchar(2000) 
,@CategoryId int
,@SortOrder int
,@CreatedBy int
,@ModifiedBy int
,@Id int OUTPUT


AS
/*


DECLARE @Question nvarchar(255) = 'question3'
		,@Answer nvarchar(2000) = 'answer3'
		,@CategoryId int = 3
		,@SortOrder int = 3
		,@CreatedBy int = 3
		,@ModifiedBy int = 3
		,@Id int = 3 

EXECUTE [dbo].[Faq_Insert]
		@Question 
		,@Answer 
		,@CategoryId 
		,@SortOrder 
		,@CreatedBy 
		,@ModifiedBy 
		,@Id OUTPUT

	Select *
	From [dbo].[Faq]
	Where Id = @Id

*/


BEGIN

INSERT INTO [dbo].[FAQs]
               ([Question]
               ,[Answer]
               ,[CategoryId]
               ,[SortOrder]
               ,[CreatedBy]
               ,[ModifiedBy])
         VALUES
               (@Question
               ,@Answer
               ,@CategoryId
               ,@SortOrder
               ,@CreatedBy
               ,@ModifiedBy)

        SELECT @Id = SCOPE_IDENTITY()

END


GO
/****** Object:  StoredProcedure [dbo].[FAQs_Select_ByCreatedBy]    Script Date: 12/2/2022 3:13:23 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author: Joshua Eslava
-- Create date: 07/19/2022
-- Description: Paginated list returned of FAQs by CreatedBy
-- Code Reviewer: Thomas Sauer

-- MODIFIED BY: author
-- MODIFIED DATE:
-- Code Reviewer:
-- Note:
-- =============================================

CREATE PROC [dbo].[FAQs_Select_ByCreatedBy]
				@PageIndex	int
				,@PageSize	int
				,@CreatedBy	int

AS
/* ====================Test code====================

    Declare     @PageIndex	int = 0
				,@PageSize	int = 5
				,@CreatedBy	int = 52

    Execute [dbo].[FAQs_Select_ByCreatedBy]
				
                @PageIndex
				,@PageSize
				,@CreatedBy	

				Select * from dbo.FAQs
   ====================Test code==================== */
 
BEGIN
   DECLARE @Offset int = @PageIndex * @PageSize
	SELECT 
		  f.Id
		  ,f.Question
		  ,f.Answer
		  ,fc.Id as CategoryId
		  ,f.SortOrder
		  ,f.DateCreated
		  ,f.DateModified
		  ,f.CreatedBy
		  ,f.ModifiedBy
		  ,TotalCount = COUNT(1) OVER()
	  FROM [dbo].[FAQs] as f inner join [dbo].[FAQsCategories] as fc
		   on f.CategoryId = fc.Id
	  WHERE CreatedBy = @CreatedBy
	  ORDER BY f.Id
	  OFFSET @Offset ROWS
	  FETCH NEXT @PageSize ROWS ONLY
END
GO
/****** Object:  StoredProcedure [dbo].[FAQs_Select_ById]    Script Date: 12/2/2022 3:13:23 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author: Joshua Eslava
-- Create date: 7/19/2022
-- Description: Faq_Select_ById
-- Code Reviewer: Thomas Sauer

-- MODIFIED BY: author
-- MODIFIED DATE:12/1/2020
-- Code Reviewer:
-- Note:
-- =============================================


CREATE proc [dbo].[FAQs_Select_ById]
@Id int

as

/*
Declare @Id int = 1 

Execute dbo.Faqs_Select_ById @Id

*/

BEGIN

SELECT [Id]
      ,[Question]
      ,[Answer]
      ,[CategoryId]
      ,[SortOrder]
      ,[DateCreated]
      ,[DateModified]
      ,[CreatedBy]
      ,[ModifiedBy]
  FROM [dbo].[Faqs]
  WHERE Id = @Id 

END


GO
/****** Object:  StoredProcedure [dbo].[FAQs_SelectAll]    Script Date: 12/2/2022 3:13:23 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author: Joshua Eslava
-- Create date: 07/19/2022
-- Description: Paginated list returned of FAQs
-- Code Reviewer: Thomas Sauer

-- MODIFIED BY: author
-- MODIFIED DATE:
-- Code Reviewer: 
-- Note:
-- =============================================

CREATE PROC [dbo].[FAQs_SelectAll]
				@PageIndex	int
				,@PageSize	int
AS
/* ====================Test code====================

    Declare     @PageIndex	int = 0
				,@PageSize	int = 1

    Execute [dbo].[FAQs_SelectAll]
                @PageIndex
				,@PageSize

   ====================Test code==================== */

BEGIN

   DECLARE @Offset int = @PageIndex * @PageSize

	SELECT
		  f.Id
		  ,f.Question
		  ,f.Answer
		  ,fc.Id as CategoryId
		  ,f.SortOrder
		  ,f.DateCreated
		  ,f.DateModified
		  ,f.CreatedBy
		  ,f.ModifiedBy
		  ,TotalCount = COUNT(1) OVER()

	  FROM [dbo].[FAQs] as f inner join [dbo].[FAQsCategories] as fc
			on f.CategoryId = fc.Id
	  ORDER BY f.Id
	  OFFSET @Offset ROWS
	  FETCH NEXT @PageSize ROWS ONLY
END
GO
/****** Object:  StoredProcedure [dbo].[FAQs_Update]    Script Date: 12/2/2022 3:13:23 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author: Joshua Eslava
-- Create date: 07/19/2022
-- Description: Updating FAQs
-- Code Reviewer: Thomas Sauer

-- MODIFIED BY: author
-- MODIFIED DATE: 
-- Code Reviewer: 
-- Note:
-- =============================================

CREATE PROC [dbo].[FAQs_Update]
			   @Id              int
               ,@Question       nvarchar(255)
               ,@Answer         nvarchar(2000)
               ,@CategoryId     int
               ,@SortOrder      int
               ,@CreatedBy      int
               ,@ModifiedBy     int


AS
/* ====================Test code====================

    Declare     @Id				int				= 1
				,@Question      nvarchar(255)   = 'question1updated'
                ,@Answer        nvarchar(2000)  = 'answer1updated'
				,@CategoryId    int				= 1
				,@SortOrder     int             = 1
                ,@CreatedBy     int             = 2
                ,@ModifiedBy    int             = 2

	Execute [dbo].[FAQs_Select_ById]	@Id

    Execute [dbo].[FAQs_Update]

                @Id
                ,@Question
                ,@Answer
                ,@CategoryId
                ,@SortOrder
                ,@CreatedBy
                ,@ModifiedBy

	Execute [dbo].[FAQs_Select_ById]	@Id

   ====================Test code==================== */

BEGIN


DECLARE @DateModified datetime2(7) = GETUTCDATE()

		UPDATE [dbo].[FAQs]
			SET [Question]		= @Question
				,[Answer]			= @Answer
				,[CategoryId]		= @CategoryId
				,[SortOrder]		= @SortOrder
				,[DateModified]	= @DateModified
				,[CreatedBy]		= @CreatedBy
				,[ModifiedBy]		= @ModifiedBy


	WHERE Id = @Id



END


GO
/****** Object:  StoredProcedure [dbo].[FAQsCategories_SelectAll]    Script Date: 12/2/2022 3:13:23 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create proc [dbo].[FAQsCategories_SelectAll]


/*
execute dbo.FAQsCategories_SelectAll

*/

as 

begin

	SELECT [Id]
		  ,[Name]
	  FROM [dbo].[FAQsCategories]

end

GO
/****** Object:  StoredProcedure [dbo].[Files_Delete_ById]    Script Date: 12/2/2022 3:13:23 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

-- =============================================
-- Author: <James Hughes>
-- Create date: <06/30/2022>
-- Description: <Files_Delete_ById>
-- Code Reviewer:

-- MODIFIED BY: Daniel Cazarez
-- MODIFIED DATE:07/29/2022
-- Code Reviewer: Lucas Gil Montoya
-- Note: Modified proc to add isDeleted col bit and implement as isDelete update
-- =============================================

CREATE   PROC [dbo].[Files_Delete_ById]
			@Id int
			,@Delete bit

/*---------TEST CODE--------------

	DECLARE	@Id int = 1
			,@Delete bit = 1

	SELECT *
	FROM [dbo].[Files]
	WHERE Id = @Id

	EXECUTE dbo.[Files_Delete_ById] 
			@Id
			,@Delete

	SELECT *
	FROM dbo.Files
	WHERE Id = @Id

*/---------END TEST CODE--------------

as
BEGIN

	UPDATE	[dbo].[Files]
		SET	[IsDeleted] = @Delete
	WHERE Id = @Id
END  
GO
/****** Object:  StoredProcedure [dbo].[Files_Insert]    Script Date: 12/2/2022 3:13:23 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author: <James Hughes>
-- Create date: <06/30/2022>
-- Description: <Files_Insert>
-- Code Reviewer:

-- MODIFIED BY: author
-- MODIFIED DATE:12/1/2020
-- Code Reviewer:
-- Note:
-- =============================================


CREATE   PROC [dbo].[Files_Insert]
			@Name nvarchar (80)
			,@Url nvarchar (255) 
			,@FileType nvarchar(50) 
			,@CreatedBy int
			,@Id int OUTPUT

AS

/* --Test Code
Declare @Id int = 0;

Declare		@Name nvarchar (80) = 'Test10'
			,@Url nvarchar (255) = 'www.evertek1.com'
			,@FileType nvarchar(50) = '.pdf'
			,@CreatedBy int = 4


Execute [dbo].[Files_Insert] 
			@Name
			,@Url
			,@FileType
			,@CreatedBy
			,@Id OUTPUT

Execute [dbo].[Files_Select_ById] @Id

--*/

BEGIN

	Declare		@FileTypeId int 

	SET			@FileTypeId = (SELECT Id FROM [dbo].[FileTypes] WHERE Name = @FileType)

	INSERT INTO [dbo].[Files]
				([Name]
				,[Url]
				,[FileTypeId]
				,[CreatedBy])

	VALUES		(@Name
				,@Url
				,@FileTypeId
				,@CreatedBy)

	SET @Id = SCOPE_IDENTITY();

END
GO
/****** Object:  StoredProcedure [dbo].[Files_InsertV2]    Script Date: 12/2/2022 3:13:23 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author: <Daniel Cazarez>
-- Create date: <07/29/2022>
-- Description: <Files_InsertV2>
-- Code Reviewer: Lucas Gil Montoya

-- MODIFIED BY: author
-- MODIFIED DATE: n/a
-- Code Reviewer:
-- Note:
-- =============================================


CREATE   PROC [dbo].[Files_InsertV2]
			@Name nvarchar (50)
			,@Url nvarchar (255) 
			,@FileType nvarchar(50) 
			,@CreatedBy int
			,@Id int OUTPUT

AS

/* --Test Code
Declare		@Id int = 0;

Declare		@Name nvarchar (50) = 'Dragon'
			,@Url nvarchar (255) = 'https://sabio-training.s3-us-west-2.amazonaws.com/c84a213b-160b-4fbe-b62b-38e48d40e866_Dragon.jpg'
			,@FileType nvarchar(50) = '.jpg'
			,@CreatedBy int = 3
			

EXECUTE [dbo].[Files_InsertV2] 
			@Name
			,@Url
			,@FileType
			,@CreatedBy
			,@Id OUTPUT
	
	SELECT *
	FROM [dbo].[Files] as f inner join dbo.FileTypes as ft
						on ft.Id = f.FileTypeId 
					

--*/

BEGIN

	Declare		@FileTypeId int 

	SET			@FileTypeId = (SELECT Id FROM [dbo].[FileTypes] WHERE Name = @FileType)

	INSERT INTO [dbo].[Files]
				([Name]
				,[Url]
				,[FileTypeId]
				,[CreatedBy])
				

	VALUES		(@Name
				,@Url
				,@FileTypeId
				,@CreatedBy)

	SET @Id = SCOPE_IDENTITY();

END
GO
/****** Object:  StoredProcedure [dbo].[Files_Search_Paginated]    Script Date: 12/2/2022 3:13:23 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

-- =============================================
-- Author: <Daniel Cazarez>
-- Create date: <07/29/2022>
-- Description: <Files_Search_Paginated>
-- Code Reviewer: Lucas Gil Montoya

-- MODIFIED BY: author
-- MODIFIED DATE: date
-- Code Reviewer: 
-- Note:
-- =============================================

CREATE   PROC [dbo].[Files_Search_Paginated]
				@PageIndex int
				,@PageSize int
				,@Query nvarchar(200)
AS
/* ----- TEST CODE -----

	Declare @PageIndex int = 0
			,@PageSize int = 25
			,@Query nvarchar(200) = 'Image'

	Execute [dbo].[Files_Search_Paginated]
				@PageIndex 
				,@PageSize 
				,@Query

*/ ----- END TEST CODE -----

BEGIN

	Declare @offset int = @PageIndex * @PageSize

	SELECT f.[Id]
			,f.[Name]
			,f.[Url]
			,f.[IsDeleted]
			,ft.Id
			,ft.[Name]
			,u.[Id] as UserId
			,TotalCount = COUNT(1) OVER()

	FROM [dbo].[Files] as f inner join dbo.FileTypes as ft
						on ft.Id = f.FileTypeId 
						inner join dbo.Users as u
						on u.Id = f.CreatedBy

	WHERE f.IsDeleted = 0
	AND (f.[Name] LIKE '%' + @Query + '%'
	OR	f.[Url] LIKE '%' + @Query + '%')
	
	ORDER BY f.Id

	OFFSET @offSet Rows
	Fetch Next @PageSize Rows ONLY

END
GO
/****** Object:  StoredProcedure [dbo].[Files_Select_ByCreatedBy]    Script Date: 12/2/2022 3:13:23 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

-- =============================================
-- Author: <James Hughes>
-- Create date: <06/30/2022>
-- Description: <Files_Select_ByCreatedBy>
-- Code Reviewer:

-- MODIFIED BY: Daniel Cazarez
-- MODIFIED DATE:08/20/2022
-- Code Reviewer:
-- Note: <Target for WHERE was incorrect. Changed from f.id to u.id, add added updated columns>
-- =============================================

CREATE   PROC [dbo].[Files_Select_ByCreatedBy] 
						@PageIndex int
						,@PageSize int
						,@CreatedBy int
as

/* ----- TEST CODE -----

	Declare		@PageIndex int = 0
				,@PageSize int = 500
				,@CreatedBy int = 135
	

	Execute [dbo].[Files_Select_ByCreatedBy] 
				@PageIndex
				,@PageSize
				,@CreatedBy

	

*/ ----- END TEST CODE -----

BEGIN

Declare	   @offset int = @PageIndex * @PageSize

	SELECT f.[Id]
			,f.[Name]
			,f.[Url]
			,f.[IsDeleted]
			,ft.[Id] as FileTypeId
			,ft.[Name]
			,u.[Id] as UserId
			,TotalCount = COUNT(1) OVER()

	FROM [dbo].[Files] as f inner join dbo.FileTypes as ft
						on ft.Id = f.FileTypeId 
						inner join dbo.Users as u
						on u.Id = f.CreatedBy

WHERE		u.Id = @CreatedBy and isDeleted = 0
ORDER BY	f.Id
			OFFSET @offset ROWS
FETCH NEXT  @PageSize ROWS ONLY


END
GO
/****** Object:  StoredProcedure [dbo].[Files_Select_ByFileTypeId_Paginated]    Script Date: 12/2/2022 3:13:23 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO


-- =============================================
-- Author: <Daniel Cazarez>
-- Create date: <07/28/2022>
-- Description: <Files_Select_ByFileTypeId_Paginated>
-- Code Reviewer: Lucas Gil Montoya

-- MODIFIED BY: N/A
-- MODIFIED DATE: N/A
-- Code Reviewer: N/A
-- Note:
-- =============================================

CREATE   PROC [dbo].[Files_Select_ByFileTypeId_Paginated] 
			@PageIndex int
			,@PageSize int
			,@TypeId int
as

/* ----- TEST CODE -----

	DECLARE	@PageIndex int = 0
			,@PageSize int = 50
			,@TypeId int = 2
	

	EXECUTE [dbo].[Files_Select_ByFileTypeId_Paginated] 
			@PageIndex
			,@PageSize
			,@TypeId

*/ ----- END TEST CODE -----

BEGIN

	DECLARE	   @offset int = @PageIndex * @PageSize

	SELECT f.[Id]
			,f.[Name]
			,f.[Url]
			,f.[IsDeleted]
			,ft.[Id] as FileTypeId
			,ft.[Name]
			,u.[Id] as UserId
			,TotalCount = COUNT(1) OVER()

	FROM [dbo].[Files] as f inner join dbo.FileTypes as ft
						on ft.Id = f.FileTypeId 
						inner join dbo.Users as u
						on u.Id = f.CreatedBy

WHERE		ft.Id = @TypeId and IsDeleted = 0
ORDER BY	f.Id
			OFFSET @offset ROWS
FETCH NEXT  @PageSize ROWS ONLY

END
GO
/****** Object:  StoredProcedure [dbo].[Files_Select_ById]    Script Date: 12/2/2022 3:13:23 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

-- =============================================
-- Author: <James Hughes>
-- Create date: <06/30/2022>
-- Description: <Files_Select_ById>
-- Code Reviewer:

-- MODIFIED BY: author
-- MODIFIED DATE:12/1/2020
-- Code Reviewer: Brandon Peace
-- Note:
-- =============================================

CREATE   PROC [dbo].[Files_Select_ById] 
	@Id int 
	
as

/* ----- TEST CODE -----

	Declare @Id int = 21;

	Execute [dbo].[Files_Select_ById] @Id

*/ ----- END TEST CODE -----

BEGIN

	SELECT f.[Name],
			f.[Id]
			,f.[Url]
			,ft.[Name] as FileType
			,u.[Id] as CreatedBy

	FROM [dbo].[Files] as f inner join dbo.FileTypes as ft
						on ft.Id = f.FileTypeId 
						inner join dbo.Users as u
						on u.Id = f.CreatedBy

			Where @Id = f.Id
END
GO
/****** Object:  StoredProcedure [dbo].[Files_Select_ByIsDeleted_Paginated]    Script Date: 12/2/2022 3:13:23 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO


-- =============================================
-- Author: <Daniel Cazarez>
-- Create date: <07/29/2022>
-- Description: <Files_Select_ByIsDeleted_Paginated>
-- Code Reviewer: Lucas Gil Montoya

-- MODIFIED BY: N/A
-- MODIFIED DATE: N/A
-- Code Reviewer: N/A
-- Note:
-- =============================================

CREATE   PROC [dbo].[Files_Select_ByIsDeleted_Paginated] 
			@PageIndex int
			,@PageSize int
			,@IsDeleted bit
AS
/* ----- TEST CODE -----

	DECLARE	@PageIndex int = 0
			,@PageSize int = 50
			,@IsDeleted bit = 1
	

	EXECUTE [dbo].[Files_Select_ByIsDeleted_Paginated] 
			@PageIndex
			,@PageSize
			,@IsDeleted

*/ ----- END TEST CODE -----

BEGIN

	DECLARE	   @offset int = @PageIndex * @PageSize

	SELECT f.[Id]
			,f.[Name]
			,f.[Url]
			,f.[IsDeleted]
			,ft.[Id] as FileTypeId
			,ft.[Name]
			,u.[Id] as UserId
			,TotalCount = COUNT(1) OVER()

	FROM [dbo].[Files] as f inner join dbo.FileTypes as ft
						on ft.Id = f.FileTypeId 
						inner join dbo.Users as u
						on u.Id = f.CreatedBy

WHERE		f.IsDeleted = @IsDeleted
ORDER BY	f.Id
			OFFSET @offset ROWS
FETCH NEXT  @PageSize ROWS ONLY

END
GO
/****** Object:  StoredProcedure [dbo].[Files_SelectAll]    Script Date: 12/2/2022 3:13:23 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author: <James Hughes>
-- Create date: <06/30/2022>
-- Description: <Files_SelectAll>
-- Code Reviewer:

-- MODIFIED BY: <Daniel Cazarez>
-- MODIFIED DATE: <07/28/2022>
-- Code Reviewer: Lucas Gil Montoya
-- Note: Added is deleted column to proc and where isdeleted is zero
-- =============================================

CREATE   PROC [dbo].[Files_SelectAll]
				@PageIndex int
				,@PageSize int

as

/* ----- TEST CODE -----

	Declare @PageIndex int = 0
			,@PageSize int = 1000

	Execute [dbo].[Files_SelectAll]
				@PageIndex 
				,@PageSize 

*/ ----- END TEST CODE -----

BEGIN

	Declare @offset int = @PageIndex * @PageSize

	SELECT f.[Id]
			,f.[Name]
			,f.[Url]
			,f.[IsDeleted]
			,ft.[Id] as FileTypeId
			,ft.[Name]
			,u.[Id] as UserId
			,TotalCount = COUNT(1) OVER()

	FROM [dbo].[Files] as f inner join dbo.FileTypes as ft
						on ft.Id = f.FileTypeId 
						inner join dbo.Users as u
						on u.Id = f.CreatedBy
	WHERE IsDeleted = 0
	ORDER BY f.Id

	OFFSET @offSet Rows
	Fetch Next @PageSize Rows ONLY

END
GO
/****** Object:  StoredProcedure [dbo].[Files_Update]    Script Date: 12/2/2022 3:13:23 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
 -- =============================================
-- Author: <James Hughes>
-- Create date: <06/30/2022>
-- Description: <Files_SelectAll>
-- Code Reviewer:

-- MODIFIED BY: author
-- MODIFIED DATE:12/1/2020
-- Code Reviewer: Brandon Peace
-- Note:
-- =============================================

CREATE   PROC [dbo].[Files_Update]
			@Url nvarchar (255) 
			,@FileType nvarchar(50) 
			,@CreatedBy int
			,@Id int 

AS

/* --Test Code

Declare		@Id int = 3

Declare		@Url nvarchar (255) = 'www.Apple.com'
			,@FileType nvarchar(100) = '.pdf'
			,@CreatedBy int = 4

Execute [dbo].[Files_Update]
			@Url
			,@FileType
			,@CreatedBy
			,@Id

			Select *
			From [dbo].[Files]
			WHERE Id = @Id

Execute [dbo].[Files_Select_ById] @Id
--*/

BEGIN

	Declare	@FileTypeId int 

		SET @FileTypeId = (SELECT Id FROM [dbo].[FileTypes] WHERE Name = @FileType)

	UPDATE	[dbo].[Files]
		SET	[Url] = @Url
			,[FileTypeId] = @FileTypeId
			,[CreatedBy] = @CreatedBy
	WHERE Id = @Id

END
GO
/****** Object:  StoredProcedure [dbo].[FileTypes_SelectAll]    Script Date: 12/2/2022 3:13:23 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author: <James Hughes>
-- Create date: <06/30/2022>
-- Description: <FileTypes_SelectAll>
-- Code Reviewer:

-- MODIFIED BY: author
-- MODIFIED DATE:12/1/2020
-- Code Reviewer: Brandon Peace
-- Note:
-- =============================================

CREATE   PROC [dbo].[FileTypes_SelectAll]
as

/* ----- TEST CODE -----

	Execute [dbo].[FileTypes_SelectAll] 

*/ ----- END TEST CODE -----

BEGIN

SELECT [Id]
		,[Name]


FROM dbo.FileTypes 

END
GO
/****** Object:  StoredProcedure [dbo].[FreelanceGoalTypes_SelectAll]    Script Date: 12/2/2022 3:13:23 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

-- =============================================
-- Author: <Ledwin Batista>
-- Create date: <07/27/2022>
-- Description: <FreelanceGoalTypes_SelectAll>
-- Code Reviewer:

-- MODIFIED BY: author
-- MODIFIED DATE:12/1/2020
-- Code Reviewer:
-- Note:
-- =============================================

CREATE PROC [dbo].[FreelanceGoalTypes_SelectAll]

AS

/* -- TEST CODE --


EXECUTE [dbo].[FreelanceGoalTypes_SelectAll] 
	
	
*/ -- END TEST CODE --

BEGIN

	SELECT 
			Id
			,[Name]

	FROM [dbo].[FreelanceGoalTypes]

END
GO
/****** Object:  StoredProcedure [dbo].[GetUserOrganizations]    Script Date: 12/2/2022 3:13:23 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- Author: <Rebekah Hoskins>
-- Create date: <20220809>
-- Description: <Select all organization associated with a user>
-- Code Reviewer:

-- MODIFIED BY: author
-- MODIFIED DATE:12/1/2020
-- Code Reviewer:
-- Note:

CREATE PROC [dbo].[GetUserOrganizations]
	@UserId int

AS

/*-----TEST CODE-----
	
	DECLARE @UserId int = 22

	EXECUTE [dbo].[GetUserOrganizations]
		@UserId

-----END TEST CODE-----
*/

BEGIN

	SELECT DISTINCT o.[Name] AS Organization
				,j.OrganizationId
		FROM [dbo].[UserJobs] AS uj INNER JOIN [dbo].[Jobs] AS j
				ON uj.JobId = j.Id
			INNER JOIN dbo.Organizations AS o
				ON o.Id=j.OrganizationId
		WHERE  [UserId] = @UserId

END
GO
/****** Object:  StoredProcedure [dbo].[Institution_Insert]    Script Date: 12/2/2022 3:13:23 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author: <Grant Lee>
-- Create date: <08/15/2022>
-- Description: <Institution_Insert>
-- Code Reviewer:

-- MODIFIED BY: author
-- MODIFIED DATE:
-- Code Reviewer:
-- Note:
-- =============================================

CREATE proc [dbo].[Institution_Insert]
			                 @Name nvarchar(150)
			                ,@Id int output

AS

/* -- TEST CODE --

	Declare @Id int = 0;

	Declare @Name nvarchar(150) = 'Alabama A & M University 2'


	EXECUTE [dbo].[Institution_Insert]
			@Name
		   ,@Id output


	Select *
	from dbo.Institution
    where Id = @Id
			
	select * 
	from dbo.Institution
		
*/ -- END TEST CODE --


BEGIN
	

	INSERT INTO [dbo].[Institution] 
			([Name])
		
		VALUES
			(@Name)
			
	SET @Id = SCOPE_IDENTITY()

END
GO
/****** Object:  StoredProcedure [dbo].[Institution_SelectAll]    Script Date: 12/2/2022 3:13:23 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author: <Grant Lee>
-- Create date: <08/05/2022>
-- Description: <Institution_SelectAll>
-- Code Reviewer:

-- MODIFIED BY: author
-- MODIFIED DATE: 8/16/2022
-- Code Reviewer: Joey Oh 8/16/2022
-- Note:
-- =============================================

CREATE proc [dbo].[Institution_SelectAll]

/*------TEST CODE------

		Execute dbo.Institution_SelectAll

*/

as

BEGIN

SELECT	 inst.[Id]
		,inst.[Name]		

	
	FROM dbo.Institution as inst

	ORDER BY inst.Id

END
GO
/****** Object:  StoredProcedure [dbo].[Jobs_Delete]    Script Date: 12/2/2022 3:13:23 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author: <Nicholas Rodriguez>
-- Create date: <07/01/2022>
-- Description: <Jobs_Delete>
-- Code Reviewer:

-- MODIFIED BY: author
-- MODIFIED DATE:12/1/2020
-- Code Reviewer:
-- Note:
-- =============================================
CREATE PROC [dbo].[Jobs_Delete]
						@Id int

/*-----------Test Code-------------

DECLARE @Id int = 1;

SELECT *
FROM [dbo].[Jobs]
WHERE Id = @Id

EXEC [dbo].[Jobs_Delete]
					@Id

SELECT *
FROM [dbo].[Jobs]
WHERE Id = @Id

*/

AS

BEGIN
	DELETE FROM [dbo].[Jobs]
		WHERE Id = @Id
END


GO
/****** Object:  StoredProcedure [dbo].[Jobs_DeleteV2]    Script Date: 12/2/2022 3:13:23 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author: <Nicholas Rodriguez>
-- Create date: <07/01/2022>
-- Description: <Jobs_Delete>
-- Code Reviewer:

-- MODIFIED BY: author
-- MODIFIED DATE:12/1/2020
-- Code Reviewer:
-- Note:
-- =============================================
CREATE PROC [dbo].[Jobs_DeleteV2]
						@Id int

/*-----------Test Code-------------

DECLARE @Id int = 148;

SELECT *
FROM [dbo].[Jobs]
WHERE Id = @Id

EXEC [dbo].[Jobs_Delete]
					@Id

SELECT *
FROM [dbo].[Jobs]
WHERE Id = @Id

*/

AS

BEGIN
	DELETE FROM dbo.Proposals
		WHERE JobId = @Id 

	DELETE FROM [dbo].[Jobs]
		WHERE Id = @Id
END


GO
/****** Object:  StoredProcedure [dbo].[Jobs_Insert]    Script Date: 12/2/2022 3:13:23 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author: <Nicholas Rodriguez>
-- Create date: <07/01/2022>
-- Description: <Jobs_Insert>
-- Code Reviewer:

-- MODIFIED BY: author
-- MODIFIED DATE:12/1/2020
-- Code Reviewer:
-- Note:
-- =============================================
CREATE PROC [dbo].[Jobs_Insert]
					@JobTypeId int
					,@Title nvarchar(200)
					,@Description nvarchar(4000)
					,@Requirements nvarchar(3000)
					,@IsActive bit
					,@OrganizationId int
					,@LocationId int
					,@ContactName nvarchar(100)
					,@ContactPhone nvarchar(20)
					,@ContactEmail nvarchar(200)
					,@UserId int
					,@Id int OUTPUT

/*---------------Test Code--------------
DECLARE @JobTypeId int = 111
		,@LocationId int = 222
		,@OrganizationId int = 333
		,@Title nvarchar(200) = 'test title'
		,@Description nvarchar(4000) = 'test description'
		,@Requirements nvarchar(3000) = 'test requirements'
		,@IsActive bit = 0
		,@ContactName nvarchar(100) = 'John Doe'
		,@ContactPhone nvarchar(20) = '(555) 123-4567'
		,@ContactEmail nvarchar(200) = 'test@email.com'
		,@UserId int = 999
		,@Id int = 0
			

	EXEC [dbo].[Jobs_Insert]
						@JobTypeId
						,@LocationId
						,@OrganizationId
						,@Title 
						,@Description 
						,@Requirements 
						,@IsActive 
						,@ContactName
						,@ContactPhone
						,@ContactEmail
						,@UserId
						,@Id OUTPUT

	Select *
	From [dbo].[Jobs]
	Where Id = @Id
*/

AS

BEGIN

INSERT INTO [dbo].[Jobs]
           ([JobTypeId]
           ,[Title]
           ,[Description]
           ,[Requirements]
           ,[IsActive]
		   ,[OrganizationId]
		   ,[LocationId]
           ,[ContactName]
           ,[ContactPhone]
           ,[ContactEmail]
           ,[CreatedBy]
           ,[ModifiedBy]
           ,[DateCreated]
           ,[DateModified])
     VALUES
           (@JobTypeId
			,@Title 
			,@Description 
			,@Requirements
			,@IsActive 
			,@OrganizationId
			,@LocationId
			,@ContactName
			,@ContactPhone
			,@ContactEmail
			,@UserId
			,@UserId
			,GETUTCDATE()
			,GETUTCDATE())

		SET @Id = SCOPE_IDENTITY()
					

END
GO
/****** Object:  StoredProcedure [dbo].[Jobs_InsertV2]    Script Date: 12/2/2022 3:13:23 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author: <Nicholas Rodriguez>
-- Create date: <07/01/2022>
-- Description: <Jobs_Insert>
-- Code Reviewer:

-- MODIFIED BY: author
-- MODIFIED DATE:12/1/2020
-- Code Reviewer:
-- Note:
-- =============================================
Create PROC [dbo].[Jobs_InsertV2]
					@JobTypeId int
					,@Title nvarchar(200)
					,@Description nvarchar(4000)
					,@Requirements nvarchar(3000)
					,@IsActive bit
					,@OrganizationId int
			        ,@LocationTypeId int
			        ,@LineOne nvarchar(255)
			        ,@LineTwo nvarchar(255)
			        ,@City nvarchar(225)
			        ,@Zip nvarchar(50)
		 	        ,@StateId int
			        ,@Latitude float 
          			,@Longitude float
					,@ContactName nvarchar(100)
					,@ContactPhone nvarchar(20)
					,@ContactEmail nvarchar(200)
					,@CreatedBy int
					,@Id int OUTPUT

/*---------------Test Code--------------
DECLARE @JobTypeId int = 111
		,@LocationId int = 222
		,@OrganizationId int = 333
		,@Title nvarchar(200) = 'test title'
		,@Description nvarchar(4000) = 'test description'
		,@Requirements nvarchar(3000) = 'test requirements'
		,@IsActive bit = 0
		,@ContactName nvarchar(100) = 'John Doe'
		,@ContactPhone nvarchar(20) = '(555) 123-4567'
		,@ContactEmail nvarchar(200) = 'test@email.com'
		,@UserId int = 999
		,@Id int = 0
			

	EXEC [dbo].[Jobs_Insert]
						@JobTypeId
						,@LocationId
						,@OrganizationId
						,@Title 
						,@Description 
						,@Requirements 
						,@IsActive 
						,@ContactName
						,@ContactPhone
						,@ContactEmail
						,@UserId
						,@Id OUTPUT

	Select *
	From [dbo].[Jobs]
	Where Id = @Id
*/

AS

BEGIN

		
		Declare @LocationId int
				,@DateModified datetime2 = GETUTCDATE()
		

		INSERT INTO [dbo].[Locations]
				   ([LocationTypeId]
				   ,[LineOne]
				   ,[LineTwo]
				   ,[City]
				   ,[Zip]
				   ,[StateId]
				   ,[Latitude]
				   ,[Longitude]
				   ,[CreatedBy])
			 VALUES
				   (@LocationTypeId
				   ,@LineOne
				   ,@LineTwo
				   ,@City
				   ,@Zip
				   ,@StateId
				   ,@Latitude
				   ,@Longitude
				   ,@CreatedBy)

		SET @LocationId = SCOPE_IDENTITY()


INSERT INTO [dbo].[Jobs]
           ([JobTypeId]
           ,[Title]
           ,[Description]
           ,[Requirements]
           ,[IsActive]
		   ,[OrganizationId]
		   ,[LocationId]
           ,[ContactName]
           ,[ContactPhone]
           ,[ContactEmail]
           ,[CreatedBy]
           ,[ModifiedBy]
           ,[DateCreated]
           ,[DateModified])
     VALUES
           (@JobTypeId
			,@Title 
			,@Description 
			,@Requirements
			,@IsActive 
			,@OrganizationId
			,@LocationId
			,@ContactName
			,@ContactPhone
			,@ContactEmail
			,@CreatedBy
			,@CreatedBy
			,@DateModified
			,@DateModified)

		SET @Id = SCOPE_IDENTITY()
					

END
GO
/****** Object:  StoredProcedure [dbo].[Jobs_Search_Pagination]    Script Date: 12/2/2022 3:13:23 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

 -- =============================================
-- Author: <Jordan Poole>
-- Create date: <07/18/2022>
-- Description: <Jobs_Search_Pagination>
-- Code Reviewer:

-- MODIFIED BY: <Thomas Sauer>
-- MODIFIED DATE:<07/21/2022?
-- Code Reviewer:
-- Note: Changed this proc to give back the same values as the Select_ById, SelectAll and Select_ByCreatedBy Procs 
-- ============================================= 
 CREATE PROC [dbo].[Jobs_Search_Pagination]   @PageIndex int ,@PageSize int ,@Query nvarchar(50)

		
AS
/*-----TEST CODE-----

Declare @PageIndex int = 0
		,@PageSize int = 10
		,@Query nvarchar(50) = 'civil'



EXECUTE [dbo].[Jobs_Search_Pagination ]
		@PageIndex
		,@PageSize
		,@Query

	

		EXECUTE dbo.jobs_selectall @PageIndex 
		,@PageSize

-----END TEST CODE-----
*/

BEGIN

	DECLARE 
		@offset int = @PageIndex * @PageSize

SELECT j.[Id]
			  ,j.[JobTypeId] AS [JobTypeId] 
			  ,jt.[Name] AS [JobTypeName]
			  ,j.[Title]
			  ,j.[Description]
			  ,j.[Requirements]
			  ,j.[LocationId] AS [Location]
			  ,l.[LocationTypeId] AS [LocationType]
			  ,lt.[Name] AS [LocationTypeName]
			  ,l.[LineOne]
			  ,l.[LineTwo]
			  ,l.[City]
			  ,l.[Zip]
			  ,l.[StateId] As [StateId]
			  ,s.[Name]
			  ,s.[Code]
			  ,l.[Latitude]
			  ,l.[Longitude]
			  ,j.[OrganizationId] AS [Organization]
			  ,o.[OrganizationTypeId] AS [OrganizationType] 
			  ,ot.[Name]
			  ,o.[Name]
			  ,o.[Headline]
			  ,o.[Description]
			  ,o.[Logo]
			  ,o.[Phone]
			  ,o.[SiteUrl]
			  ,up.[Id]
			  ,j.[CreatedBy]
			  ,up.FirstName
			  ,up.LastName
			  ,up.Mi
			  ,up.AvatarUrl
			  ,j.[DateCreated]
			  ,j.[DateModified]
			  ,TotalCount = COUNT(1) OVER()


			  
from dbo.Jobs as j
left outer join dbo.Organizations as o on o.Id = j.OrganizationId
left outer join dbo.Locations as l on l.id = j.LocationId
inner join dbo.JobTypes as jt on jt.Id = j.JobTypeId
left outer join dbo.LocationTypes as lt on lt.Id = l.LocationTypeId
left outer join dbo.OrganizationTypes as ot on ot.Id = o.OrganizationTypeId
left outer join dbo.UserProfiles as up on up.UserId = j.CreatedBy
left outer join dbo.States as s on s.Id = l.StateId

	WHERE	j.Title LIKE  '%' + @Query + '%'
		OR  j.Description LIKE '%' + @Query + '%'
		OR  j.Requirements LIKE '%' + @Query + '%'
		OR  l.Zip LIKE '%' + @Query + '%'
		OR  l.City LIKE '%' + @Query + '%'

	ORDER BY j.Id

	OFFSET @offSet Rows
	Fetch Next @PageSize Rows ONLY

END
GO
/****** Object:  StoredProcedure [dbo].[Jobs_Search_Pagination_By_Location_Only]    Script Date: 12/2/2022 3:13:23 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

 -- =============================================
-- Author: <Jordan Poole>
-- Create date: <07/27/2022>
-- Description: <Jobs_Search_Pagination_By_Location_Only>
-- Code Reviewer:

-- MODIFIED BY: <Thomas Sauer>
-- MODIFIED DATE:<07/21/2022?
-- Code Reviewer:
-- Note: Changed this proc to give back the same values as the Select_ById, SelectAll and Select_ByCreatedBy Procs 
-- ============================================= 
 CREATE PROC [dbo].[Jobs_Search_Pagination_By_Location_Only]   @PageIndex int ,@PageSize int ,@Query nvarchar(50)

		
AS
/*-----TEST CODE-----

Declare @PageIndex int = 0
		,@PageSize int = 10
		,@Query nvarchar(50) = '84144'



EXECUTE [dbo].[Jobs_Search_Pagination_By_Location_Only ]
		@PageIndex
		,@PageSize
		,@Query

	

		EXECUTE dbo.jobs_selectall @PageIndex 
		,@PageSize

-----END TEST CODE-----
*/

BEGIN

	DECLARE 
		@offset int = @PageIndex * @PageSize

SELECT j.[Id]
			  ,j.[JobTypeId] AS [JobType] 
			  ,jt.[Type]
			  ,j.[Title]
			  ,j.[Description]
			  ,j.[Requirements]
			  ,j.[LocationId] AS [Location]
			  ,l.[LocationTypeId] AS [LocationType]
			  ,lt.[Name]
			  ,l.[LineOne]
			  ,l.[LineTwo]
			  ,l.[City]
			  ,l.[Zip]
			  ,l.[StateId] As [StateId]
			  ,s.[Name]
			  ,s.[Code]
			  ,l.[Latitude]
			  ,l.[Longitude]
			  ,j.[OrganizationId] AS [Organization]
			  ,o.[OrganizationTypeId] AS [OrganizationType] 
			  ,ot.[Name]
			  ,o.[Name]
			  ,o.[Headline]
			  ,o.[Description]
			  ,o.[Logo]
			  ,o.[Phone]
			  ,o.[SiteUrl]
			  ,up.[Id]
			  ,j.[CreatedBy]
			  ,up.FirstName
			  ,up.LastName
			  ,up.Mi
			  ,up.AvatarUrl
			  ,j.[DateCreated]
			  ,j.[DateModified]
			  ,TotalCount = COUNT(1) OVER()


			  
from dbo.Jobs as j
left outer join dbo.Organizations as o on o.Id = j.OrganizationId
inner join dbo.Locations as l on l.id = j.LocationId
inner join dbo.JobTypes as jt on jt.Id = j.JobTypeId
left outer join dbo.LocationTypes as lt on lt.Id = l.LocationTypeId
left outer join dbo.OrganizationTypes as ot on ot.Id = o.OrganizationTypeId
left outer join dbo.UserProfiles as up on up.UserId = j.CreatedBy
left outer join dbo.States as s on s.Id = l.StateId

	WHERE	l.Zip LIKE  '%' + @Query + '%'
		
	ORDER BY j.Id

	OFFSET @offSet Rows
	Fetch Next @PageSize Rows ONLY

END
GO
/****** Object:  StoredProcedure [dbo].[Jobs_Search_Pagination_Joined]    Script Date: 12/2/2022 3:13:23 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
 -- =============================================
-- Author: <Jordan Poole>
-- Create date: <07/18/2022>
-- Description: <Jobs_Search_Pagination>
-- Code Reviewer:

-- MODIFIED BY: author
-- MODIFIED DATE:12/1/2020
-- Code Reviewer:
-- Note:
-- ============================================= 
 CREATE PROC [dbo].[Jobs_Search_Pagination_Joined]   @PageIndex int ,@PageSize int ,@Query nvarchar(50)

		
AS
/*-----TEST CODE-----

Declare @PageIndex int = 0
		,@PageSize int = 3
		,@Query nvarchar(50) = 'eng'



EXECUTE [dbo].[Jobs_Search_Pagination ]
		@PageIndex
		,@PageSize
		,@Query


-----END TEST CODE-----
*/

BEGIN

   Declare @offset int = @PageIndex * @PageSize

        SELECT   Id
			,JobTypeId
			,LocationId
			,OrganizationId
			,Title
			,Description 
			,Requirements 
			,IsActive
			,ContactName
			,ContactPhone
			,ContactEmail
			,CreatedBy
			,ModifiedBy
			,DateCreated
			,DateModified
			

                , TotalCount = COUNT(1) OVER() -- this the quick way of doing the count. (see below)
    FROM    dbo.Jobs
	

	WHERE Title LIKE  '%' + @Query + '%'
	OR  Description LIKE '%' + @Query + '%'
	OR  Requirements LIKE '%' + @Query + '%'
	ORDER BY Id

	OFFSET @offSet Rows
	Fetch Next @PageSize Rows ONLY

END
GO
/****** Object:  StoredProcedure [dbo].[Jobs_Search_Pagination_With_Location_Radius]    Script Date: 12/2/2022 3:13:23 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

 -- =============================================
-- Author: <Jordan Poole>
-- Create date: <07/27/2022>
-- Description: <Jobs_Search_Pagination_With_Location_Radius>
-- Code Reviewer: <Tim Rawson>

-- MODIFIED BY: <>
-- MODIFIED DATE:<>
-- Code Reviewer:
-- Note:  
-- ============================================= 
 CREATE PROC [dbo].[Jobs_Search_Pagination_With_Location_Radius]  
				@PageIndex int
				,@PageSize int
				,@Query nvarchar(50)
				,@Lat decimal(10,7)
				,@Long decimal(10,7)
				,@Distance int


		
AS
/*-----TEST CODE-----

		Declare @PageIndex int = 0
				,@PageSize int = 10
				,@Query nvarchar(50) = 'engineer'
				,@Lat decimal(10,7) = 32.7269669
				,@Long decimal(10,7) = -118.1647094
				,@Distance int = 100


		EXECUTE [dbo].[Jobs_Search_Pagination_With_Location_Radius ]
				@PageIndex
				,@PageSize
				,@Query
				,@Lat
				,@Long
				,@Distance
		

	


-----END TEST CODE-----
*/

BEGIN




		DECLARE 
		@offset int = @PageIndex * @PageSize

		  ;WITH AllRecords AS (
		SELECT j.[Id] AS [JobId]
			  ,j.[JobTypeId] AS [JobTypeId] 
			  ,jt.[Name] AS [JobTypeName]
			  ,j.[Title] AS [JobTitle]
			  ,j.[Description] AS [JobDescription]
			  ,j.[Requirements] AS [JobRequirements]
			  ,j.[LocationId] AS [LocationId]
			  ,l.[LocationTypeId] AS [LocationTypeId]
			  ,lt.[Name] AS [LocationTypeName]
			  ,l.[LineOne] 
			  ,l.[LineTwo]
			  ,l.[City]
			  ,l.[Zip]
			  ,l.[StateId] AS [StateId]
			  ,s.[Name] AS [StateName]
			  ,s.[Code] AS [StateCode]
			  ,l.[Latitude]
			  ,l.[Longitude]
			  ,j.[OrganizationId] AS [OrganizationId]
			  ,o.[OrganizationTypeId] AS [OrganizationTypeId] 
			  ,ot.[Name] AS [OrgTypeName]
			  ,o.[Name] AS [OrgName]
			  ,o.[Headline] AS [OrgHeadline]
			  ,o.[Description] AS [OrgDescription]
			  ,o.[Logo] 
			  ,o.[Phone]
			  ,o.[SiteUrl]
			  ,up.[Id] AS [UserId]
			  ,j.[CreatedBy]
			  ,up.FirstName
			  ,up.LastName
			  ,up.Mi
			  ,up.AvatarUrl
			  ,j.[DateCreated]
			  ,j.[DateModified]
			  ,TotalCount = COUNT(1) OVER()
			  ,( 3959 * acos( cos( radians(@lat) ) * cos( radians( l.Latitude ) ) * cos( radians(l.Longitude ) - radians(@Long) ) + sin( radians(@Lat) ) * sin( radians(l.Latitude) ) ) ) AS Distance
		

			FROM dbo.Jobs AS j
			left outer join dbo.Organizations AS o ON o.Id = j.OrganizationId
			inner join dbo.Locations AS l ON l.id = j.LocationId
			inner join dbo.JobTypes AS jt ON jt.Id = j.JobTypeId
			left outer join dbo.LocationTypes AS lt ON lt.Id = l.LocationTypeId
			left outer join dbo.OrganizationTypes AS ot ON ot.Id = o.OrganizationTypeId
			left outer join dbo.UserProfiles AS up ON up.UserId = j.CreatedBy
			left outer join dbo.States AS s ON s.Id = l.StateId

	

			ORDER BY j.Id

	OFFSET @offSet ROWS
	FETCH NEXT @PageSize ROWS ONLY
	)
	, filteredRecords as (
			SELECT  *
			FROM    AllRecords
			WHERE   distance <= @distance  
			AND	(AllRecords.JobTitle LIKE  '%' + @Query + '%'
			OR AllRecords.JobDescription LIKE '%' + @Query + '%'
			OR AllRecords.JobRequirements LIKE '%' + @Query + '%')
		
		)
	SELECT
			filteredRecords.[JobId]
			,filteredRecords.[JobTypeId]
			,filteredRecords.[JobTypeName]
			,filteredRecords.[JobTitle]
			,filteredRecords.[JobDescription]
			,filteredRecords.[JobRequirements]
			,filteredRecords.[LocationId]
			,filteredRecords.[LocationTypeId]
			,filteredRecords.[LocationTypeName]
			,filteredRecords.[LineOne]
			,filteredRecords.[LineTwo]
			,filteredRecords.[City]
			,filteredRecords.[Zip]
			,filteredRecords.[StateId]
			,filteredRecords.[StateName]
			,filteredRecords.[StateCode]
			,filteredRecords.[Latitude]
			,filteredRecords.[Longitude]
			,filteredRecords.[OrganizationId]
			,filteredRecords.[OrganizationTypeId]
			,filteredRecords.[OrgTypeName]
			,filteredRecords.[OrgName]
			,filteredRecords.[OrgHeadline]
			,filteredRecords.[OrgDescription]
			,filteredRecords.[Logo]
			,filteredRecords.[Phone]
			,filteredRecords.[SiteUrl]
			,filteredRecords.[UserId]
			,filteredRecords.[CreatedBy]
			,filteredRecords.[FirstName]
			,filteredRecords.[LastName]
			,filteredRecords.[Mi]
			,filteredRecords.[AvatarUrl]
			,filteredRecords.[DateCreated]
			,filteredRecords.[DateModified]
			,filteredRecords.[Distance]
			,TotalCount = COUNT(1) OVER()
    FROM    filteredRecords
    ORDER BY distance ASC;		
	

END
GO
/****** Object:  StoredProcedure [dbo].[Jobs_SearchPaginated]    Script Date: 12/2/2022 3:13:23 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author: <Nicholas Rodriguez>
-- Create date: <07/01/2022>
-- Description: <Jobs_Insert>
-- Code Reviewer:

-- MODIFIED BY: author
-- MODIFIED DATE:12/1/2020
-- Code Reviewer:
-- Note:
-- =============================================
CREATE PROC [dbo].[Jobs_SearchPaginated]
					@JobTypeId int
					,@LocationId int
					,@OrganizationId int
					,@Title nvarchar(200)
					,@Description nvarchar(4000)
					,@Requirements nvarchar(3000)
					,@IsActive bit
					,@ContactName nvarchar(100)
					,@ContactPhone nvarchar(20)
					,@ContactEmail nvarchar(200)
					,@UserId int
					,@Id int OUTPUT

/*---------------Test Code--------------
DECLARE @JobTypeId int = 111
		,@LocationId int = 222
		,@OrganizationId int = 333
		,@Title nvarchar(200) = 'test title'
		,@Description nvarchar(4000) = 'test description'
		,@Requirements nvarchar(3000) = 'test requirements'
		,@IsActive bit = 0
		,@ContactName nvarchar(100) = 'John Doe'
		,@ContactPhone nvarchar(20) = '(555) 123-4567'
		,@ContactEmail nvarchar(200) = 'test@email.com'
		,@UserId int = 999
		,@Id int = 0
			

	EXEC [dbo].[Jobs_SearchPaginated]
						@JobTypeId
						,@LocationId
						,@OrganizationId
						,@Title 
						,@Description 
						,@Requirements 
						,@IsActive 
						,@ContactName
						,@ContactPhone
						,@ContactEmail
						,@UserId
						,@Id OUTPUT

	Select *
	From [dbo].[Jobs]
	Where Id = @Id
*/

AS

BEGIN

INSERT INTO [dbo].[Jobs]
           ([JobTypeId]
           ,[LocationId]
           ,[OrganizationId]
           ,[Title]
           ,[Description]
           ,[Requirements]
           ,[IsActive]
           ,[ContactName]
           ,[ContactPhone]
           ,[ContactEmail]
           ,[CreatedBy]
           ,[ModifiedBy]
           ,[DateCreated]
           ,[DateModified])
     VALUES
           (@JobTypeId
			,@LocationId
			,@OrganizationId
			,@Title 
			,@Description 
			,@Requirements 
			,@IsActive 
			,@ContactName
			,@ContactPhone
			,@ContactEmail
			,@UserId
			,@UserId
			,GETUTCDATE()
			,GETUTCDATE())

		SET @Id = SCOPE_IDENTITY()
					

END
GO
/****** Object:  StoredProcedure [dbo].[Jobs_Select_ByCreatedBy]    Script Date: 12/2/2022 3:13:23 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

-- =============================================
-- Author: <Thomas Sauer>
-- Create date: <07/20/2022>

-- Description: <Jobs_SelectByCreatedBy brings in all relevant data belonging to jobs and includes
-- joined tables such as Locations, Organizations and States to bring a comprehenensive amount of
-- information related to Jobs by Id and orders it by CreatedBy>

-- Code Reviewer: <Joshua Eslava>
-- =============================================
	CREATE PROC [dbo].[Jobs_Select_ByCreatedBy]
				@CreatedBy int
				,@PageIndex int
				,@PageSize int

	AS

	/*

		Declare @CreatedBy int = 1
				,@PageIndex int = 0
				,@PageSize int = 50

		Execute dbo.Jobs_Select_ByCreatedBy @CreatedBy
												,@PageIndex
												,@PageSize

	*/

	BEGIN

		Declare @Offset int = @PageIndex * @PageSize
		
		SELECT j.[Id]
			  ,j.[JobTypeId] AS [JobType] 
			  ,j.[Title]
			  ,j.[Description]
			  ,j.[Requirements]
			  ,t.[Name]
			  ,j.[LocationId] AS [Location]
			  ,l.[LocationTypeId] AS [LocationType]
			  ,lt.[Name]
			  ,l.[LineOne]
			  ,l.[LineTwo]
			  ,l.[City]
			  ,l.[Zip]
			  ,l.[StateId] As [StateId]
			  ,s.[Name]
			  ,s.[Code]
			  ,l.[Latitude]
			  ,l.[Longitude]
			  ,j.[OrganizationId] AS [Organization]
			  ,o.[OrganizationTypeId] AS [OrganizationType] 
			  ,ot.[Name]
			  ,o.[Name]
			  ,o.[Headline]
			  ,o.[Description]
			  ,o.[Logo]
			  ,o.[Phone]
			  ,o.[SiteUrl]
			  ,up.[Id]
			  ,j.[CreatedBy]
			  ,up.FirstName
			  ,up.LastName
			  ,up.Mi
			  ,up.AvatarUrl
			  ,j.[DateCreated]
			  ,j.[DateModified]
			  ,TotalCount = COUNT(1) OVER()


			  

		FROM [dbo].[Jobs] AS j INNER JOIN dbo.Locations as l
		ON l.LocationTypeId = j.LocationId INNER JOIN dbo.Organizations as o
		ON j.OrganizationId = o.OrganizationTypeId INNER JOIN dbo.States as s
		ON l.StateId = s.Id INNER JOIN dbo.JobTypes as t 
		ON j.JobTypeId = t.Id Inner join  dbo.UserProfiles as up 
		ON up.UserId = j.CreatedBy inner join dbo.LocationTypes as lt  
		ON lt.Id = l.LocationTypeId inner join dbo.OrganizationTypes as ot 
		ON ot.Id = j.OrganizationId

		WHERE j.CreatedBy = @CreatedBy

		ORDER BY j.CreatedBy

		OFFSET @Offset ROWS
		FETCH NEXT @PageSize ROWS ONLY


	END
GO
/****** Object:  StoredProcedure [dbo].[Jobs_Select_ByOrganization]    Script Date: 12/2/2022 3:13:23 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE proc [dbo].[Jobs_Select_ByOrganization]
	@OrganizationId int

as

/*  Test Code

	Declare @OrganizationId int = 175

	Execute dbo.Jobs_Select_ByOrganization
		@OrganizationId


*/

Begin 
	Select j.Id,
		   j.Title,
		   j.Description,
		   j.Requirements,
		   jt.Name as JobType,
		   l.City,
		   st.Name as State,
		   l.Zip as ZipCode,
		   j.DateCreated
	FROM dbo.Jobs as j inner join dbo.Locations as l
	on j.LocationId = l.Id
	inner join dbo.JobTypes as jt
	on jt.Id = j.JobTypeId
	inner join dbo.States as st
	on  l.StateId = st.Id
	Where j.OrganizationId = @OrganizationId

End
GO
/****** Object:  StoredProcedure [dbo].[Jobs_Select_ByOrganization_Base]    Script Date: 12/2/2022 3:13:23 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE proc [dbo].[Jobs_Select_ByOrganization_Base]
	@OrganizationId int

as

/*  Test Code

	Declare @OrganizationId int = 46

	Execute dbo.Jobs_Select_ByOrganization
		@OrganizationId


*/

Begin 
	Select j.Id,
		   j.Title,
		   j.Description,
		   j.Requirements
	FROM dbo.Jobs as j
	Where j.OrganizationId = @OrganizationId

End
GO
/****** Object:  StoredProcedure [dbo].[Jobs_Select_ByOrganization_Paginated]    Script Date: 12/2/2022 3:13:23 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE proc [dbo].[Jobs_Select_ByOrganization_Paginated]
	@OrganizationId int,
	@PageIndex int,
	@PageSize int

as

/*  Test Code

	Declare @OrganizationId int = 264
	, @PageIndex int = 0
	, @PageSize int = 5

	Execute dbo.Jobs_Select_ByOrganization_Paginated
		@OrganizationId
		,@PageIndex
		,@PageSize


*/

Begin 

	DECLARE @offset int = @PageIndex * @PageSize
	Select j.Id,
		   j.Title,
		   j.Description,
		   j.Requirements,
		   j.IsActive,
		   j.OrganizationId,
		   o.OrganizationTypeId,
		   ot.Name,
		   o.Name,
		   o.Headline,
		   o.Description,
		   o.Logo,
		   o.Phone,
		   o.SiteUrl,
		   j.ContactName,
		   j.ContactPhone,
		   j.ContactEmail,
		   up.Id,
		   j.CreatedBy,
		   up.FirstName,
		   up.LastName,
		   up.Mi,
		   up.AvatarUrl,
		   jt.Id,
		   jt.Name as JobType,
		   l.LocationTypeId,
		   lt.Name,
		   j.LocationId,
		   l.LineOne,
		   l.LineTwo,
		   l.City,
		   st.Name as State,
		   l.Zip as ZipCode,
		   l.StateId,
		   l.Latitude,
		   l.Longitude,
		   j.DateCreated
		   ,TotalCount = COUNT(1) OVER()
	FROM dbo.Jobs as j inner join dbo.Locations as l
	on j.LocationId = l.Id
	inner join dbo.JobTypes as jt
	on jt.Id = j.JobTypeId
	inner join dbo.States as st
	on  l.StateId = st.Id
	inner join dbo.UserProfiles as up
	on up.UserId = j.CreatedBy
	inner join dbo.LocationTypes as lt
	on lt.Id = l.LocationTypeId
	inner join dbo.Organizations as o
	on j.OrganizationId = o.Id
	inner join dbo.OrganizationTypes as ot
	on ot.Id = o.OrganizationTypeId
	Where j.OrganizationId = @OrganizationId
	ORDER BY j.Id
	OFFSET @offset Rows
	Fetch Next @PageSize Rows ONLY

End
GO
/****** Object:  StoredProcedure [dbo].[Jobs_SelectAll]    Script Date: 12/2/2022 3:13:23 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
	-- =============================================
-- Author: <Thomas Sauer>
-- Create date: <07/20/2022>

-- Description: <Jobs_SelectAll brings in all relevant data belonging to jobs and includes
-- joined tables such as Locations, Organizations and States to bring a comprehenensive amount of
-- information related to Jobs by Id>

-- Code Reviewer: <Joshua Eslava>

-- =============================================
	
	
	CREATE PROC [dbo].[Jobs_SelectAll]

				 @PageIndex int
				,@PageSize int

	AS

	/*


				DECLARE @PageIndex int = 0
				,@PageSize int = 50

		Execute dbo.Jobs_SelectAll 
												@PageIndex
												,@PageSize

	*/

	BEGIN

		Declare @Offset int = @PageIndex * @PageSize
		
		SELECT j.[Id]
			  ,j.[JobTypeId] AS [JobType] 
			  ,t.[Name]
			  ,j.[Title]
			  ,j.[Description]
			  ,j.[Requirements]
			  ,j.[LocationId] AS [Location]
			  ,l.[LocationTypeId] AS [LocationType]
			  ,lt.[Name]
			  ,l.[LineOne]
			  ,l.[LineTwo]
			  ,l.[City]
			  ,l.[Zip]
			  ,l.[StateId] As [StateId]
			  ,s.[Name]
			  ,s.[Code]
			  ,l.[Latitude]
			  ,l.[Longitude]
			  ,j.[OrganizationId] AS [Organization]
			  ,o.[OrganizationTypeId] AS [OrganizationType] 
			  ,ot.[Name]
			  ,o.[Name]
			  ,o.[Headline]
			  ,o.[Description]
			  ,o.[Logo]
			  ,o.[Phone]
			  ,o.[SiteUrl]
			  ,up.[Id]
			  ,j.[CreatedBy]
			  ,up.FirstName
			  ,up.LastName
			  ,up.Mi
			  ,up.AvatarUrl
			  ,j.[DateCreated]
			  ,j.[DateModified]
			  ,TotalCount = COUNT(1) OVER()


			  

		FROM [dbo].[Jobs] AS j INNER JOIN dbo.Locations as l
		ON l.LocationTypeId = j.LocationId INNER JOIN dbo.Organizations as o
		ON j.OrganizationId = o.OrganizationTypeId INNER JOIN dbo.States as s
		ON l.StateId = s.Id INNER JOIN dbo.JobTypes as t 
		ON j.JobTypeId = t.Id Inner join  dbo.UserProfiles as up 
		ON up.UserId = j.CreatedBy inner join dbo.LocationTypes as lt  
		ON lt.Id = l.LocationTypeId inner join dbo.OrganizationTypes as ot 
		ON ot.Id = j.OrganizationId



		ORDER BY j.Id

		OFFSET @Offset ROWS
		FETCH NEXT @PageSize ROWS ONLY


	END
GO
/****** Object:  StoredProcedure [dbo].[Jobs_SelectById]    Script Date: 12/2/2022 3:13:23 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
	-- =============================================
-- Author: <Thomas Sauer>
-- Create date: <07/20/2022>

-- Description: <Jobs_SelectById brings in all relevant data belonging to jobs and includes
-- joined tables such as Locations, Organizations and States to bring a comprehenensive amount of
-- information related to Jobs by Id>

-- Code Reviewer: <Joshua Eslava>

-- =============================================
	
	
	CREATE PROC [dbo].[Jobs_SelectById]

				 @Id int

	AS

	/*


		DECLARE @Id int = 111

		Execute dbo.Jobs_SelectById
				@Id
											

	*/

	BEGIN
		
		SELECT j.[Id]
			  ,j.[JobTypeId] AS [JobType] 
			  ,j.[Title]
			  ,j.[Description]
			  ,j.[Requirements]
			  ,t.[Name]
			  ,j.[LocationId] AS [Location]
			  ,l.[LocationTypeId] AS [LocationType]
			  ,lt.[Name]
			  ,l.[LineOne]
			  ,l.[LineTwo]
			  ,l.[City]
			  ,l.[StateId] As [StateId]
			  ,s.[Name]
			  ,s.[Code]
			 
			  ,j.[OrganizationId] AS [Organization]
			  ,o.[OrganizationTypeId] AS [OrganizationType] 
			  ,ot.[Name]
			  ,o.[Name]
			  ,o.[Headline]
			  ,o.[Description]
			  ,o.[Logo]
			  ,o.[Phone]
			  ,o.[SiteUrl]
			  ,up.[Id]
			  ,j.[CreatedBy]
			  ,up.FirstName
			  ,up.LastName
			  ,up.Mi
			  ,up.AvatarUrl
			  ,j.[DateCreated]
			  ,j.[DateModified]
			  ,TotalCount = COUNT(1) OVER()


			  

		FROM [dbo].[Jobs] AS j INNER JOIN dbo.Locations as l
		ON l.LocationTypeId = j.LocationId INNER JOIN dbo.Organizations as o
		ON j.OrganizationId = o.OrganizationTypeId INNER JOIN dbo.States as s
		ON l.StateId = s.Id INNER JOIN dbo.JobTypes as t 
		ON j.JobTypeId = t.Id Inner join  dbo.UserProfiles as up 
		ON up.UserId = j.CreatedBy inner join dbo.LocationTypes as lt  
		ON lt.Id = l.LocationTypeId inner join dbo.OrganizationTypes as ot 
		ON ot.Id = j.OrganizationId

		WHERE j.Id = @Id


	END
GO
/****** Object:  StoredProcedure [dbo].[Jobs_Update]    Script Date: 12/2/2022 3:13:23 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author: <Nicholas Rodriguez>
-- Create date: <07/01/2022>
-- Description: <Jobs_Update>
-- Code Reviewer:

-- MODIFIED BY: author
-- MODIFIED DATE:12/1/2020
-- Code Reviewer:
-- Note:
-- =============================================
CREATE PROC [dbo].[Jobs_Update]
					@JobTypeId int
					,@LocationId int
					,@OrganizationId int
					,@Title nvarchar(200)
					,@Description nvarchar(4000)
					,@Requirements nvarchar(3000)
					,@IsActive bit
					,@ContactName nvarchar(100)
					,@ContactPhone nvarchar(20)
					,@ContactEmail nvarchar(200)
					,@UserId int
					,@Id int 

/*---------------Test Code--------------
DECLARE @JobTypeId int = 111
		,@LocationId int = 222
		,@OrganizationId int = 333
		,@Title nvarchar(200) = 'updated title'
		,@Description nvarchar(4000) = 'updated description'
		,@Requirements nvarchar(3000) = 'updated requirements'
		,@IsActive bit = 0
		,@ContactName nvarchar(100) = 'John Doe'
		,@ContactPhone nvarchar(20) = '(555) 123-4567'
		,@ContactEmail nvarchar(200) = 'test@email.com'
		,@UserId int = 555
		,@Id int = 1
			
SELECT *
FROM [dbo].[Jobs]
WHERE Id = @Id

	EXEC [dbo].[Jobs_Update]
						@JobTypeId
						,@LocationId
						,@OrganizationId
						,@Title 
						,@Description 
						,@Requirements 
						,@IsActive 
						,@ContactName
						,@ContactPhone
						,@ContactEmail
						,@UserId
						,@Id 

SELECT *
FROM [dbo].[Jobs]
WHERE Id = @Id

*/

AS

BEGIN

UPDATE [dbo].[Jobs]
   SET [JobTypeId] = @JobTypeId
      ,[LocationId] = @LocationId
      ,[OrganizationId] = @OrganizationId
      ,[Title] = @Title
      ,[Description] = @Description
      ,[Requirements] = @Requirements
      ,[IsActive] = @IsActive
      ,[ContactName] = @ContactName
      ,[ContactPhone] = @ContactPhone
      ,[ContactEmail] = @ContactEmail
      ,[ModifiedBy] = @UserId
      ,[DateModified] = GETUTCDATE()
 WHERE Id = @Id
					
END
GO
/****** Object:  StoredProcedure [dbo].[Jobs_UpdateV2]    Script Date: 12/2/2022 3:13:23 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author: <Nicholas Rodriguez>
-- Create date: <07/01/2022>
-- Description: <Jobs_Update>
-- Code Reviewer:

-- MODIFIED BY: author
-- MODIFIED DATE:12/1/2020
-- Code Reviewer:
-- Note:
-- =============================================
CREATE PROC [dbo].[Jobs_UpdateV2]
					@JobTypeId int
					,@Title nvarchar(200)
					,@Description nvarchar(4000)
					,@Requirements nvarchar(3000)
					,@IsActive bit
					,@OrganizationId int
					,@LocationTypeId int
					,@LocationId int
					,@LineOne nvarchar(255)
					,@LineTwo nvarchar(255)
			        ,@City nvarchar(225)
			        ,@Zip nvarchar(50)
		 	        ,@StateId int
			        ,@Latitude float 
          			,@Longitude float
					,@ContactName nvarchar(100)
					,@ContactPhone nvarchar(20)
					,@ContactEmail nvarchar(200)
					,@UserId int
					,@Id int 

/*---------------Test Code--------------
DECLARE @JobTypeId int = 111
		,@LocationId int = 222
		,@OrganizationId int = 333
		,@Title nvarchar(200) = 'updated title'
		,@Description nvarchar(4000) = 'updated description'
		,@Requirements nvarchar(3000) = 'updated requirements'
		,@IsActive bit = 0
		,@ContactName nvarchar(100) = 'John Doe'
		,@ContactPhone nvarchar(20) = '(555) 123-4567'
		,@ContactEmail nvarchar(200) = 'test@email.com'
		,@UserId int = 555
		,@Id int = 1
			
SELECT *
FROM [dbo].[Jobs]
WHERE Id = @Id

	EXEC [dbo].[Jobs_Update]
						@JobTypeId
						,@LocationId
						,@OrganizationId
						,@Title 
						,@Description 
						,@Requirements 
						,@IsActive 
						,@ContactName
						,@ContactPhone
						,@ContactEmail
						,@UserId
						,@Id 

SELECT *
FROM [dbo].[Jobs]
WHERE Id = @Id

*/

AS

BEGIN

UPDATE [dbo].[Locations]
   SET [LocationTypeId] = @LocationTypeId
      ,[LineOne] = @LineOne
      ,[LineTwo] = @LineTwo
      ,[City] = @City
      ,[Zip] = @Zip
      ,[StateId] = @StateId
      ,[Latitude] = @Latitude
      ,[Longitude] = @Longitude
      ,[DateModified] = GETUTCDATE()
      ,[ModifiedBy] = @UserId
 WHERE Id = @LocationId

UPDATE [dbo].[Jobs]
   SET [JobTypeId] = @JobTypeId
      ,[LocationId] = @LocationId
      ,[OrganizationId] = @OrganizationId
      ,[Title] = @Title
      ,[Description] = @Description
      ,[Requirements] = @Requirements
      ,[IsActive] = @IsActive
      ,[ContactName] = @ContactName
      ,[ContactPhone] = @ContactPhone
      ,[ContactEmail] = @ContactEmail
      ,[ModifiedBy] = @UserId
      ,[DateModified] = GETUTCDATE()
 WHERE Id = @Id
					
END
GO
/****** Object:  StoredProcedure [dbo].[JobSchedules_Delete_ById]    Script Date: 12/2/2022 3:13:23 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author: <Justin Solo>
-- Create date: <08/04/2022>
-- Description: <JobSchedules_Delete_ById>
-- Code Reviewer:

-- MODIFIED BY: author
-- MODIFIED DATE:12/1/2020
-- Code Reviewer:
-- Note:
-- =============================================

CREATE PROC [dbo].[JobSchedules_Delete_ById]
	@Id int

AS

/* ----- TEST CODE -----

	DECLARE @Id int = 5

	EXECUTE JobSchedules_Delete_ById @Id 

*/ ----- END TEST CODE -----

BEGIN
	DELETE FROM dbo.JobScheduleDaysOfWeek
		WHERE JobScheduleId = @Id

	DELETE FROM [dbo].[JobSchedules]
		  WHERE Id = @Id;

END
GO
/****** Object:  StoredProcedure [dbo].[JobSchedules_Insert]    Script Date: 12/2/2022 3:13:23 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author: <Justin Solo>
-- Create date: <08/03/2022>
-- Description: <JobSchedules_Insert>
-- Code Reviewer:

-- MODIFIED BY: author
-- MODIFIED DATE:12/1/2020
-- Code Reviewer:
-- Note:
-- =============================================
 CREATE PROC [dbo].[JobSchedules_Insert]
	@StartDate datetime2(7)
	,@EndDate datetime2(7)
	,@DailyStartTime time(7)
	,@DailyEndTime time(7)
	,@CreatedBy int
	,@ModifiedBy int
	,@DaysOfWeek [dbo].[JobDayOfWeek] READONLY
	,@JobId int
	,@OrgId int
	,@Id int OUTPUT

AS

/* ----- TEST CODE -----

	DECLARE @OrgId int = 1
			,@StartDate datetime2(7) = '2022-09-01'
			,@EndDate datetime2(7) = '2022-09-02'
			,@DailyStartTime time(7) = '07:30'
			,@DailyEndTime time(7) = '15:30'
			,@CreatedBy int = 135
			,@ModifiedBy int = 135
			,@JobId int = 7
			,@Id int 

	DECLARE @TestDay [dbo].[JobDayOfWeek]
	INSERT INTO @TestDay (DayOfWeek)
	VALUES('monday'),('friday')

	EXECUTE dbo.[JobSchedules_Insert] @StartDate
								,@EndDate
								,@DailyStartTime
								,@DailyEndTime
								,@CreatedBy
								,@ModifiedBy
								,@TestDay
								,@JobId
								,@OrgId
								,@Id OUTPUT

*/ ----- END TEST CODE -----

BEGIN

	DECLARE @Sch int 
	EXECUTE dbo.Schedules_Insert @OrgId,@CreatedBy,@ModifiedBy, @Id = @Sch OUTPUT

	INSERT INTO [dbo].[JobSchedules]
			   ([ScheduleId]
			   ,[StartDate]
			   ,[EndDate]
			   ,[DailyStartTime]
			   ,[DailyEndTime]
			   ,[CreatedBy]
			   ,[ModifiedBy]
			   ,[JobId])
		 VALUES
			   (@Sch
			   ,@StartDate
			   ,@EndDate
			   ,@DailyStartTime
			   ,@DailyEndTime
			   ,@CreatedBy
			   ,@ModifiedBy
			   ,@JobId)

		SET @Id = SCOPE_IDENTITY()

		INSERT INTO dbo.JobScheduleDaysOfWeek (JobScheduleId, DayOfWeekId)
			(SELECT @Id, d.Id
			FROM dbo.DaysOfWeeks as d
			WHERE Exists (
				SELECT 1
				FROM @DaysOfWeek as do
				WHERE d.[Name] = do.[DayOfWeek]
				)
		)

END
GO
/****** Object:  StoredProcedure [dbo].[JobSchedules_Select_ByCreatedBy]    Script Date: 12/2/2022 3:13:23 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author: <Justin Solo>
-- Create date: <08/03/2022>
-- Description: <JobSchedules_Select_ByCreatedBy>
-- Code Reviewer:

-- MODIFIED BY: author
-- MODIFIED DATE:12/1/2020
-- Code Reviewer:
-- Note:
-- =============================================
CREATE PROC [dbo].[JobSchedules_Select_ByCreatedBy]
	@UserId int

AS

/* ----- TEST CODE -----

	EXECUTE dbo.JobSchedules_Select_ByCreatedBy @UserId

*/ ----- END TEST CODE -----

BEGIN

	SELECT j.[Id]
		  ,j.[ScheduleId]
		  ,j.[StartDate]
		  ,j.[EndDate]
		  ,j.[DailyStartTime]
		  ,j.[DailyEndTime]
		  ,[DaysOfWeeks] = (
			SELECT d.Id
					,d.Name
			FROM dbo.DaysOfWeeks as d
				INNER JOIN
				dbo.JobScheduleDaysOfWeek as js
				ON d.Id = js.DayOfWeekId
				WHERE js.JobScheduleId = j.Id
				FOR JSON AUTO
		  )
		  ,j.[DateCreated]
		  ,j.[DateModified]
		  ,up.Id
		  ,j.[CreatedBy]
		  ,up.FirstName
		  ,up.Mi
		  ,up.LastName
		  ,up.AvatarUrl
		  ,up2.Id
		  ,j.[ModifiedBy]
		  ,up2.FirstName
		  ,up2.Mi
		  ,up.LastName
		  ,up2.AvatarUrl
		  ,j.JobId
		  ,jo.Title
		  ,jo.IsActive
		  ,jo.OrganizationId
		  ,og.Name
	  FROM [dbo].[JobSchedules] AS j
	  INNER JOIN
	  dbo.UserProfiles as up
	  ON up.UserId = j.CreatedBy
	  INNER JOIN
	  dbo.UserProfiles as up2
	  ON up2.UserId = j.ModifiedBy
	  INNER JOIN
	  dbo.Jobs as jo
	  ON jo.Id = j.JobId
	  INNER JOIN
	  dbo.Organizations as og
	  ON og.Id = jo.OrganizationId
	  WHERE @UserId = j.CreatedBy
	  ORDER BY j.Id

END
GO
/****** Object:  StoredProcedure [dbo].[JobSchedules_Select_ById]    Script Date: 12/2/2022 3:13:23 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author: <Justin Solo>
-- Create date: <08/03/2022>
-- Description: <JobSchedules_Select_ById>
-- Code Reviewer:

-- MODIFIED BY: author
-- MODIFIED DATE:12/1/2020
-- Code Reviewer:
-- Note:
-- =============================================
CREATE PROC [dbo].[JobSchedules_Select_ById]
	@Id int

AS

/* ----- TEST CODE -----

	DECLARE @Id int = 11

	EXECUTE dbo.JobSchedules_Select_ById @Id 

*/ ----- END TEST CODE -----

BEGIN

	SELECT j.[Id]
		  ,j.[ScheduleId]
		  ,j.[StartDate]
		  ,j.[EndDate]
		  ,j.[DailyStartTime]
		  ,j.[DailyEndTime]
		  ,[DaysOfWeeks] = (
			SELECT d.Id
					,d.Name
			FROM dbo.DaysOfWeeks as d
				INNER JOIN
				dbo.JobScheduleDaysOfWeek as js
				ON d.Id = js.DayOfWeekId
				WHERE js.JobScheduleId = j.Id
				FOR JSON AUTO
		  )
		  ,j.[DateCreated]
		  ,j.[DateModified]
		  ,up.Id
		  ,j.[CreatedBy]
		  ,up.FirstName
		  ,up.Mi
		  ,up.LastName
		  ,up.AvatarUrl
		  ,up2.Id
		  ,j.[ModifiedBy]
		  ,up2.FirstName
		  ,up2.Mi
		  ,up.LastName
		  ,up2.AvatarUrl
		  ,j.JobId
		  ,jo.Title
		  ,jo.IsActive
		  ,jo.OrganizationId
		  ,og.Name
	  FROM [dbo].[JobSchedules] AS j
	  INNER JOIN
	  dbo.UserProfiles as up
	  ON up.UserId = j.CreatedBy
	  INNER JOIN
	  dbo.UserProfiles as up2
	  ON up2.UserId = j.ModifiedBy
	  INNER JOIN
	  dbo.JObs as jo
	  ON jo.Id = j.JobId
	  INNER JOIN
	  dbo.Organizations as og
	  ON og.Id = jo.OrganizationId
	  WHERE j.id = @Id

END
GO
/****** Object:  StoredProcedure [dbo].[JobSchedules_Select_ByOrganization]    Script Date: 12/2/2022 3:13:23 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author: <Justin Solo>
-- Create date: <08/016/2022>
-- Description: <[JobSchedules_Select_ByOrganization]>
-- Code Reviewer:

-- MODIFIED BY: author
-- MODIFIED DATE:12/1/2020
-- Code Reviewer:
-- Note:
-- =============================================
CREATE PROC [dbo].[JobSchedules_Select_ByOrganization]
	@UserId int
	,@OrgId int

AS

/* ----- TEST CODE -----
	DECLARE @userId int = 135
			,@OrgId int = 1
	EXECUTE dbo.[JobSchedules_Select_ByOrganization] @UserId, @OrgId

*/ ----- END TEST CODE -----

BEGIN

	SELECT j.[Id]
		  ,j.[ScheduleId]
		  ,j.[StartDate]
		  ,j.[EndDate]
		  ,j.[DailyStartTime]
		  ,j.[DailyEndTime]
		  ,[DaysOfWeeks] = (
			SELECT d.Id
					,d.Name
			FROM dbo.DaysOfWeeks as d
				INNER JOIN
				dbo.JobScheduleDaysOfWeek as js
				ON d.Id = js.DayOfWeekId
				WHERE js.JobScheduleId = j.Id
				FOR JSON AUTO
		  )
		  ,j.[DateCreated]
		  ,j.[DateModified]
		  ,up.Id
		  ,j.[CreatedBy]
		  ,up.FirstName
		  ,up.Mi
		  ,up.LastName
		  ,up.AvatarUrl
		  ,up2.Id
		  ,j.[ModifiedBy]
		  ,up2.FirstName
		  ,up2.Mi
		  ,up.LastName
		  ,up2.AvatarUrl
		  ,j.JobId
		  ,jo.Title
		  ,jo.IsActive
		  ,jo.OrganizationId
		  ,og.Name
	  FROM [dbo].[JobSchedules] AS j
	  INNER JOIN
	  dbo.UserProfiles as up
	  ON up.UserId = j.CreatedBy
	  INNER JOIN
	  dbo.UserProfiles as up2
	  ON up2.UserId = j.ModifiedBy
	  INNER JOIN
	  dbo.Jobs as jo
	  ON jo.Id = j.JobId
	  INNER JOIN
	  dbo.Organizations as og
	  ON og.Id = jo.OrganizationId
	  WHERE @UserId = j.CreatedBy
	  AND @OrgId = jo.OrganizationId
	  ORDER BY j.Id

END
GO
/****** Object:  StoredProcedure [dbo].[JobSchedules_SelectAll]    Script Date: 12/2/2022 3:13:23 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author: <Justin Solo>
-- Create date: <08/03/2022>
-- Description: <JobSchedules_SelectAll>
-- Code Reviewer:

-- MODIFIED BY: author
-- MODIFIED DATE:12/1/2020
-- Code Reviewer:
-- Note:
-- =============================================
CREATE PROC [dbo].[JobSchedules_SelectAll]
	@PageIndex int
	,@PageSize int

AS

/* ----- TEST CODE -----

	DECLARE @PageIndex int = 0,
			@PageSize int = 20
	EXECUTE dbo.JobSchedules_SelectAll @PageIndex, @PageSize

*/ ----- END TEST CODE -----

BEGIN
	
	DECLARE @offset int = @PageIndex * @PageSize

	SELECT j.[Id]
		  ,j.[ScheduleId]
		  ,j.[StartDate]
		  ,j.[EndDate]
		  ,j.[DailyStartTime]
		  ,j.[DailyEndTime]
		  ,[DaysOfWeeks] = (
			SELECT d.Id
					,d.Name
			FROM dbo.DaysOfWeeks as d
				INNER JOIN
				dbo.JobScheduleDaysOfWeek as js
				ON d.Id = js.DayOfWeekId
				WHERE js.JobScheduleId = j.Id
				FOR JSON AUTO
		  )
		  ,j.[DateCreated]
		  ,j.[DateModified]
		  ,up.Id
		  ,j.[CreatedBy]
		  ,up.FirstName
		  ,up.Mi
		  ,up.LastName
		  ,up.AvatarUrl
		  ,up2.Id
		  ,j.[ModifiedBy]
		  ,up2.FirstName
		  ,up2.Mi
		  ,up.LastName
		  ,up2.AvatarUrl
		  ,j.JobId
		  ,jo.Title
		  ,jo.IsActive
		  ,jo.OrganizationId
		  ,og.Name
		  , TotalCount = COUNT(1) OVER()

	  FROM [dbo].[JobSchedules] AS j
	  INNER JOIN
	  dbo.UserProfiles as up
	  ON up.UserId = j.CreatedBy
	  INNER JOIN
	  dbo.UserProfiles as up2
	  ON up2.UserId = j.ModifiedBy
	  INNER JOIN
	  dbo.Jobs as jo
	  INNER JOIN
	  dbo.Organizations as og
	  ON og.Id = jo.OrganizationId
	  ON jo.Id = j.JobId
	  ORDER BY j.Id
	  
	  OFFSET @offSet ROWS
	  FETCH NEXT @PageSize Rows ONLY

END
GO
/****** Object:  StoredProcedure [dbo].[JobSchedules_Update]    Script Date: 12/2/2022 3:13:23 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author: <Justin Solo>
-- Create date: <08/03/2022>
-- Description: <JobSchedules_Update>
-- Code Reviewer:

-- MODIFIED BY: author
-- MODIFIED DATE:12/1/2020
-- Code Reviewer:
-- Note:
-- =============================================
 CREATE PROC [dbo].[JobSchedules_Update]
	@StartDate datetime2(7)
	,@EndDate datetime2(7)
	,@DailyStartTime time(7)
	,@DailyEndTime time(7)
	,@ModifiedBy int
	,@DaysOfWeek [dbo].[JobDayOfWeek] READONLY
	,@Id int

AS

/* ----- TEST CODE -----

	DECLARE @StartDate datetime2(7) = '2022-09-01'
			,@EndDate datetime2(7) = '2022-09-05'
			,@DailyStartTime time(7) = '07:30'
			,@DailyEndTime time(7) = '15:30'
			,@ModifiedBy int = 135
			,@Id int =	27
		
	DECLARE @TestDay [dbo].[JobDayOfWeek]
	INSERT INTO @TestDay (DayOfWeek)
	VALUES('monday'),('friday'), ('wednesday')

	EXECUTE dbo.JobSchedules_Update @StartDate
								,@EndDate
								,@DailyStartTime
								,@DailyEndTime
								,@ModifiedBy
								,@Testday
								,@Id 

*/ ----- END TEST CODE -----

BEGIN

	UPDATE		[dbo].[JobSchedules]
		   SET	[StartDate] = @StartDate
			   ,[EndDate] = @EndDate
			   ,[DailyStartTime] = @DailyStartTime
			   ,[DailyEndTime] = @DailyEndTime
			   ,[ModifiedBy] = @ModifiedBy
			WHERE Id = @Id
		
			DELETE FROM dbo.JobScheduleDaysOfWeek
			WHERE JobScheduleId = @Id

			INSERT INTO dbo.JobScheduleDaysOfWeek (JobScheduleId, DayOfWeekId)
				(SELECT @Id, d.Id
				FROM dbo.DaysOfWeeks as d
				WHERE Exists (
					SELECT 1
					FROM @DaysOfWeek as do
					WHERE d.[Name] = do.[DayOfWeek]
					)
			)

END
GO
/****** Object:  StoredProcedure [dbo].[JobTypes_SelectAll]    Script Date: 12/2/2022 3:13:23 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

-- =============================================
-- Author:		<Thomas Sauer>
-- Create date: <07/27/2022>
-- Description:	<Select All JobTypes>
-- Code Reviewer:

-- MODIFIED BY: n/a
-- MODIFIED DATE: n/a
-- Code Reviewer: 
-- Note: 
-- =============================================

	CREATE PROC [dbo].[JobTypes_SelectAll] 

	AS

	/*------Test Code------
		
	Execute [dbo].[JobTypes_SelectAll]


	*/---------------------

	BEGIN


	SELECT [Id]
		  ,[Name]

	  FROM [dbo].[JobTypes]

	END


GO
/****** Object:  StoredProcedure [dbo].[Language_Insert]    Script Date: 12/2/2022 3:13:23 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author: <Ledwin Batista>
-- Create date: <08/18/2022>
-- Description: <Language_Insert>
-- Code Reviewer:

-- MODIFIED BY: author
-- MODIFIED DATE:
-- Code Reviewer:
-- Note:
-- =============================================

CREATE proc [dbo].[Language_Insert]
			                 @Code varchar(64)
							,@Name varchar(64)
			                ,@Id int output

AS

/* -- TEST CODE --

	Declare @Id int = 0;

	Declare @Name varchar(64) = '123'
	Declare @Code varchar(64) = 'English'


	EXECUTE [dbo].[Language_Insert]
			 @Code
			,@Name
		    ,@Id output


	Select *
	from dbo.Languages
    where Id = @Id
			
	select * 
	from dbo.Languages
		
*/ -- END TEST CODE --


BEGIN
	

	INSERT INTO [dbo].[Languages] 
			([Code]
			,[Name])
		
		VALUES
			(@code
			,@Name)
			
	SET @Id = SCOPE_IDENTITY()

END
GO
/****** Object:  StoredProcedure [dbo].[Language_SelectAll]    Script Date: 12/2/2022 3:13:23 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author: <Ledwin Batista>
-- Create date: <08/18/2022>
-- Description: <Language_SelectAll>
-- Code Reviewer:

-- MODIFIED BY: author
-- MODIFIED DATE:
-- Code Reviewer:
-- Note:
-- =============================================

CREATE proc [dbo].[Language_SelectAll]

/*------TEST CODE------

		Execute dbo.Language_SelectAll

*/

as

BEGIN

SELECT	 lang.[Id]
		,lang.[Code]
		,lang.[Name]		

	
	FROM dbo.Languages as lang

	ORDER BY lang.Id

END
GO
/****** Object:  StoredProcedure [dbo].[Licenses_Delete_byId]    Script Date: 12/2/2022 3:13:23 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE   proc [dbo].[Licenses_Delete_byId]
			@Id int
AS

BEGIN

	DELETE FROM [dbo].[Licenses]
      WHERE Id = @Id;

END
GO
/****** Object:  StoredProcedure [dbo].[Licenses_Insert]    Script Date: 12/2/2022 3:13:23 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE   PROC [dbo].[Licenses_Insert]
			@LicenseStateId int
           ,@LicenseTypeId int
           ,@LicenseNumber nvarchar(50)
           ,@DateExpires date
           ,@CreatedBy int
		   ,@Id int OUTPUT


/*-----------Test Code----------
Declare @LicenseStateId int = 2
		,@LicenseTypeId int = 2
		,@LicenseNumber nvarchar(50) = 'LN002'
		,@DateExpires date = '01-01-2023'
		,@CreatedBy int = 16
		,@Id int =0

Execute dbo.Licenses_Insert
			@LicenseStateId
           ,@LicenseTypeId
           ,@LicenseNumber
           ,@DateExpires
           ,@CreatedBy
		   ,@Id OUTPUT
Select *
From [dbo].[Licenses]
Where Id = @Id

*/           
AS

BEGIN

INSERT INTO [dbo].[Licenses]
           ([LicenseStateId]
           ,[LicenseTypeId]
           ,[LicenseNumber]
           ,[DateExpires]
           ,[CreatedBy]
           ,[DateCreated])
     VALUES
           (@LicenseStateId
           ,@LicenseTypeId
           ,@LicenseNumber
           ,@DateExpires
           ,@CreatedBy
           ,GETUTCDATE())

	SET @Id = SCOPE_IDENTITY()

INSERT INTO [dbo].[UsersLicenses]
			([LicenseId]
			,[UserId])
	VALUES (@Id, @CreatedBy)

END

SELECT *
FROM  [dbo].[UsersLicenses]

SELECT *
FROM  [dbo].[Licenses]
GO
/****** Object:  StoredProcedure [dbo].[Licenses_Select_ByCreatedBy]    Script Date: 12/2/2022 3:13:23 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE     PROC [dbo].[Licenses_Select_ByCreatedBy]
		@PageIndex int
		,@PageSize int
		,@CreatedBy int

AS

/*---- TEST CODE ----

	Declare @PageIndex int = 0
			,@PageSize int = 10
			,@CreatedBy int = 16

	Execute [dbo].[Licenses_Select_ByCreatedBy]
			@PageIndex
			,@PageSize
			,@CreatedBy

*/ ---- TEST CODE ----

BEGIN
	Declare @offset int = @PageIndex * @PageSize

SELECT l.[Id]
      ,s.[Name] as LicenseState
      ,lt.[Name] as LicenseType
      ,l.[LicenseNumber]
      ,l.[DateExpires]
      ,u.[Email] as CreateBy
      ,l.[DateCreated]
	  ,TotalCount = COUNT(1) OVER()
  
  FROM dbo.Licenses as l 
		inner join dbo.States as s on s.Id = l.LicenseStateId
		inner join dbo.LicenseTypes as lt on lt.Id = l.LicenseTypeId
		inner join dbo.Users as u on u.Id = l.CreatedBy

	WHERE @CreatedBy = u.Id 
	ORDER BY l.Id

	OFFSET @offset Rows
	FETCH Next @PageSize Rows ONLY
END
GO
/****** Object:  StoredProcedure [dbo].[Licenses_Select_ById]    Script Date: 12/2/2022 3:13:23 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE   PROC [dbo].[Licenses_Select_ById]
		@Id int

AS

/* ----- TEST CODE -----

	DECLARE @Id int = 1

	EXECUTE dbo.Licenses_Select_ById @Id
	
*/ ----- END TEST CODE -----
BEGIN

SELECT l.[Id]
      ,s.[Name] as LicenseState
      ,lt.[Name] as LicenseType
      ,l.[LicenseNumber]
      ,l.[DateExpires]
      ,u.[Email] as CreateBy
      ,l.[DateCreated]
  
  FROM dbo.Licenses as l 
		inner join dbo.States as s on s.Id = l.LicenseStateId
		inner join dbo.LicenseTypes as lt on lt.Id = l.LicenseTypeId
		inner join dbo.Users as u on u.Id = l.CreatedBy

		WHERE @Id = l.Id

END


GO
/****** Object:  StoredProcedure [dbo].[Licenses_Select_ByLicenseTypeId]    Script Date: 12/2/2022 3:13:23 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE     PROC [dbo].[Licenses_Select_ByLicenseTypeId]
		@LicenseTyId int

AS

/* ----- TEST CODE -----

	DECLARE @LicenseTyId int = 1

	EXECUTE dbo.Licenses_Select_ByLicenseTypeId @LicenseTyId
	
*/ ----- END TEST CODE -----

BEGIN

SELECT l.[Id]
      ,s.[Name] as LicenseState
      ,lt.[Name] as LicenseType
      ,l.[LicenseNumber]
      ,l.[DateExpires]
      ,u.[Email] as CreateBy
      ,l.[DateCreated]
  
  FROM dbo.Licenses as l 
		inner join dbo.States as s on s.Id = l.LicenseStateId
		inner join dbo.LicenseTypes as lt on lt.Id = l.LicenseTypeId
		inner join dbo.Users as u on u.Id = l.CreatedBy

		WHERE @LicenseTyId = lt.Id
END
GO
/****** Object:  StoredProcedure [dbo].[Licenses_SelectAll]    Script Date: 12/2/2022 3:13:23 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE       PROC [dbo].[Licenses_SelectAll]
		@PageIndex int
		,@PageSize int

AS

/*---- TEST CODE ----

	Declare @PageIndex int = 0
			,@PageSize int = 10
			

	Execute [dbo].[Licenses_SelectAll]
			@PageIndex
			,@PageSize
			

*/ ---- TEST CODE ----

BEGIN
	Declare @offset int = @PageIndex * @PageSize

SELECT l.[Id]
      ,s.[Name] as LicenseState
      ,lt.[Name] as LicenseType
      ,l.[LicenseNumber]
      ,l.[DateExpires]
      ,u.[Email] as CreateBy
      ,l.[DateCreated]
	  ,TotalCount = COUNT(1) OVER()
  
  FROM dbo.Licenses as l 
		inner join dbo.States as s on s.Id = l.LicenseStateId
		inner join dbo.LicenseTypes as lt on lt.Id = l.LicenseTypeId
		inner join dbo.Users as u on u.Id = l.CreatedBy

	
	ORDER BY l.Id

	OFFSET @offset Rows
	FETCH Next @PageSize Rows ONLY
END
GO
/****** Object:  StoredProcedure [dbo].[Licenses_Update]    Script Date: 12/2/2022 3:13:23 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE   PROC [dbo].[Licenses_Update]
			@LicenseStateId int
           ,@LicenseTypeId int
           ,@LicenseNumber nvarchar(50)
           ,@DateExpires date
           --,@CreatedBy int
		   ,@Id int
AS
/*-------TEST CODE-----
Declare   @LicenseStateId int = 5
           ,@LicenseTypeId int = 2
           ,@LicenseNumber nvarchar(50) = 'LN002'
           ,@DateExpires date = '2024-01-01'   
		   ,@Id int = 3

Execute 
			[dbo].[Licenses_Update]
			@LicenseStateId
           ,@LicenseTypeId
           ,@LicenseNumber
           ,@DateExpires
		   ,@Id

Execute dbo.Licenses_Select_ById @Id


*/
BEGIN
	UPDATE		[dbo].[Licenses]
       SET	[LicenseStateId] = @LicenseStateId
           ,[LicenseTypeId] = @LicenseTypeId
           ,[LicenseNumber] = @LicenseNumber
           ,[DateExpires] = @DateExpires
           --,[CreatedBy] = @CreatedBy          
	  WHERE Id = @Id
END
GO
/****** Object:  StoredProcedure [dbo].[LicenseTypes_SelectAll]    Script Date: 12/2/2022 3:13:23 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-------------------------------------------------
-- Author: Harold Tran
-- Create date: 20-July-2022
-- Description: Created procedure
-- Code Review 

-- MODIFIED BY:
-- MODIFIED DATE:
-- Code Reviewer:
-- Note:
--------------------------------------------------

CREATE PROCEDURE [dbo].[LicenseTypes_SelectAll]

AS

/*

EXECUTE dbo.LicenseTypes_GetAll

*/

BEGIN

SELECT			[Id]
				,[Name]

FROM dbo.LicenseTypes

END

GO
/****** Object:  StoredProcedure [dbo].[LicenseVerification_Create]    Script Date: 12/2/2022 3:13:23 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author: Harold Tran
-- Create date: 20-July-2022
-- Description: Created Procedure
-- Code Reviewer: 

-- MODIFIED BY: 
-- MODIFIED DATE:
-- Code Reviewer: 
-- Note: 
-- =============================================

CREATE PROCEDURE [dbo].[LicenseVerification_Create]
					@LicenseTypesId int
					,@LocationsId int
					,@LicenseStateId int
					,@Url nvarchar(Max)
					,@DateExpires int
					
					,@Id int OUTPUT
					
AS

/* --TEST CODE --

Declare				@LicenseTypesId int = 3
					,@LocationsId int = 4
					,@LicenseStateId int = 4
					,@Url nvarchar(Max) = 'https://sabio-training.s3-us-west-2.amazonaws.com/bbe84c23-2bbf-440d-b8bd-a6b519fe3344_API%20Help%20info.txthttps://sabio-training.s3-us-west-2.amazonaws.com/b1a079fb-6507-430f-a61f-06107fe1ff31_Test%201.pdf'
					,@DateExpires int = 1
					
					,@Id int 

Execute dbo.LicenseVerification_Create
					@LicenseTypesId
					,@LocationsId 
					,@LicenseStateId 
					,@Url 
					,@DateExpires 
					
					,@Id OUTPUT

SELECT * FROM UserLicense


*/ --TEST CODE--

BEGIN

INSERT INTO dbo.UserLicense
					(
					[LicenseTypesId]
					,[LocationsId]
					,[LicenseStateId]
					,[Url]
					,[DateExpires]
					
					)

VALUES				(
					@LicenseTypesId
					,@LocationsId 
					,@LicenseStateId 
					,@Url 
					,@DateExpires 
					
					)

SET					@Id = SCOPE_IDENTITY();

END
GO
/****** Object:  StoredProcedure [dbo].[LicenseVerification_GetBy_UnexpiredLicenseType]    Script Date: 12/2/2022 3:13:23 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-------------------------------------------------
-- Author: Harold Tran
-- Create date: 20-July-2022
-- Description: Created Procedure

-- MODIFIED BY:
-- MODIFIED DATE:
-- Code Reviewer:
-- Note:
--------------------------------------------------
CREATE PROCEDURE [dbo].[LicenseVerification_GetBy_UnexpiredLicenseType]

				@LicenseTypeId int
				,@DateExpires int

AS

/* --Test Code--

DECLARE			@LicenseTypeId int = 1
				,@DateExpires int = 202207
EXECUTE LicenseVerification_GetBy_UnexpiredLicenseType
				@LicenseTypeId
				,@DateExpires

--need to incorporate license type
SELECT * FROM dbo.UserLicense

*/

BEGIN

SELECT			uv.Id as UserLicenseId
				,lt.Id AS LicenseTypesId
				,lt.Name LicensTypesName
				,up.Id AS UserProfileId
				,uv.UserId
				,up.FirstName
				,up.LastName
				,up.Mi
				,up.AvatarUrl
				,pt.Id AS ProfessionTypeId
				,pt.Name AS ProfessionTypeName
				,up.DOB
				,up.Email
				,up.Phone
				,up.LicenseNumber
				,up.YearsOfExperience
				,up.DesiredHourlyRate
				,up.IsActive
				,l.Id AS LocationsId
				,loct.Id AS LocationTypeId
				,loct.Name AS LocationTypeName
				,l.LineOne
				,l.LineTwo
				,l.City
				,l.Zip
				--l.StateId
				,l.Latitude
				,l.Longitude
				,s.Id AS StatesId
				,s.Code
				,S.Name
				,uv.Url
				,uv.DateExpires
				,up.Id AS UserProfileId
				,uv.[CreatedBy]
				,up.FirstName
				,up.LastName
				,up.Mi
				,up.AvatarUrl
				,uv.DateCreated
				,uv.DateModified

	
	FROM UserLicense AS uv INNER JOIN LicenseTypes AS lt
	ON lt.Id = uv.LicenseTypesId INNER JOIN UserProfiles AS up
	ON up.Id = uv.UserId INNER JOIN Locations AS l
	ON l.Id = uv.LocationsId INNER JOIN States AS s
	ON s.Id = uv.LicenseStateId INNER JOIN LocationTypes AS loct
	ON loct.Id = l.LocationTypeId INNER JOIN ProfessionTypes AS pt
	ON pt.Id = up.ProfessionTypeId
	WHERE uv.LicenseTypesId = @LicenseTypeId and uv.DateExpires >= @DateExpires

END 
				

				
GO
/****** Object:  StoredProcedure [dbo].[LicenseVerification_SearchBy_UnexpiredLicenseType]    Script Date: 12/2/2022 3:13:23 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[LicenseVerification_SearchBy_UnexpiredLicenseType]
				@PageIndex int
				,@PageSize int
				,@LicenseTypesId int
				,@DateExpires int

AS

/*
 
DECLARE			@PageIndex int = 0
				,@PageSize int = 1
				,@LicenseTypesId int = 1
				,@DateExpires int = 202207

EXECUTE LicenseVerification_SearchBy_UnexpiredLicenseType
				@PageIndex   
				,@PageSize 
				,@LicenseTypesId 
				,@DateExpires

*/

BEGIN

DECLARE			@offset int = @PageIndex * @PageSize

SELECT			uv.Id as UserLicenseId
				,lt.Id AS LicenseTypesId
				,lt.Name LicensTypesName
				,up.Id AS UserProfileId
				,uv.UserId
				,up.FirstName
				,up.LastName
				,up.Mi
				,up.AvatarUrl
				,pt.Id AS ProfessionTypeId
				,pt.Name AS ProfessionTypeName
				,up.DOB
				,up.Email
				,up.Phone
				,up.LicenseNumber
				,up.YearsOfExperience
				,up.DesiredHourlyRate
				,up.IsActive
				,l.Id AS LocationsId
				,loct.Id AS LocationTypeId
				,loct.Name AS LocationTypeName
				,l.LineOne
				,l.LineTwo
				,l.City
				,l.Zip
				--l.StateId
				,l.Latitude
				,l.Longitude
				,s.Id AS StatesId
				,s.Code
				,S.Name
				,uv.Url
				,uv.DateExpires
				,up.Id AS UserProfileId
				,uv.[CreatedBy]
				,up.FirstName
				,up.LastName
				,up.Mi
				,up.AvatarUrl
				,uv.DateCreated
				,uv.DateModified
				,TotalCount = COUNT(1) OVER()

	
	FROM UserLicense AS uv INNER JOIN LicenseTypes AS lt
	ON lt.Id = uv.LicenseTypesId INNER JOIN UserProfiles AS up
	ON up.Id = uv.UserId INNER JOIN Locations AS l
	ON l.Id = uv.LocationsId INNER JOIN States AS s
	ON s.Id = uv.LicenseStateId INNER JOIN LocationTypes AS loct
	ON loct.Id = l.LocationTypeId INNER JOIN ProfessionTypes AS pt
	ON pt.Id = up.ProfessionTypeId


	--WHERE (LicenseTypesId LIKE '%' + @Query + '%'
	--		OR DateExpires LIKE '%' + @Query + '%')
	
	WHERE uv.LicenseTypesId = @LicenseTypesId AND uv.DateExpires >= @DateExpires
	ORDER BY uv.Id
	OFFSET @offset ROWS
	FETCH NEXT @PageSize ROWS ONLY

 END
GO
/****** Object:  StoredProcedure [dbo].[LicenseVerification_SelectBy_UserLicenseId]    Script Date: 12/2/2022 3:13:23 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-------------------------------------------------
-- Author: Harold Tran
-- Create date: 20-July-2022
-- Description: Created procedure
-- Code Review 

-- MODIFIED BY:
-- MODIFIED DATE:
-- Code Reviewer:
-- Note:
--------------------------------------------------

CREATE PROCEDURE [dbo].[LicenseVerification_SelectBy_UserLicenseId]
				@Id int

AS

/* --Test Code--

DECLARE			@Id int = 135
EXECUTE LicenseVerification_SelectBy_UserLicenseId
				@Id

SELECT * FROM dbo.UserLicense

*/

BEGIN

SELECT			uv.Id as UserLicenseId
				,lt.Id AS LicenseTypesId
				,lt.Id AS LicenseTypesId
				,lt.Name LicensTypesName
				,up.Id AS UserId
				,up.Id AS UserProfileId
				,uv.UserId
				,up.FirstName
				,up.LastName
				,up.Mi
				,up.AvatarUrl
				,l.id AS LocationsId
				,s.Id AS LicenseStateId
				
				,uv.[Url]
				,uv.DateExpires
				,uv.DateCreated
				,uv.DateModified

	
	FROM UserLicense AS uv INNER JOIN LicenseTypes AS lt
	ON lt.Id = uv.LicenseTypesId INNER JOIN UserProfiles AS up
	ON up.Id = uv.UserId INNER JOIN States AS s
	ON s.Id = uv.LicenseStateId  INNER JOIN Locations AS l
	ON l.Id = uv.LocationsId
	WHERE uv.Id = @Id

END 
				

			
GO
/****** Object:  StoredProcedure [dbo].[LicenseVerification_Update]    Script Date: 12/2/2022 3:13:23 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author: Harold Tran
-- Create date: 20-July-2022
-- Description: Created Procedure
-- Code Reviewer: 

-- MODIFIED BY: 
-- MODIFIED DATE:
-- Code Reviewer: 
-- Note: 
-- =============================================

CREATE PROCEDURE [dbo].[LicenseVerification_Update]
					@LicenseTypesId int
					,@LocationsId int
					,@LicenseStateId int
					,@Url nvarchar(Max)
					,@DateExpires int
					,@Id int 

AS

/* --TEST CODE --

DECLARE				@Id int = 12

Declare				@LicenseTypesId int = 6
					,@LocationsId int = 4
					,@LicenseStateId int = 4
					,@Url nvarchar(Max) = 'y'
					,@DateExpires int = 2

Execute dbo.LicenseVerification_Update
					@LicenseTypesId
					,@LocationsId 
					,@LicenseStateId 
					,@Url 
					,@DateExpires 
					,@Id 

SELECT * FROM UserLicense
WHERE Id = @Id



*/ --TEST CODE--

BEGIN

DECLARE @datNow datetime2 = GETUTCDATE()

UPDATE dbo.UserLicense
SET					
					[LicenseTypesId] = @LicenseTypesId	
					,[LocationsId] = @LocationsId
					,[LicenseStateId] = @LicenseStateId
					,[Url] = @Url
					,[DateExpires] = @DateExpires
					,[DateModified] = @datNow
					

WHERE				Id = @Id 
					/*
						I was trying to update the userID, but the user isnt' allowed to update That
						And the user Id woudln't change for licenses
					*/

END
GO
/****** Object:  StoredProcedure [dbo].[LicenseViewer_SearchBy_UnexpiredLicenseType]    Script Date: 12/2/2022 3:13:23 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[LicenseViewer_SearchBy_UnexpiredLicenseType]
				@PageIndex int
				,@PageSize int
				,@LicenseTypesId int
				,@DateExpires int

AS

/*
 
DECLARE			@PageIndex int = 0
				,@PageSize int = 5
				,@LicenseTypesId int = 1
				,@DateExpires int = 202207

EXECUTE LicenseVerification_SearchBy_UnexpiredLicenseType
				@PageIndex   
				,@PageSize 
				,@LicenseTypesId 
				,@DateExpires

SELECT * FROM UserLicense

*/

BEGIN

DECLARE			@offset int = @PageIndex * @PageSize

SELECT			uv.Id as UserLicenseId
				,lt.Id AS LicenseTypesId
				,lt.Name LicensTypesName
				,up.Id AS UserProfileId
				,uv.UserId
				,up.FirstName
				,up.LastName
				,up.Mi
				,up.AvatarUrl
				,pt.Id AS ProfessionTypeId
				,pt.Name AS ProfessionTypeName
				,up.DOB
				,up.Email
				,up.Phone
				,up.LicenseNumber
				,up.YearsOfExperience
				,up.DesiredHourlyRate
				,up.IsActive
				,l.Id AS LocationsId
				,loct.Id AS LocationTypeId
				,loct.Name AS LocationTypeName
				,l.LineOne
				,l.LineTwo
				,l.City
				,l.Zip
				--l.StateId
				,l.Latitude
				,l.Longitude
				,s.Id AS StatesId
				,s.Code
				,S.Name
				,uv.Url
				,uv.DateExpires
				,up.Id AS UserProfileId
				,uv.[CreatedBy]
				,up.FirstName
				,up.LastName
				,up.Mi
				,up.AvatarUrl
				,uv.DateCreated
				,uv.DateModified
				,TotalCount = COUNT(1) OVER()

	
	FROM UserLicense AS uv INNER JOIN LicenseTypes AS lt
	ON lt.Id = uv.LicenseTypesId INNER JOIN UserProfiles AS up
	ON up.Id = uv.UserId INNER JOIN Locations AS l
	ON l.Id = uv.LocationsId INNER JOIN States AS s
	ON s.Id = uv.LicenseStateId INNER JOIN LocationTypes AS loct
	ON loct.Id = l.LocationTypeId INNER JOIN ProfessionTypes AS pt
	ON pt.Id = up.ProfessionTypeId


	--WHERE (LicenseTypesId LIKE '%' + @Query + '%'
	--		OR DateExpires LIKE '%' + @Query + '%')
	
	WHERE uv.LicenseTypesId = @LicenseTypesId AND uv.DateExpires >= @DateExpires
	ORDER BY uv.Id
	OFFSET @offset ROWS
	FETCH NEXT @PageSize ROWS ONLY

 END
GO
/****** Object:  StoredProcedure [dbo].[Locations_Delete_ById]    Script Date: 12/2/2022 3:13:23 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author: Hyun Kim
-- Create date: 7/9/22
-- Description: dbo.Locations_Delete_ById
-- Code Reviewer:

-- MODIFIED BY: author
-- MODIFIED DATE:12/1/2020
-- Code Reviewer:
-- Note:
-- =============================================

CREATE proc [dbo].[Locations_Delete_ById]
	@Id int

/* ----TEST CODE----

	Declare @Id int = 1

	Select *
	From dbo.Locations
	Where Id = @Id

	Execute dbo.Locations_Delete_ById
		@Id



*/ ----TEST CODE----

AS

BEGIN

DELETE FROM [dbo].[Locations]
      WHERE Id = @Id

END
GO
/****** Object:  StoredProcedure [dbo].[Locations_Insert]    Script Date: 12/2/2022 3:13:23 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author: <Hyun Kim>
-- Create date: <7/9/22>
-- Description: <Locations_Insert>
-- Code Reviewer:

-- MODIFIED BY: <Jorge Obando>
-- MODIFIED DATE: 7/28/2022
-- Code Reviewer:
-- Note: The Insert service was expecting to received @DateCreated and @DateModified as parameters so I removed it.
-- =============================================

CREATE proc [dbo].[Locations_Insert]
			@LocationTypeId int
			,@LineOne nvarchar(255)
			,@LineTwo nvarchar(255)
			,@City nvarchar(225)
			,@Zip nvarchar(50)
			,@StateId int
			,@Latitude float 
			,@Longitude float
			,@CreatedBy int
			,@ModifiedBy int
			,@Id int OUTPUT

/* ---- TEST CODE ----

	Declare @Id int = 0

	Declare @LocationTypeId int = 5
			,@LineOne nvarchar(255) = 'LineOne Insert'
			,@LineTwo nvarchar(255) = 'LineTwo Insert'
			,@City nvarchar(225) = 'City Insert'
			,@Zip nvarchar(50) = '123123'
			,@StateId int = 24
			,@Latitude float = '34.4856'
			,@Longitude float = '45.5484'
			,@CreatedBy int = 27
			,@ModifiedBy int = 27

	Execute dbo.Locations_Insert
			@LocationTypeId
			,@LineOne 
			,@LineTwo
			,@City 
			,@Zip 
			,@StateId 
			,@Latitude 
			,@Longitude 
			,@CreatedBy 
			,@ModifiedBy 
			,@Id OUTPUT

			Select @Id

			Select *
			From dbo.Locations

*/ ---- TEST CODE ----

AS

BEGIN

INSERT INTO [dbo].[Locations]
           ([LocationTypeId]
           ,[LineOne]
           ,[LineTwo]
           ,[City]
           ,[Zip]
           ,[StateId]
           ,[Latitude]
           ,[Longitude]
           ,[DateCreated]
           ,[DateModified]
           ,[CreatedBy]
           ,[ModifiedBy])
     VALUES
           (@LocationTypeId
           ,@LineOne
           ,@LineTwo
           ,@City
           ,@Zip
           ,@StateId
           ,@Latitude
           ,@Longitude
           ,GETUTCDATE()
           ,GETUTCDATE()
           ,@CreatedBy
           ,@ModifiedBy)

		SET @Id = SCOPE_IDENTITY()

END


GO
/****** Object:  StoredProcedure [dbo].[Locations_Select_ByCreatedBy]    Script Date: 12/2/2022 3:13:23 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author: <Hyun Kim>
-- Create date: <7/11/22>
-- Description: <Locations_Select_ByCreatedBy>
-- Code Reviewer:

-- MODIFIED BY: author
-- MODIFIED DATE:12/1/2020
-- Code Reviewer:
-- Note:
-- =============================================

CREATE proc [dbo].[Locations_Select_ByCreatedBy]
		@PageIndex int
		,@PageSize int
		,@CreatedBy int


/* ---- TEST CODE ----

	Declare @PageIndex int = 0
			,@PageSize int = 10
			,@CreatedBy int = 21

	Execute [dbo].[Locations_Select_ByCreatedBy]
			@PageIndex
			,@PageSize
			,@CreatedBy

*/ ---- TEST CODE ----

AS 

BEGIN
	Declare @offset int = @PageIndex * @PageSize

	SELECT l.[Id]
		  ,lt.[Id] 
		  ,lt.[Name] as LocationType
		  ,l.[LineOne]
		  ,l.[LineTwo]
		  ,l.[City]
		  ,l.[Zip]
		  ,s.[Id] as StateId
		  ,s.[Name] As StateName
		  ,s.[Code] as StateCode
		  ,l.[Latitude]
		  ,l.[Longitude]
		  ,l.[DateCreated]
		  ,l.[DateModified]
		  ,l.[CreatedBy]
		  ,l.[ModifiedBy]
		  ,TotalCount = COUNT(1) OVER()
   FROM [dbo].[Locations] as l inner join dbo.LocationTypes as lt
							on lt.Id = l.LocationTypeId
							inner join dbo.Users as u
							on u.Id = l.CreatedBy 
							inner join dbo.States as s
							on s.Id = l.StateId

	Where u.Id = @CreatedBy
	ORDER BY u.Id

	OFFSET @offset Rows
	FETCH Next @PageSize Rows ONLY

END


GO
/****** Object:  StoredProcedure [dbo].[Locations_Select_ByGeo]    Script Date: 12/2/2022 3:13:23 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author: <Hyun Kim>
-- Create date: <7/12/22>
-- Description: <Locations_Select_ByGeo>
-- Code Reviewer:

-- MODIFIED BY: <Jorge Obando>
-- MODIFIED DATE: 07/28/2022
-- Code Reviewer:
-- Note: Adding additional columns to return from dbo.State
-- =============================================

--declare @Longitude float = 33.9860091, @Latitude float = -118.3870989;
--declare @point = geography::Point(@Latitude, @Longitude, 4326);
--declare @distance = <distance in meters>;

--select * from dbo.Addresses where @point.STDistance([p]) <= @distance;


CREATE proc [dbo].[Locations_Select_ByGeo]
		@Latitude float = 24.4844
		,@Longitude float = 88.5555
		,@Distance int = 10

/* ---- TEST CODE ----

	Declare @Latitude float =  24.4844
			,@Longitude float = 88.5555
			,@Distance int = 100

	Execute [dbo].[Locations_Select_ByGeo]
			@Latitude 
			,@Longitude 
			,@Distance

*/ ---- TEST CODE ----

AS 

BEGIN

 ;with AllRecords as (

SELECT l.[Id]
      ,lt.[Id] as LocationTypeId
	  ,lt.[Name] as LocationType
	  ,l.[LineOne]
	  ,l.[LineTwo]
	  ,l.[City]
	  ,l.[Zip]
	  ,s.[Id] as StateId
	  ,s.[Name] As StateName
      ,s.[Code] as StateCode
      ,l.[Latitude]
      ,l.[Longitude]
      ,l.[DateCreated]
      ,l.[DateModified]
      ,l.[CreatedBy]
      ,l.[ModifiedBy]
	  ,( 3959 * acos( cos( radians(@Latitude) ) * cos( radians( l.Latitude ) ) * cos( radians( l.Longitude ) - radians(@Longitude) ) + sin( radians(@Latitude) ) * sin( radians(l.Latitude) ) ) ) AS distance


   FROM [dbo].[Locations] as l inner join dbo.LocationTypes as lt
							on lt.Id = l.LocationTypeId
							inner join dbo.Users as u
							on u.Id = l.CreatedBy 
							inner join dbo.States as s
							on s.Id = l.StateId)
		, filteredRecords as (
				Select  *
				From    AllRecords
				Where   Distance < @Distance
			)
			select
						filteredRecords.[Id]
						,filteredRecords.LocationTypeId
						,filteredRecords.LocationType
						,filteredRecords.[LineOne]
						,filteredRecords.[LineTwo]
						,filteredRecords.[City]
						,filteredRecords.[Zip]
						,filteredRecords.StateId
						,filteredRecords.StateName
						,filteredRecords.StateCode
						,filteredRecords.[Latitude]
						,filteredRecords.[Longitude]
						,filteredRecords.[DateCreated]
						,filteredRecords.[DateModified]
						,filteredRecords.[CreatedBy]
						,filteredRecords.[ModifiedBy]
					  ,distance
			from    filteredRecords
			ORDER BY distance;
			END
GO
/****** Object:  StoredProcedure [dbo].[Locations_Select_ById]    Script Date: 12/2/2022 3:13:23 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author: <Hyun Kim>
-- Create date: <7/10/22>
-- Description: <Locations_Select_ById>
-- Code Reviewer:

-- MODIFIED BY: <Jorge Obando>
-- MODIFIED DATE: 07/26/2022
-- Code Reviewer:
-- Note: Adding additional columns to return from dbo.State
-- =============================================

CREATE proc [dbo].[Locations_Select_ById]
		@Id int

/* ---- TEST CODE ----

	Declare @Id int = 3;

	Execute dbo.Locations_Select_ById @Id

*/ ---- TEST CODE ----

AS

BEGIN

	SELECT l.[Id]
		  ,lt.[Id] 
		  ,lt.[Name] as LocationType
		  ,l.[LineOne]
		  ,l.[LineTwo]
		  ,l.[City]
		  ,l.[Zip]
		  ,s.[Id] as StateId
		  ,s.[Name] As StateName
		  ,s.[Code] as StateCode
		  ,l.[Latitude]
		  ,l.[Longitude]
		  ,l.[DateCreated]
		  ,l.[DateModified]
		  ,l.[CreatedBy]
		  ,l.[ModifiedBy]
  FROM [dbo].[Locations] as l inner join dbo.LocationTypes as lt
							on lt.Id = l.LocationTypeId
							--inner join dbo.Users as u
							--on u.Id = l.CreatedBy 
							inner join dbo.States as s
							on s.Id = l.StateId
	Where l.Id = @Id

END

GO
/****** Object:  StoredProcedure [dbo].[Locations_SelectAll]    Script Date: 12/2/2022 3:13:23 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author: <Hyun Kim>
-- Create date: <7/10/22>
-- Description: <Locations_SelectAll>
-- Code Reviewer:

-- MODIFIED BY: <Jorge Obando>
-- MODIFIED DATE: 07/26/2022
-- Code Reviewer:
-- Note: Adding additional columns to return from dbo.State
-- =============================================

CREATE proc [dbo].[Locations_SelectAll]
					@PageIndex int
					,@PageSize int

/* ---- TEST CODE ----
	
	Declare @PageIndex int = 0
			,@PageSize int = 5

	Execute [dbo].[Locations_SelectAll]
					@PageIndex 
					,@PageSize 

*/ ---- TEST CODE ----

AS 

BEGIN
	Declare @offset int = @PageIndex * @PageSize

	SELECT l.[Id]
		  ,lt.[Id] 
		  ,lt.[Name] as LocationType
		  ,l.[LineOne]
		  ,l.[LineTwo]
		  ,l.[City]
		  ,l.[Zip]
		  ,s.[Id] as StateId
		  ,s.[Name] As StateName
		  ,s.[Code] as StateCode
		  ,l.[Latitude]
		  ,l.[Longitude]
		  ,l.[DateCreated]
		  ,l.[DateModified]
		  ,l.[CreatedBy]
		  ,l.[ModifiedBy]
		  ,TotalCount = COUNT(1) OVER()
	FROM [dbo].[Locations] as l inner join dbo.LocationTypes as lt
							on lt.Id = l.LocationTypeId
							inner join dbo.States as s
							on s.Id = l.StateId						
	ORDER BY l.Id

	OFFSET @offSet Rows
	Fetch Next @PageSize Rows ONLY
END


GO
/****** Object:  StoredProcedure [dbo].[Locations_Update]    Script Date: 12/2/2022 3:13:23 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author: <Hyun Kim>
-- Create date: <7/9/22>
-- Description: <Locations_Update>
-- Code Reviewer:

-- MODIFIED BY: <Jorge Obando>
-- MODIFIED DATE: 7/28/2022
-- Code Reviewer:
-- Note: The Insert service was expecting to received @DateCreated and @DateModified as parameters so I removed it.
-- =============================================

CREATE proc [dbo].[Locations_Update]
			 @LocationTypeId int
			,@LineOne nvarchar(255)
			,@LineTwo nvarchar(255)
			,@City nvarchar(225)
			,@Zip nvarchar(50)
			,@StateId int
			,@Latitude float 
			,@Longitude float
			,@UserId int
			,@Id int 
			
/* ----------------- TEST CODE ---------------- 

	Declare @Id int = 5

	Declare @LocationTypeId int = 1
			,@LineOne nvarchar(255) = 'testlocationaddress'
			,@LineTwo nvarchar(255) = 'test location linetwo'
			,@City nvarchar(225) = 'San Diego'
			,@Zip nvarchar(50) = '945457'
			,@StateId int = 5
			,@Latitude float = '24.35114'
			,@Longitude float = '-70.24527'
			,@UserId int = 2


	Execute dbo.Locations_Update
			@LocationTypeId
			,@LineOne 
			,@LineTwo
			,@City 
			,@Zip 
			,@StateId 
			,@Latitude 
			,@Longitude 
			,@Id 
			,@UserId

			Select @Id

			Select *
			From dbo.Locations

*/ ------------------END TEST CODE ----------

AS 

BEGIN

UPDATE [dbo].[Locations]

   SET [LocationTypeId] = @LocationTypeId
      ,[LineOne] = @LineOne
      ,[LineTwo] = @LineTwo
      ,[City] = @City
      ,[Zip] = @Zip
      ,[StateId] = @StateId
      ,[Latitude] = @Latitude
      ,[Longitude] = @Longitude
      ,[DateModified] = GETUTCDATE()
      ,[ModifiedBy] = @UserId


		WHERE Id = @Id

END

GO
/****** Object:  StoredProcedure [dbo].[LocationTypes_SelectAll]    Script Date: 12/2/2022 3:13:23 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

-- =============================================
-- Author:		<Thomas Sauer>
-- Create date: <07/27/2022>
-- Description:	<Select All LocationTypes>
-- Code Reviewer:

-- MODIFIED BY: n/a
-- MODIFIED DATE: n/a
-- Code Reviewer: 
-- Note: 
-- =============================================

	CREATE PROC [dbo].[LocationTypes_SelectAll] 

	AS

	/*------Test Code------
		
	Execute [dbo].[LocationTypes_SelectAll]


	*/---------------------

	BEGIN


	SELECT [Id]
		  ,[Name]

	  FROM [dbo].[LocationTypes]

	END


GO
/****** Object:  StoredProcedure [dbo].[Messages_DeleteById]    Script Date: 12/2/2022 3:13:23 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO


CREATE PROC [dbo].[Messages_DeleteById]
		@Id int

/*---------Start Test Code-------------

DECLARE @Id int = 5

SELECT * FROM [dbo].[Messages]

WHERE Id = @Id;

EXECUTE [dbo].[Messages_DeleteById] @Id

SELECT * FROM [dbo].[Messages]

WHERE Id = @Id;

---------End Test Code-------------*/

AS

BEGIN

DELETE FROM [dbo].[Messages]
      WHERE Id = @Id;

END
GO
/****** Object:  StoredProcedure [dbo].[Messages_Insert]    Script Date: 12/2/2022 3:13:23 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO



CREATE PROC [dbo].[Messages_Insert]

			@Message nvarchar(1000)
           ,@Subject nvarchar(100)
           ,@RecipientId int
           ,@SenderId int
           ,@DateSent datetime2
		   ,@DateRead datetime2
		   ,@Id int OUTPUT
         
    


/*---Test Code Start---

SELECT * FROM dbo.Messages

DECLARE  @Id int = 0

DECLARE 
		@Message nvarchar(1000) = '20Aug22'
		,@Subject nvarchar(100) = 'Morning Greeting'
		,@RecipientId int = '22'
		,@SenderId int = '135'
		,@DateSent datetime2 = '08-20-2022'
		,@DateRead datetime2 = '08-20-2022'

EXECUTE	[dbo].[Messages_Insert]
			@Message 
           ,@Subject 
           ,@RecipientId 
           ,@SenderId 
           ,@DateSent
		   ,@DateRead
		   ,@Id OUTPUT

SELECT * FROM dbo.Messages

Where Id = @Id 


*/---Test Code End---

AS

BEGIN

INSERT INTO [dbo].[Messages]
           ([Message]
           ,[Subject]
           ,[RecipientId]
           ,[SenderId]
           ,[DateSent]
		   ,[DateRead])
           
VALUES
        	(@Message 
           ,@Subject 
           ,@RecipientId 
           ,@SenderId 
           ,@DateSent
		   ,@DateRead) 
		   SET @Id = SCOPE_IDENTITY()

END
GO
/****** Object:  StoredProcedure [dbo].[Messages_Select_ByRece]    Script Date: 12/2/2022 3:13:23 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO



CREATE PROC [dbo].[Messages_Select_ByRece]
		@RecipientId int
		,@PageIndex int
		,@PageSize int


/*--------TEST CODE---------

SELECT * FROM [dbo].[Messages]

DECLARE				@RecipientId int = 152
					,@PageIndex int = 0
					,@PageSize int = 10


EXECUTE [dbo].[Messages_Select_ByRece] 
					@RecipientId
					,@PageIndex 
					,@PageSize 


--------TEST CODE---------*/

AS

BEGIN

DECLARE @offset int = @PageIndex * @PageSize

SELECT m.Id
      ,m.Message
      ,m.Subject
      ,m.RecipientId
      ,m.SenderId
      ,m.DateSent
      ,m.DateRead
      ,m.DateModified
      ,m.DateCreated
	  ,SenderData = 
		(
		SELECT TOP 1
			up.FirstName
			,up.LastName
			,up.Mi
			,up.AvatarUrl
		FROM dbo.userProfiles up
		WHERE up.UserId = m.RecipientId
		FOR JSON AUTO
		)

	  ,RecipientData = (
			SELECT TOP 1
				up.FirstName
				,up.LastName
				,up.Mi
				,up.AvatarUrl
				
			FROM dbo.userProfiles up
			WHERE up.UserId = m.SenderId
			FOR JSON AUTO
						)
	,TotalCount = COUNT(1) OVER()

FROM [dbo].[Messages] as m
inner join dbo.userProfiles as u on m.SenderId = u.UserId

Where m.RecipientId = @RecipientId

ORDER BY m.Id Desc
OFFSET @offSet Rows
Fetch Next @PageSize Rows ONLY

END
GO
/****** Object:  StoredProcedure [dbo].[Messages_Select_BySenderId]    Script Date: 12/2/2022 3:13:23 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO


CREATE PROC [dbo].[Messages_Select_BySenderId]
		@SenderId int
		,@PageIndex int
		,@PageSize int

/*---------TEST CODE-----------

SELECT * FROM [dbo].[userProfiles]

SELECT * FROM [dbo].[Messages]

DECLARE

		@SenderId int = 135
		,@PageIndex int = 0
		,@PageSize int = 5

EXECUTE [dbo].[Messages_Select_BySenderId] 

		@SenderId
		,@PageIndex 
		,@PageSize 
  

---------TEST CODE-----------*/


AS


BEGIN

DECLARE @offset int = @PageIndex * @PageSize

SELECT m.Id
      ,m.Message
      ,m.Subject
      ,m.RecipientId
      ,m.SenderId
      ,m.DateSent
      ,m.DateRead
      ,m.DateModified
      ,m.DateCreated
	  ,SenderData =
	  (SELECT TOP 1
		up.FirstName
		,up.LastName
		,up.Mi
		,up.AvatarUrl
		FROM dbo.userProfiles up
		WHERE up.UserId = m.RecipientId
		FOR JSON AUTO)
	 
	  ,RecipientData =
	  (
		SELECT TOP 1
		up.FirstName
		,up.LastName
		,up.Mi
		,up.AvatarUrl
		FROM dbo.userProfiles up
		WHERE up.UserId = m.SenderId
		FOR JSON AUTO
						)
		,TotalCount = COUNT(1) OVER()

FROM [dbo].[Messages] as m
inner join dbo.userProfiles as u on m.SenderId = u.UserId 

Where m.SenderId = @SenderId

ORDER BY m.Id Desc
	OFFSET @offSet Rows
	Fetch Next @PageSize Rows ONLY

END
GO
/****** Object:  StoredProcedure [dbo].[Messages_Select_Conversation]    Script Date: 12/2/2022 3:13:23 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO




CREATE PROC [dbo].[Messages_Select_Conversation]
		@RecipientId int
		,@SenderId int
		,@PageIndex int
		,@PageSize int

/*

SELECT * FROM [dbo].[Messages]

DECLARE		
			@RecipientId int = 152
			,@SenderId int = 135
			,@PageIndex int = 0
			,@PageSize int = 10

			EXECUTE [dbo].[Messages_Select_Conversation] 
			
			@RecipientId
			,@SenderId
			,@PageIndex 
			,@PageSize 
			
*/

AS

BEGIN

DECLARE @offset int = @PageIndex * @PageSize

SELECT m.Id
      ,m.Message
      ,m.Subject
      ,m.RecipientId
      ,m.SenderId
      ,m.DateSent
      ,m.DateRead
      ,m.DateModified
      ,m.DateCreated
	  ,SenderData = 
	  (SELECT TOP 1
		up.FirstName
		,up.LastName
		,up.Mi
		,up.AvatarUrl
		FROM dbo.userProfiles up
		WHERE up.UserId = m.RecipientId
		FOR JSON AUTO)
	
	  ,RecipientData = 
		(SELECT TOP 1
		up.FirstName
		,up.LastName
		,up.Mi
		,up.AvatarUrl
		FROM dbo.userProfiles up
		WHERE up.UserId = m.SenderId
		FOR JSON AUTO)
						
		,TotalCount = COUNT(1) OVER()

FROM [dbo].[Messages] as m
 inner join dbo.userProfiles as u on m.SenderId = u.UserId		

  WHERE m.SenderId = @SenderId AND m.RecipientId = @RecipientId

  
ORDER BY m.Id Desc
	OFFSET @offSet Rows
	Fetch Next @PageSize Rows ONLY
		 
END
GO
/****** Object:  StoredProcedure [dbo].[Messages_Update]    Script Date: 12/2/2022 3:13:23 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO



CREATE PROC [dbo].[Messages_Update]
	   
	  @Message   nvarchar(1000)
      ,@Subject nvarchar (100)
      ,@RecipientId  int
      ,@SenderId int
      ,@DateSent  datetime2(7)
      ,@DateRead datetime2(7)
	  ,@Id int
     

/*-------------TEST CODE---------------


DECLARE  @Id int = 2;

DECLARE
		@Message   nvarchar(1000) = 'Update2'
      ,@Subject nvarchar (100) ='Morning Message'
      ,@RecipientId  int = 2
      ,@SenderId int = 2
      ,@DateSent  datetime2 = '2022-07-31'
      ,@DateRead datetime2 ='2022-07-31'

SELECT * FROM [dbo].[Messages]

WHERE Id = @Id

EXECUTE [dbo].[Messages_Update]

			@Message
			,@Subject
			,@RecipientId
			,@SenderId
			,@DateSent
			,@DateRead
			,@Id 

SELECT * FROM [dbo].[Messages]

	WHERE Id = @Id
 
-------------TEST CODE---------------*/

AS

BEGIN

Declare @dateNow datetime2 = GETUTCDATE();

UPDATE [dbo].[Messages]

   SET [Message] = @Message
      ,[Subject] = @Subject
      ,[RecipientId] = @SenderId
      ,[SenderId] = @SenderId
      ,[DateSent] = @DateSent
      ,[DateRead] = @DateRead
      ,[DateModified] = @dateNow
     
 WHERE Id  = @Id

END
GO
/****** Object:  StoredProcedure [dbo].[NewsletterContent_Delete_ById]    Script Date: 12/2/2022 3:13:23 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE proc [dbo].[NewsletterContent_Delete_ById]
			@Id int

/*------------- TEST CODE ---------------

	Declare @Id int = 2

	Select *
	From dbo.NewsletterContent
	Where Id = @Id

	Execute dbo.NewsletterContent_Delete_ById
		@Id

	Select *
	From dbo.NewsletterContent
	Where Id = @Id

*/ -------------END TEST CODE ---------------

AS

BEGIN

DELETE FROM dbo.NewsletterContent
	Where Id = @Id

END
GO
/****** Object:  StoredProcedure [dbo].[NewsletterContent_Insert]    Script Date: 12/2/2022 3:13:23 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE proc [dbo].[NewsletterContent_Insert]

	@TemplateKeyId int 
	,@PageId int
	,@Value nvarchar(max)
	,@CreatedBy int
	,@Id int OUTPUT

/*------------- TEST CODE ---------------
	Declare @Id int = 1;

	Declare @TemplateKeyId int = 1
			,@PageId int = 1
			,@Value nvarchar(MAX) = 'Value 1'
			,@CreatedBy int = 1

	Execute dbo.NewsletterContent_Insert
			@TemplateKeyId
			,@PageId
			,@Value
			,@CreatedBy
			,@Id OUTPUT

	Select *
	From dbo.NewsletterContent
	Where Id = @Id

*/ -------------END TEST CODE ---------------

AS

BEGIN

INSERT INTO [dbo].[NewsletterContent]
           ([TemplateKeyId]
		   ,[PageId]
           ,[Value]
		   ,[DateCreated]
           ,[DateModified]
           ,[CreatedBy])
     VALUES
           (@TemplateKeyId
		   ,@PageId
           ,@Value
		   ,GETUTCDATE()
		   ,GETUTCDATE()
           ,@CreatedBy)

		SET @Id = SCOPE_IDENTITY()

END
GO
/****** Object:  StoredProcedure [dbo].[NewsletterContent_Select_ByCreatedBy]    Script Date: 12/2/2022 3:13:23 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE proc [dbo].[NewsletterContent_Select_ByCreatedBy]
			@CreatedBy int
			,@PageSize int 
			,@PageIndex int

AS

/* ------------- TEST CODE ------------------------------

Declare			@CreatedBy int = 1
				,@PageIndex int = 0
				,@PageSize int = 10
				
	

	Execute [dbo].[NewsletterContent_Select_ByCreatedBy] 
				@CreatedBy
				,@PageSize
				,@PageIndex

select *
from dbo.NewsletterContent


*/--------------- END TEST CODE --------------------------

BEGIN

	Declare @offset int = @PageIndex * @PageSize

	SELECT	
		nc.[Id]
		--,nc.[TemplateKeyId]
		,nc.[PageId]
		,nc.[Value]
		,nc.[DateCreated]
		,nc.[DateModified]
		,nkt.[Id]
		,nkt.[Name]
		,TotalCount = COUNT(1) OVER()

	FROM dbo.NewsletterContent as nc inner join dbo.NewsletterKeyTypes as nkt
		on nkt.Id = nc.TemplateKeyId
	
	WHERE		nc.CreatedBy = @CreatedBy
	ORDER BY	nc.Id
	OFFSET @offset ROWS FETCH NEXT @PageSize ROWS ONLY


END
GO
/****** Object:  StoredProcedure [dbo].[NewsletterContent_Select_ById]    Script Date: 12/2/2022 3:13:23 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE proc [dbo].[NewsletterContent_Select_ById]
			@Id int


/*----------------- TEST CODE ----------------------

	Declare @Id int = 1;
		Execute dbo.NewsletterContent_Select_ById
		@Id


*/ ---------------END TEST CODE ---------------------


AS 

BEGIN

SELECT n.[Id]
			,nt.[Name]
			,u.[Id] as CreatedBy

	FROM [dbo].[NewsletterContent] as n inner join dbo.NewsletterTypes as nt
						on nt.Id = n.TemplateKeyId
						inner join dbo.Users as u
						on u.Id = n.CreatedBy

			Where @Id = n.Id

END
GO
/****** Object:  StoredProcedure [dbo].[NewsletterContent_SelectAll]    Script Date: 12/2/2022 3:13:23 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE proc [dbo].[NewsletterContent_SelectAll]  
										@PageIndex int 
                                       ,@PageSize int

AS


/* --------------- TEST CODE ---------------

Declare @PageIndex int = 0
			,@PageSize int = 10

Execute [dbo].[NewsletterContent_SelectAll]
				@PageIndex 
				,@PageSize 

		Select *
		FROM dbo.NewsletterContent

 --------------END TEST CODE -------------- */

BEGIN

	   DECLARE @offset int = @PageIndex * @PageSize

       SELECT	*
				,TotalCount = COUNT(1) OVER()

		FROM dbo.NewsletterContent
	
		ORDER BY	Id
		OFFSET @offset ROWS
		FETCH NEXT  @PageSize ROWS ONLY

END
GO
/****** Object:  StoredProcedure [dbo].[NewsletterContent_Update]    Script Date: 12/2/2022 3:13:23 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE   proc [dbo].[NewsletterContent_Update]
			@Id int
			,@TemplateKeyId int
			,@PageId int
			,@Value int 
			,@CreatedBy int

/* ----------------- TEST CODE ---------------- 
Declare	 @Id int = 1

Declare	 @TemplateKeyId int = 10
		 ,@PageId int = 11
		 ,@Value int = 12
		 ,@CreatedBy = 13

Execute dbo.NewsletterContent_Update 
		@Id
		,@TemplateKeyId
		,@PageId
		,@Value
		,@CreatedBy

Select *
FROM dbo.NewsletterContent
WHERE Id = @Id

*/ ------------------END TEST CODE ----------

AS 

BEGIN

UPDATE	[dbo].[NewsletterContent]
		
		SET	[TemplateKeyId] = @TemplateKeyId
			,[PageId] = @PageId
			,[Value] = @Value
			,[CreatedBy] = @CreatedBy

		WHERE Id = @Id
END
GO
/****** Object:  StoredProcedure [dbo].[NewsletterKeyTypes_SelectAll]    Script Date: 12/2/2022 3:13:23 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROC [dbo].[NewsletterKeyTypes_SelectAll]
		@PageIndex int 
		,@PageSize int

	AS

	/*------Test Code------

	DECLARE  @PageIndex int = 0
			 ,@PageSize int = 10
		
	Execute [dbo].[NewsletterKeyTypes_SelectAll]
			@PageIndex 
			,@PageSize 

	*/---------------------

	BEGIN
		Declare @offset int = @PageIndex * @PageSize

	SELECT [Id]
		  ,[Name]

	  FROM [dbo].[NewsletterKeyTypes]

	  ORDER BY Id

	  OFFSET @offSet Rows
	Fetch Next @PageSize Rows ONLY

	END
GO
/****** Object:  StoredProcedure [dbo].[NewsletterSubscriptions_Insert]    Script Date: 12/2/2022 3:13:23 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

-- =============================================
-- Author: <Brandon Peace>
-- Create date: 
-- Description: <NewsletterSubscriptions_Insert>
-- Code Reviewer: James Hughes

-- MODIFIED BY: author
-- MODIFIED DATE: 07/13/2022
-- Code Reviewer:
-- Note:
-- =============================================

CREATE proc [dbo].[NewsletterSubscriptions_Insert] 
			@Email nvarchar(255)
			,@isSubscribed bit
	
AS

/*------------------- TEST CODE ------------------

DECLARE @Email nvarchar(255) = 'testemail12@yahoo.com'
		,@isSubscribed bit = 0

EXECUTE dbo.NewsletterSubscriptions_Insert
		@Email
		,@isSubscribed

SELECT Email, isSubscribed
FROM dbo.NewsletterSubscriptions


*/-------------------END TEST CODE ------------------


BEGIN

INSERT INTO dbo.NewsletterSubscriptions
			([Email]
			,[isSubscribed])

VALUES (@Email
		,@isSubscribed)

END
GO
/****** Object:  StoredProcedure [dbo].[NewsletterSubscriptions_SelectAll]    Script Date: 12/2/2022 3:13:23 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

-- =============================================
-- Author: <Brandon Peace>
-- Create date: 
-- Description: <NewsletterSubscriptions_SelectAll>
-- Code Reviewer: James Hughes

-- MODIFIED BY: author
-- MODIFIED DATE: 07/13/2022
-- Code Reviewer:
-- Note:
-- =============================================

CREATE proc [dbo].[NewsletterSubscriptions_SelectAll]
			@pageIndex int 
			,@pageSize int
			
AS

BEGIN

 DECLARE @offset int = @pageIndex * @pageSize

        SELECT   Email, isSubscribed
                ,TotalCount = COUNT(1) OVER()
        FROM    dbo.NewsletterSubscriptions 
        ORDER BY Email 

	OFFSET @offset ROWS
	FETCH NEXT @pageSize ROWS ONLY

END
GO
/****** Object:  StoredProcedure [dbo].[NewsletterSubscriptions_SelectAll_Subscribed]    Script Date: 12/2/2022 3:13:23 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

-- =============================================
-- Author: <Brandon Peace>
-- Create date: 
-- Description: <NewsletterSubscriptions_SelectAll_Subscribed>
-- Code Reviewer: James Hughes

-- MODIFIED BY: author
-- MODIFIED DATE: 07/13/2022
-- Code Reviewer:
-- Note:
-- =============================================

CREATE proc [dbo].[NewsletterSubscriptions_SelectAll_Subscribed]
			
AS

/* ------------- TEST CODE ------------------------------

Execute [dbo].[NewsletterSubscriptions_SelectAll_Subscribed]
				

		Select *
		from dbo.NewsletterSubscriptions
		Where isSubscribed = 0
		

*/--------------- END TEST CODE -------------------------

BEGIN

Select	*
FROM	dbo.NewsletterSubscriptions
WHERE	isSubscribed = 1

END
GO
/****** Object:  StoredProcedure [dbo].[NewsletterSubscriptions_Update]    Script Date: 12/2/2022 3:13:23 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

-- =============================================
-- Author: <Brandon Peace>
-- Create date: 
-- Description: <NewsletterSubscriptions_Update>
-- Code Reviewer: James Hughes

-- MODIFIED BY: author
-- MODIFIED DATE: 07/13/2022
-- Code Reviewer:
-- Note:
-- =============================================

CREATE proc [dbo].[NewsletterSubscriptions_Update]
			@Email nvarchar(255)
			,@isSubscribed bit
			
AS


/*----------------- TEST CODE ------------------

Declare		@Email nvarchar(255) = 'testemail2@yahoo.com'
			,@isSubscribed bit = 1

Execute		dbo.NewsletterSubscriptions_Update
			@Email
			,@isSubscribed

Select *
From dbo.NewsletterSubscriptions 
WHERE Email = @Email


*/----------------- END TEST CODE --------------

BEGIN

	UPDATE [dbo].[NewsletterSubscriptions]
	
	SET  [isSubscribed] = @isSubscribed
	WHERE Email = @Email


END
GO
/****** Object:  StoredProcedure [dbo].[OrgAdminDashboard_GetProsposalsByOrg]    Script Date: 12/2/2022 3:13:23 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO


CREATE proc [dbo].[OrgAdminDashboard_GetProsposalsByOrg]
	@OrganizationId int,
	@PageIndex int,
	@PageSize int

as

/* Test Code
	
	Declare @OrganizationId int = 175,
			@PageIndex int = 0,
			@PageSize int = 100

	Execute dbo.OrgAdminDashboard_GetProsposalsByOrg
		@OrganizationId,
		@PageIndex,
		@PageSize
			


*/

Begin
	  DECLARE @offset int = @PageIndex * @PageSize

       SELECT	p.[Id]
			  ,j.[Id] AS [JobId]
			  ,jt.[Name] AS JobType
			  ,ot.[Name] As Occupation
			  ,j.[Title] as [Job Title]
			  ,j.[Description]
			  ,p.[Bid]
			  ,p.[Duration]
			  ,p.[Details] 
			  ,ps.[Name] as [ProposalStatus]
			  ,p.DateCreated
			  ,u.[Id] as [CreatedBy]
			  , u.Email
			  ,u.[Id] as [ModifiedBy]
			  , TotalCount = COUNT (1) OVER()
		

		From [dbo].[Proposals] AS p 
		INNER JOIN dbo.Jobs as j On j.Id = p.JobId 
		INNER JOIN dbo.Users as u ON u.Id = p.CreatedBy 
		INNER JOIN dbo.ProposalStatus as ps ON ps.Id = p.ProposalStatusId 
		INNER JOIN dbo.Locations as l ON l.Id = j.LocationId 
		INNER JOIN dbo.Organizations as o ON j.OrganizationId = o.Id
		INNER JOIN dbo.States as s ON l.StateId = s.Id 
		INNER JOIN dbo.JobTypes as jt ON j.JobTypeId = jt.Id 
		INNER JOIN dbo.OrganizationTypes as ot ON ot.Id = o.OrganizationTypeId
		INNER JOIN dbo.LocationTypes as lt ON lt.Id = l.LocationTypeId
		Where j.OrganizationId = @OrganizationId
	
	
		ORDER BY	Id
		OFFSET @offset ROWS
		FETCH NEXT  @PageSize ROWS ONLY
End
GO
/****** Object:  StoredProcedure [dbo].[OrgAdminDashboard_GetProsposalStats]    Script Date: 12/2/2022 3:13:23 PM ******/
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
/****** Object:  StoredProcedure [dbo].[OrgAdminDashboard_GetProsposalStats15]    Script Date: 12/2/2022 3:13:23 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO


CREATE proc [dbo].[OrgAdminDashboard_GetProsposalStats15]
	@OrganizationId int,
	@TimezoneOffset int

as

/* TestCode

	Declare @OrganizationId int = 264
	Declare @TimezoneOffset int = 420

	Execute dbo.OrgAdminDashBoard_GetProsposalStats15
		@OrganizationId,
		@TimezoneOffset

*/

Begin
	CREATE TABLE countofdays15 (
		date_created datetime2(7),
		count_of_proposals int
	)

	DECLARE @CurrentDate datetime2(7) = GETUTCDATE()
	DECLARE @Counter int
	SET @Counter = 0
	WHILE(@Counter < 15)
		Begin
			Declare @DateCreated datetime2(7) = CAST(DATEADD(minute, -@TimezoneOffset, DATEADD(day, -@Counter, @CurrentDate)) as DATE)
			Declare @TotalCount int = 0
			
			SELECT @TotalCount = COUNT(1) OVER()
			FROM dbo.Proposals as p inner join dbo.Jobs as j
			on p.JobId = j.Id
			inner join dbo.Organizations as o
			on j.OrganizationId = o.Id
			Where j.OrganizationId = @OrganizationId AND CAST(p.DateCreated AS DATE) = CAST(DATEADD(minute, -@TimezoneOffset, DATEADD(day, -@Counter, @CurrentDate)) as DATE)

			INSERT into countofdays15
			SELECT @DateCreated
				  ,@TotalCount

			

			SET @Counter = @Counter + 1
	End
	

	SELECT date_created,
			count_of_proposals
	From countofdays15

	DROP TABLE countofdays15
End
GO
/****** Object:  StoredProcedure [dbo].[OrgAdminDashboard_GetProsposalStats30]    Script Date: 12/2/2022 3:13:23 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO


CREATE proc [dbo].[OrgAdminDashboard_GetProsposalStats30]
	@OrganizationId int,
	@TimezoneOffset int

as

/* TestCode

	Declare @OrganizationId int = 264
	Declare @TimezoneOffset int = 420

	Execute dbo.OrgAdminDashBoard_GetProsposalStats30
		@OrganizationId,
		@TimezoneOffset

*/

Begin
	CREATE TABLE countofdays30 (
		date_created datetime2(7),
		count_of_proposals int
	)

	DECLARE @CurrentDate datetime2(7) = GETUTCDATE()
	DECLARE @Counter int
	SET @Counter = 0
	WHILE(@Counter < 30)
		Begin
			Declare @DateCreated datetime2(7) = CAST(DATEADD(minute, -@TimezoneOffset, DATEADD(day, -@Counter, @CurrentDate)) as DATE)
			Declare @TotalCount int = 0
			
			SELECT @TotalCount = COUNT(1) OVER()
			FROM dbo.Proposals as p inner join dbo.Jobs as j
			on p.JobId = j.Id
			inner join dbo.Organizations as o
			on j.OrganizationId = o.Id
			Where j.OrganizationId = @OrganizationId AND CAST(p.DateCreated AS DATE) = CAST(DATEADD(minute, -@TimezoneOffset, DATEADD(day, -@Counter, @CurrentDate)) as DATE)

			INSERT into countofdays30
			SELECT @DateCreated
				  ,@TotalCount

			

			SET @Counter = @Counter + 1
	End
	

	SELECT date_created,
			count_of_proposals
	From countofdays30

	DROP TABLE countofdays30
End
GO
/****** Object:  StoredProcedure [dbo].[OrgAdminDashboard_Select_JobsByActiveStatus]    Script Date: 12/2/2022 3:13:23 PM ******/
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
/****** Object:  StoredProcedure [dbo].[OrgAdminDashboard_Select_JobsByJobType]    Script Date: 12/2/2022 3:13:23 PM ******/
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
/****** Object:  StoredProcedure [dbo].[OrganizationFollowers_Delete_ById]    Script Date: 12/2/2022 3:13:23 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO



CREATE proc [dbo].[OrganizationFollowers_Delete_ById]
	@OrganizationId int
	,@FollowerId int 

-- =============================================
-- Author:		<Jesse Lopez>
-- Create date: <20220723>
-- Description:	<Organization Follower>

-- MODIFIED BY: n/a
-- MODIFIED DATE: n/a
-- Code Reviewer: Joshua Eslava
-- Note: 
-- =============================================
AS
/*------TEST CODE------

		Declare  @OrganizationId int = 1
		        ,@FollowerId int = 4
 
		  	Select *
		From dbo.OrganizationFollowers
		Where OrganizationId = @OrganizationId
		AND FollowerId = @FollowerId 

		Execute dbo.OrganizationFollowers_Delete_ById
			@OrganizationId
			,@FollowerId
		
		Select *
		From dbo.OrganizationFollowers
		Where OrganizationId = @OrganizationId
		AND FollowerId = @FollowerId
		
			Select *
		From dbo.OrganizationFollowers
----------------------------------------
		  	select* from dbo.Organizations 
*/
BEGIN

DELETE FROM [dbo].[OrganizationFollowers]
    Where OrganizationId = @OrganizationId
		AND FollowerId = @FollowerId

END


GO
/****** Object:  StoredProcedure [dbo].[OrganizationFollowers_Insert]    Script Date: 12/2/2022 3:13:23 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO


CREATE PROC [dbo].[OrganizationFollowers_Insert]
            @OrganizationId int
           ,@FollowerId int

-- =============================================
-- Author:		<Jesse Lopez>
-- Create date: <20220720>
-- Description:	<Inser Organization Follower >

-- MODIFIED BY: n/a
-- MODIFIED DATE: n/a
-- Code Reviewer: Joshua Eslava
-- Note: 
-- =============================================
AS
/*-----TESTCODE-----
Declare  @OrganizationId int =  2
Declare  @FollowerId int = 20
          				
EXECUTE dbo.OrganizationFollowers_Insert 
@OrganizationId
,@FollowerId
 
SELECT * FROM dbo.OrganizationFollowers
Where OrganizationId = @OrganizationId
 
------------*/
BEGIN
	

INSERT INTO [dbo].[OrganizationFollowers]
           ([OrganizationId]
           ,[FollowerId])
     VALUES
           (@OrganizationId
           ,@FollowerId)

END


GO
/****** Object:  StoredProcedure [dbo].[OrganizationFollowers_SelectAllPaginated]    Script Date: 12/2/2022 3:13:23 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO


-------------------------------------------------
-- Author: Jesse Lopez
-- Create date: 20-July-2022
-- Description: SelectAll to OrganizationFollowers

-- MODIFIED BY:
-- MODIFIED DATE:
-- Code Reviewer:
-- Note:
--------------------------------------------------

CREATE PROC [dbo].[OrganizationFollowers_SelectAllPaginated]
			@PageIndex int
			,@PageSize int
			
AS
/*------ TEST CODE--------

DECLARE @PageIndex int = 0
DECLARE @PageSize int = 20

EXECUTE dbo.OrganizationFollowers_SelectAllPaginated
		@PageIndex
		,@PageSize


-------------------------------------

		select* from dbo.Organizations
		select* from dbo.Users


	Should only return one record per org.
	I should see every org, regardless of whether or not they have followers.
	Start with a table you want all of, or anchor on.

	'Group by' organization id
	Counts of followers by organization
	T or F if this current user is following this organization

------END TEST CODE -----*/

BEGIN

DECLARE @Offset int = @PageIndex * @PageSize

SELECT orgf.[OrganizationId]
       ,orgf.[FollowerId]
       ,O.Name
	   ,O.Headline
	   ,O.Description
	   ,O.Logo
	   ,O.Phone
	   ,O.SiteUrl
	   ,Users = (
	   select
	   uf.userid
	   ,uf.FirstName
	   ,uf.LastName
	   from dbo.Users as u inner join dbo.UserProfiles as uf
	   on u.Id = uf.UserId
	   where uf.UserId = UserId
	   for json auto)
      
	
	   ,TotalCount = COUNT(1) OVER()
  FROM [dbo].[OrganizationFollowers] as orgf 
  inner join [dbo].[Organizations] as O
  ON orgf.OrganizationId = O.Id
  inner join dbo.Users as u
  ON  u.Id = orgf.FollowerId 


 
      ORDER BY OrganizationId
	OFFSET @Offset ROWS
	FETCH NEXT @PageSize ROWS ONLY
END


GO
/****** Object:  StoredProcedure [dbo].[OrganizationFollowers_SelectById]    Script Date: 12/2/2022 3:13:23 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

-- =============================================
-- Author:		<Jesse Lopez>
-- Create date: <20220720>
-- Description:	<SelectByIdOrg Organization Follower >

-- MODIFIED BY: n/a
-- MODIFIED DATE: n/a
-- Code Reviewer: Joshua Eslava
-- Note: 
-- =============================================

CREATE PROC [dbo].[OrganizationFollowers_SelectById]
			    @OrganizationId int
			   ,@PageIndex int
			   ,@PageSize int
AS
/*------ TEST CODE--------

DECLARE     @OrganizationId int = 1
           ,@PageIndex int = 0
		   ,@PageSize int = 5

EXECUTE dbo.OrganizationFollowers_SelectById
                      @OrganizationId
					  ,@PageIndex
			          ,@PageSize

-----------------------------------------------
		select* from dbo.Organizations
		select* from dbo.Users
------END TEST CODE -----*/
BEGIN

DECLARE @Offset int = @PageIndex * @PageSize

SELECT  
       orgf.[FollowerId]
	   ,u.Id
       ,up.FirstName
	   ,up.LastName
	   ,up.Mi
	   ,up.AvatarUrl
	   
	   ,TotalCount = COUNT(1) OVER()
  FROM [dbo].[OrganizationFollowers] as orgf 
  inner join [dbo].[Organizations] as O
  ON orgf.OrganizationId = O.Id
  LEFT  join dbo.UserProfiles as up
  ON  up.UserId = orgf.FollowerId 
  inner join dbo.Users as u 
  on up.UserId = u.Id

  Where OrganizationId = @OrganizationId
      ORDER BY OrganizationId Desc
	OFFSET @Offset ROWS
	FETCH NEXT @PageSize ROWS ONLY


END




GO
/****** Object:  StoredProcedure [dbo].[OrganizationFollowers_SelectByIdUser]    Script Date: 12/2/2022 3:13:23 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

-- =============================================
-- Author:		<Jesse Lopez>
-- Create date: <20220720>
-- Description:	<SelectByIdUser an Organization Follower >

-- MODIFIED BY: n/a
-- MODIFIED DATE: n/a
-- Code Reviewer: Joshua Eslava
-- Note: 
-- =============================================

CREATE PROC [dbo].[OrganizationFollowers_SelectByIdUser]
				@FollowerId int
			   ,@PageIndex int
			   ,@PageSize int
AS
/*------ TEST CODE--------

DECLARE		 @FollowerId int = 135
			,@PageIndex int = 0
			,@PageSize int = 5

EXECUTE dbo.OrganizationFollowers_SelectByIdUser
			@FollowerId
			,@PageIndex
			,@PageSize
----------------------------------------
		select* from dbo.Organizations
		select* from dbo.Users
------END TEST CODE -----*/
BEGIN

DECLARE @Offset int = @PageIndex * @PageSize

SELECT  
       orgf.[OrganizationId]
       ,O.Name
	   ,O.Headline
	   ,O.Description
	   ,O.Logo
	   ,O.Phone
	   ,O.SiteUrl

	   ,TotalCount = COUNT(1) OVER()
  FROM [dbo].[OrganizationFollowers] as orgf 
  inner join [dbo].[Organizations] as O
  ON orgf.OrganizationId = O.Id
  Left  join dbo.UserProfiles as up
  ON  up.UserId = orgf.FollowerId 
  
  Where FollowerId = @FollowerId

    ORDER BY FollowerId Desc
	OFFSET @Offset ROWS
	FETCH NEXT @PageSize ROWS ONLY

END




GO
/****** Object:  StoredProcedure [dbo].[Organizations_Delete_ById]    Script Date: 12/2/2022 3:13:23 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

-- =============================================
-- Author:		<Matt Prothero>
-- Create date: <20220705>
-- Description:	<Delete an Organization >
-- Code Reviewer: Morgan Frost

-- MODIFIED BY: n/a
-- MODIFIED DATE: n/a
-- Code Reviewer: 
-- Note: 
-- =============================================

CREATE proc [dbo].[Organizations_Delete_ById]
	@Id int

/*------TEST CODE------

		Declare @Id int = 3

		Select *
		From dbo.Organizations
		Where Id = @Id

		Execute dbo.Organizations_Delete_ById
			@Id

		Select *
		From dbo.Organizations
		Where Id = @Id
*/

AS

BEGIN

DELETE FROM [dbo].[Organizations]
	Where Id = @Id

END


GO
/****** Object:  StoredProcedure [dbo].[Organizations_Insert]    Script Date: 12/2/2022 3:13:23 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

-- =============================================
-- Author:		<Matt Prothero>
-- Create date: <20220701>
-- Description:	<Insert an Organization >
-- Code Reviewer: Morgan Frost

-- MODIFIED BY: n/a
-- MODIFIED DATE: n/a
-- Code Reviewer: 
-- Note: 
-- =============================================

	CREATE   PROC [dbo].[Organizations_Insert]
		   
			@LocationTypeId int
			,@LineOne nvarchar(255)
			,@LineTwo nvarchar(255)
			,@City nvarchar(225)
			,@Zip nvarchar(50)
			,@StateId int
			,@Latitude float 
			,@Longitude float
		   
		   ,@OrganizationTypeId int 
           ,@Name nvarchar(200) 
           ,@Headline nvarchar(200) 
           ,@Description nvarchar(MAX) 
           ,@Logo nvarchar(255) 
           ,@Phone nvarchar(50) 
           ,@SiteUrl nvarchar(255) 
		   ,@UserId int
		   ,@Id int OUTPUT
		

	AS
	
	/* -------- TEST CODE --------
		Declare		@Id int = 0;
	
		Declare		
					@LocationTypeId int = '3'
					,@LineOne nvarchar(255) = 'Insert test'
					,@LineTwo nvarchar(255) = 'SQL Test'
					,@City nvarchar(225) = 'Yuma'
					,@Zip nvarchar(50) = '85365'
					,@StateId int = '3'
					,@Latitude float = '423455'
					,@Longitude float = '432432'

					,@OrganizationTypeId int = 6
					,@Name nvarchar(200) = 'Test 7'
					,@Headline nvarchar(200) = 'Headline'
					,@Description nvarchar(MAX) = 'Description, this is a really good description for a fake organization.'
					,@Logo nvarchar(255) = 'NH Logo'
					,@Phone nvarchar(50) = 'NH 1800TESTCODE'
					,@SiteUrl nvarchar(255) = 'www.NH.com'
					,@UserId int = '2'

		Execute		[dbo].[Organizations_Insert]
					

					@LocationTypeId 
					,@LineOne 
					,@LineTwo 
					,@City 
					,@Zip
					,@StateId 
					,@Latitude  
					,@Longitude 

					,@OrganizationTypeId 
					,@Name 
					,@Headline 
					,@Description 
					,@Logo 
					,@Phone 
					,@SiteUrl 
					,@UserId
					,@Id OUTPUT
					

			Select *
			From [dbo].[Organizations]
			WHERE Id = @Id

	*/ -------------------------

	BEGIN

	DECLARE	 @LocationId int 

	INSERT INTO		[dbo].[Locations] (
					LocationTypeId,
					LineOne,
					LineTwo,
					City,
					Zip
					,StateId
					,Latitude
					,Longitude
					,CreatedBy
					,ModifiedBy
					)
		VALUES			(@LocationTypeId
					,@LineOne 
					,@LineTwo
					,@City 
					,@Zip 
					,@StateId 
					,@Latitude 
					,@Longitude 
					,@UserId
					,@UserId 
					)
 
		 SET @LocationId = SCOPE_IDENTITY();
	INSERT INTO		[dbo].[Organizations]
					([OrganizationTypeId]
					,[Name]
					,[Headline]
					,[Description]
					,[Logo]
					,[LocationId]
					,[Phone]
					,[SiteUrl]
					,[CreatedBy])
    
		VALUES
					(@OrganizationTypeId
					,@Name
					,@Headline
					,@Description
					,@Logo
					,@LocationId
					,@Phone
					,@SiteUrl
					,@UserId)

			   SET @Id = SCOPE_IDENTITY();
	END


GO
/****** Object:  StoredProcedure [dbo].[Organizations_Select_ByCreatedBy]    Script Date: 12/2/2022 3:13:23 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Matt Prothero>
-- Create date: <20220706>
-- Description:	<Select Organization By CreatedBy >
-- Code Reviewer: Morgan Frost

-- MODIFIED BY: n/a
-- MODIFIED DATE: n/a
-- Code Reviewer: 
-- Note: 
-- =============================================

	CREATE PROC [dbo].[Organizations_Select_ByCreatedBy] 
					
			@PageIndex int
			,@PageSize int
			,@CreatedBy int

	AS

	/*--------------Test Code--------------

	Declare		@PageIndex int = 0
				,@PageSize int = 10
				,@CreatedBy int = 603
	

	Execute [dbo].[Organizations_Select_ByCreatedBy] 
				@PageIndex
				,@PageSize
				,@CreatedBy

	*/------------End Test Code---------------

		BEGIN

		Declare	   @offset int = @PageIndex * @PageSize

		SELECT	o.[Id] 
			,o.OrganizationTypeId
			,ot.Name as [OrganizationType]
			,o.[Name] as [OrganizationName]
			,o.[Headline]
			,o.[Description]
			,o.[Logo]
			,o.[LocationId] 
			,loct.[Id] as LocationTypeId
			,loct.[Name] as LocationType
			,loc.[LineOne]
			,loc.[LineTwo]
			,loc.[City]
			,loc.[Zip]
			,s.[Id] as StateId
			,s.[Name] as [State]
			,s.[Code]
			,loc.[Latitude] 
			,loc.[Longitude] 
			,o.[Phone]
			,o.[SiteUrl] as Website
			,o.[DateCreated]
			,o.[DateModified]
			,o.[CreatedBy]

			,TotalCount = COUNT(1) OVER()

	FROM		[dbo].[Organizations] as o 
			inner join [dbo].[OrganizationTypes] as ot
				on o.OrganizationTypeId = ot.Id
			inner join [dbo].[Locations] as loc				on o.LocationId = loc.Id
			inner join [dbo].States as s
				on loc.StateId = s.Id
			inner join [dbo].[LocationTypes] as loct
				on loct.Id = loc.LocationTypeId

		WHERE o.CreatedBy = @CreatedBy
		ORDER BY   CreatedBy
			OFFSET @offset ROWS
		FETCH NEXT  @PageSize ROWS ONLY
	END


GO
/****** Object:  StoredProcedure [dbo].[Organizations_SelectAll]    Script Date: 12/2/2022 3:13:23 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Matt Prothero>
-- Create date: <20220701>
-- Description:	<Select All Organizations Id (Paginated) >
-- Code Reviewer: Morgan Frost 

-- MODIFIED BY: n/a
-- MODIFIED DATE: n/a
-- Code Reviewer: 
-- Note: 
-- =============================================

	CREATE PROC [dbo].[Organizations_SelectAll] 
					@PageIndex int 
					,@PageSize int
	AS

	/*------Test Code------

	DECLARE  @PageIndex int = 0
			 ,@PageSize int = 20

	Execute [dbo].[Organizations_SelectAll] 
			@PageIndex 
			,@PageSize 

	*/---------------------

	BEGIN

	Declare @Offset int = @PageIndex * @PageSize

	SELECT	o.[Id] 
			,o.OrganizationTypeId
			,ot.Name as [OrganizationType]
			,o.[Name] as [OrganizationName]
			,o.[Headline]
			,o.[Description]
			,o.[Logo]
			,o.[LocationId] 
			,loct.[Id] as LocationTypeId
			,loct.[Name] as LocationType
			,loc.[LineOne]
			,loc.[LineTwo]
			,loc.[City]
			,loc.[Zip]
			,s.[Id] as StateId
			,s.[Name] as [State]
			,s.[Code]
			,loc.[Latitude] 
			,loc.[Longitude] 
			,o.[Phone]
			,o.[SiteUrl] as Website
			,o.[DateCreated]
			,o.[DateModified]
			,o.[CreatedBy]

			,TotalCount = COUNT(1) OVER()

	FROM		[dbo].[Organizations] as o 

			inner join [dbo].[OrganizationTypes] as ot
				on o.OrganizationTypeId = ot.Id

			inner join [dbo].[Locations] as loc				on o.LocationId = loc.Id

			inner join [dbo].States as s
				on loc.StateId = s.Id

			inner join [dbo].[LocationTypes] as loct
				on loct.Id = loc.LocationTypeId
				

	ORDER BY o.Id DESC
	

	OFFSET @Offset Rows
	Fetch Next @PageSize Rows ONLY

	END


GO
/****** Object:  StoredProcedure [dbo].[Organizations_SelectAll_ByID]    Script Date: 12/2/2022 3:13:23 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROC [dbo].[Organizations_SelectAll_ByID] 
					@PageIndex int 
					,@PageSize int
	AS

	/*------Test Code------

	DECLARE  @PageIndex int = 0
			 ,@PageSize int = 6

	Execute [dbo].[Organizations_SelectAll_ByID] 
			@PageIndex 
			,@PageSize 

	*/---------------------

	BEGIN

	Declare @offset int = @PageIndex * @PageSize

	SELECT	o.[Id] 
			,o.OrganizationTypeId
			,ot.Name as [OrganizationType]
			,o.[Name] as [OrganizationName]
			,o.[Headline]
			,o.[Description]
			,o.[Logo]
			,o.[LocationId] 
			,loct.[Id] as LocationTypeId
			,loct.[Name] as LocationType
			,loc.[LineOne]
			,loc.[LineTwo]
			,loc.[City]
			,s.[Id] as StateId
			,s.[Name] as [State]
			,s.[Code]
			,loc.[Latitude] 
			,loc.[Longitude] 
			,o.[Phone]
			,o.[SiteUrl] as Website
			,o.[DateCreated]
			,o.[DateModified]
			,o.[CreatedBy]

			,TotalCount = COUNT(1) OVER()

	FROM		[dbo].[Organizations] as o 
			inner join [dbo].[OrganizationTypes] as ot
				on o.OrganizationTypeId = ot.Id
			inner join [dbo].[Locations] as loc				on o.LocationId = loc.Id
			inner join [dbo].States as s
				on loc.StateId = s.Id
			inner join [dbo].[LocationTypes] as loct
				on loct.Id = loc.LocationTypeId
				

	ORDER BY o.Id

	OFFSET @offSet Rows
	Fetch Next @PageSize Rows ONLY

	END







SELECT [OrganizationId]
      ,[FollowerId]
  FROM [dbo].[OrganizationFollowers]

GO
/****** Object:  StoredProcedure [dbo].[Organizations_SelectById]    Script Date: 12/2/2022 3:13:23 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Matt Prothero>
-- Create date: <20220701>
-- Description:	<Select By Organization Id >
-- Code Reviewer: Morgan Frost

-- MODIFIED BY: n/a
-- MODIFIED DATE: n/a
-- Code Reviewer: 
-- Note: 
-- =============================================

	CREATE PROC [dbo].[Organizations_SelectById] 

	@Id int

	AS

	/*------Test Code------

	Declare @Id int = 2;
	
	Execute [dbo].[Organizations_SelectById] @Id


	Select *
	From [dbo].[Organizations]
	*/---------------------

	BEGIN

	SELECT	o.[Id] 
			,o.OrganizationTypeId
			,ot.Name as [OrganizationType]
			,o.[Name] as [OrganizationName]
			,o.[Headline]
			,o.[Description]
			,o.[Logo]
			,o.[LocationId] 
			,loct.[Id] as LocationTypeId
			,loct.[Name] as LocationType
			,loc.[LineOne]
			,loc.[LineTwo]
			,loc.[City]
			,loc.[Zip]
			,s.[Id] as StateId
			,s.[Name] as [State]
			,s.[Code]
			,loc.[Latitude] 
			,loc.[Longitude] 
			,o.[Phone]
			,o.[SiteUrl] as Website
			,o.[DateCreated]
			,o.[DateModified]
			,o.[CreatedBy]

			,TotalCount = COUNT(1) OVER()

	FROM		[dbo].[Organizations] as o 
			inner join [dbo].[OrganizationTypes] as ot
				on o.OrganizationTypeId = ot.Id
			inner join [dbo].[Locations] as loc				on o.LocationId = loc.Id
			inner join [dbo].States as s
				on loc.StateId = s.Id
			inner join [dbo].[LocationTypes] as loct
				on loct.Id = loc.LocationTypeId

	  WHERE o.Id = @Id

	END


GO
/****** Object:  StoredProcedure [dbo].[Organizations_Update]    Script Date: 12/2/2022 3:13:23 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Matt Prothero>
-- Create date: <20220705>
-- Description:	<Update an Organization >
-- Code Reviewer: Morgan Frost

-- MODIFIED BY: n/a
-- MODIFIED DATE: n/a 
-- Code Reviewer: 
-- Note: 
-- =============================================

	CREATE PROC [dbo].[Organizations_Update]
			@LocationTypeId int
			,@LineOne nvarchar(255)
			,@LineTwo nvarchar(255)
			,@City nvarchar(225)
			,@Zip nvarchar(50)
			,@StateId int
			,@Latitude float 
			,@Longitude float
		   
		   ,@OrganizationTypeId int 
           ,@Name nvarchar(200) 
           ,@Headline nvarchar(200) 
           ,@Description nvarchar(MAX) 
           ,@Logo nvarchar(255) 
		   ,@LocationId int
           ,@Phone nvarchar(50) 
           ,@SiteUrl nvarchar(255) 
		   ,@UserId int
		   ,@Id int
		    
		   

AS
/*------------------TEST CODE--------------------

	Declare			@Id int = 6

	Declare			@LocationTypeId int = '2'
					,@LineOne nvarchar(255) = 'SQL Test'
					,@LineTwo nvarchar(255) = 'SQL Test'
					,@City nvarchar(225) = 'Yuma'
					,@Zip nvarchar(50) = '85365'
					,@StateId int = '3'
					,@Latitude float = '423455'
					,@Longitude float = '432432'
	
				   ,@OrganizationTypeId int = 2
				   ,@Name nvarchar(200) = 'Final Test'
				   ,@Headline nvarchar(200) = 'Nikes Headline'
				   ,@Description nvarchar(MAX) = 'Nikes description'
				   ,@Logo nvarchar(255) = 'https://upload.wikimedia.org/wikipedia/commons/thumb/a/a6/Logo_NIKE.svg/1200px-Logo_NIKE.svg.png'
				   ,@LocationId int = 3
				   ,@Phone nvarchar(50) = '928-269-8534'
				   ,@SiteUrl nvarchar(255) = 'website here'
				   ,@UserId int = '603'

	Execute	[dbo].[Organizations_Update] 

					@LocationTypeId
					,@LineOne 
					,@LineTwo 
					,@City 
					,@Zip
					,@StateId 
					,@Latitude  
					,@Longitude 

					,@OrganizationTypeId 
					,@Name 
					,@Headline 
					,@Description 
					,@Logo 
					,@LocationId
					,@Phone 
					,@SiteUrl 
					,@UserId 
					,@Id 
					

				Select *
				From [dbo].[Organizations]
				WHERE Id = @Id

*/------------------TEST CODE END----------------

BEGIN 

	UPDATE [dbo].[Organizations]

	   SET  [OrganizationTypeId] = @OrganizationTypeId
			,[Name] = @Name
			,[Headline] = @Headline
			,[Description] = @Description
			,[Logo] = @Logo
			,[LocationId] = @LocationId
			,[Phone] = @Phone
			,[SiteUrl] = @SiteUrl
			,[CreatedBy] = @UserId 
			

		WHERE Id = @Id
			
	UPDATE	[dbo].[Locations]

		SET	[LocationTypeId] = @LocationTypeId
			,[LineOne] = @LineOne
			,[LineTwo] = @LineTwo
			,[City] = @City
			,[Zip] = @Zip
			,[StateId] = @StateId
			,[Latitude] = @Latitude
			,[Longitude] = @Longitude
			,[CreatedBy] = @UserId 
			

	 WHERE Id = @LocationId
	
END


GO
/****** Object:  StoredProcedure [dbo].[OrganizationTypes_Insert]    Script Date: 12/2/2022 3:13:23 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

-- =============================================
-- Author:		<Matt Prothero>
-- Create date: <20220705>
-- Description:	<Insert an OrganizationType>
-- Code Reviewer: Morgan Frost

-- MODIFIED BY: n/a
-- MODIFIED DATE: n/a
-- Code Reviewer: 
-- Note: 
-- =============================================

	CREATE   PROC [dbo].[OrganizationTypes_Insert]
			
           @Name nvarchar(50) 
		   ,@Id int OUTPUT

	AS

	/* -------- TEST CODE --------
		Declare @Id int = 1;
	
		Declare @Name nvarchar(200) = 'Youth Development'

		Execute [dbo].[OrganizationTypes_Insert]
				@Name 
				,@Id

			Select *
			From [dbo].[OrganizationTypes]
			WHERE Id = @Id

	*/ -------------------------

		BEGIN
 
		INSERT INTO [dbo].[OrganizationTypes]
				   ([Name])
    
		VALUES
			   (@Name)
			   SET @Id = SCOPE_IDENTITY();

	END


GO
/****** Object:  StoredProcedure [dbo].[OrganizationTypes_SelectAll]    Script Date: 12/2/2022 3:13:23 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

-- =============================================
-- Author:		<Matt Prothero>
-- Create date: <20220705>
-- Description:	<Select All OrganizationTypes>
-- Code Reviewer:

-- MODIFIED BY: n/a
-- MODIFIED DATE: n/a
-- Code Reviewer: 
-- Note: 
-- =============================================

	CREATE PROC [dbo].[OrganizationTypes_SelectAll] 

	AS

	/*------Test Code------
		
	Execute [dbo].[OrganizationTypes_SelectAll]


	*/---------------------

	BEGIN


	SELECT [Id]
		  ,[Name]

	  FROM [dbo].[OrganizationTypes]

	END


GO
/****** Object:  StoredProcedure [dbo].[OrganizationTypes_SelectAll_Paginated]    Script Date: 12/2/2022 3:13:23 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

-- =============================================
-- Author:		<Matt Prothero>
-- Create date: <20220705>
-- Description:	<Select All OrganizationTypes Paginated>
-- Code Reviewer:

-- MODIFIED BY: n/a
-- MODIFIED DATE: n/a
-- Code Reviewer: 
-- Note: 
-- =============================================

	CREATE PROC [dbo].[OrganizationTypes_SelectAll_Paginated] 
		@PageIndex int 
		,@PageSize int

	AS

	/*------Test Code------

	DECLARE  @PageIndex int = 0
			 ,@PageSize int = 2
		
	Execute [dbo].[OrganizationTypes_SelectAll_Paginated]
			@PageIndex 
			,@PageSize 

	*/---------------------

	BEGIN
		Declare @offset int = @PageIndex * @PageSize

	SELECT [Id]
		  ,[Name]

	  FROM [dbo].[OrganizationTypes]

	  ORDER BY Id

	  OFFSET @offSet Rows
	Fetch Next @PageSize Rows ONLY

	END


GO
/****** Object:  StoredProcedure [dbo].[ProfessionTypes_SelectAll]    Script Date: 12/2/2022 3:13:23 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author: <Hyun Kim>
-- Create date: <7/6/22>
-- Description: <ProfessionTypes_SelectAll>
-- Code Reviewer:

-- MODIFIED BY: Brijesh
-- MODIFIED DATE:12/1/2020
-- Code Reviewer:
-- Note:
-- =============================================

CREATE proc [dbo].[ProfessionTypes_SelectAll]
				

/* ---- TEST CODE ----
	

Execute [dbo].[ProfessionTypes_SelectAll]
			


*/ ---- TEST CODE ----

AS

BEGIN


SELECT pt.[Id]
      ,pt.[Name]

  FROM [dbo].[ProfessionTypes] as pt

  

END


GO
/****** Object:  StoredProcedure [dbo].[Proposals_Delete]    Script Date: 12/2/2022 3:13:23 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROC [dbo].[Proposals_Delete]
						@Id int

/*-----------Test Code-------------

DECLARE @Id int = 12;

SELECT *
FROM [dbo].[Proposals]
WHERE Id = @Id

EXEC [dbo].[Proposals_Delete]
					@Id

SELECT *
FROM [dbo].[Proposals]
WHERE Id = @Id

*/

AS

BEGIN
	DELETE FROM [dbo].[Proposals]
		WHERE Id = @Id
END


GO
/****** Object:  StoredProcedure [dbo].[Proposals_Insert]    Script Date: 12/2/2022 3:13:23 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROC [dbo].[Proposals_Insert]
			 @JobId int
			,@Bid nvarchar(50)
			,@Duration nvarchar(50)
			,@Details nvarchar(4000)
			,@CreatedBy int
			,@ModifiedBy int
			,@Id int OUTPUT

/*---------------Test Code--------------
DECLARE		 @JobId int = 167
			,@Bid nvarchar(50) = 'test bid'
			,@Duration nvarchar(50) = 'test duration'
			,@Details nvarchar(4000) = 'test details'
			,@CreatedBy int = 235
			,@ModifiedBy int = 235
			,@Id int = 0
			

	EXEC [dbo].[Proposals_Insert]
			 @JobId
			,@Bid
			,@Duration 
			,@Details
			,@CreatedBy
			,@ModifiedBy
			,@Id OUTPUT

	Select *
	From [dbo].[Proposals]
	Where Id = @Id
*/

AS

BEGIN
Declare @ProposalStatusId int = 1

INSERT INTO [dbo].[Proposals]
           ([JobId]
           ,[Bid]
           ,[Duration]
           ,[Details]
           ,[ProposalStatusId]
		   ,[CreatedBy]
           ,[ModifiedBy]
           ,[DateCreated]
           ,[DateModified])
     VALUES
            (@JobId
			,@Bid
			,@Duration 
			,@Details
			,@ProposalStatusId
			,@CreatedBy
			,@ModifiedBy
			,GETUTCDATE()
			,GETUTCDATE())

		SET @Id = SCOPE_IDENTITY()
					

END
GO
/****** Object:  StoredProcedure [dbo].[Proposals_Search]    Script Date: 12/2/2022 3:13:23 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROC [dbo].[Proposals_Search]
					 @userId int
					,@OrganizationId int
					,@DateRange int
					,@PageIndex int
					,@PageSize int

AS

/* -/-/-/- TESTING CODE BEGIN -/-/-/-

Declare @userId int = 110,
		@OrganizationId int = 2,
		@DateRange int = 80,
		@PageIndex int = 0,
		@PageSize int = 10

EXECUTE [dbo].[Proposals_Search]
			 @userId
			,@OrganizationId
			,@DateRange
			,@PageIndex
			,@PageSize


SELECT *
FROM [dbo].[Proposals]

Select *
From [dbo].[Jobs]

SELECT *
FROM [dbo].[Organizations]

select *
From [dbo].[Users]
-/-/-/-/-/- TESTING CODE END -/-/-/-/-/-

SELECT *
FROM [dbo].[Organizations]



*/

BEGIN

DECLARE @EndDate datetime = DATEADD(DAY, -@DateRange, GETUTCDATE())
			,@StartDate datetime = GETUTCDATE()
			,@offset int = @PageIndex * @PageSize

SELECT 
		p.[Id],
		p.[Bid],
		p.[Details],
		p.[Duration],
		p.DateCreated,
		j.[Id] AS [JobId],
		o.[Id] as [OrganizationId],
		o.[Name],
		u.[Id] AS UserId

	
				From [dbo].[Proposals] AS p 
				INNER JOIN dbo.Jobs as j On j.Id = p.JobId 
				INNER JOIN dbo.Organizations as o ON j.OrganizationId = o.Id 
				INNER JOIN dbo.Users as u ON u.Id = p.CreatedBy 

				WHERE @OrganizationId = j.OrganizationId AND p.CreatedBy = @userId AND @EndDate <= p.[DateCreated] AND p.[DateCreated] <= @StartDate
			
					ORDER BY p.[DateCreated] Desc
					OFFSET @offSet Rows
					Fetch Next @PageSize Rows ONLY
END
GO
/****** Object:  StoredProcedure [dbo].[Proposals_SearchByOrg]    Script Date: 12/2/2022 3:13:23 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE proc [dbo].[Proposals_SearchByOrg]
			 @userId int
			,@OrganizationId int
			,@PageIndex int
			,@PageSize int

AS

/*-----TEST CODE-----

	DECLARE  @userId int = 110
			,@OrgId int = 2
			,@PageIndex int = 0
			,@PageSize int = 5

	EXECUTE [dbo].[Proposals_SearchByOrg]
			 @userId
			,@OrgId
			,@PageIndex
			,@PageSize

-----END TEST CODE-----
*/
BEGIN

	DECLARE @offset int = @PageIndex * @PageSize

SELECT
		p.[Id],
		p.[Bid],
		p.[Details],
		p.[Duration],
		p.DateCreated,
		j.[Id] AS [JobId],
		o.[Id] as [OrganizationId],
		o.[Name],
		u.[Id] AS UserId,
		TotalCount = COUNT(1) OVER()

	
				From [dbo].[Proposals] AS p 
				INNER JOIN dbo.Jobs as j On j.Id = p.JobId 
				INNER JOIN dbo.Organizations as o ON j.OrganizationId = o.Id 
				INNER JOIN dbo.Users as u ON u.Id = p.CreatedBy 

				WHERE @OrganizationId = j.OrganizationId AND p.CreatedBy = @userId
					ORDER BY p.[DateCreated] Desc
					OFFSET @offSet Rows
					Fetch Next @PageSize Rows ONLY
END
GO
/****** Object:  StoredProcedure [dbo].[Proposals_Select_ByCreatedBy]    Script Date: 12/2/2022 3:13:23 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE proc [dbo].[Proposals_Select_ByCreatedBy]
			@CreatedBy int
			,@PageSize int 
			,@PageIndex int

AS

/* ------------- TEST CODE ------------------------------

Declare			@CreatedBy int = 4
				,@PageIndex int = 0
				,@PageSize int = 3
				
	

	Execute [dbo].[Proposals_Select_ByCreatedBy] 
				@CreatedBy
				,@PageSize
				,@PageIndex

select *
from dbo.Proposals


*/--------------- END TEST CODE --------------------------

BEGIN

	Declare @offset int = @PageIndex * @PageSize

	SELECT p.[Id]
			  ,j.[Id] AS [JobId]
			  ,jt.[Name]
			  ,lt.[Name] AS [NameOfLocation]
			  ,l.[LineOne]
			  ,l.[LineTwo]
			  ,l.[City]
			  ,l.[Zip]
			  ,s.[Name]
			  ,s.[Code]
			  ,l.[Latitude]
			  ,l.[Longitude]
			  ,ot.[Name]
			  ,o.[Name]
			  ,o.[Headline]
			  ,o.[Description]
			  ,o.[Logo]
			  ,o.[Phone]
			  ,o.[SiteUrl]
			  ,j.[Title]
			  ,j.[Description]
			  ,j.[Requirements]
			  ,j.[IsActive]
			  ,j.[ContactName]
			  ,j.[ContactPhone]
			  ,j.[ContactEmail]
			  ,p.[Bid]
			  ,p.[Duration]
			  ,p.[Details]
			  ,ps.[Name] as [ProposalStatus]
			  ,u.[Id] as [CreatedBy]
			  ,u.[Id] as [ModifiedBy]
			  , TotalCount = COUNT (1) OVER()
			  

		From [dbo].[Proposals] AS p 
		INNER JOIN dbo.Jobs as j On j.Id = p.JobId 
		INNER JOIN dbo.Users as u ON u.Id = p.CreatedBy 
		INNER JOIN dbo.ProposalStatus as ps ON ps.Id = p.ProposalStatusId 
		INNER JOIN dbo.Locations as l ON l.Id = j.LocationId 
		INNER JOIN dbo.Organizations as o ON j.OrganizationId = o.OrganizationTypeId 
		INNER JOIN dbo.States as s ON l.StateId = s.Id 
		INNER JOIN dbo.JobTypes as jt ON j.JobTypeId = jt.Id 
		INNER JOIN dbo.OrganizationTypes as ot ON ot.Id = j.OrganizationId 
		INNER JOIN dbo.LocationTypes as lt ON lt.Id = l.LocationTypeId

		WHERE p.CreatedBy = @CreatedBy
		ORDER BY p.Id
		OFFSET @offset ROWS FETCH NEXT @PageSize ROWS ONLY


END
GO
/****** Object:  StoredProcedure [dbo].[Proposals_Select_ById]    Script Date: 12/2/2022 3:13:23 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE proc [dbo].[Proposals_Select_ById]

				 @Id int

	AS

	/*


		DECLARE @Id int = 32

		Execute dbo.Proposals_Select_ById
				@Id
											

	*/

	BEGIN
		
		SELECT p.[Id]
			  ,j.[Id] AS [JobId]
			  ,jt.[Name]
			  ,lt.[Name] AS [NameOfLocation]
			  ,l.[LineOne]
			  ,l.[LineTwo]
			  ,l.[City]
			  ,l.[Zip]
			  ,s.[Name]
			  ,s.[Code]
			  ,l.[Latitude]
			  ,l.[Longitude]
			  ,ot.[Name]
			  ,o.[Name]
			  ,o.[Headline]
			  ,o.[Description]
			  ,o.[Logo]
			  ,o.[Phone]
			  ,o.[SiteUrl]
			  ,j.[Title]
			  ,j.[Description]
			  ,j.[Requirements]
			  ,j.[IsActive]
			  ,j.[ContactName]
			  ,j.[ContactPhone]
			  ,j.[ContactEmail]
			  ,p.[Bid]
			  ,p.[Duration]
			  ,p.[Details]
			  ,ps.[Name] as [ProposalStatus]
			  ,p.[CreatedBy]
			  ,p.[ModifiedBy]
			  

		From [dbo].[Proposals] AS p 
		INNER JOIN dbo.Jobs as j On j.Id = p.JobId  
		INNER JOIN dbo.ProposalStatus as ps ON ps.Id = p.ProposalStatusId 
		INNER JOIN dbo.Locations as l ON l.Id = j.LocationId 
		INNER JOIN dbo.Organizations as o ON j.OrganizationId = o.OrganizationTypeId 
		INNER JOIN dbo.States as s ON l.StateId = s.Id 
		INNER JOIN dbo.JobTypes as jt ON j.JobTypeId = jt.Id 
		INNER JOIN dbo.OrganizationTypes as ot ON ot.Id = j.OrganizationId 
		INNER JOIN dbo.LocationTypes as lt ON lt.Id = l.LocationTypeId

		where p.Id = @Id

	END
GO
/****** Object:  StoredProcedure [dbo].[Proposals_SelectAll]    Script Date: 12/2/2022 3:13:23 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE proc [dbo].[Proposals_SelectAll]  
										@PageIndex int 
                                       ,@PageSize int

AS


/* --------------- TEST CODE ---------------

Declare @PageIndex int = 0
			,@PageSize int = 3

Execute [dbo].[Proposals_SelectAll]
				@PageIndex 
				,@PageSize 

		Select *
				From [dbo].[Proposals] AS p 
		INNER JOIN dbo.Jobs as j On j.Id = p.JobId 
		INNER JOIN dbo.Users as u ON u.Id = p.CreatedBy 
		INNER JOIN dbo.ProposalStatus as ps ON ps.Id = p.ProposalStatusId 
		INNER JOIN dbo.Locations as l ON l.Id = j.LocationId 
		INNER JOIN dbo.Organizations as o ON j.OrganizationId = o.OrganizationTypeId 
		INNER JOIN dbo.States as s ON l.StateId = s.Id 
		INNER JOIN dbo.JobTypes as jt ON j.JobTypeId = jt.Id 
		INNER JOIN dbo.OrganizationTypes as ot ON ot.Id = j.OrganizationId 
		INNER JOIN dbo.LocationTypes as lt ON lt.Id = l.LocationTypeId

 --------------END TEST CODE -------------- */

BEGIN

	   DECLARE @offset int = @PageIndex * @PageSize

       SELECT	p.[Id]
			  ,j.[Id] AS [JobId]
			  ,jt.[Name]
			  ,lt.[Name] AS [NameOfLocation]
			  ,l.[LineOne]
			  ,l.[LineTwo]
			  ,l.[City]
			  ,l.[Zip]
			  ,s.[Name]
			  ,s.[Code]
			  ,l.[Latitude]
			  ,l.[Longitude]
			  ,ot.[Name]
			  ,o.[Name]
			  ,o.[Headline]
			  ,o.[Description]
			  ,o.[Logo]
			  ,o.[Phone]
			  ,o.[SiteUrl]
			  ,j.[Title]
			  ,j.[Description]
			  ,j.[Requirements]
			  ,j.[IsActive]
			  ,j.[ContactName]
			  ,j.[ContactPhone]
			  ,j.[ContactEmail]
			  ,p.[Bid]
			  ,p.[Duration]
			  ,p.[Details]
			  ,ps.[Name] as [ProposalStatus]
			  ,u.[Id] as [CreatedBy]
			  ,u.[Id] as [ModifiedBy]
		
			  , TotalCount = COUNT (1) OVER()
		

		From [dbo].[Proposals] AS p 
		INNER JOIN dbo.Jobs as j On j.Id = p.JobId 
		INNER JOIN dbo.Users as u ON u.Id = p.CreatedBy 
		INNER JOIN dbo.ProposalStatus as ps ON ps.Id = p.ProposalStatusId 
		INNER JOIN dbo.Locations as l ON l.Id = j.LocationId 
		INNER JOIN dbo.Organizations as o ON j.OrganizationId = o.Id 
		INNER JOIN dbo.States as s ON l.StateId = s.Id 
		INNER JOIN dbo.JobTypes as jt ON j.JobTypeId = jt.Id 
		INNER JOIN dbo.OrganizationTypes as ot ON ot.Id = j.OrganizationId 
		INNER JOIN dbo.LocationTypes as lt ON lt.Id = l.LocationTypeId
	
		ORDER BY	Id
		OFFSET @offset ROWS
		FETCH NEXT  @PageSize ROWS ONLY

END
GO
/****** Object:  StoredProcedure [dbo].[Proposals_Update]    Script Date: 12/2/2022 3:13:23 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROC [dbo].[Proposals_Update]
					 @JobId int
					,@Bid nvarchar(50)
					,@Duration nvarchar(50)
					,@Details nvarchar(4000)
					,@ProposalStatusId int
					,@ModifiedBy int
					,@Id int


/*---------------Test Code--------------
DECLARE				 @JobId int = 20
					,@Bid nvarchar(50) = 'new test bid'
					,@Duration nvarchar(50) = 'new test duration'
					,@Details nvarchar(4000) = 'new test details'
					,@ProposalStatusId int = 1
					,@ModifiedBy int = 110
					,@Id int = 12

SELECT *
FROM [dbo].[Proposals]
WHERE Id = @Id

	EXEC [dbo].[Proposals_Update]
					 @JobId
					,@Bid
					,@Duration 
					,@Details
					,@ProposalStatusId
					,@ModifiedBy
					,@Id

	Select *
	From [dbo].[Proposals]
	Where Id = @Id

*/

AS

BEGIN

UPDATE [dbo].[Proposals]
   SET [JobId] = @JobId
      ,[Bid] = @Bid
      ,[Duration] = @Duration
      ,[Details] = @Details
      ,[ProposalStatusId] = @ProposalStatusId
      ,[ModifiedBy] = @ModifiedBy
	  ,[DateModified] = GETUTCDATE()
 WHERE Id = @Id
					
END
GO
/****** Object:  StoredProcedure [dbo].[ProposalStatus_SelectAll]    Script Date: 12/2/2022 3:13:23 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE proc [dbo].[ProposalStatus_SelectAll]

AS

/* ----- TEST CODE -----
	
	EXECUTE dbo.ProposalStatus_SelectAll

*/ ----- END TEST CODE -----

BEGIN

	SELECT [Id]
		  ,[Name]
	  FROM [dbo].[ProposalStatus]

 END
GO
/****** Object:  StoredProcedure [dbo].[QuestionTypes_SelectAll]    Script Date: 12/2/2022 3:13:23 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROC [dbo].[QuestionTypes_SelectAll]

AS

-- =============================================
-- Author: < Steven Tran >
-- Create date: < 8/25/22 >
-- Description: < Select All from dbo.QuestionTypes >
-- Code Reviewer:

-- MODIFIED BY: author
-- MODIFIED DATE:12/1/2020
-- Code Reviewer:
-- Note:
-- ==============================================

/* TEST CODE

EXECUTE dbo.QuestionTypes_SelectAll 

*/

BEGIN

	SELECT 
		 [Id]
		,[Name]

	FROM QuestionTypes

END
GO
/****** Object:  StoredProcedure [dbo].[Rating_SelectSummary_ByEntityId]    Script Date: 12/2/2022 3:13:23 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
----------------------------------------------------------------------
-- Author: Bismark Gbede
-- Create date: <8/4/22>
-- Description: [Ratings_Select_ByEntityId]	
-- Code Reviewer: Jorge Obando

-- MODIFIED BY: 
-- MODIFIED DATE:
-- Code Reviewer:
-- Note:
----------------------------------------------------------------------

CREATE   proc [dbo].[Rating_SelectSummary_ByEntityId]
				@EntityId int
				,@EntityTypeId int

as 
/*

Declare  @EntityId int =3
		 ,@EntityTypeId int = 3
		

EXEC [dbo].[Rating_SelectSummary_ByEntityId] 
			@EntityId 
		 ,@EntityTypeId 
		

*/

BEGIN

SELECT 		
			avg(Cast(Rating  AS DECIMAL(2, 0))) as Avg_Rating
			--float(avg(Rating as Decimal(10, 1))) as Avg_Rating			
			--CAST(AVG(Rating) AS DECIMAL(10, 2)) as Avg_Rating

		
			FROM [dbo].[Ratings] as R
			inner join dbo.EntityTypes as ET
			ON ET.Id =R.EntityTypeId
			WHERE  R.EntityId = @EntityId and R.EntityTypeId = ET.Id
			Group by  R.EntityId, ET.Id, ET.Name
			Order by R.EntityId  desc

END

GO
/****** Object:  StoredProcedure [dbo].[Ratings_CreatedBy]    Script Date: 12/2/2022 3:13:23 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
----------------------------------------------------------------------
-- Author: Bismark Gbede
-- Create date: <9/7/22>
-- Description: [Ratings_CreatedBy]			
-- Code Reviewer:

-- MODIFIED BY: 
-- MODIFIED DATE:
-- Code Reviewer:
-- Note:
----------------------------------------------------------------------
 Create     proc  [dbo].[Ratings_CreatedBy]			
			 @EntityId int
			 ,@EntityTypeId int
			,@CreatedBy int 
			
as

/*

Declare
			 @EntityId int = 3
			 ,@EntityTypeId int =3
			,@CreatedBy int = 21

Execute [dbo].[Ratings_CreatedBy]	
			@EntityId 
			 ,@EntityTypeId 
			,@CreatedBy  
			
*/


BEGIN


SELECT R.[Id]
		  ,[Rating]
		  ,ET.Id as EntityTypeId
		  ,R.EntityId
		

	  FROM [dbo].[Ratings] AS R
	  inner join [dbo].[Comments] AS C ON C.Id = R.CommentId
	  inner join [dbo].[Users] as U ON U.Id = R.CreatedBy 
	  inner join [dbo].[EntityTypes] as ET ON ET.Id = R.EntityTypeId
	 

  Where R.CreatedBy= @CreatedBy

  ORDER BY R.Id

	
END
GO
/****** Object:  StoredProcedure [dbo].[Ratings_Delete_ById]    Script Date: 12/2/2022 3:13:23 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author: <BISMARK Gbede>
-- Create date: <8/2/22>
-- Description: [dbo].[Ratings_Delete_ById]
-- Code Reviewer: Morgan Frost

-- MODIFIED BY: 
-- MODIFIED DATE: 
-- Code Reviewer:
-- Note: 
-- =============================================
CREATE       proc [dbo].[Ratings_Delete_ById]
				 @Id int 
				,@UserId int
			
				

as
/*
 Declare @Id int = 81
 Declare    @UserId int = 21
				

Execute dbo.Ratings_Delete_ById
				@Id
				,@UserId
			
Select @Id
Select *
from dbo.Ratings
where Id = @Id

*/

BEGIN

	UPDATE [dbo].[Ratings]
	   SET
		  [IsDeleted] = 1
	   WHERE Id =@Id AND [CreatedBy] = @UserId
END



GO
/****** Object:  StoredProcedure [dbo].[Ratings_Insert]    Script Date: 12/2/2022 3:13:23 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author: <BISMARK Gbede>
-- Create date: <7/29/22>
-- Description: [dbo].[Ratings_Insert]
-- Code Reviewer:

-- MODIFIED BY: 
-- MODIFIED DATE: 
-- Code Reviewer:
-- Note: 
-- =============================================

CREATE     proc [dbo].[Ratings_Insert]
				@Rating tinyint
				,@CommentId int
				,@EntityTypeId int
				,@EntityId int
				,@CreatedBy int
				,@IsDeleted bit
				,@Id int Output


as
/*
 Declare @Id int = 0
 Declare        @Rating tinyint =1
				,@CommentId int = 143
				,@EntityTypeId int = 3
				,@EntityId int = 3
				,@CreatedBy int = 21
				,@IsDeleted bit = 'false'

Execute dbo.Ratings_Insert
				@Rating 
				,@CommentId 
				,@EntityTypeId 
				,@EntityId 
				,@CreatedBy 
				,@IsDeleted
				,@Id Output


*/

BEGIN

INSERT INTO [dbo].[Ratings]
           ([Rating]
           ,[CommentId]
           ,[EntityTypeId]
           ,[EntityId]
           ,[CreatedBy]
           ,[IsDeleted])
     VALUES
				(@Rating 
				,@CommentId 
				,@EntityTypeId 
				,@EntityId 
				,@CreatedBy 
				,@IsDeleted)
				
 SET @Id =SCOPE_IDENTITY()


 END

 select * from dbo.Ratings
GO
/****** Object:  StoredProcedure [dbo].[Ratings_Select_ByCommentId]    Script Date: 12/2/2022 3:13:23 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

----------------------------------------------------------------------
-- Author: Bismark Gbede
-- Create date: <9/7/22>
-- Description: [Ratings_Select_ByCommentId]	
-- Code Reviewer:

-- MODIFIED BY: 
-- MODIFIED DATE:
-- Code Reviewer:
-- Note:
----------------------------------------------------------------------
 Create     proc  [dbo].[Ratings_Select_ByCommentId]			
			 @CommentId int 
			,@CreatedBy int 
			
as

/*

Declare
      	 @CommentId int = 375
			,@CreatedBy int = 135

		Execute [dbo].[Ratings_Select_ByCommentId]	
        @CommentId 
			,@CreatedBy  
			
*/


BEGIN


SELECT r.[Id]		
      , r.[Rating]
	  ,r.CommentId
	  
	  

  FROM [dbo].[Ratings] as r inner join dbo.Comments as c
				  on c.Id = r.CommentId
				 
	
  Where r.CreatedBy= @CreatedBy	AND r.CommentId = @CommentId
 
  order by r.id
	
END
GO
/****** Object:  StoredProcedure [dbo].[Ratings_Select_ByCreatedBy]    Script Date: 12/2/2022 3:13:23 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

-- Author: Bismark Gbede
-- Create date: <7/29/22>
-- Description: <Ratings_Select_ByCreatedBy>
-- Code Reviewer: Morgan Frost

-- MODIFIED BY: 
-- MODIFIED DATE:
-- Code Reviewer:
-- Note:

CREATE     proc [dbo].[Ratings_Select_ByCreatedBy]			
			 @PageIndex int 
			,@PageSize int 
			,@CreatedBy int
as

/*

Declare
        @PageIndex int = 0
		,@PageSize int = 4
		,@CreatedBy int= 20

		Execute  dbo.Ratings_Select_ByCreatedBy
            @PageIndex
			,@PageSize
			,@CreatedBy

	
*/


BEGIN

 Declare @offset int = @PageIndex * @PageSize

	SELECT R.[Id]
		  ,[Rating]
		  ,C.Id AS CommentId
		  ,C.Text AS CommentText
		  ,ET.Id as EntityTypeId
		  ,ET.Name AS EntityType
		  ,R.EntityId
		  ,R.[DateCreated]
		  ,R.[DateModified]
		  ,U.Id as CreatedById
		  ,R.[IsDeleted]
			,TotalCount = COUNT(1) OVER()

	  FROM [dbo].[Ratings] AS R
	  inner join [dbo].[Comments] AS C ON C.Id = R.CommentId
	  inner join [dbo].[Users] as U ON U.Id = R.CreatedBy 
	  inner join [dbo].[EntityTypes] as ET ON ET.Id = R.EntityTypeId

	 

  Where R.CreatedBy= @CreatedBy

  ORDER BY R.Id

  	OFFSET @offset  Rows
	Fetch Next @PageSize Rows ONLY

END


GO
/****** Object:  StoredProcedure [dbo].[Ratings_Select_ByEntityId]    Script Date: 12/2/2022 3:13:23 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
----------------------------------------------------------------------
-- Author: Bismark Gbede
-- Create date: <8/2/22>
-- Description: [Ratings_Select_ByEntityId]	
-- Code Reviewer:

-- MODIFIED BY: 
-- MODIFIED DATE:
-- Code Reviewer:
-- Note:
----------------------------------------------------------------------
CREATE       proc  [dbo].[Ratings_Select_ByEntityId]			
			 @PageIndex int 
			,@PageSize int 
			,@EntityId int
			,@EntityTypeId int
as

/*

Declare
        @PageIndex int = 0
		,@PageSize int = 4
		,@EntityId int =3
	    ,@EntityTypeId int = 3

		Execute [dbo].[Ratings_Select_ByEntityId]
            @PageIndex
			,@PageSize
			,@EntityId 
			,@EntityTypeId


*/


BEGIN

 Declare @offset int = @PageIndex * @PageSize


SELECT r.[Id]		
      , r.[Rating]
	  ,c.Id as CommentId
	  ,c.Text as CommentText
	  ,et.Id as EntityTypeId	 
	  ,et.Name as EntityTypeName
	   ,r.EntityId
      , r.[DateCreated]
      , r.[DateModified]
	  ,u.Id as CreatedById
      ,r.[IsDeleted]
	  ,TotalCount = COUNT(1) OVER()
	  

  FROM [dbo].[Ratings] as r inner join dbo.Comments as c
				  on c.Id = r.CommentId
				  inner join dbo.EntityTypes as et
				  on et.Id = r.EntityTypeId
				  inner join dbo.Users as u 
				  on u.Id = r.CreatedBy
				  
 
 Where r.EntityTypeId= @EntityTypeId

 ORDER by r.DateCreated  desc


  	OFFSET @offset  Rows
	Fetch Next @PageSize Rows ONLY

	

END
GO
/****** Object:  StoredProcedure [dbo].[Ratings_Select_ById]    Script Date: 12/2/2022 3:13:23 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author: <BISMARK Gbede>
-- Create date: <7/29/22>
-- Description: [dbo].[Ratings_Select_ById]
-- Code Reviewer:

-- MODIFIED BY: 
-- MODIFIED DATE: 
-- Code Reviewer:
-- Note: 
-- =============================================
CREATE     proc [dbo].[Ratings_Select_ById]
			@Id int

as
/*

Declare @Id int = 11

execute [dbo].[Ratings_Select_ById]  @Id
*/
Begin

SELECT R.[Id]
		  ,[Rating]
		  ,C.Id AS CommentId
		  ,C.Text AS CommentText
		  ,ET.Id
		  ,ET.Name
		  ,R.EntityId
		  ,R.[DateCreated]
		  ,R.[DateModified]
		  ,U.Id as CreatedById
		  ,R.[IsDeleted]
		

	  FROM [dbo].[Ratings] AS R
		 inner join [dbo].[Comments] AS C ON C.Id = R.CommentId
	  inner join [dbo].[EntityTypes] as ET on ET.Id = R.EntityTypeId
	  inner join [dbo].[Users] as U ON U.Id = R.CreatedBy 
	
 Where R.Id =@Id

 End
GO
/****** Object:  StoredProcedure [dbo].[Ratings_SeletAll]    Script Date: 12/2/2022 3:13:23 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

-- =============================================
-- Author: <BISMARK Gbede>
-- Create date: <7/29/22>
-- Description: [dbo].[Ratings_SeletAll]	
-- Code Reviewer:

-- MODIFIED BY: 
-- MODIFIED DATE: 
-- Code Reviewer:
-- Note: 
-- =============================================

CREATE     proc [dbo].[Ratings_SeletAll]			
			 @PageIndex int 
			,@PageSize int 

as

/*

Declare
        @PageIndex int = 0
		,@PageSize int = 4

		Execute dbo.Ratings_SeletAll	
            @PageIndex
			,@PageSize

Select * from dbo.Ratings

*/


BEGIN

 Declare @offset int = @PageIndex * @PageSize


	SELECT R.[Id]
		  ,[Rating]
		  ,C.Id AS CommentId
		  ,C.Text AS CommentText
		  ,ET.Id as EntityTpyeId
		  ,ET.Name as EntityName
		  ,R.EntityId
		  ,R.[DateCreated]
		  ,R.[DateModified]
		  ,U.Id as CreatedById
		  ,R.[IsDeleted]
			,TotalCount = COUNT(1) OVER()

	  FROM [dbo].[Ratings] AS R
	  INNER JOIN [dbo].[Comments] AS C ON C.Id = R.CommentId
	  inner join [dbo].[EntityTypes] ET on ET.Id = R.EntityTypeId
	  inner join [dbo].[Users] as U ON U.Id = R.CreatedBy 

	  ORDER BY R.Id

  	OFFSET @offset  Rows
	Fetch Next @PageSize Rows ONLY

END


GO
/****** Object:  StoredProcedure [dbo].[Ratings_Update]    Script Date: 12/2/2022 3:13:23 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
--With this make sure that the rating can be updated if the ID of the user sending the request is equal to the user who created it.
-- =============================================
-- Author: <BISMARK Gbede>
-- Create date: <7/29/22>
-- Description: [dbo].[Ratings_Update]
-- Code Reviewer:

-- MODIFIED BY: 
-- MODIFIED DATE: 
-- Code Reviewer:
-- Note: 
-- =============================================
CREATE     proc [dbo].[Ratings_Update]
				@Id int 
				,@Rating tinyint
				,@CommentId int
				,@IsDeleted bit
				,@UserId int
				

as
/*
 
 Declare @Id int = 10
 Declare        @Rating tinyint =4
				,@CommentId int = 17
				,@IsDeleted bit = 0
				,@UserId int = 4

Execute dbo.Ratings_Update
				@Id
				,@Rating 
				,@CommentId 
				,@IsDeleted
				,@UserId

Select *
from dbo.Ratings
where Id =@Id

*/

BEGIN

UPDATE [dbo].[Ratings]
   SET [Rating] = @Rating
      ,[CommentId] = @CommentId
      ,[IsDeleted] = @IsDeleted
 WHERE Id = @Id AND [CreatedBy] = @UserId 
END

GO
/****** Object:  StoredProcedure [dbo].[ReferenceTypes_SelectAll]    Script Date: 12/2/2022 3:13:23 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO






CREATE proc [dbo].[ReferenceTypes_SelectAll]
						

as


/* .....TEST CODE.....


execute dbo.ReferenceTypes_SelectAll 
	
	
*/

BEGIN

		SELECT Id
				,[Name]
		FROM dbo.ReferenceTypes  

END




GO
/****** Object:  StoredProcedure [dbo].[Register_ThirdParty]    Script Date: 12/2/2022 3:13:23 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author: Gonzalez, Victor
-- Create date: 9/3/22
-- Description: Registers user to capture Facebook api key
-- Code Reviewer:

-- MODIFIED BY: Gonzalez, Victor
-- MODIFIED DATE:9/6/2022
-- Code Reviewer:
-- Note:
-- ==============================================

CREATE proc [dbo].[Register_ThirdParty]
			@Email nvarchar(255)
			,@Token varchar(200)
			,@TokenType int
			,@RoleId int
			,@Id int OUTPUT
as
/*
			DECLARE @Id int = 0
			DECLARE @Email nvarchar(255) = 'FbUser@test.net'
					,@Token varchar(200) = 'abc-123412'
					,@TokenType int = 3
					,@RoleId int = 1
			EXECUTE dbo.Register_ThirdParty
					@Email
					,@Token
					,@TokenType
					,@RoleId
					,@Id OUTPUT
			SELECT *
				From dbo.Users where [Id] = @Id
			SELECT *
				From dbo.UserTokens where [Token] = @Token
			SELECT *
			from dbo.Roles
*/
BEGIN
			INSERT INTO [dbo].[Users]
				([Email]
				,[Password]
				,[IsConfirmed]
				,[UserStatusId])
			VALUES
				(@Email
				,'Pass12345!'
				,'False'
				,'2')
			SET @Id = SCOPE_IDENTITY()
			INSERT INTO [dbo].[UserTokens]
				([Token]
				,[UserId]
				,[TokenType])
			VALUES
				(@Token
				,@Id
				,@TokenType)
END

IF @RoleId != 1
			BEGIN
			INSERT INTO [dbo].[UserRoles]
				([UserId]
				,[RoleId])
			VALUES
				(@Id
				,1)
END
			INSERT INTO [dbo].[UserRoles]
				([UserId]
				,[RoleId])
			VALUES
				(@Id
				,@RoleId)
GO
/****** Object:  StoredProcedure [dbo].[ResourceCategories_Insert]    Script Date: 12/2/2022 3:13:23 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- Author:		Bradley Loewen
-- Create date: 08/29/2022
-- Description:	Resource Categories Insert
-- Code Reviewer: Heriberto Martinez


-- MODIFIED BY: 
-- MODIFIED DATE:
-- Code Reviewer: 
-- Note: 

CREATE proc [dbo].[ResourceCategories_Insert]
			   @Category nvarchar(120)
			   ,@Id int OUTPUT

/*
	Declare @Id int = 0

	Declare 
		    @Name nvarchar(120) = 'Inserted Category'

	Execute dbo.ResourceCategories_Insert 
			   @Name 
			   ,@Id OUTPUT

			Select @Id

			Select *
			From dbo.ResourceCategories
			Where Id = @Id
*/

as

BEGIN

	INSERT INTO [dbo].[ResourceCategories]
           ([Category])
     VALUES
           (@Category)

	SET @Id = SCOPE_IDENTITY()

END
GO
/****** Object:  StoredProcedure [dbo].[ResourceCategories_SelectAll]    Script Date: 12/2/2022 3:13:23 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

-- Author:		Bradley Loewen
-- Create date: 08/24/2022
-- Description:	Resources Select By Id
-- Code Reviewer: Aron Canapa


-- MODIFIED BY: 
-- MODIFIED DATE:
-- Code Reviewer: 
-- Note: 

Create proc [dbo].[ResourceCategories_SelectAll]    
		


AS

/*

	Execute dbo.ResourceCategories_SelectAll @Id

*/

BEGIN

     SELECT 
		   [Id]
		   ,[Category]
	FROM [dbo].[ResourceCategories]

END
GO
/****** Object:  StoredProcedure [dbo].[ResourceCategories_Update]    Script Date: 12/2/2022 3:13:23 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- Author:		Bradley Loewen
-- Create date: 08/29/2022
-- Description:	Resource Categories Update
-- Code Reviewer: Heriberto Martinez


-- MODIFIED BY: 
-- MODIFIED DATE:
-- Code Reviewer: 
-- Note: 

CREATE proc [dbo].[ResourceCategories_Update]
			   @Category nvarchar(120)
			   ,@Id int 

/*
	Declare @Id int = 12

	Declare 
		    @Name nvarchar(120) = 'Updated Name'

			Select *
			From dbo.ResourceCategories
			Where Id = @Id

	Execute dbo.ResourceCategories_Update 
			   @Name 
			   ,@Id
			Select *
			From dbo.ResourceCategories
			Where Id = @Id
*/

as

BEGIN

	UPDATE [dbo].[ResourceCategories]
       SET [Category] = @Category
          
    WHERE Id = @Id
END
GO
/****** Object:  StoredProcedure [dbo].[Resources_Delete]    Script Date: 12/2/2022 3:13:23 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO


-- Author:		Bradley Loewen
-- Create date: 08/24/2022
-- Description:	Resources Delete
-- Code Reviewer: Aron Canapa 


-- MODIFIED BY: 
-- MODIFIED DATE:
-- Code Reviewer: 
-- Note: 

CREATE proc [dbo].[Resources_Delete]
		@Id int

as 
/*

	Declare @Id int = 8

	Select *
	From dbo.Resources
    WHERE Id = @Id;

	Execute dbo.Resources_Delete @Id

	Select *
	From dbo.Resources
    WHERE Id = @Id;

*/

BEGIN

	DELETE FROM [dbo].[Resources]
    WHERE Id = @Id;

END
GO
/****** Object:  StoredProcedure [dbo].[Resources_Details_ById]    Script Date: 12/2/2022 3:13:23 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- Author:		Bradley Loewen
-- Create date: 08/27/2022
-- Description:	Resources Select By Id with details
-- Code Reviewer: Heriberto Martinez 


-- MODIFIED BY: 
-- MODIFIED DATE:
-- Code Reviewer: 
-- Note: 


CREATE proc [dbo].[Resources_Details_ById]    
		@Id int


AS

/*
	
	Declare @Id int = 53;

	Execute dbo.Resources_Details_ById @Id

*/

BEGIN

           SELECT  r.Id
				  ,r.Name
				  ,r.Headline
				  ,r.Description
				  ,r.Logo
				  ,rc.Id as ResourceCategoryId
				  ,rc.Category as ResourceCategory
				  ,o.Id as OrganizationId
				  ,ot.Id as OrganizationTypeId
				  ,ot.Name as OrganizationTypeName
				  ,o.Name as OrganizationName
				  ,o.Headline as OrganizationHeadline
				  ,o.Description as OrganizationDescription
				  ,o.Logo as OrganizationLogo
				  ,l.Id as LocationId
				  ,lt.Id as LocationTypeId
				  ,lt.Name as LocationTypeName
				  ,l.LineOne
				  ,l.LineTwo
				  ,l.City
				  ,l.Zip
				  ,s.Id as StateId
				  ,s.Code as StateCode
				  ,s.Name as StateName
				  ,l.Latitude
				  ,l.Longitude
				  ,o.Phone as OrganizationPhone
				  ,o.SiteUrl as OrganizationSiteUrl
				  ,o.DateCreated as OrganizationDateCreated
				  ,o.DateModified as OrganizationDateModified
				  ,o.CreatedBy as OrganizationCreatedBy
				  ,r.ContactName
				  ,r.ContactEmail
				  ,r.Phone
				  ,r.SiteUrl
				  ,r.DateCreated
				  ,r.DateModified
				  ,r.CreatedBy
				  ,r.ModifiedBy
				  ,TotalCount = COUNT(1) OVER()
	
	FROM     dbo.Organizations as o inner join dbo.Resources as r
							on o.Id = r.OrgId
					inner join dbo.ResourceCategories as rc
							on rc.Id = r.ResourceCategoryId
					inner join dbo.Locations as l
							on o.LocationId = l.Id
					inner join dbo.LocationTypes as lt
							on l.LocationTypeId = lt.Id
					inner join dbo.States as s
							on l.StateId = s.Id
					inner join dbo.OrganizationTypes as ot
							on o.OrganizationTypeId = ot.Id

	WHERE	r.Id = @Id

END
GO
/****** Object:  StoredProcedure [dbo].[Resources_Details_ByLocationType]    Script Date: 12/2/2022 3:13:23 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- Author:		Bradley Loewen
-- Create date: 08/30/2022
-- Description:	Resources Select By Location Type
-- Code Reviewer: Heriberto Martinez


-- MODIFIED BY: 
-- MODIFIED DATE:
-- Code Reviewer: 
-- Note: 


CREATE proc [dbo].[Resources_Details_ByLocationType]    
		@LocationTypeId int
		,@PageIndex int 
        ,@PageSize int

AS

/*
	
	Declare @LocationTypeId int = 3
			,@PageIndex int = 0
			,@PageSize int = 3

	Execute dbo.Resources_Details_ByLocationType
					@LocationTypeId
					,@PageIndex 
					,@PageSize 

*/

BEGIN

	Declare @offset int = @PageIndex * @PageSize

          SELECT  r.Id
				  ,r.Name
				  ,r.Headline
				  ,r.Description
				  ,r.Logo
				  ,rc.Id as ResourceCategoryId
				  ,rc.Category as ResourceCategory
				  ,o.Id as OrganizationId
				  ,o.OrganizationTypeId as OrganizationType
				  ,o.Name as OrganizationName
				  ,o.Headline as OrganizationHeadline
				  ,o.Description as OrganizationDescription
				  ,o.Logo as OrganizationLogo
				  ,l.Id as LocationId
				  ,lt.Id as LocationTypeId
				  ,lt.Name as LocationTypeName
				  ,l.LineOne
				  ,l.LineTwo
				  ,l.City
				  ,l.Zip
				  ,s.Id as StateId
				  ,s.Code as StateCode
				  ,s.Name as StateName
				  ,l.Latitude
				  ,l.Longitude
				  ,l.DateCreated as LocationDateCreated
				  ,l.DateModified as LocationDateModified
				  ,l.CreatedBy as LocationCreatedBy
				  ,l.ModifiedBy as LocationModifiedBy
				  ,o.Phone as OrganizationPhone
				  ,o.SiteUrl as OrganizationSiteUrl
				  ,o.DateCreated as OrganizationDateCreated
				  ,o.DateModified as OrganizationDateModified
				  ,o.CreatedBy as OrganizationCreatedBy
				  ,r.ContactName
				  ,r.ContactEmail
				  ,r.Phone
				  ,r.SiteUrl
				  ,r.DateCreated
				  ,r.DateModified
				  ,r.CreatedBy
				  ,r.ModifiedBy
				  ,TotalCount = COUNT(1) OVER()
	
	FROM     dbo.Organizations as o inner join dbo.Resources as r
							on o.Id = r.OrgId
					inner join dbo.ResourceCategories as rc
							on rc.Id = r.ResourceCategoryId
					inner join dbo.Locations as l
							on o.LocationId = l.Id
					inner join dbo.LocationTypes as lt
							on l.LocationTypeId = lt.Id
					inner join dbo.States as s
							on l.StateId = s.Id
	Where lt.Id = @LocationTypeId

	ORDER BY [Id] 
	OFFSET @offSet Rows
	Fetch Next @PageSize Rows ONLY

END
GO
/****** Object:  StoredProcedure [dbo].[Resources_Insert]    Script Date: 12/2/2022 3:13:23 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO


-- Author:		Bradley Loewen
-- Create date: 08/24/2022
-- Description:	Resources Insert
-- Code Reviewer: Aron Canapa


-- MODIFIED BY: 
-- MODIFIED DATE:
-- Code Reviewer: 
-- Note: 

CREATE proc [dbo].[Resources_Insert]
			   @Name nvarchar(200)
			   ,@Headline nvarchar(200)
			   ,@Description nvarchar(1000)
			   ,@Logo nvarchar(255)
			   ,@ResourceCategoryId int
			   ,@OrgId int
			   ,@ContactName nvarchar(200)
			   ,@ContactEmail nvarchar(255)
			   ,@Phone nvarchar(50)
			   ,@SiteUrl nvarchar(255)
			   ,@CreatedBy int
			   ,@Id int OUTPUT

/*
	Declare @Id int = 0

	Declare 
		    @Name nvarchar(200) = 'Inserted Name With Created By Id'
			,@Headline nvarchar(200) = 'Inserted Headline'
			,@Description nvarchar(1000) = 'Inserted Desc'
			,@Logo nvarchar(255) = 'Inserted Logo'
			,@ResourceCategoryId int = 4
			,@OrgId int = 3
			,@ContactName nvarchar(200) = 'Inserted Contact Name'
			,@ContactEmail nvarchar(255) = 'Inserted Contact Email'
			,@Phone nvarchar(50) = 'Inserted Phone'
			,@SiteUrl nvarchar(255) = 'Inserted SiteUrl'
			,@CreatedBy int = 3

	Execute dbo.Resources_Insert 
			   @Name 
			   ,@Headline 
			   ,@Description 
			   ,@Logo 
			   ,@ResourceCategoryId 
			   ,@OrgId 
			   ,@ContactName 
			   ,@ContactEmail
			   ,@Phone 
			   ,@SiteUrl 
			   ,@CreatedBy
			   ,@Id OUTPUT

			Select @Id

			Select *
			From dbo.Resources
			Where Id = @Id
*/

as

BEGIN

	INSERT INTO [dbo].[Resources]
           ([Name]
           ,[Headline]
           ,[Description]
           ,[Logo]
           ,[ResourceCategoryId]
           ,[OrgId]
           ,[ContactName]
		   ,[ContactEmail]
		   ,[Phone]
		   ,[SiteUrl]
		   ,[CreatedBy]
		   ,[ModifiedBy])
     VALUES
           (@Name 
			   ,@Headline 
			   ,@Description 
			   ,@Logo 
			   ,@ResourceCategoryId 
			   ,@OrgId 
			   ,@ContactName 
			   ,@ContactEmail
			   ,@Phone 
			   ,@SiteUrl
			   ,@CreatedBy
			   ,@CreatedBy)

	SET @Id = SCOPE_IDENTITY()

END
GO
/****** Object:  StoredProcedure [dbo].[Resources_Search_Details]    Script Date: 12/2/2022 3:13:23 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- Author:		Bradley Loewen
-- Create date: 08/30/2022
-- Description:	Resources Search by Name, Headline, and Description
-- Code Reviewer: Heriberto Martinez


-- MODIFIED BY: 
-- MODIFIED DATE:
-- Code Reviewer: 
-- Note:

CREATE proc [dbo].[Resources_Search_Details]
        @PageIndex int 
        ,@PageSize int
		,@Query nvarchar(100)

as

/*	
			Declare @PageIndex int = 0
				,@PageSize int = 4
				,@Query nvarchar(100) = 'Hi'
		
		Execute dbo.Resources_Search_Details 
							@PageIndex
							,@PageSize
							,@Query
*/



BEGIN

   Declare @offset int = @PageIndex * @PageSize

		 SELECT  r.Id
				  ,r.Name
				  ,r.Headline
				  ,r.Description
				  ,r.Logo
				  ,rc.Id as ResourceCategoryId
				  ,rc.Category as ResourceCategory
				  ,o.Id as OrganizationId
				  ,ot.Id as OrganizationTypeId
				  ,ot.Name as OrganizationTypeName
				  ,o.Name as OrganizationName
				  ,o.Headline as OrganizationHeadline
				  ,o.Description as OrganizationDescription
				  ,o.Logo as OrganizationLogo
				  ,l.Id as LocationId
				  ,lt.Id as LocationTypeId
				  ,lt.Name as LocationTypeName
				  ,l.LineOne
				  ,l.LineTwo
				  ,l.City
				  ,l.Zip
				  ,s.Id as StateId
				  ,s.Code as StateCode
				  ,s.Name as StateName
				  ,l.Latitude
				  ,l.Longitude
				  ,o.Phone as OrganizationPhone
				  ,o.SiteUrl as OrganizationSiteUrl
				  ,o.DateCreated as OrganizationDateCreated
				  ,o.DateModified as OrganizationDateModified
				  ,o.CreatedBy as OrganizationCreatedBy
				  ,r.ContactName
				  ,r.ContactEmail
				  ,r.Phone
				  ,r.SiteUrl
				  ,r.DateCreated
				  ,r.DateModified
				  ,r.CreatedBy
				  ,r.ModifiedBy
				  ,TotalCount = COUNT(1) OVER()
	
	FROM     dbo.Organizations as o inner join dbo.Resources as r
							on o.Id = r.OrgId
					inner join dbo.ResourceCategories as rc
							on rc.Id = r.ResourceCategoryId
					inner join dbo.Locations as l
							on o.LocationId = l.Id
					inner join dbo.LocationTypes as lt
							on l.LocationTypeId = lt.Id
					inner join dbo.States as s
							on l.StateId = s.Id
					inner join dbo.OrganizationTypes as ot
							on o.OrganizationTypeId = ot.Id
	WHERE (r.Name LIKE '%' + @Query + '%') or (r.Headline LIKE '%' + @Query + '%') or (r.Description LIKE '%' + @Query + '%')
	ORDER BY r.Id 
	OFFSET @offSet Rows
	Fetch Next @PageSize Rows ONLY

END
GO
/****** Object:  StoredProcedure [dbo].[Resources_Select_ByCreatedBy_Paginated]    Script Date: 12/2/2022 3:13:23 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- Author:		Bradley Loewen
-- Create date: 08/24/2022
-- Description:	Resources Select By Creator
-- Code Reviewer: Aron Canapa


-- MODIFIED BY: 
-- MODIFIED DATE:
-- Code Reviewer: 
-- Note: 

CREATE proc [dbo].[Resources_Select_ByCreatedBy_Paginated]    
		@CreatedById int
		,@PageIndex int 
        ,@PageSize int

AS

/*
	
	Declare @CreatedById int = 8
			,@PageIndex int = 0
			,@PageSize int = 3

	Execute dbo.Resources_Select_ByCreatedBy_Paginated 
					@CreatedById
					,@PageIndex 
					,@PageSize 

*/

BEGIN

	Declare @offset int = @PageIndex * @PageSize

           SELECT 
		   [Id]
		   ,[Name]
           ,[Headline]
           ,[Description]
           ,[Logo]
           ,[ResourceCategoryId]
           ,[OrgId]
           ,[ContactName]
		   ,[ContactEmail]
		   ,[Phone]
		   ,[SiteUrl]
		   ,[DateCreated]
		   ,[DateModified]
		   ,[CreatedBy]
		   ,[ModifiedBy]
		   ,TotalCount = COUNT(1) OVER()
	FROM [dbo].[Resources]
	WHERE	[CreatedBy] = @CreatedById

	ORDER BY [Id] 
	OFFSET @offSet Rows
	Fetch Next @PageSize Rows ONLY

END
GO
/****** Object:  StoredProcedure [dbo].[Resources_Select_ById]    Script Date: 12/2/2022 3:13:23 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- Author:		Bradley Loewen
-- Create date: 08/24/2022
-- Description:	Resources Select By Id
-- Code Reviewer: Aron Canapa


-- MODIFIED BY: 
-- MODIFIED DATE:
-- Code Reviewer: 
-- Note: 

CREATE proc [dbo].[Resources_Select_ById]    
		@Id int


AS

/*
	
	Declare @Id int = 3;

	Execute dbo.Resources_Select_ById @Id

*/

BEGIN

     SELECT 
		   [Id]
		   ,[Name]
           ,[Headline]
           ,[Description]
           ,[Logo]
           ,[ResourceCategoryId]
           ,[OrgId]
           ,[ContactName]
		   ,[ContactEmail]
		   ,[Phone]
		   ,[SiteUrl]
		   ,[DateCreated]
		   ,[DateModified]
		   ,[CreatedBy]
		   ,[ModifiedBy]
	FROM [dbo].[Resources]
	WHERE	[Id] = @Id

END
GO
/****** Object:  StoredProcedure [dbo].[Resources_Select_ByResourceCategoryId]    Script Date: 12/2/2022 3:13:23 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- Author:		Bradley Loewen
-- Create date: 08/27/2022
-- Description:	Resources Select By Resource Type (Resource Category)
-- Code Reviewer: 


-- MODIFIED BY: 
-- MODIFIED DATE:
-- Code Reviewer: 
-- Note: 


CREATE proc [dbo].[Resources_Select_ByResourceCategoryId]    
		@ResourceCategoryId int
		,@PageIndex int 
        ,@PageSize int

AS

/*
	
	Declare @ResourceCategoryId int = 4
			,@PageIndex int = 0
			,@PageSize int = 3

	Execute dbo.Resources_Select_ByResourceCategoryId
					@ResourceCategoryId
					,@PageIndex 
					,@PageSize 

*/

BEGIN

	Declare @offset int = @PageIndex * @PageSize

           SELECT 
		   [Id]
		   ,[Name]
           ,[Headline]
           ,[Description]
           ,[Logo]
           ,[ResourceCategoryId]
           ,[OrgId]
           ,[ContactName]
		   ,[ContactEmail]
		   ,[Phone]
		   ,[SiteUrl]
		   ,[DateCreated]
		   ,[DateModified]
		   ,[CreatedBy]
		   ,[ModifiedBy]
		   ,TotalCount = COUNT(1) OVER()
	FROM [dbo].[Resources]
	WHERE [ResourceCategoryId] = @ResourceCategoryId

	ORDER BY [Id] 
	OFFSET @offSet Rows
	Fetch Next @PageSize Rows ONLY

END
GO
/****** Object:  StoredProcedure [dbo].[Resources_SelectAll_Paginated]    Script Date: 12/2/2022 3:13:23 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

-- Author:		Bradley Loewen
-- Create date: 08/24/2022
-- Description:	Resources Select All Paginated
-- Code Reviewer: Aron Canapa


-- MODIFIED BY: 
-- MODIFIED DATE:
-- Code Reviewer: 
-- Note: 

CREATE proc [dbo].[Resources_SelectAll_Paginated]    
		@PageIndex int 
        ,@PageSize int


AS

/*
	
	Declare @PageIndex int = 0
			,@PageSize int = 3

	Execute dbo.Resources_SelectAll_Paginated
				@PageIndex
				,@PageSize

*/

BEGIN

   Declare @offset int = @PageIndex * @PageSize

          SELECT 
		   [Id]
		   ,[Name]
           ,[Headline]
           ,[Description]
           ,[Logo]
           ,[ResourceCategoryId]
           ,[OrgId]
           ,[ContactName]
		   ,[ContactEmail]
		   ,[Phone]
		   ,[SiteUrl]
		   ,[DateCreated]
		   ,[DateModified]
		   ,[CreatedBy]
		   ,[ModifiedBy]
		   ,TotalCount = COUNT(1) OVER()
	FROM [dbo].[Resources]

	ORDER BY [Id] 
	OFFSET @offSet Rows
	Fetch Next @PageSize Rows ONLY

END
GO
/****** Object:  StoredProcedure [dbo].[Resources_Update]    Script Date: 12/2/2022 3:13:23 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO



-- Author:		Bradley Loewen
-- Create date: 08/24/2022
-- Description:	Resources Update
-- Code Reviewer: Aron Canapa


-- MODIFIED BY: 
-- MODIFIED DATE:
-- Code Reviewer: 
-- Note: 

CREATE proc [dbo].[Resources_Update]
			   @Name nvarchar(200)
			   ,@Headline nvarchar(200)
			   ,@Description nvarchar(1000)
			   ,@Logo nvarchar(255)
			   ,@ResourceCategoryId int
			   ,@OrgId int
			   ,@ContactName nvarchar(200)
			   ,@ContactEmail nvarchar(255)
			   ,@Phone nvarchar(50)
			   ,@SiteUrl nvarchar(255)
			   ,@ModifiedBy int
			   ,@Id int 

/*
	Declare @Id int = 13

	Declare 
		    @Name nvarchar(200) = 'Updated Name'
			,@Headline nvarchar(200) = 'Updated Headline'
			,@Description nvarchar(1000) = 'Updated Desc'
			,@Logo nvarchar(255) = 'Updated Logo'
			,@ResourceCategoryId int = 4
			,@OrgId int = 3
			,@ContactName nvarchar(200) = 'Updated Contact Name'
			,@ContactEmail nvarchar(255) = 'Updated Contact Email'
			,@Phone nvarchar(50) = 'Updated Phone'
			,@SiteUrl nvarchar(255) = 'Updated SiteUrl'
			,@ModifiedBy int = 1

			Select *
			From dbo.Resources
			Where Id = @Id

	Execute dbo.Resources_Update 
			   @Name 
			   ,@Headline 
			   ,@Description 
			   ,@Logo 
			   ,@ResourceCategoryId 
			   ,@OrgId 
			   ,@ContactName 
			   ,@ContactEmail
			   ,@Phone 
			   ,@SiteUrl 
			   ,@ModifiedBy
			   ,@Id 

			Select *
			From dbo.Resources
			Where Id = @Id
*/

as

BEGIN

	Declare @DateNow datetime2(7) = getutcdate()

	UPDATE [dbo].[Resources]
       SET [Name] = @Name
           ,[Headline] = @Headline
           ,[Description] = @Description
           ,[Logo] = @Logo
           ,[ResourceCategoryId] = @ResourceCategoryId
           ,[OrgId] = @OrgId
           ,[ContactName] = @ContactName
		   ,[ContactEmail] = @ContactEmail
		   ,[Phone] = @Phone
		   ,[SiteUrl] = @SiteUrl
		   ,[DateModified] = @DateNow
		   ,[ModifiedBy] = @ModifiedBy
    WHERE Id = @Id
END
GO
/****** Object:  StoredProcedure [dbo].[ResumeContact_Delete_ById]    Script Date: 12/2/2022 3:13:23 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

-- =============================================
-- Author: <Ledwin Batista>
-- Create date: <07/28/2022>
-- Description: <ResumeContact_Delete_ById>
-- Code Reviewer:

-- MODIFIED BY: author
-- MODIFIED DATE:12/1/2020
-- Code Reviewer:
-- Note:
-- =============================================

CREATE PROC [dbo].[ResumeContact_Delete_ById]
	@Id int

AS

/* -- TEST CODE --
	
	Declare @Id int = 1

	EXECUTE dbo.ResumeContact_Delete_ById @Id
	
*/ -- END TEST CODE --

BEGIN

	DELETE FROM dbo.ResumeContact
	WHERE @Id = Id

END
GO
/****** Object:  StoredProcedure [dbo].[ResumeContact_Insert]    Script Date: 12/2/2022 3:13:23 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

-- =============================================
-- Author: <Ledwin Batista>
-- Create date: <07/28/2022>
-- Description: <ResumeContact_Insert>
-- Code Reviewer:

-- MODIFIED BY: author
-- MODIFIED DATE:12/1/2020
-- Code Reviewer:
-- Note:
-- =============================================

CREATE PROC [dbo].[ResumeContact_Insert]
			 @Name nvarchar(200)
			,@Email nvarchar(255)
			,@Phone nvarchar(20)
			,@Notes nvarchar(500)
			,@CreatedBy int
			,@ModifiedBy int
			,@Id int OUTPUT

AS

/* -- TEST CODE --

	DECLARE
			@Name nvarchar(200) = 'Engineer Name'
			,@Email nvarchar(255) = 'Engineer Email'
			,@Phone nvarchar(20) = 'Engineer Phone'
			,@Notes nvarchar(500) = 'Engineer Notes'
			,@CreatedBy int = 12
			,@ModifiedBy int = 12
			,@Id int = 0

	EXECUTE dbo.ResumeContact_Insert 
			@Name
			,@Email
			,@Phone
			,@Notes
			,@CreatedBy
			,@ModifiedBy
			,@Id 

*/ -- END TEST CODE --

BEGIN

	INSERT INTO [dbo].[ResumeContact]
			([Name]
			,[Email]
			,[Phone]
			,[Notes]
			,[CreatedBy]
			,[ModifiedBy])

		 VALUES
			(@Name
			,@Email
			,@Phone
			,@Notes
			,@CreatedBy
			,@ModifiedBy)

		SET		@Id = SCOPE_IDENTITY()

END
GO
/****** Object:  StoredProcedure [dbo].[ResumeContact_Select_ByCreatedBy]    Script Date: 12/2/2022 3:13:23 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author: <Grant Lee>
-- Create date: <08/01/2022>
-- Description: <ResumeContact_Select_ByCreatedBy>
-- Code Reviewer: Morgan Frost 08/02/2022

-- MODIFIED BY: author
-- MODIFIED DATE: 08/01/2022
-- Code Reviewer:
-- Note:
-- =============================================

CREATE proc [dbo].[ResumeContact_Select_ByCreatedBy]
									 @PageIndex int
									,@PageSize int
									,@CreatedBy int

as

/* ----- TEST CODE -----

	Declare		 @PageIndex int = 0
				,@PageSize int = 2
				,@CreatedBy int = 4
	

	Execute [dbo].[ResumeContact_Select_ByCreatedBy] 
				 @PageIndex
				,@PageSize
				,@CreatedBy

*/ ----- END TEST CODE -----

BEGIN

Declare	   @offset int = @PageIndex * @PageSize

	SELECT   rc.[Id]
			,rc.[Name]
		    ,rc.[Email]
            ,rc.[Phone]
            ,rc.[Notes]
			,rc.[CreatedBy]
			,TotalCount = COUNT(1) OVER()

	FROM [dbo].[ResumeContact] as rc

WHERE		rc.CreatedBy = @CreatedBy
ORDER BY	rc.Id
			OFFSET @offset ROWS
FETCH NEXT  @PageSize ROWS ONLY

END
GO
/****** Object:  StoredProcedure [dbo].[ResumeContact_Select_ById]    Script Date: 12/2/2022 3:13:23 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author: <Grant Lee>
-- Create date: <08/01/2022>
-- Description: <ResumeContact_Select_ById>
-- Code Reviewer: Morgan Frost 08/02/2022

-- MODIFIED BY: author
-- MODIFIED DATE:08/01/2022
-- Code Reviewer:
-- Note:
-- =============================================

CREATE proc [dbo].[ResumeContact_Select_ById]
									@Id int
as

/*
	Declare @Id int = 5
	Execute dbo.ResumeContact_Select_ById
									  @Id

	Select *
	FROM dbo.ResumeContact
*/


BEGIN

SELECT resCon.[Id]
      ,resCon.[Name]
      ,resCon.[Email]
      ,resCon.[Phone]
      ,resCon.[Notes]
      ,resCon.[DateCreated]
      ,resCon.[DateModified]
      ,resCon.[CreatedBy]
      ,resCon.[ModifiedBy]
  FROM [dbo].[ResumeContact] as resCon
  WHERE resCon.Id = @Id

END
GO
/****** Object:  StoredProcedure [dbo].[ResumeContact_SelectAll]    Script Date: 12/2/2022 3:13:23 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author: <Grant Lee>
-- Create date: <08/01/2022>
-- Description: <ResumeContact_SelectAll>
-- Code Reviewer: Morgan Frost 08/02/2022

-- MODIFIED BY: author
-- MODIFIED DATE: 08/01/2022
-- Code Reviewer:
-- Note:
-- =============================================

CREATE proc [dbo].[ResumeContact_SelectAll]
							@PageIndex int
						   ,@PageSize int
as

/*------TEST CODE------

Declare @PageIndex int = 0
	   ,@PageSize int = 10

Execute [dbo].[ResumeContact_SelectAll]
						  @PageIndex
						 ,@PageSize

*/

BEGIN

Declare @offset int = @PageIndex * @PageSize

SELECT rc.[Id]
      ,rc.[Name]
      ,rc.[Email]
      ,rc.[Phone]
      ,rc.[Notes]
      ,rc.[DateCreated]
      ,rc.[DateModified]
      ,rc.[CreatedBy]
      ,rc.[ModifiedBy]
	  ,TotalCount = COUNT(1) OVER()

  FROM [dbo].[ResumeContact] as rc
  ORDER BY rc.Id

  OFFSET @offSet Rows
  FETCH NEXT @PageSize ROWS ONLY

END
GO
/****** Object:  StoredProcedure [dbo].[ResumeContact_Update]    Script Date: 12/2/2022 3:13:23 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

-- =============================================
-- Author: <Ledwin Batista>
-- Create date: <07/28/2022>
-- Description: <ResumeContact_Update>
-- Code Reviewer:

-- MODIFIED BY: author
-- MODIFIED DATE:12/1/2020
-- Code Reviewer:
-- Note:
-- =============================================

CREATE PROC [dbo].[ResumeContact_Update]
			@Name nvarchar(200)
			,@Email nvarchar(255)
			,@Phone nvarchar(20)
			,@Notes nvarchar(500)
			,@CreatedBy int
			,@ModifiedBy int
			,@Id int
AS

BEGIN

	UPDATE [dbo].[ResumeContact]

	   SET [Name] = @Name
		  ,[Email] = @Email
		  ,[Phone] = @Phone
		  ,[Notes] = @Notes
		  ,[CreatedBy] = @CreatedBy
		  ,[ModifiedBy] = @ModifiedBy
		  ,[DateModified] = GETUTCDATE()

	 WHERE @Id = Id

END


GO
/****** Object:  StoredProcedure [dbo].[ResumeEducation_Insert]    Script Date: 12/2/2022 3:13:23 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author: <Ledwin Batista>
-- Create date: <07/29/2022>
-- Description: <ResumeEducation_Insert>
-- Code Reviewer:

-- MODIFIED BY: author
-- MODIFIED DATE:12/1/2020
-- Code Reviewer:
-- Note:
-- =============================================

CREATE PROC [dbo].[ResumeEducation_Insert]
			@ResumeId int
			,@EducationId int

AS

/* -- TEST CODE --

	Declare
			 @ResumeId int = 1
			,@EducationId int = 3


	EXECUTE [dbo].[ResumeEducation_Insert]
			 @ResumeId
			,@EducationId


	Select * 
	from dbo.Resumes
	order by ResumeId
			
	select * 
	from dbo.Education
		
*/ -- END TEST CODE --


BEGIN
	

	INSERT INTO [dbo].[ResumeEducation] 
			([ResumeId]
			,[EducationId])
		
		VALUES
			(@ResumeId
			,@EducationId) 

END


GO
/****** Object:  StoredProcedure [dbo].[ResumeEducation_SelectAll]    Script Date: 12/2/2022 3:13:23 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author: <Grant Lee>
-- Create date: <08/01/2022>
-- Description: <ResumeEducation_SelectAll>
-- Code Reviewer: Morgan Frost 08/02/2022

-- MODIFIED BY: author
-- MODIFIED DATE: 08/01/2022
-- Code Reviewer:
-- Note:
-- =============================================

CREATE proc [dbo].[ResumeEducation_SelectAll]
							@PageIndex int
						   ,@PageSize int
as

/*------TEST CODE------

Declare @PageIndex int = 0
	   ,@PageSize int = 10

Execute [dbo].[ResumeEducation_SelectAll]
						  @PageIndex
						 ,@PageSize

*/

BEGIN

Declare @offset int = @PageIndex * @PageSize

SELECT rEdu.ResumeId
      ,rEdu.EducationId
	  ,TotalCount = COUNT(1) OVER()

  FROM [dbo].[ResumeEducation] as rEdu
  ORDER BY rEdu.ResumeId

  OFFSET @offSet Rows
  FETCH NEXT @PageSize ROWS ONLY

END
GO
/****** Object:  StoredProcedure [dbo].[ResumeExperience_Insert]    Script Date: 12/2/2022 3:13:23 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author: <Ledwin Batista>
-- Create date: <07/29/2022>
-- Description: <ResumeExperience_Insert>
-- Code Reviewer:

-- MODIFIED BY: author
-- MODIFIED DATE:12/1/2020
-- Code Reviewer:
-- Note:
-- =============================================

CREATE PROC [dbo].[ResumeExperience_Insert]
			@ResumeId int
			,@ExperienceId int

AS

/* -- TEST CODE --

	Declare
			 @ResumeId int = 1
			,@ExperienceId int = 2


	EXECUTE [dbo].[ResumeExperience_Insert]
			 @ResumeId
			,@ExperienceId


	Select * 
	from dbo.Resumes
	order by ResumeId
			
	select * 
	from dbo.Experience
		
*/ -- END TEST CODE --


BEGIN
	

	INSERT INTO [dbo].[ResumeExperience] 
			([ResumeId]
			,[ExperienceId])
		
		VALUES
			(@ResumeId
			,@ExperienceId) 

END


GO
/****** Object:  StoredProcedure [dbo].[ResumeExperience_SelectAll]    Script Date: 12/2/2022 3:13:23 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author: <Grant Lee>
-- Create date: <08/01/2022>
-- Description: <ResumeExperience_SelectAll>
-- Code Reviewer: Morgan Frost 08/02/2022

-- MODIFIED BY: author
-- MODIFIED DATE: 08/01/2022
-- Code Reviewer:
-- Note:
-- =============================================

CREATE proc [dbo].[ResumeExperience_SelectAll]
							@PageIndex int
						   ,@PageSize int
as

/*------TEST CODE------

Declare @PageIndex int = 0
	   ,@PageSize int = 10

Execute [dbo].[ResumeExperience_SelectAll]
						  @PageIndex
						 ,@PageSize

*/

BEGIN

Declare @offset int = @PageIndex * @PageSize

SELECT re.ResumeId
      ,re.ExperienceId
	  ,TotalCount = COUNT(1) OVER()

  FROM [dbo].[ResumeExperience] as re
  ORDER BY re.ResumeId

  OFFSET @offSet Rows
  FETCH NEXT @PageSize ROWS ONLY

END
GO
/****** Object:  StoredProcedure [dbo].[ResumeFreelanceGoalType_Insert]    Script Date: 12/2/2022 3:13:23 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author: <Ledwin Batista>
-- Create date: <07/29/2022>
-- Description: <ResumeFreelanceGoalType_Insert>
-- Code Reviewer:

-- MODIFIED BY: author
-- MODIFIED DATE:12/1/2020
-- Code Reviewer:
-- Note:
-- =============================================

CREATE PROC [dbo].[ResumeFreelanceGoalType_Insert]
			@ResumeId int
			,@FreelanceGoalTypeId int

AS

/* -- TEST CODE --

	Declare
			 @ResumeId int = 1
			,@FreelanceGoalTypeId = 4


	EXECUTE [dbo].[ResumeFreelanceGoalType_Insert]
			 @ResumeId
			,@FreelanceGoalTypeId


	Select * 
	from dbo.Resumes
	order by ResumeId
			
	select * 
	from dbo.FreelanceGoalTypeId
		
*/ -- END TEST CODE --


BEGIN
	

	INSERT INTO [dbo].[ResumeFreelanceGoalType] 
			([ResumeId]
			,[FreelanceGoalTypeId])
		
		VALUES
			(@ResumeId
			,@FreelanceGoalTypeId) 

END


GO
/****** Object:  StoredProcedure [dbo].[ResumeFreelanceGoalTypes_SelectAll]    Script Date: 12/2/2022 3:13:23 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author: <Grant Lee>
-- Create date: <08/01/2022>
-- Description: <ResumeFreelanceGoalTypes_SelectAll>
-- Code Reviewer: Morgan Frost 08/02/2022

-- MODIFIED BY: author
-- MODIFIED DATE: 08/01/2022
-- Code Reviewer:
-- Note:
-- =============================================

CREATE proc [dbo].[ResumeFreelanceGoalTypes_SelectAll]
							@PageIndex int
						   ,@PageSize int
as

/*------TEST CODE------

Declare @PageIndex int = 0
	   ,@PageSize int = 10

Execute [dbo].[ResumeFreelanceGoalTypes_SelectAll]
						  @PageIndex
						 ,@PageSize

*/

BEGIN

Declare @offset int = @PageIndex * @PageSize

SELECT rf.ResumeId
      ,rf.FreelanceGoalTypeId
	  ,TotalCount = COUNT(1) OVER()

  FROM [dbo].[ResumeFreelanceGoalType] as rf
  ORDER BY rf.ResumeId

  OFFSET @offSet Rows
  FETCH NEXT @PageSize ROWS ONLY

END
GO
/****** Object:  StoredProcedure [dbo].[Resumes_Delete_ById]    Script Date: 12/2/2022 3:13:23 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

-- =============================================
-- Author: <Ledwin Batista>
-- Create date: <07/28/2022>
-- Description: <Resumes_Delete_ById>
-- Code Reviewer: Thomas Sauer

-- MODIFIED BY: author
-- MODIFIED DATE:12/1/2020
-- Code Reviewer:
-- Note:
-- =============================================

CREATE PROC [dbo].[Resumes_Delete_ById]
	@Id int

AS

/* -- TEST CODE --
	
	Declare @Id int = 1

	EXECUTE dbo.Resumes_Delete_ById @Id
	
*/ -- END TEST CODE --

BEGIN

	DELETE FROM dbo.Resumes
	WHERE @Id = Id

END
GO
/****** Object:  StoredProcedure [dbo].[Resumes_Insert]    Script Date: 12/2/2022 3:13:23 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

-- =============================================
-- Author: <Ledwin Batista>
-- Create date: <07/28/2022>
-- Description: <Resumes_Insert>
-- Code Reviewer: Thomas Sauer

-- MODIFIED BY: author
-- MODIFIED DATE:12/1/2020
-- Code Reviewer:
-- Note:
-- =============================================

CREATE PROC [dbo].[Resumes_Insert]
			 @Description nvarchar(200)
			,@ResumeContactId int
			,@ResumeFileId int
			,@CreatedBy int
			,@Id int OUTPUT

AS

/* -- TEST CODE --

	DECLARE
			@Description nvarchar(200) = 'Engineer'
			,@ResumeContactId int = 15
			,@ResumeFileId int = 3
			,@CreatedBy int = 12
			,@Id int = 0

	EXECUTE dbo.Resumes_Insert 
			@Description
			,@ResumeContactId
			,@ResumeFileId
			,@CreatedBy
			,@Id

*/ -- END TEST CODE --

BEGIN

	INSERT INTO [dbo].[Resumes]
			([Description]
			,[ResumeContactId]
			,[ResumeFileId]
			,[CreatedBy])

		 VALUES
			(@Description
			,@ResumeContactId
			,@ResumeFileId
			,@CreatedBy)

		SET		@Id = SCOPE_IDENTITY()

END
GO
/****** Object:  StoredProcedure [dbo].[Resumes_InsertV2]    Script Date: 12/2/2022 3:13:23 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author: <Justin Solo>
-- Create date: <09/02/2022>
-- Description: <Resume_InsertV2>
-- Code Reviewer:

-- MODIFIED BY: author
-- MODIFIED DATE:12/1/2020
-- Code Reviewer:
-- Note:
-- =============================================
CREATE PROC [dbo].[Resumes_InsertV2]
	@Description nvarchar(200)
	,@Name nvarchar(200)
	,@Email nvarchar(255)
	,@Phone nvarchar(20)
	,@Notes nvarchar(500)
	,@ResumeFileId int
	,@CreatedBy int
	,@ModifiedBy int
	,@BatchExperience dbo.Experience READONLY
	,@BatchEducation dbo.Education READONLY
	,@Id int OUTPUT
AS	

/*-----TEST CODE-----

DECLARE @Description nvarchar(200) = 'Desc test 2'
		,@Name nvarchar(200) = 'First2 Last2'
		,@Email nvarchar(255) =  'email2@email.com'
		,@Phone nvarchar(20) = '1112223333'
		,@Notes nvarchar(500) = 'Resume notes2'
		,@ResumeFileId int = 5
		,@CreatedBy int = 136
		,@ModifiedBy int = 136
		,@Id int = 0

	DECLARE @BatchExperience dbo.Experience 
	INSERT INTO @BatchExperience
				(Title
				,EmploymentTypeId
				,CompanyName
				,LocationId
				,IsCurrent
				,DateStart
				,DateEnd
				,Description
				,CreatedBy
				,ModifiedBy)
			VALUES
				('Full Stack'
				,1
				,'Aquifer'
				,1
				,1
				,'09-02-2022'
				,'09-03-2022'
				,'Desc test'
				,136
				,136)
	INSERT INTO @BatchExperience
				(Title
				,EmploymentTypeId
				,CompanyName
				,LocationId
				,IsCurrent
				,DateStart
				,DateEnd
				,Description
				,CreatedBy
				,ModifiedBy)
			VALUES
				('Back End Engineer'
				,1
				,'Amazon'
				,1
				,1
				,'09-02-2021'
				,'09-01-2022'
				,'Desc test'
				,136
				,136)
	SELECT * FROM @BatchExperience

	DECLARE @BatchEducation dbo.Education
	INSERT INTO @BatchEducation
				(InstitutionId
				,EdProgramTypeId
				,SpecializationTypeId
				,DateStart
				,DateEnd
				,Description
				,IsGraduated
				,CreatedBy
				,ModifiedBy)
			VALUES
				(1
				,2
				,2
				,'05-01-2017'
				,'09-01-2022'
				,'Ed desc'
				,1
				,135
				,135)
	SELECT * from @BatchEducation

	EXECUTE dbo.Resumes_InsertV2
				@Description
				,@Name
				,@Email
				,@Phone
				,@Notes
				,@ResumeFileId
				,@CreatedBy
				,@ModifiedBy
				,@BatchExperience
				,@BatchEducation
				,@Id OUTPUT
		
*/


BEGIN
	DECLARE @ExperienceId int = 0
	DECLARE @ResumeId int = 0
	DECLARE @ResumeContactId int = 0
	DECLARE @ResumeEducationId int = 0

	INSERT INTO dbo.ResumeContact
					(Name
					,Email
					,Phone
					,Notes
					,CreatedBy
					,ModifiedBy)
			VALUES
					(@Name
					,@Email
					,@Phone
					,@Notes
					,@CreatedBy
					,@ModifiedBy)

			SET @ResumeContactId = SCOPE_IDENTITY();

	INSERT INTO dbo.Education
					(InstitutionId
					,EdProgramTypeId
					,SpecializationTypeId
					,DateStart
					,DateEnd
					,Description
					,IsGraduated
					,CreatedBy
					,ModifiedBy)
			SELECT 
					ed.InstitutionId
					,ed.EdProgramTypeId
					,ed.SpecializationTypeId
					,ed.DateStart
					,ed.DateEnd
					,ed.Description
					,ed.IsGraduated
					,ed.CreatedBy
					,ed.ModifiedBy
			FROM @BatchEducation as ed

	INSERT INTO dbo.Experience
					(Title
					,EmploymentTypeId
					,CompanyName
					,LocationId
					,IsCurrent
					,DateStart
					,DateEnd
					,Description
					,CreatedBy
					,ModifiedBy)
			SELECT be.Title
					,be.EmploymentTypeId
					,be.CompanyName
					,be.LocationId
					,be.IsCurrent
					,be.DateStart
					,be.DateEnd
					,be.Description
					,be.CreatedBy
					,be.ModifiedBy
			FROM @BatchExperience as be

	INSERT INTO dbo.Resumes
				(Description
				,ResumeContactId
				,ResumeFileId
				,CreatedBy)
			VALUES
				(@Description
				,@ResumeContactId
				,@ResumeFileId
				,@CreatedBy)

			SET @ResumeId = SCOPE_IDENTITY()

	INSERT INTO dbo.ResumeExperience
				(ResumeId
				,ExperienceId)
			SELECT
				@ResumeId
				,e.Id
			FROM dbo.Experience as e
			WHERE EXISTS(SELECT 1
						FROM @BatchExperience as be
						WHERE e.CreatedBy = be.CreatedBy)

	INSERT INTO dbo.ResumeEducation
				(ResumeId
				,EducationId)
			SELECT
				@ResumeId
				,ed.Id
			FROM dbo.Education as ed
			WHERE Exists(SELECT 1
						FROM @BatchEducation as be
						WHERE ed.CreatedBy = be.CreatedBy)

END
GO
/****** Object:  StoredProcedure [dbo].[Resumes_InsertV3]    Script Date: 12/2/2022 3:13:23 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author: <Justin Solo>
-- Create date: <09/02/2022>
-- Description: <Resume_InsertV2>
-- Code Reviewer:

-- MODIFIED BY: author
-- MODIFIED DATE:12/1/2020
-- Code Reviewer:
-- Note:
-- =============================================
CREATE PROC [dbo].[Resumes_InsertV3]
	@Description nvarchar(200)
	,@Name nvarchar(200)
	,@Email nvarchar(255)
	,@Phone nvarchar(20)
	,@Notes nvarchar(500)
	,@ResumeFileId int
	,@CreatedBy int
	,@ModifiedBy int
	,@BatchExperience dbo.Experience READONLY
	,@BatchEducation dbo.Education READONLY
	,@BatchSkills dbo.Skills READONLY
	,@BatchFreelanceGoals dbo.FreelanceGoal READONLY
	,@Id int OUTPUT
AS	

/*-----TEST CODE-----

DECLARE @Description nvarchar(200) = 'Desc test 24'
		,@Name nvarchar(200) = 'First2 Last2'
		,@Email nvarchar(255) =  'email2@email.com'
		,@Phone nvarchar(20) = '1112223333'
		,@Notes nvarchar(500) = 'Resume notes2'
		,@ResumeFileId int = 5
		,@CreatedBy int = 136
		,@ModifiedBy int = 136
		,@Id int = 0

	DECLARE @BatchExperience dbo.Experience
	INSERT INTO @BatchExperience
				(Title
				,EmploymentTypeId
				,CompanyName
				,LocationId
				,IsCurrent
				,DateStart
				,DateEnd
				,Description
				,CreatedBy
				,ModifiedBy
				)
			VALUES
				('Full Stack'
				,1
				,'Aquifer'
				,1
				,1
				,'09-02-2022'
				,'09-03-2022'
				,'Desc test'
				,136
				,136
				)
	INSERT INTO @BatchExperience
				(Title
				,EmploymentTypeId
				,CompanyName
				,LocationId
				,IsCurrent
				,DateStart
				,DateEnd
				,Description
				,CreatedBy
				,ModifiedBy)
			VALUES
				('Back End Engineer'
				,1
				,'Amazon'
				,1
				,1
				,'09-02-2021'
				,'09-01-2022'
				,'Desc test'
				,136
				,136
				)
	SELECT * FROM @BatchExperience

	DECLARE @BatchEducation dbo.Education
	INSERT INTO @BatchEducation
				(InstitutionId
				,EdProgramTypeId
				,SpecializationTypeId
				,DateStart
				,DateEnd
				,Description
				,IsGraduated
				,CreatedBy
				,ModifiedBy)
			VALUES
				(1
				,2
				,2
				,'05-01-2017'
				,'09-01-2022'
				,'Ed desc'
				,1
				,135
				,135)
	SELECT * from @BatchEducation

	DECLARE @BatchSkills dbo.Skills
	INSERT INTO @BatchSkills (Name)
	VALUES ('ASP.NET')
		   ,('Javascript')
	SELECT * FROM @BatchSkills

	DECLARE @BatchFreelanceGoals dbo.FreelanceGoal
	INSERT INTO @BatchFreelanceGoals (Name)
	VALUES ('Fulltime')
	SELECT * FROM @BatchFreelanceGoals

	EXECUTE dbo.Resumes_InsertV3
				@Description
				,@Name
				,@Email
				,@Phone
				,@Notes
				,@ResumeFileId
				,@CreatedBy
				,@ModifiedBy
				,@BatchExperience
				,@BatchEducation
				,@Id OUTPUT
		
*/


BEGIN
	DECLARE @ExperienceId int = 0
	DECLARE @ResumeId int = 0
	DECLARE @ResumeContactId int = 0
	DECLARE @ResumeEducationId int = 0

	DECLARE @ExperiencesId as table ( ExId int )
	DECLARE @EducationsId as table ( EdId int )

	INSERT INTO dbo.ResumeContact
					(Name
					,Email
					,Phone
					,Notes
					,CreatedBy
					,ModifiedBy)
			VALUES
					(@Name
					,@Email
					,@Phone
					,@Notes
					,@CreatedBy
					,@ModifiedBy)

			SET @ResumeContactId = SCOPE_IDENTITY();

	INSERT INTO dbo.Education
					(InstitutionId
					,EdProgramTypeId
					,SpecializationTypeId
					,DateStart
					,DateEnd
					,Description
					,IsGraduated
					,CreatedBy
					,ModifiedBy)

			OUTPUT	INSERTED.Id
			INTO	@EducationsId
					(
						EdId
					)
			SELECT 

					ed.InstitutionId
					,ed.EdProgramTypeId
					,ed.SpecializationTypeId
					,ed.DateStart
					,ed.DateEnd
					,ed.Description
					,ed.IsGraduated
					,ed.CreatedBy
					,ed.ModifiedBy
			FROM @BatchEducation as ed

	INSERT INTO dbo.Experience
					(Title
					,EmploymentTypeId
					,CompanyName
					,LocationId
					,IsCurrent
					,DateStart
					,DateEnd
					,Description
					,CreatedBy
					,ModifiedBy)
					
			OUTPUT	INSERTED.Id
			INTO	@ExperiencesId
					(
						ExId
					)
					
			SELECT be.Title
					,be.EmploymentTypeId
					,be.CompanyName
					,be.LocationId
					,be.IsCurrent
					,be.DateStart
					,be.DateEnd
					,be.Description
					,be.CreatedBy
					,be.ModifiedBy
			FROM @BatchExperience as be

	INSERT INTO dbo.Resumes
				(Description
				,ResumeContactId
				,ResumeFileId
				,CreatedBy)
			VALUES
				(@Description
				,@ResumeContactId
				,@ResumeFileId
				,@CreatedBy)

			SET @Id = SCOPE_IDENTITY()
			SET @ResumeId = SCOPE_IDENTITY()

	INSERT INTO dbo.ResumeExperience
				(ResumeId
				,ExperienceId)
			SELECT
				@ResumeId
				,e.Id
			FROM dbo.Experience as e
			WHERE EXISTS(SELECT 1
						FROM @ExperiencesId as be
						WHERE e.Id = be.ExId)

	INSERT INTO dbo.ResumeEducation
				(ResumeId
				,EducationId)
			SELECT
				@ResumeId
				,ed.Id
			FROM dbo.Education as ed
			WHERE Exists(SELECT 1
						FROM @EducationsId as be
						WHERE ed.Id = be.EdId)

	INSERT INTO [dbo].[Skills] ([Name])
	SELECT	 [s].[Name]
	FROM	 @BatchSkills as s
	WHERE	 NOT EXISTS( SELECT 1
						 FROM [dbo].[Skills] AS [OS]
						 WHERE [OS].Name = [s].Name )
	
	INSERT INTO dbo.ResumeSkills(ResumeId, SkillId)
		(SELECT @ResumeId, [OS].Id
			FROM dbo.Skills as [OS]
			WHERE Exists (
						SELECT 1
						FROM @BatchSkills as s
						WHERE [OS].[Name] = [s].[Name]
			)
		)

	INSERT INTO [dbo].[FreelanceGoalTypes] ([Name])
	SELECT	 [s].[Name]
	FROM	 @BatchFreelanceGoals as s
	WHERE	 NOT EXISTS( SELECT 1
						 FROM [dbo].[FreelanceGoalTypes] AS [OS]
						 WHERE [OS].Name = [s].Name )
	
	INSERT INTO dbo.ResumeFreelanceGoalType(ResumeId, FreelanceGoalTypeId)
		(SELECT @ResumeId, [OS].Id
			FROM dbo.FreelanceGoalTypes as [OS]
			WHERE Exists (
						SELECT 1
						FROM @BatchFreelanceGoals as s
						WHERE [OS].[Name] = [s].[Name]
			)
		)
END
GO
/****** Object:  StoredProcedure [dbo].[Resumes_Select_ByCreatedBy]    Script Date: 12/2/2022 3:13:23 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

-- =============================================
-- Author: <Ledwin Batista>
-- Create date: <07/29/2022>
-- Description: <Resumes_Select_ByCreatedBy>
-- Code Reviewer:

-- MODIFIED BY: author
-- MODIFIED DATE:12/1/2020
-- Code Reviewer:
-- Note:
-- =============================================

CREATE PROC [dbo].[Resumes_Select_ByCreatedBy]
		 @PageIndex int
		,@PageSize int
		,@CreatedBy int

/* -- TEST CODE --

	Declare  @PageIndex int = 0
			,@PageSize int = 10
			,@CreatedBy int = 12

	Execute [dbo].[Resumes_Select_ByCreatedBy]
			 @PageIndex
			,@PageSize
			,@CreatedBy

*/ -- END TEST CODE --

AS

BEGIN

	Declare @offset int = @PageIndex * @PageSize

	SELECT
			 r.[Id]
			,r.[Description]
			,r.[ResumeContactId]
			,r.[ResumeFileId]
			,r.[CreatedBy]
			,r.[DateCreated]
			,r.[DateModified]
			,TotalCount = COUNT(1) OVER()
			
	FROM [dbo].[Resumes] AS r INNER JOIN [dbo].[ResumeContact] AS rc
							ON rc.Id = r.ResumeContactId
							INNER JOIN [dbo].[Education] AS edu
							ON edu.CreatedBy = r.CreatedBy
							INNER JOIN [dbo].[Experience] AS ex
							ON ex.CreatedBy = r.CreatedBy

	WHERE r.Id = @CreatedBy
	ORDER BY r.Id

	OFFSET @offset Rows
	FETCH Next @PageSize Rows ONLY

END
GO
/****** Object:  StoredProcedure [dbo].[Resumes_Select_ById]    Script Date: 12/2/2022 3:13:23 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

-- =============================================
-- Author: <Ledwin Batista>
-- Create date: <07/29/2022>
-- Description: <Resumes_Select_ById @Id>
-- Code Reviewer:

-- MODIFIED BY: author
-- MODIFIED DATE:12/1/2020
-- Code Reviewer:
-- Note:
-- =============================================

CREATE proc [dbo].[Resumes_Select_ById]
			@Id int

/* -- TEST CODE --

	Declare @Id int = 1;

	Execute [dbo].[Resumes_Select_ById] @Id

*/ -- END TEST CODE --

AS

BEGIN

	SELECT
			 r.[Id]
			,r.[Description]
			,r.[ResumeContactId]
			,r.[ResumeFileId]
			,r.[CreatedBy]
			,r.[DateCreated]
			,r.[DateModified]
			

	FROM [dbo].[Resumes] AS r INNER JOIN [dbo].[ResumeContact] AS rc
							ON rc.Id = r.ResumeContactId
							INNER JOIN [dbo].[Education] AS edu
							ON edu.CreatedBy = r.CreatedBy
							INNER JOIN [dbo].[Experience] AS ex
							ON ex.CreatedBy = r.CreatedBy

	WHERE r.Id = @Id

END
GO
/****** Object:  StoredProcedure [dbo].[Resumes_SelectAll]    Script Date: 12/2/2022 3:13:23 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

-- =============================================
-- Author: <Ledwin Batista>
-- Create date: <07/29/2022>
-- Description: <Resumes_SelectAll>
-- Code Reviewer:

-- MODIFIED BY: author
-- MODIFIED DATE:12/1/2020
-- Code Reviewer:
-- Note:
-- =============================================

CREATE PROC [dbo].[Resumes_SelectAll]
		 @PageIndex int
		,@PageSize int

/* -- TEST CODE --

	Declare  @PageIndex int = 0
			,@PageSize int = 10

	Execute [dbo].[Resumes_SelectAll]
			 @PageIndex
			,@PageSize

*/ -- END TEST CODE --

AS

BEGIN

	Declare @offset int = @PageIndex * @PageSize

	SELECT
			 r.[Id]
			,r.[Description]
			,r.[ResumeContactId]
			,r.[ResumeFileId]
			,r.[CreatedBy]
			,r.[DateCreated]
			,r.[DateModified]
			,TotalCount = COUNT(1) OVER()
			
	FROM [dbo].[Resumes] AS r INNER JOIN [dbo].[ResumeContact] AS rc
							ON rc.Id = r.ResumeContactId
							INNER JOIN [dbo].[Education] AS edu
							ON edu.CreatedBy = r.CreatedBy
							INNER JOIN [dbo].[Experience] AS ex
							ON ex.CreatedBy = r.CreatedBy

	ORDER BY r.Id

	OFFSET @offset Rows
	FETCH Next @PageSize Rows ONLY

END
GO
/****** Object:  StoredProcedure [dbo].[Resumes_SelectAll_Details]    Script Date: 12/2/2022 3:13:23 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author: <Grant Lee>
-- Create date: <08/01/2022>
-- Description: <SelectAll_Details>
-- Code Reviewer: Morgan Frost 08/02/2022

-- MODIFIED BY: author
-- MODIFIED DATE: 08/12/2022
-- Code Reviewer:
-- Note:
-- =============================================


CREATE proc [dbo].[Resumes_SelectAll_Details]
								@PageIndex int
							   ,@PageSize int


as

/*

declare @PageIndex int = 0
declare @PageSize int = 2
Execute [dbo].[Resumes_SelectAll_Details]
		 @PageIndex
		 ,@PageSize

*/

BEGIN

DECLARE @offset int = @PageIndex * @PageSize

SELECT r_base.Id as [Id]	  
	  ,r_contact.[Name] as [Name]
	  ,r_contact.Email as [Email]
	  ,r_contact.Phone as [Phone Number]
	  ,r_base.[Description] as [Description]
	  ,[Past Experience] =
								  (
									SELECT experience.Title as Title
										  ,experience.EmploymentTypeId as EmploymentTypeId
										  ,et.[Name] as EmploymentType
										  ,experience.CompanyName as CompanyName
										  ,experience.LocationId as LocationId
										  ,l.city as City
										  ,l.zip as Zip										    
										  ,experience.IsCurrent as IsCurrentJob
									FROM dbo.Experience as experience inner join dbo.ResumeExperience as ex
														on ex.ExperienceId = experience.Id
																	  inner join dbo.EmploymentTypes as et
														on et.Id = experience.EmploymentTypeId
																	  inner join dbo.Locations as l
														on l.Id = experience.LocationId
									WHERE ex.ResumeId = r_base.Id
									FOR JSON PATH
								  )
	  ,[Education] =
								  (
									SELECT education.InstitutionId as [InstitutionId]
										  ,inst.[Name] as [Institution]
										  ,education.EdProgramTypeId as [EdProgramTypeId]
										  ,edprgm.[Name] as [EducationProgramType]
										  ,education.SpecializationTypeId as [SpecializationTypeId]
										  ,spelzn.[Name] as [Specialization]
										  ,education.DateStart as [StartDate]
										  ,education.DateEnd as [EndDate]
										  ,education.[Description] as [Description]
									FROM dbo.Education as education inner join dbo.ResumeEducation as ed
													   on ed.EducationId = education.Id
																	inner join dbo.Institution as inst
													   on inst.Id = education.InstitutionId
																	inner join dbo.EdProgramTypes as edprgm
													   on edprgm.Id = education.EdProgramTypeId
																	inner join dbo.SpecializationTypes as spelzn
													   on spelzn.Id = education.SpecializationTypeId
									WHERE ed.ResumeId = r_base.Id
									FOR JSON PATH
								  )

	  ,[Freelance Goals] =
								  (
									SELECT  freelance.Id as Id
										   ,freelance.[Name] as [FreelanceGoal]
									FROM dbo.FreelanceGoalTypes as freelance inner join dbo.ResumeFreelanceGoalType as fr
															    on fr.FreelanceGoalTypeId = freelance.Id
									WHERE fr.ResumeId = r_base.Id
									FOR JSON AUTO
								  )

	  ,[List of Skills] = 
								  (
									SELECT   skills.Id as Id
											,skills.Name as [Skill]
									FROM dbo.Skills as skills inner join dbo.ResumeSkills as sk
												    on sk.SkillId = skills.Id
									WHERE sk.ResumeId = r_base.Id
									FOR JSON AUTO
								  )
	  ,r_base.ResumeFileId as [Resume File Id]
	  ,TotalCount = COUNT(1) OVER()

FROM dbo.Resumes as r_base inner join dbo.ResumeContact as r_contact
				 on r_base.ResumeContactId = r_contact.Id
				 ORDER BY Id

OFFSET @offset Rows
FETCH NEXT @PageSize Rows ONLY

END
GO
/****** Object:  StoredProcedure [dbo].[Resumes_SelectAllV2]    Script Date: 12/2/2022 3:13:23 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO


CREATE PROC [dbo].[Resumes_SelectAllV2]

AS

/* --- TEST CODE ---

EXECUTE dbo.Resumes_SelectAllV2

*/

BEGIN

	SELECT r.[Id]
		  ,r.ResumeContactId
		  ,rc.Name
		  ,rc.Email
		  ,rc.Phone
		  ,rc.Notes
		  ,rc.DateCreated
		  ,rc.DateModified
		  ,rc.CreatedBy
		  ,rc.ModifiedBy
		  ,r.[Description]
		  ,r.[ResumeContactId]
		  ,r.[ResumeFileId]
		  ,r.[CreatedBy]
		  ,r.[DateCreated]
		  ,r.[DateModified]
		  ,Experience = (SELECT *
						 FROM dbo.Experience as e
						 INNER JOIN
						 dbo.ResumeExperience as re
						 ON e.Id = re.ExperienceId
						 WHERE re.ResumeId = r.Id
						 FOR JSON AUTO)
		  ,Education = (SELECT *
						FROM dbo.Education as e
						INNER JOIN
						dbo.ResumeEducation as re
						on e.Id = re.EducationId
						WHERE re.ResumeId = r.Id
						FOR JSON AUTO)
		  ,ResumeFileId
	  FROM [dbo].[Resumes] as r
	  INNER JOIN
	  dbo.ResumeContact as rc
	  on rc.Id = r.ResumeContactId



END
GO
/****** Object:  StoredProcedure [dbo].[Resumes_SelectAllV3]    Script Date: 12/2/2022 3:13:23 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROC [dbo].[Resumes_SelectAllV3]

AS

/* --- TEST CODE ---

EXECUTE dbo.Resumes_SelectAllV3

*/

BEGIN

	SELECT r.[Id]
		  ,r.ResumeContactId
		  ,rc.Name
		  ,rc.Email
		  ,rc.Phone
		  ,rc.Notes
		  ,rc.DateCreated
		  ,rc.DateModified
		  ,rc.CreatedBy
		  ,rc.ModifiedBy
		  ,r.[Description]
		  ,r.[ResumeContactId]
		  ,r.[ResumeFileId]
		  ,r.[CreatedBy]
		  ,r.[DateCreated]
		  ,r.[DateModified]
		  ,Experience = (SELECT *
						 FROM dbo.Experience as e
						 INNER JOIN
						 dbo.ResumeExperience as re
						 ON e.Id = re.ExperienceId
						 WHERE re.ResumeId = r.Id
						 FOR JSON AUTO)
		  ,Education = (SELECT *
						FROM dbo.Education as e
						INNER JOIN
						dbo.ResumeEducation as re
						on e.Id = re.EducationId
						WHERE re.ResumeId = r.Id
						FOR JSON AUTO)
		  ,Skills = (SELECT *
					FROM dbo.Skills as s
					INNER JOIN
					dbo.ResumeSkills as rs
					ON s.Id = rs.SkillId
					WHERE rs.ResumeId = r.Id
					FOR JSON AUTO)	
		  ,FreelanceGoal = (Select *
							FROM dbo.FreelanceGoalTypes as fg
							INNER JOIN
							dbo.ResumeFreelanceGoalType as rf
							on fg.Id = rf.FreelanceGoalTypeId
							WHERE rf.ResumeId = r.Id
							FOR JSON AUTO)
 		  ,ResumeFileId
	  FROM [dbo].[Resumes] as r
	  INNER JOIN
	  dbo.ResumeContact as rc
	  on rc.Id = r.ResumeContactId



END
GO
/****** Object:  StoredProcedure [dbo].[Resumes_SelectById_Details]    Script Date: 12/2/2022 3:13:23 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author: <Grant Lee>
-- Create date: <07/29/2022>
-- Description: <SelectById_Details>
-- Code Reviewer: Morgan Frost 08/02/2022

-- MODIFIED BY: author
-- MODIFIED DATE: 08/12/2022
-- Code Reviewer:
-- Note:
-- =============================================


CREATE proc [dbo].[Resumes_SelectById_Details]
								   @Id int

as

/*

declare @Id int = 25
Execute [dbo].[Resumes_SelectById_Details] @Id

---- resume field id 2 and 4 occupied ----
*/

BEGIN

SELECT r_base.Id as [Id]	  
	  ,r_contact.[Name] as [Name]
	  ,r_contact.Email as [Email]
	  ,r_contact.Phone as [Phone Number]
	  ,r_base.[Description] as [Description]
	  , PastExperience =
								  (
									SELECT experience.Title as Title
										  ,experience.EmploymentTypeId as EmploymentTypeId
										  ,et.[Name] as EmploymentType
										  ,experience.CompanyName as CompanyName
										  ,experience.LocationId as LocationId
										  ,l.city as City
										  ,l.zip as Zip										    
										  ,experience.IsCurrent as IsCurrentJob
									FROM dbo.Experience as experience inner join dbo.ResumeExperience as ex
														on ex.ExperienceId = experience.Id
																	  inner join dbo.EmploymentTypes as et
														on et.Id = experience.EmploymentTypeId
																	  inner join dbo.Locations as l
														on l.Id = experience.LocationId
									WHERE ex.ResumeId = @Id AND r_base.Id = @Id
									
									FOR JSON PATH
								  )
	  ,[Education] =
								  (
									SELECT education.InstitutionId as [InstitutionId]
										  ,inst.[Name] as [Institution]
										  ,education.EdProgramTypeId as [EdProgramTypeId]
										  ,edprgm.[Name] as [EducationProgramType]
										  ,education.SpecializationTypeId as [SpecializationTypeId]
										  ,spelzn.[Name] as [Specialization]
										  ,education.DateStart as [StartDate]
										  ,education.DateEnd as [EndDate]
										  ,education.[Description] as [Description]
									FROM dbo.Education as education inner join dbo.ResumeEducation as ed
													   on ed.EducationId = education.Id
																	inner join dbo.Institution as inst
													   on inst.Id = education.InstitutionId
																	inner join dbo.EdProgramTypes as edprgm
													   on edprgm.Id = education.EdProgramTypeId
																	inner join dbo.SpecializationTypes as spelzn
													   on spelzn.Id = education.SpecializationTypeId
									WHERE ed.ResumeId = @Id
									FOR JSON PATH
								  )

	  ,[Freelance Goals] =
								  (
									SELECT  freelance.Id as Id
										   ,freelance.[Name] as [FreelanceGoal]
									FROM dbo.FreelanceGoalTypes as freelance inner join dbo.ResumeFreelanceGoalType as fr
															    on fr.FreelanceGoalTypeId = freelance.Id
									WHERE fr.ResumeId = @Id
									FOR JSON AUTO
								  )

	  ,[List of Skills] = 
								  (
									SELECT   skills.Id as Id
											,skills.Name as [Skill]
									FROM dbo.Skills as skills inner join dbo.ResumeSkills as sk
												    on sk.SkillId = skills.Id
									WHERE sk.ResumeId = @Id
									FOR JSON AUTO
								  )
	  ,r_base.ResumeFileId as [Resume File Id]

FROM dbo.Resumes as r_base inner join dbo.ResumeContact as r_contact
				 on r_base.ResumeContactId = r_contact.Id
				 /*
						   inner join dbo.ResumeExperience as r_experienceBridge
				 on r_base.Id = r_experienceBridge.ResumeId
				 
						   inner join dbo.ResumeEducation as r_educationBridge
				 on r_base.Id = r_educationBridge.ResumeId
				 
						   inner join dbo.ResumeFreelanceGoalType as r_freelanceBridge
				 on r_base.Id = r_freelanceBridge.ResumeId
				 
						   inner join dbo.ResumeSkills as r_skillsBridge
				 on r_base.Id = r_skillsBridge.ResumeId
				 */

WHERE r_base.Id = @Id

END
GO
/****** Object:  StoredProcedure [dbo].[Resumes_Update]    Script Date: 12/2/2022 3:13:23 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

-- =============================================
-- Author: <Ledwin Batista>
-- Create date: <07/28/2022>
-- Description: <Resumes_Update>
-- Code Reviewer: Thomas Sauer

-- MODIFIED BY: author
-- MODIFIED DATE:12/1/2020
-- Code Reviewer: 
-- Note:
-- =============================================

CREATE PROC [dbo].[Resumes_Update]
			@Description nvarchar(200)
			,@ResumeContactId int
			,@ResumeFileId int
			,@CreatedBy int
			,@Id int
AS

BEGIN

	UPDATE [dbo].[Resumes]

	   SET [Description] = @Description
		  ,[ResumeContactId] = @ResumeContactId
		  ,[ResumeFileId] = @ResumeFileId
		  ,[CreatedBy] = @CreatedBy
		  ,[DateModified] = GETUTCDATE()

	 WHERE @Id = Id

END


GO
/****** Object:  StoredProcedure [dbo].[ResumeSkills_Insert]    Script Date: 12/2/2022 3:13:23 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author: <Ledwin Batista>
-- Create date: <07/29/2022>
-- Description: <ResumeSkills_Insert>
-- Code Reviewer:

-- MODIFIED BY: author
-- MODIFIED DATE:12/1/2020
-- Code Reviewer:
-- Note:
-- =============================================

CREATE PROC [dbo].[ResumeSkills_Insert]
			@ResumeId int
			,@SkillId int

AS

/* -- TEST CODE --

	Declare
			 @ResumeId int = 1
			,@SkillId int = 4


	EXECUTE [dbo].[ResumeSkills_Insert]
			 @ResumeId
			,@SkillId


	Select * 
	from dbo.Resumes
	order by ResumeId
			
	select * 
	from dbo.ResumeSkills
		
*/ -- END TEST CODE --


BEGIN
	

	INSERT INTO [dbo].[ResumeSkills] 
			([ResumeId]
			,[SkillId])
		
		VALUES
			(@ResumeId
			,@SkillId) 

END


GO
/****** Object:  StoredProcedure [dbo].[ResumeSkills_SelectAll]    Script Date: 12/2/2022 3:13:23 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author: <Grant Lee>
-- Create date: <08/01/2022>
-- Description: <ResumeSkills_SelectAll>
-- Code Reviewer: Morgan Frost 08/02/2022

-- MODIFIED BY: author
-- MODIFIED DATE: 08/01/2022
-- Code Reviewer:
-- Note:
-- =============================================

CREATE proc [dbo].[ResumeSkills_SelectAll]
							@PageIndex int
						   ,@PageSize int
as

/*------TEST CODE------

Declare @PageIndex int = 0
	   ,@PageSize int = 10

Execute [dbo].[ResumeSkills_SelectAll]
						  @PageIndex
						 ,@PageSize

*/

BEGIN

Declare @offset int = @PageIndex * @PageSize

SELECT rs.ResumeId
      ,rs.SkillId
	  ,TotalCount = COUNT(1) OVER()

  FROM [dbo].[ResumeSkills] as rs
  ORDER BY rs.ResumeId

  OFFSET @offSet Rows
  FETCH NEXT @PageSize ROWS ONLY

END
GO
/****** Object:  StoredProcedure [dbo].[ResumeViews_Count_ByResumeId]    Script Date: 12/2/2022 3:13:23 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

-- =============================================
-- Author: <Aron Canapa>
-- Create date: <8/26/2022>
-- Description: <ResumeViews_Count_ByResumeId>
-- Code Reviewer:Justin Solo

-- MODIFIED BY:
-- MODIFIED DATE:
-- Code Reviewer:
-- Note:
-- =============================================

CREATE PROC [dbo].[ResumeViews_Count_ByResumeId]

@ResumeId int
,@Count int = 0 OUTPUT

as

/*

DECLARE @ResumeId int = 26

EXECUTE [dbo].[ResumeViews_Count_ByResumeId] @ResumeId

*/

BEGIN

SET @Count = (select  COUNT([Id])

FROM [dbo].[ResumeViews]

Where [ResumeId] = @ResumeId)

END
GO
/****** Object:  StoredProcedure [dbo].[ResumeViews_Insert]    Script Date: 12/2/2022 3:13:23 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROC [dbo].[ResumeViews_Insert]
@ViewerId int
,@ResumeId int
,@Id int = 0 OUTPUT
as

/*

Select   [Id]
		,[ViewerId]
		,[ResumeId]
		,[DateCreated]
From dbo.ResumeViews

DECLARE @ViewerId int = 20
		,@ResumeId int= 25

EXECUTE [dbo].[ResumeViews_Insert]   @ViewerId
									,@ResumeId

Select   [Id]
		,[ViewerId]
		,[ResumeId]
		,[DateCreated]
From dbo.ResumeViews

*/

IF (@ViewerId != (SELECT [CreatedBy] FROM [dbo].[Resumes] where [Id] = @ResumeId))

BEGIN

INSERT INTO [dbo].[ResumeViews]
			([ResumeId]
			,[ViewerId])

	   VALUES (@ResumeId
		    ,@ViewerId)
	
	SET @Id = SCOPE_IDENTITY()

END
GO
/****** Object:  StoredProcedure [dbo].[ResumeViews_PagedByResumeId]    Script Date: 12/2/2022 3:13:23 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

-- =============================================
-- Author: <Aron Canapa>
-- Create date: <8/26/2022>
-- Description: <ResumeViews_PagedByResumeId>
-- Code Reviewer:Justin Solo

-- MODIFIED BY:
-- MODIFIED DATE:
-- Code Reviewer:
-- Note:
-- =============================================

CREATE PROC [dbo].[ResumeViews_PagedByResumeId]

@PageIndex int
,@PageSize int
,@ResumeId int

as

/*

DECLARE @PageIndex int = 0
		,@PageSize int = 3
		,@ResumeId int = 25

EXECUTE [dbo].[ResumeViews_PagedByResumeId] @PageIndex 
											,@PageSize 
											,@ResumeId 

*/

BEGIN

DECLARE @offset int = @PageIndex * @PageSize

SELECT [Id]
		,[ResumeId]
		,[ViewerId]
		,[DateCreated]
		,TotalCount = COUNT(1) OVER()

FROM	[dbo].[ResumeViews]
Where [ResumeId] = @ResumeId

 ORDER BY DateCreated

  OFFSET @offset Rows
  Fetch Next @PageSize Rows ONLY


END
GO
/****** Object:  StoredProcedure [dbo].[ResumeViews_SelectById]    Script Date: 12/2/2022 3:13:23 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

-- =============================================
-- Author: <Aron Canapa>
-- Create date: <8/26/2022>
-- Description: <ResumeViews_SelectById>
-- Code Reviewer:Justin Solo

-- MODIFIED BY:
-- MODIFIED DATE:
-- Code Reviewer:
-- Note:
-- =============================================


CREATE PROC [dbo].[ResumeViews_SelectById]

@Id int

as

/*

DECLARE @Id int = 2

EXECUTE [dbo].[ResumeViews_SelectById] @Id

*/

BEGIN

SELECT [Id]
		,[ResumeId]
		,[ViewerId]
		,[DateCreated]
FROM [dbo].[ResumeViews]
Where Id = @Id

END
GO
/****** Object:  StoredProcedure [dbo].[Roles_SelectAll]    Script Date: 12/2/2022 3:13:23 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author: <Justin Solo>
-- Create date: <07/01/2022>
-- Description: <Users_Select_ByEmail>
-- Code Reviewer:

-- MODIFIED BY: author
-- MODIFIED DATE:12/1/2020
-- Code Reviewer:
-- Note:
-- =============================================

CREATE PROC [dbo].[Roles_SelectAll]

AS

/* ----- TEST CODE -----
	
	EXECUTE dbo.Roles_SelectAll

*/ ----- END TEST CODE -----

BEGIN

	SELECT [Id]
		  ,[Name]
	  FROM [dbo].[Roles]

END
GO
/****** Object:  StoredProcedure [dbo].[ScheduleAvailability_Delete_ById]    Script Date: 12/2/2022 3:13:23 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author: <Justin Solo>
-- Create date: <07/19/2022>
-- Description: <ScheduleAvailability_Delete_ById>
-- Code Reviewer:

-- MODIFIED BY: author
-- MODIFIED DATE:12/1/2020
-- Code Reviewer:
-- Note:
-- =============================================
CREATE PROC [dbo].[ScheduleAvailability_Delete_ById]
	@Id int

AS

/* ----- TEST CODE -----

	DECLARE @Id int = 1

	EXECUTE ScheduleAvailability_Delete_ById @Id 

*/ ----- END TEST CODE -----

BEGIN

	DELETE FROM [dbo].[ScheduleAvailability]
	  WHERE @Id = Id

END
GO
/****** Object:  StoredProcedure [dbo].[ScheduleAvailability_Insert]    Script Date: 12/2/2022 3:13:23 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author: <Justin Solo>
-- Create date: <07/19/2022>
-- Description: <ScheduleAvailability_Insert>
-- Code Reviewer:

-- MODIFIED BY: author
-- MODIFIED DATE:12/1/2020
-- Code Reviewer:
-- Note:
-- =============================================
CREATE PROC [dbo].[ScheduleAvailability_Insert]
	@ScheduleId int
	,@DayOfWeek int
	,@StartDate datetime2(7)
	,@EndDate datetime2(7)
	,@CreatedBy int
	,@ModifiedBy int
	,@Id int OUTPUT

AS

/* ----- TEST CODE -----

	DECLARE  @ScheduleId int = 3
			,@OrgId int = 1
			,@DayOfWeek int = 2
			,@StartDate datetime2(7) = '06:30'
			,@EndDate datetime2(7) = '14:30'
			,@CreatedBy int = 20
			,@ModifiedBy int = 20
			,@Id int = 0

	EXECUTE dbo.ScheduleAvailability_Insert @ScheduleId
								,@OrgId
								,@DayOfWeek
								,@StartTime
								,@EndTime
								,@CreatedBy
								,@ModifiedBy
								,@Id 

*/ ----- END TEST CODE -----

BEGIN

	INSERT INTO [dbo].[ScheduleAvailability]
           ([ScheduleId]
           ,[DayOfWeek]
           ,[StartDate]
           ,[EndDate]
           ,[CreatedBy]
           ,[ModifiedBy])
     VALUES
           (@ScheduleId
           ,@DayOfWeek
           ,@StartDate
           ,@EndDate
           ,@CreatedBy
           ,@ModifiedBy)
	SET		@Id = SCOPE_IDENTITY()

END
GO
/****** Object:  StoredProcedure [dbo].[ScheduleAvailability_Select_ByCreatedBy]    Script Date: 12/2/2022 3:13:23 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author: <Justin Solo>
-- Create date: <07/19/2022>
-- Description: <ScheduleAvailability_Select_ByCreatedBy>
-- Code Reviewer:

-- MODIFIED BY: author
-- MODIFIED DATE:12/1/2020
-- Code Reviewer:
-- Note:
-- =============================================
CREATE PROC [dbo].[ScheduleAvailability_Select_ByCreatedBy]
	@UserId int

AS

/* ----- TEST CODE -----

	@UserId int = 135

	EXECUTE dbo.ScheduleAvailability_Select_ByCreatedBy @UserId

*/ ----- END TEST CODE -----

BEGIN

	SELECT sa.[Id]
		  ,sa.[ScheduleId]
		  ,dt.[Name] as DayOfWeek
		  ,sa.[StartDate]
		  ,sa.[EndDate]
		  ,sa.[DateCreated]
		  ,sa.[DateModified]
		  ,up.Id
		  ,sa.[CreatedBy]
		  ,up.FirstName
		  ,up.Mi
		  ,up.LastName
		  ,up.AvatarUrl
		  ,up2.Id
		  ,sa.[ModifiedBy]
		  ,up2.FirstName
		  ,up2.Mi
		  ,up2.LastName
		  ,up2.AvatarUrl
	  FROM [dbo].[ScheduleAvailability] AS sa
	  INNER JOIN
	  dbo.ScheduleAvailability_DayTypes as dt
	  on dt.id = sa.DayOfWeek
	  INNER JOIN
	  dbo.UserProfiles as up
	  on up.UserId = sa.CreatedBy
	  INNER JOIN
	  dbo.UserProfiles as up2
	  on up2.UserId = sa.ModifiedBy

	  WHERE @UserId = sa.CreatedBy

END
GO
/****** Object:  StoredProcedure [dbo].[ScheduleAvailability_Select_ById]    Script Date: 12/2/2022 3:13:23 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author: <Justin Solo>
-- Create date: <07/19/2022>
-- Description: <ScheduleAvailability_Select_ById>
-- Code Reviewer:

-- MODIFIED BY: author
-- MODIFIED DATE:12/1/2020
-- Code Reviewer:
-- Note:
-- =============================================
CREATE PROC [dbo].[ScheduleAvailability_Select_ById]
	@Id int

AS

/* ----- TEST CODE -----

	DECLARE @Id int = 5

	EXECUTE dbo.ScheduleAvailability_Select_ById @Id 

*/ ----- END TEST CODE -----

BEGIN

	SELECT sa.[Id]
		  ,sa.[ScheduleId]
		  ,sa.[DayOfWeek]
		  ,sa.[StartDate]
		  ,sa.[EndDate]
		  ,sa.[DateCreated]
		  ,sa.[DateModified]
		  ,up.Id
		  ,sa.[CreatedBy]
		  ,up.FirstName
		  ,up.Mi
		  ,up.LastName
		  ,up.AvatarUrl
		  ,up2.Id
		  ,sa.[ModifiedBy]
		  ,up2.FirstName
		  ,up2.Mi
		  ,up2.LastName
		  ,up2.AvatarUrl
	  FROM [dbo].[ScheduleAvailability] AS sa
	  INNER JOIN
	  dbo.UserProfiles as up
	  ON up.UserId = sa.CreatedBy
	  INNER JOIN
	  dbo.UserProfiles as up2
	  ON up2.UserId = sa.ModifiedBy
	  
	  WHERE @Id = sa.Id

END
GO
/****** Object:  StoredProcedure [dbo].[ScheduleAvailability_SelectAll]    Script Date: 12/2/2022 3:13:23 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author: <Justin Solo>
-- Create date: <07/19/2022>
-- Description: <ScheduleAvailability_SelectAll>
-- Code Reviewer:

-- MODIFIED BY: author
-- MODIFIED DATE:12/1/2020
-- Code Reviewer:
-- Note:
-- =============================================
CREATE PROC [dbo].[ScheduleAvailability_SelectAll]
	@PageIndex int
	,@PageSize int

AS

/* ----- TEST CODE -----

	Declare @PageIndex int = 0
			,@PageSize int = 5

	EXECUTE dbo.ScheduleAvailability_SelectAll @PageIndex
											  ,@PageSize

*/ ----- END TEST CODE -----

BEGIN

	Declare @offset int = @PageIndex * @PageSize

	SELECT sa.[Id]
		  ,sa.[ScheduleId]
		  ,sa.[DayOfWeek]
		  ,sa.[StartDate]
		  ,sa.[EndDate]
		  ,sa.[DateCreated]
		  ,sa.[DateModified]
		  ,up.Id
		  ,sa.[CreatedBy]
		  ,up.FirstName
		  ,up.Mi
		  ,up.LastName
		  ,up.AvatarUrl
		  ,up2.Id
		  ,sa.[ModifiedBy]
		  ,up2.FirstName
		  ,up2.Mi
		  ,up.LastName
		  ,up2.AvatarUrl
		  ,TotalCount = COUNT(1) OVER()
	  FROM [dbo].[ScheduleAvailability] as sa
	  INNER JOIN
	  dbo.UserProfiles as up
	  ON up.UserId = sa.CreatedBy
	  INNER JOIN
	  dbo.UserProfiles as up2
	  ON up2.UserId = sa.ModifiedBy
	  ORDER BY sa.Id

	  OFFSET @offSet Rows
	  Fetch Next @PageSize Rows ONLY

END
GO
/****** Object:  StoredProcedure [dbo].[ScheduleAvailability_Update]    Script Date: 12/2/2022 3:13:23 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author: <Justin Solo>
-- Create date: <07/19/2022>
-- Description: <ScheduleAvailability_Update>
-- Code Reviewer:

-- MODIFIED BY: author
-- MODIFIED DATE:12/1/2020
-- Code Reviewer:
-- Note:
-- =============================================
CREATE PROC [dbo].[ScheduleAvailability_Update]
	@ScheduleId int
	,@DayOfWeek int
	,@StartTime time(7)
	,@EndTime time(7)
	,@ModifiedBy int
	,@Id int

AS

/* ----- TEST CODE -----

	DECLARE @ScheduleId int = 1
			,@DayOfWeek int = 2
			,@StartTime time = '06:30'
			,@EndTime time(7) = '16:30'
			,@ModifiedBy int = 145
			,@Id int = 4

	EXECUTE ScheduleAvailability_Update @ScheduleId
								,@DayOfWeek
								,@StartTime
								,@EndTime
								,@ModifiedBy
								,@Id 

*/ ----- END TEST CODE -----

BEGIN

	UPDATE [dbo].[ScheduleAvailability]
	   SET [ScheduleId] = @ScheduleId
		  ,[DayOfWeek] = @DayOfWeek
		  ,[StartTime] = @StartTime
		  ,[EndTime] = @EndTime
		  ,[DateModified] = GETUTCDATE()
		  ,[ModifiedBy] = @ModifiedBy
	 WHERE @Id = Id

END
GO
/****** Object:  StoredProcedure [dbo].[Schedules_Delete_ById]    Script Date: 12/2/2022 3:13:23 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author: <Justin Solo>
-- Create date: <07/19/2022>
-- Description: <Schedules_Delete_ById>
-- Code Reviewer:

-- MODIFIED BY: author
-- MODIFIED DATE:12/1/2020
-- Code Reviewer:
-- Note:
-- =============================================

CREATE PROC [dbo].[Schedules_Delete_ById]
	@Id int

AS

/* ----- TEST CODE -----
	
	Declare @Id int = 1

	EXECUTE dbo.Schedules_Delete_ById @Id
	
*/ ----- END TEST CODE -----

BEGIN

	DELETE FROM dbo.Schedules
	WHERE @Id = Id

END
GO
/****** Object:  StoredProcedure [dbo].[Schedules_Insert]    Script Date: 12/2/2022 3:13:23 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author: <Justin Solo>
-- Create date: <07/19/2022>
-- Description: <Schedules_Insert>
-- Code Reviewer:

-- MODIFIED BY: author
-- MODIFIED DATE:12/1/2020
-- Code Reviewer:
-- Note:
-- =============================================

CREATE PROC [dbo].[Schedules_Insert]
	@OrgId int
	,@CreatedBy int
	,@ModifiedBy int
	,@Id int OUTPUT

AS

/* ----- TEST CODE -----

	DECLARE @OrgId int = 3
			,@CreatedBy int = 135
			,@ModifiedBy int = 135
			,@Id int = 0

	EXECUTE dbo.Schedules_Insert @OrgId
								,@CreatedBy
								,@ModifiedBy
								,@Id 
*/ ----- END TEST CODE -----

BEGIN

	INSERT INTO [dbo].[Schedules]
			   ([OrgId]
			   ,[CreatedBy]
			   ,[ModifiedBy])
		 VALUES
			   (@OrgId
				,@CreatedBy
				,@ModifiedBy)

		SET		@Id = SCOPE_IDENTITY()

END
GO
/****** Object:  StoredProcedure [dbo].[Schedules_Select_ByCreatedBy]    Script Date: 12/2/2022 3:13:23 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author: <Justin Solo>
-- Create date: <07/19/2022>
-- Description: <Schedules_Select_ByCreatedBy>
-- Code Reviewer:

-- MODIFIED BY: author
-- MODIFIED DATE:12/1/2020
-- Code Reviewer:
-- Note:
-- =============================================

CREATE PROC [dbo].[Schedules_Select_ByCreatedBy]
	@PageIndex int
	,@PageSize int
	,@UserId int

AS

/* ----- TEST CODE -----
	
	Declare @PageIndex int = 0
			,@PageSize int = 5
			,@UserId int = 135

	EXECUTE dbo.Schedules_Select_ByCreatedBy @PageIndex
											,@PageSize
											,@UserId
	
*/ ----- END TEST CODE -----

BEGIN

	Declare @offset int = @PageIndex * @PageSize

	SELECT s.Id
			,o.Name AS Organization
			,up.Id
			,up.UserId
			,up.FirstName
			,up.Mi
			,up.LastName
			,up.AvatarUrl
			,up2.Id
			,up2.UserId
			,up2.FirstName
			,up2.Mi
			,up2.LastName
			,up2.AvatarUrl
			,s.DateCreated
			,s.DateModified
			,TotalCount = COUNT(1) OVER()
	FROM dbo.Schedules as s
	INNER JOIN
	dbo.Organizations as o
	ON o.Id = s.OrgId 
	INNER JOIN
	dbo.UserProfiles as up
	ON up.UserId = s.CreatedBy
	INNER JOIN
	dbo.UserProfiles as up2
	ON up2.UserId = s.ModifiedBy

	WHERE @UserId = s.CreatedBy
	ORDER BY s.Id

	OFFSET @offSet Rows
	Fetch Next @PageSize Rows ONLY

END
GO
/****** Object:  StoredProcedure [dbo].[Schedules_Select_ById]    Script Date: 12/2/2022 3:13:23 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author: <Justin Solo>
-- Create date: <07/19/2022>
-- Description: <Schedules_Select_ById>
-- Code Reviewer:

-- MODIFIED BY: author
-- MODIFIED DATE:12/1/2020
-- Code Reviewer:
-- Note:
-- =============================================

CREATE PROC [dbo].[Schedules_Select_ById]
	@Id int 

AS

/* ----- TEST CODE -----

	DECLARE @Id int = 3

	EXECUTE dbo.Schedules_Select_ById @Id
	
*/ ----- END TEST CODE -----

BEGIN

	SELECT s.Id
			,o.Name AS Organization
			,up.Id
			,up.UserId
			,up.FirstName
			,up.Mi
			,up.LastName
			,up.AvatarUrl
			,up2.Id
			,up2.UserId
			,up2.FirstName
			,up2.Mi
			,up2.LastName
			,up2.AvatarUrl
			,s.DateCreated
			,s.DateModified
	FROM dbo.Schedules AS s
	INNER JOIN
	dbo.Organizations as o
	on o.Id = s.OrgId
	INNER JOIN
	dbo.UserProfiles as up
	on up.UserId = s.CreatedBy
	INNER JOIN
	dbo.UserProfiles as up2
	ON up2.UserId = s.ModifiedBy
	WHERE @Id = s.Id
END
GO
/****** Object:  StoredProcedure [dbo].[Schedules_SelectAll]    Script Date: 12/2/2022 3:13:23 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author: <Justin Solo>
-- Create date: <07/19/2022>
-- Description: <Schedules_SelectAll>
-- Code Reviewer:

-- MODIFIED BY: author
-- MODIFIED DATE:12/1/2020
-- Code Reviewer:
-- Note:
-- =============================================

CREATE PROC [dbo].[Schedules_SelectAll]
	@PageIndex int
	,@PageSize int

AS

/* ----- TEST CODE -----
	
	Declare @PageIndex int = 0
			,@PageSize int = 50

	EXECUTE dbo.Schedules_SelectAll @PageIndex
									,@PageSize
	
*/ ----- END TEST CODE -----

BEGIN

	Declare @offset int = @PageIndex * @PageSize

	SELECT s.Id
			,o.Name AS Organization
			,up.Id
			,up.UserId
			,up.FirstName
			,up.Mi
			,up.LastName
			,up.AvatarUrl
			,up2.Id
			,up.UserId
			,up2.FirstName
			,up2.Mi
			,up2.LastName
			,Up2.AvatarUrl
			,s.DateCreated
			,s.DateModified
			,TotalCount = COUNT(1) OVER()
	FROM dbo.Schedules AS s
	INNER JOIN
	dbo.Organizations as o
	ON o.Id = s.OrgId
	INNER JOIN
	dbo.UserProfiles AS up
	ON up.UserId = s.CreatedBy
	INNER JOIN
	dbo.UserProfiles as up2
	ON up2.UserId = s.ModifiedBy

	ORDER BY s.Id

	OFFSET @offSet Rows
	Fetch Next @PageSize Rows ONLY

END
GO
/****** Object:  StoredProcedure [dbo].[Schedules_Update]    Script Date: 12/2/2022 3:13:23 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author: <Justin Solo>
-- Create date: <07/19/2022>
-- Description: <Schedules_Update>
-- Code Reviewer:

-- MODIFIED BY: author
-- MODIFIED DATE:12/1/2020
-- Code Reviewer:
-- Note:
-- =============================================
CREATE PROC [dbo].[Schedules_Update]
	@OrgId int
	,@CreatedBy int
	,@ModifiedBy int
	,@Id int
AS

BEGIN

	UPDATE [dbo].[Schedules]
	   SET [OrgId] = @OrgId
		  ,[CreatedBy] = @CreatedBy
		  ,[ModifiedBy] = @ModifiedBy
		  ,[DateModified] = GETUTCDATE()
	 WHERE @Id = Id

END


GO
/****** Object:  StoredProcedure [dbo].[ShareStory_DeleteById]    Script Date: 12/2/2022 3:13:23 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE proc [dbo].[ShareStory_DeleteById]
		@Id int
		
		
As
/*
Select *
		From dbo.sharestory
Declare @Id int =3

Execute dbo.ShareStory_DeleteById
							@Id
		Select *
		From dbo.sharestory

*/

Begin
	
	
Delete from dbo.ShareStory
Where Id=@Id



	
	
End
GO
/****** Object:  StoredProcedure [dbo].[ShareStory_Insert]    Script Date: 12/2/2022 3:13:23 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE proc [dbo].[ShareStory_Insert]
            @Name nvarchar(50)
           ,@Email nvarchar(50)
           ,@Story nvarchar(3000)
           ,@CreatedBy int
           ,@ListFileIds dbo.IntTable readonly
          ,@ShareStoryId int output
AS
BEGIN
/* --Test Code
Declare     @Name nvarchar(50) ='Juan'
            ,@Email nvarchar(50) ='juan@test.com'
            ,@Story nvarchar(3000) ='Story content story content'
			,@Url nvarchar(3000) =''
			,@CreatedBy int = 4
           ,@ShareStoryId int
		   ,@ListFileIds dbo.IntTable 

		   Insert into @ListFileIds values(1)
		   Insert into @ListFileIds values(2)
		   Insert into @ListFileIds values(3)

		   select * from @ListFileIds 
Execute dbo.[ShareStory_Insert] 
            @Name
            ,@Email
            ,@Story
			,@Url
            ,@CreatedBy
            ,@ListFileIds 
            ,@ShareStoryId output
            
Select * from sharestory order by DateCreated desc
            select * from files order by id desc;-- 469
            select * from ShareStory;-- 1
            select * from ShareStoryFile;-- 1
            select * from FileTypes;
--*/
--i commented it out last on 08/03--Declare @ShareStoryFileId int;
INSERT INTO [dbo].[ShareStory]
           ([Name]
           ,[Email]
           ,[Story]    
           ,[CreatedBy])
    
     VALUES
            (@Name 
           ,@Email 
           ,@Story    
           ,@CreatedBy         
            )
           SET @ShareStoryId = SCOPE_IDENTITY()

EXECUTE dbo.ShareStoryFile_Inserts_UDT 
                    @ShareStoryId 
                    ,@ListFileIds
END

GO
/****** Object:  StoredProcedure [dbo].[ShareStory_SelectAll]    Script Date: 12/2/2022 3:13:23 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE proc [dbo].[ShareStory_SelectAll]
			 @PageIndex int 
			,@PageSize int 

AS

/*
	Declare  @PageIndex int = 0
		    ,@PageSize int = 50

	Execute [dbo].[ShareStory_SelectAll]
			 @PageIndex
		    ,@PageSize

select * from dbo.sharestory
select * from dbo.files


*/

BEGIN

	DECLARE @Offset int = @PageIndex * @PageSize

Select distinct
		ss.Id,
		ss.Name as 'Story Name',
       ss.Email as 'Email',
	   ss.Story as 'Story',
	   ss.CreatedBy as 'CreatedBy',
	   (Select
	   F.[Id]
		,F.[Name]
		,F.[FileTypeId]
		,F.[Url]

		from dbo.Files as F inner join dbo.ShareStoryFile ssf

		on ssf.FileId = f.Id

		join FileTypes ft on f.FileTypeId = ft.Id
		where ssf.ShareStoryId =ss.id
		for json auto
		) as files 
	   

			,TotalCount = COUNT(1) OVER()
	From dbo.ShareStory as ss
inner join dbo.ShareStoryFile as sst on ss.Id = sst.ShareStoryId

	ORDER BY Id
	OFFSET @Offset ROWS
	FETCH NEXT @PageSize ROWS ONLY

END
GO
/****** Object:  StoredProcedure [dbo].[ShareStory_SelectByCreatedBy]    Script Date: 12/2/2022 3:13:23 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE  PROC [dbo].[ShareStory_SelectByCreatedBy]
				@CreatedBy int
				

AS 

/*----- Test Code -----

		Declare @CreatedBy int = 456
		Execute dbo.[ShareStory_SelectByCreatedBy] @CreatedBy

		Select *
		From dbo.ShareStory

----- End Test Code -----
*/

BEGIN


Select 
		ss.Id,
		ss.Name as 'Story Name',
       ss.Email as 'Email',
	   ss.Story as 'Story',
	   ss.CreatedBy as 'CreatedBy',
	   FT.Name as 'File Type',
	   F.Url as 'File URL',
	   U.Email as 'File Creator'
	   


From dbo.ShareStory as ss
inner join dbo.ShareStoryFile as sst on ss.Id = sst.ShareStoryId
inner join dbo.Files as F on F.Id=sst.FileId
inner join FileTypes FT on FT.Id=F.FileTypeId
inner join Users U on U.Id=F.CreatedBy
where  sst.ShareStoryId = ss.Id

End
GO
/****** Object:  StoredProcedure [dbo].[ShareStory_SelectById]    Script Date: 12/2/2022 3:13:23 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE proc [dbo].[ShareStory_SelectById]
		@Id int
AS
BEGIN

/* --Test Code

Declare @Id int =82
Execute [dbo].[ShareStory_SelectById] @Id

select *
from dbo.ShareStory
--*/
Select 
		ss.Id,
		ss.Name as 'Story Name',
       ss.Email as 'Email',
	   ss.Story as 'Story',
	   ss.CreatedBy as 'CreatedBy',
	   FT.Name as 'File Type',
	   F.Url as 'File URL',
	   U.Email as 'File Creator'
	   


From dbo.ShareStory as ss
inner join dbo.ShareStoryFile as sst on ss.Id = sst.ShareStoryId
inner join dbo.Files as F on F.Id=sst.FileId
inner join FileTypes FT on FT.Id=F.FileTypeId
inner join Users U on U.Id=F.CreatedBy
where  sst.ShareStoryId = @Id

End

GO
/****** Object:  StoredProcedure [dbo].[ShareStory_Update]    Script Date: 12/2/2022 3:13:23 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE proc [dbo].[ShareStory_Update]	
				@ShareStoryId int 
				,@Name nvarchar(50)
				,@Email nvarchar(50)
				,@Story nvarchar(3000)
				,@CreatedBy int
				,@ListFileIds dbo.IntTable readonly
				

				

As

Begin

/*----- Test Code -----
Execute dbo.[ShareStory_Update]	

@ShareStoryId=8
,@Name="Juan-new 
",@Email="juan-new@email.com3"
,@Story="This is my new story"
,@CreatedBy = 147


Select *
From dbo.ShareStory

----- End Test Code -----*/

Update dbo.ShareStory

Set				[Name]= @Name
				,[Email]=@Email
				,[Story]=@Story 
				,[DateModified] = GETDATE()
				,[CreatedBy]=@CreatedBy

				
where Id=@ShareStoryId


EXECUTE dbo.ShareStoryFile_Update_UDT 
						@ShareStoryId 
						,@ListFileIds
end
GO
/****** Object:  StoredProcedure [dbo].[ShareStoryFile_Inserts_UDT]    Script Date: 12/2/2022 3:13:23 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE proc [dbo].[ShareStoryFile_Inserts_UDT]
			@ShareStoryId int
			,@ListFileIds dbo.IntTable readonly
			
as
begin
/*

--Insert



*/

INSERT INTO [dbo].[ShareStoryFile]
           ([ShareStoryId]
           ,[FileId])
     Select 
           @ShareStoryId
           ,l.Data
		from @ListFileIds as l


END
GO
/****** Object:  StoredProcedure [dbo].[ShareStoryFile_SelectAll]    Script Date: 12/2/2022 3:13:23 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE Proc [dbo].[ShareStoryFile_SelectAll]
					@PageIndex int,
					@PageSize int

As

	/*==================TEST====================

		Declare @PageIndex int = 0
				,@PageSize int = 10

		Execute [dbo].[ShareStoryFile_SelectAll]	
				@PageIndex
				,@PageSize

				Select *
				from dbo.ShareStoryFile
	====================CODE==================== */

	Begin

			Declare  @offset int = @PageIndex * @PageSize

			Select		[ShareStoryId]
						,[FileId]
						,TotalCount = COUNT(1) OVER()
			From dbo.ShareStoryFile
			Order By [ShareStoryId]

			OFFSET @offset Rows
			Fetch Next @PageSize Rows ONLY
	End
GO
/****** Object:  StoredProcedure [dbo].[ShareStoryFile_Update_UDT]    Script Date: 12/2/2022 3:13:23 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE proc [dbo].[ShareStoryFile_Update_UDT]
			@ShareStoryId int
			,@ListFileIds dbo.IntTable readonly
			
			
as
begin/*


*/
--delete from dbo.ShareStoryFile
--where ShareStoryId = @ShareStoryId


INSERT INTO [dbo].[ShareStoryFile]
           ([ShareStoryId]
           ,[FileId])
     Select 
           @ShareStoryId
           ,l.Data
		from @ListFileIds as l





END
GO
/****** Object:  StoredProcedure [dbo].[SiteReferences_Insert]    Script Date: 12/2/2022 3:13:23 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO



CREATE proc [dbo].[SiteReferences_Insert]
								@ReferenceTypeId int
								,@UserId int


AS

/*.....TEST CODE.....

	Declare @ReferenceTypeId int = '3'  
			,@UserId int = 5;


	execute dbo.SiteReferences_Insert
								@ReferenceTypeId 
								,@UserId 

			Select * 
			from dbo.SiteReferences
			order by UserId
			
			select * 
			from dbo.ReferenceTypes

			select *
			from dbo.Users
		

*/


BEGIN
	

	INSERT INTO [dbo].[SiteReferences] 
								([ReferenceTypeId]
								,[UserId])
		
		VALUES (@ReferenceTypeId
					,@UserId) 

END


GO
/****** Object:  StoredProcedure [dbo].[SiteReferences_SelectAll]    Script Date: 12/2/2022 3:13:23 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
/****** Object:  StoredProcedure [dbo].[SiteReferences_SelectAll]    Script Date: 7/6/2022 10:26:21 AM ******/



CREATE PROC [dbo].[SiteReferences_SelectAll]
									@PageIndex int
									,@PageSize int
	
AS

/*.....TEST CODE.....

	Declare @PageIndex int = 0
			,@PageSize int = 10



execute dbo.SiteReferences_SelectAll 
								@PageIndex 
								,@PageSize
				
					select * from dbo.SiteReferences

					select * from dbo.ReferenceTypes
*/

BEGIN
	
	Declare @offset int = @PageIndex * @PageSize
	

		Select s.UserId
				,r.Name as Reference
				,TotalCount = COUNT(1) OVER() 
									
		from dbo.SiteReferences as s 
			inner join dbo.ReferenceTypes as r
				on s.ReferenceTypeId = r.Id
		 
		ORDER BY s.UserId

	OFFSET @offSet Rows
	Fetch Next @PageSize Rows ONLY

END

GO
/****** Object:  StoredProcedure [dbo].[SiteReferences_SelectAllChart]    Script Date: 12/2/2022 3:13:23 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO


CREATE PROC [dbo].[SiteReferences_SelectAllChart]
	
AS

/*.....TEST CODE.....


execute dbo.SiteReferences_SelectAllChart
				
					select * from dbo.SiteReferences

					select * from dbo.ReferenceTypes
*/

BEGIN
		
			SELECT
			(SELECT COUNT(ReferenceTypeId)
			FROM dbo.SiteReferences
			WHERE ReferenceTypeId = 1)  as Search,  

			(SELECT COUNT(ReferenceTypeId) 
			FROM dbo.SiteReferences
			WHERE ReferenceTypeId = 2) as Google,

			(SELECT COUNT(ReferenceTypeId) 
			FROM dbo.SiteReferences 
			WHERE ReferenceTypeId = 3) as Facebook,

			(SELECT COUNT(ReferenceTypeId) 
			FROM dbo.SiteReferences 
			WHERE ReferenceTypeId = 4) as OtherSocial,

			(SELECT COUNT(ReferenceTypeId) 
			FROM dbo.SiteReferences 
			WHERE ReferenceTypeId = 5) as Email,

			(SELECT COUNT(ReferenceTypeId) 
			FROM dbo.SiteReferences 
			WHERE ReferenceTypeId = 6) as WordOfMouth,

			(SELECT COUNT(ReferenceTypeId)
			FROM dbo.SiteReferences
			WHERE ReferenceTypeId = 7) as Recruiter,

			(SELECT COUNT(ReferenceTypeId) 
			FROM dbo.SiteReferences 
			WHERE ReferenceTypeId = 8) as JobFair,

			(SELECT COUNT(ReferenceTypeId)
			FROM dbo.SiteReferences
			WHERE ReferenceTypeId = 9)  as Other
			
		
END

GO
/****** Object:  StoredProcedure [dbo].[Skills_SelectAll]    Script Date: 12/2/2022 3:13:23 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author: <Grant Lee>
-- Create date: <08/01/2022>
-- Description: <Skills_SelectAll>
-- Code Reviewer: Morgan Frost 08/02/2022

-- MODIFIED BY: author
-- MODIFIED DATE: 8/16/2022
-- Code Reviewer:
-- Note:
-- =============================================

CREATE proc [dbo].[Skills_SelectAll]

as

/*------TEST CODE------

Execute [dbo].[Skills_SelectAll]

*/

BEGIN


SELECT sk.[Id]
      ,sk.[Name]


  FROM [dbo].[Skills] as sk
  ORDER BY sk.Id

END
GO
/****** Object:  StoredProcedure [dbo].[SpecializationTypes_SelectAll]    Script Date: 12/2/2022 3:13:23 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

-- =============================================
-- Author: <Ledwin Batista>
-- Create date: <07/27/2022>
-- Description: <SpecializationTypes_SelectAll>
-- Code Reviewer:

-- MODIFIED BY: author
-- MODIFIED DATE: 8/16/2022
-- Code Reviewer:
-- Note:
-- =============================================

CREATE PROC [dbo].[SpecializationTypes_SelectAll]

AS

/* -- TEST CODE --


EXECUTE [dbo].[SpecializationTypes_SelectAll] 
	
	
*/ -- END TEST CODE --

BEGIN

	SELECT 
			Id
			,[Name]

	FROM [dbo].[SpecializationTypes]

END
GO
/****** Object:  StoredProcedure [dbo].[States_SelectAll]    Script Date: 12/2/2022 3:13:23 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author: <Hyun Kim>
-- Create date: <07/11/2022>
-- Description: <States_SelectAll>
-- Code Reviewer:

-- MODIFIED BY: author
-- MODIFIED DATE:12/1/2020
-- Code Reviewer:
-- Note:
-- =============================================

CREATE proc [dbo].[States_SelectAll]
			--@PageIndex int
			--,@PageSize int

as


/* ----- TEST CODE -----

	Declare @PageIndex int = 0
			,@PageSize int = 55

	Execute [dbo].[States_SelectAll]
				@PageIndex 
				,@PageSize 

	Select * 
	from dbo.States

*/ ----- END TEST CODE -----

BEGIN

	--Declare @offset int = @PageIndex * @PageSize

SELECT s.[Id]
      ,s.[Code]
      ,s.[Name]
	  --,TotalCount = COUNT(1) OVER()
	
  FROM [dbo].[States] as s 
	order by s.[Code] asc

END


GO
/****** Object:  StoredProcedure [dbo].[SubscriptionTransactions_Insert]    Script Date: 12/2/2022 3:13:23 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author: <Tim Rawson>
-- Create date: <08/06/2022>
-- Description: <SubscriptionTransactions_Insert>
-- Code Reviewer: <Jordan Poole>

-- MODIFIED BY: --
-- MODIFIED DATE:12/1/2020
-- Code Reviewer:
-- Note:
-- =============================================

CREATE   PROC [dbo].[SubscriptionTransactions_Insert]
					@SubscriptionId int,
					@PaymentAcctId int,
					@UserId int,
					@PurchaseAmount smallmoney,
					@PurchaseDate datetime2,
					@StripeInvoiceId nvarchar(100),
					@StripeInvoiceUrl nvarchar(200),
					@StripeSubscriptionId nvarchar(100),
					@Id int OUTPUT

/*---------------Test Code--------------
	Declare @Id int = 1;

	Declare	@SubscriptionId int = 3,
			@PaymentAcctId int = 1,
			@UserId int = 17,
			@PurchaseAmount smallmoney = 10.00,
			@PurchaseDate datetime2 = '2022-08-06 16:48:04.00',
			@StripeInvoiceId nvarchar(100) = 'in_1LTqALCltgTQrEVzh1hjxTxo',
			@StripeInvoiceUrl nvarchar(200) = 'https://invoice.stripe.com/i/acct_1LGZ1HCltgTQrEVz/test_YWNjdF8xTEdaMUhDbHRnVFFyRVZ6LF9NQ0VkYkhYWXpWamZFejVBVGRjYklIRmpENzJubElFLDUwMzQ1Mjg00200Ljtnbl5l?s=ap',
			@StripeSubscriptionId nvarchar(100) = 'sub_1LTqALCltgTQrEVznUrSGNl9'


	EXECUTE [dbo].[SubscriptionTransactions_Insert]
			@SubscriptionId,
			@PaymentAcctId,
			@UserId,
			@PurchaseAmount,
			@PurchaseDate,
			@StripeInvoiceId,
			@StripeInvoiceUrl,
			@StripeSubscriptionId,
			@Id OUTPUT

	SELECT * From [dbo].[SubscriptionTransactions]

*/--------------End Test Code-----------
AS

BEGIN

INSERT INTO [dbo].[SubscriptionTransactions]
			([SubscriptionId],
			[PaymentAcctId],
			[UserId],
			[PurchaseAmount],
			[PurchaseDate],
			[StripeInvoiceId],
			[StripeInvoiceUrl],
			[StripeSubscriptionId])
	VALUES
			(@SubscriptionId ,
			@PaymentAcctId ,
			@UserId,
			@PurchaseAmount ,
			@PurchaseDate ,
			@StripeInvoiceId ,
			@StripeInvoiceUrl ,
			@StripeSubscriptionId)

	SET @Id = SCOPE_IDENTITY()

END

GO
/****** Object:  StoredProcedure [dbo].[SubscriptionTransactions_Insert_Cancellation]    Script Date: 12/2/2022 3:13:23 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author: <Tim Rawson>
-- Create date: <08/06/2022>
-- Description: <SubscriptionTransactions_Insert_Cancellation>
-- Code Reviewer: <Jordan Poole>

-- MODIFIED BY: --
-- MODIFIED DATE:12/1/2020
-- Code Reviewer:
-- Note:
-- =============================================

CREATE   PROC [dbo].[SubscriptionTransactions_Insert_Cancellation]
					@SubscriptionId int,
					@PaymentAcctId int,
					@UserId int,
					@CancellationDate datetime2,
					@RefundedAmount smallmoney,
					@StripeInvoiceId nvarchar(100),
					@StripeInvoiceUrl nvarchar(200),
					@StripeSubscriptionId nvarchar(100),
					@Id int OUTPUT

/*---------------Test Code--------------
	Declare @Id int = 2;

	Declare	@SubscriptionId int = 1,
			@PaymentAcctId int = 1,
			@UserId int = 17,
			@CancellationDate datetime2 = '2022-08-06 16:48:04.00',
			@RefundedAmount smallmoney = 10.00,
			@StripeInvoiceId nvarchar(100) = 'in_1LTqALCltgTQrEVzh1hjxTxo',
			@StripeInvoiceUrl nvarchar(200) = 'https://invoice.stripe.com/i/acct_1LGZ1HCltgTQrEVz/test_YWNjdF8xTEdaMUhDbHRnVFFyRVZ6LF9NQ0VkYkhYWXpWamZFejVBVGRjYklIRmpENzJubElFLDUwMzQ1Mjg00200Ljtnbl5l?s=ap',
			@StripeSubscriptionId nvarchar(100) = 'sub_1LTqALCltgTQrEVznUrSGNl9'


	EXECUTE [dbo].[SubscriptionTransactions_Insert_Cancellation]
			@SubscriptionId,
			@PaymentAcctId,
			@UserId,
			@CancellationDate,
			@RefundedAmount,
			@StripeInvoiceId,
			@StripeInvoiceUrl,
			@StripeSubscriptionId,
			@Id OUTPUT

	SELECT * From [dbo].[SubscriptionTransactions]

*/--------------End Test Code-----------
AS

BEGIN

INSERT INTO [dbo].[SubscriptionTransactions]
			([SubscriptionId],
			[PaymentAcctId],
			[UserId],
			[CancellationDate],
			[RefundedAmount],
			[StripeInvoiceId],
			[StripeInvoiceUrl],
			[StripeSubscriptionId])
	VALUES
			(@SubscriptionId ,
			@PaymentAcctId ,
			@UserId,
			@CancellationDate ,
			@RefundedAmount ,
			@StripeInvoiceId ,
			@StripeInvoiceUrl ,
			@StripeSubscriptionId)

	SET @Id = SCOPE_IDENTITY()

END

GO
/****** Object:  StoredProcedure [dbo].[Survey_Submit]    Script Date: 12/2/2022 3:13:23 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author: <Allen Luong>
-- Create date: <9/17/2022>
-- Description: <SurveySubmit INSERT into dbo.SurveyAnswers and dbo.SurveyInstaces>
-- Code Reviewer:

-- MODIFIED BY: author
-- MODIFIED DATE:12/1/2020
-- Code Reviewer:
-- Note:
-- ==============================================

CREATE proc [dbo].[Survey_Submit]
            --Instance Insert
			 @SurveyId int
			,@StatusId int
			,@UserId int
			
           
		   --surveyanswers insert
			,@batchAnswers dbo.BatchSurveyAnswers READONLY
			
             ,@InstanceId int  output
            
as

/* ------ TEST CODE ------

Declare  @SurveyId int = 3
		,@StatusId int = 1
		,@UserId int = 244
		

Declare @batchAnswers dbo.BatchSurveyAnswers
		Insert into @batchAnswers(QuestionId, AnswerOptionId, Answer, AnswerNumber)
		Values
		(13, Null, Null, 1), 
		(16, 28, Null, Null), 
		(17, 30, Null, Null), 
		(17, 37, Null, Null),
		(18, Null, 'iPhone 12', Null)


Execute dbo.Survey_Submit

         @SurveyId 
		,@StatusId 
		,@UserId 
		,@batchAnswers

SELECT *
FROM dbo.SurveyInstances

SELECT *
FROM dbo.SurveyAnswers

*/

BEGIN


INSERT INTO [dbo].SurveyInstances
			(
				SurveyId
			   ,StatusId
			   ,UserId
			)

VALUES		(
				 @SurveyId
				,@StatusId
				,@UserId
			)

			SET @InstanceId = SCOPE_IDENTITY()
		

INSERT INTO dbo.SurveyAnswers
		(
			 InstanceId
			,QuestionId
			,AnswerOptionId 
			,Answer 
			,AnswerNumber 
		)
	SELECT 	 @InstanceId
			,b.QuestionId
			,b.AnswerOptionId
			,b.Answer
			,b.AnswerNumber
			
		
	FROM	@batchAnswers as b
			
		

END

GO
/****** Object:  StoredProcedure [dbo].[SurveyAnswers_DELETE]    Script Date: 12/2/2022 3:13:23 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Allen Luong C-119>
-- Create date: <8/26/2022>
-- Description:	<DELETE for dbo.SurveysAnswers>
-- Code Reviewer:


-- MODIFIED BY: author
-- MODIFIED DATE:12/1/2020
-- Code Reviewer: 
-- Note: 
-- =============================================
CREATE proc [dbo].[SurveyAnswers_DELETE]
	@Id int
as

/*

			Declare @Id int = 2

			Select * 
			From dbo.SurveyAnswers
			WHERE Id = @Id


			Execute  dbo.SurveyAnswers_DELETE @Id

			Select * 
			From dbo.SurveyAnswers
			WHERE Id = @Id

*/

BEGIN

DELETE FROM dbo.SurveyAnswers
Where Id = @Id
END


GO
/****** Object:  StoredProcedure [dbo].[SurveyAnswers_Insert]    Script Date: 12/2/2022 3:13:23 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author: <Allen Luong>
-- Create date: <8/26/2022>
-- Description: <INSERT into dbo.SurveyAnswers>
-- Code Reviewer:

-- MODIFIED BY: author
-- MODIFIED DATE:12/1/2020
-- Code Reviewer:
-- Note:
-- ==============================================

CREATE proc [dbo].[SurveyAnswers_Insert]
            @InstanceId int
           ,@QuestionId int
           ,@AnswerOptionId int
           ,@Answer nvarchar(500)
           ,@AnswerNumber int
           ,@Id int OUTPUT
as

/* ------ TEST CODE ------

Declare
			@InstanceId int = 1
           ,@QuestionId int = 1
           ,@AnswerOptionId int = 2
           ,@Answer nvarchar(500) = 'Test Answer for SurveyAnswers'
           ,@AnswerNumber int = 0
           ,@Id int = 0

Execute dbo.SurveyAnswers_Insert

     
            @InstanceId
           ,@QuestionId
           ,@AnswerOptionId 
           ,@Answer 
           ,@AnswerNumber 
           ,@Id OUTPUT

SELECT *
FROM dbo.SurveyAnswers
*/

BEGIN

INSERT INTO [dbo].[SurveyAnswers]
			(
				[InstanceId]
			   ,[QuestionId]
			   ,[AnswerOptionId]
			   ,[Answer]
			   ,[AnswerNumber]
			)

VALUES
		(
			@InstanceId
           ,@QuestionId
           ,@AnswerOptionId 
           ,@Answer 
           ,@AnswerNumber 
		)
		SET @Id = SCOPE_IDENTITY()

END

GO
/****** Object:  StoredProcedure [dbo].[SurveyAnswers_Update]    Script Date: 12/2/2022 3:13:23 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

-- =============================================
-- Author:		<Allen Luong C-119>
-- Create date: <8/24/2022>
-- Description:	<UPDATE for dbo.Surveys>
-- Code Reviewer:


-- MODIFIED BY: author
-- MODIFIED DATE:12/1/2020
-- Code Reviewer: 
-- Note: 
-- =============================================


CREATE proc [dbo].[SurveyAnswers_Update]
            @InstanceId int
           ,@QuestionId int
           ,@AnswerOptionId int
           ,@Answer nvarchar(500)
           ,@AnswerNumber int
           ,@Id int OUTPUT
as

/* ----TEST CODE----

Declare		@Id int = 3           

Declare
			@InstanceId int = 1
           ,@QuestionId int = 1
           ,@AnswerOptionId int = 3
           ,@Answer nvarchar(500) = 'Test Update for SurveyAnswers'
           ,@AnswerNumber int = 5
		 

EXECUTE dbo.SurveyAnswers_Update

            @InstanceId 
           ,@QuestionId 
           ,@AnswerOptionId 
           ,@Answer 
           ,@AnswerNumber 
           ,@Id 

SELECT *
from dbo.surveyAnswers

*/

BEGIN

Declare @DateModified datetime2 = getutcdate();

UPDATE [dbo].[SurveyAnswers]
   SET InstanceId = @InstanceId
      ,QuestionId = @QuestionId
      ,AnswerOptionId = @AnswerOptionId
      ,Answer = @Answer
      ,AnswerNumber = @AnswerNumber
		,DateModified = @DateModified


	WHERE Id = @Id
END

GO
/****** Object:  StoredProcedure [dbo].[SurveyInstances_Insert]    Script Date: 12/2/2022 3:13:23 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROC [dbo].[SurveyInstances_Insert]
-- =============================================
-- Author: Michael Shepard
-- Create date: 26 August 2022
-- Description: Inserts into SurveyInstances table
-- Code Reviewer:

-- MODIFIED BY: author
-- MODIFIED DATE:
-- Code Reviewer:
-- Note:
-- ==============================================
		@SurveyId int
		,@StatusId int
		,@UserId int
		,@Id int OUTPUT
as
/*
DECLARE @SurveyId int = 4
		,@StatusId int = 1
		,@UserId int = 118
		,@Id int

EXECUTE dbo.SurveyInstances_Insert
		@SurveyId
		,@StatusId
		,@UserId
		,@Id OUTPUT

SELECT *
	FROM dbo.SurveyInstances
	WHERE @Id = Id

*/
BEGIN

DECLARE @DatNow datetime2(7) = GETUTCDATE()

INSERT INTO dbo.SurveyInstances
			(SurveyId
			,StatusId
			,UserId
			,DateCreated
			,DateModified)
	VALUES
			(@SurveyId
			,@StatusId
			,@UserId
			,@DatNow
			,@DatNow)
	SET		@Id = SCOPE_IDENTITY()

END
GO
/****** Object:  StoredProcedure [dbo].[SurveyInstances_SearchBySurvey]    Script Date: 12/2/2022 3:13:23 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROC [dbo].[SurveyInstances_SearchBySurvey]
-- =============================================
-- Author: Michael Shepard
-- Create date: 13 September 2022
-- Description: selects all response data for 
--				 surveys matching the query, returning questions,
--				answers, and answer frequency
-- Code Reviewer: Vincent Goitortua

-- MODIFIED BY: 
-- MODIFIED DATE: 
-- Code Reviewer:
-- Note: 
-- ==============================================
@PageIndex int 
,@PageSize int
,@Query nvarchar(128)

as
/*

DECLARE @PageIndex int = 0
		,@PageSize int = 2
		,@Query nvarchar(128) = 'a'
EXECUTE dbo.SurveyInstances_SearchBySurvey @PageIndex ,@PageSize ,@Query

*/
BEGIN

DECLARE @offset int = @PageIndex * @PageSize

SELECT DISTINCT
	su.Id as SurveyId
	,su.[Name] as SurveyName
	,Instances = 
			(
			SELECT COUNT(*)
			FROM dbo.SurveyInstances as si
			WHERE si.SurveyId = su.Id
			)
	,su.[Description] as [Description]
	,su.CompanyLogo as Logo
	,su.DateCreated as DateCreated
	,u.Id as CreatedBy
	,up.FirstName as CreatorFirstName
	,up.LastName as CreatorLastName
	,InsightData = 
				(
				SELECT DISTINCT
				sq.Id as 'Question.Id'
				,sq.Question as 'Question.Question'
				,qt.Id as 'Question.QuestionTypeId'
				,qt.[Name] as 'Question.QuestionTypeName'
				,sq.SortOrder as 'Question.SortOrder'
				,Answers =
					json_query(CASE
						WHEN qt.Id = 2 OR qt.Id = 3 THEN
							(
							SELECT DISTINCT
								san.AnswerOptionId as OptionId
								,coalesce(sqao.[Text], sqao.[Value]) as AnswerString
								,AnswerBinary = NULL
								,Total = COUNT(*) 
							FROM dbo.SurveyAnswers AS san
							inner join dbo.SurveyQuestionAnswerOptions as sqao on san.AnswerOptionId = sqao.Id
							WHERE san.QuestionId = sq.Id
							GROUP BY san.AnswerOptionId, sqao.[Text], sqao.[Value]
							FOR JSON AUTO
							)
						WHEN qt.Id = 10 THEN
							(
							SELECT DISTINCT
								san.AnswerOptionId as OptionId
								,san.Answer as AnswerString
								,san.AnswerNumber as AnswerBinary
								,Total = COUNT(*) 
							FROM dbo.SurveyAnswers AS san
							WHERE san.QuestionId = sq.Id
							GROUP BY san.AnswerNumber, san.AnswerOptionId, san.Answer
							FOR JSON AUTO
							)
						WHEN qt.Id = 1 THEN
							(
							SELECT DISTINCT
								san.AnswerOptionId as OptionId
								,san.Answer as AnswerString
								,san.AnswerNumber as AnswerBinary
								,Total = NULL
							FROM dbo.SurveyAnswers AS san
							WHERE san.QuestionId = sq.Id
							FOR JSON AUTO
							)
					END)
				FROM dbo.SurveyQuestions as sq
				inner join dbo.Surveys as s on sq.SurveyId = s.Id
				inner join dbo.QuestionTypes as qt on sq.QuestionTypeId = qt.Id
				inner join dbo.SurveyInstances as si on si.SurveyId = su.Id
				WHERE si.SurveyId = s.Id AND sq.SurveyId = s.Id AND su.Id = s.Id
				ORDER BY qt.Name
				FOR JSON PATH
				)
	,TotalCount = COUNT(1) OVER()
FROM dbo.Surveys as su
	inner join dbo.Users as u on su.CreatedBy = u.Id
	inner join dbo.UserProfiles as up on u.Id = up.UserId
WHERE su.[Name] LIKE '%' + @Query + '%' OR su.[Description] LIKE '%' + @Query + '%'
ORDER BY DateCreated DESC

OFFSET @offSet Rows
FETCH NEXT @PageSize Rows ONLY


END
GO
/****** Object:  StoredProcedure [dbo].[SurveyInstances_SearchPaginate]    Script Date: 12/2/2022 3:13:23 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROC [dbo].[SurveyInstances_SearchPaginate]
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
EXECUTE dbo.[SurveyInstances_SearchPaginate] @PageIndex, @PageSize, @Query

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
						WHERE si.Id = sa.InstanceId
						FOR JSON PATH
						)
					,sa.Answer AS OpenEndedAnswer
					,sa.AnswerNumber AS BinaryAnswer
					,AnswerOptions.[Text] AS AnswerOptionText
					,AnswerOptions.[Value] AS AnswerOptionValue
				FROM dbo.SurveyQuestions as sq 
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
GO
/****** Object:  StoredProcedure [dbo].[SurveyInstances_SearchPaginateV2]    Script Date: 12/2/2022 3:13:23 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROC [dbo].[SurveyInstances_SearchPaginateV2]
-- =============================================
-- Author: Michael Shepard
-- Create date: 29 August 2022
-- Description: selects data for all instances
--				of a survey that match a 
--				given query
-- Code Reviewer: Rebekah Hoskins

-- MODIFIED BY: author
-- MODIFIED DATE: 12 September 2022
-- Code Reviewer: Allen Luong
-- Note:add question type id and name columns
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
							,sq.SortOrder as QuestionSortOrder
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
GO
/****** Object:  StoredProcedure [dbo].[SurveyInstances_SearchPaginateV3]    Script Date: 12/2/2022 3:13:23 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROC [dbo].[SurveyInstances_SearchPaginateV3]
-- =============================================
-- Author: Michael Shepard
-- Create date: 29 August 2022
-- Description: selects data for all instances
--				of a survey that match a 
--				given query
-- Code Reviewer: Rebekah Hoskins

-- MODIFIED BY: author
-- MODIFIED DATE: 12 September 2022
-- Code Reviewer: Allen Luong
-- Note:add question type id and name columns
-- ==============================================
		 @PageIndex int 
		,@PageSize int
		,@Query int
as
/*

DECLARE @PageIndex int = 0
		,@PageSize int = 7
		,@Query int = 2
EXECUTE dbo.[SurveyInstances_SearchPaginateV3] @PageIndex, @PageSize, @Query

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
						WHERE sa.QuestionId = sq.Id
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
				WHERE sa.QuestionId = sq.Id
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
			WHERE si.Id = sa.InstanceId  AND  s.Id = @Query

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
GO
/****** Object:  StoredProcedure [dbo].[SurveyInstances_SelectAll]    Script Date: 12/2/2022 3:13:23 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROC [dbo].[SurveyInstances_SelectAll]
-- =============================================
-- Author: Michael Shepard
-- Create date: 27 August 2022
-- Description: selects data for all instances
--		from SurveyInstances, SurveyQuestions,
--		 SurveyQuestionsAnswerOptions,
--		SurveyAnswers, Surveys, Users tables
-- Code Reviewer: Rebekah Hoskins

-- MODIFIED BY: author
-- MODIFIED DATE:
-- Code Reviewer:
-- Note:
-- ==============================================
as
/*
EXECUTE dbo.SurveyInstances_SelectAll
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
						WHERE si.Id = sa.InstanceId
						FOR JSON PATH
						)
					,sa.Answer AS OpenEndedAnswer
					,sa.AnswerNumber AS BinaryAnswer
					,AnswerOptions.[Text] AS AnswerOptionText
					,AnswerOptions.[Value] AS AnswerOptionValue
				FROM dbo.SurveyQuestions as sq 
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


END
GO
/****** Object:  StoredProcedure [dbo].[SurveyInstances_SelectAllBriefPaginate]    Script Date: 12/2/2022 3:13:23 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROC [dbo].[SurveyInstances_SelectAllBriefPaginate]
-- =============================================
-- Author: Michael Shepard
-- Create date: 27 August 2022
-- Description: selects brief data for all instances
--		from SurveyInstances, SurveyQuestions,
--		 SurveyQuestionsAnswerOptions,
--		SurveyAnswers, Surveys, Users tables
--		in paginated form
-- Code Reviewer:

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
		,@PageSize int = 25
EXECUTE dbo.SurveyInstances_SelectAllBriefPaginate @PageIndex, @PageSize
*/
BEGIN

DECLARE @offset int = @PageIndex * @PageSize

SELECT 
		si.Id
		,u.Id as UserId
		,s.[Name] as SurveyName
		,st.[Name] as SurveyType
		,stat.[Name] as InstanceStatus
		,si.DateCreated as DateTaken
		,TotalCount = COUNT(1) OVER()

FROM dbo.SurveyInstances as si
	inner join dbo.Users as u on si.UserId = u.Id
	inner join dbo.Surveys as s on si.SurveyId = s.Id
	inner join dbo.SurveyStatus as stat on si.StatusId = stat.Id
	inner join dbo.SurveyTypes as st on s.SurveyTypeId = st.Id
ORDER BY si.SurveyId

OFFSET @offSet Rows
FETCH NEXT @PageSize Rows ONLY

END
GO
/****** Object:  StoredProcedure [dbo].[SurveyInstances_SelectAllByIdPaginate]    Script Date: 12/2/2022 3:13:23 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROC [dbo].[SurveyInstances_SelectAllByIdPaginate]
-- =============================================
-- Author: Michael Shepard
-- Create date: 22 September 2022
-- Description: selects data for all instances
--		of a given survey
--		from SurveyInstances, SurveyQuestions,
--		 SurveyQuestionsAnswerOptions,
--		SurveyAnswers, Surveys, Users tables
--		in paginated form
-- Code Reviewer: 

-- MODIFIED BY: author
-- MODIFIED DATE: 
-- Code Reviewer: 
-- ==============================================
		 @PageIndex int 
		,@PageSize int
		,@Id int
as
/*
DECLARE @PageIndex int = 0
		,@PageSize int = 10
		,@Id int = 1
EXECUTE dbo.SurveyInstances_SelectAllByIdPaginate @PageIndex, @PageSize, @Id
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
							,sq.SortOrder as QuestionSortOrder
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
				WHERE si.Id = sa.InstanceId and si.SurveyId = @Id
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
	Where s.Id = @Id
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
GO
/****** Object:  StoredProcedure [dbo].[SurveyInstances_SelectAllBySurvey]    Script Date: 12/2/2022 3:13:23 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROC [dbo].[SurveyInstances_SelectAllBySurvey]
-- =============================================
-- Author: Michael Shepard
-- Create date: 13 September 2022
-- Description: selects all response data for 
--				all surveys, returning questions,
--				answers, and answer frequency
-- Code Reviewer: 

-- MODIFIED BY: 
-- MODIFIED DATE: 
-- Code Reviewer:
-- Note: 
-- ==============================================


as
/*


EXECUTE dbo.SurveyInstances_SelectAllBySurvey

*/
BEGIN

SELECT 
	su.Id as SurveyId
	,su.[Name] as SurveyName
	,Instances = 
			(
			SELECT COUNT(*)
			FROM dbo.SurveyInstances as si
			WHERE si.SurveyId = su.Id
			)
	,su.[Description] as [Description]
	,su.CompanyLogo as Logo
	,su.DateCreated as DateCreated
	,u.Id as CreatedBy
	,up.FirstName as CreatorFirstName
	,up.LastName as CreatorLastName
	,InsightData = 
				(
				SELECT
				sq.Id as 'Question.Id'
				,sq.Question as 'Question.Question'
				,qt.Id as 'Question.QuestionTypeId'
				,qt.[Name] as 'Question.QuestionTypeName'
				,sq.SortOrder as 'Question.SortOrder'
				,Answers =
					json_query(CASE
						WHEN qt.Id = 2 OR qt.Id = 3 THEN
							(
							SELECT 
								san.AnswerOptionId as OptionId
								,coalesce(sqao.[Text], sqao.[Value]) as AnswerString
								,AnswerBinary = NULL
								,Total = COUNT(*) 
							FROM dbo.SurveyAnswers AS san
							inner join dbo.SurveyQuestionAnswerOptions as sqao on san.AnswerOptionId = sqao.Id
							WHERE san.QuestionId = sq.Id
							GROUP BY san.AnswerOptionId, sqao.[Text], sqao.[Value]
							FOR JSON AUTO
							)
						WHEN qt.Id = 10 THEN
							(
							SELECT 
								san.AnswerOptionId as OptionId
								,san.Answer as AnswerString
								,san.AnswerNumber as AnswerBinary
								,Total = COUNT(*) 
							FROM dbo.SurveyAnswers AS san
							WHERE san.QuestionId = sq.Id
							GROUP BY san.AnswerNumber, san.AnswerOptionId, san.Answer
							FOR JSON AUTO
							)
						WHEN qt.Id = 1 THEN
							(
							SELECT 
								san.AnswerOptionId as OptionId
								,san.Answer as AnswerString
								,san.AnswerNumber as AnswerBinary
								,Total = NULL
							FROM dbo.SurveyAnswers AS san
							WHERE san.QuestionId = sq.Id
							FOR JSON AUTO
							)
					END)
				FROM dbo.SurveyQuestions as sq
				inner join dbo.Surveys as s on sq.SurveyId = s.Id
				inner join dbo.QuestionTypes as qt on sq.QuestionTypeId = qt.Id
				inner join dbo.SurveyInstances as si on si.SurveyId = su.Id
				WHERE si.SurveyId = s.Id AND sq.SurveyId = s.Id
				ORDER BY qt.Name
				FOR JSON PATH
				)
FROM dbo.Surveys as su
	inner join dbo.Users as u on su.CreatedBy = u.Id
	inner join dbo.UserProfiles as up on u.Id = up.UserId
ORDER BY DateCreated DESC


END
GO
/****** Object:  StoredProcedure [dbo].[SurveyInstances_SelectAllBySurveyPaginate]    Script Date: 12/2/2022 3:13:23 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROC [dbo].[SurveyInstances_SelectAllBySurveyPaginate]
-- =============================================
-- Author: Michael Shepard
-- Create date: 13 September 2022
-- Description: selects all response data for 
--				all surveys paginated, returning questions,
--				answers, and answer frequency
-- Code Reviewer: Vincent Goitortua

-- MODIFIED BY: 
-- MODIFIED DATE: 
-- Code Reviewer:
-- Note: 
-- ==============================================
	@PageIndex int 
	,@PageSize int
as
/*
DECLARE @PageIndex int = 0
		,@PageSize int = 2

EXECUTE dbo.SurveyInstances_SelectAllBySurveyPaginate @PageIndex, @PageSize

*/
BEGIN

DECLARE @offset int = @PageIndex * @PageSize

SELECT 
	su.Id as SurveyId
	,su.[Name] as SurveyName
	,Instances = 
			(
			SELECT COUNT(*)
			FROM dbo.SurveyInstances as si
			WHERE si.SurveyId = su.Id
			)
	,su.[Description] as [Description]
	,su.CompanyLogo as Logo
	,su.DateCreated as DateCreated
	,u.Id as CreatedBy
	,up.FirstName as CreatorFirstName
	,up.LastName as CreatorLastName
	,InsightData = 
				(
				SELECT DISTINCT
				sq.Id as 'Question.Id'
				,sq.Question as 'Question.Question'
				,qt.Id as 'Question.QuestionTypeId'
				,qt.[Name] as 'Question.QuestionTypeName'
				,sq.SortOrder as 'Question.SortOrder'
				,Answers =
					json_query(CASE
						WHEN qt.Id = 2 OR qt.Id = 3 THEN
							(
							SELECT DISTINCT
								san.AnswerOptionId as OptionId
								,coalesce(sqao.[Text], sqao.[Value]) as AnswerString
								,AnswerBinary = NULL
								,Total = COUNT(*) 
							FROM dbo.SurveyAnswers AS san
							inner join dbo.SurveyQuestionAnswerOptions as sqao on san.AnswerOptionId = sqao.Id
							WHERE san.QuestionId = sq.Id
							GROUP BY san.AnswerOptionId, sqao.[Text], sqao.[Value]
							FOR JSON AUTO
							)
						WHEN qt.Id = 10 THEN
							(
							SELECT DISTINCT
								san.AnswerOptionId as OptionId
								,san.Answer as AnswerString
								,san.AnswerNumber as AnswerBinary
								,Total = COUNT(*) 
							FROM dbo.SurveyAnswers AS san
							WHERE san.QuestionId = sq.Id
							GROUP BY san.AnswerNumber, san.AnswerOptionId, san.Answer
							FOR JSON AUTO
							)
						WHEN qt.Id = 1 THEN
							(
							SELECT DISTINCT
								san.AnswerOptionId as OptionId
								,san.Answer as AnswerString
								,san.AnswerNumber as AnswerBinary
								,Total = NULL
							FROM dbo.SurveyAnswers AS san
							WHERE san.QuestionId = sq.Id
							FOR JSON AUTO
							)
					END)
				FROM dbo.SurveyQuestions as sq
				inner join dbo.Surveys as s on sq.SurveyId = s.Id
				inner join dbo.QuestionTypes as qt on sq.QuestionTypeId = qt.Id
				inner join dbo.SurveyInstances as si on s.Id = si.SurveyId
				WHERE sq.SurveyId = s.Id AND s.Id = si.SurveyId AND s.Id = su.Id
				ORDER BY qt.Name
				FOR JSON PATH
				)
	,TotalCount = COUNT(1) OVER()
FROM dbo.Surveys as su
	inner join dbo.Users as u on su.CreatedBy = u.Id
	inner join dbo.UserProfiles as up on u.Id = up.UserId
ORDER BY DateCreated DESC

OFFSET @offSet Rows
FETCH NEXT @PageSize Rows ONLY

END
GO
/****** Object:  StoredProcedure [dbo].[SurveyInstances_SelectAllPaginate]    Script Date: 12/2/2022 3:13:23 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROC [dbo].[SurveyInstances_SelectAllPaginate]
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
EXECUTE dbo.SurveyInstances_SelectAllPaginate @PageIndex, @PageSize
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
						WHERE si.Id = sa.InstanceId
						FOR JSON PATH
						)
					,sa.Answer AS OpenEndedAnswer
					,sa.AnswerNumber AS BinaryAnswer
					,AnswerOptions.[Text] AS AnswerOptionText
					,AnswerOptions.[Value] AS AnswerOptionValue
				FROM dbo.SurveyQuestions as sq 
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
GO
/****** Object:  StoredProcedure [dbo].[SurveyInstances_SelectAllPaginateV2]    Script Date: 12/2/2022 3:13:23 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROC [dbo].[SurveyInstances_SelectAllPaginateV2]
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
-- MODIFIED DATE: 12 September 2022
-- Code Reviewer: Allen Luong
-- Note:add question type id and name columns
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
							,sq.SortOrder as QuestionSortOrder
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
GO
/****** Object:  StoredProcedure [dbo].[SurveyInstances_SelectAllV2]    Script Date: 12/2/2022 3:13:23 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROC [dbo].[SurveyInstances_SelectAllV2]
-- =============================================
-- Author: Michael Shepard
-- Create date: 27 August 2022
-- Description: selects data for all instances
--		from SurveyInstances, SurveyQuestions,
--		 SurveyQuestionsAnswerOptions,
--		SurveyAnswers, Surveys, Users tables
-- Code Reviewer: Rebekah Hoskins

-- MODIFIED BY: author
-- MODIFIED DATE: 12 September 2022
-- Code Reviewer: Allen Luong
-- Note:add question type id and name columns
-- ==============================================
as
/*
EXECUTE dbo.SurveyInstances_SelectAllV2
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
							,sq.SortOrder as QuestionSortOrder
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


END
GO
/****** Object:  StoredProcedure [dbo].[SurveyInstances_SelectByInstanceId]    Script Date: 12/2/2022 3:13:23 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROC [dbo].[SurveyInstances_SelectByInstanceId]
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
EXECUTE dbo.SurveyInstances_SelectByInstanceId @Id
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
						WHERE si.Id = sa.InstanceId
						FOR JSON PATH
						)
					,sa.Answer AS OpenEndedAnswer
					,sa.AnswerNumber AS BinaryAnswer
					,AnswerOptions.[Text] AS AnswerOptionText
					,AnswerOptions.[Value] AS AnswerOptionValue
				FROM dbo.SurveyQuestions as sq 
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
GO
/****** Object:  StoredProcedure [dbo].[SurveyInstances_SelectByInstanceIdV2]    Script Date: 12/2/2022 3:13:23 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROC [dbo].[SurveyInstances_SelectByInstanceIdV2]
-- =============================================
-- Author: Michael Shepard
-- Create date: 26 August 2022
-- Description: selects data for one survey instance 
--      from SurveyInstances, SurveyQuestions, 
--		SurveyQuestionsAnswerOptions,
--		SurveyAnswers, Surveys, Users tables
-- Code Reviewer: Rebekah Hoskins

-- MODIFIED BY: author
-- MODIFIED DATE: 9 September 2022
-- Code Reviewer: Allen Luong
-- Note: add question type id and name columns
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
							,sq.SortOrder as QuestionSortOrder
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
GO
/****** Object:  StoredProcedure [dbo].[SurveyInstances_SelectBySurveyId]    Script Date: 12/2/2022 3:13:23 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROC [dbo].[SurveyInstances_SelectBySurveyId]
-- =============================================
-- Author: Michael Shepard
-- Create date: 13 September 2022
-- Description: selects all response data for 
--				a given survey, returning questions,
--				answers, and answer frequency
-- Code Reviewer: Vincent Goitortua

-- MODIFIED BY: 
-- MODIFIED DATE: 
-- Code Reviewer:
-- Note: 
-- ==============================================

@Id int

as
/*

DECLARE @Id int = 2
EXECUTE dbo.SurveyInstances_SelectBySurveyId @Id

*/
BEGIN

SELECT 
	su.Id as SurveyId
	,su.[Name] as SurveyName
	,Instances = 
			(
			SELECT COUNT(*)
			FROM dbo.SurveyInstances as si
			WHERE si.SurveyId = @Id
			)
	,su.[Description] as [Description]
	,su.CompanyLogo as Logo
	,su.DateCreated as DateCreated
	,u.Id as CreatedBy
	,up.FirstName as CreatorFirstName
	,up.LastName as CreatorLastName
	,InsightData = 
				(
				SELECT
				sq.Id as 'Question.Id'
				,sq.Question as 'Question.Question'
				,qt.Id as 'Question.QuestionTypeId'
				,qt.[Name] as 'Question.QuestionTypeName'
				,sq.SortOrder as 'Question.SortOrder'
				,Answers =
					json_query(CASE
						WHEN qt.Id = 2 OR qt.Id = 3 THEN
							(
							SELECT 
								san.AnswerOptionId as OptionId
								,coalesce(sqao.[Text], sqao.[Value]) as AnswerString
								,AnswerBinary = NULL
								,Total = COUNT(*) 
							FROM dbo.SurveyAnswers AS san
							inner join dbo.SurveyQuestionAnswerOptions as sqao on san.AnswerOptionId = sqao.Id
							WHERE san.QuestionId = sq.Id
							GROUP BY san.AnswerOptionId, sqao.[Text], sqao.[Value]
							FOR JSON AUTO
							)
						WHEN qt.Id = 10 THEN
							(
							SELECT 
								san.AnswerOptionId as OptionId
								,san.Answer as AnswerString
								,san.AnswerNumber as AnswerBinary
								,Total = COUNT(*) 
							FROM dbo.SurveyAnswers AS san
							WHERE san.QuestionId = sq.Id
							GROUP BY san.AnswerNumber, san.AnswerOptionId, san.Answer
							FOR JSON AUTO
							)
						WHEN qt.Id = 1 THEN
							(
							SELECT 
								san.AnswerOptionId as OptionId
								,san.Answer as AnswerString
								,san.AnswerNumber as AnswerBinary
								,Total = NULL
							FROM dbo.SurveyAnswers AS san
							WHERE san.QuestionId = sq.Id
							FOR JSON AUTO
							)
					END)
				FROM dbo.SurveyQuestions as sq
				inner join dbo.Surveys as s on sq.SurveyId = s.Id
				inner join dbo.QuestionTypes as qt on sq.QuestionTypeId = qt.Id
				WHERE @Id = s.Id
				ORDER BY sq.SortOrder
				FOR JSON PATH
				)
FROM dbo.Surveys as su
	inner join dbo.Users as u on su.CreatedBy = u.Id
	inner join dbo.UserProfiles as up on u.Id = up.UserId
WHERE @Id = su.Id


END
GO
/****** Object:  StoredProcedure [dbo].[SurveyInstances_Update]    Script Date: 12/2/2022 3:13:23 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROC [dbo].[SurveyInstances_Update]
-- =============================================
-- Author: Michael Shepard
-- Create date: 26 August 2022
-- Description: Updates the status of an entry in
--				the SurveyInstances table
-- Code Reviewer:

-- MODIFIED BY: author
-- MODIFIED DATE:
-- Code Reviewer:
-- Note:
-- ==============================================

		@Id int
		,@StatusId int

as
/*
DECLARE @Id int = 
		,@StatusId = 

SELECT *
	FROM dbo.SurveyInstances
	WHERE @Id = Id

EXECUTE dbo.SurveyInstances_Update
		@Id
		,@StatusId

SELECT *
	FROM dbo.SurveyInstances
	WHERE @Id = Id

*/
BEGIN

DECLARE @DatNow datetime2(7) = GETUTCDATE()

UPDATE dbo.SurveyInstances
	SET DateModified = @DatNow
		,StatusId = @StatusId
	WHERE @Id = Id



END
GO
/****** Object:  StoredProcedure [dbo].[SurveyQuestionAnswerOptions_Delete]    Script Date: 12/2/2022 3:13:23 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROC [dbo].[SurveyQuestionAnswerOptions_Delete]
-- =============================================
-- Author:		Michael Shepard
-- Create date: 24 August 2022
-- Description:	deletes from the survey question answer option table
-- Code Reviewer: Ron Busnardo
-- ==============================================

	@Id int

as

/*
DECLARE	@Id int = 7

SELECT	*
	FROM dbo.SurveyQuestionAnswerOptions
	WHERE [Id] = @Id

EXECUTE dbo.SurveyQuestionAnswerOptions_Delete
		@Id

SELECT	*
	FROM dbo.SurveyQuestionAnswerOptions
	WHERE [Id] = @Id

*/

BEGIN

DELETE FROM	dbo.SurveyQuestionAnswerOptions
		WHERE [Id] = @Id

END
GO
/****** Object:  StoredProcedure [dbo].[SurveyQuestionAnswerOptions_Insert]    Script Date: 12/2/2022 3:13:23 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROC [dbo].[SurveyQuestionAnswerOptions_Insert]
-- =============================================
-- Author:		Michael Shepard
-- Create date: 24 August 2022
-- Description:	inserts into the survey question answer option table
-- Code Reviewer: Ron Busnardo
-- ==============================================

	@Id int OUTPUT
	,@QuestionId int
	,@Text nvarchar(500)
	,@Value nvarchar(100)
	,@AdditionalInfo nvarchar(200)
	,@SortOrder int
	,@CreatedBy int

as

/*
DECLARE	@Id int
		,@QuestionId int = 2
		,@Text nvarchar(500) = 'test text'
		,@Value nvarchar(100) = 'test value'
		,@AdditionalInfo nvarchar(200) = 'test additional info'
		,@SortOrder int = 1
		,@CreatedBy int = 4

EXECUTE dbo.SurveyQuestionAnswerOptions_Insert
		@Id OUTPUT
		,@QuestionId
		,@Text
		,@Value
		,@AdditionalInfo
		,@SortOrder
		,@CreatedBy

SELECT	[Id]
		,[QuestionId]
		,[Text]
		,[Value]
		,[AdditionalInfo]
		,[SortOrder]
		,[CreatedBy]
	FROM dbo.SurveyQuestionAnswerOptions
	WHERE [Id] = @Id

*/

BEGIN

INSERT INTO dbo.SurveyQuestionAnswerOptions
			([QuestionId]
			,[Text]
			,[Value]
			,[AdditionalInfo]
			,[SortOrder]
			,[CreatedBy])
		VALUES
			(@QuestionId
			,@Text
			,@Value
			,@AdditionalInfo
			,@SortOrder
			,@CreatedBy)
		SET @Id = SCOPE_IDENTITY()
END
GO
/****** Object:  StoredProcedure [dbo].[SurveyQuestionAnswerOptions_Update]    Script Date: 12/2/2022 3:13:23 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROC [dbo].[SurveyQuestionAnswerOptions_Update]
-- =============================================
-- Author:		Michael Shepard
-- Create date: 24 August 2022
-- Description: updates the survey question answer option table
-- Code Reviewer: Ron Busnardo
-- ==============================================

	@Id int
	,@QuestionId int
	,@Text nvarchar(500)
	,@Value nvarchar(100)
	,@AdditionalInfo nvarchar(200)
	,@SortOrder int

as

/*
DECLARE	@Id int = 1
		,@QuestionId int = 1
		,@Text nvarchar(500) = 'updated test text'
		,@Value nvarchar(100) = 'updated test value'
		,@AdditionalInfo nvarchar(200) = 'updated test additional info'
		,@SortOrder int = 1

SELECT	*
	FROM dbo.SurveyQuestionAnswerOptions
	WHERE [Id] = @Id

EXECUTE dbo.SurveyQuestionAnswerOptions_Update
		@Id
		,@QuestionId
		,@Text
		,@Value
		,@AdditionalInfo
		,@SortOrder

SELECT	*
	FROM dbo.SurveyQuestionAnswerOptions
	WHERE [Id] = @Id

*/

BEGIN

UPDATE dbo.SurveyQuestionAnswerOptions
		SET	[QuestionId] = @QuestionId
			,[Text] = @Text
			,[Value] = @Value
			,[AdditionalInfo] = @AdditionalInfo
			,[SortOrder] = @SortOrder
		WHERE [Id] = @Id

END
GO
/****** Object:  StoredProcedure [dbo].[SurveyQuestions_Insert]    Script Date: 12/2/2022 3:13:23 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROC [dbo].[SurveyQuestions_Insert]

			@SurveyId int
           ,@UserId int
           ,@Question nvarchar(500)
           ,@HelpText nvarchar(255)
		   ,@IsRequired bit
		   ,@isMultipleAllowed bit
		   ,@QuestionTypeId int
		   ,@StatusId int
		   ,@SortOrder int
		   ,@Id int OUTPUT

-- =============================================
-- Author: < Steven Tran >
-- Create date: < 8/24/22 >
-- Description: < Insert into SurveyQuestion table >
-- Code Reviewer:

-- MODIFIED BY:
-- MODIFIED DATE:
-- Code Reviewer: Justin Solo
-- Note:
-- =============================================

/*-----------Test Code----------

DECLARE		

			@SurveyId int = 1
           ,@UserId int = 1
           ,@Question nvarchar(500) = 'Test Question'
           ,@HelpText nvarchar(255) = 'Test HelpText'
		   ,@IsRequired bit = 1
		   ,@isMultipleAllowed bit = 0
		   ,@QuestionTypeId int = 1
		   ,@StatusId int = 1
           ,@SortOrder int = 1
		   ,@Id int

EXECUTE dbo.SurveyQuestions_Insert
			
			@SurveyId
           ,@UserId
           ,@Question
           ,@HelpText
		   ,@IsRequired
		   ,@isMultipleAllowed
		   ,@QuestionTypeId
		   ,@StatusId
		   ,@SortOrder
		   ,@Id OUTPUT

Select *
FROM [dbo].[SurveyQuestions]
WHERE Id = @Id

*/          

AS

BEGIN

INSERT INTO [dbo].[SurveyQuestions]
           (
		    [SurveyId]
           ,[UserId]
           ,[Question]
           ,[HelpText]
		   ,[IsRequired]
		   ,[isMultipleAllowed]
           ,[QuestionTypeId]
		   ,[StatusId]
           ,[SortOrder]
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

END
GO
/****** Object:  StoredProcedure [dbo].[SurveyQuestions_Update]    Script Date: 12/2/2022 3:13:23 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROC [dbo].[SurveyQuestions_Update]

			@Id int
		   ,@SurveyId int
           ,@UserId int
           ,@Question nvarchar(500)
           ,@HelpText nvarchar(255)
		   ,@IsRequired bit
		   ,@isMultipleAllowed bit
		   ,@QuestionTypeId int
		   ,@StatusId int
		   ,@SortOrder int

-- =============================================
-- Author: < Steven Tran >
-- Create date: < 8/24/22 >
-- Description: < Update SurveyQuestion table by Id >
-- Code Reviewer:

-- MODIFIED BY:
-- MODIFIED DATE:
-- Code Reviewer: Justin Solo
-- Note:
-- =============================================

/*-----------Test Code----------

DECLARE		
		    @Id int = 1
		   ,@SurveyId int = 1
           ,@UserId int = 1
           ,@Question nvarchar(500) = 'Test Question'
           ,@HelpText nvarchar(255) = 'Test HelpText'
		   ,@IsRequired bit = 0
		   ,@isMultipleAllowed bit = 1
		   ,@QuestionTypeId int = 1
		   ,@StatusId int = 1
           ,@SortOrder int = 1


EXECUTE dbo.SurveyQuestions_Update
			
			@Id
		   ,@SurveyId
           ,@UserId
           ,@Question
           ,@HelpText
		   ,@IsRequired
		   ,@isMultipleAllowed
		   ,@QuestionTypeId
		   ,@StatusId
		   ,@SortOrder

Select *
FROM [dbo].[SurveyQuestions]
WHERE Id = @Id

*/          

AS

BEGIN

UPDATE [dbo].[SurveyQuestions]

	 SET
		 [SurveyId] = @SurveyId
        ,[UserId] = @UserId
        ,[Question] = @Question
        ,[HelpText] = @HelpText
		,[IsRequired] = @IsRequired
		,[isMultipleAllowed] = @isMultipleAllowed
        ,[QuestionTypeId] = @QuestionTypeId
		,[StatusId] = @StatusId
        ,[SortOrder] = @SortOrder

	WHERE Id = @Id

	SET @Id = SCOPE_IDENTITY()

END
GO
/****** Object:  StoredProcedure [dbo].[SurveyQuestionWithBatchAnswerOptions_Insert]    Script Date: 12/2/2022 3:13:23 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROC [dbo].[SurveyQuestionWithBatchAnswerOptions_Insert]
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
GO
/****** Object:  StoredProcedure [dbo].[Surveys_Insert]    Script Date: 12/2/2022 3:13:23 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Allen Luong C-119>
-- Create date: <8/24/2022>
-- Description:	<INSERT for dbo.Surveys>
-- Code Reviewer:


-- MODIFIED BY: author
-- MODIFIED DATE:12/1/2020
-- Code Reviewer: 
-- Note: 
-- =============================================


CREATE proc [dbo].[Surveys_Insert]
            @Name nvarchar(100)
           ,@Description nvarchar(2000)
           ,@StatusId int
           ,@SurveyTypeId int
           ,@CreatedBy int
		   ,@CompanyLogo nvarchar(255)
		   ,@Id int OUTPUT

as

/*----TEST CODE----

Declare
            @Name nvarchar(100) = 'Test Survey'
           ,@Description nvarchar(2000) = 'Test Survey Description'
           ,@StatusId int = 1
           ,@SurveyTypeId int = 2
           ,@CreatedBy int = 102
		   ,@CompanyLogo nvarchar (255) = 'A logo'
		   ,@Id int = 0

Execute dbo.Surveys_Insert

            @Name
           ,@Description 
           ,@StatusId
           ,@SurveyTypeId
           ,@CreatedBy
		   ,@CompanyLogo
		   ,@Id OUTPUT

Select * 
FROM dbo.Surveys


*/

BEGIN

INSERT INTO [dbo].[Surveys]
		(
			 [Name]
			,[Description]
			,[StatusId]
			,[SurveyTypeId]
			,[CreatedBy]
			,[CompanyLogo]
		)

VALUES	
		(
            @Name 
           ,@Description 
           ,@StatusId 
           ,@SurveyTypeId 
           ,@CreatedBy 
		   ,@CompanyLogo
		)
		SET @Id = SCOPE_IDENTITY()


END


GO
/****** Object:  StoredProcedure [dbo].[Surveys_InsertV2]    Script Date: 12/2/2022 3:13:23 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Steven Tran
-- Description:	<INSERT v2 for dbo.Surveys>
-- Code Reviewer:


-- MODIFIED BY: author
-- MODIFIED DATE:12/1/2020
-- Code Reviewer: 
-- Note: 
-- =============================================


Create proc [dbo].[Surveys_InsertV2]
            @Name nvarchar(100)
           ,@Description nvarchar(2000)
           ,@StatusId int
           ,@SurveyTypeId int
           ,@CreatedBy int
		   ,@CompanyLogo nvarchar(255)
		   ,@Id int OUTPUT

as

/*----TEST CODE----

Declare
            @Name nvarchar(100) = 'Test Survey'
           ,@Description nvarchar(2000) = 'Test Survey Description'
           ,@StatusId int = 1
           ,@SurveyTypeId int = 2
           ,@CreatedBy int = 102
		   ,@CompanyLogo nvarchar (255) = 'A logo'
		   ,@Id int = 0

Execute dbo.Surveys_Insert

            @Name
           ,@Description 
           ,@StatusId
           ,@SurveyTypeId
           ,@CreatedBy
		   ,@CompanyLogo
		   ,@Id OUTPUT

Select * 
FROM dbo.Surveys


*/

BEGIN

INSERT INTO [dbo].[Surveys]
		(
			 [Name]
			,[Description]
			,[StatusId]
			,[SurveyTypeId]
			,[CreatedBy]
			,[CompanyLogo]
		)

VALUES	
		(
            @Name 
           ,@Description 
           ,@StatusId 
           ,@SurveyTypeId 
           ,@CreatedBy 
		   ,@CompanyLogo
		)
		SET @Id = SCOPE_IDENTITY()


END


GO
/****** Object:  StoredProcedure [dbo].[Surveys_Search]    Script Date: 12/2/2022 3:13:23 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Allen Luong C-119>
-- Create date: <8/24/2022>
-- Description:	<Search for dbo.Surveys>
-- Code Reviewer:


-- MODIFIED BY: author
-- MODIFIED DATE:12/1/2020
-- Code Reviewer: 
-- Note: 
-- =============================================



CREATE Proc [dbo].[Surveys_Search]

					 @PageIndex int
					,@PageSize int
					,@Query nvarchar(200) 

as

/* ----TEST CODE----
Declare 
		 @PageIndex int = 0
		,@PageSize int = 4
		,@Query nvarchar(200) = 'Test'

Execute dbo.Surveys_Search
		 @PageIndex 
		,@PageSize 
		,@Query 

Select * 
FROM dbo.Surveys



*/

BEGIN

Declare @offset int = @PageIndex * @Pagesize

SELECT s.[Id]
      ,s.[Name]
      ,[Description]
	  ,st.Id
      ,st.[Name]
	  ,sty.Id
      ,sty.[Name]
      ,s.[CreatedBy]
	  ,up.FirstName
	  ,up.LastName
	  ,s.CompanyLogo
      ,s.[DateCreated]
      ,s.[DateModified]
	  ,TotalCount = COUNT(1) OVER()  
FROM [dbo].[Surveys] as s 
inner join dbo.SurveyStatus as st
on st.Id = s.StatusId
inner join dbo.SurveyTypes as sty
on sty.id = s.SurveyTypeId
inner join dbo.Users as u 
on s.CreatedBy = u.Id
inner join dbo.UserProfiles as up 
on up.UserId = u.Id

WHERE s.Name like '%' + @Query + '%'

	ORDER BY s.Name 
	OFFSET @offset ROWS 
	FETCH NEXT @PageSize ROWS ONLY 



END
GO
/****** Object:  StoredProcedure [dbo].[Surveys_Select_ById]    Script Date: 12/2/2022 3:13:23 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Allen Luong C-119>
-- Create date: <8/24/2022>
-- Description:	<SELECT_BYID for dbo.Surveys>
-- Code Reviewer:


-- MODIFIED BY: author
-- MODIFIED DATE:12/1/2020
-- Code Reviewer: 
-- Note: 
-- =============================================



CREATE Proc [dbo].[Surveys_Select_ById]

	@Id int

as

/* ----TEST CODE----
Declare @Id int = 1

Execute dbo.Surveys_Select_ById

@Id

*/

BEGIN

SELECT s.[Id]
      ,s.[Name]
      ,[Description]
	  ,st.Id
      ,st.[Name]
	  ,sty.Id
      ,sty.[Name]
      ,s.[CreatedBy]
	  ,s.CompanyLogo
      ,s.[DateCreated]
      ,s.[DateModified]
 
FROM [dbo].[Surveys] as s inner join dbo.SurveyStatus as st
on st.Id = s.StatusId
inner join dbo.SurveyTypes as sty
on sty.id = s.SurveyTypeId

Where s.Id = @Id

END


GO
/****** Object:  StoredProcedure [dbo].[Surveys_SelectAll]    Script Date: 12/2/2022 3:13:23 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Allen Luong C-119>
-- Create date: <8/24/2022>
-- Description:	<SELECT_ALL for dbo.Surveys>
-- Code Reviewer:


-- MODIFIED BY: author
-- MODIFIED DATE:12/1/2020
-- Code Reviewer: 
-- Note: 
-- =============================================

CREATE proc [dbo].[Surveys_SelectAll]
		@PageIndex int
		,@PageSize int

as

/* ----TEST CODE----
Declare		
		@PageIndex int = 0
		,@PageSize int = 10

Execute dbo.Surveys_SelectAll

		@PageIndex 
		,@PageSize 

*/

BEGIN

Declare @offset int = @PageIndex * @Pagesize

SELECT s.[Id]
      ,s.[Name]
      ,[Description]
	  ,st.Id
      ,st.[Name]
	  ,sty.Id
      ,sty.[Name]
      ,s.[CreatedBy]
	  ,s.CompanyLogo
      ,s.[DateCreated]
      ,s.[DateModified]
	  ,TotalCount = COUNT(1) OVER()  
FROM [dbo].[Surveys] as s inner join dbo.SurveyStatus as st
on st.Id = s.StatusId
inner join dbo.SurveyTypes as sty
on sty.id = s.SurveyTypeId

    ORDER BY	s.Id
				OFFSET @offset Rows
	Fetch Next	@PageSize Rows ONLY

END


GO
/****** Object:  StoredProcedure [dbo].[Surveys_SelectAllV2]    Script Date: 12/2/2022 3:13:23 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Allen Luong C-119>
-- Create date: <10/3/2022>
-- Description:	<SELECT_ALLV2 for dbo.Surveys>
-- Code Reviewer:


-- MODIFIED BY: author
-- MODIFIED DATE:12/1/2020
-- Code Reviewer: 
-- Note: 
-- =============================================

CREATE proc [dbo].[Surveys_SelectAllV2]
		@PageIndex int
		,@PageSize int

as

/* ----TEST CODE----
Declare		
		@PageIndex int = 0
		,@PageSize int = 50

Execute dbo.Surveys_SelectAllV2

		@PageIndex 
		,@PageSize 

*/

BEGIN

Declare @offset int = @PageIndex * @Pagesize

SELECT s.[Id]
      ,s.[Name]
      ,[Description]
	  ,st.Id
      ,st.[Name]
	  ,sty.Id
      ,sty.[Name]
      ,s.[CreatedBy]
	  ,up.FirstName
	  ,up.LastName
	  ,s.CompanyLogo
      ,s.[DateCreated]
      ,s.[DateModified]
	  ,TotalCount = COUNT(1) OVER()  
FROM [dbo].[Surveys] as s 
inner join dbo.SurveyStatus as st
on st.Id = s.StatusId
inner join dbo.SurveyTypes as sty
on sty.id = s.SurveyTypeId
inner join dbo.Users as u 
on s.CreatedBy = u.Id
left outer join dbo.UserProfiles as up 
on up.UserId = u.Id

    ORDER BY	s.DateCreated
				DESC
				OFFSET @offset Rows
	Fetch Next	@PageSize Rows ONLY

END


GO
/****** Object:  StoredProcedure [dbo].[Surveys_SelectByCreatedBy]    Script Date: 12/2/2022 3:13:23 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Allen Luong C-119>
-- Create date: <8/24/2022>
-- Description:	<SELECT_ByCreatedBy for dbo.Surveys>
-- Code Reviewer:


-- MODIFIED BY: author
-- MODIFIED DATE:12/1/2020
-- Code Reviewer: 
-- Note: 
-- =============================================



CREATE Proc [dbo].[Surveys_SelectByCreatedBy]

					 @PageIndex int
					,@PageSize int
					,@CreatedBy int 

as

/* ----TEST CODE----
Declare 
					 @PageIndex int = 0
					,@PageSize int = 4
					,@CreatedBy int = 102

Execute dbo.Surveys_SelectByCreatedBy
					 @PageIndex 
					,@PageSize
					,@CreatedBy

SELECT * FROM dbo.Surveys

*/

BEGIN

Declare @offset int = @PageIndex * @Pagesize

SELECT s.[Id]
      ,s.[Name]
      ,[Description]
	  ,st.Id
      ,st.[Name]
	  ,sty.Id
      ,sty.[Name]
      ,s.[CreatedBy]
	  ,s.CompanyLogo
      ,s.[DateCreated]
      ,s.[DateModified]
	  ,TotalCount = COUNT(1) OVER()  
FROM [dbo].[Surveys] as s inner join dbo.SurveyStatus as st
on st.Id = s.StatusId
inner join dbo.SurveyTypes as sty
on sty.id = s.SurveyTypeId

WHERE CreatedBy = @CreatedBy

	ORDER BY s.Id desc
	OFFSET @offset ROWS 
	FETCH NEXT @PageSize ROWS ONLY 



END

GO
/****** Object:  StoredProcedure [dbo].[Surveys_SelectById_Details]    Script Date: 12/2/2022 3:13:23 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Allen Luong C-119>
-- Create date: <8/24/2022>
-- Description:	<SELECT_BYID for dbo.Surveys>
-- Code Reviewer:


-- MODIFIED BY: author
-- MODIFIED DATE:12/1/2020
-- Code Reviewer: 
-- Note: 
-- =============================================



CREATE Proc [dbo].[Surveys_SelectById_Details]

	@Id int

as

/* ----TEST CODE----
Declare @Id int = 1

Execute dbo.[Surveys_SelectById_Details]

@Id

select *
from dbo.surveyquestionansweroptions

select *
from dbo.surveyquestions

select *
from dbo.surveys

select *
from dbo.surveyanswers
*/

BEGIN

SELECT s.[Id]
		,s.[Name]
		,[Description]
		,st.Id
		,st.[Name]
		,sty.Id
		,sty.[Name]
		,s.[CreatedBy]
		,s.CompanyLogo	
		,Questions =	(
						SELECT	 sq.Question
								,sq.QuestionTypeId
								,sq.Id as QuestionId
								,sq.SortOrder
								
						FROM dbo.SurveyQuestions as sq 	
						WHERE sq.isMultipleAllowed = 0 AND sq.SurveyId = s.Id AND sq.QuestionTypeId <> 2
						FOR JSON AUTO
						)
		,QuestionsWithOptions =			
						(										
						SELECT   sq.Question
								,sq.QuestionTypeId
								,sq.Id as QuestionId
								,sq.SortOrder
								,AnswerOptions.Id as AnsOptId
								,AnswerOptions.[Text]
								,AnswerOptions.[Value]
								,AnswerOptions.AdditionalInfo
								,AnswerOptions.SortOrder
						FROM dbo.SurveyQuestions as sq  
						inner join dbo.SurveyQuestionAnswerOptions as AnswerOptions
						on sq.Id = AnswerOptions.QuestionId
						WHERE s.Id = sq.SurveyId
						FOR JSON AUTO
						)
		
		,s.[DateCreated]
		,s.[DateModified]
 
FROM [dbo].[Surveys] as s 
inner join dbo.SurveyStatus as st
on st.Id = s.StatusId
inner join dbo.SurveyTypes as sty
on sty.id = s.SurveyTypeId

Where s.Id = @Id

END
GO
/****** Object:  StoredProcedure [dbo].[Surveys_SelectById_DetailsV2]    Script Date: 12/2/2022 3:13:23 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Allen Luong C-119>
-- Create date: <9/12/2022>
-- Description:	<SELECTBYID_DETAILSV2 for dbo.Surveys>
-- Code Reviewer:


-- MODIFIED BY: author
-- MODIFIED DATE:12/1/2020
-- Code Reviewer: 
-- Note: 
-- =============================================

CREATE proc [dbo].[Surveys_SelectById_DetailsV2]

	@Id int

as

/* ----TEST CODE----
Declare @Id int = 1

Execute dbo.[Surveys_SelectById_DetailsV2]

@Id

select *
from dbo.surveyquestionansweroptions

select *
from dbo.surveyquestions

select *
from dbo.surveys

select *
from dbo.surveyanswers
*/

BEGIN

SELECT s.[Id]
		,s.[Name]
		,[Description]
		,st.Id
		,st.[Name]
		,sty.Id
		,sty.[Name]
		,s.[CreatedBy]
		,s.CompanyLogo	
		,QandA =	(										
						SELECT   sq.Question
								,sq.QuestionTypeId
								,sq.Id as QuestionId
								,sq.SortOrder
								,AnswerOptions.Id as AnsOptId
								,AnswerOptions.[Text]
								,AnswerOptions.[Value]
								,AnswerOptions.AdditionalInfo
								,AnswerOptions.SortOrder
						FROM dbo.SurveyQuestions as sq  
						LEFT OUTER JOIN dbo.SurveyQuestionAnswerOptions as AnswerOptions
						on sq.Id = AnswerOptions.QuestionId 
						WHERE s.Id = sq.SurveyId
						FOR JSON AUTO
					)
		
		,s.[DateCreated]
		,s.[DateModified]
 
FROM [dbo].[Surveys] as s 
inner join dbo.SurveyStatus as st
on st.Id = s.StatusId
inner join dbo.SurveyTypes as sty
on sty.id = s.SurveyTypeId

Where s.Id = @Id

END
GO
/****** Object:  StoredProcedure [dbo].[Surveys_Update]    Script Date: 12/2/2022 3:13:23 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Allen Luong C-119>
-- Create date: <8/24/2022>
-- Description:	<UPDATE for dbo.Surveys>
-- Code Reviewer:


-- MODIFIED BY: author
-- MODIFIED DATE:12/1/2020
-- Code Reviewer: 
-- Note: 
-- =============================================


CREATE proc [dbo].[Surveys_Update]
	   @Name nvarchar(100)
      ,@Description nvarchar(2000)
      ,@StatusId int
      ,@SurveyTypeId int
      ,@CreatedBy int
	  ,@CompanyLogo nvarchar (255)
	  ,@Id int
as

/* ----TEST CODE----
Declare @Id int = 2

Declare
		 @Name nvarchar(100) = 'Updating a Survey'
		,@Description nvarchar(2000) = 'Survey Update'
		,@StatusId int = 2
		,@SurveyTypeId int = 2
		,@CreatedBy int = 104
		,@CompanyLogo nvarchar (255) = 'An updated Logo'

EXECUTE dbo.Surveys_Update

	   @Name
      ,@Description 
      ,@StatusId 
      ,@SurveyTypeId 
      ,@CreatedBy 
	  ,@CompanyLogo
	  ,@Id 

SELECT *
from dbo.surveys

*/

BEGIN

Declare @DateModified datetime2 = getutcdate();

UPDATE [dbo].[Surveys]
   SET [Name] = @Name
      ,[Description] = @Description
      ,[StatusId] = @StatusId
      ,[SurveyTypeId] = @SurveyTypeId
      ,[CreatedBy] = @CreatedBy
	  ,CompanyLogo = @CompanyLogo
      ,[DateModified] = @DateModified


	WHERE Id = @Id
END

GO
/****** Object:  StoredProcedure [dbo].[SurveyStatus_SelectAll]    Script Date: 12/2/2022 3:13:23 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author: <Allen Luong>
-- Create date: <8/25/2022>
-- Description: <SelectAll(Paginated) for dbo.SurveyStatus>
-- Code Reviewer:

-- MODIFIED BY: author
-- MODIFIED DATE:12/1/2020
-- Code Reviewer:
-- Note:
-- ==============================================
CREATE proc [dbo].[SurveyStatus_SelectAll]
				 @PageIndex int
				,@PageSize int


as

/*

Declare
				 @PageIndex int = 0
				,@PageSize int = 4

Execute dbo.SurveyStatus_SelectAll

				 @PageIndex
				,@PageSize

Select *
From dbo.SurveyStatus

*/
BEGIN 

Declare @offset int = @PageIndex * @Pagesize

SELECT 
			 [Id]
			,[Name]
			,TotalCount = COUNT(1) OVER()
	FROM [dbo].[SurveyStatus]
	Order BY Id

  	OFFSET @offset Rows
	Fetch Next @PageSize Rows ONLY


END


GO
/****** Object:  StoredProcedure [dbo].[SurveyTypes_SelectAll]    Script Date: 12/2/2022 3:13:23 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author: <Allen Luong>
-- Create date: <8/25/2022>
-- Description: <SelectALL for dbo.SurveyTypes>
-- Code Reviewer:

-- MODIFIED BY: author
-- MODIFIED DATE:12/1/2020
-- Code Reviewer:
-- Note:
-- =============================================

CREATE proc [dbo].[SurveyTypes_SelectAll]

as

/*

Execute dbo.SurveyTypes_SelectAll

*/
BEGIN

SELECT 
		 [Id]
		,[Name]
  FROM [dbo].[SurveyTypes]

END


GO
/****** Object:  StoredProcedure [dbo].[Timesheet_Delete]    Script Date: 12/2/2022 3:13:23 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- Author: <Rebekah Hoskins>
-- Create date: <20220719>
-- Description: <Delete timesheet entries in the timesheet table by specific ID>
-- Code Reviewer:

-- MODIFIED BY: author
-- MODIFIED DATE:12/1/2020
-- Code Reviewer:
-- Note:

CREATE PROC [dbo].[Timesheet_Delete]
		@Id int

AS

/*-----TEST CODE-----

	DECLARE @Id int = 52

	EXECUTE[dbo].[Timesheet_Delete]
		@Id

-----END TEST CODE-----
*/

BEGIN

DELETE FROM [dbo].[Timesheet]
      WHERE @Id = Id
END


GO
/****** Object:  StoredProcedure [dbo].[Timesheet_Insert]    Script Date: 12/2/2022 3:13:23 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- Author: <Rebekah Hoskins>
-- Create date: <20220805>
-- Description: <Insert data into the timesheet table storing records of hours employees worked>
-- Code Reviewer:

-- MODIFIED BY: author
-- MODIFIED DATE:12/1/2020
-- Code Reviewer:
-- Note:

CREATE PROC [dbo].[Timesheet_Insert]
			@Date date
			,@TimeFrom nvarchar(10)
			,@TimeTo nvarchar(10)
			,@OrgId int
			,@Comment nvarchar(800)
			,@CreatedBy int
			,@JobId int

			,@Id int OUTPUT

AS

/*-----TEST CODE-----

	DECLARE @Date date = '2022-08-02'
			,@TimeFrom nvarchar(10) = '12:00'
			,@TimeTo nvarchar(10) = '13:00'
			,@OrgId int = 2
			,@Comment nvarchar(800) = 'other org'
			,@CreatedBy int = 135
			,@JobId int = 2
			,@Id int = 0

	EXECUTE [dbo].[Timesheet_Insert]
			@Date
			,@TimeFrom
			,@TimeTo
			,@OrgId
			,@Comment
			,@CreatedBy
			,@JobId
			,@Id OUTPUT

-----END TEST CODE-----
*/

BEGIN
	
	DECLARE @DateSubmitted datetime2(7) = getutcdate()
			,@TotalHrs int = DATEDIFF(hour, @TimeFrom, @TimeTo)

	INSERT INTO [dbo].[Timesheet]
           ([Date]
           ,[TimeFrom]
           ,[TimeTo]
           ,[OrgId]
           ,[Comment]
           ,[DateSubmmitted]
           ,[CreatedBy]
		   ,[JobId]
		   ,[TotalHrs]
           ,[ModifiedBy])
     VALUES
           (@Date
			,@TimeFrom
			,@TimeTo
			,@OrgId
			,@Comment
			,@DateSubmitted
			,@CreatedBy
			,@JobId
			,@TotalHrs
			,@CreatedBy)

		SET @Id = SCOPE_IDENTITY()

	Select *
	From [dbo].[Timesheet]
	Where Id = @Id

END

GO
/****** Object:  StoredProcedure [dbo].[Timesheet_Organizations]    Script Date: 12/2/2022 3:13:23 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- Author: <Rebekah Hoskins>
-- Create date: <20220803>
-- Description: <Bring back orgs and orgNames>
-- Code Reviewer:

-- MODIFIED BY: author
-- MODIFIED DATE:12/1/2020
-- Code Reviewer:
-- Note:

CREATE PROC [dbo].[Timesheet_Organizations]

AS

/*-----TEST CODE-----

	EXECUTE [dbo].[Timesheet_Organizations]

-----END TEST CODE-----
*/

BEGIN 

	SELECT Id
			,Name
	FROM dbo.Organizations

END
GO
/****** Object:  StoredProcedure [dbo].[Timesheet_Search]    Script Date: 12/2/2022 3:13:23 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- Author: <Rebekah Hoskins>
-- Create date: <20220802>
-- Description: <Current Users timesheet entry retrieved from a date and org input>
-- Code Reviewer:

-- MODIFIED BY: author
-- MODIFIED DATE:12/1/2020
-- Code Reviewer:
-- Note:

CREATE PROC [dbo].[Timesheet_Search]
			@userId int
			,@PageIndex int
			,@PageSize int
			,@OrganizationId int

AS

/*-----TEST CODE-----

	DECLARE @userId int = 22
			,@PageIndex int = 0
			,@PageSize int = 5
			,@OrgId int = 2

	EXECUTE [dbo].[Timesheet_Search]
			@userId
			,@PageIndex
			,@PageSize
			,@OrgId

-----END TEST CODE-----
*/
BEGIN

	DECLARE @offset int = @PageIndex * @PageSize

	SELECT DISTINCT t.[Id]
		,t.[Date]
		,t.[TimeFrom]
		,t.[TimeTo]
		,t.[Comment]
		,t.[OrgId] AS OrganizationId
		,o.[Name] AS Organization
		,j.[LocationId]
		,l.[City]
		,j.[Title]
		,t.[JobId]
		,u.[Id] AS UserId
		,TotalCount = COUNT(1) OVER()

	FROM [dbo].[Timesheet] AS t INNER JOIN dbo.Users AS u
			ON t.CreatedBy = u.Id
		INNER JOIN dbo.UserProfiles AS up
			ON u.Id = up.UserId
		INNER JOIN dbo.Jobs as j
			ON t.JobId = j.Id 
		INNER JOIN dbo.JobTypes as jt
			ON j.JobTypeId = jt.Id
		INNER JOIN dbo.Locations as l
			ON l.Id = j.[LocationId]
		INNER JOIN dbo.Organizations as o
			ON o.Id = t.OrgId
	WHERE @OrganizationId = t.OrgId AND t.CreatedBy = @userId
	
	ORDER BY t.[Date] Desc
	OFFSET @offSet Rows
	Fetch Next @PageSize Rows ONLY

END

GO
/****** Object:  StoredProcedure [dbo].[Timesheet_SearchDay]    Script Date: 12/2/2022 3:13:23 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- Author: <Rebekah Hoskins>
-- Create date: <20220802>
-- Description: <Current Users timesheet entries from the last Day and for a specific orgID>
-- Code Reviewer:

-- MODIFIED BY: author
-- MODIFIED DATE:12/1/2020
-- Code Reviewer:
-- Note:

CREATE PROC [dbo].[Timesheet_SearchDay]
			@userId int
			,@PageIndex int
			,@PageSize int
			,@OrgId int

AS

/*-----TEST CODE-----

	DECLARE @userId int = 22
			,@PageIndex int = 0
			,@PageSize int = 5
			,@OrgId int = 2

	EXECUTE [dbo].[Timesheet_SearchDay]
			@userId
			,@PageIndex
			,@PageSize
			,@OrgId

-----END TEST CODE-----
*/
BEGIN

	DECLARE @offset int = @PageIndex * @PageSize
			,@Date datetime = GETUTCDATE()

	SELECT DISTINCT t.[Id]
		,t.[Date]
		,t.[TimeFrom]
		,t.[TimeTo]
		,t.[Comment]
		,t.[OrgId] AS OrganizationId
		,o.[Name] AS Organization
		,j.[LocationId]
		,l.[City]
		,j.[Title]
		,t.[JobId]
		,u.[Id] AS UserId
		,TotalCount = COUNT(1) OVER()

	FROM [dbo].[Timesheet] AS t INNER JOIN dbo.Users AS u
			ON t.CreatedBy = u.Id
		INNER JOIN dbo.UserProfiles AS up
			ON u.Id = up.UserId
		INNER JOIN dbo.Jobs as j
			ON t.JobId = j.Id 
		INNER JOIN dbo.JobTypes as jt
			ON j.JobTypeId = jt.Id
		INNER JOIN dbo.Locations as l
			ON l.Id = j.[LocationId]
		INNER JOIN dbo.Organizations as o
			ON o.Id = t.OrgId
	WHERE @orgId = t.OrgId AND t.CreatedBy = @userId AND t.[Date] = @Date
	
	ORDER BY t.[Date] Desc
	OFFSET @offSet Rows
	Fetch Next @PageSize Rows ONLY

END

GO
/****** Object:  StoredProcedure [dbo].[Timesheet_SearchFilter]    Script Date: 12/2/2022 3:13:23 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- Author: <Rebekah Hoskins>
-- Create date: <20220802>
-- Description: <Current Users timesheet entries from the last week and for a specific orgID>
-- Code Reviewer:

-- MODIFIED BY: author
-- MODIFIED DATE:12/1/2020
-- Code Reviewer:
-- Note:

CREATE PROC [dbo].[Timesheet_SearchFilter]
			@userId int
			,@OrganizationId int
			,@DateRange int
			,@PageIndex int
			,@PageSize int

AS

/*-----TEST CODE-----

	DECLARE @userId int = 23
			,@OrganizationId int = 1
			,@DateRange int = 30
			,@PageIndex int = 0
			,@PageSize int = 10

	EXECUTE [dbo].[Timesheet_SearchFilter]
			@userId
			,@OrganizationId
			,@DateRange
			,@PageIndex
			,@PageSize

-----END TEST CODE-----
*/
BEGIN

	DECLARE @EndDate datetime = DATEADD(DAY, -@DateRange, GETUTCDATE())
			,@StartDate datetime = GETUTCDATE()
			,@offset int = @PageIndex * @PageSize

	SELECT DISTINCT t.[Id]
		,t.[Date]
		,t.[TimeFrom]
		,t.[TimeTo]
		,t.[Comment]
		,t.[OrgId] AS OrganizationId
		,o.[Name] AS Organization
		,j.[LocationId]
		,l.[City]
		,j.[Title]
		,t.[JobId]
		,u.[Id] AS UserId
		,t.[TotalHrs]
		,TotalCount = COUNT(1) OVER()

	FROM [dbo].[Timesheet] AS t INNER JOIN dbo.Users AS u
			ON t.CreatedBy = u.Id
		INNER JOIN dbo.UserProfiles AS up
			ON u.Id = up.UserId
		INNER JOIN dbo.Jobs as j
			ON t.JobId = j.Id 
		INNER JOIN dbo.JobTypes as jt
			ON j.JobTypeId = jt.Id
		INNER JOIN dbo.Locations as l
			ON l.Id = j.[LocationId]
		INNER JOIN dbo.Organizations as o
			ON o.Id = t.OrgId
	WHERE @OrganizationId = t.OrgId AND t.CreatedBy = @userId AND @EndDate <= t.[Date] AND t.[Date] <= @StartDate
	
	ORDER BY t.[Date] Desc
	OFFSET @offSet Rows
	Fetch Next @PageSize Rows ONLY


END
GO
/****** Object:  StoredProcedure [dbo].[Timesheet_SearchMonth]    Script Date: 12/2/2022 3:13:23 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- Author: <Rebekah Hoskins>
-- Create date: <20220802>
-- Description: <Current Users timesheet entries from the last month and for a specific orgID>
-- Code Reviewer:

-- MODIFIED BY: author
-- MODIFIED DATE:12/1/2020
-- Code Reviewer:
-- Note:


CREATE PROC [dbo].[Timesheet_SearchMonth]
			@userId int
			,@PageIndex int
			,@PageSize int
			,@OrgId int

AS

/*-----TEST CODE-----

	DECLARE @userId int = 22
			,@PageIndex int = 0
			,@PageSize int = 5
			,@OrgId int = 2

	EXECUTE [dbo].[Timesheet_SearchMonth]
			@userId
			,@PageIndex
			,@PageSize
			,@OrgId

-----END TEST CODE-----
*/
BEGIN

	DECLARE @offset int = @PageIndex * @PageSize
			,@Date datetime = DATEADD(MONTH, -1, GETUTCDATE())

	SELECT DISTINCT t.[Id]
		,t.[Date]
		,t.[TimeFrom]
		,t.[TimeTo]
		,t.[Comment]
		,t.[OrgId] AS OrganizationId
		,o.[Name] AS Organization
		,j.[LocationId]
		,l.[City]
		,j.[Title]
		,t.[JobId]
		,u.[Id] AS UserId
		,TotalCount = COUNT(1) OVER()

	FROM [dbo].[Timesheet] AS t INNER JOIN dbo.Users AS u
			ON t.CreatedBy = u.Id
		INNER JOIN dbo.UserProfiles AS up
			ON u.Id = up.UserId
		INNER JOIN dbo.Jobs as j
			ON t.JobId = j.Id 
		INNER JOIN dbo.JobTypes as jt
			ON j.JobTypeId = jt.Id
		INNER JOIN dbo.Locations as l
			ON l.Id = j.[LocationId]
		INNER JOIN dbo.Organizations as o
			ON o.Id = t.OrgId
	WHERE @orgId = t.OrgId AND t.CreatedBy = @userId AND t.[Date] >= @Date
	
	ORDER BY t.[Date] Desc
	OFFSET @offSet Rows
	Fetch Next @PageSize Rows ONLY

END
GO
/****** Object:  StoredProcedure [dbo].[Timesheet_SearchWeek]    Script Date: 12/2/2022 3:13:23 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- Author: <Rebekah Hoskins>
-- Create date: <20220802>
-- Description: <Current Users timesheet entries from the last week and for a specific orgID>
-- Code Reviewer:

-- MODIFIED BY: author
-- MODIFIED DATE:12/1/2020
-- Code Reviewer:
-- Note:

CREATE PROC [dbo].[Timesheet_SearchWeek]
			@userId int
			,@PageIndex int
			,@PageSize int
			,@OrgId int

AS

/*-----TEST CODE-----

	DECLARE @userId int = 22
			,@PageIndex int = 0
			,@PageSize int = 5
			,@OrgId int = 2

	EXECUTE [dbo].[Timesheet_SearchWeek]
			@userId
			,@PageIndex
			,@PageSize
			,@OrgId

-----END TEST CODE-----
*/
BEGIN

	DECLARE @offset int = @PageIndex * @PageSize
			,@Date datetime = DATEADD(DAY, -7, GETUTCDATE())

	SELECT DISTINCT t.[Id]
		,t.[Date]
		,t.[TimeFrom]
		,t.[TimeTo]
		,t.[Comment]
		,t.[OrgId] AS OrganizationId
		,o.[Name] AS Organization
		,j.[LocationId]
		,l.[City]
		,j.[Title]
		,t.[JobId]
		,u.[Id] AS UserId
		,TotalCount = COUNT(1) OVER()

	FROM [dbo].[Timesheet] AS t INNER JOIN dbo.Users AS u
			ON t.CreatedBy = u.Id
		INNER JOIN dbo.UserProfiles AS up
			ON u.Id = up.UserId
		INNER JOIN dbo.Jobs as j
			ON t.JobId = j.Id 
		INNER JOIN dbo.JobTypes as jt
			ON j.JobTypeId = jt.Id
		INNER JOIN dbo.Locations as l
			ON l.Id = j.[LocationId]
		INNER JOIN dbo.Organizations as o
			ON o.Id = t.OrgId
	WHERE @orgId = t.OrgId AND t.CreatedBy = @userId AND t.[Date] >= @Date
	
	ORDER BY t.[Date] Desc
	OFFSET @offSet Rows
	Fetch Next @PageSize Rows ONLY

END
GO
/****** Object:  StoredProcedure [dbo].[Timesheet_SelectAllByCurrentUser]    Script Date: 12/2/2022 3:13:23 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- Author: <Rebekah Hoskins>
-- Create date: <20220719>
-- Description: <Select all timesheet table records associated to userId>
-- Code Reviewer:

-- MODIFIED BY: author
-- MODIFIED DATE:12/1/2020
-- Code Reviewer:
-- Note:

CREATE PROC [dbo].[Timesheet_SelectAllByCurrentUser]
			@userId int

AS

/*-----TEST CODE-----

	DECLARE @userId int = 22

	EXECUTE [dbo].[Timesheet_SelectAllByCurrentUser]
			@userId

-----END TEST CODE-----
*/
BEGIN

	SELECT DISTINCT t.[Id]
		,t.[Date]
		,t.[TimeFrom]
		,t.[TimeTo]
		,t.[Comment]
		,t.[OrgId] AS OrganizationId
		,o.[Name] AS Organization
		,j.[LocationId]
		,l.[City]
		,j.[Title]
		,t.[JobId]
		,u.[Id] AS UserId

	FROM [dbo].[Timesheet] AS t INNER JOIN dbo.Users AS u
			ON t.CreatedBy = u.Id
		INNER JOIN dbo.UserProfiles AS up
			ON u.Id = up.UserId
		INNER JOIN dbo.Jobs as j
			ON t.JobId = j.Id 
		INNER JOIN dbo.JobTypes as jt
			ON j.JobTypeId = jt.Id
		INNER JOIN dbo.Locations as l
			ON l.Id = j.[LocationId]
		INNER JOIN dbo.Organizations as o
			ON o.Id = t.OrgId
	WHERE @userId = t.CreatedBy

END

GO
/****** Object:  StoredProcedure [dbo].[Timesheet_SelectAllByOrg]    Script Date: 12/2/2022 3:13:23 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- Author: <Rebekah Hoskins>
-- Create date: <20220719>
-- Description: <Select all timesheet table records associated to the orgId>
-- Code Reviewer:

-- MODIFIED BY:
-- MODIFIED DATE:
-- Code Reviewer:
-- Note:

CREATE PROC [dbo].[Timesheet_SelectAllByOrg]
			@PageIndex int
			,@PageSize int
			,@OrganizationId int

AS

/*-----TEST CODE-----

	DECLARE @orgId int = 2

	EXECUTE [dbo].[Timesheet_SelectAllByOrg]
			@orgId

-----END TEST CODE-----
*/

BEGIN

	DECLARE @offset int = @PageIndex * @PageSize

	SELECT DISTINCT t.[Id]
		,t.[Date]
		,t.[TimeFrom]
		,t.[TimeTo]
		,t.[Comment]
		,t.[OrgId] AS OrganizationId
		,o.[Name] AS Organization
		,j.[LocationId]
		,l.[City]
		,j.[Title]
		,t.[JobId]
		,u.[Id] AS UserId
		,TotalCount = COUNT(1) OVER()

	FROM [dbo].[Timesheet] AS t INNER JOIN dbo.Users AS u
			ON t.CreatedBy = u.Id
		INNER JOIN dbo.UserProfiles AS up
			ON u.Id = up.UserId
		INNER JOIN dbo.Jobs as j
			ON t.JobId = j.Id 
		INNER JOIN dbo.JobTypes as jt
			ON j.JobTypeId = jt.Id
		INNER JOIN dbo.Locations as l
			ON l.Id = j.[LocationId]
		INNER JOIN dbo.Organizations as o
			ON o.Id = t.OrgId
	WHERE @OrganizationId = t.OrgId

END
GO
/****** Object:  StoredProcedure [dbo].[Timesheet_SelectAllbYUser_Pagination]    Script Date: 12/2/2022 3:13:23 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- Author: <Rebekah Hoskins>
-- Create date: <20220801>
-- Description: <Select all timesheet table records associated with userId and return in a paginated format>
-- Code Reviewer:

-- MODIFIED BY: author
-- MODIFIED DATE:12/1/2020
-- Code Reviewer:
-- Note:

CREATE PROC [dbo].[Timesheet_SelectAllbYUser_Pagination]
			@userId int
			,@PageIndex int
			,@PageSize int

AS

/*-----TEST CODE-----

	DECLARE @userId int = 135
			,@PageIndex int = 0
			,@PageSize int = 25

	EXECUTE [dbo].[Timesheet_SelectAllbYUser_Pagination]
			@userId
			,@PageIndex
			,@PageSize

-----END TEST CODE-----
*/

BEGIN

	DECLARE @offset int = @PageIndex * @PageSize

	SELECT DISTINCT t.[Id]
		,t.[Date]
		,t.[TimeFrom]
		,t.[TimeTo]
		,t.[Comment]
		,t.[OrgId] AS OrganizationId
		,o.[Name] AS Organization
		,j.[LocationId]
		,l.[City]
		,j.[Title]
		,t.[JobId]
		,u.[Id] AS UserId
		,t.[TotalHrs]
		,TotalCount = COUNT(1) OVER()

	FROM [dbo].[Timesheet] AS t INNER JOIN dbo.Users AS u
			ON t.CreatedBy = u.Id
		INNER JOIN dbo.UserProfiles AS up
			ON u.Id = up.UserId
		INNER JOIN dbo.Jobs as j
			ON t.JobId = j.Id 
		INNER JOIN dbo.JobTypes as jt
			ON j.JobTypeId = jt.Id
		INNER JOIN dbo.Locations as l
			ON l.Id = j.[LocationId]
		INNER JOIN dbo.Organizations as o
			ON o.Id = t.OrgId
	WHERE @userId = t.CreatedBy
	
	ORDER BY t.[Date] Desc
	OFFSET @offSet Rows
	Fetch Next @PageSize Rows ONLY

END

GO
/****** Object:  StoredProcedure [dbo].[Timesheet_SelectAllOrg_User]    Script Date: 12/2/2022 3:13:23 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- Author: <Rebekah Hoskins>
-- Create date: <20220719>
-- Description: <Select all timesheet table records associated with users firstName, lastName, and org>
-- Code Reviewer:

-- MODIFIED BY: author
-- MODIFIED DATE:12/1/2020
-- Code Reviewer:
-- Note:

CREATE PROC [dbo].[Timesheet_SelectAllOrg_User]
			@userId int
			,@PageIndex int
			,@PageSize int
			,@OrganizationId int

AS

/*-----TEST CODE-----

	DECLARE @userId int = 135
			,@PageIndex int = 0
			,@PageSize int = 5
			,@OrgId int = 2

	EXECUTE [dbo].[Timesheet_SelectAllOrg_User]
			@userId
			,@PageIndex
			,@PageSize
			,@OrgId

-----END TEST CODE-----
*/
BEGIN

	DECLARE @offset int = @PageIndex * @PageSize

	SELECT DISTINCT t.[Id]
		,t.[Date]
		,t.[TimeFrom]
		,t.[TimeTo]
		,t.[Comment]
		,t.[OrgId] AS OrganizationId
		,o.[Name] AS Organization
		,j.[LocationId]
		,l.[City]
		,j.[Title]
		,t.[JobId]
		,u.[Id] AS UserId
		,t.[TotalHrs]
		,TotalCount = COUNT(1) OVER()

	FROM [dbo].[Timesheet] AS t INNER JOIN dbo.Users AS u
			ON t.CreatedBy = u.Id
		INNER JOIN dbo.UserProfiles AS up
			ON u.Id = up.UserId
		INNER JOIN dbo.Jobs as j
			ON t.JobId = j.Id 
		INNER JOIN dbo.JobTypes as jt
			ON j.JobTypeId = jt.Id
		INNER JOIN dbo.Locations as l
			ON l.Id = j.[LocationId]
		INNER JOIN dbo.Organizations as o
			ON o.Id = t.OrgId
	WHERE @OrganizationId = t.OrgId AND t.CreatedBy = @userId
	
	ORDER BY t.[Date] Desc
	OFFSET @offSet Rows
	Fetch Next @PageSize Rows ONLY

END
GO
/****** Object:  StoredProcedure [dbo].[Timesheet_Update]    Script Date: 12/2/2022 3:13:23 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- Author: <Rebekah Hoskins>
-- Create date: <20220719>
-- Description: <Insert data into the timesheet table storing records of hours employees worked>
-- Code Reviewer:

-- MODIFIED BY: author
-- MODIFIED DATE:12/1/2020
-- Code Reviewer:
-- Note:

CREATE PROC [dbo].[Timesheet_Update]
			@Date date
			,@TimeFrom nvarchar(10)
			,@TimeTo nvarchar(10)
			,@OrgId int
			,@Comment nvarchar(800)
			,@ModifiedBy int
			,@JobId int
			,@Id int

AS

/*-----TEST CODE-----

	DECLARE @Date date = '2022-08-02'
			,@TimeFrom nvarchar(10) = '08:00'
			,@TimeTo nvarchar(10) = '08:30'
			,@OrgId int = 1
			,@Comment nvarchar(800) = 'postman update test'
			,@ModifiedBy int = 22
			,@JobId int
			,@Id int = 51

	EXECUTE [dbo].[Timesheet_Update]
			@Date
			,@TimeFrom
			,@TimeTo
			,@OrgId
			,@Comment
			,@ModifiedBy
			,@JobId
			,@Id
-----END TEST CODE-----
*/

BEGIN

	UPDATE [dbo].[Timesheet]
		SET [Date] = @Date
			,[TimeFrom] = @TimeFrom
			,[TimeTo] = @TimeTo
			,[OrgId] = @OrgId
			,[Comment] = @Comment
			,[JobId] = @JobId
			,[TotalHrs] = DATEDIFF(hour, @TimeFrom, @TimeTo)
			,[ModifiedBy] = @ModifiedBy
		WHERE @Id = Id

	SELECT *
	FROM [dbo].[Timesheet]

 END

GO
/****** Object:  StoredProcedure [dbo].[TimesheetTotalByUser]    Script Date: 12/2/2022 3:13:23 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROC [dbo].[TimesheetTotalByUser]
		@UserId int

AS

/*-----TEST CODE-----

	DECLARE @UserId int = 135

	EXECUTE [dbo].[TimesheetTotalByOrg] 
		@UserId

-----END TEST CODE-----
*/

BEGIN
	
	
	SELECT Total = SUM(TotalHrs)
	FROM dbo.Timesheet
	WHERE @UserId = CreatedBy	

END
GO
/****** Object:  StoredProcedure [dbo].[TokenTypes_SelectAll]    Script Date: 12/2/2022 3:13:23 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author: <Justin Solo>
-- Create date: <07/05/2022>
-- Description: <TokenTypes_SelectAll>
-- Code Reviewer:

-- MODIFIED BY: author
-- MODIFIED DATE:12/1/2020
-- Code Reviewer:
-- Note:
-- =============================================

CREATE PROC [dbo].[TokenTypes_SelectAll]

AS

/* ----- TEST CODE -----
	
	EXECUTE dbo.TokenTypes_SelectAll

*/ ----- END TEST CODE -----

BEGIN

	SELECT [Id]
		  ,[Name]
	  FROM [dbo].[TokenTypes]

END


GO
/****** Object:  StoredProcedure [dbo].[UrlTypes_SelectAll]    Script Date: 12/2/2022 3:13:23 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE proc [dbo].[UrlTypes_SelectAll]

as

/*....TEST CODE....

execute dbo.UrlTypes_SelectAll

*/

BEGIN


SELECT [Id]
      ,[Name]
  FROM [dbo].[UrlTypes]

END

GO
/****** Object:  StoredProcedure [dbo].[userDashboard]    Script Date: 12/2/2022 3:13:23 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

-- =============================================
-- Author: <Rebekah Hoskins>
-- Create date: <08/31/2022>
-- Description: <user dashboard info>
-- Code Reviewer:

-- MODIFIED BY: author
-- MODIFIED DATE:12/1/2020
-- Code Reviewer:
-- Note:
-- =============================================

CREATE PROC [dbo].[userDashboard]
	@UserId int

AS

/*-----TEST CODE-----

	DECLARE @UserId int = 135

	EXECUTE [dbo].[userDashboard]
		@UserId

-----END TEST CODE-----
*/

BEGIN

	SELECT TOP 1
			b.[Id]
			,b.[Title]
			,b.[Subject]
			,b.[Content]
			,b.[IsPublished]
			,b.[ImageUrl]
			,b.[DateCreated]
			,b.[DateModified]
			,b.[DatePublish]
			,bt.[Id]
			,bt.[Name] as BlogType
			,b.AuthorId
			,up.UserId
			,up.[FirstName]
			,up.[LastName]
			,up.[Mi]
			,up.[AvatarUrl]

		FROM dbo.Blogs as b inner join dbo.BlogTypes as bt
							on bt.Id = b.BlogTypeId
							inner join dbo.UserProfiles as up
							on up.UserId = b.AuthorId	
		WHERE IsPublished = 1
		ORDER BY b.DatePublish DESC

	SELECT TOP 5
			n.Id
			,n.PublishDate
			,nt.Name
			,nt.Description
			,nt.ExampleUrl
		FROM dbo.Newsletters AS n INNER JOIN dbo.NewsletterTemplates AS nt
			ON n.NewsletterTemplateId = nt.Id
		WHERE IsPublished = 1
		ORDER BY n.PublishDate DESC

	SELECT t.Date, t.TotalHrs, t.OrgId, o.Name, o.Logo
		FROM dbo.Timesheet AS t INNER JOIN dbo.Organizations AS o
			ON t.OrgId = o.Id
		WHERE @UserId = t.CreatedBy AND t.[Date] >= DATEADD(DAY, -5, GETUTCDATE())
		ORDER BY t.Date DESC

	SELECT TOP 5 
			o.Name
			,t.OrgId
			,Total = SUM(TotalHrs)
		FROM dbo.Timesheet AS t INNER JOIN dbo.Organizations AS o
			ON t.OrgId = o.Id
		WHERE @UserId = t.CreatedBy	
		GROUP BY OrgId, o.Name
		ORDER BY Total DESC

	SELECT j.Id
			,j.Title
			,j.Description
			,j.Requirements
		FROM dbo.UserJobs AS uj INNER JOIN dbo.Jobs AS j
			ON uj.JobId = j.Id
		WHERE uj.UserId = @UserId

END
GO
/****** Object:  StoredProcedure [dbo].[UserProfessionTypes_Insert_UDT]    Script Date: 12/2/2022 3:13:23 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author: <Hyun Kim>
-- Create date: <7/25/22>
-- Description: <[UserProfessionTypes_Insert_UDT]>
-- Code Reviewer:

-- MODIFIED BY: author
-- MODIFIED DATE:12/1/2020
-- Code Reviewer:
-- Note:
-- =============================================


CREATE proc [dbo].[UserProfessionTypes_Insert_UDT]
				@UserProfileId int
				,@ListProfessionTypeId dbo.IntTable readonly

AS

/*-----TEST CODE----

	Declare @UserProfileId int = 20  
			,@ProfessionTypeId int = 1, 2;


	execute dbo.UserProfessionTypes_Insert_UDT
							@UserProfileId 
							,@ProfessionTypeId 

			Select * 
			from dbo.UserProfessionTypes
			order by ProfessionTypeId
			
			select * 
			from dbo.ProfessionTypes

			select *
			from dbo.UserProfileId
		


*/-----TEST CODE----

BEGIN

INSERT INTO [dbo].[UserProfessionTypes]
			   ([UserProfileId]
			   ,[ProfessionTypeId])

     Select
           @UserProfileId 
           ,lpt.data
		  from @ListProfessionTypeId as lpt

END
GO
/****** Object:  StoredProcedure [dbo].[UserProfessionTypes_SelectAll]    Script Date: 12/2/2022 3:13:23 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

-- =============================================
-- Author: <Hyun Kim>
-- Create date: <7/25/22>
-- Description: <[UserProfessionTypes_SelectAll]>
-- Code Reviewer:

-- MODIFIED BY: author
-- MODIFIED DATE:12/1/2020
-- Code Reviewer:
-- Note:
-- =============================================

CREATE proc [dbo].[UserProfessionTypes_SelectAll]
						@PageIndex int
						,@PageSize int

AS


/* ----TEST CODE----

	Declare @PageIndex int = 0
			,@PageSize int = 10



execute dbo.UserProfessionTypes_SelectAll 
								@PageIndex 
								,@PageSize
				
					select * from dbo.ProfessionTypes

					select * from dbo.UserProfiles

*/ ----TEST CODE----

BEGIN

	Declare @offset int = @PageIndex * @PageSize

	Select upt.UserProfileId
		   ,pt.Name as ProfessionType
		   ,TotalCount = COUNT(1) OVER() 
			

  FROM [dbo].[UserProfessionTypes] as upt
		inner join dbo.ProfessionTypes as pt
		on upt.ProfessionTypeId = pt.Id

		ORDER BY upt.UserProfileId

	OFFSET @offSet Rows
	Fetch Next @PageSize Rows ONLY

END
GO
/****** Object:  StoredProcedure [dbo].[UserProfileLicenses_SelectAll]    Script Date: 12/2/2022 3:13:23 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE   proc [dbo].[UserProfileLicenses_SelectAll]
							@PageIndex int
						   ,@PageSize int
as

BEGIN

/*------TEST CODE------

Declare @PageIndex int = 0
	   ,@PageSize int = 10

Execute [dbo].[UserProfileLicenses_SelectAll]
						  @PageIndex
						 ,@PageSize

*/
Declare @offset int = @PageIndex * @PageSize

SELECT	up.Id
		,up.FirstName
		,up.LastName
		,up.Mi
		,up.AvatarUrl
		,up.DOB
		,up.Phone
		,up.email
		,Licenses = (SELECT lt.[Name] as licenseType
							,s.[Name] as licenseState
							,l.LicenseNumber as licenseNumber
							,l.DateExpires	as dateExpires
							,u.Email as creatBy

					FROM dbo.UserProfileLicenses as upl 
							inner join dbo.Licenses as l on upl.LicenseId = l.Id
							inner join dbo.States as s on l.LicenseStateId = s.Id
							inner join dbo.LicenseTypes as lt on l.LicenseTypeId = lt.Id
							inner join dbo.users as u  on l.CreatedBy = u.Id
							WHERE upl.UserProfileId = up.Id
						FOR JSON PATH
						)
		,TotalCount = COUNT(1) OVER()

FROM dbo.UserProfiles as up 
		inner join dbo.Users as u on up.UserId = u.Id

ORDER BY up.Id
	
	OFFSET @offSet Rows
  FETCH NEXT @PageSize ROWS ONLY

END
GO
/****** Object:  StoredProcedure [dbo].[UserProfiles_Delete_ById]    Script Date: 12/2/2022 3:13:23 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE proc [dbo].[UserProfiles_Delete_ById]
	@Id int

/* ----TEST CODE----

	Declare @Id int = 7

	Select *
	From dbo.UserProfiles
	Where Id = @Id

	Execute dbo.UserProfiles_Delete_ById
		@Id



*/ ----TEST CODE----

AS

BEGIN

DELETE FROM [dbo].[UserProfiles]
      WHERE Id = @Id

END
GO
/****** Object:  StoredProcedure [dbo].[UserProfiles_Insert]    Script Date: 12/2/2022 3:13:23 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

-- =============================================
-- Author: <Rebekah Hoskins>
-- Create date: <2022/08/06>
-- Description: <UserProfiles_Insert>
-- Code Reviewer:

-- MODIFIED BY:
-- MODIFIED DATE:
-- Code Reviewer:
-- Note:
-- =============================================

CREATE proc [dbo].[UserProfiles_Insert]

			@LocationTypeId int
			,@LineOne nvarchar(255)
			,@LineTwo nvarchar(255)
			,@City nvarchar(225)
			,@Zip nvarchar(50)
			,@StateId int
			,@Latitude float 
			,@Longitude float

			,@UserId int

			,@BatchProfessionTypes dbo.UserProfessionTypes READONLY

			,@FirstName nvarchar(100)
			,@LastName nvarchar(100)
			,@Mi nvarchar(2)
			,@AvatarUrl nvarchar(255)
			,@DOB datetime2(7)
			,@Email nvarchar(100)
			,@Phone nvarchar(20)
			,@YearsOfExperience nvarchar(10)
			,@DesiredHourlyRate nvarchar(10)
			,@IsActive bit
			,@Id int OUTPUT

/* ---- TEST CODE ----


	Declare @LocationTypeId int = 5
			,@LineOne nvarchar(255) = 'LineOne Insert'
			,@LineTwo nvarchar(255) = 'LineTwo Insert'
			,@City nvarchar(225) = 'City Insert'
			,@Zip nvarchar(50) = '123123'
			,@StateId int = 24
			,@Latitude float = '34.4856'
			,@Longitude float = '45.5484'
	
			,@UserId int = 79
			,@FirstName nvarchar(100) = 'TestUserFirstNameNew'
			,@LastName nvarchar(100) = 'TestUserLastNameNew'
			,@Mi nvarchar(2) = 'T'
			,@AvatarUrl nvarchar(255) = 'TestImageUrl.com'
			,@DOB datetime2(7) = '1955-05-05'
			,@Email nvarchar(100) = 'testuseremail@gmail.com'
			,@Phone nvarchar(20) = '1234567890'
			,@YearsOfExperience nvarchar(10) = '10'
			,@DesiredHourlyRate nvarchar(10) = '100'
			,@IsActive bit = 1
			,@Id int = 0

			
	Declare @BatchProfessionTypes dbo.UserProfessionTypes
	INSERT INTO @BatchProfessionTypes ([Name])
	Values('Accountant'),('Real Estate Agent')
	Execute [dbo].[UserProfiles_Insert]		 
			@LocationTypeId
			,@LineOne 
			,@LineTwo
			,@City 
			,@Zip 
			,@StateId 
			,@Latitude 
			,@Longitude 

			,@UserId 
			,@BatchProfessionTypes

			,@FirstName 
			,@LastName 
			,@Mi
			,@AvatarUrl 
			,@DOB 
			,@Email
			,@Phone
			,@YearsOfExperience 
			,@DesiredHourlyRate 
			,@IsActive
			,@Id OUTPUT

*/ ---- TEST CODE ----

AS

BEGIN

	DECLARE	 @LocationId int 

	INSERT INTO		[dbo].[Locations] 
					(LocationTypeId
					,LineOne
					,LineTwo
					,City
					,Zip
					,StateId
					,Latitude
					,Longitude
					,CreatedBy
					,ModifiedBy)

		VALUES		(@LocationTypeId
					,@LineOne 
					,@LineTwo
					,@City 
					,@Zip 
					,@StateId 
					,@Latitude 
					,@Longitude 
					,@UserId
					,@UserId 
					)
 
		 SET @LocationId = SCOPE_IDENTITY();

	INSERT INTO [dbo].[UserProfiles]
			   ([UserId]
			   ,[LocationId]
			   ,[FirstName]
			   ,[LastName]
			   ,[Mi]
			   ,[AvatarUrl]
			   ,[DOB]
			   ,[Email]
			   ,[Phone]
			   ,[YearsOfExperience]
			   ,[DesiredHourlyRate]
			   ,[IsActive])
		   
		 VALUES
			   (@UserId
				,@LocationId
				,@FirstName
				,@LastName
				,@Mi
				,@AvatarUrl
				,@DOB
				,@Email
				,@Phone
				,@YearsOfExperience
				,@DesiredHourlyRate
				,@IsActive
				)
		SET @Id = SCOPE_IDENTITY()
	
	 DELETE FROM dbo.UserProfessionTypes
	 WHERE UserId = @UserId

	 INSERT INTO dbo.ProfessionTypes ([Name])
			SELECT b.[Name]
			FROM @BatchProfessionTypes AS b
			WHERE NOT Exists(SELECT pt.Id FROM dbo.ProfessionTypes AS pt
							WHERE b.Name = pt.Name)

	INSERT INTO dbo.UserProfessionTypes(UserId,ProfessionTypeId)
				(SELECT @UserId, pt.Id
				FROM dbo.ProfessionTypes AS pt
				
				WHERE Exists(SELECT 1
							FROM @BatchProfessionTypes AS bt
							WHERE pt.[Name] = bt.[Name])
							
							)

END



GO
/****** Object:  StoredProcedure [dbo].[UserProfiles_InsertV2]    Script Date: 12/2/2022 3:13:23 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author: <Rebekah Hoskins>
-- Create date: <8/31/2022>
-- Description: <user profile insert as a transaction with rollback>
-- Code Reviewer:

-- MODIFIED BY: author
-- MODIFIED DATE:12/1/2020
-- Code Reviewer:
-- Note:
-- =============================================

CREATE PROC [dbo].[UserProfiles_InsertV2]
		@LocationTypeId int
		,@LineOne nvarchar(255)
		,@LineTwo nvarchar(255)
		,@City nvarchar(225)
		,@Zip nvarchar(50)
		,@StateId int
		,@Latitude float 
		,@Longitude float

		,@UserId int

		,@BatchProfessionTypes dbo.UserProfessionTypes READONLY

		,@FirstName nvarchar(100)
		,@LastName nvarchar(100)
		,@Mi nvarchar(2)
		,@AvatarUrl nvarchar(255)
		,@DOB datetime2(7)
		,@Email nvarchar(100)
		,@Phone nvarchar(20)
		,@YearsOfExperience nvarchar(10)
		,@DesiredHourlyRate nvarchar(10)
		,@IsActive bit
		,@Id int OUTPUT

		,@BatchLicense dbo.LicenseV2 READONLY

AS

/*-----TEST CODE-----

	DECLARE @LocationTypeId int = 5
			,@LineOne nvarchar(255) = 'LineOne Insert'
			,@LineTwo nvarchar(255) = 'LineTwo Insert'
			,@City nvarchar(225) = 'City Insert'
			,@Zip nvarchar(50) = '123123'
			,@StateId int = 24
			,@Latitude float = '34.4856'
			,@Longitude float = '45.5484'
	
			,@UserId int = 16
			,@FirstName nvarchar(100) = 'TestUserFirstNameNew'
			,@LastName nvarchar(100) = 'TestUserLastNameNew'
			,@Mi nvarchar(2) = 'T'
			,@AvatarUrl nvarchar(255) = 'TestImageUrl.com'
			,@DOB datetime2(7) = '1955-05-05'
			,@Email nvarchar(100) = 'testuseremail@gmail.com'
			,@Phone nvarchar(20) = '1234567890'
			,@YearsOfExperience nvarchar(10) = '10'
			,@DesiredHourlyRate nvarchar(10) = '100'
			,@IsActive bit = 1
			,@Id int = 0

			
	Declare @BatchProfessionTypes dbo.UserProfessionTypes
	INSERT INTO @BatchProfessionTypes ([Name])
	Values('Accountant'),('Real Estate Agent')

	Declare @BatchLicense dbo.LicenseV2
	INSERT INTO @BatchLicense
				([LicenseStateId]
			   ,[LicenseTypeId]
			   ,[LicenseNumber]
			   ,[CreateBy]
			   ,[DateExpires])
	Values(1, 2, 'M003', @UserId, '01-01-2023')

	sELECT *
	FROM @BatchLicense


	EXECUTE [UserProfiles_InsertV2]
		@LocationTypeId
		,@LineOne
		,@LineTwo
		,@City
		,@Zip
		,@StateId
		,@Latitude 
		,@Longitude

		,@UserId

		,@BatchProfessionTypes

		,@FirstName
		,@LastName
		,@Mi
		,@AvatarUrl
		,@DOB
		,@Email
		,@Phone
		,@YearsOfExperience
		,@DesiredHourlyRate
		,@IsActive
		,@Id OUTPUT

		,@BatchLicense

	SELECT *
	FROM dbo.UserProfiles
	Where UserId = 16

	SELECT *
	FROM dbo.Licenses AS li INNER JOIN dbo.UsersLicenses AS ul
		ON li.Id = ul.LicenseId
	WHERE 16 = ul.UserId

	SELECT *
	FROM dbo.UsersLicenses
	WHERE 16 = UserId
-----END TEST CODE-----
*/

SET XACT_ABORT ON
Declare @Tran nvarchar(50)  = '_userProfile_InsertV2'

BEGIN TRY

BEGIN Transaction @Tran

-- your code here
DECLARE @LocationId int = 0

INSERT INTO		[dbo].[Locations] 
					(LocationTypeId
					,LineOne
					,LineTwo
					,City
					,Zip
					,StateId
					,Latitude
					,Longitude
					,CreatedBy
					,ModifiedBy)

		VALUES		(@LocationTypeId
					,@LineOne 
					,@LineTwo
					,@City 
					,@Zip 
					,@StateId 
					,@Latitude 
					,@Longitude 
					,@UserId
					,@UserId 
					)
 
		 SET @LocationId = SCOPE_IDENTITY();

	INSERT INTO [dbo].[UserProfiles]
			   ([UserId]
			   ,[LocationId]
			   ,[FirstName]
			   ,[LastName]
			   ,[Mi]
			   ,[AvatarUrl]
			   ,[DOB]
			   ,[Email]
			   ,[Phone]
			   ,[YearsOfExperience]
			   ,[DesiredHourlyRate]
			   ,[IsActive])
		   
		 VALUES
			   (@UserId
				,@LocationId
				,@FirstName
				,@LastName
				,@Mi
				,@AvatarUrl
				,@DOB
				,@Email
				,@Phone
				,@YearsOfExperience
				,@DesiredHourlyRate
				,@IsActive
				)
		SET @Id = SCOPE_IDENTITY()
	
	 DELETE FROM dbo.UserProfessionTypes
	 WHERE UserId = @UserId

	 INSERT INTO dbo.ProfessionTypes ([Name])
			SELECT b.[Name]
			FROM @BatchProfessionTypes AS b
			WHERE NOT Exists(SELECT pt.Id FROM dbo.ProfessionTypes AS pt
							WHERE b.Name = pt.Name)

	INSERT INTO dbo.UserProfessionTypes(UserId,ProfessionTypeId)
				(SELECT @UserId, pt.Id
				FROM dbo.ProfessionTypes AS pt
				
				WHERE Exists(SELECT 1
							FROM @BatchProfessionTypes AS bt
							WHERE pt.[Name] = bt.[Name])
							
							)
							
	DELETE FROM [dbo].[UsersLicenses]
	WHERE @UserId = UserId

	INSERT INTO [dbo].[Licenses]
			   ([LicenseStateId]
			   ,[LicenseTypeId]
			   ,[LicenseNumber]
			   ,[DateExpires]
			   ,[CreatedBy]
			   ,[DateCreated])
		SELECT b.[LicenseStateId]
			 ,b.[LicenseTypeId]
			 ,b.[LicenseNumber]
			 ,b.[DateExpires]
			 ,@UserId
			 ,getutcdate()
		FROM @BatchLicense AS b
		WHERE NOT EXISTS (SELECT 1
							FROM [dbo].[Licenses] AS l 
							WHERE l.CreatedBy = b.CreateBy)

	INSERT INTO [dbo].[UsersLicenses]
				([LicenseId]
				,[UserId])
			(SELECT l.Id, @UserId
			FROM [dbo].[Licenses] AS l
			WHERE Exists(SELECT 1
						FROM @BatchLicense AS bl
						WHERE @UserId = bl.CreateBy))

Commit Transaction @Tran

END TRY
BEGIN Catch



    IF (XACT_STATE()) = -1
    BEGIN
        PRINT 'The transaction is in an uncommittable state.' +
              ' Rolling back transaction.'
        ROLLBACK TRANSACTION @Tran;;
    END;

    -- Test whether the transaction is active and valid.
    IF (XACT_STATE()) = 1
    BEGIN
        PRINT 'The transaction is committable.' +
              ' Committing transaction.'
        COMMIT TRANSACTION @Tran;;
    END;

        -- If you want to see error info
       -- SELECT
        --ERROR_NUMBER() AS ErrorNumber,
        --ERROR_SEVERITY() AS ErrorSeverity,
        --ERROR_STATE() AS ErrorState,
       -- ERROR_PROCEDURE() AS ErrorProcedure,
       -- ERROR_LINE() AS ErrorLine,
       -- ERROR_MESSAGE() AS ErrorMessage

-- to just get the error thrown and see the bad news as an exception
    THROW

End Catch




SET XACT_ABORT OFF

GO
/****** Object:  StoredProcedure [dbo].[UserProfiles_Select_ByCreatedBy]    Script Date: 12/2/2022 3:13:23 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author: <Rebekah Hoskins>
-- Create date: <20220817>
-- Description: <UserProfiles_Select_ByCreatedBy>
-- Code Reviewer:

-- MODIFIED BY: author
-- MODIFIED DATE:12/1/2020
-- Code Reviewer:
-- Note:
-- =============================================

CREATE proc [dbo].[UserProfiles_Select_ByCreatedBy]
		@PageIndex int
		,@PageSize int
		,@CreatedBy int

/* ---- TEST CODE ----

	Declare @PageIndex int = 0
			,@PageSize int = 20
			,@CreatedBy int = 50

	Execute [dbo].[UserProfiles_Select_ByCreatedBy]
			@PageIndex
			,@PageSize
			,@CreatedBy

*/ ---- TEST CODE ----

AS 

BEGIN
	Declare @offset int = @PageIndex * @PageSize

	SELECT up.[Id]
		,up.[UserId]
		,up.[FirstName]
		,up.[LastName]
		,up.[Mi]
		,up.[AvatarUrl]
		,up.[DOB]
		,up.[Email]
		,up.[Phone]
		,up.[YearsOfExperience]
		,up.[DesiredHourlyRate]
		,up.[IsActive]
		,up.[DateCreated]
		,up.[DateModified]
		,l.[Id] as LocationId
		,l.[Id] as LocationTypeId
		,lt.[Name] as LocationTypeName
		,l.[LineOne]
		,l.[LineTwo]
		,l.[City]
		,l.[Zip]
		,s.[Id] as StateId
		,s.[Code] as StateCode
		,s.[Name] as StateName
		,l.[Latitude]
		,l.[Longitude]
		,License = (SELECT li.[LicenseStateId]
					,li.[LicenseTypeId]
					,li.[LicenseNumber]
					,li.[DateExpires]
					,li.[CreatedBy]
		FROM dbo.Licenses AS li INNER JOIN dbo.UsersLicenses AS ul
			ON li.Id = ul.LicenseId
		WHERE up.UserId = ul.UserId FOR JSON AUTO)
		,Professions = (SELECT pt.Id, pt.Name
					from dbo.ProfessionTypes as pt INNER JOIN dbo.UserProfessionTypes as upt
					ON pt.Id = upt.ProfessionTypeId
					WHERE upt.UserId = up.UserId FOR JSON AUTO
					)
		,TotalCount = COUNT(1) OVER()

	FROM [dbo].[UserProfiles] as up inner join dbo.Locations as l
								on l.Id = up.LocationId
								inner join dbo.Users as u
								on u.Id = up.UserId
								inner join dbo.LocationTypes as lt
								on lt.Id = l.LocationTypeId
								inner join dbo.States as s
								on s.Id = l.StateId

		Where up.UserId = @CreatedBy
		ORDER BY up.Id

		OFFSET @offset Rows
		FETCH Next @PageSize Rows ONLY

END


GO
/****** Object:  StoredProcedure [dbo].[UserProfiles_Select_ById]    Script Date: 12/2/2022 3:13:23 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

-- =============================================
-- Author: <Rebekah Hoskins>
-- Create date: <20220817>>
-- Description: <UserProfiles_Select_ById>
-- Code Reviewer:

-- MODIFIED BY:
-- MODIFIED DATE: 
-- Code Reviewer:
-- Note: Select By User Id
-- =============================================

CREATE proc [dbo].[UserProfiles_Select_ById]
		@UserId int

/* ---- TEST CODE ----

	Declare @UserId int = 135;
	Execute dbo.UserProfiles_Select_ById
			@UserId

*/ ---- TEST CODE ----

AS 

BEGIN

	SELECT up.[Id]
		,up.[UserId]
		,up.[FirstName]
		,up.[LastName]
		,up.[Mi]
		,up.[AvatarUrl]
		,up.[DOB]
		,up.[Email]
		,up.[Phone]
		,up.[YearsOfExperience]
		,up.[DesiredHourlyRate]
		,up.[IsActive]
		,up.[DateCreated]
		,up.[DateModified]
		,l.[Id] as LocationId
		,l.[Id] as LocationTypeId
		,lt.[Name] as LocationTypeName
		,l.[LineOne]
		,l.[LineTwo]
		,l.[City]
		,l.[Zip]
		,s.[Id] as StateId
		,s.[Code] as StateCode
		,s.[Name] as StateName
		,l.[Latitude]
		,l.[Longitude]
		,License = (SELECT li.[LicenseStateId]
					,li.[LicenseTypeId]
					,li.[LicenseNumber]
					,li.[DateExpires]
					,li.[CreatedBy]
		FROM dbo.Licenses AS li INNER JOIN dbo.UsersLicenses AS ul
			ON li.Id = ul.LicenseId
		WHERE up.UserId = ul.UserId FOR JSON AUTO)
		,Professions = (SELECT pt.Id, pt.Name
					from dbo.ProfessionTypes as pt INNER JOIN dbo.UserProfessionTypes as upt
					ON pt.Id = upt.ProfessionTypeId
					WHERE upt.UserId = up.UserId FOR JSON AUTO
					)

	FROM [dbo].[UserProfiles] as up inner join dbo.Locations as l
								on l.Id = up.LocationId
								inner join dbo.Users as u
								on u.Id = up.UserId
								inner join dbo.LocationTypes as lt
								on lt.Id = l.LocationTypeId
								inner join dbo.States as s
								on s.Id = l.StateId

	Where up.UserId = @UserId 

END


GO
/****** Object:  StoredProcedure [dbo].[UserProfiles_SelectAll]    Script Date: 12/2/2022 3:13:23 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

-- =============================================
-- Author: <Rebekah Hoskins>
-- Create date: <20220817>
-- Description: <UserProfiles_SelectAll>
-- Code Reviewer:

-- MODIFIED BY: author
-- MODIFIED DATE:12/1/2020
-- Code Reviewer:
-- Note:
-- =============================================

CREATE proc [dbo].[UserProfiles_SelectAll]
					@PageIndex int
					,@PageSize int

/* ---- TEST CODE ----
	
Declare @PageIndex int = 0
			,@PageSize int = 20

Execute [dbo].[UserProfiles_SelectAll]
				@PageIndex 
				,@PageSize 

*/ ---- TEST CODE ----

AS

BEGIN

	Declare @offset int = @PageIndex * @PageSize

	SELECT up.[Id]
		,up.[UserId]
		,up.[FirstName]
		,up.[LastName]
		,up.[Mi]
		,up.[AvatarUrl]
		,up.[DOB]
		,up.[Email]
		,up.[Phone]
		,up.[YearsOfExperience]
		,up.[DesiredHourlyRate]
		,up.[IsActive]
		,up.[DateCreated]
		,up.[DateModified]
		,l.[Id] as LocationId
		,l.[Id] as LocationTypeId
		,lt.[Name] as LocationTypeName
		,l.[LineOne]
		,l.[LineTwo]
		,l.[City]
		,l.[Zip]
		,s.[Id] as StateId
		,s.[Code] as StateCode
		,s.[Name] as StateName
		,l.[Latitude]
		,l.[Longitude]
		,License = (SELECT li.[LicenseStateId]
					,li.[LicenseTypeId]
					,li.[LicenseNumber]
					,li.[DateExpires]
					,li.[CreatedBy]
		FROM dbo.Licenses AS li INNER JOIN dbo.UsersLicenses AS ul
			ON li.Id = ul.LicenseId
		WHERE up.UserId = ul.UserId FOR JSON AUTO)
		,Professions = (SELECT pt.Id, pt.Name
					from dbo.ProfessionTypes as pt INNER JOIN dbo.UserProfessionTypes as upt
					ON pt.Id = upt.ProfessionTypeId
					WHERE upt.UserProfileId = up.UserId FOR JSON AUTO
					)
		,TotalCount = COUNT(1) OVER()

	FROM [dbo].[UserProfiles] as up inner join dbo.Locations as l
								on l.Id = up.LocationId
								inner join dbo.Users as u
								on u.Id = up.UserId
								inner join dbo.LocationTypes as lt
								on lt.Id = l.LocationTypeId
								inner join dbo.States as s
								on s.Id = l.StateId

	ORDER BY up.Id

	OFFSET @offSet Rows
	Fetch Next @PageSize Rows ONLY

END


GO
/****** Object:  StoredProcedure [dbo].[UserProfiles_Update]    Script Date: 12/2/2022 3:13:23 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

-- =============================================
-- Author: <Hyun Kim>
-- Create date: <7/3/22>
-- Description: <UserProfiles_Update>
-- Code Reviewer:

-- MODIFIED BY: author
-- MODIFIED DATE:12/1/2020
-- Code Reviewer:
-- Note:
-- =============================================

CREATE proc [dbo].[UserProfiles_Update]
			@LocationTypeId int
			,@LineOne nvarchar(255)
			,@LineTwo nvarchar(255)
			,@City nvarchar(225)
			,@Zip nvarchar(50)
			,@StateId int
			,@Latitude float 
			,@Longitude float

			,@UserId int
			,@FirstName nvarchar(100)
			,@LastName nvarchar(100)
			,@Mi nvarchar(2)
			,@AvatarUrl nvarchar(255)
			,@DOB datetime2(7)
			,@Email nvarchar(100)
			,@Phone nvarchar(20)
			,@YearsOfExperience nvarchar(10)
			,@DesiredHourlyRate nvarchar(10)
			,@IsActive bit
			,@BatchProfessionTypes dbo.UserProfessionTypes READONLY
			,@Id int 


AS

/* ---- TEST CODE ----

Declare @Id int = 42		

	Declare @UserId int = 135
			,@FirstName nvarchar(100) = 'Dave'
			,@LastName nvarchar(100) = 'Johnson'
			,@Mi nvarchar(2) = 'T'
			,@LocationId int = 3
			,@AvatarUrl nvarchar(255) = 'https://bit.ly/3PgKxrQ'
			--,@ProfessionTypeId int = 1
			,@DOB datetime2(7) = '1955-05-05'
			,@Email nvarchar(100) = 'yeraga1270@teasya.com'
			,@Phone nvarchar(20) = '474 456-7890'
			,@LicenseNumber nvarchar(50) = 'SOEJ23J97'
			,@YearsOfExperience nvarchar(10) = '10'
			,@DesiredHourlyRate nvarchar(10) = '100'
			,@IsActive bit = 1

	Execute [dbo].[UserProfiles_Update]		 
			@Id
			,@UserId 
			,@FirstName 
			,@LastName 
			,@Mi
			,@LocationId 
			,@AvatarUrl 
			--,@ProfessionTypeId 
			,@DOB 
			,@Email
			,@Phone
			,@LicenseNumber 
			,@YearsOfExperience 
			,@DesiredHourlyRate 
			,@IsActive 


			Select @Id

			Select *
			From [dbo].[UserProfiles]

			Where Id = @Id

*/ ---- TEST CODE ----

BEGIN

	DECLARE	 @LocationId int 

	INSERT INTO	[dbo].[Locations] (
					LocationTypeId,
					LineOne,
					LineTwo,
					City,
					Zip
					,StateId
					,Latitude
					,Longitude
					,CreatedBy
					,ModifiedBy
					)
		VALUES			(@LocationTypeId
					,@LineOne 
					,@LineTwo
					,@City 
					,@Zip 
					,@StateId 
					,@Latitude 
					,@Longitude 
					,@UserId
					,@UserId 
					)
 
		 SET @LocationId = SCOPE_IDENTITY();

	UPDATE [dbo].[UserProfiles]

	   SET [UserId] = @UserId
		  ,[FirstName] = @FirstName
		  ,[LastName] = @LastName
		  ,[Mi] = @Mi
		  ,[LocationId] = @LocationId 
		  ,[AvatarUrl] = @AvatarUrl
		  ,[DOB] = @DOB
		  ,[Email] = @Email
		  ,[Phone] = @Phone
		  ,[YearsOfExperience] = @YearsOfExperience
		  ,[DesiredHourlyRate] = @DesiredHourlyRate
		  ,[IsActive] = @IsActive
	 WHERE Id = @Id

	 DELETE FROM dbo.UserProfessionTypes
	 WHERE UserId = @UserId

	 INSERT INTO dbo.ProfessionTypes ([Name])
			SELECT b.[Name]
			FROM @BatchProfessionTypes AS b
			WHERE NOT Exists(SELECT pt.Id FROM dbo.ProfessionTypes AS pt
							WHERE b.Name = pt.Name)

	INSERT INTO dbo.UserProfessionTypes(UserId,ProfessionTypeId)
				(SELECT @UserId, pt.Id
				FROM dbo.ProfessionTypes AS pt
				
				WHERE Exists(SELECT 1
							FROM @BatchProfessionTypes AS bt
							WHERE pt.[Name] = bt.[Name])
							
							)
END
GO
/****** Object:  StoredProcedure [dbo].[UserProfiles_UpdateV2]    Script Date: 12/2/2022 3:13:23 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author: <Rebekah Hoskins>
-- Create date: <8/31/2022>
-- Description: <user profile update as a transaction with rollback>
-- Code Reviewer:

-- MODIFIED BY: author
-- MODIFIED DATE:12/1/2020
-- Code Reviewer:
-- Note:
-- =============================================

CREATE PROC [dbo].[UserProfiles_UpdateV2]
		@LocationTypeId int
		,@LineOne nvarchar(255)
		,@LineTwo nvarchar(255)
		,@City nvarchar(225)
		,@Zip nvarchar(50)
		,@StateId int
		,@Latitude float 
		,@Longitude float

		,@UserId int

		,@BatchProfessionTypes dbo.UserProfessionTypes READONLY

		,@FirstName nvarchar(100)
		,@LastName nvarchar(100)
		,@Mi nvarchar(2)
		,@AvatarUrl nvarchar(255)
		,@DOB datetime2(7)
		,@Email nvarchar(100)
		,@Phone nvarchar(20)
		,@YearsOfExperience nvarchar(10)
		,@DesiredHourlyRate nvarchar(10)
		,@IsActive bit
		,@Id int OUTPUT

		,@BatchLicense dbo.LicenseV2 READONLY

AS

/*-----TEST CODE-----

	DECLARE @LocationTypeId int = 5
			,@LineOne nvarchar(255) = 'LineOne Insert'
			,@LineTwo nvarchar(255) = 'LineTwo Insert'
			,@City nvarchar(225) = 'City Insert'
			,@Zip nvarchar(50) = '123123'
			,@StateId int = 24
			,@Latitude float = '34.4856'
			,@Longitude float = '45.5484'
	
			,@UserId int = 135
			,@FirstName nvarchar(100) = 'TestUserFirstNameNew'
			,@LastName nvarchar(100) = 'TestUserLastNameNew'
			,@Mi nvarchar(2) = 'T'
			,@AvatarUrl nvarchar(255) = 'TestImageUrl.com'
			,@DOB datetime2(7) = '1955-05-05'
			,@Email nvarchar(100) = 'testuseremail@gmail.com'
			,@Phone nvarchar(20) = '1234567890'
			,@YearsOfExperience nvarchar(10) = '10'
			,@DesiredHourlyRate nvarchar(10) = '100'
			,@IsActive bit = 1
			,@Id int = 91

			
	Declare @BatchProfessionTypes dbo.UserProfessionTypes
	INSERT INTO @BatchProfessionTypes ([Name])
	Values('Accountant'),('Real Estate Agent')

	Declare @BatchLicense dbo.LicenseV2
	INSERT INTO @BatchLicense
				([LicenseStateId]
			   ,[LicenseTypeId]
			   ,[LicenseNumber]
			   ,[CreateBy]
			   ,[DateExpires])
	Values(1, 2, 'M003', @UserId, '01-01-2023')

	sELECT *
	FROM @BatchLicense


	EXECUTE [UserProfiles_UpdateV2]
		@LocationTypeId
		,@LineOne
		,@LineTwo
		,@City
		,@Zip
		,@StateId
		,@Latitude 
		,@Longitude

		,@UserId

		,@BatchProfessionTypes

		,@FirstName
		,@LastName
		,@Mi
		,@AvatarUrl
		,@DOB
		,@Email
		,@Phone
		,@YearsOfExperience
		,@DesiredHourlyRate
		,@IsActive
		,@Id OUTPUT

		,@BatchLicense

	SELECT *
	FROM dbo.UserProfiles
	Where UserId = 16

	SELECT *
	FROM dbo.Licenses AS li INNER JOIN dbo.UsersLicenses AS ul
		ON li.Id = ul.LicenseId
	WHERE 135 = ul.UserId

	SELECT *
	FROM dbo.UsersLicenses
	WHERE 135 = UserId
-----END TEST CODE-----
*/

SET XACT_ABORT ON
Declare @Tran nvarchar(50)  = '_userProfile_UpdateV2'

BEGIN TRY

BEGIN Transaction @Tran

-- your code here
DECLARE @LocationId int = 0

	INSERT INTO		[dbo].[Locations] 
					(LocationTypeId
					,LineOne
					,LineTwo
					,City
					,Zip
					,StateId
					,Latitude
					,Longitude
					,CreatedBy
					,ModifiedBy)

		VALUES		(@LocationTypeId
					,@LineOne 
					,@LineTwo
					,@City 
					,@Zip 
					,@StateId 
					,@Latitude 
					,@Longitude 
					,@UserId
					,@UserId 
					)
 
			SET @LocationId = SCOPE_IDENTITY();

	UPDATE [dbo].[UserProfiles]

	SET [UserId] = @UserId
		,[LocationId] = @LocationId
		,[FirstName] = @FirstName
		,[LastName] = @LastName
		,[Mi] = @Mi
		,[AvatarUrl] = @AvatarUrl
		,[DOB] = @DOB
		,[Email] = @Email
		,[Phone] = @Phone
		,[YearsOfExperience] = @YearsOfExperience
		,[DesiredHourlyRate] = @DesiredHourlyRate
		,[IsActive] = @IsActive
	WHERE Id = @Id
	
	 DELETE FROM dbo.UserProfessionTypes
	 WHERE UserId = @UserId

	 INSERT INTO dbo.ProfessionTypes ([Name])
			SELECT b.[Name]
			FROM @BatchProfessionTypes AS b
			WHERE NOT Exists(SELECT pt.Id FROM dbo.ProfessionTypes AS pt
							WHERE b.Name = pt.Name)

	INSERT INTO dbo.UserProfessionTypes(UserId,ProfessionTypeId)
				(SELECT @UserId, pt.Id
				FROM dbo.ProfessionTypes AS pt
				
				WHERE Exists(SELECT 1
							FROM @BatchProfessionTypes AS bt
							WHERE pt.[Name] = bt.[Name])
							)
							
	DELETE FROM [dbo].[UsersLicenses]
	WHERE @UserId = UserId

	INSERT INTO [dbo].[Licenses]
			   ([LicenseStateId]
			   ,[LicenseTypeId]
			   ,[LicenseNumber]
			   ,[DateExpires]
			   ,[CreatedBy]
			   ,[DateCreated])
		SELECT b.[LicenseStateId]
			 ,b.[LicenseTypeId]
			 ,b.[LicenseNumber]
			 ,b.[DateExpires]
			 ,@UserId
			 ,getutcdate()
		FROM @BatchLicense AS b
		WHERE NOT EXISTS (SELECT 1
							FROM [dbo].[Licenses] AS l 
							WHERE l.CreatedBy = b.CreateBy)

	INSERT INTO [dbo].[UsersLicenses]
				([LicenseId]
				,[UserId])
			(SELECT l.Id, @UserId
			FROM [dbo].[Licenses] AS l
			WHERE Exists(SELECT 1
						FROM @BatchLicense AS bl
						WHERE @UserId = bl.CreateBy))

Commit Transaction @Tran

END TRY
BEGIN Catch



    IF (XACT_STATE()) = -1
    BEGIN
        PRINT 'The transaction is in an uncommittable state.' +
              ' Rolling back transaction.'
        ROLLBACK TRANSACTION @Tran;;
    END;

    -- Test whether the transaction is active and valid.
    IF (XACT_STATE()) = 1
    BEGIN
        PRINT 'The transaction is committable.' +
              ' Committing transaction.'
        COMMIT TRANSACTION @Tran;;
    END;

        -- If you want to see error info
       -- SELECT
        --ERROR_NUMBER() AS ErrorNumber,
        --ERROR_SEVERITY() AS ErrorSeverity,
        --ERROR_STATE() AS ErrorState,
       -- ERROR_PROCEDURE() AS ErrorProcedure,
       -- ERROR_LINE() AS ErrorLine,
       -- ERROR_MESSAGE() AS ErrorMessage

-- to just get the error thrown and see the bad news as an exception
    THROW

End Catch




SET XACT_ABORT OFF

GO
/****** Object:  StoredProcedure [dbo].[UserRoles_Insert]    Script Date: 12/2/2022 3:13:23 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author: <Justin Solo>
-- Create date: <07/06/2022>
-- Description: <Users_UserRoles_Insert>
-- Code Reviewer:

-- MODIFIED BY: author
-- MODIFIED DATE:12/1/2020
-- Code Reviewer:
-- Note:
-- =============================================

CREATE PROC [dbo].[UserRoles_Insert]
	@UserId int
	,@RoleId int

AS

/* ----- TEST CODE -----

	DECLARE @UserId int = 23
			,@Role int = 1
	
	EXECUTE dbo.UserRoles_Insert @UserId, @Role

*/ ----- END TEST CODE -----


BEGIN

	INSERT INTO [dbo].[UserRoles]
			   ([UserId]
			   ,[RoleId])
		 VALUES
			   (@UserId
			   ,@RoleId)

END
GO
/****** Object:  StoredProcedure [dbo].[Users_Confirm]    Script Date: 12/2/2022 3:13:23 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author: <Justin Solo>
-- Create date: <07/07/2022>
-- Description: <Users_Confirm>
-- Code Reviewer:

-- MODIFIED BY: author
-- MODIFIED DATE:12/1/2020
-- Code Reviewer:
-- Note:
-- =============================================

CREATE PROC [dbo].[Users_Confirm]
	@Id int
	,@IsConfirmed int

AS

/* ----- TEST CODE -----

	DECLARE @Id int = 27
	
	DECLARE @IsConfirmed int = 5

	EXECUTE dbo.Users_Confirm	@Id
								,@IsConfirmed

*/ ----- END TEST CODE -----

BEGIN

	UPDATE [dbo].[Users]
	   SET [IsConfirmed] = @IsConfirmed
		  ,[DateModified] = GETUTCDATE()
	 WHERE Id = @Id

END
GO
/****** Object:  StoredProcedure [dbo].[Users_CountAll_Period]    Script Date: 12/2/2022 3:13:23 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author: <Lucas Gil>
-- Create date: <07/19/2022>
-- Description: <[dbo].[Users_SelectAll]>
-- Code Reviewer:

-- MODIFIED BY: author
-- MODIFIED DATE:12/1/2020
-- Code Reviewer:
-- Note:
-- =============================================

CREATE PROC [dbo].[Users_CountAll_Period]
AS
/*  -------TEST CODE---------

	EXECUTE [dbo].[Users_CountAll_Period]

*/ ----- END TEST CODE -----
BEGIN

DECLARE 
	@dateRange INT = 7
	,@now DATETIME2(7) = GETUTCDATE()
	,@isDailyCount BIT = 1
	,@dayOfTheYeat INT
	--------------------------

DECLARE 
		@beginDate DATETIME2(7) = DATEADD(DAY, -@dateRange, @now)
DECLARE 
		@userDates TABLE (dayInYear int)

    ------------------------

	--TESTING THE VARIABLES VALUES--

	--SELECT @now, @beginDate, @dateRange, @isDailyCount

	-------------------------

	INSERT INTO @userDates
				(dayInYear)
	SELECT top 1000
				DATEPART(DAYOFYEAR, DateCreated)
	FROM [dbo].[Users]
	WHERE DateCreated >= @beginDate
	
	SELECT TOP 1000
		total = COUNT(1)
		,dayInYear
		,DateOfTheYear = DATEADD(DAY, dayInYear-1, DATEADD(YEAR,2022-1900,0))
		
	FROM @userDates
	GROUP BY dayInYear

END
GO
/****** Object:  StoredProcedure [dbo].[Users_Current]    Script Date: 12/2/2022 3:13:23 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author: <Rebekah Hoskins>
-- Create date: <2022/08/06>
-- Description: <UserProfiles_Insert>
-- Code Reviewer:

-- MODIFIED BY:
-- MODIFIED DATE:
-- Code Reviewer:
-- Note:
-- =============================================

CREATE PROC [dbo].[Users_Current]
		@UserId int

/*-----TEST CODE-----

	DECLARE @UserId = 135

	EXECUTE [dbo].[Users_Current]
		@UserId

-----END TEST CODE-----
*/

AS

BEGIN 

	SELECT 
		CASE WHEN EXISTS 
		(
			SELECT *
			FROM dbo.UserProfiles
			WHERE UserId = @UserId
		)

	THEN CAST (1 AS BIT)
	ELSE CAST (0 AS BIT)

END
END
GO
/****** Object:  StoredProcedure [dbo].[Users_Insert]    Script Date: 12/2/2022 3:13:23 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author: <Justin Solo>
-- Create date: <07/01/2022>
-- Description: <User_Insert>
-- Code Reviewer:

-- MODIFIED BY: author
-- MODIFIED DATE:12/1/2020
-- Code Reviewer:
-- Note:
-- =============================================


CREATE PROC [dbo].[Users_Insert]
	@Email nvarchar(100)
	,@Password nvarchar(100)
	,@IsConfirmed bit
	,@UserStatusId int
	,@Id int OUTPUT

AS

/* ----- TEST CODE -----

	DECLARE @Id int = 1
	
	DECLARE @Email nvarchar(100) = 'test@testuser.com'
			,@Password nvarchar(100) = 'testuser'
			,@IsConfirmed bit = 1
			,@UserStatusId int = 1

	EXECUTE dbo.Users_Insert @Email
							,@Password
							,@IsConfirmed
							,@UserStatusId
							,@Id

*/ ----- END TEST CODE -----

BEGIN

	IF NOT EXISTS (select * FROM dbo.Users AS U
					WHERE U.Email = @Email)
	INSERT INTO [dbo].[Users]
			   ([Email]
			   ,[Password]
			   ,[IsConfirmed]
			   ,[UserStatusId])
		 VALUES
			   (@Email
			   ,@Password
			   ,@IsConfirmed
			   ,@UserStatusId)
		SET		@Id = SCOPE_IDENTITY()

END



GO
/****** Object:  StoredProcedure [dbo].[Users_InsertV2]    Script Date: 12/2/2022 3:13:23 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author: <Justin Solo>
-- Create date: <07/06/2022>
-- Description: <User_InsertV2>
-- Code Reviewer:

-- MODIFIED BY: author
-- MODIFIED DATE:12/1/2020
-- Code Reviewer:
-- Note:
-- =============================================


CREATE PROC [dbo].[Users_InsertV2]
								@Email nvarchar(100)
								,@Password nvarchar(100)
								,@IsConfirmed bit
								,@UserStatusId int
								,@RoleId int
								,@Id int OUTPUT

AS

/* ----- TEST CODE -----

	DECLARE @Id int = 1

	DECLARE @Email nvarchar(100) = 'yawilep863@otodir.com'
			,@Password nvarchar(100) = 'CnmProTestUser1!'
			,@IsConfirmed bit = 1
			,@UserStatusId int = 1
			,@Role int = 1

	EXECUTE dbo.Users_InsertV2 @Email
								,@Password
								,@IsConfirmed
								,@UserStatusId
								,@Role
								,@Id OUTPUT


								select * from dbo.Users


*/ ----- END TEST CODE -----

BEGIN

	IF NOT EXISTS (select * FROM dbo.Users AS U
					WHERE U.Email = @Email)

	INSERT INTO [dbo].[Users]
			   ([Email]
			   ,[Password]
			   ,[IsConfirmed]
			   ,[UserStatusId])
		 VALUES
			   (@Email
			   ,@Password
			   ,@IsConfirmed
			   ,@UserStatusId)
		SET		@Id = SCOPE_IDENTITY()

	EXECUTE Dbo.UserRoles_Insert @Id, @RoleId

END
GO
/****** Object:  StoredProcedure [dbo].[Users_Select_AuthData]    Script Date: 12/2/2022 3:13:23 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Justin Solo>
-- Create date: <07/08/2022>
-- Description:	<Users_Select_AuthData>
-- Code Reviewer:


-- MODIFIED BY: author
-- MODIFIED DATE:12/1/2020
-- Code Reviewer: 
-- Note: 
-- =============================================
CREATE PROC [dbo].[Users_Select_AuthData]
	@Email nvarchar(100)

AS

/* ----- TEST CODE -----

	DECLARE @Email nvarchar(100) = 'testinguser1@gmail.com'
	
	EXECUTE dbo.Users_Select_AuthData @Email

*/ ----- END TEST CODE -----

BEGIN

	SELECT [U].[Id]
		  ,[U].[Email]
		  ,[U].[Password]
		  ,R.[Name]
	  FROM [dbo].[Users] AS U
		   INNER JOIN
		   dbo.UserRoles AS UR
		   ON U.Id = UR.UserId
		   INNER JOIN
		   dbo.Roles AS R
		   ON UR.RoleId = R.Id
			
	  WHERE [U].[Email] = @Email

END
GO
/****** Object:  StoredProcedure [dbo].[Users_Select_AuthDataV2]    Script Date: 12/2/2022 3:13:23 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Justin Solo>
-- Create date: <07/08/2022>
-- Description:	<Users_Select_AuthDataV2>
-- Code Reviewer:


-- MODIFIED BY: author
-- MODIFIED DATE:12/1/2020
-- Code Reviewer: 
-- Note: 
-- =============================================

CREATE PROC [dbo].[Users_Select_AuthDataV2]
	@Email nvarchar(100)

AS

/* ----- TEST CODE -----

	DECLARE @Email nvarchar(100) = 'testuser7000@gmail.com'
	
	EXECUTE dbo.Users_Select_AuthDataV2 @Email

*/ ----- END TEST CODE -----


Declare @status INT
		,@confirm BIT
SELECT @status = U.UserStatusId
		,@confirm = U.IsConfirmed
FROM dbo.Users as U
WHERE @Email = Email

BEGIN
	IF @confirm = 1
		IF @status < 2
			SELECT [U].[Id]
				  ,[U].[Email]
				  ,[U].[Password]
				  ,R.[Name]
				  ,U.IsConfirmed
			  FROM [dbo].[Users] AS U
				   INNER JOIN
				   dbo.UserRoles AS UR
				   ON U.Id = UR.UserId
				   INNER JOIN
				   dbo.Roles AS R
				   ON UR.RoleId = R.Id

			  WHERE [U].[Email] = @Email
		ELSE
			  THROW 60001, 'User is not active.', 16
	ELSE
		THROW 60002, 'User is not confirmed.', 16
END

GO
/****** Object:  StoredProcedure [dbo].[Users_Select_ByEmail]    Script Date: 12/2/2022 3:13:23 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author: <Justin Solo>
-- Create date: <07/01/2022>
-- Description: <Users_Select_ByEmail>
-- Code Reviewer:

-- MODIFIED BY: author
-- MODIFIED DATE:12/1/2020
-- Code Reviewer:
-- Note:
-- =============================================

CREATE PROC [dbo].[Users_Select_ByEmail]
	@Email nvarchar(100)

AS

/* ----- TEST CODE -----

	DECLARE @Email nvarchar(100) = 'yeraga1270@teasya.com'
	
	EXECUTE dbo.Users_Select_ByEmail @Email

*/ ----- END TEST CODE -----

BEGIN

	SELECT [U].[Id]
		  ,[U].[Email]
		  ,[U].[Password]
		  ,[U].[IsConfirmed]
		  ,[US].[Name]
		  ,[U].[DateCreated]
		  ,[U].[DateModified]
	  FROM [dbo].[Users] AS U
		   INNER JOIN
		   [dbo].[UserStatus] AS US
		   ON [U].UserStatusId = [US].[Id]
			
	  WHERE [U].[Email] = @Email

END
GO
/****** Object:  StoredProcedure [dbo].[Users_Select_ById]    Script Date: 12/2/2022 3:13:23 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author: <Justin Solo>
-- Create date: <07/01/2022>
-- Description: <Users_Select_ById>
-- Code Reviewer:

-- MODIFIED BY: author
-- MODIFIED DATE:12/1/2020
-- Code Reviewer:
-- Note:
-- =============================================


CREATE PROC [dbo].[Users_Select_ById]
	@Id int

AS

/* ----- TEST CODE -----

	DECLARE @Id int = 1
	
	EXECUTE dbo.Users_Select_ById @Id

*/ ----- END TEST CODE -----

BEGIN

	SELECT [U].[Id]
		  ,[U].[Email]
		  ,[U].[Password]
		  ,[U].[IsConfirmed]
		  ,[US].[Name]
		  ,[U].[DateCreated]
		  ,[U].[DateModified]
	  FROM [dbo].[Users] AS U
		   INNER JOIN
		   [dbo].[UserStatus] AS US
		   ON [U].UserStatusId = [US].[Id]
			
	  WHERE [U].[Id] = @Id

END


GO
/****** Object:  StoredProcedure [dbo].[Users_Select_ByIdV2]    Script Date: 12/2/2022 3:13:23 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Justin Solo>
-- Create date: <07/15/2022>
-- Description:	<Users_Select_ByIdV2>
-- Code Reviewer:


-- MODIFIED BY: author
-- MODIFIED DATE:12/1/2020
-- Code Reviewer: 
-- Note: 
-- =============================================
CREATE PROC [dbo].[Users_Select_ByIdV2]
	@Id int

AS

/* ----- TEST CODE -----

	DECLARE @Id int = 113
	
	EXECUTE dbo.Users_Select_ByIdV2 @Id

*/ ----- END TEST CODE -----

BEGIN

	SELECT [U].[Id]
		  ,[U].[Email]
		  ,R.[Name] as Roles
	  FROM [dbo].[Users] AS U
		   INNER JOIN
		   dbo.UserRoles AS UR
		   ON U.Id = UR.UserId
		   INNER JOIN
		   dbo.Roles AS R
		   ON UR.RoleId = R.Id
			
	  WHERE [U].[Id] = @Id

END
GO
/****** Object:  StoredProcedure [dbo].[Users_SelectPass_ByEmail]    Script Date: 12/2/2022 3:13:23 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author: <Justin Solo>
-- Create date: <07/07/2022>
-- Description: <Users_SelectPass_ByEmail>
-- Code Reviewer:

-- MODIFIED BY: author
-- MODIFIED DATE:12/1/2020
-- Code Reviewer:
-- Note:
-- =============================================

CREATE PROC [dbo].[Users_SelectPass_ByEmail]
	@Email nvarchar(100)

AS

/* ----- TEST CODE -----

	DECLARE @Email nvarchar(100) = 'test@testuser.com'
	
	EXECUTE dbo.Users_SelectPass_ByEmail @Email

*/ ----- END TEST CODE -----

BEGIN

	SELECT [U].[Password]
	  FROM [dbo].[Users] AS U
		   INNER JOIN
		   [dbo].[UserStatus] AS US
		   ON [U].UserStatusId = [US].[Id]
			
	  WHERE [U].[Email] = @Email

END
GO
/****** Object:  StoredProcedure [dbo].[Users_Update]    Script Date: 12/2/2022 3:13:23 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author: <Justin Solo>
-- Create date: <07/01/2022>
-- Description: <User_Insert>
-- Code Reviewer:

-- MODIFIED BY: author
-- MODIFIED DATE:12/1/2020
-- Code Reviewer:
-- Note:
-- =============================================

CREATE PROC [dbo].[Users_Update]
	@Email nvarchar(100)
	,@Password nvarchar(100)
	,@IsConfirmed bit
	,@UserStatusId int
	,@Id int

AS

/* ----- TEST CODE -----

	DECLARE @Id int = 1
	
	DECLARE @Email nvarchar(100) = 'testuser@email.com'
			,@Password nvarchar(100) = 'testuser'
			,@IsConfirmed bit = 1
			,@UserStatusId int = 1

	EXECUTE dbo.Users_Update @Email
							,@Password
							,@IsConfirmed
							,@UserStatusId
							,@Id

*/ ----- END TEST CODE -----

BEGIN

	UPDATE [dbo].[Users]
	   SET [Email] = @Email
		  ,[Password] = @Password
		  ,[IsConfirmed] = @IsConfirmed
		  ,[UserStatusId] = @UserStatusId
		  ,[DateModified] = GETUTCDATE()
	 WHERE Id = @Id

END


GO
/****** Object:  StoredProcedure [dbo].[Users_UpdateIsConfirm]    Script Date: 12/2/2022 3:13:23 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author: <Justin Solo>
-- Create date: <07/07/2022>
-- Description: <Users_UpdateStatus>
-- Code Reviewer:

-- MODIFIED BY: author
-- MODIFIED DATE:12/1/2020
-- Code Reviewer:
-- Note:
-- =============================================

CREATE PROC [dbo].[Users_UpdateIsConfirm]
	@Token nvarchar(100)

AS

/* ----- TEST CODE -----

	DECLARE @Token nvarchar(100) = 'f9a2fd64-7d62-4ef7-86a4-6216403b8c8e'
	
	EXECUTE dbo.Users_UpdateIsConfirm	@Token

*/ ----- END TEST CODE -----

BEGIN

	DECLARE @userIdToUpdate int

	SELECT @userIdToUpdate = ut.UserId
	FROM dbo.UserTokens as ut
	WHERE @Token = ut.Token

	UPDATE dbo.Users
		SET [IsConfirmed] = 1
			,[DateModified] = GETUTCDATE()
	WHERE Id = @userIdToUpdate

END
GO
/****** Object:  StoredProcedure [dbo].[Users_UpdateStatus]    Script Date: 12/2/2022 3:13:23 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author: <Justin Solo>
-- Create date: <07/07/2022>
-- Description: <Users_UpdateStatus>
-- Code Reviewer:

-- MODIFIED BY: author
-- MODIFIED DATE:12/1/2020
-- Code Reviewer:
-- Note:
-- =============================================

CREATE PROC [dbo].[Users_UpdateStatus]
	@Id int
	,@UserStatusId int

AS

/* ----- TEST CODE -----

	DECLARE @Id int = 27
	
	DECLARE @UserStatusId int = 5

	EXECUTE dbo.Users_UpdateStatus	@Id
									,@UserStatusId

*/ ----- END TEST CODE -----

BEGIN

	UPDATE [dbo].[Users]
	   SET [UserStatusId] = @UserStatusId
		  ,[DateModified] = GETUTCDATE()
	 WHERE Id = @Id

END
GO
/****** Object:  StoredProcedure [dbo].[UserStatus_SelectAll]    Script Date: 12/2/2022 3:13:23 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author: <Justin Solo>
-- Create date: <07/01/2022>
-- Description: <Users_Select_ByEmail>
-- Code Reviewer:

-- MODIFIED BY: author
-- MODIFIED DATE:12/1/2020
-- Code Reviewer:
-- Note:
-- =============================================

CREATE PROC [dbo].[UserStatus_SelectAll]

AS

/* ----- TEST CODE -----
	
	EXECUTE dbo.UserStatus_SelectAll

*/ ----- END TEST CODE -----

BEGIN

	SELECT [Id]
		  ,[Name]
	  FROM [dbo].[UserStatus]

 END
GO
/****** Object:  StoredProcedure [dbo].[UserStatus_SelectByEmail]    Script Date: 12/2/2022 3:13:23 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO





CREATE PROC [dbo].[UserStatus_SelectByEmail]



AS


BEGIN

	SELECT *
	FROM dbo.UserStatus

END
GO
/****** Object:  StoredProcedure [dbo].[UserSubscriptionStatus_Insert]    Script Date: 12/2/2022 3:13:23 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author: <Tim Rawson>
-- Create date: <08/06/2022>
-- Description: <UserSubscriptionStatus_Insert>
-- Code Reviewer:<Jordan Poole>

-- MODIFIED BY: --
-- MODIFIED DATE:12/1/2020
-- Code Reviewer:
-- Note:
-- =============================================

CREATE   PROC [dbo].[UserSubscriptionStatus_Insert]
				@SubscriptionId int,
				@UserId int,
				@SubscriptionStartDate datetime2,
				@isSubscribed bit,
				@Id int OUTPUT

/*---------------Test Code--------------
		Declare @Id int = 1;

		Declare	@SubscriptionId int = 3
				,@UserId int = 17
				,@SubscriptionStartDate datetime2 = '2022-08-06 16:48:04.00'
				,@isSubscribed bit = 'True'

		EXECUTE [dbo].[UserSubscriptionStatus_Insert]
				@SubscriptionId
				,@UserId
				,@SubscriptionStartDate
				,@isSubscribed
				,@Id OUTPUT

		SELECT * From [dbo].[UserSubscriptionStatus]

*/--------------End Test Code-----------
AS

BEGIN

INSERT INTO [dbo].[UserSubscriptionStatus]
			([SubscriptionId],
			[UserId],
			[SubscriptionStartDate],
			[isSubscribed])
	VALUES
			(@SubscriptionId,
			@UserId,
			@SubscriptionStartDate,
			@isSubscribed)

	SET @Id = SCOPE_IDENTITY()

END
GO
/****** Object:  StoredProcedure [dbo].[UserSubscriptionStatus_Select_Active]    Script Date: 12/2/2022 3:13:23 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author: <Tim Rawson>
-- Create date: <08/06/2022>
-- Description: <UserSubscriptionStatus_Select_Active>
-- Code Reviewer:<Jordan Poole>

-- MODIFIED BY: --
-- MODIFIED DATE:12/1/2020
-- Code Reviewer:
-- Note:
-- =============================================

CREATE   PROC [dbo].[UserSubscriptionStatus_Select_Active]
					@PageIndex int,
					@PageSize int

/*---------------Test Code--------------
		DECLARE @PageIndex int = 0
				,@PageSize int = 50


		EXECUTE [dbo].[UserSubscriptionStatus_Select_Active]
				@PageIndex
				,@PageSize

*/--------------End Test Code-----------

AS

BEGIN

	DECLARE @Offset int = @PageIndex * @PageSize

	SELECT	us.[UserId]
			,cs.Name
			,us.[SubscriptionStartDate]
			,TOTALCOUNT = COUNT(1) OVER()
	
	FROM [dbo].[UserSubscriptionStatus] as us INNER JOIN dbo.CnmProSubscriptions as cs
	ON us.SubscriptionId = cs.Id
	WHERE [isSubscribed] = 1

	ORDER BY us.[SubscriptionStartDate]

	OFFSET @Offset ROWS
	FETCH NEXT @PageSize ROWS ONLY

END
GO
/****** Object:  StoredProcedure [dbo].[UserSubscriptionStatus_Select_CountByType]    Script Date: 12/2/2022 3:13:23 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author: <Tim Rawson>
-- Create date: <08/06/2022>
-- Description: <UserSubscriptionStatus_Select_CountByType>
-- Code Reviewer:<Jordan Poole>

-- MODIFIED BY: --
-- MODIFIED DATE:12/1/2020
-- Code Reviewer:
-- Note:
-- =============================================

CREATE   PROC [dbo].[UserSubscriptionStatus_Select_CountByType]

/*---------------Test Code--------------

EXECUTE [dbo].[UserSubscriptionStatus_Select_CountByType]

*/--------------End Test Code-----------

AS

BEGIN

SELECT COUNT(SubscriptionId) as [Basic]
		From UserSubscriptionStatus
		WHERE SubscriptionId = 1;

SELECT COUNT(SubscriptionId) as [Upgraded]
		From UserSubscriptionStatus
		WHERE SubscriptionId = 3;

SELECT COUNT(SubscriptionId) as [Premium]
		From UserSubscriptionStatus
		WHERE SubscriptionId = 5;

END
GO
/****** Object:  StoredProcedure [dbo].[UserSubscriptionStatus_SelectAll]    Script Date: 12/2/2022 3:13:23 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author: <Tim Rawson>
-- Create date: <08/06/2022>
-- Description: <UserSubscriptionStatus_SelectAll>
-- Code Reviewer:<Jordan Poole>

-- MODIFIED BY: --
-- MODIFIED DATE:12/1/2020
-- Code Reviewer:
-- Note:
-- =============================================

CREATE   PROC [dbo].[UserSubscriptionStatus_SelectAll]
			@PageIndex int,
			@PageSize int

/*---------------Test Code--------------
		DECLARE @PageIndex int = 0
				,@PageSize int = 50


		EXECUTE [dbo].[UserSubscriptionStatus_SelectAll]
				@PageIndex
				,@PageSize

*/--------------End Test Code-----------

AS

BEGIN

DECLARE @Offset int = @PageIndex * @PageSize

	SELECT	us.[UserId],
			cs.Name,
			us.[SubscriptionStartDate]
	
	FROM [dbo].[UserSubscriptionStatus] as us INNER JOIN dbo.CnmProSubscriptions as cs
	ON us.SubscriptionId = cs.Id

	ORDER BY us.[SubscriptionStartDate]

	OFFSET @Offset ROWS
	FETCH NEXT @PageSize ROWS ONLY

END
GO
/****** Object:  StoredProcedure [dbo].[UserSubscriptionStatus_SelectById]    Script Date: 12/2/2022 3:13:23 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

-- =============================================
-- Author: <Tim Rawson>
-- Create date: <08/06/2022>
-- Description: <UserSubscriptionStatus_SelectById>
-- Code Reviewer:<Jordan Poole>

-- MODIFIED BY: --
-- MODIFIED DATE:12/1/2020
-- Code Reviewer:
-- Note:
-- =============================================

CREATE   PROC [dbo].[UserSubscriptionStatus_SelectById]
					@UserId int

/*---------------Test Code--------------

DECLARE @UserId int = 17
EXECUTE [dbo].[UserSubscriptionStatus_SelectById]
		@UserId

*/--------------End Test Code-----------
AS

BEGIN

SELECT	us.[UserId],
		cs.Name,
		us.[SubscriptionStartDate],
		us.[SubscriptionEndDate],
		us.[isSubscribed]

FROM [dbo].[UserSubscriptionStatus] as us INNER JOIN dbo.CnmProSubscriptions as cs
		ON us.SubscriptionId = cs.Id

END
GO
/****** Object:  StoredProcedure [dbo].[UserSubscriptionStatus_Update_Cancellation]    Script Date: 12/2/2022 3:13:23 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author: <Tim Rawson>
-- Create date: <08/06/2022>
-- Description: <UserSubscriptionStatus_Update_Cancellation>
-- Code Reviewer:<Jordan Poole>

-- MODIFIED BY: --
-- MODIFIED DATE:12/1/2020
-- Code Reviewer:
-- Note:
-- =============================================

CREATE   PROC [dbo].[UserSubscriptionStatus_Update_Cancellation]
				@SubscriptionId int,
				@UserId int,
				@SubscriptionEndDate datetime2,
				@isSubscribed bit,
				@Id int

/*---------------Test Code--------------

		Declare	@SubscriptionId int = 3,
				@UserId int = 17,
				@SubscriptionEndDate datetime2 = '2022-08-06 16:50:04.00',
				@isSubscribed bit = 'False',
				@Id int = 1

		EXECUTE [dbo].[UserSubscriptionStatus_Update_Cancellation]
				@SubscriptionId,
				@UserId,
				@SubscriptionEndDate,
				@isSubscribed,
				@Id

		SELECT * From [dbo].[UserSubscriptionStatus]

*/--------------End Test Code-----------

AS 

BEGIN 

UPDATE [dbo].[UserSubscriptionStatus]
		SET	[SubscriptionId] = @SubscriptionId,
			[UserId] = @UserId,
			[SubscriptionEndDate] = @SubscriptionEndDate,
			[isSubscribed] = @isSubscribed
	
	WHERE Id = @Id


END
GO
/****** Object:  StoredProcedure [dbo].[UserSubscriptionStatus_Update_Upgrade]    Script Date: 12/2/2022 3:13:23 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author: <Tim Rawson>
-- Create date: <08/06/2022>
-- Description: <UserSubscriptionStatus_Update_Upgrade>
-- Code Reviewer:<Jordan Poole>

-- MODIFIED BY: --
-- MODIFIED DATE:12/1/2020
-- Code Reviewer:
-- Note:
-- =============================================

CREATE   PROC [dbo].[UserSubscriptionStatus_Update_Upgrade]
				@SubscriptionId int,
				@UserId int,
				@SubscriptionStartDate datetime2,
				@isSubscribed bit,
				@Id int

/*---------------Test Code--------------

		Declare	@SubscriptionId int = 5,
				@UserId int = 17,
				@SubscriptionStartDate datetime2 = '2022-08-06 16:01:04.00',
				@isSubscribed bit = 'True',
				@Id int = 1

		EXECUTE [dbo].[UserSubscriptionStatus_Update_Upgrade]
				@SubscriptionId,
				@UserId,
				@SubscriptionStartDate,
				@isSubscribed,
				@Id

		SELECT * From [dbo].[UserSubscriptionStatus]

*/--------------End Test Code-----------

AS

BEGIN

UPDATE [dbo].[UserSubscriptionStatus]
		SET	[SubscriptionId] = @SubscriptionId,
			[UserId] = @UserId,
			[SubscriptionStartDate] = @SubscriptionStartDate,
			[isSubscribed] = @isSubscribed
	
	WHERE Id = @Id

END
GO
/****** Object:  StoredProcedure [dbo].[UserTokens_Delete_ById]    Script Date: 12/2/2022 3:13:23 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author: <Justin Solo>
-- Create date: <07/05/2022>
-- Description: <UserTokens_Delete_ById>
-- Code Reviewer:

-- MODIFIED BY: author
-- MODIFIED DATE:12/1/2020
-- Code Reviewer:
-- Note:
-- =============================================

CREATE PROC [dbo].[UserTokens_Delete_ById]
	@Id int

AS

/* ----- TEST CODE -----

	DECLARE	@Id int = 1

	EXECUTE dbo.UserTokens_Delete_ById @Id

*/ ----- END TEST CODE -----

BEGIN

	DELETE FROM [dbo].[UserTokens]
		  WHERE @Id = UserId

END
GO
/****** Object:  StoredProcedure [dbo].[UserTokens_Delete_ByToken]    Script Date: 12/2/2022 3:13:23 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author: <Justin Solo>
-- Create date: <07/05/2022>
-- Description: <UserTokens_Delete_ByToken>
-- Code Reviewer:

-- MODIFIED BY: author
-- MODIFIED DATE:12/1/2020
-- Code Reviewer:
-- Note:
-- =============================================

CREATE PROC [dbo].[UserTokens_Delete_ByToken]
	@Token varchar(200)

AS

/* ----- TEST CODE -----

	DECLARE	@Token varchar(200) = 'testtoken'

	EXECUTE dbo.UserTokens_Delete_ByToken @Token

*/ ----- END TEST CODE -----

BEGIN

	DELETE FROM [dbo].[UserTokens]
		  WHERE @Token = Token

END


GO
/****** Object:  StoredProcedure [dbo].[UserTokens_Insert]    Script Date: 12/2/2022 3:13:23 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author: <Justin Solo>
-- Create date: <07/05/2022>
-- Description: <UserTokens_Insert>
-- Code Reviewer:

-- MODIFIED BY: author
-- MODIFIED DATE:12/1/2020
-- Code Reviewer:
-- Note:
-- =============================================

CREATE PROC [dbo].[UserTokens_Insert]
	@Token varchar(200)
	,@UserId int
    ,@TokenType int

AS

/* ----- TEST CODE -----

	DECLARE	@Token varchar(200) = 'testtoken'
			,@UserId int = 1
			,@TokenType int = 1

	EXECUTE dbo.UserTokens_Insert @Token
								  ,@UserId
								  ,@TokenType

*/ ----- END TEST CODE -----

BEGIN

INSERT INTO [dbo].[UserTokens]
           ([Token]
           ,[UserId]
           ,[TokenType])
     VALUES
           (@Token
           ,@UserId
           ,@TokenType)

END


GO
/****** Object:  StoredProcedure [dbo].[UserTokens_Select_ById]    Script Date: 12/2/2022 3:13:23 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author: <Justin Solo>
-- Create date: <07/05/2022>
-- Description: <UserTokens_Select_ById (UserId)>
-- Code Reviewer:

-- MODIFIED BY: author
-- MODIFIED DATE:12/1/2020
-- Code Reviewer:
-- Note:
-- =============================================

CREATE PROC [dbo].[UserTokens_Select_ById]
	@Id int

AS

/* ----- TEST CODE -----

	DECLARE @Id int = 1
	
	EXECUTE dbo.UserTokens_Select_ById @Id

*/ ----- END TEST CODE -----

BEGIN

	SELECT [Token]
		  ,[UserId]
		  ,[TokenType]
	  FROM [dbo].[UserTokens] as UT
	  WHERE @Id = [UT].UserId

END
GO
/****** Object:  StoredProcedure [dbo].[UserTokens_Select_ByTokenType]    Script Date: 12/2/2022 3:13:23 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author: <Justin Solo>
-- Create date: <07/05/2022>
-- Description: <UserTokens_Select_ByTokenType>
-- Code Reviewer:

-- MODIFIED BY: author
-- MODIFIED DATE:12/1/2020
-- Code Reviewer:
-- Note:
-- =============================================

CREATE PROC [dbo].[UserTokens_Select_ByTokenType]
	@TokenType int

AS

/* ----- TEST CODE -----

	DECLARE @TokenType int = 1
	
	EXECUTE dbo.UserTokens_Select_ByTokenType @TokenType

*/ ----- END TEST CODE -----

BEGIN

	SELECT [Token]
		  ,[UserId]
		  ,[TokenType]
	  FROM [dbo].[UserTokens] as UT
	  WHERE @TokenType = [UT].[TokenType]

END
GO
/****** Object:  StoredProcedure [dbo].[UserTokens_SelectAll]    Script Date: 12/2/2022 3:13:23 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author: <Justin Solo>
-- Create date: <07/05/2022>
-- Description: <UserTokens_SelectAll>
-- Code Reviewer:

-- MODIFIED BY: author
-- MODIFIED DATE:12/1/2020
-- Code Reviewer:
-- Note:
-- =============================================

CREATE PROC [dbo].[UserTokens_SelectAll]
	@PageIndex int 
	,@PageSize int

AS

/* ----- TEST CODE -----

	DECLARE	@PageIndex int = 0
			,@PageSize int = 1
	EXECUTE dbo.UserTokens_SelectAll @PageIndex
									,@PageSize

*/ ----- END TEST CODE -----

BEGIN

	Declare @offSet int = @PageIndex * @PageSize

	SELECT [Token]
		  ,[UserId]
		  ,[TokenType]
	  FROM [dbo].[UserTokens]

	  ORDER BY TokenType

	  OFFSET @offSet Rows
	  Fetch Next @PageSize Rows ONLY

END
GO
USE [master]
GO
ALTER DATABASE [CnmPro] SET  READ_WRITE 
GO
