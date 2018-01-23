USE [rde_463612]
GO

/****** Object:  Table [dbo].[location]    Script Date: 24/05/2016 13:02:13 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

SET ANSI_PADDING ON
GO


-- Creates the location Table
CREATE TABLE [dbo].[location](
	[LocationID] [int] IDENTITY(10000,1) NOT NULL,
	[buildingname] [varchar](40) NOT NULL,
	[room] [nchar](10) NULL,
 CONSTRAINT [PK_location] PRIMARY KEY CLUSTERED 
(
	[LocationID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

SET ANSI_PADDING OFF
GO

USE [rde_463612]
GO

INSERT INTO [dbo].[location]
           ([buildingname]
           ,[room])
     VALUES
           ('Fenner','52-B'), ('Library','101'), ('Library','200'), ('Sportshall',''), ('Grant',''), ('Foss',''), ('Larkin','G3'), ('Applied Science','3C'), ('Student Union','')

GO





USE [rde_463612]
GO

/****** Object:  Table [dbo].[staff]    Script Date: 24/05/2016 13:02:24 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

SET ANSI_PADDING ON
GO

-- Creates the staff Table
CREATE TABLE [dbo].[staff](
	[UsernameID] [varchar](30) NOT NULL,
	[firstname] [varchar](20) NOT NULL,
	[surname] [varchar](20) NOT NULL,
	[email] [varchar](30) NULL,
	[contactnumber] [int] NULL,
 CONSTRAINT [PK_staff] PRIMARY KEY CLUSTERED 
(
	[UsernameID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

SET ANSI_PADDING OFF
GO

USE [rde_463612]
GO

/****** Object:  Table [dbo].[staffLocation]    Script Date: 24/05/2016 13:02:45 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

SET ANSI_PADDING ON
GO


USE [rde_463612]
GO

INSERT INTO [dbo].[staff]
           ([UsernameID]
           ,[firstname]
           ,[surname]
           ,[email]
           ,[contactnumber])
     VALUES
           ('oneusername','onefirstname','onelastname','',''),('twousername','twofirstname','twolastname','',''),('threeusername','threefirstname','threelastname','',''),
		   ('fourusername','fourfirstname','fourlastname','',''),('fiveusername','fivefirstname','fivelastname','',''),('sixusername','sixfirstname','sixlastname','',''),
		   ('sevenusername','sevenusername','sevenusername','',''),('eightusername','eightusername','eightusername','',''),('nineusername','nineusername','nineusername','',''),
		   ('tenusername','tenusername','tenusername','','')
GO






-- Creates the staffLocation Table
CREATE TABLE [dbo].[staffLocation](
	[StaffLocationID] [int] IDENTITY(10000,1) NOT NULL,
	[usernameID] [varchar](30) NOT NULL,
	[locationID] [int] NOT NULL,
	[time] [datetime] NOT NULL,
 CONSTRAINT [PK_staffLocation] PRIMARY KEY CLUSTERED 
(
	[StaffLocationID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

SET ANSI_PADDING OFF
GO

ALTER TABLE [dbo].[staffLocation]  WITH CHECK ADD  CONSTRAINT [FK_staffLocation_location] FOREIGN KEY([locationID])
REFERENCES [dbo].[location] ([LocationID])
GO

ALTER TABLE [dbo].[staffLocation] CHECK CONSTRAINT [FK_staffLocation_location]
GO

ALTER TABLE [dbo].[staffLocation]  WITH CHECK ADD  CONSTRAINT [FK_staffLocation_staff] FOREIGN KEY([usernameID])
REFERENCES [dbo].[staff] ([UsernameID])
GO

ALTER TABLE [dbo].[staffLocation] CHECK CONSTRAINT [FK_staffLocation_staff]
GO


USE [rde_463612]
GO

