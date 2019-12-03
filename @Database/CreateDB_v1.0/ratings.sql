USE [ExamSheet_database]
GO

/****** Object:  Table [dbo].[Ratings]    Script Date: 11/30/2019 5:05:44 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[Ratings](
	[StudentId] [nvarchar](50) NOT NULL,
	[ExamSheetId] [nvarchar](50) NOT NULL,
	[Mark] [tinyint] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[StudentId] ASC,
	[ExamSheetId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

ALTER TABLE [dbo].[Ratings]  WITH CHECK ADD FOREIGN KEY([StudentId])
REFERENCES [dbo].[Students] ([Id])
GO
ALTER TABLE [dbo].[Ratings]  WITH CHECK ADD FOREIGN KEY([ExamSheetId])
REFERENCES [dbo].[ExamSheets] ([Id])
GO
