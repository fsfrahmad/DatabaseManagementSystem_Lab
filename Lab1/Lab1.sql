USE [TestDB_2022_CS_1]
GO
/****** Object:  Table [dbo].[student]    Script Date: 1/23/2024 2:00:38 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[student](
	[Id] [nchar](10) NOT NULL,
	[Name] [nchar](10) NOT NULL,
	[Section] [nchar](10) NOT NULL,
	[Contact] [nchar](10) NULL,
	[Address] [nchar](10) NULL,
	[CGPA] [nchar](10) NULL
) ON [PRIMARY]
GO
INSERT [dbo].[student] ([Id], [Name], [Section], [Contact], [Address], [CGPA]) VALUES (N'1         ', N'Saad      ', N'A         ', N'123       ', N'abc       ', N'4.0       ')
GO
