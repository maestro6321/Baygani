-- SQL Manager for SQL Server 5.0.1.51843
-- ---------------------------------------
-- Host      : 192.168.0.3
-- Database  : BayganiDB
-- Version   : Microsoft SQL Server 2014 12.0.2000.8


--
-- Definition for table sysdiagrams : 
--

CREATE TABLE dbo.sysdiagrams (
  name sysname COLLATE Persian_100_CI_AS NOT NULL,
  principal_id int NOT NULL,
  diagram_id int IDENTITY(1, 1) NOT NULL,
  version int NULL,
  definition varbinary(max) NULL,
  PRIMARY KEY CLUSTERED (diagram_id)
    WITH (
      PAD_INDEX = OFF, IGNORE_DUP_KEY = OFF, STATISTICS_NORECOMPUTE = OFF,
      ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON)
)
ON [PRIMARY]
GO

--
-- Definition for table tblAttachment : 
--

CREATE TABLE dbo.tblAttachment (
  id int IDENTITY(1, 1) NOT NULL,
  Letter_ID int NULL,
  Attachment nvarchar(max) COLLATE Persian_100_CI_AS NULL,
  PRIMARY KEY CLUSTERED (id)
    WITH (
      PAD_INDEX = OFF, IGNORE_DUP_KEY = OFF, STATISTICS_NORECOMPUTE = OFF,
      ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON)
)
ON [PRIMARY]
GO

--
-- Definition for table tblBayegani : 
--

CREATE TABLE dbo.tblBayegani (
  id int NOT NULL,
  Shomare_name nvarchar(50) COLLATE Persian_100_CI_AS NOT NULL,
  tarikh nvarchar(50) COLLATE Persian_100_CI_AS NULL,
  title nvarchar(500) COLLATE Persian_100_CI_AS NULL,
  id_ferestande int NULL,
  id_vahed_ferestande int NULL,
  id_girande int NULL,
  id_vahed_girande int NULL,
  type int NULL,
  category int NULL,
  olaviat int NULL,
  CONSTRAINT PK_tblBayegani PRIMARY KEY CLUSTERED (Shomare_name)
    WITH (
      PAD_INDEX = OFF, IGNORE_DUP_KEY = OFF, STATISTICS_NORECOMPUTE = OFF,
      ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON)
)
ON [PRIMARY]
GO

--
-- Definition for table tblCategory : 
--

CREATE TABLE dbo.tblCategory (
  id int IDENTITY(1, 1) NOT NULL,
  CategoryName nvarchar(50) COLLATE Persian_100_CI_AS NULL,
  PRIMARY KEY CLUSTERED (id)
    WITH (
      PAD_INDEX = OFF, IGNORE_DUP_KEY = OFF, STATISTICS_NORECOMPUTE = OFF,
      ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON)
)
ON [PRIMARY]
GO

--
-- Definition for table tblEdarat : 
--

CREATE TABLE dbo.tblEdarat (
  id int IDENTITY(1, 1) NOT NULL,
  name_edare nvarchar(50) COLLATE Persian_100_CI_AS NULL,
  PRIMARY KEY CLUSTERED (id)
    WITH (
      PAD_INDEX = OFF, IGNORE_DUP_KEY = OFF, STATISTICS_NORECOMPUTE = OFF,
      ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON)
)
ON [PRIMARY]
GO

--
-- Definition for table tblOlaviat : 
--

CREATE TABLE dbo.tblOlaviat (
  id int IDENTITY(1, 1) NOT NULL,
  Olaviat_name nvarchar(50) COLLATE Persian_100_CI_AS NULL,
  PRIMARY KEY CLUSTERED (id)
    WITH (
      PAD_INDEX = OFF, IGNORE_DUP_KEY = OFF, STATISTICS_NORECOMPUTE = OFF,
      ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON)
)
ON [PRIMARY]
GO

--
-- Definition for table tblOption : 
--

CREATE TABLE dbo.tblOption (
  id int IDENTITY(1, 1) NOT NULL,
  DefaultAttachFolder nvarchar(max) COLLATE Persian_100_CI_AS NULL
)
ON [PRIMARY]
GO

