USE [u0135287_ArtDuviks]
GO
/****** Object:  Table [dbo].[DisplayPlaces]    Script Date: 19.12.2015 10:26:51 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[DisplayPlaces](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[PlaceOfDisplaying] [nvarchar](50) NULL,
 CONSTRAINT [PK_DisplayPlaces] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Owners]    Script Date: 19.12.2015 10:26:51 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Owners](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](50) NULL,
	[Phone] [nvarchar](50) NULL,
 CONSTRAINT [PK_Owners] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Pages]    Script Date: 19.12.2015 10:26:51 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Pages](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[Content] [varchar](max) NULL,
	[Name] [nvarchar](50) NULL,
 CONSTRAINT [PK_Pages] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[PetBreeds]    Script Date: 19.12.2015 10:26:51 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[PetBreeds](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](50) NOT NULL,
	[RussianName] [nvarchar](50) NULL,
	[Description] [varchar](max) NULL,
 CONSTRAINT [PK_PetBreeds] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[Pets]    Script Date: 19.12.2015 10:26:51 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Pets](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](50) NOT NULL,
	[RussianName] [nvarchar](50) NULL,
	[BreedID] [int] NOT NULL,
	[BirthDate] [date] NULL,
	[UnderThePictureText] [varchar](max) NULL,
	[OwnerID] [int] NOT NULL,
	[MotherID] [int] NULL,
	[FatherID] [int] NULL,
	[WhereDisplay] [int] NULL,
	[PictureID] [int] NULL,
 CONSTRAINT [PK_Pets] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[PetsPictures]    Script Date: 19.12.2015 10:26:51 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[PetsPictures](
	[PetID] [int] NOT NULL,
	[PictureID] [int] NOT NULL,
 CONSTRAINT [PK_PetsPictures] PRIMARY KEY CLUSTERED 
(
	[PetID] ASC,
	[PictureID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Pictures]    Script Date: 19.12.2015 10:26:51 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Pictures](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[Image] [varchar](max) NULL,
 CONSTRAINT [PK_Pictures] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
SET IDENTITY_INSERT [dbo].[DisplayPlaces] ON 

INSERT [dbo].[DisplayPlaces] ([ID], [PlaceOfDisplaying]) VALUES (1, N'Отображать на сайте')
INSERT [dbo].[DisplayPlaces] ([ID], [PlaceOfDisplaying]) VALUES (2, N'Отображать на главной')
INSERT [dbo].[DisplayPlaces] ([ID], [PlaceOfDisplaying]) VALUES (3, N'Не отображать')
SET IDENTITY_INSERT [dbo].[DisplayPlaces] OFF
SET IDENTITY_INSERT [dbo].[Owners] ON 

INSERT [dbo].[Owners] ([ID], [Name], [Phone]) VALUES (1, N'Анна', N'8-952-121-47-50')
INSERT [dbo].[Owners] ([ID], [Name], [Phone]) VALUES (2, N'Юлия', N'8-903-640-5796')
SET IDENTITY_INSERT [dbo].[Owners] OFF
SET IDENTITY_INSERT [dbo].[PetBreeds] ON 

INSERT [dbo].[PetBreeds] ([ID], [Name], [RussianName], [Description]) VALUES (1, N'MainCun', N'Мейн-кун', N'Порода самых больших домашних кошек')
INSERT [dbo].[PetBreeds] ([ID], [Name], [RussianName], [Description]) VALUES (2, N'Briatain', N'Британская порода', NULL)
INSERT [dbo].[PetBreeds] ([ID], [Name], [RussianName], [Description]) VALUES (3, N'Scotland', N'Шотландская порода', NULL)
INSERT [dbo].[PetBreeds] ([ID], [Name], [RussianName], [Description]) VALUES (4, N'Bengals', N'Бенгальская порода', NULL)
SET IDENTITY_INSERT [dbo].[PetBreeds] OFF
ALTER TABLE [dbo].[Pets]  WITH CHECK ADD  CONSTRAINT [FK_Pets_DisplayPlaces] FOREIGN KEY([WhereDisplay])
REFERENCES [dbo].[DisplayPlaces] ([ID])
GO
ALTER TABLE [dbo].[Pets] CHECK CONSTRAINT [FK_Pets_DisplayPlaces]
GO
ALTER TABLE [dbo].[Pets]  WITH CHECK ADD  CONSTRAINT [FK_Pets_Owners] FOREIGN KEY([OwnerID])
REFERENCES [dbo].[Owners] ([ID])
GO
ALTER TABLE [dbo].[Pets] CHECK CONSTRAINT [FK_Pets_Owners]
GO
ALTER TABLE [dbo].[Pets]  WITH CHECK ADD  CONSTRAINT [FK_Pets_PetBreeds] FOREIGN KEY([BreedID])
REFERENCES [dbo].[PetBreeds] ([ID])
GO
ALTER TABLE [dbo].[Pets] CHECK CONSTRAINT [FK_Pets_PetBreeds]
GO
ALTER TABLE [dbo].[PetsPictures]  WITH CHECK ADD  CONSTRAINT [FK_PetsPictures_Pets] FOREIGN KEY([PetID])
REFERENCES [dbo].[Pets] ([ID])
GO
ALTER TABLE [dbo].[PetsPictures] CHECK CONSTRAINT [FK_PetsPictures_Pets]
GO
ALTER TABLE [dbo].[PetsPictures]  WITH CHECK ADD  CONSTRAINT [FK_PetsPictures_Pictures] FOREIGN KEY([PictureID])
REFERENCES [dbo].[Pictures] ([ID])
GO
ALTER TABLE [dbo].[PetsPictures] CHECK CONSTRAINT [FK_PetsPictures_Pictures]
GO
