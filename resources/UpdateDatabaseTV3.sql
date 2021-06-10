USE [NewDBName]
GO

-----------------------------------------------------------------------------------------------------
-----------------------------------------------------------------------------------------------------
-- Procedures
-----------------------------------------------------------------------------------------------------
-----------------------------------------------------------------------------------------------------

-- General procedures for saving and retrieving values from SETTING table
-- SPTV_GETSETTINGINT
if not exists(select * from sys.objects where object_id=OBJECT_ID(N'[dbo].[SPTV_GETSETTINGINT]') and type in (N'P'))
begin
  exec sp_executesql 
  N'create procedure SPTV_GETSETTINGINT @paramname nvarchar(50), @returnvalue int output
    as
    declare @value int
    set @value = @returnvalue
    begin try
      select @returnvalue = cast(PARAMVALUE as int) from SETTING where PARAMNAME=@paramname
    end try
    begin catch
      set @value = @value
    end catch'
end
GO

-- SPTV_GETSETTINGSTR
if not exists(select * from sys.objects where object_id=OBJECT_ID(N'[dbo].[SPTV_GETSETTINGSTR]') and type in (N'P'))
begin
  exec sp_executesql 
  N'create procedure SPTV_GETSETTINGSTR @paramname nvarchar(50), @returnvalue nvarchar(250) output
    as
    declare @value nvarchar(250)
    set @value = @returnvalue
    begin try
      select @returnvalue = PARAMVALUE from SETTING where PARAMNAME=@paramname
    end try
    begin catch
      set @value = @value
    end catch'
end
GO

-- SPTV_SETSETTINGINT
if not exists(select * from sys.objects where object_id=OBJECT_ID(N'[dbo].[SPTV_SETSETTINGINT]') and type in (N'P'))
begin
  exec sp_executesql 
  N'create procedure SPTV_SETSETTINGINT @paramname nvarchar(50), @value int
    as
    declare @valuestring nvarchar(50)
    set @valuestring = cast(@value as nvarchar)
    if not exists(select * from SETTING where PARAMNAME=@paramname)
    begin
      insert into SETTING(PARAMNAME, PARAMVALUE) values (@paramname, @valuestring)
    end else begin
      update SETTING set PARAMVALUE=@valuestring where PARAMNAME=@paramname
    end'
end
GO

-- SPTV_SETSETTINGSTR
if not exists(select * from sys.objects where object_id=OBJECT_ID(N'[dbo].[SPTV_SETSETTINGSTR]') and type in (N'P'))
begin
  exec sp_executesql 
  N'create procedure SPTV_SETSETTINGSTR @paramname nvarchar(50), @value nvarchar(250)
    as
    if not exists(select * from SETTING where PARAMNAME=@paramname)
    begin
      insert into SETTING(PARAMNAME, PARAMVALUE) values (@paramname, @value)
    end else begin
      update SETTING set PARAMVALUE=@value where PARAMNAME=@paramname
    end'
end
GO

-- Procedures for storing and retrieving version number

-- SPTV_GETTVVERSION
if not exists(select * from sys.objects where object_id=OBJECT_ID(N'[dbo].[SPTV_GETTVVERSION]') and type in (N'P'))
begin
  exec sp_executesql 
  N'create procedure SPTV_GETTVVERSION @version nvarchar(250) output
    as
    execute SPTV_GETSETTINGSTR ''TVVERSION'', @version out'
end
GO

-- SPTV_SETTVVERSION
if not exists(select * from sys.objects where object_id=OBJECT_ID(N'[dbo].[SPTV_SETTVVERSION]') and type in (N'P'))
begin
  exec sp_executesql 
  N'create procedure SPTV_SETTVVERSION @version nvarchar(250)
    as
    execute SPTV_SETSETTINGSTR ''TVVERSION'', @version'
end
GO

--CreateOrReplaceView
if exists(select * from sys.objects where object_id=OBJECT_ID(N'[dbo].[sp_CreateOrReplaceView]') and type in (N'P'))
  drop procedure sp_CreateOrReplaceView
GO
  
create procedure [dbo].[sp_CreateOrReplaceView] @ViewName varchar(40), @SelectSQL text
AS
	declare
		@stat varchar(50),
		@SQLText varchar(8000)

	if exists(select * from dbo.sysobjects where id = object_id(@ViewName) and OBJECTPROPERTY(id, N'IsView') = 1)
		set @stat = N'alter' 
	else
		set @stat = N'create'

	set @SQLText = @stat + N' view ' + @ViewName + N' as '+cast(@SelectSQL as varchar(8000))

	exec sp_sqlexec @SQLText
GO

-----------------------------------------------------------------------------------------------------
-----------------------------------------------------------------------------------------------------
-- Tables
-----------------------------------------------------------------------------------------------------
-----------------------------------------------------------------------------------------------------
if not exists(select * from INFORMATION_SCHEMA.TABLES where TABLE_NAME='ATTACHMENTFIELD') begin
	CREATE TABLE [dbo].[ATTACHMENTFIELD](
		[REGISTRATIONID]      [INT]				NOT NULL,
		[ATTACHMENTPATH]      [NVARCHAR](255)	NOT NULL,
		[DATAID]              [NVARCHAR](255)	NOT NULL,
		[DATAVALUE]           [NVARCHAR](255)	NOT NULL,
		[FIELDID]             [NVARCHAR](255)	NOT NULL,
		[CAPTION]             [NVARCHAR](255)	NOT NULL,
		[PARENTID]            [NVARCHAR](255),  
		[PARENTVALUE]         [NVARCHAR](255), 
    PRIMARY KEY ([ATTACHMENTPATH], [REGISTRATIONID])
)
end
GO

if not exists(select * from INFORMATION_SCHEMA.TABLES where TABLE_NAME='OFFLINECACHE') begin
	CREATE TABLE [dbo].[OFFLINECACHE](
		 [RECID] [int] IDENTITY(1,1) NOT NULL,
		 [SYSTEM] [nvarchar](50) NOT NULL,
		 [SYSTEMID] [nvarchar](50) NOT NULL,
		 [CACHEVALUE] [nvarchar](max) NULL,
        CONSTRAINT [PK_OFFLINECACHE] PRIMARY KEY CLUSTERED
	         ([RECID] ASC
	)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
	) ON [PRIMARY]
end
GO

if not exists(select * from INFORMATION_SCHEMA.TABLES where TABLE_NAME='NUMBERLOG') begin
	CREATE TABLE [dbo].[NUMBERLOG](
		[RECID] [int] IDENTITY(1,1) NOT NULL,
		[DEVICEID] [nvarchar](50) NOT NULL,
		[PHONENUMBER] [nchar](100) NOT NULL,
		[LOGTYPE] [int] NOT NULL,
		[TIMESTAMP] [datetime] NOT NULL,
	 CONSTRAINT [PK_NUMBERLOG] PRIMARY KEY CLUSTERED 
	(
		[RECID] ASC
	)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
	) ON [PRIMARY]
end
GO

if not exists(select * from INFORMATION_SCHEMA.TABLES where TABLE_NAME='CALLERHISTORYFILTER') begin
	CREATE TABLE [dbo].[CALLERHISTORYFILTER](
		[RECID] [int] IDENTITY(1,1) NOT NULL,
		[CAPTION] [nvarchar](50) NOT NULL,
		[CUSTOMFIELDID] [int] NOT NULL,
		[CUSTOMFIELDVALUEIDS] [nvarchar](max) NULL,
		[DEVICEINTERVALS] [nvarchar](max) NULL,
	 CONSTRAINT [PK_CALLERHISTORYFILTER] PRIMARY KEY CLUSTERED 
	(
		[RECID] ASC
	)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
	) ON [PRIMARY]
end
GO

-- TAble used to store user images
if not exists(select * from INFORMATION_SCHEMA.TABLES where TABLE_NAME='RESOURCEIMAGE') begin
CREATE TABLE [dbo].[RESOURCEIMAGE](
	[USERID] [int] NOT NULL,
	[SMALLIMAGE] [nvarchar](max) NULL,
	[LARGEIMAGE] [nvarchar](max) NULL,
 CONSTRAINT [PK_RESOURCEIMAGE] PRIMARY KEY CLUSTERED 
(
	[USERID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
end
go

-- TAble used to store personal user groups. A list of favourites users to se in the client when selecting "My/Favourites"
if not exists(select * from INFORMATION_SCHEMA.TABLES where TABLE_NAME='RESOURCEGROUP') begin
CREATE TABLE [dbo].[RESOURCEGROUP](
	[RECID] [int] IDENTITY(1,1) NOT NULL,
	[OWNERRESOURCEID] [int] NOT NULL,
	[MEMBERRESOURCEID] [int] NOT NULL,
 CONSTRAINT [PK_RESOURCEGROUP] PRIMARY KEY CLUSTERED 
(
	[RECID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
end
go

if not exists(select * from INFORMATION_SCHEMA.TABLES where TABLE_NAME='CALLSTATISTIC') begin
CREATE TABLE [dbo].[CALLSTATISTIC](
	[RECID] [int] IDENTITY(1,1) NOT NULL,
	[CALLDIRECTION] [varchar](50) NOT NULL,
	[CALLLENGTH] [int] NOT NULL,
	[CREATEDTIME] [datetime] NOT NULL,
	[STARTTIME] [datetime] NULL,
	[ENDTIME] [datetime] NULL,
	[INITIATINGDEVICE] [varchar](50) NOT NULL,
	[CALLINGDEVICE] [varchar](50) NOT NULL,
	[CALLEDDEVICE] [varchar](50) NOT NULL,
	[CALLEDDEVICERESERVATIONID] [int] NULL,
	[CALLEDDEVICESTATEID] [int] NULL,
	[CALLEDDEVICECOMSTATE] [varchar](50) NULL,
	[EXTERNALDEVICETYPE] [varchar](50) NULL,
	[NOOFESTABLISHED] [int] NOT NULL,
	[FIRSTQUEUETIME] [int] NULL,
	[FIRSTQUEUEDEVICE] [varchar](50) NOT NULL,
	[FIRSTDELIVEREDTIME] [int] NULL,
	[FIRSTDELIVEREDDEVICE] [varchar](50) NOT NULL,
	[FIRSTESTABLISHEDTIME] [int] NULL,
	[FIRSTESTABLISHEDDEVICE] [varchar](50) NOT NULL,
	[SECONDESTABLISHEDTIME] [int] NULL,
	[SECONDESTABLISHEDDEVICE] [varchar](50) NOT NULL,
	[LASTESTABLISHEDTIME] [int] NULL,
	[LASTESTABLISHEDDEVICE] [varchar](50) NOT NULL,
	[LASTTBESTABLISHEDDEVICE] [varchar](50) NULL,
	[LASTTRANSFERREDTIME] [int] NULL,
	[LASTTRANSFERRINGDEVICE] [varchar](50) NOT NULL,
	[LASTTRANSFERREDTODEVICE] [varchar](50) NOT NULL,
	[LASTTRANSFERREDTORESERVATIONID] [int] NULL,
	[LASTTRANSFERREDTOSTATEID] [int] NULL,
	[LASTTRANSFERREDTOCOMSTATE] [varchar](50) NULL,
	[FAILEDCAUSE] [varchar](50) NOT NULL,
	[CALLENDED] [int] NOT NULL,
	[GID] [varchar](50) NOT NULL,
 CONSTRAINT [PK_CALLGENERICSTATISTIC] PRIMARY KEY CLUSTERED 
(
	[RECID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
end
GO

if not exists(select * from INFORMATION_SCHEMA.TABLES where TABLE_NAME='CHANGELOG') begin
CREATE TABLE [dbo].[CHANGELOG](
	[CHANGETYPE] [nvarchar](50) NOT NULL,
	[ACTION] [nvarchar](50) NOT NULL,
	[RECID] [int] NOT NULL,
	[RESOURCEID] [int] NOT NULL,
	[CHANGETIME] [bigint] NOT NULL
) ON [PRIMARY]
end
GO

if not exists(select * from INFORMATION_SCHEMA.TABLES where TABLE_NAME='REPORTRESOURCESETTING') begin
    CREATE TABLE [dbo].[REPORTRESOURCESETTING](
        [RESOURCERECID] [int] NOT NULL,
        [PARAMNAME] [nvarchar](50) NOT NULL,
        [PARAMVALUE] [nvarchar](250) NOT NULL,
        PRIMARY KEY ([RESOURCERECID], [PARAMNAME])
    ) ON [PRIMARY];
	select 'Created table REPORTRESOURCESETTING';
end
go

if not exists(select * from INFORMATION_SCHEMA.TABLES where TABLE_NAME='ACCESSTOKEN') begin
CREATE TABLE [dbo].[ACCESSTOKEN](
	[RECID]			[INT] IDENTITY(1,1)		NOT NULL,
	[TOKEN]			[NVARCHAR](36)			NOT NULL,
	[TIMETOLIVE]	[DATETIME2](7)			NULL,
	[CREATED]		[DATETIME2](7)			NOT NULL,
	[LASTUSED]		[DATETIME2](7)			NULL,
	[RESOURCEID]	[INT]					NULL,
 CONSTRAINT [PK_ACCESSTOKEN] PRIMARY KEY CLUSTERED 
(
	[RECID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]

ALTER TABLE [dbo].[ACCESSTOKEN] ADD  CONSTRAINT [DF_ACCESSTOKEN_CREATED]  DEFAULT (getdate()) FOR [CREATED]

ALTER TABLE [dbo].[ACCESSTOKEN]  WITH CHECK ADD  CONSTRAINT [FK_RESOURCE_ACCESSTOKEN] FOREIGN KEY([RESOURCEID])
REFERENCES [dbo].[RESOURCE] ([RECID])
ON DELETE SET NULL

end
GO

if not exists(select * from INFORMATION_SCHEMA.TABLES where TABLE_NAME='PASSWORDRESETTOKEN') begin
CREATE TABLE [dbo].[PASSWORDRESETTOKEN](
	[RECID]			[INT] IDENTITY(1,1)		NOT NULL,
	[TOKEN]			[NVARCHAR](36)			NOT NULL,
	[CREATED]		[DATETIME2](7)			NOT NULL,
	[RESOURCEID]	[INT]					NULL,
 CONSTRAINT [PK_PASSWORDRESETTOKEN] PRIMARY KEY CLUSTERED 
(
	[RECID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]

ALTER TABLE [dbo].[PASSWORDRESETTOKEN] ADD  CONSTRAINT [DF_PASSWORDRESETTOKEN_CREATED]  DEFAULT (getdate()) FOR [CREATED]

ALTER TABLE [dbo].[PASSWORDRESETTOKEN]  WITH CHECK ADD  CONSTRAINT [FK_RESOURCE_PASSWORDRESETTOKEN] FOREIGN KEY([RESOURCEID])
REFERENCES [dbo].[RESOURCE] ([RECID])
ON DELETE SET NULL

end
GO

if not exists(select * from INFORMATION_SCHEMA.TABLES where TABLE_NAME='TENANT') 
begin
CREATE TABLE [dbo].[TENANT](
	[RECID]				  [INT] IDENTITY(1,1) NOT NULL,
	[CUSTOMER]            [NVARCHAR](max)	  NOT NULL
 CONSTRAINT [PK_TENANT] PRIMARY KEY CLUSTERED 
(
	[RECID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]

-- Insert default tentant
insert into TENANT(CUSTOMER) values ('Default')
end
GO

if not exists(select * from INFORMATION_SCHEMA.TABLES where TABLE_NAME='CALLCENTERRULE') 
begin
CREATE TABLE [dbo].[CALLCENTERRULE](
	[RECID]				  [INT] IDENTITY(1,1) NOT NULL,
	[ORDERNO]			  [INT]				  NOT NULL,
	[NAME]                [NVARCHAR](max)	  NOT NULL,
	[APPLIESTO]           [NVARCHAR](max)	  NOT NULL,
	[OPENINGHOURS]        [NVARCHAR](max)	  NOT NULL,
	[OPENINGDAYS]         [NVARCHAR](max)	  NOT NULL,
	[EXCEPTDATES]         [NVARCHAR](max)	  NULL,
	[MINAGENTS]			  [INT]				  NOT NULL,
	[MESSAGETOSEND]       [NVARCHAR](max)	  NOT NULL,
	[MAILRECIPIENTS]      [NVARCHAR](max)	  NULL,
	[SMSRECIPIENTS]       [NVARCHAR](max)	  NULL
 CONSTRAINT [PK_CALLCENTERRULE] PRIMARY KEY CLUSTERED 
(
	[RECID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
end
GO




-----------------------------------------------------------------------------------------------------
-----------------------------------------------------------------------------------------------------
-- Primary Keys
-----------------------------------------------------------------------------------------------------
-----------------------------------------------------------------------------------------------------

if not exists(select * from [INFORMATION_SCHEMA].[TABLE_CONSTRAINTS] WHERE upper([TABLE_NAME]) = 'REPORTRESOURCESETTING' and [CONSTRAINT_TYPE] = 'PRIMARY KEY') begin
    truncate table [dbo].[REPORTRESOURCESETTING];	-- Rows must be deleted because there will be duplicate (RESOURCERECID, PARAMNAME) entries in table, preventing the primary key creation to take place
													-- Table can be truncated because data is likely wrong for most users anyways. If it is correct, it will only be by accident
	alter table [dbo].[REPORTRESOURCESETTING] add primary key([RESOURCERECID], [PARAMNAME]);
	select 'Add primary key to table REPORTRESOURCESETTING';
end
go

-----------------------------------------------------------------------------------------------------
-----------------------------------------------------------------------------------------------------
-- Fields
-----------------------------------------------------------------------------------------------------
-----------------------------------------------------------------------------------------------------


if not exists(select * from INFORMATION_SCHEMA.COLUMNS where TABLE_NAME='CUSTOMFIELD' and COLUMN_NAME='USEINREPORTS')
begin
  alter table CUSTOMFIELD add USEINREPORTS int null default 0
end
GO


if not exists(select * from INFORMATION_SCHEMA.COLUMNS where TABLE_NAME='STATEFINISH' and COLUMN_NAME='RECID')
begin
  if exists(select * from INFORMATION_SCHEMA.TABLE_CONSTRAINTS where CONSTRAINT_NAME='PK_STATEFINISH')
  begin
    exec sp_executesql N'alter table STATEFINISH drop constraint PK_STATEFINISH'
  end
  exec sp_executesql N'alter table STATEFINISH add RECID int not null identity (1,1) constraint PK_STATEFINISH primary key'
end
GO

-- PROFILE, add DNDTYPEENUM
if not exists(select * from INFORMATION_SCHEMA.COLUMNS where TABLE_NAME='PROFILE' and COLUMN_NAME='DNDTYPEENUM')
begin
  alter table PROFILE add DNDTYPEENUM int not null default 0
end
GO

-- PROFILE, add NEXTDNDTYPEENUM
if not exists(select * from INFORMATION_SCHEMA.COLUMNS where TABLE_NAME='PROFILE' and COLUMN_NAME='NEXTDNDTYPEENUM')
begin
  alter table PROFILE add NEXTDNDTYPEENUM int not null default 0
end
GO

-- PROFILE, add ACTIVATEFROMSMARTCLIENT
if not exists(select * from INFORMATION_SCHEMA.COLUMNS where TABLE_NAME='PROFILE' and COLUMN_NAME='ACTIVATEFROMSMARTCLIENT')
begin
  alter table PROFILE ADD ACTIVATEFROMSMARTCLIENT integer not null default(1)     
  Exec('update PROFILE set ACTIVATEFROMSMARTCLIENT = ACTIVATEFROMCLIENT')
end
GO


-- RESOURCE USERID. Increase length of field to allow 20 characters.
if exists(select * from INFORMATION_SCHEMA.COLUMNS where TABLE_NAME='RESOURCE' and COLUMN_NAME='USERID' and CHARACTER_MAXIMUM_LENGTH<>50) 
begin
  alter table RESOURCE alter column USERID nvarchar(50) NULL
end
GO

-- RESOURCE CALENDARCONNECTORID. Add column if it does not exist
if not exists(select * from INFORMATION_SCHEMA.COLUMNS where TABLE_NAME='RESOURCE' and COLUMN_NAME='CALENDARCONNECTORID') 
begin
  alter table RESOURCE ADD 
    CALENDARCONNECTORID integer not null default(-1)
end
GO

-- RESOURCE USEATTACHMENTS. Add column if it does not exist
if not exists(select * from INFORMATION_SCHEMA.COLUMNS where TABLE_NAME='RESOURCE' and COLUMN_NAME='USEATTACHMENTS') 
begin
  alter table RESOURCE ADD 
    USEATTACHMENTS integer not null default(0) 
end
go

-- RESOURCE RIGHTSSMSANONYMOUS. Add column if it does not exist
if not exists(select * from INFORMATION_SCHEMA.COLUMNS where TABLE_NAME='RESOURCE' and COLUMN_NAME='RIGHTSSMSANONYMOUS') 
begin
  alter table RESOURCE ADD 
    RIGHTSSMSANONYMOUS integer not null default(0) 
end
go

-- RESOURCE RIGHTSSMSANONYMOUS. Add column if it does not exist
if not exists(select * from INFORMATION_SCHEMA.COLUMNS where TABLE_NAME='RESOURCE' and COLUMN_NAME='REQUIREATTACHMENT') 
begin
  alter table RESOURCE ADD REQUIREATTACHMENT integer not null default(0) 
end
go

-- RESOURCE MUSTCHANGEPASSWORD. Add column if it does not exist
if not exists(select * from INFORMATION_SCHEMA.COLUMNS where TABLE_NAME='RESOURCE' and COLUMN_NAME='MUSTCHANGEPASSWORD') 
begin
  alter table RESOURCE ADD 
    MUSTCHANGEPASSWORD bit not null default(0)
  select 'Added MUSTCHANGEPASSWORD bit not null default(0) to table RESOURCE'
end
GO


if not exists(select * from INFORMATION_SCHEMA.COLUMNS where TABLE_NAME='RESOURCE' and COLUMN_NAME='ADCONNECTORID') 
begin
  alter table RESOURCE ADD 
    ADCONNECTORID integer 
end
GO

if not exists(select * from INFORMATION_SCHEMA.COLUMNS where TABLE_NAME='RESOURCE' and COLUMN_NAME='ADGROUPTYPE') 
begin
  alter table RESOURCE ADD 
    ADGROUPTYPE integer 
end
GO

if not exists(select * from INFORMATION_SCHEMA.COLUMNS where TABLE_NAME='RESOURCE' and COLUMN_NAME='ADISSYNCHRONIZED') 
begin
  alter table RESOURCE ADD 
    ADISSYNCHRONIZED integer
end
GO

if not exists(select * from INFORMATION_SCHEMA.COLUMNS where TABLE_NAME='RESOURCE' and COLUMN_NAME='ADISADOBJECT') 
begin
  alter table RESOURCE ADD 
    ADISADOBJECT integer 
end
GO

if not exists(select * from INFORMATION_SCHEMA.COLUMNS where TABLE_NAME='RESOURCE' and COLUMN_NAME='ADSTANDALONE') 
begin
  alter table RESOURCE ADD 
    ADSTANDALONE integer 
end
GO

if not exists(select * from INFORMATION_SCHEMA.COLUMNS where TABLE_NAME='RESOURCE' and COLUMN_NAME='ADMEMBERS') 
begin
  alter table RESOURCE ADD 
    ADMEMBERS nvarchar(max) 
end
GO

if not exists(select * from INFORMATION_SCHEMA.COLUMNS where TABLE_NAME='RESOURCE' and COLUMN_NAME='ADMEMBEROF') 
begin
  alter table RESOURCE ADD 
    ADMEMBEROF nvarchar(max) 
end
GO

if not exists(select * from INFORMATION_SCHEMA.COLUMNS where TABLE_NAME='RESOURCE' and COLUMN_NAME='LICENSETYPES') 
begin
  alter table RESOURCE ADD 
    LICENSETYPES nvarchar(max) 
end
GO

if not exists(select * from INFORMATION_SCHEMA.COLUMNS where TABLE_NAME='RESOURCE' and COLUMN_NAME='PASSWORDCHANGEDBY') 
begin
  alter table RESOURCE ADD 
    PASSWORDCHANGEDBY nvarchar(max) 
end
GO

if not exists(select * from INFORMATION_SCHEMA.COLUMNS where TABLE_NAME='RESOURCE' and COLUMN_NAME='PASSWORDCHANGEDDATE') 
begin
  alter table RESOURCE ADD 
    PASSWORDCHANGEDDATE datetime2(7)
end
GO

if not exists(select * from INFORMATION_SCHEMA.COLUMNS where TABLE_NAME='CUSTOMFIELD' and COLUMN_NAME='ADSYNCHRONIZED') 
begin
  alter table CUSTOMFIELD ADD ADSYNCHRONIZED int NOT NULL Default -1
end
GO


if not exists(select * from INFORMATION_SCHEMA.COLUMNS where TABLE_NAME='STATE' and COLUMN_NAME='TEXTHIDDEN')
begin
  alter table STATE ADD
    TEXTHIDDEN integer not null default(0)
end
GO

if not exists(select * from INFORMATION_SCHEMA.COLUMNS where TABLE_NAME='STATE' and COLUMN_NAME='ACTIVATEFROMSMARTCLIENT')
begin
  alter table STATE ADD ACTIVATEFROMSMARTCLIENT integer not null default(1)
  Exec('update STATE set ACTIVATEFROMSMARTCLIENT = ACTIVATEFROMCLIENT')
end
GO

if not exists(select * from INFORMATION_SCHEMA.COLUMNS where TABLE_NAME='STATE' and COLUMN_NAME='VALIDUNTILEND')
begin
  alter table STATE ADD VALIDUNTILEND integer not null default(0)
end
GO

if not exists(select * from INFORMATION_SCHEMA.COLUMNS where TABLE_NAME='STATE' and COLUMN_NAME='DONOTPROMOTE')
begin
  alter table STATE ADD DONOTPROMOTE integer not null default(0)
end
GO

if not exists(select * from INFORMATION_SCHEMA.COLUMNS where TABLE_NAME='STATE' and COLUMN_NAME='USEINTOTALVIEWTIME')
begin
  alter table STATE ADD USEINTOTALVIEWTIME integer not null default(1)
end
GO

if not exists(select * from INFORMATION_SCHEMA.COLUMNS where TABLE_NAME='PHONEBOOK' and COLUMN_NAME='USERLEVEL') 
begin
  alter table PHONEBOOK ADD 
    USERLEVEL integer not null default(0)
end
GO

if not exists(select * from INFORMATION_SCHEMA.COLUMNS where TABLE_NAME='PHONEBOOK' and COLUMN_NAME='OWNER') 
begin
  alter table PHONEBOOK ADD 
    [OWNER] nvarchar(50) null 
end
GO

ALTER TABLE PhoneBook ALTER COLUMN Number NVARCHAR(Max)

Go

if not exists(select * from INFORMATION_SCHEMA.COLUMNS where TABLE_NAME='RESOURCEDETAIL' and COLUMN_NAME='EXTERNALMONITOR') 
begin
  alter table RESOURCEDETAIL ADD 
    EXTERNALMONITOR integer Not null Default 1
end
GO

if not exists(select * from INFORMATION_SCHEMA.COLUMNS where TABLE_NAME='STATE' and COLUMN_NAME='ALLOWCURRENTSTATE') 
begin
  alter table STATE ADD 
    ALLOWCURRENTSTATE integer Not null Default 1
end
GO

if not exists(select * from INFORMATION_SCHEMA.COLUMNS where TABLE_NAME='STATE' and COLUMN_NAME='PUBLICSTATE') 
begin
  alter table STATE ADD 
    PUBLICSTATE integer Not null Default -1
end
GO

if not exists(select * from INFORMATION_SCHEMA.COLUMNS where TABLE_NAME='STATE' and COLUMN_NAME='PUBLISHEDSTATE') 
begin
  alter table STATE ADD 
    PUBLISHEDSTATE integer Not null Default -1
end

if not exists(select * from INFORMATION_SCHEMA.COLUMNS where TABLE_NAME='STATE' and COLUMN_NAME='CALLSTATISTICSTYPE') 
begin
  alter table STATE ADD CALLSTATISTICSTYPE integer Not null Default 1
end

if not exists(select * from INFORMATION_SCHEMA.COLUMNS where TABLE_NAME='STATE' and COLUMN_NAME='HIDEMOBILESTATE') 
begin
  alter table STATE ADD HIDEMOBILESTATE integer Not null Default 0
end

if not exists(select * from INFORMATION_SCHEMA.COLUMNS where TABLE_NAME='RESOURCE' and COLUMN_NAME='RIGHTSREMOTECALLING') 
begin
  alter table RESOURCE ADD 
    RIGHTSREMOTECALLING integer Not null Default 0
end
GO

if not exists(select * from INFORMATION_SCHEMA.COLUMNS where TABLE_NAME='RESOURCE' and COLUMN_NAME='USERDAYLENGTH') 
begin
  alter table RESOURCE ADD USERDAYLENGTH integer null
end
GO

if not exists(select * from INFORMATION_SCHEMA.COLUMNS where TABLE_NAME='NUMBERLOG' and COLUMN_NAME='STARTTIME') 
begin
  alter table NUMBERLOG ADD 
    STARTTIME  datetime Not Null
end
GO

if not exists(select * from INFORMATION_SCHEMA.COLUMNS where TABLE_NAME='NUMBERLOG' and COLUMN_NAME='ENDTIME') 
begin
  alter table NUMBERLOG ADD 
    ENDTIME datetime Not Null
end
GO

if exists(select * from INFORMATION_SCHEMA.COLUMNS where TABLE_NAME='NUMBERLOG' and COLUMN_NAME='TIMESTAMP') begin
  exec sp_executesql N'update NUMBERLOG set STARTTIME=[TIMESTAMP]'
  exec sp_executesql N'alter table NUMBERLOG drop column TIMESTAMP'
end
GO 

if exists(select * from SYS.DEFAULT_CONSTRAINTS where NAME = 'DF_RESOURCE_RIGHTSWEBCLIENT') 
begin
	ALTER TABLE [dbo].[RESOURCE] DROP  CONSTRAINT [DF_RESOURCE_RIGHTSWEBCLIENT]
	select 'Removed default constraint on column RIGHTSMOBILEWEB in RESOURCE table';
end
GO

if exists(select * from INFORMATION_SCHEMA.COLUMNS where TABLE_NAME='RESOURCE' and COLUMN_NAME='RIGHTSMOBILEWEB') 
begin
  if exists(select con.[name] as constraint_name, schema_name(t.schema_id) + '.' + t.[name]  as [table], col.[name] as column_name, con.[definition]
    from sys.default_constraints con
      left outer join sys.objects t on con.parent_object_id = t.object_id
      left outer join sys.all_columns col on con.parent_column_id = col.column_id and con.parent_object_id = col.object_id
    where
      upper(schema_name(t.schema_id) + '.' + t.[name]) = upper('DBO.RESOURCE') and upper(col.[name]) = 'RIGHTSMOBILEWEB') 
  begin
    select 'An unknown default constraint is defined on RIGHTSMOBILEWEB column in RESOURCE table. Column must be removed manually.';
  end
  else
  begin
    alter table RESOURCE DROP COLUMN RIGHTSMOBILEWEB;
    select 'Removed column RIGHTSMOBILEWEB from RESOURCE table';
  end;
end
GO

if not exists(select * from INFORMATION_SCHEMA.COLUMNS where TABLE_NAME='RESOURCE' and COLUMN_NAME='RIGHTSSMARTPHONE') 
begin
  alter table RESOURCE ADD 
    RIGHTSSMARTPHONE integer Not null Default 0
end
GO

if not exists(select * from INFORMATION_SCHEMA.COLUMNS where TABLE_NAME='RESOURCE' and COLUMN_NAME='RIGHTSCALLCENTER') 
begin
  alter table RESOURCE ADD 
    RIGHTSCALLCENTER integer Not null Default 0
end
GO

if not exists(select * from INFORMATION_SCHEMA.COLUMNS where TABLE_NAME='RESOURCE' and COLUMN_NAME='RIGHTSCALLCENTERADMIN') 
begin
  alter table RESOURCE ADD 
    RIGHTSCALLCENTERADMIN integer Not null Default 0
  select 'Added RIGHTSCALLCENTERADMIN integer Not null Default 0 to table RESOURCE'
end
GO

if not exists(select * from INFORMATION_SCHEMA.COLUMNS where TABLE_NAME='RESOURCE' and COLUMN_NAME='RIGHTSCONTACTCENTERAGENT') 
begin
  alter table RESOURCE ADD 
    RIGHTSCONTACTCENTERAGENT integer Not null Default 0
  select 'Added RIGHTSCONTACTCENTERAGENT integer Not null Default 0 to table RESOURCE'
end
GO

if not exists(select * from INFORMATION_SCHEMA.COLUMNS where TABLE_NAME='RESOURCE' and COLUMN_NAME='RIGHTSSETCALLERID') 
begin
  alter table RESOURCE ADD 
    RIGHTSSETCALLERID integer Not null Default 0
end
GO

if not exists(select * from INFORMATION_SCHEMA.COLUMNS where TABLE_NAME='RESOURCE' and COLUMN_NAME='RIGHTSSHAREPOINT') 
begin
  alter table RESOURCE ADD 
    RIGHTSSHAREPOINT integer Not null Default 0
end
GO

if not exists(select * from INFORMATION_SCHEMA.COLUMNS where TABLE_NAME='RESOURCE' and COLUMN_NAME='PUBLISHUSER') 
begin
  alter table RESOURCE ADD 
    PUBLISHUSER integer Not null Default 0
end
GO

if not exists(select * from INFORMATION_SCHEMA.COLUMNS where TABLE_NAME='RESOURCE' and COLUMN_NAME='EXTERNALPASSWORD') 
begin
  alter table RESOURCE ADD 
    EXTERNALPASSWORD nvarchar(64) null 
end
GO

if not exists(select * from INFORMATION_SCHEMA.COLUMNS where TABLE_NAME='RESOURCE' and COLUMN_NAME='LOCKCALENDAR') 
begin
  alter table RESOURCE ADD 
    LOCKCALENDAR integer Not null Default 0
end
GO

if not exists(select * from INFORMATION_SCHEMA.COLUMNS where TABLE_NAME='RESOURCE' and COLUMN_NAME='NUMBERLOG') 
begin
  alter table RESOURCE ADD 
    NUMBERLOG integer Not null Default 1
end
GO

if not exists(select * from INFORMATION_SCHEMA.COLUMNS where TABLE_NAME='RESOURCE' and COLUMN_NAME='NUMBERLOGEXTERNAL') 
begin
  alter table RESOURCE ADD 
    NUMBERLOGEXTERNAL integer Not null Default 1
end
GO

if not exists(select * from INFORMATION_SCHEMA.COLUMNS where TABLE_NAME='RESOURCE' and COLUMN_NAME='RIGHTSCALLSTATISTICS') 
begin
  alter table RESOURCE ADD 
    RIGHTSCALLSTATISTICS integer Not null Default 0
end
GO

if not exists(select * from INFORMATION_SCHEMA.COLUMNS where TABLE_NAME='RESOURCE' and COLUMN_NAME='DISABLEADRESYNC') 
begin
  alter table RESOURCE ADD 
    DISABLEADRESYNC integer Not null Default 0
end
GO

if not exists(select * from INFORMATION_SCHEMA.COLUMNS where TABLE_NAME='RESOURCE' and COLUMN_NAME='DELETED') 
begin
  alter table RESOURCE ADD 
    DELETED integer Not null Default 0
end
GO


if not exists(select * from INFORMATION_SCHEMA.COLUMNS where TABLE_NAME='FORWARDINGRULE' and COLUMN_NAME='PROFILEID') 
begin
  alter table FORWARDINGRULE ADD 
    PROFILEID integer Not null Default -1
end
GO

if not exists(select * from INFORMATION_SCHEMA.COLUMNS where TABLE_NAME='FORWARDINGRULE' and COLUMN_NAME='FORWARDTYPE') 
begin
  alter table FORWARDINGRULE ADD 
    FORWARDTYPE integer Not null Default 0
end
GO

if not exists(select * from INFORMATION_SCHEMA.COLUMNS where TABLE_NAME='FORWARDINGRULE' and COLUMN_NAME='DNDTYPE') 
begin
  alter table FORWARDINGRULE ADD 
    DNDTYPE integer Not null Default 0
end
GO

if not exists(select * from INFORMATION_SCHEMA.COLUMNS where TABLE_NAME='RESERVATION' and COLUMN_NAME='DELETED') 
begin
  alter table RESERVATION ADD 
    DELETED int not null default 0 
end
GO

if not exists(select * from INFORMATION_SCHEMA.COLUMNS where TABLE_NAME='RESERVATION' and COLUMN_NAME='ATTACHMENT') 
begin
  alter table RESERVATION ADD 
    ATTACHMENT xml null
end
GO

if not exists(select * from INFORMATION_SCHEMA.COLUMNS where TABLE_NAME='RESERVATION' and COLUMN_NAME='ORIGINSYSTEM') 
begin
  alter table RESERVATION ADD 
    ORIGINSYSTEM nvarchar(50) null 
end
GO

if not exists(select * from INFORMATION_SCHEMA.COLUMNS where TABLE_NAME='RESERVATION' and COLUMN_NAME='ORIGINEXECUTION') 
begin
  alter table RESERVATION ADD 
    ORIGINEXECUTION int null 
end
GO

if not exists(select * from INFORMATION_SCHEMA.COLUMNS where TABLE_NAME='RESERVATION' and COLUMN_NAME='ORIGINRESOURCEID') 
begin
  alter table RESERVATION ADD 
    ORIGINRESOURCEID int null 
end
GO

-- Forwarding Rules 20-02-2010

if not exists(select * from INFORMATION_SCHEMA.COLUMNS where TABLE_NAME='FORWARDINGRULE' and COLUMN_NAME='VERSION') 
begin
  alter table FORWARDINGRULE ADD VERSION int NULL
end
GO

if not exists(select * from INFORMATION_SCHEMA.COLUMNS where TABLE_NAME='FORWARDINGRULE' and COLUMN_NAME='ACTIVE') 
begin
  alter table FORWARDINGRULE ADD 
    ACTIVE int NULL
end
GO

if not exists(select * from INFORMATION_SCHEMA.COLUMNS where TABLE_NAME='FORWARDINGRULE' and COLUMN_NAME='CAPTION') 
begin
  alter table FORWARDINGRULE ADD 
    CAPTION nvarchar(50) NULL
end
GO

if not exists(select * from INFORMATION_SCHEMA.COLUMNS where TABLE_NAME='FORWARDINGRULE' and COLUMN_NAME='DESCRIPTION') 
begin
  alter table FORWARDINGRULE ADD 
    DESCRIPTION nvarchar(255) NULL
end
GO

if not exists(select * from INFORMATION_SCHEMA.COLUMNS where TABLE_NAME='FORWARDINGRULE' and COLUMN_NAME='RULENUMBERTYPE') 
begin
  alter table FORWARDINGRULE ADD 
    RULENUMBERTYPE int NULL
end
GO

if not exists(select * from INFORMATION_SCHEMA.COLUMNS where TABLE_NAME='FORWARDINGRULE' and COLUMN_NAME='RULENUMBERFIXED') 
begin
  alter table FORWARDINGRULE ADD 
    RULENUMBERFIXED nvarchar(50) NULL
end
GO

if not exists(select * from INFORMATION_SCHEMA.COLUMNS where TABLE_NAME='FORWARDINGRULE' and COLUMN_NAME='RULENUMBERFIELD') 
begin
  alter table FORWARDINGRULE ADD 
    RULENUMBERFIELD nvarchar(50) NULL
end
GO

if not exists(select * from INFORMATION_SCHEMA.COLUMNS where TABLE_NAME='FORWARDINGRULE' and COLUMN_NAME='FILTERTYPE') 
begin
  alter table FORWARDINGRULE ADD 
    FILTERTYPE int NULL
end
GO

if not exists(select * from INFORMATION_SCHEMA.COLUMNS where TABLE_NAME='FORWARDINGRULE' and COLUMN_NAME='FIELDNAME') 
begin
  alter table FORWARDINGRULE ADD 
    FIELDNAME nvarchar(50) NULL
end
GO

if not exists(select * from INFORMATION_SCHEMA.COLUMNS where TABLE_NAME='FORWARDINGRULE' and COLUMN_NAME='FIELDVALUE') 
begin
  alter table FORWARDINGRULE ADD 
    FIELDVALUE nvarchar(50) NULL
end
GO

if not exists(select * from INFORMATION_SCHEMA.COLUMNS where TABLE_NAME='FORWARDINGRULE' and COLUMN_NAME='CUSTOMFIELDID') 
begin
  alter table FORWARDINGRULE ADD 
    CUSTOMFIELDID int NULL
end
GO

if not exists(select * from INFORMATION_SCHEMA.COLUMNS where TABLE_NAME='FORWARDINGRULE' and COLUMN_NAME='CUSTOMFIELDVALUEID') 
begin
  alter table FORWARDINGRULE ADD 
    CUSTOMFIELDVALUEID int NULL
end
GO

if not exists(select * from INFORMATION_SCHEMA.COLUMNS where TABLE_NAME='STATEFINISH' and COLUMN_NAME='INVALIDATED') 
begin
  alter table STATEFINISH ADD 
    INVALIDATED int NOT NULL Default 0
end
GO

if not exists(select * from INFORMATION_SCHEMA.COLUMNS where TABLE_NAME='RESOURCE' and COLUMN_NAME='ADUSERGUID') 
begin
  alter table RESOURCE ADD 
    ADUSERGUID nvarchar(max)
end
GO

if not exists(select * from INFORMATION_SCHEMA.COLUMNS where TABLE_NAME='RESOURCE' and COLUMN_NAME='OSOUSERID') 
begin
  alter table RESOURCE ADD 
    OSOUSERID nvarchar(max)
end
GO

if not exists(select * from INFORMATION_SCHEMA.COLUMNS where TABLE_NAME='RESOURCE' and COLUMN_NAME='USERKEYID') 
begin
  alter table RESOURCE ADD 
    USERKEYID nvarchar(max)
end
GO

if not exists(select * from INFORMATION_SCHEMA.COLUMNS where TABLE_NAME='RESOURCE' and COLUMN_NAME='BROADSOFTUSERID') 
begin
  alter table RESOURCE ADD 
    BROADSOFTUSERID nvarchar(max)
end
GO

if exists(select * from INFORMATION_SCHEMA.COLUMNS where TABLE_NAME='PROVIDERVALUE' and COLUMN_NAME='PARAMVALUE') 
begin
  alter table PROVIDERVALUE ALTER COLUMN
    PARAMVALUE nvarchar(max)
end
GO

if not exists(select * from INFORMATION_SCHEMA.COLUMNS where TABLE_NAME='CUSTOMFIELD' and COLUMN_NAME='VISIBLEINCLIENT') 
begin
  alter table CUSTOMFIELD ADD 
    VISIBLEINCLIENT int NOT NULL Default 1
end
GO

if not exists(select * from INFORMATION_SCHEMA.COLUMNS where TABLE_NAME='CUSTOMFIELD' and COLUMN_NAME='USEASDIVISION') 
begin
  alter table CUSTOMFIELD ADD USEASDIVISION int NOT NULL Default -1
end
GO

if not exists(select * from INFORMATION_SCHEMA.COLUMNS where TABLE_NAME='CUSTOMFIELD' and COLUMN_NAME='USEASCAPABILITY') 
begin
  alter table CUSTOMFIELD ADD USEASCAPABILITY int NOT NULL Default -1
end
GO

if not exists(select * from INFORMATION_SCHEMA.COLUMNS where TABLE_NAME='CUSTOMFIELD' and COLUMN_NAME='USEINTIME') 
begin
  alter table CUSTOMFIELD ADD USEINTIME int NOT NULL Default 1
end
GO

if not exists(select * from INFORMATION_SCHEMA.COLUMNS where TABLE_NAME='PROFILE' and COLUMN_NAME='QUICKSTATEORDER') 
begin
  alter table PROFILE ADD 
    QUICKSTATEORDER int NOT NULL Default 0
end
GO

if not exists(select * from INFORMATION_SCHEMA.COLUMNS where TABLE_NAME='PROFILE' and COLUMN_NAME='QUICKSTATEACTIVE') 
begin
  alter table PROFILE ADD 
    QUICKSTATEACTIVE int NOT NULL Default 0
end
GO

if not exists(select * from INFORMATION_SCHEMA.COLUMNS where TABLE_NAME='PROFILE' and COLUMN_NAME='QUICKSTATESHOWSTATEDIALOG') 
begin
  alter table PROFILE ADD 
    QUICKSTATESHOWSTATEDIALOG int NOT NULL Default 0
end
GO

if not exists(select * from INFORMATION_SCHEMA.COLUMNS where TABLE_NAME='PROFILE' and COLUMN_NAME='TASKBARORDER') 
begin
  alter table PROFILE ADD 
    TASKBARORDER int NOT NULL Default 0
end
GO

if not exists(select * from INFORMATION_SCHEMA.COLUMNS where TABLE_NAME='PROFILE' and COLUMN_NAME='TASKBARACTIVE') 
begin
  alter table PROFILE ADD 
    TASKBARACTIVE int NOT NULL Default 0
end
GO

if exists(select * from INFORMATION_SCHEMA.COLUMNS where TABLE_NAME='RESOURCE' and COLUMN_NAME='CALENDARID') 
begin
  alter table RESOURCE ALTER COLUMN
    CALENDARID nvarchar(max)
end
GO

if not exists(select * from INFORMATION_SCHEMA.COLUMNS where TABLE_NAME='CALLSTATISTIC' and COLUMN_NAME='CALLEDDID') 
begin
  alter table CALLSTATISTIC ADD CALLEDDID varchar(50)
end
GO

if not exists(select * from INFORMATION_SCHEMA.COLUMNS where TABLE_NAME='CALLSTATISTIC' and COLUMN_NAME='ANSWEREDBYVM') 
begin
  alter table CALLSTATISTIC ADD ANSWEREDBYVM int NOT NULL default 0
end
GO

if not exists(select * from INFORMATION_SCHEMA.COLUMNS where TABLE_NAME='PROFILE' and COLUMN_NAME='CALLERIDDEVICETYPE')
begin
  alter table PROFILE ADD CALLERIDDEVICETYPE integer not null default(0)     
end
GO

if not exists(select * from INFORMATION_SCHEMA.COLUMNS where TABLE_NAME='PROFILE' and COLUMN_NAME='CALLERIDNUMBER')
begin
  alter table PROFILE ADD CALLERIDNUMBER varchar(50)
end
GO

if not exists(select * from INFORMATION_SCHEMA.COLUMNS where TABLE_NAME='PROFILE' and COLUMN_NAME='NEXTCALLERIDDEVICETYPE')
begin
  alter table PROFILE ADD NEXTCALLERIDDEVICETYPE integer not null default(0)     
end
GO

if not exists(select * from INFORMATION_SCHEMA.COLUMNS where TABLE_NAME='PROFILE' and COLUMN_NAME='NEXTCALLERIDNUMBER')
begin
  alter table PROFILE ADD NEXTCALLERIDNUMBER varchar(50)
end
GO

if not exists(select * from INFORMATION_SCHEMA.COLUMNS where TABLE_NAME='FORWARDINGRULE' and COLUMN_NAME='CALLERIDDEVICETYPE')
begin
  alter table FORWARDINGRULE ADD CALLERIDDEVICETYPE integer not null default(0)     
  select 'Added CALLERIDDEVICETYPE to FORWARDINGRULE'
end
GO

if not exists(select * from INFORMATION_SCHEMA.COLUMNS where TABLE_NAME='FORWARDINGRULE' and COLUMN_NAME='CALLERIDNUMBER')
begin
  alter table FORWARDINGRULE ADD CALLERIDNUMBER varchar(50)
  select 'Added CALLERIDNUMBER to FORWARDINGRULE'
end
GO

if not exists(select * from INFORMATION_SCHEMA.COLUMNS where TABLE_NAME='RESOURCEDETAIL' and COLUMN_NAME='CALLERIDTARGET') 
begin
  alter table RESOURCEDETAIL ADD 
    CALLERIDTARGET integer Not null Default 0
  select 'Added CALLERIDTARGET to RESOURCEDETAIL'
end
GO

if not exists(select * from INFORMATION_SCHEMA.COLUMNS where TABLE_NAME='RESERVATION' and COLUMN_NAME='STARTCALLCENTERSETTING') 
begin
  alter table RESERVATION ADD 
    STARTCALLCENTERSETTING nvarchar(max)
  select 'Added STARTCALLCENTERSETTING to RESERVATION'
end
GO

if not exists(select * from INFORMATION_SCHEMA.COLUMNS where TABLE_NAME='RESERVATION' and COLUMN_NAME='ENDCALLCENTERSETTING') 
begin
  alter table RESERVATION ADD 
    ENDCALLCENTERSETTING nvarchar(max)
  select 'Added ENDCALLCENTERSETTING to RESERVATION'
end
GO

if not exists(select * from INFORMATION_SCHEMA.COLUMNS where TABLE_NAME='PROFILE' and COLUMN_NAME='STARTCALLCENTERSETTING')
begin
  alter table PROFILE ADD
    STARTCALLCENTERSETTING nvarchar(max)
  select 'Added STARTCALLCENTERSETTING to PROFILE'
end
GO

if not exists(select * from INFORMATION_SCHEMA.COLUMNS where TABLE_NAME='PROFILE' and COLUMN_NAME='ENDCALLCENTERSETTING')
begin
  alter table PROFILE ADD
    ENDCALLCENTERSETTING nvarchar(max)
  select 'Added ENDCALLCENTERSETTING to PROFILE'
end
GO

if not exists(select * from INFORMATION_SCHEMA.COLUMNS where TABLE_NAME='FORWARDINGRULE' and COLUMN_NAME='CALLCENTERLOGINTYPE')
begin
  alter table FORWARDINGRULE ADD CALLCENTERLOGINTYPE integer not null default(0)     
  select 'Added CALLCENTERLOGINTYPE to FORWARDINGRULE'
end
GO

if not exists(select * from INFORMATION_SCHEMA.COLUMNS where TABLE_NAME='FORWARDINGRULE' and COLUMN_NAME='CALLCENTERLOGIN')
begin
  alter table FORWARDINGRULE ADD CALLCENTERLOGIN nvarchar(max)
  select 'Added CALLCENTERLOGIN to FORWARDINGRULE'
end
GO

if not exists(select * from INFORMATION_SCHEMA.COLUMNS where TABLE_NAME='FORWARDINGRULE' and COLUMN_NAME='CALLCENTERLOGOUTTYPE')
begin
  alter table FORWARDINGRULE ADD CALLCENTERLOGOUTTYPE integer not null default(0)     
  select 'Added CALLCENTERLOGOUTTYPE to FORWARDINGRULE'
end
GO

if not exists(select * from INFORMATION_SCHEMA.COLUMNS where TABLE_NAME='FORWARDINGRULE' and COLUMN_NAME='CALLCENTERLOGOUT')
begin
  alter table FORWARDINGRULE ADD CALLCENTERLOGOUT nvarchar(max)
  select 'Added CALLCENTERLOGOUT to FORWARDINGRULE'
end
GO

if not exists(select * from INFORMATION_SCHEMA.COLUMNS where TABLE_NAME='PROFILE' and COLUMN_NAME='CUSTOMFIELDFILTER')
begin
  alter table PROFILE ADD CUSTOMFIELDFILTER nvarchar(max)
  select 'Added CUSTOMFIELDFILTER to PROFILE'
end
GO

if not exists(select * from INFORMATION_SCHEMA.COLUMNS where TABLE_NAME='RESERVATION' and COLUMN_NAME='ENDATTACHMENT') 
begin
  alter table RESERVATION ADD 
    ENDATTACHMENT xml null
  select 'Added ENDATTACHMENT to RESERVATION'
end
GO

if not exists(select * from sys.identity_columns WHERE OBJECT_NAME(OBJECT_ID) = 'FORWARDINGRULE')
begin
  alter table FORWARDINGRULE drop constraint PK_FORWARDINGRULE

  ALTER TABLE FORWARDINGRULE
  DROP COLUMN RECID
  
  ALTER TABLE FORWARDINGRULE 
  ADD RECID int not null IDENTITY(1,1) CONSTRAINT PK_FORWARDINGRULE PRIMARY KEY

  select 'Created FORWARDINGRULE.RECID as identity column'
end

if not exists(select * from INFORMATION_SCHEMA.COLUMNS where TABLE_NAME='RESOURCE' and COLUMN_NAME='TENANTID')
begin
  alter table RESOURCE ADD TENANTID integer not null default(1)
  select 'Added TENANTID to RESOURCE'
  ALTER TABLE RESOURCE  WITH CHECK ADD  CONSTRAINT FK_TENANT_RESOURCE FOREIGN KEY(TENANTID)
  REFERENCES TENANT(RECID)
  select 'Added FK_TENANT_RESOURCE'
end
GO

if not exists(select * from INFORMATION_SCHEMA.COLUMNS where TABLE_NAME='STATE' and COLUMN_NAME='TENANTID')
begin
  alter table STATE ADD TENANTID integer not null default(1)
  select 'Added TENANTID to STATE'
  ALTER TABLE STATE  WITH CHECK ADD  CONSTRAINT FK_TENANT_STATE FOREIGN KEY(TENANTID)
  REFERENCES TENANT (RECID)
  select 'Added FK_TENANT_STATE'
end
GO

if not exists(select * from INFORMATION_SCHEMA.COLUMNS where TABLE_NAME='PROFILE' and COLUMN_NAME='TENANTID')
begin
  alter table PROFILE ADD TENANTID integer not null default(1)
  select 'Added TENANTID to PROFILE'
  ALTER TABLE PROFILE  WITH CHECK ADD  CONSTRAINT FK_TENANT_PROFILE FOREIGN KEY(TENANTID)
  REFERENCES TENANT (RECID)
  select 'Added FK_TENANT_PROFILE'
end
GO

if not exists(select * from INFORMATION_SCHEMA.COLUMNS where TABLE_NAME='PROVIDER' and COLUMN_NAME='TENANTID')
begin
  alter table PROVIDER ADD TENANTID integer not null default(1)
  select 'Added TENANTID to PROVIDER'
  ALTER TABLE PROVIDER  WITH CHECK ADD  CONSTRAINT FK_TENANT_PROVIDER FOREIGN KEY(TENANTID)
  REFERENCES TENANT (RECID)
  select 'Added FK_TENANT_PROVIDER'
end
GO

if not exists(select * from INFORMATION_SCHEMA.COLUMNS where TABLE_NAME='PBXDEVICE' and COLUMN_NAME='TENANTID')
begin
  alter table PBXDEVICE ADD TENANTID integer not null default(1)
  select 'Added TENANTID to PBXDEVICE'
  ALTER TABLE PBXDEVICE  WITH CHECK ADD  CONSTRAINT FK_TENANT_PBXDEVICE FOREIGN KEY(TENANTID)
  REFERENCES TENANT (RECID)
  select 'Added FK_TENANT_PBXDEVICE'
end
GO

if not exists(select * from INFORMATION_SCHEMA.COLUMNS where TABLE_NAME='CUSTOMFIELD' and COLUMN_NAME='TENANTID')
begin
  alter table CUSTOMFIELD ADD TENANTID integer not null default(1)
  select 'Added TENANTID to CUSTOMFIELD'
  ALTER TABLE CUSTOMFIELD  WITH CHECK ADD  CONSTRAINT FK_TENANT_CUSTOMFIELD FOREIGN KEY(TENANTID)
  REFERENCES TENANT (RECID)
  select 'Added FK_TENANT_CUSTOMFIELD'
end
GO

if not exists(select * from INFORMATION_SCHEMA.COLUMNS where TABLE_NAME='CLIENTSETTING' and COLUMN_NAME='TENANTID')
begin
  alter table CLIENTSETTING ADD TENANTID integer not null default(1)
  select 'Added TENANTID to CLIENTSETTING'
  ALTER TABLE CLIENTSETTING  WITH CHECK ADD  CONSTRAINT FK_TENANT_CLIENTSETTING FOREIGN KEY(TENANTID)
  REFERENCES TENANT (RECID)
  select 'Added FK_TENANT_CLIENTSETTING'
end
GO

if not exists(select * from INFORMATION_SCHEMA.COLUMNS where TABLE_NAME='RCLPHONE' and COLUMN_NAME='TENANTID')
begin
  alter table RCLPHONE ADD TENANTID integer not null default(1)
  select 'Added TENANTID to RCLPHONE'
  ALTER TABLE RCLPHONE  WITH CHECK ADD  CONSTRAINT FK_TENANT_RCLPHONE FOREIGN KEY(TENANTID)
  REFERENCES TENANT (RECID)
  select 'Added FK_TENANT_RCLPHONE'
end
GO

if not exists(select * from INFORMATION_SCHEMA.COLUMNS where TABLE_NAME='RCLCALLER' and COLUMN_NAME='TENANTID')
begin
  alter table RCLCALLER ADD TENANTID integer not null default(1)
  select 'Added TENANTID to RCLCALLER'
  ALTER TABLE RCLCALLER  WITH CHECK ADD  CONSTRAINT FK_TENANT_RCLCALLER FOREIGN KEY(TENANTID)
  REFERENCES TENANT (RECID)
  select 'Added FK_TENANT_RCLCALLER'
end
GO

if not exists(select * from INFORMATION_SCHEMA.COLUMNS where TABLE_NAME='CALLSTATISTIC' and COLUMN_NAME='TENANTID')
begin
  alter table CALLSTATISTIC ADD TENANTID integer not null default(1)
  select 'Added TENANTID to CALLSTATISTIC'
  ALTER TABLE CALLSTATISTIC  WITH CHECK ADD  CONSTRAINT FK_TENANT_CALLSTATISTIC FOREIGN KEY(TENANTID)
  REFERENCES TENANT (RECID)
  select 'Added FK_TENANT_CALLSTATISTIC'
end
GO

if not exists(select * from INFORMATION_SCHEMA.COLUMNS where TABLE_NAME='NUMBERLOG' and COLUMN_NAME='TENANTID')
begin
  alter table NUMBERLOG ADD TENANTID integer not null default(1)
  select 'Added TENANTID to NUMBERLOG'
  ALTER TABLE NUMBERLOG  WITH CHECK ADD  CONSTRAINT FK_TENANT_NUMBERLOG FOREIGN KEY(TENANTID)
  REFERENCES TENANT (RECID)
  select 'Added FK_TENANT_NUMBERLOG'
end
GO

if not exists(select * from INFORMATION_SCHEMA.COLUMNS where TABLE_NAME='PHONEBOOK' and COLUMN_NAME='TENANTID')
begin
  alter table PHONEBOOK ADD TENANTID integer not null default(1)
  select 'Added TENANTID to PHONEBOOK'
  ALTER TABLE PHONEBOOK  WITH CHECK ADD  CONSTRAINT FK_TENANT_PHONEBOOK FOREIGN KEY(TENANTID)
  REFERENCES TENANT (RECID)
  select 'Added FK_TENANT_PHONEBOOK'
end
GO

if not exists(select * from INFORMATION_SCHEMA.COLUMNS where TABLE_NAME='FORWARDINGRULE' and COLUMN_NAME='TENANTID')
begin
  alter table FORWARDINGRULE ADD TENANTID integer not null default(1)
  select 'Added TENANTID to FORWARDINGRULE'
  ALTER TABLE FORWARDINGRULE  WITH CHECK ADD  CONSTRAINT FK_TENANT_FORWARDINGRULE FOREIGN KEY(TENANTID)
  REFERENCES TENANT (RECID)
  select 'Added FK_TENANT_FORWARDINGRULE'
end
GO

if not exists(select * from INFORMATION_SCHEMA.COLUMNS where TABLE_NAME='PROFILE' and COLUMN_NAME='ACTIVATEFROMDOORWAY')
begin
  alter table PROFILE ADD ACTIVATEFROMDOORWAY integer not null default(1)     
  Exec('update PROFILE set ACTIVATEFROMDOORWAY = ACTIVATEFROMSMARTCLIENT')
  select 'Added ACTIVATEFROMDOORWAY to PROFILE'
end
GO

if not exists(select * from INFORMATION_SCHEMA.COLUMNS where TABLE_NAME='STATE' and COLUMN_NAME='ACTIVATEFROMDOORWAY')
begin
  alter table STATE ADD ACTIVATEFROMDOORWAY integer not null default(1)
  Exec('update STATE set ACTIVATEFROMDOORWAY = ACTIVATEFROMSMARTCLIENT')
  select 'Added ACTIVATEFROMDOORWAY to STATE'
end
GO

if not exists(select * from INFORMATION_SCHEMA.COLUMNS where TABLE_NAME='RESOURCE' and COLUMN_NAME='PUREADMIN') 
begin
  alter table RESOURCE ADD PUREADMIN integer Not null Default 0
  select 'Added PUREADMIN to RESOURCE'
end
GO

if not exists(select * from INFORMATION_SCHEMA.COLUMNS where TABLE_NAME='STATE' and COLUMN_NAME='ACTIVATIONOVERRIDES')
begin
  alter table STATE ADD ACTIVATIONOVERRIDES nvarchar(max)
  select 'Added ACTIVATIONOVERRIDES to STATE'
end
GO

if not exists(select * from INFORMATION_SCHEMA.COLUMNS where TABLE_NAME='RESOURCE' and COLUMN_NAME='CALENDARSUBJECTPRIVATE') 
begin
  alter table RESOURCE ADD CALENDARSUBJECTPRIVATE integer Not null Default 0
  select 'Added CALENDARSUBJECTPRIVATE to RESOURCE'
end
GO

if not exists(select * from INFORMATION_SCHEMA.COLUMNS where TABLE_NAME='PROVIDER' and COLUMN_NAME='AUTOUPDATE')
begin
  alter table PROVIDER ADD AUTOUPDATE integer not null default(0)
  select 'Added AUTOUPDATE to PROVIDER'
end
GO

-----------------------------------------------------------------------------------------------------
-----------------------------------------------------------------------------------------------------
-- Indexes
-----------------------------------------------------------------------------------------------------
-----------------------------------------------------------------------------------------------------

if (select COUNT(*) from sys.indexes where name = 'IX_RESERVATIONDETAIL_RESERVATIONID') = 0
begin
  CREATE NONCLUSTERED INDEX [IX_RESERVATIONDETAIL_RESERVATIONID] ON [dbo].[RESERVATIONDETAIL] 
  (
	  [RESERVATIONID] ASC
  ) WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]  
end
GO

if not exists(select * from sys.indexes where name='IX_CHANGELOG_ACT_CTIME' and object_id = OBJECT_ID('CHANGELOG')) 
begin
  CREATE NONCLUSTERED INDEX [IX_CHANGELOG_ACT_CTIME] ON [dbo].[CHANGELOG] 
  (
    [ACTION], 
    [CHANGETIME]
  ) 
  INCLUDE 
  (
    [RESOURCEID]
  );
  select 'Created index IX_CHANGELOG_ACT_CTIME';
end
GO

if not exists(select * from sys.indexes where name='IX_CHANGELOG_TYPE_ID_TIME' and object_id = OBJECT_ID('CHANGELOG')) 
begin
  CREATE NONCLUSTERED INDEX [IX_CHANGELOG_TYPE_ID_TIME] ON [dbo].[CHANGELOG] 
  (
    [CHANGETYPE], 
    [RECID], 
    [CHANGETIME]
  ) 
  INCLUDE 
  (
    [RESOURCEID]
  );
  select 'Created index IX_CHANGELOG_TYPE_ID_TIME';
end
GO

if not exists(select * from sys.indexes where name='IX_RESERVATION_DEL_HIS_RID_STEND' and object_id = OBJECT_ID('RESERVATION')) 
begin
    CREATE NONCLUSTERED INDEX [IX_RESERVATION_DEL_HIS_RID_STEND] ON [dbo].[RESERVATION] 
    (
        [DELETED], 
        [HISTORY], 
        [RESOURCEID], 
        [STATEEND]
    ) 
    INCLUDE 
    (
        [STATEID], 
        [STATESTART]
    );
  select 'Created index IX_RESERVATION_DEL_HIS_RID_STEND';
end
GO

if not exists(select * from sys.indexes where name='IX_RESERVATION_RESOURCEID_SS_A' and object_id = OBJECT_ID('RESERVATION')) 
begin
    CREATE NONCLUSTERED INDEX [IX_RESERVATION_RESOURCEID_SS_A] ON [dbo].[RESERVATION] 
    (
        [RESOURCEID]
    )
    INCLUDE 
    (
        [STATESTART],
        [ATTACHMENT]
    );
    SELECT 'Created index IX_RESERVATION_RESOURCEID_SS_A';
end
GO

if not exists(select * from sys.indexes where name='IX_RESERVATION_DEL_STA' and object_id = OBJECT_ID('RESERVATION')) 
begin
    CREATE NONCLUSTERED INDEX [IX_RESERVATION_DEL_STA] ON [dbo].[RESERVATION] 
    (
        [DELETED], 
        [STATEEND]
    ) 
    INCLUDE 
    (
        [RECID]
    );
  select 'Created index IX_RESERVATION_DEL_STA';
end
GO

if not exists(select * from sys.indexes where name='IX_NUMBERLOG_LOGTYPE' and object_id = OBJECT_ID('NUMBERLOG')) 
begin
    CREATE NONCLUSTERED INDEX [IX_NUMBERLOG_LOGTYPE] ON [dbo].[NUMBERLOG] 
    (
        [LOGTYPE]
    ) 
    INCLUDE 
    (
        [DEVICEID], 
        [ENDTIME], 
        [PHONENUMBER], 
        [STARTTIME]
    );
  select 'Created index IX_NUMBERLOG_LOGTYPE';
end
GO

if not exists(select * from sys.indexes where name = 'IX_ATTACHMENTFIELD_PARENTID' and object_id = OBJECT_ID('ATTACHMENTFIELD'))
begin
	CREATE NONCLUSTERED INDEX [IX_ATTACHMENTFIELD_PARENTID] ON [dbo].[ATTACHMENTFIELD]
	(
		[PARENTID] ASC
	)
	INCLUDE
	(
		[REGISTRATIONID]
	)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
	select 'Created index IX_ATTACHMENTFIELD_PARENTID';
end
GO

if exists(select * from sys.indexes where name='IX_PHONE' and object_id = OBJECT_ID('RCLPHONE'))
begin
    ALTER TABLE RCLPHONE DROP CONSTRAINT IX_PHONE
	select 'Dropped constraint IX_PHONE on table RCLPHONE'
end
GO

if not exists(select * from sys.indexes where name='IX_TENANT_PHONE' and object_id = OBJECT_ID('RCLPHONE'))
begin
    ALTER TABLE RCLPHONE ADD CONSTRAINT [IX_TENANT_PHONE] UNIQUE NONCLUSTERED ([TENANTID],[EXTERNALNO] ASC) WITH (IGNORE_DUP_KEY = OFF) ON [PRIMARY]
	select 'Added constraint IX_TENANT_PHONE on table RCLPHONE'
end
GO

if not exists(select * from sys.indexes where name='IX_PHONEBOOK_TENANTID' and object_id = OBJECT_ID('PHONEBOOK'))
begin
    CREATE NONCLUSTERED INDEX [IX_PHONEBOOK_TENANTID] ON [dbo].[PHONEBOOK] 
    (
	    [TENANTID] ASC
    ) WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]  
    select 'Created index IX_PHONEBOOK_TENANTID'
end
GO

if not exists(select * from sys.indexes where name='IX_PHONEBOOK_USRLVL_TENID' and object_id = OBJECT_ID('PHONEBOOK'))
begin
    CREATE NONCLUSTERED INDEX [IX_PHONEBOOK_USRLVL_TENID] ON [dbo].[PHONEBOOK] 
    (
	    [USERLEVEL],
      [TENANTID]
    )
    INCLUDE (
      [NUMBER], 
      [NAME]
    );
    select 'Created index IX_PHONEBOOK_USRLVL_TENID'
end
GO

-----------------------------------------------------------------------------------------------------
-----------------------------------------------------------------------------------------------------
-- Create views
-----------------------------------------------------------------------------------------------------
-----------------------------------------------------------------------------------------------------
-- General Phonebook
exec sp_CreateOrReplaceView 
	'VI_PHONEBOOKSTANDARD',
	N'select RECID, NUMBER, NAME, ADRESS, USERLEVEL, OWNER, TENANTID
	  from PHONEBOOK
	  where USERLEVEL=0'
GO

-- UserLevel Phonebook
exec sp_CreateOrReplaceView
	'VI_PHONEBOOKUSER',
	N'select RECID, NUMBER, NAME, ADRESS, USERLEVEL, OWNER, TENANTID
	  from PHONEBOOK
	  where USERLEVEL=1'
GO

-- All Phonebook entries
exec sp_CreateOrReplaceView 
	'VI_PHONEBOOK',
	N'select RECID, NUMBER, NAME, ADRESS, USERLEVEL, OWNER, TENANTID
	  from VI_PHONEBOOKUSER
	  union all
	  select RECID, NUMBER, NAME, ADRESS, USERLEVEL, OWNER, TENANTID
	  from VI_PHONEBOOKSTANDARD'
GO

-- Front end for CallStatistics. Used by e.g. Effo Jet Reports
exec sp_CreateOrReplaceView 
	'VI_CALLSTATISTIC',
	N'SELECT        RECID, CALLDIRECTION, CALLLENGTH, CREATEDTIME, STARTTIME, ENDTIME, INITIATINGDEVICE, CALLINGDEVICE, CALLEDDEVICE, CALLEDDEVICERESERVATIONID, CALLEDDEVICESTATEID, 
                         CALLEDDEVICECOMSTATE, EXTERNALDEVICETYPE, NOOFESTABLISHED, FIRSTQUEUETIME, FIRSTQUEUEDEVICE, FIRSTDELIVEREDTIME, FIRSTDELIVEREDDEVICE, FIRSTESTABLISHEDTIME, 
                         FIRSTESTABLISHEDDEVICE, SECONDESTABLISHEDTIME, SECONDESTABLISHEDDEVICE, LASTESTABLISHEDTIME, LASTESTABLISHEDDEVICE, LASTTBESTABLISHEDDEVICE, LASTTRANSFERREDTIME, 
                         LASTTRANSFERRINGDEVICE, LASTTRANSFERREDTODEVICE, LASTTRANSFERREDTORESERVATIONID, LASTTRANSFERREDTOSTATEID, LASTTRANSFERREDTOCOMSTATE, FAILEDCAUSE, CALLENDED, GID, 
                         DATEPART(HOUR, STARTTIME) AS STARTHOUR
	  from CALLSTATISTIC'
GO
-----------------------------------------------------------------------------------------------------
-----------------------------------------------------------------------------------------------------
-- Call Statistics Functions
-----------------------------------------------------------------------------------------------------
-----------------------------------------------------------------------------------------------------

if exists(select * from sys.objects where object_id=OBJECT_ID(N'[dbo].[SPTV_CallStatistics_CustomDatePart]') and type in (N'FN'))
  drop function SPTV_CallStatistics_CustomDatePart
GO


CREATE FUNCTION [dbo].[SPTV_CallStatistics_CustomDatePart]
(
	@DatePart varchar(10),
	@DateTime datetime
)
RETURNS int
AS
BEGIN

	IF @DatePart = 'DW'
		RETURN DatePart(dw, @DateTime);
	
	IF @DatePart = 'HH'
		RETURN DatePart(hh, @DateTime);

	IF @DatePart = 'WK'
		RETURN DatePart(wk, @DateTime);

	IF @DatePart = 'MM'
		RETURN DatePart(mm, @DateTime);

	IF @DatePart = 'QQ'
		RETURN DatePart(qq, @DateTime);

	IF @DatePart = 'YY'
		RETURN DatePart(yy, @DateTime);

	RETURN 0;
END
GO

if exists(select * from sys.objects where object_id=OBJECT_ID(N'[dbo].[SPTV_CallStatistics_GetExcludes]') and type in (N'TF'))
  drop function SPTV_CallStatistics_GetExcludes
GO

CREATE FUNCTION [dbo].[SPTV_CallStatistics_GetExcludes]
(
)
RETURNS 
@Table TABLE 
(
	Column2 varchar(8000)
)
AS
BEGIN
	DECLARE @CALLSTATISTICSSWITCHBOARDS nvarchar(max)
	SET @CALLSTATISTICSSWITCHBOARDS = 
		(
		select TOP 1 PARAMVALUE from PROVIDERPARAMETER pp, PROVIDERVALUE pv
		where
			pp.PARAMSHORTNAME = 'CALLSTATISTICSEXCLUDES'
			and
			pv.PARAMETERID = pp.RECID
		)
	
	INSERT INTO @Table SELECT * FROM dbo.SPTV_CallStatistics_Split(@CALLSTATISTICSSWITCHBOARDS, ',')

	RETURN;
END
GO

if exists(select * from sys.objects where object_id=OBJECT_ID(N'[dbo].[SPTV_CallStatistics_BasicData]') and type in (N'IF'))
  drop function SPTV_CallStatistics_BasicData
GO

CREATE FUNCTION [dbo].[SPTV_CallStatistics_BasicData]
(	
	@DateStart datetime,
	@DateEnd datetime,
	@CallDirection varchar(1000),
	@IncludeMondays bit,
	@IncludeTuesdays bit,
	@IncludeWednesdays bit,
	@IncludeThursdays bit,
	@IncludeFridays bit,
	@IncludeSaturdays bit,
	@IncludeSundays bit,
	@HourStart int,
	@MinuteStart int,
	@HourEnd int,
	@MinuteEnd int
)
RETURNS TABLE 
AS
RETURN 
(
	-- Add the SELECT statement with parameter references here
	SELECT *
	FROM [CALLSTATISTIC]
	WHERE
		(
			CALLINGDEVICE != ''
			OR
			CALLEDDEVICE != ''
		)
		and
		[CALLLENGTH] >= 
			COALESCE((select pv.PARAMVALUE from PROVIDERPARAMETER pp, PROVIDERVALUE pv
			where pp.PARAMSHORTNAME = 'CALLSTATISTICSMINCALLLENGTH'
			and pv.PARAMETERID = pp.RECID), 5)
		and
		(
			[CALLDIRECTION] = @CallDirection
			OR
			(
				@CallDirection = 'CustomerService'
				AND
				(
					[CALLDIRECTION] = 'tsdtIncomingExternal'
					AND
					(
						[EXTERNALDEVICETYPE] IS NULL
						OR
						[EXTERNALDEVICETYPE] != 'tsedtCompanyCalling'
					)
				)
			)
			OR
			(
				@CallDirection = 'InternalService'
				AND
				(
					[CALLDIRECTION] = 'tsdtInternal'
					OR
					(
						[CALLDIRECTION] = 'tsdtIncomingExternal'
						AND
						[EXTERNALDEVICETYPE] = 'tsedtCompanyCalling'
					)
					OR
					(
						[CALLDIRECTION] = 'tsdtOutgoingExternal'
						AND
						[EXTERNALDEVICETYPE] = 'tsedtCompanyCalled'
					)
				)
			)
			OR
			(
				@CallDirection = 'ResourcesIncoming'
				AND
				(
					[CALLDIRECTION] = 'tsdtIncomingExternal'
					OR
					[CALLDIRECTION] = 'tsdtInternal'
					OR
					(
						[CALLDIRECTION] = 'tsdtOutgoingExternal'
						AND
						[EXTERNALDEVICETYPE] = 'tsedtCompanyCalled'
					)
				)
			)
			OR
			(
				@CallDirection = 'ResourcesOutgoing'
				AND
				(
					[CALLDIRECTION] = 'tsdtOutgoingExternal'
					OR
					[CALLDIRECTION] = 'tsdtInternal'
					OR
					(
						[CALLDIRECTION] = 'tsdtIncomingExternal'
						AND
						[EXTERNALDEVICETYPE] = 'tsedtCompanyCalling'
					)
				)
			)
		)
		and
		[STARTTIME] >= @DateStart
		and
		[STARTTIME] < DATEADD(DD, 1, @DateEnd)
		and
		(
			@IncludeMondays = 1
			or
			DATEPART(DW, [STARTTIME]) != 2
		)
		and
		(
			@IncludeTuesdays = 1
			or
			DATEPART(DW, [STARTTIME]) != 3
		)
		and
		(
			@IncludeWednesdays = 1
			or
			DATEPART(DW, [STARTTIME]) != 4
		)
		and
		(
			@IncludeThursdays = 1
			or
			DATEPART(DW, [STARTTIME]) != 5
		)
		and
		(
			@IncludeFridays = 1
			or
			DATEPART(DW, [STARTTIME]) != 6
		)
		and
		(
			@IncludeSaturdays = 1
			or
			DATEPART(DW, [STARTTIME]) != 7
		)
		and
		(
			@IncludeSundays = 1
			or
			DATEPART(DW, [STARTTIME]) != 1
		)
		and
		(
			@HourStart = 0
			or
			(
				DATEPART(HH, [STARTTIME]) > @HourStart
				or
				(
					DATEPART(HH, [STARTTIME]) = @HourStart
					and
					DATEPART(MI, [STARTTIME]) >= @MinuteStart
				)
			)
		)
		and
		(
			@HourEnd = 0
			or
			(
				DATEPART(HH, [STARTTIME]) < @HourEnd
				or
				(
					DATEPART(HH, [STARTTIME]) = @HourEnd
					and
					DATEPART(MI, [STARTTIME]) < @MinuteEnd
				)
			)
		)
		AND
		[CALLEDDEVICE] COLLATE DATABASE_DEFAULT NOT IN
		(
			select * from SPTV_CallStatistics_GetExcludes()
		)
)
GO

if exists(select * from sys.objects where object_id=OBJECT_ID(N'[dbo].[SPTV_CallStatistics_Split]') and type in (N'TF'))
  drop function SPTV_CallStatistics_Split
GO

CREATE FUNCTION [dbo].[SPTV_CallStatistics_Split](@String varchar(8000), @Delimiter char(1))       
returns @temptable TABLE (items varchar(8000))       
as       
begin       
    declare @idx int       
    declare @slice varchar(8000)       

    select @idx = 1       
        if len(@String)<1 or @String is null  return       

    while @idx!= 0       
    begin       
        set @idx = charindex(@Delimiter,@String)       
        if @idx!=0       
            set @slice = left(@String,@idx - 1)       
        else       
            set @slice = @String       

        if(len(@slice)>0)  
            insert into @temptable(Items) values(@slice)       

        set @String = right(@String,len(@String) - @idx)       
        if len(@String) = 0 break       
    end   
return       
end
GO

-----------------------------------------------------------------------------------------------------
-----------------------------------------------------------------------------------------------------
-- Call Statistics Procedures
-----------------------------------------------------------------------------------------------------
-----------------------------------------------------------------------------------------------------

-- General procedures for retrieving values from CALLSTATISTICS table

if exists(select * from sys.objects where object_id=OBJECT_ID(N'[dbo].[SPTV_CallStatistics_Basic_Data]') and type in (N'P'))
  drop procedure [SPTV_CallStatistics_Basic_Data]
GO

CREATE PROCEDURE [dbo].[SPTV_CallStatistics_Basic_Data]
	@DateStart datetime,
	@DateEnd datetime,
	@CallDirection varchar(1000), 
	@IncludeMondays bit,
	@IncludeTuesdays bit,
	@IncludeWednesdays bit,
	@IncludeThursdays bit,
	@IncludeFridays bit,
	@IncludeSaturdays bit,
	@IncludeSundays bit,
	@HourStart int,
	@MinuteStart int,
	@HourEnd int,
	@MinuteEnd int,
	@Details bit = false
AS
BEGIN
       -- SET NOCOUNT ON added to prevent extra result sets from
       -- interfering with SELECT statements.
       SET NOCOUNT ON;

       CREATE TABLE #TempTable
       (
              [RECID] int,
              [STARTTIME] datetime,
              [CALLINGDEVICE] varchar(50),
              [CALLEDDEVICE] varchar(50),
              [FIRSTQUEUEDEVICE] varchar(50),
              [FIRSTDELIVEREDDEVICE] varchar(50),
              [NOOFESTABLISHED] int,
              [CALLLENGTH] int,
              [LASTTRANSFERREDTIME] int,
              [FIRSTESTABLISHEDDEVICE] varchar(50),
              [FIRSTESTABLISHEDTIME] int,
              [SECONDESTABLISHEDDEVICE] varchar(50),
              [SECONDESTABLISHEDTIME] int,
              [LASTTRANSFERREDTODEVICE] varchar(50),
              [LASTESTABLISHEDDEVICE] varchar(50)
       )

       if (@Details = 0)
	   begin
		   SELECT [RECID], [STARTTIME], [CALLINGDEVICE], [CALLEDDEVICE], [FIRSTQUEUEDEVICE], [FIRSTDELIVEREDDEVICE], [NOOFESTABLISHED], [CALLLENGTH], [LASTTRANSFERREDTIME], [FIRSTESTABLISHEDDEVICE], [FIRSTESTABLISHEDTIME], [SECONDESTABLISHEDDEVICE], [SECONDESTABLISHEDTIME], [LASTTRANSFERREDTODEVICE], [LASTESTABLISHEDDEVICE], [LASTESTABLISHEDTIME], [LASTTRANSFERRINGDEVICE], [LASTTRANSFERREDTOSTATEID], [LASTTRANSFERREDTOCOMSTATE], [CALLEDDEVICESTATEID], [CALLEDDEVICECOMSTATE]
		   FROM SPTV_CallStatistics_BasicData(@DateStart, @DateEnd, @CallDirection, @IncludeMondays, @IncludeTuesdays, @IncludeWednesdays, @IncludeThursdays, @IncludeFridays, @IncludeSaturdays, @IncludeSundays, @HourStart, @MinuteStart, @HourEnd, @MinuteEnd) BasicData
	   end
	   else
	   begin
	       SELECT *
		   FROM SPTV_CallStatistics_BasicData(@DateStart, @DateEnd, @CallDirection, @IncludeMondays, @IncludeTuesdays, @IncludeWednesdays, @IncludeThursdays, @IncludeFridays, @IncludeSaturdays, @IncludeSundays, @HourStart, @MinuteStart, @HourEnd, @MinuteEnd) BasicData
	   end
END

GO

-- RH 28.11.2016: No longer used START
if exists(select * from sys.objects where object_id=OBJECT_ID(N'[dbo].[SPTV_CallStatistics_Avg_Hangup_CallLength]') and type in (N'P'))
  drop procedure SPTV_CallStatistics_Avg_Hangup_CallLength
GO

if exists(select * from sys.objects where object_id=OBJECT_ID(N'[dbo].[SPTV_CallStatistics_Calls_DatePart]') and type in (N'P'))
  drop procedure SPTV_CallStatistics_Calls_DatePart
GO

if exists(select * from sys.objects where object_id=OBJECT_ID(N'[dbo].[SPTV_CallStatistics_Calls_Days]') and type in (N'P'))
  drop procedure SPTV_CallStatistics_Calls_Days
GO

if exists(select * from sys.objects where object_id=OBJECT_ID(N'[dbo].[SPTV_CallStatistics_Calls_States]') and type in (N'P'))
  drop procedure SPTV_CallStatistics_Calls_States
GO

if exists(select * from sys.objects where object_id=OBJECT_ID(N'[dbo].[SPTV_CallStatistics_MostCalls]') and type in (N'P'))
  drop procedure SPTV_CallStatistics_MostCalls
GO

if exists(select * from sys.objects where object_id=OBJECT_ID(N'[dbo].[SPTV_CallStatistics_MostCallsOutgoing]') and type in (N'P'))
  drop procedure SPTV_CallStatistics_MostCallsOutgoing
GO

if exists(select * from sys.objects where object_id=OBJECT_ID(N'[dbo].[SPTV_CallStatistics_MostUnanswered]') and type in (N'P'))
  drop procedure SPTV_CallStatistics_MostUnanswered
GO

if exists(select * from sys.objects where object_id=OBJECT_ID(N'[dbo].[SPTV_CallStatistics_Transfers_Days]') and type in (N'P'))
  drop procedure SPTV_CallStatistics_Transfers_Days
GO

if exists(select * from sys.objects where object_id=OBJECT_ID(N'[dbo].[SPTV_CallStatistics_Transfers_States]') and type in (N'P'))
  drop procedure SPTV_CallStatistics_Transfers_States
GO

if exists(select * from sys.objects where object_id=OBJECT_ID(N'[dbo].[SPTV_CallStatistics_GetSwitchBoards]') and type in (N'TF'))
  drop function SPTV_CallStatistics_GetSwitchBoards
GO
-- RH 28.11.2016: No longer used END

-- RH 18.11.2014: No longer used
if exists(select * from sys.objects where object_id=OBJECT_ID(N'[dbo].[SPTV_CallStatistics_GetCallStatisticsDaysToInclude]') and type in (N'P'))
  drop procedure SPTV_CallStatistics_GetCallStatisticsDaysToInclude
GO

-- RH 18.11.2014: No longer used
if exists(select * from sys.objects where object_id=OBJECT_ID(N'[dbo].[SPTV_CallStatistics_GetCallStatisticsEndTime]') and type in (N'P'))
  drop procedure SPTV_CallStatistics_GetCallStatisticsEndTime
GO

-- RH 18.11.2014: No longer used
if exists(select * from sys.objects where object_id=OBJECT_ID(N'[dbo].[SPTV_CallStatistics_GetCallStatisticsStartTime]') and type in (N'P'))
  drop procedure SPTV_CallStatistics_GetCallStatisticsStartTime
GO

if exists(select * from sys.objects where object_id=OBJECT_ID(N'[dbo].[SPTV_CallStatistics_GetCallStatisticsParameter]') and type in (N'P'))
  drop procedure SPTV_CallStatistics_GetCallStatisticsParameter
GO

CREATE PROCEDURE [dbo].[SPTV_CallStatistics_GetCallStatisticsParameter]
       @PARAMSHORTNAME nvarchar(50)
AS
BEGIN
       -- SET NOCOUNT ON added to prevent extra result sets from
       -- interfering with SELECT statements.
       SET NOCOUNT ON;

       select pv.PARAMVALUE from PROVIDERPARAMETER pp, PROVIDERVALUE pv
       where pp.PARAMSHORTNAME = @PARAMSHORTNAME
       and pv.PARAMETERID = pp.RECID
END
GO



if exists(select * from sys.objects where object_id=OBJECT_ID(N'[dbo].[SPTV_CallStatistics_GetPhoneNumbersFromResourceIDs]') and type in (N'P'))
  drop procedure SPTV_CallStatistics_GetPhoneNumbersFromResourceIDs
GO

CREATE PROCEDURE [dbo].[SPTV_CallStatistics_GetPhoneNumbersFromResourceIDs]
	-- Add the parameters for the stored procedure here
	@ResourceIDs varchar(max)
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

	select PARAMVALUE from RESOURCEDETAIL
	where
		(
			SUBTYPEENUM = 2
			OR
			SUBTYPEENUM = 5
		)
		and
		RESOURCEID in
		(
			select * from dbo.SPTV_CallStatistics_Split(@ResourceIDs, ',')
		)
END
GO

-----------------------------------------------------------------------------------------------------
-----------------------------------------------------------------------------------------------------
-- Update Values
-----------------------------------------------------------------------------------------------------
-----------------------------------------------------------------------------------------------------

--- update WCF connector default port to 8033
update PROVIDERPARAMETER set DEFAULTVALUE = 'localhost:8033'
where PARAMSHORTNAME = 'ENDPOINT'
and SUBTYPEID in (select RECID from PROVIDERSUBTYPE where TYPENAME = 'MobileWeb')
GO

-- subtype (LotusNotes)
if not exists(select RECID from PROVIDERSUBTYPE where CATEGORYENUM=3 and upper(TYPENAME)='LOTUSNOTES')
begin
  insert into PROVIDERSUBTYPE(CATEGORYENUM, TYPENAME) select RECID, 'LotusNotes' from PROVIDERCATEGORY where upper(CATEGORYNAME)='CALENDAR'
end
GO

-- parameters for LotusNotes
if not exists(select * from PROVIDERPARAMETER where SUBTYPEID = (select RECID from PROVIDERSUBTYPE where upper(TYPENAME)='LOTUSNOTES'))
begin
  -- parameters
  insert into PROVIDERPARAMETER(SUBTYPEID, PARAMSHORTNAME, PARAMNAME, DEFAULTVALUE, DESCRIPTION, SORTORDER) select RECID,'IP','IP Address','127.0.0.1','IP address of LotusNotes server', 0 from PROVIDERSUBTYPE where upper(TYPENAME)='LOTUSNOTES'
  insert into PROVIDERPARAMETER(SUBTYPEID, PARAMSHORTNAME, PARAMNAME, DEFAULTVALUE, DESCRIPTION, SORTORDER) select RECID,'USERNAME','Username','SUser','User with access to all calendars', 1 from PROVIDERSUBTYPE where upper(TYPENAME)='LOTUSNOTES'
  insert into PROVIDERPARAMETER(SUBTYPEID, PARAMSHORTNAME, PARAMNAME, DEFAULTVALUE, DESCRIPTION, SORTORDER) select RECID,'PASSWORD','Password','Pwd','Password for user with access to all calendars', 2 from PROVIDERSUBTYPE where upper(TYPENAME)='LOTUSNOTES'
  insert into PROVIDERPARAMETER(SUBTYPEID, PARAMSHORTNAME, PARAMNAME, DEFAULTVALUE, DESCRIPTION, SORTORDER) select RECID,'PLUSDAYS','Recurring Days Ahead','30','Days from now to show recurring appointments', 3 from PROVIDERSUBTYPE where upper(TYPENAME)='LOTUSNOTES'
  insert into PROVIDERPARAMETER(SUBTYPEID, PARAMSHORTNAME, PARAMNAME, DEFAULTVALUE, DESCRIPTION, SORTORDER) select RECID,'PINGINTERVAL','Ping Interval','60','Seconds between ping messages if no traffic is present', 4 from PROVIDERSUBTYPE where upper(TYPENAME)='LOTUSNOTES'
  insert into PROVIDERPARAMETER(SUBTYPEID, PARAMSHORTNAME, PARAMNAME, DEFAULTVALUE, DESCRIPTION, SORTORDER) select RECID,'PROMOTIONINTERVAL','Promotion Interval','60','Seconds between traversion appointmentlist', 5 from PROVIDERSUBTYPE where upper(TYPENAME)='LOTUSNOTES'
  insert into PROVIDERPARAMETER(SUBTYPEID, PARAMSHORTNAME, PARAMNAME, DEFAULTVALUE, DESCRIPTION, SORTORDER) select RECID,'BUSYCODE','Busy Template Shortname','T_BU','Template to use for busy LotusNotes appointments', 6 from PROVIDERSUBTYPE where upper(TYPENAME)='LOTUSNOTES'
  insert into PROVIDERPARAMETER(SUBTYPEID, PARAMSHORTNAME, PARAMNAME, DEFAULTVALUE, DESCRIPTION, SORTORDER) select RECID,'OOFCODE','Out of Office Template Shortname','T_OOF','Template to use for OutOfOffice LotusNotes appointments', 7 from PROVIDERSUBTYPE where upper(TYPENAME)='LOTUSNOTES'
end
GO

-- SCRAMBLED parameter for LotusNotes
-- first increase SORTORDER
if not exists(select * from PROVIDERPARAMETER where upper(PARAMSHORTNAME)='SCRAMBLED' and SUBTYPEID = (select RECID from PROVIDERSUBTYPE where upper(TYPENAME)='LOTUSNOTES'))
begin
  if exists(select RECID from PROVIDERPARAMETER where SUBTYPEID = (select RECID from PROVIDERSUBTYPE where upper(TYPENAME)='LOTUSNOTES') and SORTORDER=3)
  begin
    update PROVIDERPARAMETER
    set SORTORDER=SORTORDER+1
    where
      SUBTYPEID in (select RECID from PROVIDERSUBTYPE where upper(TYPENAME)='LOTUSNOTES') and
      SORTORDER>=3
  end
end
GO
if not exists(select * from PROVIDERPARAMETER where upper(PARAMSHORTNAME)='SCRAMBLED' and SUBTYPEID = (select RECID from PROVIDERSUBTYPE where upper(TYPENAME)='LOTUSNOTES'))
begin
  insert into PROVIDERPARAMETER(SUBTYPEID, PARAMSHORTNAME, PARAMNAME, DEFAULTVALUE, DESCRIPTION, SORTORDER) 
  select 
    ps.RECID,'SCRAMBLED','Password scrambled','0','Password scrambled using ''Totalview password scrambler''', 3
  from PROVIDERSUBTYPE ps
  left outer join PROVIDERPARAMETER pp on pp.SUBTYPEID=ps.RECID
  where upper(TYPENAME)='LOTUSNOTES'
  group by
    ps.RECID
end
GO

-- delete eeFrokost from Sonofon connector
if exists(select RECID from PROVIDERPARAMETER where PARAMSHORTNAME='eeFrokost' and SUBTYPEID=(select top 1 RECID from PROVIDERSUBTYPE where TYPENAME='Sonofon'))
begin
  delete from PROVIDERPARAMETER where PARAMSHORTNAME='eeFrokost' and SUBTYPEID=(select top 1 RECID from PROVIDERSUBTYPE where TYPENAME='Sonofon')
  delete from PROVIDERVALUE where PARAMETERID not in (select RECID from PROVIDERPARAMETER)
end
GO
-- delete eeMode from Sonofon connector
if exists(select RECID from PROVIDERPARAMETER where PARAMSHORTNAME='eeMode' and SUBTYPEID=(select top 1 RECID from PROVIDERSUBTYPE where TYPENAME='Sonofon'))
begin
  delete from PROVIDERPARAMETER where PARAMSHORTNAME='eeMode' and SUBTYPEID=(select top 1 RECID from PROVIDERSUBTYPE where TYPENAME='Sonofon')
  delete from PROVIDERVALUE where PARAMETERID not in (select RECID from PROVIDERPARAMETER)
end
GO
-- delete eeGaaetForDag from Sonofon connector
if exists(select RECID from PROVIDERPARAMETER where PARAMSHORTNAME='eeGaaetForDag' and SUBTYPEID=(select top 1 RECID from PROVIDERSUBTYPE where TYPENAME='Sonofon'))
begin
  delete from PROVIDERPARAMETER where PARAMSHORTNAME='eeGaaetForDag' and SUBTYPEID=(select top 1 RECID from PROVIDERSUBTYPE where TYPENAME='Sonofon')
  delete from PROVIDERVALUE where PARAMETERID not in (select RECID from PROVIDERPARAMETER)
end
GO
-- delete eeSyg from Sonofon connector
if exists(select RECID from PROVIDERPARAMETER where PARAMSHORTNAME='eeSyg' and SUBTYPEID=(select top 1 RECID from PROVIDERSUBTYPE where TYPENAME='Sonofon'))
begin
  delete from PROVIDERPARAMETER where PARAMSHORTNAME='eeSyg' and SUBTYPEID=(select top 1 RECID from PROVIDERSUBTYPE where TYPENAME='Sonofon')
  delete from PROVIDERVALUE where PARAMETERID not in (select RECID from PROVIDERPARAMETER)
end
GO
-- change default port value
if exists(select RECID from PROVIDERPARAMETER where PARAMSHORTNAME='port' and DEFAULTVALUE<>'80' and SUBTYPEID=(select top 1 RECID from PROVIDERSUBTYPE where TYPENAME='Sonofon'))
begin
  update PROVIDERPARAMETER set DEFAULTVALUE='80' where PARAMSHORTNAME='port' and SUBTYPEID=(select top 1 RECID from PROVIDERSUBTYPE where TYPENAME='Sonofon')
end
GO

-- SMS Connector. Add syntax20 parameter.
if not exists(select RECID from PROVIDERPARAMETER where PARAMSHORTNAME='syntax20' and SUBTYPEID=(select top 1 RECID from PROVIDERSUBTYPE where TYPENAME='SMS'))
begin
  insert into PROVIDERPARAMETER(SUBTYPEID, PARAMSHORTNAME, PARAMNAME, DEFAULTVALUE, DESCRIPTION, SORTORDER) select RECID,'syntax20','SMS Syntax 2.0','TRUE','Use the SMS syntax 2.0 scheme', 11 from PROVIDERSUBTYPE where TYPENAME='SMS'
end
GO

-- SMS Connector. Change Defaultvalue from "Com 1" to "1". Also change description.
if exists(select RECID from PROVIDERPARAMETER where PARAMSHORTNAME='COMPORT' and upper(DEFAULTVALUE)='COM 1' and SUBTYPEID=(select top 1 RECID from PROVIDERSUBTYPE where upper(TYPENAME)='SMS'))
begin
  update PROVIDERPARAMETER set
    DEFAULTVALUE='1',
    DESCRIPTION='GSM Station''''s COM port (1, 2...)'
  where
    PARAMSHORTNAME='COMPORT' and
    SUBTYPEID=(select top 1 RECID from PROVIDERSUBTYPE where upper(TYPENAME)='SMS')
end
GO


-- Server "Connector". Add CALLERHISTORYMAXRECORDS parameter.
if not exists(select RECID from PROVIDERPARAMETER where PARAMSHORTNAME='CALLERHISTORYMAXRECORDS' and SUBTYPEID=(select top 1 RECID from PROVIDERSUBTYPE where Upper(TYPENAME)='SERVER'))
begin
  insert into PROVIDERPARAMETER(SUBTYPEID, PARAMSHORTNAME, PARAMNAME, DEFAULTVALUE, DESCRIPTION, SORTORDER) 
  select 
    ps.RECID,'CALLERHISTORYMAXRECORDS','Returned History Records','15','Max number of caller history records', 
    max(pp.SORTORDER)+1 
  from PROVIDERSUBTYPE ps 
  left outer join PROVIDERPARAMETER pp on pp.SUBTYPEID=ps.RECID where upper(ps.TYPENAME)='SERVER' 
  group by 
    ps.RECID
end
GO
-- insert default CALLERHISTORYMAXRECORDS value into server connector
if not exists(select RECID from PROVIDERVALUE where PARAMETERID=(select RECID from PROVIDERPARAMETER where PARAMSHORTNAME='CALLERHISTORYMAXRECORDS' and SUBTYPEID=(select top 1 RECID from PROVIDERSUBTYPE where Upper(TYPENAME)='SERVER')))
begin
  insert into PROVIDERVALUE(PROVIDERID, PARAMETERID, PARAMVALUE)
  select
    p.RECID, pp.RECID, pp.DEFAULTVALUE 
  from
    PROVIDER p
  join PROVIDERSUBTYPE ps on ps.RECID=p.SUBTYPE and upper(ps.TYPENAME)='SERVER'
  join PROVIDERPARAMETER pp on pp.SUBTYPEID=p.SUBTYPE and PARAMSHORTNAME='CALLERHISTORYMAXRECORDS'
end
GO

-- Server "Connector". Add CALLERHISTORYVALIDDAYS parameter.
if not exists(select RECID from PROVIDERPARAMETER where PARAMSHORTNAME='CALLERHISTORYVALIDDAYS' and SUBTYPEID=(select top 1 RECID from PROVIDERSUBTYPE where Upper(TYPENAME)='SERVER'))
begin
  insert into PROVIDERPARAMETER(SUBTYPEID, PARAMSHORTNAME, PARAMNAME, DEFAULTVALUE, DESCRIPTION, SORTORDER) 
  select 
    ps.RECID,'CALLERHISTORYVALIDDAYS','Valid Days for History Records','-1','Number of days the caller history records are valid', 
    max(pp.SORTORDER)+1 
  from PROVIDERSUBTYPE ps 
  left outer join PROVIDERPARAMETER pp on pp.SUBTYPEID=ps.RECID where upper(ps.TYPENAME)='SERVER' 
  group by 
    ps.RECID
end
GO
-- insert default CALLERHISTORYVALIDDAYS value into server connector
if not exists(select RECID from PROVIDERVALUE where PARAMETERID=(select RECID from PROVIDERPARAMETER where PARAMSHORTNAME='CALLERHISTORYVALIDDAYS' and SUBTYPEID=(select top 1 RECID from PROVIDERSUBTYPE where Upper(TYPENAME)='SERVER')))
begin
  insert into PROVIDERVALUE(PROVIDERID, PARAMETERID, PARAMVALUE)
  select
    p.RECID, pp.RECID, pp.DEFAULTVALUE
  from
    PROVIDER p
  join PROVIDERSUBTYPE ps on ps.RECID=p.SUBTYPE and upper(ps.TYPENAME)='SERVER'
  join PROVIDERPARAMETER pp on pp.SUBTYPEID=p.SUBTYPE and PARAMSHORTNAME='CALLERHISTORYVALIDDAYS'
end
GO

-- Server "Connector". Add IMPORTUSERENABLED parameter.
if not exists(select RECID from PROVIDERPARAMETER where PARAMSHORTNAME='IMPORTUSERENABLED' and SUBTYPEID=(select top 1 RECID from PROVIDERSUBTYPE where Upper(TYPENAME)='SERVER'))
begin
  insert into PROVIDERPARAMETER(SUBTYPEID, PARAMSHORTNAME, PARAMNAME, DEFAULTVALUE, DESCRIPTION, SORTORDER) 
  select 
    ps.RECID,'IMPORTUSERENABLED','Enable scheduled import of users','0','Enable scheduled import of users (0=disabled, 1=enabled)', 
    max(pp.SORTORDER)+1 
  from PROVIDERSUBTYPE ps 
  left outer join PROVIDERPARAMETER pp on pp.SUBTYPEID=ps.RECID where upper(ps.TYPENAME)='SERVER' 
  group by 
    ps.RECID
end
GO
-- insert default IMPORTUSERENABLED value into server connector
if not exists(select RECID from PROVIDERVALUE where PARAMETERID=(select RECID from PROVIDERPARAMETER where PARAMSHORTNAME='IMPORTUSERENABLED' and SUBTYPEID=(select top 1 RECID from PROVIDERSUBTYPE where Upper(TYPENAME)='SERVER')))
begin
  insert into PROVIDERVALUE(PROVIDERID, PARAMETERID, PARAMVALUE)
  select
    p.RECID, pp.RECID, pp.DEFAULTVALUE
  from
    PROVIDER p
  join PROVIDERSUBTYPE ps on ps.RECID=p.SUBTYPE and upper(ps.TYPENAME)='SERVER'
  join PROVIDERPARAMETER pp on pp.SUBTYPEID=p.SUBTYPE and PARAMSHORTNAME='IMPORTUSERENABLED'
end
GO

-- Server "Connector". Add IMPORTUSEROCCURRENCE parameter.
if not exists(select RECID from PROVIDERPARAMETER where PARAMSHORTNAME='IMPORTUSEROCCURRENCE' and SUBTYPEID=(select top 1 RECID from PROVIDERSUBTYPE where Upper(TYPENAME)='SERVER'))
begin
  insert into PROVIDERPARAMETER(SUBTYPEID, PARAMSHORTNAME, PARAMNAME, DEFAULTVALUE, DESCRIPTION, SORTORDER) 
  select 
    ps.RECID,'IMPORTUSEROCCURRENCE','Frequency','0','Scheduled user import frequency (0=daily, 1=weekly)', 
    max(pp.SORTORDER)+1 
  from PROVIDERSUBTYPE ps 
  left outer join PROVIDERPARAMETER pp on pp.SUBTYPEID=ps.RECID where upper(ps.TYPENAME)='SERVER' 
  group by 
    ps.RECID
end
GO

-- insert default IMPORTUSEROCCURRENCE value into server connector
if not exists(select RECID from PROVIDERVALUE where PARAMETERID=(select RECID from PROVIDERPARAMETER where PARAMSHORTNAME='IMPORTUSEROCCURRENCE' and SUBTYPEID=(select top 1 RECID from PROVIDERSUBTYPE where Upper(TYPENAME)='SERVER')))
begin
  insert into PROVIDERVALUE(PROVIDERID, PARAMETERID, PARAMVALUE)
  select
    p.RECID, pp.RECID, pp.DEFAULTVALUE
  from
    PROVIDER p
  join PROVIDERSUBTYPE ps on ps.RECID=p.SUBTYPE and upper(ps.TYPENAME)='SERVER'
  join PROVIDERPARAMETER pp on pp.SUBTYPEID=p.SUBTYPE and PARAMSHORTNAME='IMPORTUSEROCCURRENCE'
end
GO

-- Server "Connector". Add IMPORTUSERDAY parameter.
if not exists(select RECID from PROVIDERPARAMETER where PARAMSHORTNAME='IMPORTUSERDAY' and SUBTYPEID=(select top 1 RECID from PROVIDERSUBTYPE where Upper(TYPENAME)='SERVER'))
begin
  insert into PROVIDERPARAMETER(SUBTYPEID, PARAMSHORTNAME, PARAMNAME, DEFAULTVALUE, DESCRIPTION, SORTORDER) 
  select 
    ps.RECID,'IMPORTUSERDAY','Day','0','Scheduled user import day for weekly import (0=sunday, 1=monday...)', 
    max(pp.SORTORDER)+1 
  from PROVIDERSUBTYPE ps 
  left outer join PROVIDERPARAMETER pp on pp.SUBTYPEID=ps.RECID where upper(ps.TYPENAME)='SERVER' 
  group by 
    ps.RECID
end
GO

-- insert default IMPORTUSERDAY value into server connector
if not exists(select RECID from PROVIDERVALUE where PARAMETERID=(select RECID from PROVIDERPARAMETER where PARAMSHORTNAME='IMPORTUSERDAY' and SUBTYPEID=(select top 1 RECID from PROVIDERSUBTYPE where Upper(TYPENAME)='SERVER')))
begin
  insert into PROVIDERVALUE(PROVIDERID, PARAMETERID, PARAMVALUE)
  select
    p.RECID, pp.RECID, pp.DEFAULTVALUE
  from
    PROVIDER p
  join PROVIDERSUBTYPE ps on ps.RECID=p.SUBTYPE and upper(ps.TYPENAME)='SERVER'
  join PROVIDERPARAMETER pp on pp.SUBTYPEID=p.SUBTYPE and PARAMSHORTNAME='IMPORTUSERDAY'
end
GO

-- Server "Connector". Add IMPORTUSERTIME parameter.
if not exists(select RECID from PROVIDERPARAMETER where PARAMSHORTNAME='IMPORTUSERTIME' and SUBTYPEID=(select top 1 RECID from PROVIDERSUBTYPE where Upper(TYPENAME)='SERVER'))
begin
  insert into PROVIDERPARAMETER(SUBTYPEID, PARAMSHORTNAME, PARAMNAME, DEFAULTVALUE, DESCRIPTION, SORTORDER) 
  select 
    ps.RECID,'IMPORTUSERTIME','Time','00.00','Time for scheduled user import (hh.mm)', 
    max(pp.SORTORDER)+1 
  from PROVIDERSUBTYPE ps 
  left outer join PROVIDERPARAMETER pp on pp.SUBTYPEID=ps.RECID where upper(ps.TYPENAME)='SERVER' 
  group by 
    ps.RECID
end
GO

-- insert default IMPORTUSERTIME value into server connector
if not exists(select RECID from PROVIDERVALUE where PARAMETERID=(select RECID from PROVIDERPARAMETER where PARAMSHORTNAME='IMPORTUSERTIME' and SUBTYPEID=(select top 1 RECID from PROVIDERSUBTYPE where Upper(TYPENAME)='SERVER')))
begin
  insert into PROVIDERVALUE(PROVIDERID, PARAMETERID, PARAMVALUE)
  select
    p.RECID, pp.RECID, pp.DEFAULTVALUE
  from
    PROVIDER p
  join PROVIDERSUBTYPE ps on ps.RECID=p.SUBTYPE and upper(ps.TYPENAME)='SERVER'
  join PROVIDERPARAMETER pp on pp.SUBTYPEID=p.SUBTYPE and PARAMSHORTNAME='IMPORTUSERTIME'
end
GO

-- Server "Connector". Add IMPORTUSERFILENAME parameter.
if not exists(select RECID from PROVIDERPARAMETER where PARAMSHORTNAME='IMPORTUSERFILENAME' and SUBTYPEID=(select top 1 RECID from PROVIDERSUBTYPE where Upper(TYPENAME)='SERVER'))
begin
  insert into PROVIDERPARAMETER(SUBTYPEID, PARAMSHORTNAME, PARAMNAME, DEFAULTVALUE, DESCRIPTION, SORTORDER) 
  select 
    ps.RECID,'IMPORTUSERFILENAME','Filename','','Full path and filename of XML file for sceduled user import (Has to be a path recognized and writable by the server)', 
    max(pp.SORTORDER)+1 
  from PROVIDERSUBTYPE ps 
  left outer join PROVIDERPARAMETER pp on pp.SUBTYPEID=ps.RECID where upper(ps.TYPENAME)='SERVER' 
  group by 
    ps.RECID
end
GO

-- insert default IMPORTUSERFILENAME value into server connector
if not exists(select RECID from PROVIDERVALUE where PARAMETERID=(select RECID from PROVIDERPARAMETER where PARAMSHORTNAME='IMPORTUSERFILENAME' and SUBTYPEID=(select top 1 RECID from PROVIDERSUBTYPE where Upper(TYPENAME)='SERVER')))
begin
  insert into PROVIDERVALUE(PROVIDERID, PARAMETERID, PARAMVALUE)
  select
    p.RECID, pp.RECID, pp.DEFAULTVALUE
  from
    PROVIDER p
  join PROVIDERSUBTYPE ps on ps.RECID=p.SUBTYPE and upper(ps.TYPENAME)='SERVER'
  join PROVIDERPARAMETER pp on pp.SUBTYPEID=p.SUBTYPE and PARAMSHORTNAME='IMPORTUSERFILENAME'
end
GO


-- HiPath Connector. Add AREATRUNKCODE parameter.
if not exists(select * from PROVIDERPARAMETER where PARAMSHORTNAME='AREATRUNKCODE' and SUBTYPEID=(select top 1 RECID from PROVIDERSUBTYPE where Upper(TYPENAME)='HIPATH'))
begin
  insert into PROVIDERPARAMETER(SUBTYPEID, PARAMSHORTNAME, PARAMNAME, DEFAULTVALUE, DESCRIPTION, SORTORDER) 
  select 
    ps.RECID,'AREATRUNKCODE','Area Trunk Code',NULL,'Trunk access code used in HiPath''s area', 
    max(pp.SORTORDER)+1 
  from PROVIDERSUBTYPE ps 
  left outer join PROVIDERPARAMETER pp on pp.SUBTYPEID=ps.RECID where upper(ps.TYPENAME)='HIPATH' 
  group by 
    ps.RECID
end
GO

-- Server virtual directory for reports
if not exists(select RECID from PROVIDERPARAMETER where PARAMSHORTNAME='VIRTUALDIRECTORY' and SUBTYPEID=(select top 1 RECID from PROVIDERSUBTYPE where Upper(TYPENAME)='SERVER'))
begin
  insert into PROVIDERPARAMETER(SUBTYPEID, PARAMSHORTNAME, PARAMNAME, DEFAULTVALUE, DESCRIPTION, SORTORDER) 
  select 
    ps.RECID,'VIRTUALDIRECTORY','Reports directory','http://','URL to reports virtual directory', 
    max(pp.SORTORDER)+1 
  from PROVIDERSUBTYPE ps 
  left outer join PROVIDERPARAMETER pp on pp.SUBTYPEID=ps.RECID where upper(ps.TYPENAME)='SERVER' 
  group by 
    ps.RECID
end
GO

-- set REPORTSANIMATION default false
if not exists(select RECID from PROVIDERPARAMETER where PARAMSHORTNAME='REPORTSANIMATION' and SUBTYPEID=(select top 1 RECID from PROVIDERSUBTYPE where Upper(TYPENAME)='SERVER'))
begin
  insert into PROVIDERPARAMETER(SUBTYPEID, PARAMSHORTNAME, PARAMNAME, DEFAULTVALUE, DESCRIPTION, SORTORDER) 
  select 
    ps.RECID,'REPORTSANIMATION','Animate report login screen','0','Animate report login screen', max(pp.SORTORDER)+1 
  from PROVIDERSUBTYPE ps 
  left outer join PROVIDERPARAMETER pp on pp.SUBTYPEID=ps.RECID where upper(ps.TYPENAME)='SERVER' 
  group by 
    ps.RECID
end
else begin
  UPDATE PROVIDERPARAMETER
  SET DEFAULTVALUE = 0
  WHERE PARAMSHORTNAME='REPORTSANIMATION' and SUBTYPEID=(select top 1 RECID from PROVIDERSUBTYPE where Upper(TYPENAME)='SERVER');
end
GO

if not exists(select * from INFORMATION_SCHEMA.COLUMNS where TABLE_NAME='RESOURCEDETAIL' and COLUMN_NAME='PRIVATEPARAMETER') 
begin
  alter table RESOURCEDETAIL add 
    PRIVATEPARAMETER int not null default 0,
    constraint [CK_RESOURCEDETAIL_PRIVATEPARAMETER] check ([PRIVATEPARAMETER]=1 or [PRIVATEPARAMETER]=0)
end
GO

if not exists(select * from PROVIDERSUBTYPE where CATEGORYENUM=5 and upper(TYPENAME)='TDC')
  insert into PROVIDERSUBTYPE (CATEGORYENUM, TYPENAME) values (5, 'TDC')
  
if not exists(select * from PROVIDERSUBTYPE where CATEGORYENUM=5 and upper(TYPENAME)='TELIA')
  insert into PROVIDERSUBTYPE (CATEGORYENUM, TYPENAME) values (5, 'Telia')

if not exists(select * from PROVIDERSUBTYPE where CATEGORYENUM=8 and upper(TYPENAME)='MobileWeb')
  insert into PROVIDERSUBTYPE (CATEGORYENUM, TYPENAME) values (8, 'MobileWeb')
  
-- Cirque/Telenor no longer part of Totalview since 3.3.2
delete from PROVIDERSUBTYPE where typename = 'Cirque'
delete from PROVIDERSUBTYPE where typename = 'Telenor'

if not exists(select * from PROVIDERSUBTYPE where CATEGORYENUM=3 and upper(TYPENAME)='EXCHANGEEWS')
  insert into PROVIDERSUBTYPE (CATEGORYENUM, TYPENAME) values (3, 'ExchangeEWS')

if not exists(select * from PROVIDERSUBTYPE where CATEGORYENUM=3 and upper(TYPENAME)='EXCHANGEONLINE')
  insert into PROVIDERSUBTYPE (CATEGORYENUM, TYPENAME) values (3, 'ExchangeOnline')

if not exists(select * from PROVIDERSUBTYPE where CATEGORYENUM=3 and upper(TYPENAME)='GOOGLECALENDAR')
  insert into PROVIDERSUBTYPE (CATEGORYENUM, TYPENAME) values (3, 'GoogleCalendar')

if not exists(select * from PROVIDERSUBTYPE where CATEGORYENUM=3 and upper(TYPENAME)='EXCHANGE365')
begin
  insert into PROVIDERSUBTYPE (CATEGORYENUM, TYPENAME) values (3, 'Exchange365')
  select 'Inserted provider subtype Exchange365'
end

if not exists(select * from PROVIDERSUBTYPE where CATEGORYENUM=9 and upper(TYPENAME)='ActiveDirectory')
  insert into PROVIDERSUBTYPE (CATEGORYENUM, TYPENAME) values (9, 'ActiveDirectory')

if not exists(select * from PROVIDERSUBTYPE where CATEGORYENUM=2 and upper(TYPENAME)='OpenScapeVoice')
  insert into PROVIDERSUBTYPE (CATEGORYENUM, TYPENAME) values (2, 'OpenScapeVoice')

if not exists(select * from PROVIDERSUBTYPE where CATEGORYENUM=9 and upper(TYPENAME)='OpenScapeOffice')
  insert into PROVIDERSUBTYPE (CATEGORYENUM, TYPENAME) values (9, 'OpenScapeOffice')

if not exists(select * from PROVIDERSUBTYPE where CATEGORYENUM=2 and upper(TYPENAME)='3CX')
  insert into PROVIDERSUBTYPE (CATEGORYENUM, TYPENAME) values (2, '3CX')

if not exists(select * from PROVIDERSUBTYPE where CATEGORYENUM=2 and upper(TYPENAME)='BroadWorksHey')
begin
  update PROVIDERSUBTYPE set TYPENAME = 'BroadWorksNettala' where TYPENAME = 'BroadWorks'
  insert into PROVIDERSUBTYPE (CATEGORYENUM, TYPENAME) values (2, 'BroadWorksHey')
  select 'Add BroadWorksHey with CATEGORYENUM=2 to PROVIDERSUBTYPE'
end;

if not exists(select * from PROVIDERSUBTYPE where CATEGORYENUM=2 and upper(TYPENAME)='IPOffice')
begin
  insert into PROVIDERSUBTYPE (CATEGORYENUM, TYPENAME) values (2, 'IPOffice')
  select 'Add IPOffice with CATEGORYENUM=2 to PROVIDERSUBTYPE'
end;

if not exists(select * from PROVIDERSUBTYPE where CATEGORYENUM=2 and upper(TYPENAME)='BroadWorksNettala')
  insert into PROVIDERSUBTYPE (CATEGORYENUM, TYPENAME) values (2, 'BroadWorksNettala')

if not exists(select * from PROVIDERSUBTYPE where CATEGORYENUM=9 and upper(TYPENAME)='Lync')
  insert into PROVIDERSUBTYPE (CATEGORYENUM, TYPENAME) values (9, 'Lync')

if not exists(select * from PROVIDERSUBTYPE where CATEGORYENUM=9 and upper(TYPENAME)='TeamsPresence')
  insert into PROVIDERSUBTYPE (CATEGORYENUM, TYPENAME) values (9, 'TeamsPresence')

if not exists(select * from PROVIDERSUBTYPE where CATEGORYENUM=9 and upper(TYPENAME)='Extension')
  insert into PROVIDERSUBTYPE (CATEGORYENUM, TYPENAME) values (9, 'Extension')

if not exists(select * from PROVIDERSUBTYPE where CATEGORYENUM=9 and upper(TYPENAME)='Attachments')
  insert into PROVIDERSUBTYPE (CATEGORYENUM, TYPENAME) values (9, 'Attachments')

if not exists(select * from PROVIDERSUBTYPE where CATEGORYENUM=5 and upper(TYPENAME)='UniTelMobileState')
  insert into PROVIDERSUBTYPE (CATEGORYENUM, TYPENAME) values (5, 'UniTelMobileState')

if not exists(select * from PROVIDERSUBTYPE where CATEGORYENUM=5 and upper(TYPENAME)='Telenor')
  insert into PROVIDERSUBTYPE (CATEGORYENUM, TYPENAME) values (5, 'Telenor')

if not exists(select * from PROVIDERSUBTYPE where CATEGORYENUM=5 and upper(TYPENAME)='Kvantel')
  insert into PROVIDERSUBTYPE (CATEGORYENUM, TYPENAME) values (5, 'Kvantel')

if not exists(select * from PROVIDERSUBTYPE where CATEGORYENUM=5 and upper(TYPENAME)='3Mobil')
  insert into PROVIDERSUBTYPE (CATEGORYENUM, TYPENAME) values (5, '3Mobil')

if not exists(select * from PROVIDERSUBTYPE where CATEGORYENUM=4 and upper(TYPENAME)='LinkMobility')
begin
  insert into PROVIDERSUBTYPE (CATEGORYENUM, TYPENAME) values (4, 'LinkMobility')
  select 'Add LinkMobility with CATEGORYENUM=4 to PROVIDERSUBTYPE'
end

if not exists(select * from PROVIDERSUBTYPE where CATEGORYENUM=4 and upper(TYPENAME)='TwilioSMS')
begin
  insert into PROVIDERSUBTYPE (CATEGORYENUM, TYPENAME) values (4, 'TwilioSMS')
  select 'Add TwilioSMS with CATEGORYENUM=4 to PROVIDERSUBTYPE'
end

if not exists(select * from PROVIDERSUBTYPE where CATEGORYENUM=5 and upper(TYPENAME)='TelenorNo')
begin
  insert into PROVIDERSUBTYPE (CATEGORYENUM, TYPENAME) values (5, 'TelenorNo')
  select 'Add TelenorNo with CATEGORYENUM=5 to PROVIDERSUBTYPE'
end;

if not exists(select * from PROVIDERSUBTYPE where CATEGORYENUM=11 and TYPENAME='Sona.fo')
begin
  insert into PROVIDERSUBTYPE (CATEGORYENUM, TYPENAME) values (11, 'Sona.fo')
  select 'Add Sona.fo with CATEGORYENUM=11 to PROVIDERSUBTYPE'
end;

if not exists(select * from SETTING where PARAMNAME='WorkAndJobsNewIndicator')
	insert into SETTING (PARAMNAME, PARAMVALUE) values ('WorkAndJobsNewIndicator', '2018-04-01')
  

declare @DbVer int
execute SPTV_GETDBVERSION @DbVer output
if (@DbVer<142)
begin
  update RESOURCE set DEACTIVEDATE = CONVERT(datetime,'01-01-2008',105) where DEACTIVEDATE is null and ACTIVE=0
end
GO

declare @DbVer int
execute SPTV_GETDBVERSION @DbVer output
if (@DbVer<145)
begin
  update PROVIDERPARAMETER set DEFAULTVALUE=1 where upper(PARAMSHORTNAME)='SYNTAX20' and SUBTYPEID in (select RECID from PROVIDERSUBTYPE where TYPENAME='SMS')
end
GO

---------------------------------------------------------------------------------------
-- Clean up provider settings no longer supported                                    --
---------------------------------------------------------------------------------------
--Delete all GroupWise, FlexIm and Dinner providers data
delete from PROVIDERVALUE where PROVIDERID in (select RECID from PROVIDERSUBTYPE where UPPER(TYPENAME) in ('FLEXIM', 'DINNER', 'GROUPWISE'))
delete from PROVIDERPARAMETER where SUBTYPEID in (select RECID from PROVIDERSUBTYPE where UPPER(TYPENAME) in ('FLEXIM', 'DINNER', 'GROUPWISE'))
delete from PROVIDER where CATEGORYENUM = (select RECID from PROVIDERCATEGORY where upper(CATEGORYNAME) = 'TERMINAL') 
                           or
                           SUBTYPE in (SELECT RECID FROM PROVIDERSUBTYPE where UPPER(TYPENAME) in ('FLEXIM', 'DINNER', 'GROUPWISE'))
delete from PROVIDERSUBTYPE where upper(TYPENAME) in ('FLEXIM', 'DINNER', 'GROUPWISE')
delete from PROVIDERCATEGORY where upper(CATEGORYNAME) = 'TERMINAL'
--Delete Exchange/WebDav provider Calendar subtype
declare
  @SubTypeId as int,
  @CategoryEnum as int
begin
  set @CategoryEnum = (select RECID from PROVIDERCATEGORY where upper(CATEGORYNAME) = 'CALENDAR');
  set @SubTypeId = (select RECID from PROVIDERSUBTYPE where upper(TYPENAME) = 'EXCHANGE' and CATEGORYENUM = @CategoryEnum);
 
  delete from PROVIDERVALUE where PROVIDERID in (select RECID from PROVIDER where SUBTYPE = @SubTypeId);
  delete from PROVIDERPARAMETER where SUBTYPEID = @SubTypeId;
  delete from PROVIDER where SUBTYPE = @SubTypeId;
  delete from PROVIDERSUBTYPE where RECID = @SubTypeId;
end;
GO

---------------------------------------------------------------------------------------
-- Insert default sms/mail templates                                                 --
--                                                                                   --
-- Template will only be inserted if no default is already inserted for switchboards --
---------------------------------------------------------------------------------------
declare 
  @ClientSettingsID as int
  
--Check if default ClientSetting exists
if exists(select * from CLIENTSETTING where RESOURCEID=-1 and SETTINGTYPE=2 and CLIENTTYPE=3)
  set @ClientSettingsID = (select top 1 RECID from CLIENTSETTING where RESOURCEID=-1 and SETTINGTYPE=2 and CLIENTTYPE=3)
else begin
  --Insert default Clientsetting if it does not exist
  insert into CLIENTSETTING (RESOURCEID, SETTINGTYPE, CLIENTTYPE, MACHINENAME   ) values
                            (-1,          2,           3,          'ALL MACHINES')
  select @ClientSettingsID = Ident_current('CLIENTSETTING')
end

--Insert default sms/mail templates if they don't already exist
if not exists(select * from CLIENTSETTINGDETAIL where CLIENTSETTINGID = @ClientSettingsID and PARAMNAME = 'MESSAGETEMPLATE')
	insert into CLIENTSETTINGDETAIL 
	  (CLIENTSETTINGID,   PARAMNAME, PARAMVALUE, PARAMTYPE, FORCEUSE) values
	  (@ClientSettingsID, 
	   'MESSAGETEMPLATE',
	   '<?xml version="1.0" encoding="UTF-8"?>' + CHAR(13) + CHAR(10) +
	   '<MessageTemplates>' + CHAR(13) + CHAR(10) +
	   '   <Template Name="Telephone message" Subject="[C] called you today [D] at [T]" Flag="">' + CHAR(13) + CHAR(10) +
	   '	<Body Line="Please call back at the number [N]"/>' + CHAR(13) + CHAR(10) +
	   '	<Body Line=""/>' + CHAR(13) + CHAR(10) +
	   '	<Body Line="Best regards "/>' + CHAR(13) + CHAR(10) +
	   '	<Body Line="[o]"/>' + CHAR(13) + CHAR(10) +
	   ' </Template>' + CHAR(13) + CHAR(10) +
	   '   <Template Name="Urgent message" Subject="[C] called you today [D] at [T]" Flag="HighImportance">' + CHAR(13) + CHAR(10) +
	   '	<Body Line="Please call [C] at the number [N] as soon as possible."/>' + CHAR(13) + CHAR(10) +
	   '	<Body Line=""/>' + CHAR(13) + CHAR(10) +
	   '	<Body Line="Best regards"/>' + CHAR(13) + CHAR(10) +
	   '	<Body Line="[o]"/>' + CHAR(13) + CHAR(10) +
	   ' </Template>' + CHAR(13) + CHAR(10) +
	   '</MessageTemplates>', 
	   1, 
	   0)

---------------------------------------------------------------------------------------
-- Add some Provider Parameters                                                      --
--                                                                                   --
-- 16/06-2020 - BAC                                                                  --
---------------------------------------------------------------------------------------
declare @serverSubType int = (select TOP (1) RECID from PROVIDERSUBTYPE where TYPENAME = 'Server')

if not exists(select * from PROVIDERPARAMETER where SUBTYPEID = @serverSubType and PARAMSHORTNAME = 'NUMBERLOGGINGVALIDDAYS')
begin
  insert into PROVIDERPARAMETER (SUBTYPEID, PARAMSHORTNAME, PARAMNAME, DEFAULTVALUE, DESCRIPTION, SORTORDER) values (@serverSubType, 'NUMBERLOGGINGVALIDDAYS', 'Valid number days', '14', 'Number of days the records will be saved in DB', 1)
end

if not exists(select * from PROVIDERPARAMETER where SUBTYPEID = @serverSubType and PARAMSHORTNAME = 'NUMBERLOGGING')
begin
  insert into PROVIDERPARAMETER (SUBTYPEID, PARAMSHORTNAME, PARAMNAME, DEFAULTVALUE, DESCRIPTION, SORTORDER) values (@serverSubType, 'NUMBERLOGGING', 'Enable number logging', '0', '0 = FALSE, 1 = TRUE', 1);
end

if not exists(select * from PROVIDERPARAMETER where SUBTYPEID = @serverSubType and PARAMSHORTNAME = 'ENABLEPHONEBOOKCLIENTCHANGE')
begin
  insert into PROVIDERPARAMETER (SUBTYPEID, PARAMSHORTNAME, PARAMNAME, DEFAULTVALUE, DESCRIPTION, SORTORDER) values (@serverSubType, 'ENABLEPHONEBOOKCLIENTCHANGE', 'Enable client modification of phone book', '1', '0 = FALSE, 1 = TRUE', 1);
end

if not exists(select * from PROVIDERPARAMETER where SUBTYPEID = @serverSubType and PARAMSHORTNAME = 'SERVICERESETEVERY')
begin
  insert into PROVIDERPARAMETER (SUBTYPEID, PARAMSHORTNAME, PARAMNAME, DEFAULTVALUE, DESCRIPTION, SORTORDER) values (@serverSubType, 'SERVICERESETEVERY', 'Number of days between server restarts', '7', 'Number of days between server restarts', 1);
end

if not exists(select * from PROVIDERPARAMETER where SUBTYPEID = @serverSubType and PARAMSHORTNAME = 'LOGCALLSTATISTICS')
begin
  insert into PROVIDERPARAMETER (SUBTYPEID, PARAMSHORTNAME, PARAMNAME, DEFAULTVALUE, DESCRIPTION, SORTORDER) values (@serverSubType, 'LOGCALLSTATISTICS', 'Enable call statistics logging', '0', 'Enable call statistics logging', 1);
end

--WCF Parameters
declare @wcfSubType int = (select TOP (1) RECID from PROVIDERSUBTYPE where TYPENAME = 'MobileWeb')

if @wcfSubType is not null
begin
  if not exists(select * from PROVIDERPARAMETER where SUBTYPEID = @wcfSubType and PARAMSHORTNAME = 'MINPWDLENGTH')
  begin
    insert into PROVIDERPARAMETER (SUBTYPEID, PARAMSHORTNAME, PARAMNAME, DEFAULTVALUE, DESCRIPTION, SORTORDER) values (@wcfSubType, 'MINPWDLENGTH', 'Password length', '4', 'Min. mobile password length to use', 1);
  end

  if not exists(select * from PROVIDERPARAMETER where SUBTYPEID = @wcfSubType and PARAMSHORTNAME = 'PREFIXINTERVAL')
  begin
    insert into PROVIDERPARAMETER (SUBTYPEID, PARAMSHORTNAME, PARAMNAME, DEFAULTVALUE, DESCRIPTION, SORTORDER) values (@wcfSubType, 'PREFIXINTERVAL', '', '4', 'Interval for local phone prefix', 1);
  end

  if not exists(select * from PROVIDERPARAMETER where SUBTYPEID = @wcfSubType and PARAMSHORTNAME = 'ENDPOINT')
  begin
    insert into PROVIDERPARAMETER (SUBTYPEID, PARAMSHORTNAME, PARAMNAME, DEFAULTVALUE, DESCRIPTION, SORTORDER) values (@wcfSubType, 'ENDPOINT', 'WCF endpoint address', 'localhost:8033', 'Web service endpoint address', 1);
  end

  if not exists(select * from PROVIDERPARAMETER where SUBTYPEID = @wcfSubType and PARAMSHORTNAME = 'ENABLEWINLOGIN')
  begin
    insert into PROVIDERPARAMETER (SUBTYPEID, PARAMSHORTNAME, PARAMNAME, DEFAULTVALUE, DESCRIPTION, SORTORDER) values (@wcfSubType, 'ENABLEWINLOGIN', 'Enable win login', '0', 'Enable win login', 1);
  end

  if not exists(select * from PROVIDERPARAMETER where SUBTYPEID = @wcfSubType and PARAMSHORTNAME = 'ADMINUSERNAME')
  begin
    insert into PROVIDERPARAMETER (SUBTYPEID, PARAMSHORTNAME, PARAMNAME, DEFAULTVALUE, DESCRIPTION, SORTORDER) values (@wcfSubType, 'ADMINUSERNAME', 'Win login admin username', 'Totalview', 'Win login admin username', 1);
  end

  if not exists(select * from PROVIDERPARAMETER where SUBTYPEID = @wcfSubType and PARAMSHORTNAME = 'ADMINPASSWORD')
  begin
    insert into PROVIDERPARAMETER (SUBTYPEID, PARAMSHORTNAME, PARAMNAME, DEFAULTVALUE, DESCRIPTION, SORTORDER) values (@wcfSubType, 'ADMINPASSWORD', 'Win login admin password', '12345', 'Win login admin password', 1);
  end

  if not exists(select * from PROVIDERPARAMETER where SUBTYPEID = @wcfSubType and PARAMSHORTNAME = 'CALLCENTERGROUPS')
  begin
    insert into PROVIDERPARAMETER (SUBTYPEID, PARAMSHORTNAME, PARAMNAME, DEFAULTVALUE, DESCRIPTION, SORTORDER) values (@wcfSubType, 'CALLCENTERGROUPS', 'Call center groups', '', 'Call center groups', 1);
  end

  if not exists(select * from PROVIDERPARAMETER where SUBTYPEID = @wcfSubType and PARAMSHORTNAME = 'LOGGINGLEVEL')
  begin
    insert into PROVIDERPARAMETER (SUBTYPEID, PARAMSHORTNAME, PARAMNAME, DEFAULTVALUE, DESCRIPTION, SORTORDER) values (@wcfSubType, 'LOGGINGLEVEL', 'Logging level', '2', 'Logging level', 1);
  end

  if not exists(select * from PROVIDERPARAMETER where SUBTYPEID = @wcfSubType and PARAMSHORTNAME = 'WALLBOARDAPIKEY')
  begin
    insert into PROVIDERPARAMETER (SUBTYPEID, PARAMSHORTNAME, PARAMNAME, DEFAULTVALUE, DESCRIPTION, SORTORDER) values (@wcfSubType, 'WALLBOARDAPIKEY', 'Wallboard API key', '', 'Wallboard API key', 1);
  end
end
GO

---------------------------------------------------------------------------------------
-- Add new Provider Parameters for server to store customer information              --
--                                                                                   --
-- 16/06-2020 - BAC                                                                  --
---------------------------------------------------------------------------------------
declare @serverSubType int = (select TOP (1) RECID from PROVIDERSUBTYPE where TYPENAME = 'Server')

if not exists(select * from PROVIDERPARAMETER where SUBTYPEID = @serverSubType and PARAMSHORTNAME = 'CUSTOMERNAME')
begin
  insert into PROVIDERPARAMETER (SUBTYPEID, PARAMSHORTNAME, PARAMNAME, DEFAULTVALUE, DESCRIPTION, SORTORDER) values (@serverSubType, 'CUSTOMERNAME', 'Customer Name', '', 'Name of the owner of the Totalview installation', 1)
end

if not exists(select * from PROVIDERPARAMETER where SUBTYPEID = @serverSubType and PARAMSHORTNAME = 'CUSTOMERCVRNUMBER')
begin
  insert into PROVIDERPARAMETER (SUBTYPEID, PARAMSHORTNAME, PARAMNAME, DEFAULTVALUE, DESCRIPTION, SORTORDER) values (@serverSubType, 'CUSTOMERCVRNUMBER', 'Customer CVR Number', '', 'CVR number of the owner of the Totalview installation', 1)
end
GO



---------------------------------------------------------------------------------------
-- Add new ExternalLogin to support Single Sign On With Identity Server              --
--                                                                                   --
-- 06/11-2020 - BAC                                                                  --
---------------------------------------------------------------------------------------
if not exists(select * from INFORMATION_SCHEMA.TABLES where TABLE_NAME='EXTERNALLOGIN') 
begin

CREATE TABLE [dbo].[EXTERNALLOGIN](
	[PROVIDER] [nvarchar](450) NOT NULL,
	[PROVIDERKEY] [nvarchar](450) NOT NULL,
	[RESOURCEID] [int] NOT NULL,
 CONSTRAINT [PK_EXTERNALLOGIN] PRIMARY KEY CLUSTERED 
(
	[PROVIDER] ASC,
	[PROVIDERKEY] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]


ALTER TABLE [dbo].[EXTERNALLOGIN]  WITH CHECK ADD  CONSTRAINT [FK_EXTERNALLOGIN_RESOURCE_RESOURCEID] FOREIGN KEY(RESOURCEID)
REFERENCES [dbo].[RESOURCE] ([RECID])
ON DELETE CASCADE

ALTER TABLE [dbo].[EXTERNALLOGIN] CHECK CONSTRAINT [FK_EXTERNALLOGIN_RESOURCE_RESOURCEID]

select 'EXTERNALLOGIN table created';

END
GO



---------------------------------------------------------------------------------------
-- Add new ExternalLoginProvider to support Single Sign On With Identity Server      --
--                                                                                   --
-- 20/11-2020 - BAC                                                                  --
---------------------------------------------------------------------------------------
if not exists(select * from INFORMATION_SCHEMA.TABLES where TABLE_NAME='EXTERNALLOGINPROVIDER') 
begin

CREATE TABLE [EXTERNALLOGINPROVIDER] (
    [RECID] int NOT NULL IDENTITY,
    [TYPE] int NOT NULL,
    [ISENABLED] bit NOT NULL default(0),
    CONSTRAINT [PK_EXTERNALLOGINPROVIDER] PRIMARY KEY ([RECID])
);

select 'EXTERNALLOGINPROVIDER table created';

END
GO

if not exists(select * from INFORMATION_SCHEMA.TABLES where TABLE_NAME='EXTERNALLOGINPROVIDERPROPERTY') 
begin

CREATE TABLE [EXTERNALLOGINPROVIDERPROPERTY] (
    [KEY] nvarchar(450) NOT NULL,
    [EXTERNALLOGINPROVIDERID] int NOT NULL,
    [VALUE] nvarchar(max) NULL,
    CONSTRAINT [PK_EXTERNALLOGINPROVIDERPROPERTY] PRIMARY KEY ([EXTERNALLOGINPROVIDERID], [KEY]),
    CONSTRAINT [FK_EXTERNALLOGINPROVIDERPROPERTY_EXTERNALLOGINPROVIDER_EXTERNALLOGINPROVIDERID] FOREIGN KEY ([EXTERNALLOGINPROVIDERID]) REFERENCES [EXTERNALLOGINPROVIDER] ([RECID]) ON DELETE CASCADE
);

select 'EXTERNALLOGINPROVIDERPROPERTY table created';

END
GO



--------------------------------------------------------------------------
-- In older server versions lot's of entries were posted in statefinish --
-- Entries up to 01-01-2009 will be deleted                             --
--------------------------------------------------------------------------
declare @DbVer int
execute SPTV_GETDBVERSION @DbVer output
if (@DbVer<147)
begin
  delete from STATEFINISH WHERE STATESTART < '20090101'
end
GO

-----------------------------------------------------------------------------------------------------
-----------------------------------------------------------------------------------------------------
-- Update Version
-----------------------------------------------------------------------------------------------------
-----------------------------------------------------------------------------------------------------

declare @version nvarchar(250)
set @version = '2020.2.12.0'
execute SPTV_SETTVVERSION @version
GO

declare @DbVer int
set @DbVer = 202021200
execute SPTV_SETDBVERSION @DbVer
select 'Basic DB version updated to ' + CONVERT(nvarchar, @DbVer, 0)
GO