--
-- Definition for table tblType : 
--

CREATE TABLE dbo.tblType (
  id int IDENTITY(1, 1) NOT NULL,
  name nvarchar(50) COLLATE Persian_100_CI_AS NULL,
  PRIMARY KEY CLUSTERED (id)
    WITH (
      PAD_INDEX = OFF, IGNORE_DUP_KEY = OFF, STATISTICS_NORECOMPUTE = OFF,
      ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON)
)
ON [PRIMARY]
GO

--
-- Definition for table tblVahed : 
--

CREATE TABLE dbo.tblVahed (
  id int IDENTITY(1, 1) NOT NULL,
  name_vahed nvarchar(50) COLLATE Persian_100_CI_AS NULL,
  PRIMARY KEY CLUSTERED (id)
    WITH (
      PAD_INDEX = OFF, IGNORE_DUP_KEY = OFF, STATISTICS_NORECOMPUTE = OFF,
      ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON)
)
ON [PRIMARY]
GO

--
-- Definition for table Users : 
--

CREATE TABLE dbo.Users (
  Username nvarchar(50) COLLATE Persian_100_CI_AS NOT NULL,
  Password nvarchar(50) COLLATE Persian_100_CI_AS NULL,
  CONSTRAINT PK_LoginUsers PRIMARY KEY CLUSTERED (Username)
    WITH (
      PAD_INDEX = OFF, IGNORE_DUP_KEY = OFF, STATISTICS_NORECOMPUTE = OFF,
      ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON)
)
ON [PRIMARY]
GO

--
-- Definition for user-defined function fn_diagramobjects : 
--
GO
SET ANSI_NULLS ON
SET QUOTED_IDENTIFIER ON
GO

	CREATE FUNCTION dbo.fn_diagramobjects() 
	RETURNS int
	WITH EXECUTE AS N'dbo'
	AS
	BEGIN
		declare @id_upgraddiagrams		int
		declare @id_sysdiagrams			int
		declare @id_helpdiagrams		int
		declare @id_helpdiagramdefinition	int
		declare @id_creatediagram	int
		declare @id_renamediagram	int
		declare @id_alterdiagram 	int 
		declare @id_dropdiagram		int
		declare @InstalledObjects	int

		select @InstalledObjects = 0

		select 	@id_upgraddiagrams = object_id(N'dbo.sp_upgraddiagrams'),
			@id_sysdiagrams = object_id(N'dbo.sysdiagrams'),
			@id_helpdiagrams = object_id(N'dbo.sp_helpdiagrams'),
			@id_helpdiagramdefinition = object_id(N'dbo.sp_helpdiagramdefinition'),
			@id_creatediagram = object_id(N'dbo.sp_creatediagram'),
			@id_renamediagram = object_id(N'dbo.sp_renamediagram'),
			@id_alterdiagram = object_id(N'dbo.sp_alterdiagram'), 
			@id_dropdiagram = object_id(N'dbo.sp_dropdiagram')

		if @id_upgraddiagrams is not null
			select @InstalledObjects = @InstalledObjects + 1
		if @id_sysdiagrams is not null
			select @InstalledObjects = @InstalledObjects + 2
		if @id_helpdiagrams is not null
			select @InstalledObjects = @InstalledObjects + 4
		if @id_helpdiagramdefinition is not null
			select @InstalledObjects = @InstalledObjects + 8
		if @id_creatediagram is not null
			select @InstalledObjects = @InstalledObjects + 16
		if @id_renamediagram is not null
			select @InstalledObjects = @InstalledObjects + 32
		if @id_alterdiagram  is not null
			select @InstalledObjects = @InstalledObjects + 64
		if @id_dropdiagram is not null
			select @InstalledObjects = @InstalledObjects + 128
		
		return @InstalledObjects 
	END
GO

--
-- Definition for stored procedure sp_tblAttachment_delete : 
--
GO
SET ANSI_NULLS ON
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[sp_tblAttachment_delete]
(
	@Letter_ID int,
	@Attachment nvarchar(max)
)
AS
BEGIN
	delete from [dbo].[tblAttachment] where [Letter_ID]=@Letter_ID and [Attachment]=@Attachment
