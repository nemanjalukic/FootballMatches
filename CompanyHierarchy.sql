USE [master]
GO
/****** Object:  Database [CompanyHierarchy]    Script Date: 8/17/2020 10:46:38 ******/
CREATE DATABASE [CompanyHierarchy]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'CompanyHierarchy', FILENAME = N'C:\Users\Nemanja\AppData\Local\Microsoft\Microsoft SQL Server Local DB\Instances\MSSqllocaldb\CompanyHierarchy.mdf' , SIZE = 8192KB , MAXSIZE = UNLIMITED, FILEGROWTH = 65536KB )
 LOG ON 
( NAME = N'CompanyHierarchy_log', FILENAME = N'C:\Users\Nemanja\AppData\Local\Microsoft\Microsoft SQL Server Local DB\Instances\MSSqllocaldb\CompanyHierarchy.ldf' , SIZE = 8192KB , MAXSIZE = 2048GB , FILEGROWTH = 65536KB )
GO
ALTER DATABASE [CompanyHierarchy] SET COMPATIBILITY_LEVEL = 130
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [CompanyHierarchy].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [CompanyHierarchy] SET ANSI_NULL_DEFAULT ON 
GO
ALTER DATABASE [CompanyHierarchy] SET ANSI_NULLS ON 
GO
ALTER DATABASE [CompanyHierarchy] SET ANSI_PADDING ON 
GO
ALTER DATABASE [CompanyHierarchy] SET ANSI_WARNINGS ON 
GO
ALTER DATABASE [CompanyHierarchy] SET ARITHABORT ON 
GO
ALTER DATABASE [CompanyHierarchy] SET AUTO_CLOSE OFF 
GO
ALTER DATABASE [CompanyHierarchy] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [CompanyHierarchy] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [CompanyHierarchy] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [CompanyHierarchy] SET CURSOR_DEFAULT  LOCAL 
GO
ALTER DATABASE [CompanyHierarchy] SET CONCAT_NULL_YIELDS_NULL ON 
GO
ALTER DATABASE [CompanyHierarchy] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [CompanyHierarchy] SET QUOTED_IDENTIFIER ON 
GO
ALTER DATABASE [CompanyHierarchy] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [CompanyHierarchy] SET  DISABLE_BROKER 
GO
ALTER DATABASE [CompanyHierarchy] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [CompanyHierarchy] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [CompanyHierarchy] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [CompanyHierarchy] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [CompanyHierarchy] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [CompanyHierarchy] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [CompanyHierarchy] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [CompanyHierarchy] SET RECOVERY FULL 
GO
ALTER DATABASE [CompanyHierarchy] SET  MULTI_USER 
GO
ALTER DATABASE [CompanyHierarchy] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [CompanyHierarchy] SET DB_CHAINING OFF 
GO
ALTER DATABASE [CompanyHierarchy] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [CompanyHierarchy] SET TARGET_RECOVERY_TIME = 60 SECONDS 
GO
ALTER DATABASE [CompanyHierarchy] SET DELAYED_DURABILITY = DISABLED 
GO
ALTER DATABASE [CompanyHierarchy] SET QUERY_STORE = OFF
GO
USE [CompanyHierarchy]
GO
ALTER DATABASE SCOPED CONFIGURATION SET LEGACY_CARDINALITY_ESTIMATION = OFF;
GO
ALTER DATABASE SCOPED CONFIGURATION SET MAXDOP = 0;
GO
ALTER DATABASE SCOPED CONFIGURATION SET PARAMETER_SNIFFING = ON;
GO
ALTER DATABASE SCOPED CONFIGURATION SET QUERY_OPTIMIZER_HOTFIXES = OFF;
GO
USE [CompanyHierarchy]
GO
/****** Object:  Table [dbo].[Employee]    Script Date: 8/17/2020 10:46:38 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Employee](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](50) NULL,
	[Position] [nvarchar](50) NULL,
	[SuperiorId] [int] NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET IDENTITY_INSERT [dbo].[Employee] ON 

INSERT [dbo].[Employee] ([Id], [Name], [Position], [SuperiorId]) VALUES (1, N'Jesicsa', N'CEO', NULL)
INSERT [dbo].[Employee] ([Id], [Name], [Position], [SuperiorId]) VALUES (2, N'Mario', N'Production Menager', 1)
INSERT [dbo].[Employee] ([Id], [Name], [Position], [SuperiorId]) VALUES (3, N'Romero', N'HR Menager', 1)
INSERT [dbo].[Employee] ([Id], [Name], [Position], [SuperiorId]) VALUES (4, N'Mary', N'Marketing Menager', 1)
INSERT [dbo].[Employee] ([Id], [Name], [Position], [SuperiorId]) VALUES (5, N'Lucy', N'Sales Officer', 4)
INSERT [dbo].[Employee] ([Id], [Name], [Position], [SuperiorId]) VALUES (6, N'Jacob', N'Fabrication Menager', 2)
INSERT [dbo].[Employee] ([Id], [Name], [Position], [SuperiorId]) VALUES (7, N'Andreas', N'Development Menager', 2)
INSERT [dbo].[Employee] ([Id], [Name], [Position], [SuperiorId]) VALUES (8, N'Susan', N'Worker', 7)
INSERT [dbo].[Employee] ([Id], [Name], [Position], [SuperiorId]) VALUES (9, N'John', N'Worker', 7)
SET IDENTITY_INSERT [dbo].[Employee] OFF
GO
ALTER TABLE [dbo].[Employee]  WITH CHECK ADD  CONSTRAINT [FK_Employee_ToTable] FOREIGN KEY([SuperiorId])
REFERENCES [dbo].[Employee] ([Id])
GO
ALTER TABLE [dbo].[Employee] CHECK CONSTRAINT [FK_Employee_ToTable]
GO
/****** Object:  StoredProcedure [dbo].[Procedure]    Script Date: 8/17/2020 10:46:38 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[Procedure]
	@param1 int
AS BEGIN
;WITH rCTE AS 
(
    SELECT  Id ,
            SuperiorId ,
            Name,
            Position
    FROM Employee WHERE ID=@param1
    UNION ALL
    SELECT  n.Id ,
            n.SuperiorId,
            n.Name,
            n.Position
    FROM rCTE r
    INNER JOIN Employee n ON n.SuperiorId = r.Id
)
SELECT *  
FROM rCTE r;
END
GO
USE [master]
GO
ALTER DATABASE [CompanyHierarchy] SET  READ_WRITE 
GO
