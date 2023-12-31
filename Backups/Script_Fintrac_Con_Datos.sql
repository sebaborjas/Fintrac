USE [master]
GO
/****** Object:  Database [Fintrac]    Script Date: 16/11/2023 20:33:50 ******/
CREATE DATABASE [Fintrac]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'Fintrac', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL15.SQLEXPRESS\MSSQL\DATA\Fintrac.mdf' , SIZE = 8192KB , MAXSIZE = UNLIMITED, FILEGROWTH = 10%)
 LOG ON 
( NAME = N'Fintrac_log', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL15.SQLEXPRESS\MSSQL\DATA\Fintrac_log.ldf' , SIZE = 1024KB , MAXSIZE = 2048GB , FILEGROWTH = 10%)
 WITH CATALOG_COLLATION = DATABASE_DEFAULT
GO
ALTER DATABASE [Fintrac] SET COMPATIBILITY_LEVEL = 150
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [Fintrac].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [Fintrac] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [Fintrac] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [Fintrac] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [Fintrac] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [Fintrac] SET ARITHABORT OFF 
GO
ALTER DATABASE [Fintrac] SET AUTO_CLOSE OFF 
GO
ALTER DATABASE [Fintrac] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [Fintrac] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [Fintrac] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [Fintrac] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [Fintrac] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [Fintrac] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [Fintrac] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [Fintrac] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [Fintrac] SET  DISABLE_BROKER 
GO
ALTER DATABASE [Fintrac] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [Fintrac] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [Fintrac] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [Fintrac] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [Fintrac] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [Fintrac] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [Fintrac] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [Fintrac] SET RECOVERY SIMPLE 
GO
ALTER DATABASE [Fintrac] SET  MULTI_USER 
GO
ALTER DATABASE [Fintrac] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [Fintrac] SET DB_CHAINING OFF 
GO
ALTER DATABASE [Fintrac] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [Fintrac] SET TARGET_RECOVERY_TIME = 0 SECONDS 
GO
ALTER DATABASE [Fintrac] SET DELAYED_DURABILITY = DISABLED 
GO
ALTER DATABASE [Fintrac] SET ACCELERATED_DATABASE_RECOVERY = OFF  
GO
ALTER DATABASE [Fintrac] SET QUERY_STORE = OFF
GO
USE [Fintrac]
GO
/****** Object:  Table [dbo].[__EFMigrationsHistory]    Script Date: 16/11/2023 20:33:50 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[__EFMigrationsHistory](
	[MigrationId] [nvarchar](150) NOT NULL,
	[ProductVersion] [nvarchar](32) NOT NULL,
 CONSTRAINT [PK___EFMigrationsHistory] PRIMARY KEY CLUSTERED 
(
	[MigrationId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Accounts]    Script Date: 16/11/2023 20:33:50 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Accounts](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](max) NOT NULL,
	[CreationDate] [datetime2](7) NOT NULL,
	[Currency] [int] NOT NULL,
	[WorkSpaceID] [int] NOT NULL,
 CONSTRAINT [PK_Accounts] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Category]    Script Date: 16/11/2023 20:33:50 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Category](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](max) NOT NULL,
	[CreationDate] [datetime2](7) NOT NULL,
	[Type] [int] NOT NULL,
	[Status] [int] NOT NULL,
	[WorkspaceID] [int] NOT NULL,
 CONSTRAINT [PK_Category] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[CategoryGoal]    Script Date: 16/11/2023 20:33:50 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[CategoryGoal](
	[CategoryId] [int] NOT NULL,
	[GoalId] [int] NOT NULL,
 CONSTRAINT [PK_CategoryGoal] PRIMARY KEY CLUSTERED 
(
	[CategoryId] ASC,
	[GoalId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[CreditCards]    Script Date: 16/11/2023 20:33:50 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[CreditCards](
	[Id] [int] NOT NULL,
	[BankName] [nvarchar](max) NOT NULL,
	[LastDigits] [nvarchar](max) NOT NULL,
	[AvailableCredit] [float] NOT NULL,
	[DeadLine] [int] NOT NULL,
 CONSTRAINT [PK_CreditCards] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Exchange]    Script Date: 16/11/2023 20:33:50 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Exchange](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Date] [datetime2](7) NOT NULL,
	[CurrencyValue] [float] NOT NULL,
	[Currency] [int] NOT NULL,
	[WorkspaceID] [int] NOT NULL,
 CONSTRAINT [PK_Exchange] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Goal]    Script Date: 16/11/2023 20:33:50 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Goal](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Token] [nvarchar](max) NULL,
	[Title] [nvarchar](max) NOT NULL,
	[MaxAmount] [float] NOT NULL,
	[WorkspaceID] [int] NOT NULL,
 CONSTRAINT [PK_Goal] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Invitation]    Script Date: 16/11/2023 20:33:50 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Invitation](
	[ID] [int] NOT NULL,
	[AdminId] [nvarchar](450) NOT NULL,
	[UserToInviteId] [nvarchar](450) NOT NULL,
 CONSTRAINT [PK_Invitation] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[PersonalAccounts]    Script Date: 16/11/2023 20:33:50 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[PersonalAccounts](
	[Id] [int] NOT NULL,
	[StartingAmount] [float] NOT NULL,
 CONSTRAINT [PK_PersonalAccounts] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Transactions]    Script Date: 16/11/2023 20:33:50 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Transactions](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[AccountId] [int] NOT NULL,
	[CategoryId] [int] NOT NULL,
	[Title] [nvarchar](max) NOT NULL,
	[CreationDate] [datetime2](7) NOT NULL,
	[Amount] [float] NOT NULL,
	[Currency] [int] NOT NULL,
 CONSTRAINT [PK_Transactions] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Users]    Script Date: 16/11/2023 20:33:50 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Users](
	[Email] [nvarchar](450) NOT NULL,
	[Name] [nvarchar](max) NOT NULL,
	[LastName] [nvarchar](max) NOT NULL,
	[Address] [nvarchar](max) NULL,
	[Password] [nvarchar](max) NOT NULL,
 CONSTRAINT [PK_Users] PRIMARY KEY CLUSTERED 
(
	[Email] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[UserWorkspace]    Script Date: 16/11/2023 20:33:50 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[UserWorkspace](
	[UserId] [nvarchar](450) NOT NULL,
	[WorkspaceId] [int] NOT NULL,
 CONSTRAINT [PK_UserWorkspace] PRIMARY KEY CLUSTERED 
(
	[UserId] ASC,
	[WorkspaceId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Workspace]    Script Date: 16/11/2023 20:33:50 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Workspace](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](max) NOT NULL,
	[UserAdminId] [nvarchar](450) NOT NULL,
 CONSTRAINT [PK_Workspace] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
INSERT [dbo].[__EFMigrationsHistory] ([MigrationId], [ProductVersion]) VALUES (N'20231116201933_MigrationV1', N'7.0.13')
GO
SET IDENTITY_INSERT [dbo].[Accounts] ON 

INSERT [dbo].[Accounts] ([Id], [Name], [CreationDate], [Currency], [WorkSpaceID]) VALUES (1, N'CreditoDolares', CAST(N'2023-11-16T00:00:00.0000000' AS DateTime2), 1, 1)
INSERT [dbo].[Accounts] ([Id], [Name], [CreationDate], [Currency], [WorkSpaceID]) VALUES (2, N'SantanderPesos', CAST(N'2023-11-16T00:00:00.0000000' AS DateTime2), 0, 1)
INSERT [dbo].[Accounts] ([Id], [Name], [CreationDate], [Currency], [WorkSpaceID]) VALUES (3, N'Santander Dolares', CAST(N'2023-11-16T00:00:00.0000000' AS DateTime2), 1, 1)
INSERT [dbo].[Accounts] ([Id], [Name], [CreationDate], [Currency], [WorkSpaceID]) VALUES (4, N'Oca blue', CAST(N'2023-11-16T00:00:00.0000000' AS DateTime2), 0, 3)
INSERT [dbo].[Accounts] ([Id], [Name], [CreationDate], [Currency], [WorkSpaceID]) VALUES (5, N'Tarjeta Mides', CAST(N'2023-10-06T00:00:00.0000000' AS DateTime2), 2, 3)
INSERT [dbo].[Accounts] ([Id], [Name], [CreationDate], [Currency], [WorkSpaceID]) VALUES (6, N'Credito Fucac', CAST(N'2023-11-16T00:00:00.0000000' AS DateTime2), 1, 3)
INSERT [dbo].[Accounts] ([Id], [Name], [CreationDate], [Currency], [WorkSpaceID]) VALUES (7, N'Itau Debito', CAST(N'2023-11-16T00:00:00.0000000' AS DateTime2), 0, 2)
INSERT [dbo].[Accounts] ([Id], [Name], [CreationDate], [Currency], [WorkSpaceID]) VALUES (8, N'Credito BBVA', CAST(N'2023-11-16T00:00:00.0000000' AS DateTime2), 1, 2)
INSERT [dbo].[Accounts] ([Id], [Name], [CreationDate], [Currency], [WorkSpaceID]) VALUES (9, N'Debito BBVA', CAST(N'2023-11-16T00:00:00.0000000' AS DateTime2), 0, 2)
INSERT [dbo].[Accounts] ([Id], [Name], [CreationDate], [Currency], [WorkSpaceID]) VALUES (10, N'Ticket Alimentacion', CAST(N'2023-11-02T00:00:00.0000000' AS DateTime2), 0, 1)
SET IDENTITY_INSERT [dbo].[Accounts] OFF
GO
SET IDENTITY_INSERT [dbo].[Category] ON 

INSERT [dbo].[Category] ([Id], [Name], [CreationDate], [Type], [Status], [WorkspaceID]) VALUES (1, N'Categoria1', CAST(N'2023-11-01T00:00:00.0000000' AS DateTime2), 1, 0, 1)
INSERT [dbo].[Category] ([Id], [Name], [CreationDate], [Type], [Status], [WorkspaceID]) VALUES (2, N'Categoria2', CAST(N'2023-11-01T00:00:00.0000000' AS DateTime2), 0, 0, 1)
INSERT [dbo].[Category] ([Id], [Name], [CreationDate], [Type], [Status], [WorkspaceID]) VALUES (3, N'Categoria3', CAST(N'2023-11-16T00:00:00.0000000' AS DateTime2), 1, 0, 1)
INSERT [dbo].[Category] ([Id], [Name], [CreationDate], [Type], [Status], [WorkspaceID]) VALUES (4, N'Ahorro', CAST(N'2023-11-16T00:00:00.0000000' AS DateTime2), 1, 0, 1)
INSERT [dbo].[Category] ([Id], [Name], [CreationDate], [Type], [Status], [WorkspaceID]) VALUES (5, N'Ingresos Particulares', CAST(N'2023-11-01T00:00:00.0000000' AS DateTime2), 0, 0, 3)
INSERT [dbo].[Category] ([Id], [Name], [CreationDate], [Type], [Status], [WorkspaceID]) VALUES (6, N'Hogar', CAST(N'2023-11-01T00:00:00.0000000' AS DateTime2), 1, 0, 3)
INSERT [dbo].[Category] ([Id], [Name], [CreationDate], [Type], [Status], [WorkspaceID]) VALUES (7, N'Otros gastos', CAST(N'2023-11-16T00:00:00.0000000' AS DateTime2), 1, 0, 3)
INSERT [dbo].[Category] ([Id], [Name], [CreationDate], [Type], [Status], [WorkspaceID]) VALUES (8, N'Comida', CAST(N'2023-11-16T00:00:00.0000000' AS DateTime2), 1, 0, 2)
INSERT [dbo].[Category] ([Id], [Name], [CreationDate], [Type], [Status], [WorkspaceID]) VALUES (9, N'Casa', CAST(N'2023-11-16T00:00:00.0000000' AS DateTime2), 1, 0, 2)
INSERT [dbo].[Category] ([Id], [Name], [CreationDate], [Type], [Status], [WorkspaceID]) VALUES (10, N'Auto', CAST(N'2023-11-16T00:00:00.0000000' AS DateTime2), 1, 0, 2)
INSERT [dbo].[Category] ([Id], [Name], [CreationDate], [Type], [Status], [WorkspaceID]) VALUES (11, N'Sueldo', CAST(N'2023-08-30T00:00:00.0000000' AS DateTime2), 0, 0, 1)
INSERT [dbo].[Category] ([Id], [Name], [CreationDate], [Type], [Status], [WorkspaceID]) VALUES (12, N'Comida', CAST(N'2023-11-06T00:00:00.0000000' AS DateTime2), 1, 0, 1)
SET IDENTITY_INSERT [dbo].[Category] OFF
GO
INSERT [dbo].[CategoryGoal] ([CategoryId], [GoalId]) VALUES (4, 1)
INSERT [dbo].[CategoryGoal] ([CategoryId], [GoalId]) VALUES (1, 2)
INSERT [dbo].[CategoryGoal] ([CategoryId], [GoalId]) VALUES (3, 2)
INSERT [dbo].[CategoryGoal] ([CategoryId], [GoalId]) VALUES (1, 3)
INSERT [dbo].[CategoryGoal] ([CategoryId], [GoalId]) VALUES (3, 3)
INSERT [dbo].[CategoryGoal] ([CategoryId], [GoalId]) VALUES (4, 3)
INSERT [dbo].[CategoryGoal] ([CategoryId], [GoalId]) VALUES (6, 4)
INSERT [dbo].[CategoryGoal] ([CategoryId], [GoalId]) VALUES (6, 5)
INSERT [dbo].[CategoryGoal] ([CategoryId], [GoalId]) VALUES (7, 6)
INSERT [dbo].[CategoryGoal] ([CategoryId], [GoalId]) VALUES (8, 7)
INSERT [dbo].[CategoryGoal] ([CategoryId], [GoalId]) VALUES (8, 8)
INSERT [dbo].[CategoryGoal] ([CategoryId], [GoalId]) VALUES (9, 8)
INSERT [dbo].[CategoryGoal] ([CategoryId], [GoalId]) VALUES (10, 8)
INSERT [dbo].[CategoryGoal] ([CategoryId], [GoalId]) VALUES (12, 9)
INSERT [dbo].[CategoryGoal] ([CategoryId], [GoalId]) VALUES (6, 10)
GO
INSERT [dbo].[CreditCards] ([Id], [BankName], [LastDigits], [AvailableCredit], [DeadLine]) VALUES (1, N'Santander', N'1234', 10, 4)
INSERT [dbo].[CreditCards] ([Id], [BankName], [LastDigits], [AvailableCredit], [DeadLine]) VALUES (6, N'FUCAC', N'8985', 3000, 8)
INSERT [dbo].[CreditCards] ([Id], [BankName], [LastDigits], [AvailableCredit], [DeadLine]) VALUES (8, N'BBVA', N'5555', 6000, 7)
GO
SET IDENTITY_INSERT [dbo].[Exchange] ON 

INSERT [dbo].[Exchange] ([Id], [Date], [CurrencyValue], [Currency], [WorkspaceID]) VALUES (1, CAST(N'2023-10-05T00:00:00.0000000' AS DateTime2), 38, 1, 1)
INSERT [dbo].[Exchange] ([Id], [Date], [CurrencyValue], [Currency], [WorkspaceID]) VALUES (2, CAST(N'2023-10-05T00:00:00.0000000' AS DateTime2), 41.5, 2, 1)
INSERT [dbo].[Exchange] ([Id], [Date], [CurrencyValue], [Currency], [WorkspaceID]) VALUES (3, CAST(N'2023-11-03T00:00:00.0000000' AS DateTime2), 39.3, 1, 1)
INSERT [dbo].[Exchange] ([Id], [Date], [CurrencyValue], [Currency], [WorkspaceID]) VALUES (4, CAST(N'2023-11-09T00:00:00.0000000' AS DateTime2), 40, 2, 1)
INSERT [dbo].[Exchange] ([Id], [Date], [CurrencyValue], [Currency], [WorkspaceID]) VALUES (5, CAST(N'2023-11-14T00:00:00.0000000' AS DateTime2), 38, 1, 3)
INSERT [dbo].[Exchange] ([Id], [Date], [CurrencyValue], [Currency], [WorkspaceID]) VALUES (6, CAST(N'2023-11-14T00:00:00.0000000' AS DateTime2), 40, 2, 3)
INSERT [dbo].[Exchange] ([Id], [Date], [CurrencyValue], [Currency], [WorkspaceID]) VALUES (7, CAST(N'2023-11-04T00:00:00.0000000' AS DateTime2), 40, 1, 3)
INSERT [dbo].[Exchange] ([Id], [Date], [CurrencyValue], [Currency], [WorkspaceID]) VALUES (8, CAST(N'2023-11-14T00:00:00.0000000' AS DateTime2), 38.4, 1, 2)
INSERT [dbo].[Exchange] ([Id], [Date], [CurrencyValue], [Currency], [WorkspaceID]) VALUES (9, CAST(N'2023-11-15T00:00:00.0000000' AS DateTime2), 38.9, 1, 2)
SET IDENTITY_INSERT [dbo].[Exchange] OFF
GO
SET IDENTITY_INSERT [dbo].[Goal] ON 

INSERT [dbo].[Goal] ([Id], [Token], [Title], [MaxAmount], [WorkspaceID]) VALUES (1, NULL, N'Gastar Menos 5000', 5000, 1)
INSERT [dbo].[Goal] ([Id], [Token], [Title], [MaxAmount], [WorkspaceID]) VALUES (2, NULL, N'Gastar Menos 10000', 10000, 1)
INSERT [dbo].[Goal] ([Id], [Token], [Title], [MaxAmount], [WorkspaceID]) VALUES (3, NULL, N'Gastar Menos 20000', 20000, 1)
INSERT [dbo].[Goal] ([Id], [Token], [Title], [MaxAmount], [WorkspaceID]) VALUES (4, NULL, N'Gastar menos de 50000', 50000, 3)
INSERT [dbo].[Goal] ([Id], [Token], [Title], [MaxAmount], [WorkspaceID]) VALUES (5, NULL, N'Gastar menos de 150000', 150000, 3)
INSERT [dbo].[Goal] ([Id], [Token], [Title], [MaxAmount], [WorkspaceID]) VALUES (6, NULL, N'Gastar menos en coimas', 50000, 3)
INSERT [dbo].[Goal] ([Id], [Token], [Title], [MaxAmount], [WorkspaceID]) VALUES (7, NULL, N'Gastar menos en comida', 80000, 2)
INSERT [dbo].[Goal] ([Id], [Token], [Title], [MaxAmount], [WorkspaceID]) VALUES (8, NULL, N'Gastar menos por mes', 80000, 2)
INSERT [dbo].[Goal] ([Id], [Token], [Title], [MaxAmount], [WorkspaceID]) VALUES (9, NULL, N'Gastar Menos de 50000', 50000, 1)
INSERT [dbo].[Goal] ([Id], [Token], [Title], [MaxAmount], [WorkspaceID]) VALUES (10, NULL, N'Gastar menos en leche', 3000, 3)
SET IDENTITY_INSERT [dbo].[Goal] OFF
GO
INSERT [dbo].[PersonalAccounts] ([Id], [StartingAmount]) VALUES (2, 58000)
INSERT [dbo].[PersonalAccounts] ([Id], [StartingAmount]) VALUES (3, 2000)
INSERT [dbo].[PersonalAccounts] ([Id], [StartingAmount]) VALUES (4, 580000)
INSERT [dbo].[PersonalAccounts] ([Id], [StartingAmount]) VALUES (5, 2500)
INSERT [dbo].[PersonalAccounts] ([Id], [StartingAmount]) VALUES (7, 650000)
INSERT [dbo].[PersonalAccounts] ([Id], [StartingAmount]) VALUES (9, 70000)
INSERT [dbo].[PersonalAccounts] ([Id], [StartingAmount]) VALUES (10, 9000)
GO
SET IDENTITY_INSERT [dbo].[Transactions] ON 

INSERT [dbo].[Transactions] ([ID], [AccountId], [CategoryId], [Title], [CreationDate], [Amount], [Currency]) VALUES (1, 2, 1, N'Alquiler', CAST(N'2023-11-09T00:00:00.0000000' AS DateTime2), 14000, 0)
INSERT [dbo].[Transactions] ([ID], [AccountId], [CategoryId], [Title], [CreationDate], [Amount], [Currency]) VALUES (2, 3, 2, N'Parlante', CAST(N'2023-11-12T00:00:00.0000000' AS DateTime2), 78, 1)
INSERT [dbo].[Transactions] ([ID], [AccountId], [CategoryId], [Title], [CreationDate], [Amount], [Currency]) VALUES (3, 2, 3, N'Comida', CAST(N'2023-11-10T00:00:00.0000000' AS DateTime2), 268, 0)
INSERT [dbo].[Transactions] ([ID], [AccountId], [CategoryId], [Title], [CreationDate], [Amount], [Currency]) VALUES (4, 2, 3, N'Comida', CAST(N'2023-11-16T00:00:00.0000000' AS DateTime2), 268, 0)
INSERT [dbo].[Transactions] ([ID], [AccountId], [CategoryId], [Title], [CreationDate], [Amount], [Currency]) VALUES (5, 2, 2, N'Luz', CAST(N'2023-11-16T00:00:00.0000000' AS DateTime2), 3567, 0)
INSERT [dbo].[Transactions] ([ID], [AccountId], [CategoryId], [Title], [CreationDate], [Amount], [Currency]) VALUES (6, 4, 5, N'Changa cortando pasto', CAST(N'2023-11-16T00:00:00.0000000' AS DateTime2), 560, 0)
INSERT [dbo].[Transactions] ([ID], [AccountId], [CategoryId], [Title], [CreationDate], [Amount], [Currency]) VALUES (7, 4, 5, N'Pintar casa vecino', CAST(N'2023-11-15T00:00:00.0000000' AS DateTime2), 2500, 0)
INSERT [dbo].[Transactions] ([ID], [AccountId], [CategoryId], [Title], [CreationDate], [Amount], [Currency]) VALUES (8, 5, 5, N'Plan emergencia', CAST(N'2023-11-14T00:00:00.0000000' AS DateTime2), 550, 2)
INSERT [dbo].[Transactions] ([ID], [AccountId], [CategoryId], [Title], [CreationDate], [Amount], [Currency]) VALUES (9, 4, 7, N'Coima multa alcoholemia', CAST(N'2023-11-09T00:00:00.0000000' AS DateTime2), 6000, 0)
INSERT [dbo].[Transactions] ([ID], [AccountId], [CategoryId], [Title], [CreationDate], [Amount], [Currency]) VALUES (10, 4, 7, N'Coima multa alcoholemia', CAST(N'2023-11-16T00:00:00.0000000' AS DateTime2), 6000, 0)
INSERT [dbo].[Transactions] ([ID], [AccountId], [CategoryId], [Title], [CreationDate], [Amount], [Currency]) VALUES (11, 7, 9, N'Compra sillon', CAST(N'2023-11-16T00:00:00.0000000' AS DateTime2), 6400, 0)
INSERT [dbo].[Transactions] ([ID], [AccountId], [CategoryId], [Title], [CreationDate], [Amount], [Currency]) VALUES (12, 9, 10, N'Nafta', CAST(N'2023-11-16T00:00:00.0000000' AS DateTime2), 3000, 0)
INSERT [dbo].[Transactions] ([ID], [AccountId], [CategoryId], [Title], [CreationDate], [Amount], [Currency]) VALUES (13, 8, 9, N'Compra Play 5', CAST(N'2023-11-16T00:00:00.0000000' AS DateTime2), 640, 1)
INSERT [dbo].[Transactions] ([ID], [AccountId], [CategoryId], [Title], [CreationDate], [Amount], [Currency]) VALUES (14, 10, 12, N'Carne', CAST(N'2023-11-08T00:00:00.0000000' AS DateTime2), 600, 0)
INSERT [dbo].[Transactions] ([ID], [AccountId], [CategoryId], [Title], [CreationDate], [Amount], [Currency]) VALUES (15, 10, 12, N'Surtido Semanal', CAST(N'2023-11-16T00:00:00.0000000' AS DateTime2), 3654, 0)
INSERT [dbo].[Transactions] ([ID], [AccountId], [CategoryId], [Title], [CreationDate], [Amount], [Currency]) VALUES (16, 3, 3, N'Compra Ventilador', CAST(N'2023-11-15T00:00:00.0000000' AS DateTime2), 96, 1)
INSERT [dbo].[Transactions] ([ID], [AccountId], [CategoryId], [Title], [CreationDate], [Amount], [Currency]) VALUES (17, 10, 12, N'Carne', CAST(N'2023-11-16T00:00:00.0000000' AS DateTime2), 600, 0)
INSERT [dbo].[Transactions] ([ID], [AccountId], [CategoryId], [Title], [CreationDate], [Amount], [Currency]) VALUES (18, 10, 11, N'Deposito Ticket', CAST(N'2023-11-16T00:00:00.0000000' AS DateTime2), 8500, 0)
INSERT [dbo].[Transactions] ([ID], [AccountId], [CategoryId], [Title], [CreationDate], [Amount], [Currency]) VALUES (19, 2, 3, N'Comida', CAST(N'2023-11-16T00:00:00.0000000' AS DateTime2), 268, 0)
INSERT [dbo].[Transactions] ([ID], [AccountId], [CategoryId], [Title], [CreationDate], [Amount], [Currency]) VALUES (20, 4, 6, N'Leche', CAST(N'2023-11-16T00:00:00.0000000' AS DateTime2), 156, 0)
INSERT [dbo].[Transactions] ([ID], [AccountId], [CategoryId], [Title], [CreationDate], [Amount], [Currency]) VALUES (21, 4, 6, N'Leche', CAST(N'2023-10-31T00:00:00.0000000' AS DateTime2), 580, 0)
SET IDENTITY_INSERT [dbo].[Transactions] OFF
GO
INSERT [dbo].[Users] ([Email], [Name], [LastName], [Address], [Password]) VALUES (N'agubolso@fintrac.com', N'Agustina', N'Martinez', N'José Pedro Varela', N'1234123412')
INSERT [dbo].[Users] ([Email], [Name], [LastName], [Address], [Password]) VALUES (N'elsebamiguense@fintrac.com', N'Seba', N'Borjas', N'Migues 1234', N'1234123412')
INSERT [dbo].[Users] ([Email], [Name], [LastName], [Address], [Password]) VALUES (N'emilianomarott@gmail.com', N'Emiliano', N'Marotta', N'José Pedro Varela', N'1234123412')
GO
INSERT [dbo].[UserWorkspace] ([UserId], [WorkspaceId]) VALUES (N'elsebamiguense@fintrac.com', 1)
INSERT [dbo].[UserWorkspace] ([UserId], [WorkspaceId]) VALUES (N'emilianomarott@gmail.com', 1)
INSERT [dbo].[UserWorkspace] ([UserId], [WorkspaceId]) VALUES (N'agubolso@fintrac.com', 2)
INSERT [dbo].[UserWorkspace] ([UserId], [WorkspaceId]) VALUES (N'elsebamiguense@fintrac.com', 3)
INSERT [dbo].[UserWorkspace] ([UserId], [WorkspaceId]) VALUES (N'agubolso@fintrac.com', 4)
GO
SET IDENTITY_INSERT [dbo].[Workspace] ON 

INSERT [dbo].[Workspace] ([ID], [Name], [UserAdminId]) VALUES (1, N'Espacio personal de Emiliano Marotta', N'emilianomarott@gmail.com')
INSERT [dbo].[Workspace] ([ID], [Name], [UserAdminId]) VALUES (2, N'Espacio personal de Agustina Martinez', N'agubolso@fintrac.com')
INSERT [dbo].[Workspace] ([ID], [Name], [UserAdminId]) VALUES (3, N'Espacio personal de Seba Borjas', N'elsebamiguense@fintrac.com')
INSERT [dbo].[Workspace] ([ID], [Name], [UserAdminId]) VALUES (4, N'Gastos Familiares', N'agubolso@fintrac.com')
SET IDENTITY_INSERT [dbo].[Workspace] OFF
GO
/****** Object:  Index [IX_Accounts_WorkSpaceID]    Script Date: 16/11/2023 20:33:50 ******/
CREATE NONCLUSTERED INDEX [IX_Accounts_WorkSpaceID] ON [dbo].[Accounts]
(
	[WorkSpaceID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
/****** Object:  Index [IX_Category_WorkspaceID]    Script Date: 16/11/2023 20:33:50 ******/
CREATE NONCLUSTERED INDEX [IX_Category_WorkspaceID] ON [dbo].[Category]
(
	[WorkspaceID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
/****** Object:  Index [IX_CategoryGoal_GoalId]    Script Date: 16/11/2023 20:33:50 ******/
CREATE NONCLUSTERED INDEX [IX_CategoryGoal_GoalId] ON [dbo].[CategoryGoal]
(
	[GoalId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
/****** Object:  Index [IX_Exchange_WorkspaceID]    Script Date: 16/11/2023 20:33:50 ******/
CREATE NONCLUSTERED INDEX [IX_Exchange_WorkspaceID] ON [dbo].[Exchange]
(
	[WorkspaceID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
/****** Object:  Index [IX_Goal_WorkspaceID]    Script Date: 16/11/2023 20:33:50 ******/
CREATE NONCLUSTERED INDEX [IX_Goal_WorkspaceID] ON [dbo].[Goal]
(
	[WorkspaceID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [IX_Invitation_AdminId]    Script Date: 16/11/2023 20:33:50 ******/
CREATE NONCLUSTERED INDEX [IX_Invitation_AdminId] ON [dbo].[Invitation]
(
	[AdminId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [IX_Invitation_UserToInviteId]    Script Date: 16/11/2023 20:33:50 ******/
CREATE NONCLUSTERED INDEX [IX_Invitation_UserToInviteId] ON [dbo].[Invitation]
(
	[UserToInviteId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
/****** Object:  Index [IX_Transactions_AccountId]    Script Date: 16/11/2023 20:33:50 ******/
CREATE NONCLUSTERED INDEX [IX_Transactions_AccountId] ON [dbo].[Transactions]
(
	[AccountId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
/****** Object:  Index [IX_Transactions_CategoryId]    Script Date: 16/11/2023 20:33:50 ******/
CREATE NONCLUSTERED INDEX [IX_Transactions_CategoryId] ON [dbo].[Transactions]
(
	[CategoryId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
/****** Object:  Index [IX_UserWorkspace_WorkspaceId]    Script Date: 16/11/2023 20:33:50 ******/
CREATE NONCLUSTERED INDEX [IX_UserWorkspace_WorkspaceId] ON [dbo].[UserWorkspace]
(
	[WorkspaceId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [IX_Workspace_UserAdminId]    Script Date: 16/11/2023 20:33:50 ******/
CREATE NONCLUSTERED INDEX [IX_Workspace_UserAdminId] ON [dbo].[Workspace]
(
	[UserAdminId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
ALTER TABLE [dbo].[Accounts]  WITH CHECK ADD  CONSTRAINT [FK_Accounts_Workspace_WorkSpaceID] FOREIGN KEY([WorkSpaceID])
REFERENCES [dbo].[Workspace] ([ID])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[Accounts] CHECK CONSTRAINT [FK_Accounts_Workspace_WorkSpaceID]
GO
ALTER TABLE [dbo].[Category]  WITH CHECK ADD  CONSTRAINT [FK_Category_Workspace_WorkspaceID] FOREIGN KEY([WorkspaceID])
REFERENCES [dbo].[Workspace] ([ID])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[Category] CHECK CONSTRAINT [FK_Category_Workspace_WorkspaceID]
GO
ALTER TABLE [dbo].[CategoryGoal]  WITH CHECK ADD  CONSTRAINT [FK_CategoryGoal_Category_CategoryId] FOREIGN KEY([CategoryId])
REFERENCES [dbo].[Category] ([Id])
GO
ALTER TABLE [dbo].[CategoryGoal] CHECK CONSTRAINT [FK_CategoryGoal_Category_CategoryId]
GO
ALTER TABLE [dbo].[CategoryGoal]  WITH CHECK ADD  CONSTRAINT [FK_CategoryGoal_Goal_GoalId] FOREIGN KEY([GoalId])
REFERENCES [dbo].[Goal] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[CategoryGoal] CHECK CONSTRAINT [FK_CategoryGoal_Goal_GoalId]
GO
ALTER TABLE [dbo].[CreditCards]  WITH CHECK ADD  CONSTRAINT [FK_CreditCards_Accounts_Id] FOREIGN KEY([Id])
REFERENCES [dbo].[Accounts] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[CreditCards] CHECK CONSTRAINT [FK_CreditCards_Accounts_Id]
GO
ALTER TABLE [dbo].[Exchange]  WITH CHECK ADD  CONSTRAINT [FK_Exchange_Workspace_WorkspaceID] FOREIGN KEY([WorkspaceID])
REFERENCES [dbo].[Workspace] ([ID])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[Exchange] CHECK CONSTRAINT [FK_Exchange_Workspace_WorkspaceID]
GO
ALTER TABLE [dbo].[Goal]  WITH CHECK ADD  CONSTRAINT [FK_Goal_Workspace_WorkspaceID] FOREIGN KEY([WorkspaceID])
REFERENCES [dbo].[Workspace] ([ID])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[Goal] CHECK CONSTRAINT [FK_Goal_Workspace_WorkspaceID]
GO
ALTER TABLE [dbo].[Invitation]  WITH CHECK ADD  CONSTRAINT [FK_Invitation_Users_AdminId] FOREIGN KEY([AdminId])
REFERENCES [dbo].[Users] ([Email])
GO
ALTER TABLE [dbo].[Invitation] CHECK CONSTRAINT [FK_Invitation_Users_AdminId]
GO
ALTER TABLE [dbo].[Invitation]  WITH CHECK ADD  CONSTRAINT [FK_Invitation_Users_UserToInviteId] FOREIGN KEY([UserToInviteId])
REFERENCES [dbo].[Users] ([Email])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[Invitation] CHECK CONSTRAINT [FK_Invitation_Users_UserToInviteId]
GO
ALTER TABLE [dbo].[Invitation]  WITH CHECK ADD  CONSTRAINT [FK_Invitation_Workspace_ID] FOREIGN KEY([ID])
REFERENCES [dbo].[Workspace] ([ID])
GO
ALTER TABLE [dbo].[Invitation] CHECK CONSTRAINT [FK_Invitation_Workspace_ID]
GO
ALTER TABLE [dbo].[PersonalAccounts]  WITH CHECK ADD  CONSTRAINT [FK_PersonalAccounts_Accounts_Id] FOREIGN KEY([Id])
REFERENCES [dbo].[Accounts] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[PersonalAccounts] CHECK CONSTRAINT [FK_PersonalAccounts_Accounts_Id]
GO
ALTER TABLE [dbo].[Transactions]  WITH CHECK ADD  CONSTRAINT [FK_Transactions_Accounts_AccountId] FOREIGN KEY([AccountId])
REFERENCES [dbo].[Accounts] ([Id])
GO
ALTER TABLE [dbo].[Transactions] CHECK CONSTRAINT [FK_Transactions_Accounts_AccountId]
GO
ALTER TABLE [dbo].[Transactions]  WITH CHECK ADD  CONSTRAINT [FK_Transactions_Category_CategoryId] FOREIGN KEY([CategoryId])
REFERENCES [dbo].[Category] ([Id])
GO
ALTER TABLE [dbo].[Transactions] CHECK CONSTRAINT [FK_Transactions_Category_CategoryId]
GO
ALTER TABLE [dbo].[UserWorkspace]  WITH CHECK ADD  CONSTRAINT [FK_UserWorkspace_Users_UserId] FOREIGN KEY([UserId])
REFERENCES [dbo].[Users] ([Email])
GO
ALTER TABLE [dbo].[UserWorkspace] CHECK CONSTRAINT [FK_UserWorkspace_Users_UserId]
GO
ALTER TABLE [dbo].[UserWorkspace]  WITH CHECK ADD  CONSTRAINT [FK_UserWorkspace_Workspace_WorkspaceId] FOREIGN KEY([WorkspaceId])
REFERENCES [dbo].[Workspace] ([ID])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[UserWorkspace] CHECK CONSTRAINT [FK_UserWorkspace_Workspace_WorkspaceId]
GO
ALTER TABLE [dbo].[Workspace]  WITH CHECK ADD  CONSTRAINT [FK_Workspace_Users_UserAdminId] FOREIGN KEY([UserAdminId])
REFERENCES [dbo].[Users] ([Email])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[Workspace] CHECK CONSTRAINT [FK_Workspace_Users_UserAdminId]
GO
USE [master]
GO
ALTER DATABASE [Fintrac] SET  READ_WRITE 
GO