END
GO

--
-- Definition for stored procedure sp_tblAttachment_insert : 
--
GO
SET ANSI_NULLS ON
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[sp_tblAttachment_insert]
(
	@Letter_ID int,
	@Attachment nvarchar(max)
)
AS
BEGIN
	INSERT INTO [dbo].[tblAttachment] ([Letter_ID], [Attachment]) VALUES (@Letter_ID, @Attachment);
END
GO

--
-- Definition for stored procedure sp_tblBayegani_insert : 
--
GO
SET ANSI_NULLS ON
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[sp_tblBayegani_insert](
	@Shomare_name nvarchar(50),
	@tarikh nvarchar(50),
	@title nvarchar(50),
	@id_ferestande int,
	@id_vahed_ferestande int,
	@id_girande int,
	@id_vahed_girande int,
	@type int,
	@category int,
	@olaviat int)
AS
declare @rows int
BEGIN
	select @rows=COUNT(*) from tblBayegani
	INSERT INTO [dbo].[tblBayegani] ([id],[Shomare_name], [tarikh], [title], [id_ferestande], [id_vahed_ferestande], [id_girande], [id_vahed_girande], [type], [category], [olaviat]) VALUES (@rows+1,@Shomare_name, @tarikh, @title, @id_ferestande, @id_vahed_ferestande, @id_girande, @id_vahed_girande, @type, @category, @olaviat);
END
GO

--
-- Definition for stored procedure sp_tblBayegani_select_ByID : 
--
GO
SET ANSI_NULLS ON
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[sp_tblBayegani_select_ByID]
@id int
AS
BEGIN
	SELECT [Shomare_name]
	,[tarikh]
	,[title]
	,[id_ferestande]
	,[id_vahed_ferestande]
	,[id_girande]
	,[id_vahed_girande]
	,[type]
	,[category]
	,[olaviat]
	FROM [dbo].[tblBayegani] where [id]=@id;
END
GO

--
-- Definition for stored procedure sp_tblBayegani_update : 
--
GO
SET ANSI_NULLS ON
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[sp_tblBayegani_update](
	@id int,
	@Shomare_name nvarchar(50),
	@tarikh nvarchar(50),
	@title nvarchar(50),
	@id_ferestande int,
	@id_vahed_ferestande int,
	@id_girande int,
	@id_vahed_girande int,
	@type int,
	@category int,
	@olaviat int)
AS
BEGIN
	UPDATE [dbo].[tblBayegani]
   SET [Shomare_name] = @Shomare_name
      ,[tarikh] = @tarikh
      ,[title] = @title
      ,[id_ferestande] = @id_ferestande
      ,[id_vahed_ferestande] = @id_vahed_ferestande
      ,[id_girande] = @id_girande
      ,[id_vahed_girande] = @id_vahed_girande
      ,[type] = @type
      ,[category] = @category
      ,[olaviat] = @olaviat
 WHERE [id]=@id
END
GO

--
-- Definition for stored procedure sp_tblEdarat_insert : 
--
GO
SET ANSI_NULLS ON
SET QUOTED_IDENTIFIER ON
GO

create procedure dbo.sp_tblEdarat_insert
@name_edare nvarchar(50)
as
begin
INSERT INTO [dbo].[tblEdarat]
           ([name_edare])
     VALUES
           (@name_edare)
end
GO

--
-- Definition for stored procedure sp_tblEdarat_update : 
--
GO
SET ANSI_NULLS ON
SET QUOTED_IDENTIFIER ON
GO

create procedure dbo.sp_tblEdarat_update
(
	@name_edare nvarchar(50),
	@id int
)
as
begin
UPDATE [dbo].[tblEdarat]
   SET [name_edare] = @name_edare
 WHERE [id]=@id
end
GO

--
-- Definition for stored procedure sp_tblOption_update_defaultAttachFolder : 
--
GO
SET ANSI_NULLS ON
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[sp_tblOption_update_defaultAttachFolder] 
@id int,
@patch nvarchar(max)
as
begin
UPDATE [dbo].[tblOption]
	SET [DefaultAttachFolder] = @patch
	WHERE [id]=@id
