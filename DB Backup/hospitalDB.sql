USE [master]
GO
/****** Object:  Database [HospitalDB]    Script Date: 12/22/2019 1:56:01 PM ******/
CREATE DATABASE [HospitalDB]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'HospitalDB', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL14.MSSQLSERVER\MSSQL\DATA\HospitalDB.mdf' , SIZE = 8192KB , MAXSIZE = UNLIMITED, FILEGROWTH = 65536KB )
 LOG ON 
( NAME = N'HospitalDB_log', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL14.MSSQLSERVER\MSSQL\DATA\HospitalDB_log.ldf' , SIZE = 8192KB , MAXSIZE = 2048GB , FILEGROWTH = 65536KB )
GO
ALTER DATABASE [HospitalDB] SET COMPATIBILITY_LEVEL = 140
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [HospitalDB].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [HospitalDB] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [HospitalDB] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [HospitalDB] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [HospitalDB] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [HospitalDB] SET ARITHABORT OFF 
GO
ALTER DATABASE [HospitalDB] SET AUTO_CLOSE OFF 
GO
ALTER DATABASE [HospitalDB] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [HospitalDB] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [HospitalDB] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [HospitalDB] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [HospitalDB] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [HospitalDB] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [HospitalDB] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [HospitalDB] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [HospitalDB] SET  DISABLE_BROKER 
GO
ALTER DATABASE [HospitalDB] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [HospitalDB] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [HospitalDB] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [HospitalDB] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [HospitalDB] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [HospitalDB] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [HospitalDB] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [HospitalDB] SET RECOVERY SIMPLE 
GO
ALTER DATABASE [HospitalDB] SET  MULTI_USER 
GO
ALTER DATABASE [HospitalDB] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [HospitalDB] SET DB_CHAINING OFF 
GO
ALTER DATABASE [HospitalDB] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [HospitalDB] SET TARGET_RECOVERY_TIME = 60 SECONDS 
GO
ALTER DATABASE [HospitalDB] SET DELAYED_DURABILITY = DISABLED 
GO
EXEC sys.sp_db_vardecimal_storage_format N'HospitalDB', N'ON'
GO
ALTER DATABASE [HospitalDB] SET QUERY_STORE = OFF
GO
USE [HospitalDB]
GO
ALTER DATABASE SCOPED CONFIGURATION SET IDENTITY_CACHE = ON;
GO
ALTER DATABASE SCOPED CONFIGURATION SET LEGACY_CARDINALITY_ESTIMATION = OFF;
GO
ALTER DATABASE SCOPED CONFIGURATION FOR SECONDARY SET LEGACY_CARDINALITY_ESTIMATION = PRIMARY;
GO
ALTER DATABASE SCOPED CONFIGURATION SET MAXDOP = 0;
GO
ALTER DATABASE SCOPED CONFIGURATION FOR SECONDARY SET MAXDOP = PRIMARY;
GO
ALTER DATABASE SCOPED CONFIGURATION SET PARAMETER_SNIFFING = ON;
GO
ALTER DATABASE SCOPED CONFIGURATION FOR SECONDARY SET PARAMETER_SNIFFING = PRIMARY;
GO
ALTER DATABASE SCOPED CONFIGURATION SET QUERY_OPTIMIZER_HOTFIXES = OFF;
GO
ALTER DATABASE SCOPED CONFIGURATION FOR SECONDARY SET QUERY_OPTIMIZER_HOTFIXES = PRIMARY;
GO
USE [HospitalDB]
GO
/****** Object:  Table [dbo].[Appointment]    Script Date: 12/22/2019 1:56:01 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Appointment](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[PatientID] [int] NULL,
	[DoctorID] [int] NULL,
	[Date] [datetime] NULL,
	[Amount] [decimal](5, 2) NULL,
 CONSTRAINT [PK_Appointment] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Bed]    Script Date: 12/22/2019 1:56:02 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Bed](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[RoomID] [int] NULL,
 CONSTRAINT [PK_Bed] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Bill]    Script Date: 12/22/2019 1:56:02 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Bill](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[PatientID] [int] NULL,
	[DiagnosisID] [int] NULL,
	[PrescriptionID] [int] NULL,
	[Date] [datetime] NULL,
 CONSTRAINT [PK_Bill] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Diagnosis]    Script Date: 12/22/2019 1:56:02 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Diagnosis](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[PatientID] [int] NULL,
	[DoctorID] [int] NULL,
	[TreatmentID] [int] NULL,
	[Comments] [varchar](max) NULL,
	[Date] [datetime] NULL,
 CONSTRAINT [PK_Treatment] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Doctor]    Script Date: 12/22/2019 1:56:02 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Doctor](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[Name] [varchar](50) NULL,
	[NIC] [varchar](12) NULL,
	[Contact] [varchar](10) NULL,
	[Email] [varchar](100) NULL,
	[Address] [varchar](max) NULL,
	[UserID] [int] NULL,
	[CatergoryID] [int] NULL,
 CONSTRAINT [PK_Doctor] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Doctor_Category]    Script Date: 12/22/2019 1:56:02 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Doctor_Category](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[Name] [varchar](50) NULL,
	[DateAdded] [datetime] NULL,
 CONSTRAINT [PK_Doctor_Category] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[In_Patient]    Script Date: 12/22/2019 1:56:02 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[In_Patient](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[Patient_ID] [int] NULL,
	[Doctor_ID] [int] NULL,
	[Date] [datetime] NULL,
	[BedID] [int] NULL,
 CONSTRAINT [PK_In_Patient] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Inventory]    Script Date: 12/22/2019 1:56:02 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Inventory](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[MedicineID] [int] NULL,
	[Quantity] [int] NULL,
 CONSTRAINT [PK_Inventory] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Medicine]    Script Date: 12/22/2019 1:56:02 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Medicine](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[Name] [varchar](50) NULL,
	[Category] [varchar](50) NULL,
	[Price] [decimal](6, 2) NULL,
 CONSTRAINT [PK_Medicine] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[MedItems]    Script Date: 12/22/2019 1:56:02 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[MedItems](
	[MID] [int] IDENTITY(1,1) NOT NULL,
	[PID] [bigint] NOT NULL,
	[MItem] [varchar](20) NOT NULL,
	[CostPrice] [int] NOT NULL,
	[SellingPrice] [int] NOT NULL,
	[Quantity] [int] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[MID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Out_Patient]    Script Date: 12/22/2019 1:56:02 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Out_Patient](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[PatinetID] [int] NULL,
	[DoctorID] [int] NULL,
	[Date] [datetime] NULL,
 CONSTRAINT [PK_Out_Patient] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Patient]    Script Date: 12/22/2019 1:56:02 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Patient](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[Name] [varchar](50) NULL,
	[Contact] [varchar](10) NULL,
	[Address] [varchar](max) NULL,
	[NIC] [varchar](12) NULL,
	[IsNonNIC] [bit] NULL,
	[GuardianNIC] [varchar](12) NULL,
 CONSTRAINT [PK_Patient] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Prescription]    Script Date: 12/22/2019 1:56:02 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Prescription](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[PatinetID] [int] NULL,
	[DoctorID] [int] NULL,
	[MedicineID] [int] NULL,
	[Price] [decimal](6, 2) NULL,
	[Date] [datetime] NULL,
 CONSTRAINT [PK_Prescription] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Products]    Script Date: 12/22/2019 1:56:03 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Products](
	[PID] [int] IDENTITY(1,1) NOT NULL,
	[PTitle] [varchar](100) NOT NULL,
	[DateAdded] [datetime] NULL,
PRIMARY KEY CLUSTERED 
(
	[PID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Report]    Script Date: 12/22/2019 1:56:03 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Report](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[PatientID] [int] NULL,
	[ReportType] [int] NULL,
	[Results] [varchar](max) NULL,
	[SampleDate] [datetime] NULL,
	[TestedDate] [datetime] NULL,
	[Remarks] [varchar](max) NULL,
	[Fee] [decimal](18, 2) NULL,
	[ReportHtml] [varchar](max) NULL,
 CONSTRAINT [PK_Report] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[ReportType]    Script Date: 12/22/2019 1:56:03 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ReportType](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[Name] [varchar](50) NULL,
	[DateAdded] [datetime] NULL,
 CONSTRAINT [PK_ReportType] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Room]    Script Date: 12/22/2019 1:56:03 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Room](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[Type] [varchar](50) NULL,
	[Price] [decimal](6, 2) NULL,
 CONSTRAINT [PK_Room] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Sales]    Script Date: 12/22/2019 1:56:03 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Sales](
	[SID] [int] IDENTITY(1,1) NOT NULL,
	[IID] [bigint] NOT NULL,
	[Date] [varchar](20) NOT NULL,
	[PatientID] [bigint] NOT NULL,
	[TotalBill] [int] NOT NULL,
	[Quantity] [int] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[SID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Treatment]    Script Date: 12/22/2019 1:56:03 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Treatment](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[Name] [varchar](50) NULL,
	[CategoryID] [int] NULL,
	[Price] [decimal](6, 2) NULL
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Treatment_Category]    Script Date: 12/22/2019 1:56:03 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Treatment_Category](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[Name] [varchar](50) NULL
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[User]    Script Date: 12/22/2019 1:56:03 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[User](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[Firstname] [varchar](50) NULL,
	[Lastname] [varchar](50) NULL,
	[Address_line_1] [varchar](50) NULL,
	[Address_line_2] [varchar](50) NULL,
	[PostalCode] [varchar](50) NULL,
	[Email] [varchar](50) NULL,
	[Password] [varchar](max) NULL,
	[Salt] [varchar](max) NULL,
	[Gender] [varchar](50) NULL,
	[MobileNo] [varchar](50) NULL,
	[RoleID] [int] NULL,
	[Doctor_Category] [int] NULL,
	[RegisteredDate] [datetime] NULL,
	[ActiveStatus] [int] NULL,
 CONSTRAINT [PK_User] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[User_Login]    Script Date: 12/22/2019 1:56:03 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[User_Login](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[userID] [int] NULL,
	[user_role] [int] NULL,
	[user_login_os] [varchar](50) NULL,
	[user_login_date] [datetime] NULL,
	[user_logged_in_timezone] [varchar](50) NULL,
	[user_logged_in_IP] [varchar](50) NULL,
	[user_logged_out_date] [varchar](50) NULL,
	[token] [varchar](max) NULL,
 CONSTRAINT [PK_user_login] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[User_Role]    Script Date: 12/22/2019 1:56:03 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[User_Role](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[Name] [varchar](50) NULL,
	[DateAdded] [datetime] NULL,
 CONSTRAINT [PK_User_Role] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET IDENTITY_INSERT [dbo].[Doctor_Category] ON 

INSERT [dbo].[Doctor_Category] ([ID], [Name], [DateAdded]) VALUES (1, N'General', CAST(N'2019-12-18T22:31:20.000' AS DateTime))
INSERT [dbo].[Doctor_Category] ([ID], [Name], [DateAdded]) VALUES (2, N'Cardiologists', CAST(N'2019-12-18T22:36:35.000' AS DateTime))
SET IDENTITY_INSERT [dbo].[Doctor_Category] OFF
SET IDENTITY_INSERT [dbo].[Patient] ON 

INSERT [dbo].[Patient] ([ID], [Name], [Contact], [Address], [NIC], [IsNonNIC], [GuardianNIC]) VALUES (1, N'Azeem', N'0777555888', N'Colombo', N'912040542V', 0, N'N/A')
SET IDENTITY_INSERT [dbo].[Patient] OFF
SET IDENTITY_INSERT [dbo].[Products] ON 

INSERT [dbo].[Products] ([PID], [PTitle], [DateAdded]) VALUES (1, N'Pain Killers', NULL)
SET IDENTITY_INSERT [dbo].[Products] OFF
SET IDENTITY_INSERT [dbo].[Report] ON 

INSERT [dbo].[Report] ([ID], [PatientID], [ReportType], [Results], [SampleDate], [TestedDate], [Remarks], [Fee], [ReportHtml]) VALUES (1, 1, 2, N'Add New Report
PatientAdd New Report
PatientAdd New Report
PatientAdd New Report
PatientAdd New Report
Patient', CAST(N'2019-12-10T00:00:00.000' AS DateTime), CAST(N'2019-12-12T00:00:00.000' AS DateTime), N'Add New Report
PatientAdd New Report
PatientAdd New Report
PatientAdd New Report
PatientAdd New Report
Patient', CAST(4000.00 AS Decimal(18, 2)), N'
        <div class="row">
            <div class="col-md-3">
                <p>Name: </p>
            </div>
            <div class="col-md-9">
                <p id="PatientNameRpt">Azeem</p>
            </div>
        </div>
        <div class="row">
            <div class="col-md-3">
                <p>Report Type: </p>
            </div>
            <div class="col-md-9">
                <p id="ReportTypeRpt">Diabetic Report</p>
            </div>
        </div>
        <div class="row">
            <div class="col-md-3">
                <p>Sample Date: </p>
            </div>
            <div class="col-md-9">
                <p id="SampleDateRpt">Tue Dec 10 2019 05:30:00 GMT+0530 (India Standard Time)</p>
            </div>
        </div>
        <div class="row">
            <div class="col-md-3">
                <p>Tested Date: </p>
            </div>
            <div class="col-md-9">
                <p id="TestedDateRpt">Thu Dec 12 2019 05:30:00 GMT+0530 (India Standard Time)</p>
            </div>
        </div>
        <div class="row">
            <div class="col-md-3">
                <p>Results: </p>
            </div>
            <div class="col-md-9">
                <p id="ResultsRpt">Add New Report
PatientAdd New Report
PatientAdd New Report
PatientAdd New Report
PatientAdd New Report
Patient</p>
            </div>
        </div>
        <div class="row">
            <div class="col-md-3">
                <p>Remarks: </p>
            </div>
            <div class="col-md-9">
                <p id="RemarksRpt">Add New Report
PatientAdd New Report
PatientAdd New Report
PatientAdd New Report
PatientAdd New Report
Patient</p>
            </div>
        </div>
        <div class="row">
            <div class="col-md-3">
                <p>Fee: </p>
            </div>
            <div class="col-md-9">
                <p id="FeeRpt">4000</p>
            </div>
        </div>
      ')
SET IDENTITY_INSERT [dbo].[Report] OFF
SET IDENTITY_INSERT [dbo].[ReportType] ON 

INSERT [dbo].[ReportType] ([ID], [Name], [DateAdded]) VALUES (2, N'Diabetic Report', CAST(N'2019-12-21T18:40:00.457' AS DateTime))
SET IDENTITY_INSERT [dbo].[ReportType] OFF
SET IDENTITY_INSERT [dbo].[User] ON 

INSERT [dbo].[User] ([ID], [Firstname], [Lastname], [Address_line_1], [Address_line_2], [PostalCode], [Email], [Password], [Salt], [Gender], [MobileNo], [RoleID], [Doctor_Category], [RegisteredDate], [ActiveStatus]) VALUES (4, N'TEST', N'User', N'', N'', N'', N'TEST@test.com', N'dqHJETJY08vrlWmQ+ON+QeXTwERurPR32/b5zZadH7U=', N'BOPf+iUDdcSaTkfSmrtlZ7T1ioB6/6KiI/LEmPh6+w5lLHJAwOO8kET3KPEwGB5u1UQsdzB+K8rCcly9Z1DTD4bGhxcE9L2lv3foO04xCDMZk2PFLSH9huXAxFvuKL7ZxIObFgTAuUYzosG9iie3wP4O9aXEI4+LuzBgyb2vsBCrKPA7BAMYDQrtJDEUwJZWlma2NP8dtvFWngv1aEKGMUgr1IAdfhzRTYPNS+D4/EHRuA726nf6M5JuO94pieRUD6L5rL5Zq873DiybyvE8Lhl+ZaHHD4sWKqCVWusX5+nuye6DiiEOzNBNV5QfTtk4nTgr9gNscHyr1mJYBUUwOA==', N'Male', N'23123131', 1, 0, CAST(N'2019-12-21T11:08:10.000' AS DateTime), 1)
SET IDENTITY_INSERT [dbo].[User] OFF
SET IDENTITY_INSERT [dbo].[User_Login] ON 

INSERT [dbo].[User_Login] ([ID], [userID], [user_role], [user_login_os], [user_login_date], [user_logged_in_timezone], [user_logged_in_IP], [user_logged_out_date], [token]) VALUES (1, 4, 0, N'Microsoft Windows NT 10.0.18362.0', CAST(N'2019-12-21T14:06:44.467' AS DateTime), N'System.CurrentSystemTimeZone', N'fe80::c8eb:4a99:12e6:7632', N'Jan  1 1900 12:00AM', N'eyJhbGciOiJIUzUxMiIsInR5cCI6IkpXVCJ9.eyJuYW1laWQiOiIwIiwidW5pcXVlX25hbWUiOiJURVNUQHRlc3QuY29tIiwibmJmIjoxNTc2OTE3MzkxLCJleHAiOjE1NzcwMDM3ODksImlhdCI6MTU3NjkxNzM5MX0.2VR_l3hZ2AycZLe_o00VPBGzchMpFkIABLbZrrlpCqQ9m2tp1VfqpV3Wl2cZuKnPjS-GIpL53xRsKtbWp3duww')
INSERT [dbo].[User_Login] ([ID], [userID], [user_role], [user_login_os], [user_login_date], [user_logged_in_timezone], [user_logged_in_IP], [user_logged_out_date], [token]) VALUES (2, 4, 5, N'Microsoft Windows NT 10.0.18362.0', CAST(N'2019-12-21T14:11:34.580' AS DateTime), N'System.CurrentSystemTimeZone', N'fe80::c8eb:4a99:12e6:7632', N'Jan  1 1900 12:00AM', N'eyJhbGciOiJIUzUxMiIsInR5cCI6IkpXVCJ9.eyJuYW1laWQiOiIwIiwidW5pcXVlX25hbWUiOiJURVNUQHRlc3QuY29tIiwibmJmIjoxNTc2OTE3Njk0LCJleHAiOjE1NzcwMDQwOTQsImlhdCI6MTU3NjkxNzY5NH0.1Xyx8MOoyDlzI8LxJTXFVS_--I4Uo0NxXWLR3tYYfjOHDWur3TyfNkDIxggj85vbNhlXbQafW1O2CU2nMfM2FQ')
INSERT [dbo].[User_Login] ([ID], [userID], [user_role], [user_login_os], [user_login_date], [user_logged_in_timezone], [user_logged_in_IP], [user_logged_out_date], [token]) VALUES (3, 4, 5, N'Microsoft Windows NT 10.0.18362.0', CAST(N'2019-12-21T14:13:20.950' AS DateTime), N'System.CurrentSystemTimeZone', N'fe80::c8eb:4a99:12e6:7632', N'Jan  1 1900 12:00AM', N'eyJhbGciOiJIUzUxMiIsInR5cCI6IkpXVCJ9.eyJuYW1laWQiOiIwIiwidW5pcXVlX25hbWUiOiJURVNUQHRlc3QuY29tIiwibmJmIjoxNTc2OTE3ODAwLCJleHAiOjE1NzcwMDQyMDAsImlhdCI6MTU3NjkxNzgwMH0.zN2KZtno5mVuUQhg9k2o17iJrphCYZIK3KUydNvlfAi_rxjaKen410kSa4h77FNBN0V2ewdTUyKOsV6SoDfiSA')
INSERT [dbo].[User_Login] ([ID], [userID], [user_role], [user_login_os], [user_login_date], [user_logged_in_timezone], [user_logged_in_IP], [user_logged_out_date], [token]) VALUES (4, 4, 5, N'Microsoft Windows NT 10.0.18362.0', CAST(N'2019-12-21T14:19:48.053' AS DateTime), N'System.CurrentSystemTimeZone', N'fe80::c8eb:4a99:12e6:7632', N'Jan  1 1900 12:00AM', N'eyJhbGciOiJIUzUxMiIsInR5cCI6IkpXVCJ9.eyJuYW1laWQiOiIwIiwidW5pcXVlX25hbWUiOiJURVNUQHRlc3QuY29tIiwibmJmIjoxNTc2OTE4MTg4LCJleHAiOjE1NzcwMDQ1ODgsImlhdCI6MTU3NjkxODE4OH0.9djs1nZ8SmPjzhfmG2813-Gq5_hS3D9Wc_0iGhFNuTt29hMFqrTYcYRw621THv8eDo7EyxrmS0Ndhviqmznpaw')
INSERT [dbo].[User_Login] ([ID], [userID], [user_role], [user_login_os], [user_login_date], [user_logged_in_timezone], [user_logged_in_IP], [user_logged_out_date], [token]) VALUES (5, 4, 5, N'Microsoft Windows NT 10.0.18362.0', CAST(N'2019-12-21T14:21:24.537' AS DateTime), N'System.CurrentSystemTimeZone', N'fe80::c8eb:4a99:12e6:7632', N'Jan  1 1900 12:00AM', N'eyJhbGciOiJIUzUxMiIsInR5cCI6IkpXVCJ9.eyJuYW1laWQiOiIwIiwidW5pcXVlX25hbWUiOiJURVNUQHRlc3QuY29tIiwibmJmIjoxNTc2OTE4Mjg0LCJleHAiOjE1NzcwMDQ2ODQsImlhdCI6MTU3NjkxODI4NH0.hC3y4I_auB_4pCaYluwImFGdvjxp1ncX6gqvKNIdOWfKq4oPtcv1PapQShDtnU-uvxRdCjBFC-H1FVLdC1qpCA')
INSERT [dbo].[User_Login] ([ID], [userID], [user_role], [user_login_os], [user_login_date], [user_logged_in_timezone], [user_logged_in_IP], [user_logged_out_date], [token]) VALUES (6, 4, 5, N'Microsoft Windows NT 10.0.18362.0', CAST(N'2019-12-21T14:22:18.783' AS DateTime), N'System.CurrentSystemTimeZone', N'fe80::c8eb:4a99:12e6:7632', N'Jan  1 1900 12:00AM', N'eyJhbGciOiJIUzUxMiIsInR5cCI6IkpXVCJ9.eyJuYW1laWQiOiIwIiwidW5pcXVlX25hbWUiOiJURVNUQHRlc3QuY29tIiwibmJmIjoxNTc2OTE4MzM4LCJleHAiOjE1NzcwMDQ3MzgsImlhdCI6MTU3NjkxODMzOH0.CQzZYGNCA85sFvPREkTa4Iu2_NNxNyjc-_jDDMHYOFQYq9EN53Wirc623EMXjAdWIitND1cyCdW0BlsT2curYA')
INSERT [dbo].[User_Login] ([ID], [userID], [user_role], [user_login_os], [user_login_date], [user_logged_in_timezone], [user_logged_in_IP], [user_logged_out_date], [token]) VALUES (7, 4, 1, N'Microsoft Windows NT 10.0.18362.0', CAST(N'2019-12-21T14:23:08.383' AS DateTime), N'System.CurrentSystemTimeZone', N'fe80::c8eb:4a99:12e6:7632', N'Jan  1 1900 12:00AM', N'eyJhbGciOiJIUzUxMiIsInR5cCI6IkpXVCJ9.eyJuYW1laWQiOiIwIiwidW5pcXVlX25hbWUiOiJURVNUQHRlc3QuY29tIiwibmJmIjoxNTc2OTE4Mzg4LCJleHAiOjE1NzcwMDQ3ODgsImlhdCI6MTU3NjkxODM4OH0.t78g4KaryBoFknYr-k8P9ZzBqzLudpvmIiO6uJXAy681wvxROW4LJXqh_RuGqWt6w_b7sklxMpbS2y-Aj4VTQw')
INSERT [dbo].[User_Login] ([ID], [userID], [user_role], [user_login_os], [user_login_date], [user_logged_in_timezone], [user_logged_in_IP], [user_logged_out_date], [token]) VALUES (8, 4, 1, N'Microsoft Windows NT 10.0.18362.0', CAST(N'2019-12-21T15:17:26.737' AS DateTime), N'System.CurrentSystemTimeZone', N'fe80::c8eb:4a99:12e6:7632', N'Jan  1 1900 12:00AM', N'eyJhbGciOiJIUzUxMiIsInR5cCI6IkpXVCJ9.eyJuYW1laWQiOiIwIiwidW5pcXVlX25hbWUiOiJURVNUQHRlc3QuY29tIiwibmJmIjoxNTc2OTIxNjQ2LCJleHAiOjE1NzcwMDgwNDYsImlhdCI6MTU3NjkyMTY0Nn0.5-6vkmjfebxgawPDNVJ-D-wt-t-ZPyIWoRMxt23Q2V6ZUlq8KEHYYiVASqQWvvn90V9UyAKtXZnwAZYytrlLEQ')
INSERT [dbo].[User_Login] ([ID], [userID], [user_role], [user_login_os], [user_login_date], [user_logged_in_timezone], [user_logged_in_IP], [user_logged_out_date], [token]) VALUES (9, 4, 1, N'Microsoft Windows NT 10.0.18362.0', CAST(N'2019-12-21T15:20:56.483' AS DateTime), N'System.CurrentSystemTimeZone', N'fe80::c8eb:4a99:12e6:7632', N'Jan  1 1900 12:00AM', N'eyJhbGciOiJIUzUxMiIsInR5cCI6IkpXVCJ9.eyJuYW1laWQiOiIwIiwidW5pcXVlX25hbWUiOiJURVNUQHRlc3QuY29tIiwibmJmIjoxNTc2OTIxODU2LCJleHAiOjE1NzcwMDgyNTYsImlhdCI6MTU3NjkyMTg1Nn0.yDmHrkBMeF0NgP49JkbbJU2zRVNK7G9tQ7cT5BGecJmSiQfUitO-IIk1HOF39_7h4eijZJNe9D5p3kS1YvC8Lg')
INSERT [dbo].[User_Login] ([ID], [userID], [user_role], [user_login_os], [user_login_date], [user_logged_in_timezone], [user_logged_in_IP], [user_logged_out_date], [token]) VALUES (10, 4, 1, N'Microsoft Windows NT 10.0.18362.0', CAST(N'2019-12-21T16:28:51.993' AS DateTime), N'System.CurrentSystemTimeZone', N'fe80::c8eb:4a99:12e6:7632', N'Jan  1 1900 12:00AM', N'eyJhbGciOiJIUzUxMiIsInR5cCI6IkpXVCJ9.eyJuYW1laWQiOiIwIiwidW5pcXVlX25hbWUiOiJURVNUQHRlc3QuY29tIiwibmJmIjoxNTc2OTI1OTMxLCJleHAiOjE1NzcwMTIzMzEsImlhdCI6MTU3NjkyNTkzMX0.PxoxpJHjQk8xKizJgId7q3zpC6bRN7ZtqWBKFNWlszPso3d0vJZpsiQkQDs4hdC264KGlxy-m5dcN0ZTH8ZFRw')
INSERT [dbo].[User_Login] ([ID], [userID], [user_role], [user_login_os], [user_login_date], [user_logged_in_timezone], [user_logged_in_IP], [user_logged_out_date], [token]) VALUES (11, 4, 1, N'Microsoft Windows NT 10.0.18362.0', CAST(N'2019-12-21T18:09:10.903' AS DateTime), N'System.CurrentSystemTimeZone', N'fe80::c8eb:4a99:12e6:7632', N'', N'eyJhbGciOiJIUzUxMiIsInR5cCI6IkpXVCJ9.eyJuYW1laWQiOiI0IiwidW5pcXVlX25hbWUiOiJURVNUQHRlc3QuY29tIiwibmJmIjoxNTc2OTMxOTUwLCJleHAiOjE1NzcwMTgzNTAsImlhdCI6MTU3NjkzMTk1MH0.zF9EHti8b3vCpEdHPYnu5rI28A4bouBW3MaoXgkr8Wwby8Z6_Lm4mwfFvnP8lYDyAaNS_nX-tBMxfQETd245rg')
INSERT [dbo].[User_Login] ([ID], [userID], [user_role], [user_login_os], [user_login_date], [user_logged_in_timezone], [user_logged_in_IP], [user_logged_out_date], [token]) VALUES (12, 4, 1, N'Microsoft Windows NT 10.0.18362.0', CAST(N'2019-12-21T18:26:10.180' AS DateTime), N'System.CurrentSystemTimeZone', N'fe80::c8eb:4a99:12e6:7632', N'', N'eyJhbGciOiJIUzUxMiIsInR5cCI6IkpXVCJ9.eyJuYW1laWQiOiI0IiwidW5pcXVlX25hbWUiOiJURVNUQHRlc3QuY29tIiwibmJmIjoxNTc2OTMyOTY0LCJleHAiOjE1NzcwMTkzNjMsImlhdCI6MTU3NjkzMjk2NH0.zALeB1nc9-OIg7oAAPs1MRPk8B8unpQsSGJAuCh3kt7CapcSrZkpk5XY7G-DkpsR6CmLs363L7x5Dy6Cz9QQAA')
INSERT [dbo].[User_Login] ([ID], [userID], [user_role], [user_login_os], [user_login_date], [user_logged_in_timezone], [user_logged_in_IP], [user_logged_out_date], [token]) VALUES (13, 4, 1, N'Microsoft Windows NT 10.0.18362.0', CAST(N'2019-12-21T20:07:00.873' AS DateTime), N'System.CurrentSystemTimeZone', N'fe80::c8eb:4a99:12e6:7632', N'', N'eyJhbGciOiJIUzUxMiIsInR5cCI6IkpXVCJ9.eyJuYW1laWQiOiI0IiwidW5pcXVlX25hbWUiOiJURVNUQHRlc3QuY29tIiwibmJmIjoxNTc2OTM5MDE5LCJleHAiOjE1NzcwMjU0MTcsImlhdCI6MTU3NjkzOTAxOX0.IqFejGt1AuA0A0r-lKc_SumRMx4z6-BFUmMrDAvCjPR2ADlb1APpN5RXkcDRQDVPtUlkf_TwIEIAmpQ99ZU9Pg')
INSERT [dbo].[User_Login] ([ID], [userID], [user_role], [user_login_os], [user_login_date], [user_logged_in_timezone], [user_logged_in_IP], [user_logged_out_date], [token]) VALUES (14, 4, 1, N'Microsoft Windows NT 10.0.18362.0', CAST(N'2019-12-21T20:19:42.373' AS DateTime), N'System.CurrentSystemTimeZone', N'fe80::c8eb:4a99:12e6:7632', N'', N'eyJhbGciOiJIUzUxMiIsInR5cCI6IkpXVCJ9.eyJuYW1laWQiOiI0IiwidW5pcXVlX25hbWUiOiJURVNUQHRlc3QuY29tIiwibmJmIjoxNTc2OTM5NzgyLCJleHAiOjE1NzcwMjYxODIsImlhdCI6MTU3NjkzOTc4Mn0.qjPLI4OClitI3xdjLSgk-4Yf_eZs8b2wVRd3N5plgfhKs6l10R2aRE3aELy5wjdSE03H_H6jG2rzBX0_EV6OmA')
INSERT [dbo].[User_Login] ([ID], [userID], [user_role], [user_login_os], [user_login_date], [user_logged_in_timezone], [user_logged_in_IP], [user_logged_out_date], [token]) VALUES (15, 4, 1, N'Microsoft Windows NT 10.0.18362.0', CAST(N'2019-12-21T20:20:41.007' AS DateTime), N'System.CurrentSystemTimeZone', N'fe80::c8eb:4a99:12e6:7632', N'', N'eyJhbGciOiJIUzUxMiIsInR5cCI6IkpXVCJ9.eyJuYW1laWQiOiI0IiwidW5pcXVlX25hbWUiOiJURVNUQHRlc3QuY29tIiwibmJmIjoxNTc2OTM5ODQxLCJleHAiOjE1NzcwMjYyNDEsImlhdCI6MTU3NjkzOTg0MX0.MPCTTkDy6mCmu0zm-n9tNWFYiYPScduKOKf2eHXfd8jJeVB0QIlWa0qWxVTZfb3VCUtUg6MRmVnHBWCteDqcgA')
INSERT [dbo].[User_Login] ([ID], [userID], [user_role], [user_login_os], [user_login_date], [user_logged_in_timezone], [user_logged_in_IP], [user_logged_out_date], [token]) VALUES (16, 4, 1, N'Microsoft Windows NT 10.0.18362.0', CAST(N'2019-12-21T21:32:04.147' AS DateTime), N'System.CurrentSystemTimeZone', N'fe80::c8eb:4a99:12e6:7632', N'', N'eyJhbGciOiJIUzUxMiIsInR5cCI6IkpXVCJ9.eyJuYW1laWQiOiI0IiwidW5pcXVlX25hbWUiOiJURVNUQHRlc3QuY29tIiwibmJmIjoxNTc2OTQ0MTI0LCJleHAiOjE1NzcwMzA1MjQsImlhdCI6MTU3Njk0NDEyNH0.cWkAy51uI1joqTSqXkjhHjr2IT4Y5Xi2FGmQC8V-VW25qZrdEHnT4gsZjPzXJNEQzkGqpHIyqpt1bOCjOy2MBw')
INSERT [dbo].[User_Login] ([ID], [userID], [user_role], [user_login_os], [user_login_date], [user_logged_in_timezone], [user_logged_in_IP], [user_logged_out_date], [token]) VALUES (17, 4, 1, N'Microsoft Windows NT 10.0.18362.0', CAST(N'2019-12-22T09:35:53.000' AS DateTime), N'System.CurrentSystemTimeZone', N'fe80::c9b1:b2dd:6a19:a50f', N'', N'eyJhbGciOiJIUzUxMiIsInR5cCI6IkpXVCJ9.eyJuYW1laWQiOiI0IiwidW5pcXVlX25hbWUiOiJURVNUQHRlc3QuY29tIiwibmJmIjoxNTc2OTg3NTUyLCJleHAiOjE1NzcwNzM5NTIsImlhdCI6MTU3Njk4NzU1Mn0.DvZFxD81F0ivtQVJsIX5W7-OXj5RaP3zp33bvhJi34NxtsIq2StA222OgIluHL61bD9EAbPEx-_N-o-8zBBhYQ')
INSERT [dbo].[User_Login] ([ID], [userID], [user_role], [user_login_os], [user_login_date], [user_logged_in_timezone], [user_logged_in_IP], [user_logged_out_date], [token]) VALUES (18, 4, 1, N'Microsoft Windows NT 10.0.18362.0', CAST(N'2019-12-22T11:41:16.910' AS DateTime), N'System.CurrentSystemTimeZone', N'fe80::c9b1:b2dd:6a19:a50f', N'', N'eyJhbGciOiJIUzUxMiIsInR5cCI6IkpXVCJ9.eyJuYW1laWQiOiI0IiwidW5pcXVlX25hbWUiOiJURVNUQHRlc3QuY29tIiwibmJmIjoxNTc2OTk1MDc2LCJleHAiOjE1NzcwODE0NzYsImlhdCI6MTU3Njk5NTA3Nn0.pNTrHnlSyy7l2RqLsN2HI4KG2MSpZfbFEfAt-MwcZS5pKevqNPQaVg4WWmoBu6-qwqZ4DUFFWJfif_iD0puRvQ')
INSERT [dbo].[User_Login] ([ID], [userID], [user_role], [user_login_os], [user_login_date], [user_logged_in_timezone], [user_logged_in_IP], [user_logged_out_date], [token]) VALUES (19, 4, 1, N'Microsoft Windows NT 10.0.18362.0', CAST(N'2019-12-22T12:04:05.800' AS DateTime), N'System.CurrentSystemTimeZone', N'fe80::c9b1:b2dd:6a19:a50f', N'', N'eyJhbGciOiJIUzUxMiIsInR5cCI6IkpXVCJ9.eyJuYW1laWQiOiI0IiwidW5pcXVlX25hbWUiOiJURVNUQHRlc3QuY29tIiwibmJmIjoxNTc2OTk2NDQ1LCJleHAiOjE1NzcwODI4NDUsImlhdCI6MTU3Njk5NjQ0NX0.Gq4kH8oNWcxA57Pd9W9pnCt4Lk3HFr5N7gwpSfkDybah3PQtfCSm5SN-9VLRZ6raBfJ9PKt4sNmU453Md1E0gg')
SET IDENTITY_INSERT [dbo].[User_Login] OFF
SET IDENTITY_INSERT [dbo].[User_Role] ON 

INSERT [dbo].[User_Role] ([ID], [Name], [DateAdded]) VALUES (1, N'Super Admin', CAST(N'2019-12-18T22:12:53.000' AS DateTime))
INSERT [dbo].[User_Role] ([ID], [Name], [DateAdded]) VALUES (2, N'Doctor', CAST(N'2019-12-18T22:14:56.000' AS DateTime))
INSERT [dbo].[User_Role] ([ID], [Name], [DateAdded]) VALUES (5, N'Nurse', CAST(N'2019-12-19T23:32:14.810' AS DateTime))
SET IDENTITY_INSERT [dbo].[User_Role] OFF
ALTER TABLE [dbo].[Doctor]  WITH CHECK ADD  CONSTRAINT [FK_Doctor_Doctor_Category] FOREIGN KEY([CatergoryID])
REFERENCES [dbo].[Doctor_Category] ([ID])
GO
ALTER TABLE [dbo].[Doctor] CHECK CONSTRAINT [FK_Doctor_Doctor_Category]
GO
ALTER TABLE [dbo].[Doctor]  WITH CHECK ADD  CONSTRAINT [FK_Doctor_User] FOREIGN KEY([UserID])
REFERENCES [dbo].[User] ([ID])
GO
ALTER TABLE [dbo].[Doctor] CHECK CONSTRAINT [FK_Doctor_User]
GO
ALTER TABLE [dbo].[User]  WITH CHECK ADD  CONSTRAINT [FK_User_Role] FOREIGN KEY([RoleID])
REFERENCES [dbo].[User_Role] ([ID])
GO
ALTER TABLE [dbo].[User] CHECK CONSTRAINT [FK_User_Role]
GO
/****** Object:  StoredProcedure [dbo].[SP_AddNewDoctorCategory]    Script Date: 12/22/2019 1:56:03 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Ahamed Azeem
-- Create date: 19-12-2019
-- Description:	SP To Add A New Doctor Category
-- =============================================
CREATE PROCEDURE [dbo].[SP_AddNewDoctorCategory]
	-- Add the parameters for the stored procedure here
	@DoctorCategory VARCHAR(10),
	@DateAdded DateTime
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    INSERT INTO User_Role([Name],DateAdded) VALUES(@DoctorCategory, @DateAdded)
END 
GO
/****** Object:  StoredProcedure [dbo].[SP_AddNewReportType]    Script Date: 12/22/2019 1:56:03 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Ahamed Azeem
-- Create date: 19-12-2019
-- Description:	SP To Add A New User Role
-- =============================================
CREATE PROCEDURE [dbo].[SP_AddNewReportType]
	-- Add the parameters for the stored procedure here
	@ReportType VARCHAR(50),
	@DateAdded DateTime
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    INSERT INTO ReportType([Name],DateAdded) VALUES(@ReportType, @DateAdded)
END 
GO
/****** Object:  StoredProcedure [dbo].[SP_AddNewUserRole]    Script Date: 12/22/2019 1:56:03 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Ahamed Azeem
-- Create date: 19-12-2019
-- Description:	SP To Add A New User Role
-- =============================================
CREATE PROCEDURE [dbo].[SP_AddNewUserRole]
	-- Add the parameters for the stored procedure here
	@UserRole VARCHAR(10),
	@DateAdded DateTime
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    INSERT INTO User_Role([Name],DateAdded) VALUES(@UserRole, @DateAdded)
END 
GO
/****** Object:  StoredProcedure [dbo].[SP_DeleteDoctorCategory]    Script Date: 12/22/2019 1:56:03 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Ahamed Azeem
-- Create date: 19-12-2019
-- Description:	SP To Delete A Doctor Category
-- =============================================
CREATE PROCEDURE [dbo].[SP_DeleteDoctorCategory]
	-- Add the parameters for the stored procedure here
	@DoctorCategory VARCHAR(50),
	@DoctorCategoryID int
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    DELETE FROM Doctor_Category WHERE Name = @DoctorCategory AND ID = @DoctorCategoryID
END 
GO
/****** Object:  StoredProcedure [dbo].[SP_DeleteReportType]    Script Date: 12/22/2019 1:56:03 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Ahamed Azeem
-- Create date: 19-12-2019
-- Description:	SP To Delete A User Role
-- =============================================
CREATE PROCEDURE [dbo].[SP_DeleteReportType]
	-- Add the parameters for the stored procedure here
	@ReportType VARCHAR(50),
	@ReportTypeID int
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    DELETE FROM ReportType WHERE Name = @ReportType AND ID = @ReportTypeID
END 
GO
/****** Object:  StoredProcedure [dbo].[SP_DeleteUserRole]    Script Date: 12/22/2019 1:56:03 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Ahamed Azeem
-- Create date: 19-12-2019
-- Description:	SP To Delete A User Role
-- =============================================
CREATE PROCEDURE [dbo].[SP_DeleteUserRole]
	-- Add the parameters for the stored procedure here
	@UserRole VARCHAR(10),
	@RoleID int
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    DELETE FROM User_Role WHERE Name = @UserRole AND ID = @RoleID
END 
GO
/****** Object:  StoredProcedure [dbo].[SP_GetAllPatientReports]    Script Date: 12/22/2019 1:56:03 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Ahamed Azeem
-- Create date: 19-12-2019
-- Description:	SP To Get User Roles
-- =============================================
CREATE PROCEDURE [dbo].[SP_GetAllPatientReports]
	-- Add the parameters for the stored procedure here
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    SELECT rpt.[ID]
      ,p.[Name]
      ,rt.[Name]
      ,rpt.[Results]
      ,rpt.[SampleDate]
      ,rpt.[TestedDate]
      ,rpt.[Remarks]
      ,rpt.[Fee]
      ,rpt.[ReportHtml] from [Report] rpt
	  join Patient p on p.ID = rpt.[PatientID]
	  join ReportType rt on rt.ID = rpt.[ReportType]
	  ORDER BY rpt.ID desc
END	   
GO
/****** Object:  StoredProcedure [dbo].[SP_GetDoctorCategories]    Script Date: 12/22/2019 1:56:03 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Ahamed Azeem
-- Create date: 19-12-2019
-- Description:	SP To Get User Roles
-- =============================================
CREATE PROCEDURE [dbo].[SP_GetDoctorCategories]
	-- Add the parameters for the stored procedure here
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    SELECT * from Doctor_Category
END 
GO
/****** Object:  StoredProcedure [dbo].[SP_GetPatientReports]    Script Date: 12/22/2019 1:56:03 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Ahamed Azeem
-- Create date: 19-12-2019
-- Description:	SP To Get User Roles
-- =============================================
CREATE PROCEDURE [dbo].[SP_GetPatientReports]
	-- Add the parameters for the stored procedure here
	@PatientID int
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    SELECT rpt.[ID]
      ,p.[Name]
      ,rt.[Name]
      ,rpt.[Results]
      ,rpt.[SampleDate]
      ,rpt.[TestedDate]
      ,rpt.[Remarks]
      ,rpt.[Fee]
      ,rpt.[ReportHtml] from [Report] rpt
	  join Patient p on p.ID = rpt.[PatientID]
	  join ReportType rt on rt.ID = rpt.[ReportType]
	  WHERE p.ID = @PatientID
	  ORDER BY rpt.ID desc
END	   
GO
/****** Object:  StoredProcedure [dbo].[SP_GetReportTypes]    Script Date: 12/22/2019 1:56:03 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Ahamed Azeem
-- Create date: 19-12-2019
-- Description:	SP To Get User Roles
-- =============================================
CREATE PROCEDURE [dbo].[SP_GetReportTypes]
	-- Add the parameters for the stored procedure here
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    SELECT * from ReportType
END 
GO
/****** Object:  StoredProcedure [dbo].[SP_GetUserRoles]    Script Date: 12/22/2019 1:56:03 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Ahamed Azeem
-- Create date: 19-12-2019
-- Description:	SP To Get User Roles
-- =============================================
CREATE PROCEDURE [dbo].[SP_GetUserRoles]
	-- Add the parameters for the stored procedure here
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    SELECT * from User_Role
END 
GO
/****** Object:  StoredProcedure [dbo].[SP_GetUsersForUsersPage]    Script Date: 12/22/2019 1:56:03 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Ahamed Azeem
-- Create date: 19-12-2019
-- Description:	SP To Get User Roles
-- =============================================
CREATE PROCEDURE [dbo].[SP_GetUsersForUsersPage]
	-- Add the parameters for the stored procedure here
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    SELECT u.ID
      ,u.Firstname
      ,u.Lastname
      ,u.Address_line_1
      ,u.Address_line_2
      ,u.PostalCode
      ,u.Email
      ,u.Password
      ,u.Salt
      ,u.Gender
      ,u.MobileNo
      ,ur.Name AS [Role]
     ,IIF(u.[Doctor_Category] = 0, 'Not A Doctor', (SELECT dc.Name FROM Doctor_Category dc WHERE dc.ID = u.[Doctor_Category])) AS [Doctor_Category]
      ,u.RegisteredDate
      ,IIF(u.ActiveStatus = 0 , 'Inactive', 'Active') AS ActiveStatus from [User] u
	  join User_Role ur on ur.ID = u.RoleID
END 
GO
/****** Object:  StoredProcedure [dbo].[SP_IsExistingDoctorCategory]    Script Date: 12/22/2019 1:56:03 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Ahamed Azeem
-- Create date: 18-12-2019
-- Description:	SP To Check If the Doctor Category Exists
-- =============================================
CREATE PROCEDURE [dbo].[SP_IsExistingDoctorCategory]
	-- Add the parameters for the stored procedure here
	@DoctorCategory VARCHAR(10)
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    SELECT * from Doctor_Category WHERE Name = @DoctorCategory
END
GO
/****** Object:  StoredProcedure [dbo].[SP_IsExistingDoctorCategoryForUpdateAndDelete]    Script Date: 12/22/2019 1:56:03 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Ahamed Azeem
-- Create date: 19-12-2019
-- Description:	SP To Check If the Doctor Category Exists For Update And Delete
-- =============================================
CREATE PROCEDURE [dbo].[SP_IsExistingDoctorCategoryForUpdateAndDelete]
	-- Add the parameters for the stored procedure here
	@DoctorCategoryID INT
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    SELECT * from Doctor_Category WHERE ID = @DoctorCategoryID
END
GO
/****** Object:  StoredProcedure [dbo].[SP_IsExistingReportTypeForUpdateAndDelete]    Script Date: 12/22/2019 1:56:03 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Ahamed Azeem
-- Create date: 19-12-2019
-- Description:	SP To Check If the User Role Exists For Update And Delete
-- =============================================
CREATE PROCEDURE [dbo].[SP_IsExistingReportTypeForUpdateAndDelete]
	-- Add the parameters for the stored procedure here
	@ReportTypeID INT
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    SELECT * from ReportType WHERE ID = @ReportTypeID
END
GO
/****** Object:  StoredProcedure [dbo].[SP_IsExistingUserRole]    Script Date: 12/22/2019 1:56:03 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Ahamed Azeem
-- Create date: 18-12-2019
-- Description:	SP To Check If the User Role Exists
-- =============================================
CREATE PROCEDURE [dbo].[SP_IsExistingUserRole]
	-- Add the parameters for the stored procedure here
	@UserRole VARCHAR(10)
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    SELECT * from User_Role WHERE Name = @UserRole
END
GO
/****** Object:  StoredProcedure [dbo].[SP_IsExistingUserRoleForUpdateAndDelete]    Script Date: 12/22/2019 1:56:03 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Ahamed Azeem
-- Create date: 19-12-2019
-- Description:	SP To Check If the User Role Exists For Update And Delete
-- =============================================
CREATE PROCEDURE [dbo].[SP_IsExistingUserRoleForUpdateAndDelete]
	-- Add the parameters for the stored procedure here
	@UserRoleID INT
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    SELECT * from User_Role WHERE ID = @UserRoleID
END
GO
/****** Object:  StoredProcedure [dbo].[SP_UpdateDoctorCategory]    Script Date: 12/22/2019 1:56:03 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Ahamed Azeem
-- Create date: 19-12-2019
-- Description:	SP To Update A Doctor Category
-- =============================================
CREATE PROCEDURE [dbo].[SP_UpdateDoctorCategory]
	-- Add the parameters for the stored procedure here
	@DoctorCategory VARCHAR(50),
	@DoctorCategoryID int
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    UPDATE Doctor_Category SET Name = @DoctorCategory WHERE ID = @DoctorCategoryID
END 
GO
/****** Object:  StoredProcedure [dbo].[SP_UpdateReportType]    Script Date: 12/22/2019 1:56:03 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Ahamed Azeem
-- Create date: 21-12-2019
-- Description:	SP To UPDATE A Report Type
-- =============================================
CREATE PROCEDURE [dbo].[SP_UpdateReportType]
	-- Add the parameters for the stored procedure here
	@ReportType VARCHAR(50),
	@ReportTypeID int
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    UPDATE ReportType SET [Name] = @ReportType WHERE ID = @ReportTypeID
END 
GO
/****** Object:  StoredProcedure [dbo].[SP_UpdateUserActiveStatus]    Script Date: 12/22/2019 1:56:03 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Ahamed Azeem
-- Create date: 19-12-2019
-- Description:	SP To Add A New User Role
-- =============================================
CREATE PROCEDURE [dbo].[SP_UpdateUserActiveStatus]
	-- Add the parameters for the stored procedure here
	@ActiveStatus bit = 0,
	@RoleID int
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    UPDATE [User] SET ActiveStatus = @ActiveStatus WHERE ID = @RoleID
END 
GO
/****** Object:  StoredProcedure [dbo].[SP_UpdateUserRole]    Script Date: 12/22/2019 1:56:03 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Ahamed Azeem
-- Create date: 19-12-2019
-- Description:	SP To Add A New User Role
-- =============================================
CREATE PROCEDURE [dbo].[SP_UpdateUserRole]
	-- Add the parameters for the stored procedure here
	@UserRole VARCHAR(50),
	@RoleID int
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    UPDATE User_Role SET Name = @UserRole WHERE ID = @RoleID
END 
GO
USE [master]
GO
ALTER DATABASE [HospitalDB] SET  READ_WRITE 
GO
