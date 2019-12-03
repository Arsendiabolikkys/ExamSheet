USE [ExamSheet_database]
GO

/****** Object:  Table [dbo].[ExamSheets]    Script Date: 11/30/2019 5:05:40 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[ExamSheets](
	[Id] [nvarchar](50) NOT NULL,
	[TeacherId] [nvarchar](50) NOT NULL,
	[SubjectId] [nvarchar](50) NOT NULL,
	[GroupId] [nvarchar](50) NOT NULL,
	[State] [tinyint] NOT NULL,
	[OpenDate] [datetime] NULL,
	[CloseDate] [datetime] NULL,
	[FacultyId] [nvarchar](50) NOT NULL,
	[Semester] [tinyint] NULL,
	[Year] [smallint] NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

ALTER TABLE [dbo].[ExamSheets]  WITH CHECK ADD FOREIGN KEY([FacultyId])
REFERENCES [dbo].[Faculties] ([Id])
GO

ALTER TABLE [dbo].[ExamSheets]  WITH CHECK ADD FOREIGN KEY([TeacherId])
REFERENCES [dbo].[Teachers] ([Id])
GO

ALTER TABLE [dbo].[ExamSheets]  WITH CHECK ADD FOREIGN KEY([SubjectId])
REFERENCES [dbo].[Subjects] ([Id])
GO

ALTER TABLE [dbo].[ExamSheets]  WITH CHECK ADD FOREIGN KEY([GroupId])
REFERENCES [dbo].[Groups] ([Id])
GO