end
GO

--
-- Definition for stored procedure sp_tblVahed_Insert : 
--
GO
SET ANSI_NULLS ON
SET QUOTED_IDENTIFIER ON
GO

create procedure dbo.sp_tblVahed_Insert
@name_vahed nvarchar(50)
as
begin
INSERT INTO [dbo].[tblVahed]
           ([name_vahed])
     VALUES
           (@name_vahed)
end
GO

--
-- Definition for stored procedure sp_tblVahed_update : 
--
GO
SET ANSI_NULLS ON
SET QUOTED_IDENTIFIER ON
GO

create procedure dbo.sp_tblVahed_update
(
	@name_vahed nvarchar(50),
	@id int
)
as
begin
UPDATE [dbo].[tblVahed]
   SET [name_vahed] = @name_vahed
 WHERE [id]=@id
end
GO

--
-- Definition for stored procedure sp_Users_Insert : 
--
GO
SET ANSI_NULLS ON
SET QUOTED_IDENTIFIER ON
GO

create procedure dbo.sp_Users_Insert
	@UserName NVARCHAR(50),
	@Password NVARCHAR(50)
as
begin
INSERT INTO [dbo].[Users]
           ([Username]
           ,[Password])
     VALUES
           (@UserName,
            @Password)
end
GO

--
-- Definition for stored procedure sp_Users_Update : 
--
GO
SET ANSI_NULLS ON
SET QUOTED_IDENTIFIER ON
GO

CREATE procedure [dbo].[sp_Users_Update]
	@UserName NVARCHAR(50),
	@Password NVARCHAR(50)
as
begin
UPDATE [dbo].[Users]
   SET  [Password] = @Password
 WHERE [Username] = @UserName
end
GO

--
-- Definition for stored procedure sp_Users_VerifyLoginDetails : 
--
GO
SET ANSI_NULLS ON
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[sp_Users_VerifyLoginDetails]
(
	@UserName NVARCHAR(50),
	@Password NVARCHAR(50)
)
AS
	BEGIN
		select '#' from [dbo].[Users] where
		[Username] = @UserName
		and
		[Password] = @Password
	END
GO

--
-- Definition for stored procedure ups_SelectAllLetterByDate : 
--
GO
SET ANSI_NULLS ON
SET QUOTED_IDENTIFIER ON
GO

create PROCEDURE [dbo].[ups_SelectAllLetterByDate] 
@date_From nvarchar(50),
@date_To nvarchar(50)
as
begin
	SELECT p.[id]
      ,p.[Shomare_name] as "شماره نامه"
      ,p.[tarikh] as "تاریخ نامه"
      ,p.[title] as "عنوان نامه"
      ,l1.[name_edare] as "فرستنده نامه"
      ,l2.[name_vahed] as "واحد فرستنده نامه"
      ,pl1.[name_edare] as "گیرنده نامه"
      ,pl2.[name_vahed] as "واحد گیرنده نامه"
      ,ps.name as "نوع نامه"
      /**,l3.CategoryName as "حساسیت نامه"**/
      /**,l4.[Olaviat_name] as "اولویت نامه"**/
  FROM [dbo].[tblBayegani] p 
Inner join [dbo].[tblEdarat] l1 On p.id_ferestande = l1.id
Inner join [dbo].[tblVahed] l2 On p.id_vahed_ferestande = l2.id
Inner join [dbo].[tblEdarat] pl1 On p.id_girande = pl1.id
Inner join [dbo].[tblVahed] pl2 On p.id_vahed_girande = pl2.id
Inner join [dbo].tblType ps On p.type = ps.id
Inner join [dbo].tblCategory l3 On p.category = l3.id
inner join [dbo].[tblOlaviat] l4 on p.olaviat=l4.id
where p.tarikh >= @date_From and p.tarikh <=@date_To
end
GO

--
-- Definition for stored procedure ups_SelectAllLetterByShomare_Name : 
--
GO
SET ANSI_NULLS ON
SET QUOTED_IDENTIFIER ON
GO

