USE [ExamSheet_database]
GO

/****** Object:  Table [dbo].[Accounts]    Script Date: 11/30/2019 5:04:50 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[Accounts](
	[Id] [nvarchar](50) NOT NULL,
	[Email] [nvarchar](255) NOT NULL,
	[PasswordHash] [nvarchar](1024) NOT NULL,
	[Salt] [nvarchar](1024) NOT NULL,
	[ReferenceId] [nvarchar](50) NULL,
	[AccountType] [tinyint] NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO


