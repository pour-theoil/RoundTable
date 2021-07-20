USE [master]
GO

IF db_id('RoundTable') IS NOT NULL
BEGIN
  ALTER DATABASE [RoundTable] SET SINGLE_USER WITH ROLLBACK IMMEDIATE;
  DROP DATABASE [RoundTable]
END
GO

CREATE DATABASE [RoundTable]
GO

USE [RoundTable]
GO


CREATE TABLE [story] (
  [id] int PRIMARY KEY NOT NULL,
  [slug] nvarchar(255) NOT NULL,
  [typeId] int NOT NULL,
  [nationalId] int,
  [categoryId] int NOT NULL,
  [summary] nvarchar(255),
  [statusId] int NOT NULL,
  [reporterId] int NOT NULL,
  [storyUrl] nvarchar(255) NOT NULL,
  [laststatusupdate] datetime
)
GO

CREATE TABLE [storySource] (
  [id] int PRIMARY KEY NOT NULL,
  [storyId] int NOT NULL,
  [sourceId] int NOT NULL
)
GO

CREATE TABLE [source] (
  [id] int PRIMARY KEY NOT NULL,
  [name] nvarchar(255) NOT NULL,
  [phone] nvarchar(255),
  [email] nvarchar(255),
  [jobtitle] nvarchar(255),
  [organization] nvarchar(255),
  [reporterId] int NOT NULL
)
GO

CREATE TABLE [sourceCatagory] (
  [id] int PRIMARY KEY NOT NULL,
  [sourceId] int NOT NULL,
  [categoryId] int NOT NULL
)
GO

CREATE TABLE [reporter] (
  [id] int PRIMARY KEY NOT NULL,
  [name] nvarchar(255) NOT NULL,
  [organization] nvarchar(255) NOT NULL,
  [email] nvarchar(255) NOT NULL,
  [phone] int NOT NULL,
  [firebaseId] int NOT NULL
)
GO

CREATE TABLE [type] (
  [id] int PRIMARY KEY NOT NULL,
  [name] nvarchar(255) NOT NULL
)
GO

CREATE TABLE [status] (
  [id] int PRIMARY KEY NOT NULL,
  [name] nvarchar(255) NOT NULL
)
GO

CREATE TABLE [category] (
  [id] int PRIMARY KEY NOT NULL,
  [name] nvarchar(255) NOT NULL
)
GO

CREATE TABLE [nationalOutlet] (
  [id] int PRIMARY KEY NOT NULL,
  [name] nvarchar(255)
)
GO