create PROCEDURE [dbo].[ups_SelectAllLetterByShomare_Name] 
@Shomare_name nvarchar(50)
as
begin
	SELECT p.[id]
      ,p.[Shomare_name] as "شماره نامه"
      ,p.[tarikh] as "تاریخ نامه"
      ,p.[title] as "عنوان نامه"
      ,l1.[name_edare] as "فرستنده نامه"
      ,l2.[name_vahed] as "واحد فرستنده نامه"
      ,pl1.[name_edare] as "گیرنده نامه"
      ,pl2.[name_vahed] as "واحد گیرنده نامه"
      ,ps.name as "نوع نامه"
      /**,l3.CategoryName as "حساسیت نامه"**/
      /**,l4.[Olaviat_name] as "اولویت نامه"**/
  FROM [dbo].[tblBayegani] p 
Inner join [dbo].[tblEdarat] l1 On p.id_ferestande = l1.id
Inner join [dbo].[tblVahed] l2 On p.id_vahed_ferestande = l2.id
Inner join [dbo].[tblEdarat] pl1 On p.id_girande = pl1.id
Inner join [dbo].[tblVahed] pl2 On p.id_vahed_girande = pl2.id
Inner join [dbo].tblType ps On p.type = ps.id
Inner join [dbo].tblCategory l3 On p.category = l3.id
inner join [dbo].[tblOlaviat] l4 on p.olaviat=l4.id
where p.Shomare_name like '%'+@Shomare_name+'%'
end
GO

--
-- Definition for stored procedure ups_SelectAllLetterByTitle_Name : 
--
GO
SET ANSI_NULLS ON
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[ups_SelectAllLetterByTitle_Name] 
@title nvarchar(50)
as
begin
	SELECT p.[id]
      ,p.[Shomare_name] as "شماره نامه"
      ,p.[tarikh] as "تاریخ نامه"
      ,p.[title] as "عنوان نامه"
      ,l1.[name_edare] as "فرستنده نامه"
      ,l2.[name_vahed] as "واحد فرستنده نامه"
      ,pl1.[name_edare] as "گیرنده نامه"
      ,pl2.[name_vahed] as "واحد گیرنده نامه"
      ,ps.name as "نوع نامه"
      /**,l3.CategoryName as "حساسیت نامه"**/
      /**,l4.[Olaviat_name] as "اولویت نامه"**/
  FROM [dbo].[tblBayegani] p 
Inner join [dbo].[tblEdarat] l1 On p.id_ferestande = l1.id
Inner join [dbo].[tblVahed] l2 On p.id_vahed_ferestande = l2.id
Inner join [dbo].[tblEdarat] pl1 On p.id_girande = pl1.id
Inner join [dbo].[tblVahed] pl2 On p.id_vahed_girande = pl2.id
Inner join [dbo].tblType ps On p.type = ps.id
Inner join [dbo].tblCategory l3 On p.category = l3.id
inner join [dbo].[tblOlaviat] l4 on p.olaviat=l4.id
where p.title like '%'+@title+'%'
end
GO

--
-- Definition for stored procedure ups_SelectAllLetterByType : 
--
GO
SET ANSI_NULLS ON
SET QUOTED_IDENTIFIER ON
GO

create PROCEDURE [dbo].[ups_SelectAllLetterByType] 
@id_type int
as
begin
	SELECT p.[id]
      ,p.[Shomare_name] as "شماره نامه"
      ,p.[tarikh] as "تاریخ نامه"
      ,p.[title] as "عنوان نامه"
      ,l1.[name_edare] as "فرستنده نامه"
      ,l2.[name_vahed] as "واحد فرستنده نامه"
      ,pl1.[name_edare] as "گیرنده نامه"
      ,pl2.[name_vahed] as "واحد گیرنده نامه"
      ,ps.name as "نوع نامه"
      /**,l3.CategoryName as "حساسیت نامه"**/
      /**,l4.[Olaviat_name] as "اولویت نامه"**/
  FROM [dbo].[tblBayegani] p 
