SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[FeatureUser]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[FeatureUser](
	[UserId] [uniqueidentifier] NOT NULL,
	[FeatureId] [uniqueidentifier] NOT NULL,
	[TypeName] [nvarchar](20) NULL,
	[LastUpdatedDate] [datetime] NULL,
 CONSTRAINT [PK_FeatureUser] PRIMARY KEY CLUSTERED 
(
	[UserId] ASC,
	[FeatureId] ASC
)WITH (IGNORE_DUP_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
END
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'编号ID' ,@level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'FeatureUser', @level2type=N'COLUMN', @level2name=N'UserId'

GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'编号ID' ,@level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'FeatureUser', @level2type=N'COLUMN', @level2name=N'FeatureId'

GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[OrderProcess]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[OrderProcess](
	[Id] [uniqueidentifier] NOT NULL CONSTRAINT [DF_OrderProcess_Id]  DEFAULT (newid()),
	[OrderId] [uniqueidentifier] NULL,
	[StaffCode] [varchar](36) NULL,
	[StepName] [nvarchar](20) NULL,
	[Pictures] [nvarchar](1000) NULL,
	[DeviceId] [varchar](100) NULL,
	[Latlng] [varchar](100) NULL,
	[LatlngPlace] [nvarchar](256) NULL,
	[Ip] [varchar](20) NULL,
	[IpPlace] [nvarchar](256) NULL,
	[RecordDate] [datetime] NULL,
	[LastUpdatedDate] [datetime] NULL,
 CONSTRAINT [PK_OrderProcess] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (IGNORE_DUP_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
END
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'编号ID' ,@level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'OrderProcess', @level2type=N'COLUMN', @level2name=N'Id'

GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'' ,@level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'OrderProcess', @level2type=N'COLUMN', @level2name=N'OrderId'

GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[OrderMake]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[OrderMake](
	[Id] [uniqueidentifier] NOT NULL CONSTRAINT [DF_OrderMake_Id]  DEFAULT (newid()),
	[AppCode] [char](6) NULL,
	[UserId] [uniqueidentifier] NULL,
	[CustomerCode] [varchar](36) NULL,
	[OrderCode] [varchar](36) NULL,
	[FromName] [nvarchar](50) NULL,
	[FromAddress] [nvarchar](256) NULL,
	[FromPhone] [varchar](20) NULL,
	[ToCity] [nvarchar](50) NULL,
	[ToName] [nvarchar](50) NULL,
	[ToAddress] [nvarchar](256) NULL,
	[ToPhone] [varchar](20) NULL,
	[StaffCode] [varchar](36) NULL,
	[StaffCodeOfTake] [varchar](36) NULL,
	[TakeTime] [datetime] NULL,
	[ReachTime] [datetime] NULL,
	[CargoName] [nvarchar](256) NULL,
	[ServiceProduct] [nvarchar](256) NULL,
	[PieceQty] [int] NULL,
	[Weight] [float] NULL,
	[TranPrice] [decimal](18, 2) NULL,
	[IncreServicePrice] [decimal](18, 2) NULL,
	[TotalPrice] [decimal](18, 2) NULL,
	[PayWay] [nvarchar](30) NULL,
	[Remark] [nvarchar](256) NULL,
	[RecordDate] [datetime] NULL,
	[LastUpdatedDate] [datetime] NULL,
 CONSTRAINT [PK_OrderMake] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (IGNORE_DUP_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
END
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'编号ID' ,@level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'OrderMake', @level2type=N'COLUMN', @level2name=N'Id'

GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Customer]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[Customer](
	[Id] [uniqueidentifier] NOT NULL CONSTRAINT [DF_Customer_Id]  DEFAULT (newid()),
	[AppCode] [char](6) NULL,
	[UserId] [uniqueidentifier] NULL,
	[Coded] [varchar](36) NULL,
	[Named] [nvarchar](50) NULL,
	[ShortName] [nvarchar](50) NULL,
	[ContactMan] [nvarchar](20) NULL,
	[ContactPhone] [varchar](20) NULL,
	[TelPhone] [varchar](20) NULL,
	[Fax] [varchar](20) NULL,
	[PostCode] [varchar](50) NULL,
	[Address] [nvarchar](256) NULL,
	[CityName] [nvarchar](30) NULL,
	[TradeName] [nvarchar](50) NULL,
	[CooperateTime] [datetime] NULL,
	[AgreementTimeout] [datetime] NULL,
	[JoinPrice] [decimal](18, 2) NULL,
	[DiscountAbout] [nvarchar](50) NULL,
	[PayWay] [nvarchar](30) NULL,
	[StaffCode] [varchar](36) NULL,
	[Remark] [nvarchar](256) NULL,
	[RecordDate] [datetime] NULL,
	[LastUpdatedDate] [datetime] NULL,
 CONSTRAINT [PK_Customer] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (IGNORE_DUP_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
END
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'编号ID' ,@level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'Customer', @level2type=N'COLUMN', @level2name=N'Id'

