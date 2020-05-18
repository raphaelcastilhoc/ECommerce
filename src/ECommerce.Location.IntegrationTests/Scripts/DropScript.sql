IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[State]') AND type in (N'U'))
ALTER TABLE [dbo].[State] DROP CONSTRAINT IF EXISTS [FK_State_Country]
GO
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[City]') AND type in (N'U'))
ALTER TABLE [dbo].[City] DROP CONSTRAINT IF EXISTS [FK_StateId_StateId1]
GO
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Address]') AND type in (N'U'))
ALTER TABLE [dbo].[Address] DROP CONSTRAINT IF EXISTS [FK_Address_City]
GO
/****** Object:  Table [dbo].[State]    Script Date: 12/05/2020 10:28:43 ******/
DROP TABLE IF EXISTS [dbo].[State]
GO
/****** Object:  Table [dbo].[Country]    Script Date: 12/05/2020 10:28:43 ******/
DROP TABLE IF EXISTS [dbo].[Country]
GO
/****** Object:  Table [dbo].[City]    Script Date: 12/05/2020 10:28:43 ******/
DROP TABLE IF EXISTS [dbo].[City]
GO
/****** Object:  Table [dbo].[Address]    Script Date: 12/05/2020 10:28:43 ******/
DROP TABLE IF EXISTS [dbo].[Address]
GO