Inner join [dbo].[tblEdarat] l1 On p.id_ferestande = l1.id
Inner join [dbo].[tblVahed] l2 On p.id_vahed_ferestande = l2.id
Inner join [dbo].[tblEdarat] pl1 On p.id_girande = pl1.id
Inner join [dbo].[tblVahed] pl2 On p.id_vahed_girande = pl2.id
Inner join [dbo].tblType ps On p.type = ps.id
Inner join [dbo].tblCategory l3 On p.category = l3.id
inner join [dbo].[tblOlaviat] l4 on p.olaviat=l4.id
where p.type = @id_type
end
GO

--
-- Definition for stored procedure ups_SelectAllLetterForDataGridView : 
--
GO
SET ANSI_NULLS ON
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[ups_SelectAllLetterForDataGridView] 
as
begin
	SELECT p.[id]
      ,p.[Shomare_name] as "شماره نامه"
      ,p.[tarikh] as "تاریخ نامه"
      ,p.[title] as "عنوان نامه"
      ,l1.[name_edare] as "فرستنده نامه"
      ,l2.[name_vahed] as "واحد فرستنده نامه"
      ,pl1.[name_edare] as "گیرنده نامه"
      ,pl2.[name_vahed] as "واحد گیرنده نامه"
      ,ps.name as "نوع نامه"
      /**,l3.CategoryName as "حساسیت نامه"**/
      /**,l4.[Olaviat_name] as "اولویت نامه"**/
  FROM [dbo].[tblBayegani] p
Inner join [dbo].[tblEdarat] l1 On p.id_ferestande = l1.id
Inner join [dbo].[tblVahed] l2 On p.id_vahed_ferestande = l2.id
Inner join [dbo].[tblEdarat] pl1 On p.id_girande = pl1.id
Inner join [dbo].[tblVahed] pl2 On p.id_vahed_girande = pl2.id
Inner join [dbo].tblType ps On p.type = ps.id
Inner join [dbo].tblCategory l3 On p.category = l3.id
inner join [dbo].[tblOlaviat] l4 on p.olaviat=l4.id
end
GO

--
-- Data for table dbo.tblBayegani  (LIMIT 0,500)
--

INSERT INTO dbo.tblBayegani (id, Shomare_name, tarikh, title, id_ferestande, id_vahed_ferestande, id_girande, id_vahed_girande, type, category, olaviat)
VALUES 
  (1, N'1208-PA-OL', N'1399/12/05', N'asdasdsa', 1, 1, 3, 1, 2, 1, 1)
GO

INSERT INTO dbo.tblBayegani (id, Shomare_name, tarikh, title, id_ferestande, id_vahed_ferestande, id_girande, id_vahed_girande, type, category, olaviat)
VALUES 
  (2, N'158556', N'1399/05/12', N'test 1', 1, 1, 3, 6, 1, 1, 1)
GO

INSERT INTO dbo.tblBayegani (id, Shomare_name, tarikh, title, id_ferestande, id_vahed_ferestande, id_girande, id_vahed_girande, type, category, olaviat)
VALUES 
  (3, N'5852223', N'1399/08/20', N'fssssqqwsd', 3, 1, 1, 1, 1, 3, 1)
GO

INSERT INTO dbo.tblBayegani (id, Shomare_name, tarikh, title, id_ferestande, id_vahed_ferestande, id_girande, id_vahed_girande, type, category, olaviat)
VALUES 
  (4, N'851223', N'1399/11/20', N'asdasdasd', 1, 1, 1, 1, 1, 1, 1)
GO

INSERT INTO dbo.tblBayegani (id, Shomare_name, tarikh, title, id_ferestande, id_vahed_ferestande, id_girande, id_vahed_girande, type, category, olaviat)
VALUES 
  (5, N'85-PA-OL-A122', N'1399/12/10', N'ddtttqwe wq', 3, 6, 1, 3, 2, 2, 1)
GO

--
-- Data for table dbo.tblCategory  (LIMIT 0,500)
--

SET IDENTITY_INSERT dbo.tblCategory ON
GO

INSERT INTO dbo.tblCategory (id, CategoryName)
VALUES 
  (1, N'داخلی')
GO

INSERT INTO dbo.tblCategory (id, CategoryName)
VALUES 
  (2, N'عادی')
GO

INSERT INTO dbo.tblCategory (id, CategoryName)
VALUES 
  (3, N'محرمانه')
