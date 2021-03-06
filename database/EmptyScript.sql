USE [master]
GO
/****** Object:  Database [ControlAccesoPersonalAut]    Script Date: 24/12/2020 10:57:07 ******/
CREATE DATABASE [ControlAccesoPersonalAut]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'ControlAccesoPersonalAut', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL15.MSSQLSERVER\MSSQL\DATA\ControlAccesoPersonalAut.mdf' , SIZE = 8192KB , MAXSIZE = UNLIMITED, FILEGROWTH = 65536KB )
 LOG ON 
( NAME = N'ControlAccesoPersonalAut_log', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL15.MSSQLSERVER\MSSQL\DATA\ControlAccesoPersonalAut_log.ldf' , SIZE = 8192KB , MAXSIZE = 2048GB , FILEGROWTH = 65536KB )
 WITH CATALOG_COLLATION = DATABASE_DEFAULT
GO
ALTER DATABASE [ControlAccesoPersonalAut] SET COMPATIBILITY_LEVEL = 150
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [ControlAccesoPersonalAut].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [ControlAccesoPersonalAut] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [ControlAccesoPersonalAut] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [ControlAccesoPersonalAut] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [ControlAccesoPersonalAut] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [ControlAccesoPersonalAut] SET ARITHABORT OFF 
GO
ALTER DATABASE [ControlAccesoPersonalAut] SET AUTO_CLOSE OFF 
GO
ALTER DATABASE [ControlAccesoPersonalAut] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [ControlAccesoPersonalAut] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [ControlAccesoPersonalAut] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [ControlAccesoPersonalAut] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [ControlAccesoPersonalAut] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [ControlAccesoPersonalAut] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [ControlAccesoPersonalAut] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [ControlAccesoPersonalAut] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [ControlAccesoPersonalAut] SET  ENABLE_BROKER 
GO
ALTER DATABASE [ControlAccesoPersonalAut] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [ControlAccesoPersonalAut] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [ControlAccesoPersonalAut] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [ControlAccesoPersonalAut] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [ControlAccesoPersonalAut] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [ControlAccesoPersonalAut] SET READ_COMMITTED_SNAPSHOT ON 
GO
ALTER DATABASE [ControlAccesoPersonalAut] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [ControlAccesoPersonalAut] SET RECOVERY FULL 
GO
ALTER DATABASE [ControlAccesoPersonalAut] SET  MULTI_USER 
GO
ALTER DATABASE [ControlAccesoPersonalAut] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [ControlAccesoPersonalAut] SET DB_CHAINING OFF 
GO
ALTER DATABASE [ControlAccesoPersonalAut] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [ControlAccesoPersonalAut] SET TARGET_RECOVERY_TIME = 60 SECONDS 
GO
ALTER DATABASE [ControlAccesoPersonalAut] SET DELAYED_DURABILITY = DISABLED 
GO
ALTER DATABASE [ControlAccesoPersonalAut] SET ACCELERATED_DATABASE_RECOVERY = OFF  
GO
EXEC sys.sp_db_vardecimal_storage_format N'ControlAccesoPersonalAut', N'ON'
GO
ALTER DATABASE [ControlAccesoPersonalAut] SET QUERY_STORE = OFF
GO
USE [ControlAccesoPersonalAut]
GO
/****** Object:  Table [dbo].[__EFMigrationsHistory]    Script Date: 24/12/2020 10:57:07 ******/
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
/****** Object:  Table [dbo].[AdmoDeHorarios]    Script Date: 24/12/2020 10:57:07 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[AdmoDeHorarios](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[act] [bit] NOT NULL,
	[Empleado] [int] NOT NULL,
	[horario] [int] NOT NULL,
	[fechaInicio] [date] NOT NULL,
	[fechaAcaba] [date] NOT NULL,
 CONSTRAINT [PK_AdmoDeHorarios] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[AspNetRoleClaims]    Script Date: 24/12/2020 10:57:07 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[AspNetRoleClaims](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[RoleId] [nvarchar](450) NOT NULL,
	[ClaimType] [nvarchar](max) NULL,
	[ClaimValue] [nvarchar](max) NULL,
 CONSTRAINT [PK_AspNetRoleClaims] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[AspNetRoles]    Script Date: 24/12/2020 10:57:07 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[AspNetRoles](
	[Id] [nvarchar](450) NOT NULL,
	[Name] [nvarchar](256) NULL,
	[NormalizedName] [nvarchar](256) NULL,
	[ConcurrencyStamp] [nvarchar](max) NULL,
 CONSTRAINT [PK_AspNetRoles] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[AspNetUserClaims]    Script Date: 24/12/2020 10:57:07 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[AspNetUserClaims](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[UserId] [nvarchar](450) NOT NULL,
	[ClaimType] [nvarchar](max) NULL,
	[ClaimValue] [nvarchar](max) NULL,
 CONSTRAINT [PK_AspNetUserClaims] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[AspNetUserLogins]    Script Date: 24/12/2020 10:57:07 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[AspNetUserLogins](
	[LoginProvider] [nvarchar](128) NOT NULL,
	[ProviderKey] [nvarchar](128) NOT NULL,
	[ProviderDisplayName] [nvarchar](max) NULL,
	[UserId] [nvarchar](450) NOT NULL,
 CONSTRAINT [PK_AspNetUserLogins] PRIMARY KEY CLUSTERED 
(
	[LoginProvider] ASC,
	[ProviderKey] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[AspNetUserRoles]    Script Date: 24/12/2020 10:57:07 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[AspNetUserRoles](
	[UserId] [nvarchar](450) NOT NULL,
	[RoleId] [nvarchar](450) NOT NULL,
 CONSTRAINT [PK_AspNetUserRoles] PRIMARY KEY CLUSTERED 
(
	[UserId] ASC,
	[RoleId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[AspNetUsers]    Script Date: 24/12/2020 10:57:07 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[AspNetUsers](
	[Id] [nvarchar](450) NOT NULL,
	[UserName] [nvarchar](256) NULL,
	[NormalizedUserName] [nvarchar](256) NULL,
	[Email] [nvarchar](256) NULL,
	[NormalizedEmail] [nvarchar](256) NULL,
	[EmailConfirmed] [bit] NOT NULL,
	[PasswordHash] [nvarchar](max) NULL,
	[SecurityStamp] [nvarchar](max) NULL,
	[ConcurrencyStamp] [nvarchar](max) NULL,
	[PhoneNumber] [nvarchar](max) NULL,
	[PhoneNumberConfirmed] [bit] NOT NULL,
	[TwoFactorEnabled] [bit] NOT NULL,
	[LockoutEnd] [datetimeoffset](7) NULL,
	[LockoutEnabled] [bit] NOT NULL,
	[AccessFailedCount] [int] NOT NULL,
 CONSTRAINT [PK_AspNetUsers] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[AspNetUserTokens]    Script Date: 24/12/2020 10:57:07 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[AspNetUserTokens](
	[UserId] [nvarchar](450) NOT NULL,
	[LoginProvider] [nvarchar](128) NOT NULL,
	[Name] [nvarchar](128) NOT NULL,
	[Value] [nvarchar](max) NULL,
 CONSTRAINT [PK_AspNetUserTokens] PRIMARY KEY CLUSTERED 
(
	[UserId] ASC,
	[LoginProvider] ASC,
	[Name] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Empleado]    Script Date: 24/12/2020 10:57:07 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Empleado](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[act] [bit] NOT NULL,
	[articulo] [bit] NOT NULL,
	[cargo] [varchar](50) NOT NULL,
	[persona] [int] NOT NULL,
	[empresa] [int] NOT NULL,
	[sueldo] [float] NULL,
 CONSTRAINT [PK_Empleado] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Empresa]    Script Date: 24/12/2020 10:57:07 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Empresa](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[act] [bit] NOT NULL,
	[rut] [varchar](12) NOT NULL,
	[nombre] [varchar](50) NOT NULL,
	[description] [varchar](150) NOT NULL,
 CONSTRAINT [PK_Empresa] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Feriados]    Script Date: 24/12/2020 10:57:07 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Feriados](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[act] [bit] NOT NULL,
	[fecha] [datetime] NOT NULL,
	[description] [varchar](150) NOT NULL,
	[nombre] [varchar](50) NOT NULL,
	[anual] [bit] NOT NULL,
 CONSTRAINT [PK_Feriados] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Horarios]    Script Date: 24/12/2020 10:57:07 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Horarios](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[nombre] [varchar](50) NOT NULL,
	[act] [bit] NOT NULL,
	[LHoraI] [time](7) NULL,
	[LHoraS] [time](7) NULL,
	[LHoraBS] [time](7) NULL,
	[LHoraBI] [time](7) NULL,
	[MHoraI] [time](7) NULL,
	[MHoraS] [time](7) NULL,
	[MHoraBS] [time](7) NULL,
	[MHoraBI] [time](7) NULL,
	[IHoraI] [time](7) NULL,
	[IHoraS] [time](7) NULL,
	[IHoraBS] [time](7) NULL,
	[IHoraBI] [time](7) NULL,
	[JHoraI] [time](7) NULL,
	[JHoraS] [time](7) NULL,
	[JHoraBS] [time](7) NULL,
	[JHoraBI] [time](7) NULL,
	[VHoraI] [time](7) NULL,
	[VHoraS] [time](7) NULL,
	[VHoraBS] [time](7) NULL,
	[VHoraBI] [time](7) NULL,
	[SHoraI] [time](7) NULL,
	[SHoraS] [time](7) NULL,
	[SHoraBS] [time](7) NULL,
	[SHoraBI] [time](7) NULL,
	[DHoraI] [time](7) NULL,
	[DHoraS] [time](7) NULL,
	[DHoraBS] [time](7) NULL,
	[DHoraBI] [time](7) NULL
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Peronas]    Script Date: 24/12/2020 10:57:07 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Peronas](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[act] [bit] NOT NULL,
	[nombre] [varchar](200) NOT NULL,
	[nombre2] [varchar](200) NOT NULL,
	[apellido] [varchar](200) NOT NULL,
	[apellido2] [varchar](200) NOT NULL,
	[telefono] [varchar](200) NOT NULL,
	[correo] [varchar](200) NOT NULL,
	[salt] [varchar](200) NOT NULL,
	[rut] [varchar](200) NOT NULL,
 CONSTRAINT [PK_Peronas] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[RegistroDiario]    Script Date: 24/12/2020 10:57:07 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[RegistroDiario](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[act] [bit] NOT NULL,
	[fecha] [date] NOT NULL,
	[Empleado] [int] NOT NULL,
	[HoraI] [time](7) NOT NULL,
	[HoraS] [time](7) NULL,
	[HoraBS] [time](7) NULL,
	[HoraBI] [time](7) NULL,
PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [IX_AspNetRoleClaims_RoleId]    Script Date: 24/12/2020 10:57:07 ******/
CREATE NONCLUSTERED INDEX [IX_AspNetRoleClaims_RoleId] ON [dbo].[AspNetRoleClaims]
(
	[RoleId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [RoleNameIndex]    Script Date: 24/12/2020 10:57:07 ******/
CREATE UNIQUE NONCLUSTERED INDEX [RoleNameIndex] ON [dbo].[AspNetRoles]
(
	[NormalizedName] ASC
)
WHERE ([NormalizedName] IS NOT NULL)
WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [IX_AspNetUserClaims_UserId]    Script Date: 24/12/2020 10:57:07 ******/
CREATE NONCLUSTERED INDEX [IX_AspNetUserClaims_UserId] ON [dbo].[AspNetUserClaims]
(
	[UserId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [IX_AspNetUserLogins_UserId]    Script Date: 24/12/2020 10:57:07 ******/
CREATE NONCLUSTERED INDEX [IX_AspNetUserLogins_UserId] ON [dbo].[AspNetUserLogins]
(
	[UserId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [IX_AspNetUserRoles_RoleId]    Script Date: 24/12/2020 10:57:07 ******/
CREATE NONCLUSTERED INDEX [IX_AspNetUserRoles_RoleId] ON [dbo].[AspNetUserRoles]
(
	[RoleId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [EmailIndex]    Script Date: 24/12/2020 10:57:07 ******/
CREATE NONCLUSTERED INDEX [EmailIndex] ON [dbo].[AspNetUsers]
(
	[NormalizedEmail] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [UserNameIndex]    Script Date: 24/12/2020 10:57:07 ******/
CREATE UNIQUE NONCLUSTERED INDEX [UserNameIndex] ON [dbo].[AspNetUsers]
(
	[NormalizedUserName] ASC
)
WHERE ([NormalizedUserName] IS NOT NULL)
WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
ALTER TABLE [dbo].[AspNetRoleClaims]  WITH CHECK ADD  CONSTRAINT [FK_AspNetRoleClaims_AspNetRoles_RoleId] FOREIGN KEY([RoleId])
REFERENCES [dbo].[AspNetRoles] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[AspNetRoleClaims] CHECK CONSTRAINT [FK_AspNetRoleClaims_AspNetRoles_RoleId]
GO
ALTER TABLE [dbo].[AspNetUserClaims]  WITH CHECK ADD  CONSTRAINT [FK_AspNetUserClaims_AspNetUsers_UserId] FOREIGN KEY([UserId])
REFERENCES [dbo].[AspNetUsers] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[AspNetUserClaims] CHECK CONSTRAINT [FK_AspNetUserClaims_AspNetUsers_UserId]
GO
ALTER TABLE [dbo].[AspNetUserLogins]  WITH CHECK ADD  CONSTRAINT [FK_AspNetUserLogins_AspNetUsers_UserId] FOREIGN KEY([UserId])
REFERENCES [dbo].[AspNetUsers] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[AspNetUserLogins] CHECK CONSTRAINT [FK_AspNetUserLogins_AspNetUsers_UserId]
GO
ALTER TABLE [dbo].[AspNetUserRoles]  WITH CHECK ADD  CONSTRAINT [FK_AspNetUserRoles_AspNetRoles_RoleId] FOREIGN KEY([RoleId])
REFERENCES [dbo].[AspNetRoles] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[AspNetUserRoles] CHECK CONSTRAINT [FK_AspNetUserRoles_AspNetRoles_RoleId]
GO
ALTER TABLE [dbo].[AspNetUserRoles]  WITH CHECK ADD  CONSTRAINT [FK_AspNetUserRoles_AspNetUsers_UserId] FOREIGN KEY([UserId])
REFERENCES [dbo].[AspNetUsers] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[AspNetUserRoles] CHECK CONSTRAINT [FK_AspNetUserRoles_AspNetUsers_UserId]
GO
ALTER TABLE [dbo].[AspNetUserTokens]  WITH CHECK ADD  CONSTRAINT [FK_AspNetUserTokens_AspNetUsers_UserId] FOREIGN KEY([UserId])
REFERENCES [dbo].[AspNetUsers] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[AspNetUserTokens] CHECK CONSTRAINT [FK_AspNetUserTokens_AspNetUsers_UserId]
GO
/****** Object:  StoredProcedure [dbo].[deleteAdmoH]    Script Date: 24/12/2020 10:57:07 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO


CREATE PROCEDURE [dbo].[deleteAdmoH]
@id as int
AS
BEGIN
	UPDATE AdmoDeHorarios SET act = 0 where id= @id
END
GO
/****** Object:  StoredProcedure [dbo].[DeleteEmpleado]    Script Date: 24/12/2020 10:57:07 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[DeleteEmpleado]
@id as int
AS
BEGIN
	update Empleado set act = 0  where id = @id;
END
GO
/****** Object:  StoredProcedure [dbo].[DeleteEmpresa]    Script Date: 24/12/2020 10:57:07 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

create procedure [dbo].[DeleteEmpresa]
@id as int

as
begin
	if exists (select 1 from Empresa where id = @id )
		update Empresa set act = 0 where id = @id
end
GO
/****** Object:  StoredProcedure [dbo].[deleteFeriado]    Script Date: 24/12/2020 10:57:07 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[deleteFeriado]
@id as int
AS
BEGIN
	update Feriados set act = 0 where id = @id;
END
GO
/****** Object:  StoredProcedure [dbo].[deleteHorario]    Script Date: 24/12/2020 10:57:07 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

create procedure [dbo].[deleteHorario]
@id as int
as
begin

	update Horarios set act = 0 where id = @id;
end
GO
/****** Object:  StoredProcedure [dbo].[DeletePersona]    Script Date: 24/12/2020 10:57:07 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO


CREATE PROCEDURE [dbo].[DeletePersona]
@id as int
AS
BEGIN
	update Peronas set act=0 where id = @id;
END
GO
/****** Object:  StoredProcedure [dbo].[DeleteRegistros]    Script Date: 24/12/2020 10:57:07 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[DeleteRegistros]
@id int
AS
BEGIN
  update RegistroDiario set act = 0 where id = @id;
END
GO
/****** Object:  StoredProcedure [dbo].[getAdmoH]    Script Date: 24/12/2020 10:57:07 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO


CREATE PROCEDURE [dbo].[getAdmoH]

AS 
BEGIN
	SELECT * FROM AdmoDeHorarios WHERE act = 1;
END 
GO
/****** Object:  StoredProcedure [dbo].[getAdmoHID]    Script Date: 24/12/2020 10:57:07 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[getAdmoHID]
@id AS int
AS
BEGIN
	SELECT * FROM AdmoDeHorarios WHERE id = @id
END
GO
/****** Object:  StoredProcedure [dbo].[getAdmoHorarioIDEmpleadoYUnaFecha]    Script Date: 24/12/2020 10:57:07 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[getAdmoHorarioIDEmpleadoYUnaFecha]
@idEmpleado int,
@fecha date
AS
BEGIN
	select * from AdmoDeHorarios where empleado = @idEmpleado and @fecha >= fechaInicio and @fecha <= fechaAcaba
END
GO
/****** Object:  StoredProcedure [dbo].[getAdmoHorariosEntreFechas]    Script Date: 24/12/2020 10:57:07 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[getAdmoHorariosEntreFechas]
@fechaInicio date,
@fechaFinal date
AS
BEGIN
	select * from AdmoDeHorarios where fechaInicio >= @fechaInicio and fechaInicio <= @fechaFinal or fechaAcaba >= @fechaInicio and fechaAcaba<= @fechaFinal;
END
GO
/****** Object:  StoredProcedure [dbo].[getEmpleado]    Script Date: 24/12/2020 10:57:07 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[getEmpleado]
@id as int
AS
BEGIN
	select * from Empleado where id = @id;
END
GO
/****** Object:  StoredProcedure [dbo].[getEmpleados]    Script Date: 24/12/2020 10:57:07 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO


CREATE PROCEDURE [dbo].[getEmpleados]
AS
BEGIN
	select * from Empleado where act = 1;
END
GO
/****** Object:  StoredProcedure [dbo].[GetEmpresa]    Script Date: 24/12/2020 10:57:07 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

Create procedure [dbo].[GetEmpresa]
@id as int
AS
BEGIN
	 SELECT * from Empresa where id = @id
END
GO
/****** Object:  StoredProcedure [dbo].[GetEmpresas]    Script Date: 24/12/2020 10:57:07 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO


Create procedure [dbo].[GetEmpresas]
AS
BEGIN
	 SELECT * from Empresa where act = 1
END
GO
/****** Object:  StoredProcedure [dbo].[getFeriado]    Script Date: 24/12/2020 10:57:07 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO


CREATE PROCEDURE[dbo].[getFeriado]
@id as int
AS
BEGIN
	select * from Feriados where id = @id;
END
GO
/****** Object:  StoredProcedure [dbo].[getFeriados]    Script Date: 24/12/2020 10:57:07 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO


	CREATE PROCEDURE[dbo].[getFeriados]

AS
BEGIN
	select * from Feriados where act = 1;
END
GO
/****** Object:  StoredProcedure [dbo].[getFeriadosEntreFechas]    Script Date: 24/12/2020 10:57:07 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[getFeriadosEntreFechas]
@fechaInicio date,
@fechaFinal date
AS
BEGIN
	select * from Feriados where fecha >= @fechaInicio and fecha <= @fechaFinal;
END
GO
/****** Object:  StoredProcedure [dbo].[getHorarioId]    Script Date: 24/12/2020 10:57:07 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[getHorarioId]
@id as int
as
begin
	select * from Horarios where id = @id;
end
GO
/****** Object:  StoredProcedure [dbo].[gethorarios]    Script Date: 24/12/2020 10:57:07 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO


CREATE PROCEDURE [dbo].[gethorarios]
as
begin
	 select * FROM Horarios WHERE act = 1
	 END
GO
/****** Object:  StoredProcedure [dbo].[getNumeroDeReportesPorFecha]    Script Date: 24/12/2020 10:57:07 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Hector Contreras
-- =============================================
CREATE PROCEDURE [dbo].[getNumeroDeReportesPorFecha]
	@fecha date
AS
BEGIN
	select COUNT(*) from RegistroDiario where fecha = @fecha
END
GO
/****** Object:  StoredProcedure [dbo].[GetPersona]    Script Date: 24/12/2020 10:57:07 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO



CREATE PROCEDURE [dbo].[GetPersona]
@id as int
AS
BEGIN
	select * from Peronas where id = @id;
END
GO
/****** Object:  StoredProcedure [dbo].[GetPersonas]    Script Date: 24/12/2020 10:57:07 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[GetPersonas]

AS
BEGIN
	select * from Peronas where act = 1;
END
GO
/****** Object:  StoredProcedure [dbo].[getRegistroID]    Script Date: 24/12/2020 10:57:07 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[getRegistroID]
@id as int
AS
BEGIN
 select * from RegistroDiario where id = @id;
END
GO
/****** Object:  StoredProcedure [dbo].[getRegistros]    Script Date: 24/12/2020 10:57:07 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO




CREATE PROCEDURE [dbo].[getRegistros]
AS
BEGIN
 select * from RegistroDiario where act = 1;
END
GO
/****** Object:  StoredProcedure [dbo].[getRegistrosPorFecha]    Script Date: 24/12/2020 10:57:07 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE pROCEDURE [dbo].[getRegistrosPorFecha]
@fecha date
AS
BEGIN
		select * from RegistroDiario where fecha = @fecha;
END
GO
/****** Object:  StoredProcedure [dbo].[getRegistrosPorFechaYIdEmpleado]    Script Date: 24/12/2020 10:57:07 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE pROCEDURE [dbo].[getRegistrosPorFechaYIdEmpleado]
@fecha date,
@idEmpleado int
AS
BEGIN
		select * from RegistroDiario where fecha = @fecha and Empleado = @idEmpleado;
END
GO
/****** Object:  StoredProcedure [dbo].[InsertMarcasRegistroDiario]    Script Date: 24/12/2020 10:57:07 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[InsertMarcasRegistroDiario]
	@fecha date,
	@Empleado int,
	@hora time
AS
BEGIN

declare @id int
set @id = 0;

	if not exists(select 1 from RegistroDiario where fecha= @fecha and Empleado = @Empleado)
		insert RegistroDiario(act,fecha,Empleado,HoraI) values (1,@fecha,@Empleado,@hora);
		--set @id = SCOPE_IDENTITY();
	else
		select @id = id from RegistroDiario where fecha= @fecha and Empleado = @Empleado;

	
	if(@id > 0)
	BEGIN
		declare @hbi time
		declare @hbs time
		declare @hs time

		select @hbi = HoraBI, @hbs = HoraBS, @Hs = HoraS from RegistroDiario where id = @id;

		if(@hbs = null or @hbs = '00:00:00')
			update RegistroDiario set HoraBS = @hora where id = @id;


	END
	if(@id = 0)
		set @id = SCOPE_IDENTITY();

select * from RegistroDiario where id =@id;

END
GO
/****** Object:  StoredProcedure [dbo].[postAdminH]    Script Date: 24/12/2020 10:57:07 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE procedure [dbo].[postAdminH]
@empleado as int,
@horario as int,
@fechaInicio as date,
@fechaAcaba as date
AS	
BEGIN

	if exists (select 1 from Empleado where id = @empleado)
		if Exists (select 1 from Horarios where id = @horario)
			insert into AdmoDeHorarios (act,Empleado,horario,fechaInicio,fechaAcaba) values (1,@empleado,@horario,@fechaInicio,@fechaAcaba)
END

GO
/****** Object:  StoredProcedure [dbo].[PostEmpleado]    Script Date: 24/12/2020 10:57:07 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[PostEmpleado] 
@persona as int,
@empresa as int,
@sueldo as float,
@articulo  bit,
@cargo varchar(50)
AS
BEGIN
	if exists (select 1 from Empresa where id = @empresa) and exists(select 1 from Peronas where id = @persona)
		--if exists (select 1 from Peronas where id = @persona)
			begin
				insert into Empleado(articulo,cargo,persona,empresa,sueldo,act) values (@articulo,@cargo,@persona,@empresa,@sueldo,1)
				return 1;
			end
		else
		return -1;
	
END
GO
/****** Object:  StoredProcedure [dbo].[PostEmpresa]    Script Date: 24/12/2020 10:57:07 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		HectorContreras
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[PostEmpresa]
@rut as varchar(12),
@nombre as varchar(50),
@description as varchar(150)

AS
BEGIN
	if not exists (select 1 from Empresa where rut = @rut)
		insert into Empresa(act,rut,nombre,description) values (1,@rut,@nombre,@description) 
END
GO
/****** Object:  StoredProcedure [dbo].[postFeriado]    Script Date: 24/12/2020 10:57:07 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[postFeriado]
@fecha as datetime,
@description as varchar(150),
@nombre as varchar(50),
@anual as bit
AS
BEGIN
	if not exists (select 1 from Feriados where fecha = @fecha)
		insert into Feriados (fecha, nombre,description, anual, act) values(@fecha, @nombre,@description, @anual, 1)
END
GO
/****** Object:  StoredProcedure [dbo].[postHorario]    Script Date: 24/12/2020 10:57:07 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[postHorario]

	@nombre as varchar(50),
	@LHoraI as time, 
@LHoraS as time, 
@LHoraBS as time, 
@LHoraBI as time,

@MHoraI as time, 
@MHoraS as time, 
@MHoraBS as time, 
@MHoraBI as time,

@IHoraI as time, 
@IHoraS as time, 
@IHoraBS as time, 
@IHoraBI as time,

@JHoraI as time, 
@JHoraS as time, 
@JHoraBS as time, 
@JHoraBI as time,

@VHoraI as time, 
@VHoraS as time, 
@VHoraBS as time, 
@VHoraBI as time,

@SHoraI as time, 
@SHoraS as time, 
@SHoraBS as time, 
@SHoraBI as time,

@DHoraI as time, 
@DHoraS as time, 
@DHoraBS as time, 
@DHoraBI as time
AS
BEGIN
	if not exists (select 1 from Horarios where nombre =@nombre)
		insert into Horarios (nombre,act,LHoraI,LHoraS,LHoraBS,LHoraBI,MHoraI,MHoraS,MHoraBS,MHoraBI,IHoraI,IHoraS,IHoraBS,IHoraBI,JHoraI,JHoraS,JHoraBS,JHoraBI,VHoraI,VHoraS,VHoraBS,VHoraBI,SHoraI,SHoraS,SHoraBS,SHoraBI,DHoraI,DHoraS,DHoraBS,DHoraBI)
			values(@nombre,1,@LHoraI,@LHoraS,@LHoraBS,@LHoraBI,@MHoraI,@MHoraS,@MHoraBS,@MHoraBI,@IHoraI,@IHoraS,@IHoraBS,@IHoraBI,@JHoraI,@JHoraS,@JHoraBS,@JHoraBI,@VHoraI,@VHoraS,@VHoraBS,@VHoraBI,@SHoraI,@SHoraS,@SHoraBS,@SHoraBI,@DHoraI,@DHoraS,@DHoraBS,@DHoraBI);

END
GO
/****** Object:  StoredProcedure [dbo].[PostPersona]    Script Date: 24/12/2020 10:57:07 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[PostPersona]
	-- @id as int      ,
 @rut as varchar(200)       , 
 @nombre as varchar(200)    ,   
 @nombre2 as varchar(200)   ,    
 @apellido as varchar(200)  ,    
 @apellido2 as varchar(200) ,    
 @telefono as varchar(200) ,
 @correo as varchar(200) ,
 @salt as varchar(200) 
 --@act as bit  
AS
BEGIN
	insert into Peronas(rut,nombre ,nombre2 ,apellido,apellido2,telefono,correo,salt ,act) values(@rut,@nombre ,@nombre2 ,@apellido,@apellido2,@telefono,@correo,@salt,1);
END
GO
/****** Object:  StoredProcedure [dbo].[PostRegistro]    Script Date: 24/12/2020 10:57:07 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

-- @HoraS time,
-- @HoraBS time,
-- @HoraBI time

CREATE procedure [dbo].[PostRegistro]
@fecha as date,
@Empleado int,
@HoraI time

AS
BEGIN
 insert into RegistroDiario (act , fecha, Empleado, HoraI) values (1,@fecha, @Empleado, @HoraI)
END
GO
/****** Object:  StoredProcedure [dbo].[PutAdmoH]    Script Date: 24/12/2020 10:57:07 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[PutAdmoH]
@id as int,
@empleado as int,
@horario as int,
@fechaInicio as date,
@fechaAcaba as date
AS
BEGIN
if exists (select 1 from Empleado where id = @empleado)
		if Exists (select 1 from Horarios where id = @horario)
			Update AdmoDeHorarios set act = 1, Empleado = @empleado,horario= @horario,fechaInicio= @fechaInicio,fechaAcaba= @fechaAcaba where id = @id;
END
GO
/****** Object:  StoredProcedure [dbo].[PutEmpleado]    Script Date: 24/12/2020 10:57:07 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[PutEmpleado]
@id as int,
@persona as int,
@empresa as int,
@sueldo as float,
@articulo  bit,
@cargo varchar(50)
as
begin
	if exists (select 1 from Empresa where id = @empresa) 
		if exists(select 1 from Peronas where id = @persona) 
			if exists(select 1 from Empleado where id = @id)
				update [dbo].[Empleado] set persona = @persona, empresa= @empresa, sueldo= @sueldo, articulo = @articulo, cargo=@cargo  where id = @id;
			
end
GO
/****** Object:  StoredProcedure [dbo].[PutEmpresa]    Script Date: 24/12/2020 10:57:07 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE procedure [dbo].[PutEmpresa]
@id as int,
@rut as varchar(12),
@nombre as varchar(50),
@description as varchar(150)

AS
BEGIN
	if exists (select 1 from Empresa where id = @id and rut = @rut )
		update Empresa set nombre = @nombre, rut = @rut, description= @description where  id = @id 
END
GO
/****** Object:  StoredProcedure [dbo].[putFeriado]    Script Date: 24/12/2020 10:57:07 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[putFeriado]
@id as int,
@fecha as datetime,
@description as varchar(150),
@nombre as varchar(50),
@anual as bit
AS
BEGIN
	if not exists (select 1 from Feriados where fecha = @fecha and id != @id)
		update Feriados set fecha = @fecha, nombre = @nombre, description = @description, anual = @anual, act = 1 where id = @id;
END
GO
/****** Object:  StoredProcedure [dbo].[putHorario]    Script Date: 24/12/2020 10:57:07 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[putHorario]
@id as int,
@nombre as varchar(50),
	@LHoraI as time, 
@LHoraS as time, 
@LHoraBS as time, 
@LHoraBI as time,

@MHoraI as time, 
@MHoraS as time, 
@MHoraBS as time, 
@MHoraBI as time,

@IHoraI as time, 
@IHoraS as time, 
@IHoraBS as time, 
@IHoraBI as time,

@JHoraI as time, 
@JHoraS as time, 
@JHoraBS as time, 
@JHoraBI as time,

@VHoraI as time, 
@VHoraS as time, 
@VHoraBS as time, 
@VHoraBI as time,

@SHoraI as time, 
@SHoraS as time, 
@SHoraBS as time, 
@SHoraBI as time,

@DHoraI as time, 
@DHoraS as time, 
@DHoraBS as time, 
@DHoraBI as time


AS 
BEGIN 
 update Horarios set nombre=@nombre, act=1, LHoraI=@LHoraI,LHoraS=@LHoraS,LHoraBS=@LHoraBS,LHoraBI=@LHoraBI,MHoraI=@MHoraI,MHoraS=@MHoraS,MHoraBS=@MHoraBS,MHoraBI=@MHoraBI,IHoraI=@IHoraI,IHoraS=@IHoraS,IHoraBS=@IHoraBS,IHoraBI=@IHoraBI,JHoraI=@JHoraI,JHoraS=@JHoraS,JHoraBS=@JHoraBS,JHoraBI=@JHoraBI,VHoraI=@VHoraI,VHoraS=@VHoraS,VHoraBS=@VHoraBS,VHoraBI=@VHoraBI,SHoraI=@SHoraI,SHoraS=@SHoraS,SHoraBS=@SHoraBS,SHoraBI=@SHoraBI,DHoraI=@DHoraI,DHoraS=@DHoraS,DHoraBS=@DHoraBS,DHoraBI=@DHoraBI where id = @id
END
GO
/****** Object:  StoredProcedure [dbo].[PutPersona]    Script Date: 24/12/2020 10:57:07 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO


CREATE PROCEDURE [dbo].[PutPersona]
@id as int,
 @rut as varchar(200)       , 
 @nombre as varchar(200)    ,   
 @nombre2 as varchar(200)   ,    
 @apellido as varchar(200)  ,    
 @apellido2 as varchar(200) ,    
 @telefono as varchar(200) ,
 @correo as varchar(200) ,
 @salt as varchar(200) 
AS
BEGIN
	update Peronas set 
rut = @rut,nombre = @nombre ,nombre2 = @nombre2 ,apellido = @apellido,apellido2 = @apellido2,telefono = @telefono,correo = @correo,salt = @salt, act = 1  where id = @id;
END
GO
/****** Object:  StoredProcedure [dbo].[putRegistros]    Script Date: 24/12/2020 10:57:07 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[putRegistros]
@id as int,
@HoraS time,
@HoraBS time,
@HoraBI time

AS
BEGIN

	if(@HoraS != null)
		update RegistroDiario set HoraS = @HoraS where id = @id;
	if(@HoraBI != null)
		update RegistroDiario set HoraBI = @HoraBI where id = @id;
	if(@HoraBS != null)
	begin
		declare @band time;
		SELECT @band = HoraBI FROM [BDemployeesCA].[dbo].RegistroDiario where id = @id;

		if(@band != null)
			update RegistroDiario set HoraBS = @HoraBS where id = @id;
	end
END

GO
/****** Object:  StoredProcedure [dbo].[putRegistrosBI]    Script Date: 24/12/2020 10:57:07 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[putRegistrosBI]
@id as int,
--@HoraS time
--@HoraBS time
@HoraBI time
AS 
BEGIN

	update RegistroDiario set HoraBi = @HoraBI where id = @id;
END
GO
/****** Object:  StoredProcedure [dbo].[putRegistrosBS]    Script Date: 24/12/2020 10:57:07 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[putRegistrosBS]
@id as int,
--@HoraS time
@HoraBS time
--@HoraBI time
AS 
BEGIN
	declare @band time;
		SELECT @band = HoraBI FROM [BDemployeesCA].[dbo].RegistroDiario where id = @id;

		if(@band != null)
			update RegistroDiario set HoraBS = @HoraBS where id = @id;
END
GO
/****** Object:  StoredProcedure [dbo].[putRegistrosHS]    Script Date: 24/12/2020 10:57:07 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[putRegistrosHS]
@id as int,
@HoraS time
--@HoraBS time,
--@HoraBI time
AS 
BEGIN
	update RegistroDiario set HoraS = @HoraS where id = @id;
END
GO
/****** Object:  StoredProcedure [dbo].[registrosEntreFechas]    Script Date: 24/12/2020 10:57:07 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[registrosEntreFechas]
	@fechaInicio date,
	@fechaFinal date
	--@admoHorarios int
AS
BEGIN
	--if(@admoHorarios > 0)
		--select * from RegistroDiario where fecha >= @fechaInicio and fecha <= @fechaFinal AND AdmoHorarios = @admoHorarios; 
	--else
		select * from RegistroDiario where fecha >= @fechaInicio and fecha <= @fechaFinal 
END
GO
/****** Object:  StoredProcedure [dbo].[registrosEntreFechasP]    Script Date: 24/12/2020 10:57:07 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
Create PROCEDURE [dbo].[registrosEntreFechasP]
	@fechaInicio date,
	@fechaFinal date,
	@idEmpleado int
	--@admoHorarios int
AS
BEGIN
		select * from RegistroDiario where fecha >= @fechaInicio and fecha <= @fechaFinal and Empleado = @idEmpleado
END
GO
USE [master]
GO
ALTER DATABASE [ControlAccesoPersonalAut] SET  READ_WRITE 
GO
