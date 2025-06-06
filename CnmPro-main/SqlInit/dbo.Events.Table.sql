USE [CnmPro]
GO
/****** Object:  Table [dbo].[Events]    Script Date: 9/19/2022 10:13:05 AM ******/
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
ALTER TABLE [dbo].[Events] ADD  CONSTRAINT [DF_Events_StartDate]  DEFAULT (getutcdate()) FOR [StartDate]
GO
ALTER TABLE [dbo].[Events] ADD  CONSTRAINT [DF_Events_EndDate]  DEFAULT (getutcdate()) FOR [EndDate]
GO
ALTER TABLE [dbo].[Events] ADD  CONSTRAINT [DF_Events_DateCreated]  DEFAULT (getutcdate()) FOR [DateCreated]
GO
ALTER TABLE [dbo].[Events] ADD  CONSTRAINT [DF_Events_DateModified]  DEFAULT (getutcdate()) FOR [DateModified]
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