GO

INSERT INTO dbo.tblCategory (id, CategoryName)
VALUES 
  (5, N'خیلی محرمانه')
GO

SET IDENTITY_INSERT dbo.tblCategory OFF
GO

--
-- Data for table dbo.tblEdarat  (LIMIT 0,500)
--

SET IDENTITY_INSERT dbo.tblEdarat ON
GO

INSERT INTO dbo.tblEdarat (id, name_edare)
VALUES 
  (1, N'هنگام')
GO

INSERT INTO dbo.tblEdarat (id, name_edare)
VALUES 
  (3, N'پتروکیان')
GO

INSERT INTO dbo.tblEdarat (id, name_edare)
VALUES 
  (5, N'پیدک')
GO

SET IDENTITY_INSERT dbo.tblEdarat OFF
GO

--
-- Data for table dbo.tblOlaviat  (LIMIT 0,500)
--

SET IDENTITY_INSERT dbo.tblOlaviat ON
GO

INSERT INTO dbo.tblOlaviat (id, Olaviat_name)
VALUES 
  (1, N'عادی')
GO

INSERT INTO dbo.tblOlaviat (id, Olaviat_name)
VALUES 
  (2, N'فوری')
GO

INSERT INTO dbo.tblOlaviat (id, Olaviat_name)
VALUES 
  (3, N'خیلی فوری')
GO

SET IDENTITY_INSERT dbo.tblOlaviat OFF
GO

--
-- Data for table dbo.tblOption  (LIMIT 0,500)
--

SET IDENTITY_INSERT dbo.tblOption ON
GO

INSERT INTO dbo.tblOption (id, DefaultAttachFolder)
VALUES 
  (1, N'C:\Users\Ali\Desktop\ALI\test for baygani')
GO

SET IDENTITY_INSERT dbo.tblOption OFF
GO

--
-- Data for table dbo.tblType  (LIMIT 0,500)
--

SET IDENTITY_INSERT dbo.tblType ON
GO

INSERT INTO dbo.tblType (id, name)
VALUES 
  (1, N'صادره')
GO

INSERT INTO dbo.tblType (id, name)
VALUES 
  (2, N'وارده')
GO

SET IDENTITY_INSERT dbo.tblType OFF
GO

--
-- Data for table dbo.tblVahed  (LIMIT 0,500)
--

SET IDENTITY_INSERT dbo.tblVahed ON
GO

INSERT INTO dbo.tblVahed (id, name_vahed)
VALUES 
  (1, N'دبیرخانه')
GO

INSERT INTO dbo.tblVahed (id, name_vahed)
VALUES 
  (2, N'معاونت مالی')
GO

INSERT INTO dbo.tblVahed (id, name_vahed)
VALUES 
  (3, N'مدیریت')
GO

INSERT INTO dbo.tblVahed (id, name_vahed)
VALUES 
  (4, N'معاونت')
GO

INSERT INTO dbo.tblVahed (id, name_vahed)
VALUES 
  (6, N'دفتر فنی')
GO

INSERT INTO dbo.tblVahed (id, name_vahed)
VALUES 
  (7, N'مالی')
GO

SET IDENTITY_INSERT dbo.tblVahed OFF
GO

--
-- Data for table dbo.Users  (LIMIT 0,500)
--

INSERT INTO dbo.Users (Username, Password)
VALUES 
  (N'Admin', N'12')
GO

INSERT INTO dbo.Users (Username, Password)
VALUES 
  (N'کاربر شماره 1', N'10')
GO

INSERT INTO dbo.Users (Username, Password)
VALUES 
  (N'کاربر شماره 2', N'10')
GO

--
-- Definition for indices : 
--

ALTER TABLE dbo.sysdiagrams
ADD CONSTRAINT UK_principal_name 
UNIQUE NONCLUSTERED (principal_id, name)
WITH (
  PAD_INDEX = OFF,
  IGNORE_DUP_KEY = OFF,
  STATISTICS_NORECOMPUTE = OFF,
  ALLOW_ROW_LOCKS = ON,
  ALLOW_PAGE_LOCKS = ON)
ON [PRIMARY]
GO

