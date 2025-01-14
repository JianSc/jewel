if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[GOODS_BACK_REK]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[GOODS_BACK_REK]
GO

if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[QGOODS_BACK_GET]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[QGOODS_BACK_GET]
GO

if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[QZ_CBEI_LOG]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[QZ_CBEI_LOG]
GO

if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[QZ_CKDEL]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[QZ_CKDEL]
GO

if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[QZ_CKDH_LOG]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[QZ_CKDH_LOG]
GO

if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[QZ_CKD_SH]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[QZ_CKD_SH]
GO

if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[QZ_GDOOSTJ_XSED]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[QZ_GDOOSTJ_XSED]
GO

if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[QZ_GOODSADD]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[QZ_GOODSADD]
GO

if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[QZ_GOODSADD_GYS]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[QZ_GOODSADD_GYS]
GO

if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[QZ_GOODSADD_RKD]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[QZ_GOODSADD_RKD]
GO

if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[QZ_GOODSDEL]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[QZ_GOODSDEL]
GO

if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[QZ_GOODSTJ]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[QZ_GOODSTJ]
GO

if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[QZ_GOODS_BACK]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[QZ_GOODS_BACK]
GO

if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[QZ_GOODS_CKU]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[QZ_GOODS_CKU]
GO

if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[QZ_GOODS_KUT]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[QZ_GOODS_KUT]
GO

if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[QZ_GOODS_LOG]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[QZ_GOODS_LOG]
GO

if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[QZ_GOODS_XSED_LOG]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[QZ_GOODS_XSED_LOG]
GO

if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[QZ_KHU_JF]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[QZ_KHU_JF]
GO

if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[QZ_RKDH_LOG]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[QZ_RKDH_LOG]
GO

if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[QZ_SALES]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[QZ_SALES]
GO

if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[QZ_SALE_BACK]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[QZ_SALE_BACK]
GO

if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[QZ_YGONG_ADD]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[QZ_YGONG_ADD]
GO

if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[QZ_YHUI]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[QZ_YHUI]
GO

if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[qCKU]') and OBJECTPROPERTY(id, N'IsUserTable') = 1)
drop table [dbo].[qCKU]
GO

if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[qDQU]') and OBJECTPROPERTY(id, N'IsUserTable') = 1)
drop table [dbo].[qDQU]
GO

if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[qDWEI]') and OBJECTPROPERTY(id, N'IsUserTable') = 1)
drop table [dbo].[qDWEI]
GO

if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[qGOODS]') and OBJECTPROPERTY(id, N'IsUserTable') = 1)
drop table [dbo].[qGOODS]
GO

if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[qGOODS_BACK]') and OBJECTPROPERTY(id, N'IsUserTable') = 1)
drop table [dbo].[qGOODS_BACK]
GO

if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[qGOODS_BACK_DR]') and OBJECTPROPERTY(id, N'IsUserTable') = 1)
drop table [dbo].[qGOODS_BACK_DR]
GO

if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[qGOODS_BACK_TXT]') and OBJECTPROPERTY(id, N'IsUserTable') = 1)
drop table [dbo].[qGOODS_BACK_TXT]
GO

if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[qGOODS_CBEI_LOG]') and OBJECTPROPERTY(id, N'IsUserTable') = 1)
drop table [dbo].[qGOODS_CBEI_LOG]
GO

if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[qGOODS_CKD]') and OBJECTPROPERTY(id, N'IsUserTable') = 1)
drop table [dbo].[qGOODS_CKD]
GO

if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[qGOODS_CKU]') and OBJECTPROPERTY(id, N'IsUserTable') = 1)
drop table [dbo].[qGOODS_CKU]
GO

if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[qGOODS_CKU2]') and OBJECTPROPERTY(id, N'IsUserTable') = 1)
drop table [dbo].[qGOODS_CKU2]
GO

if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[qGOODS_CUSTJF]') and OBJECTPROPERTY(id, N'IsUserTable') = 1)
drop table [dbo].[qGOODS_CUSTJF]
GO

if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[qGOODS_CUSTJF_LOG]') and OBJECTPROPERTY(id, N'IsUserTable') = 1)
drop table [dbo].[qGOODS_CUSTJF_LOG]
GO

if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[qGOODS_CUST_ZK]') and OBJECTPROPERTY(id, N'IsUserTable') = 1)
drop table [dbo].[qGOODS_CUST_ZK]
GO

if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[qGOODS_GYS_LIST]') and OBJECTPROPERTY(id, N'IsUserTable') = 1)
drop table [dbo].[qGOODS_GYS_LIST]
GO

if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[qGOODS_KUT]') and OBJECTPROPERTY(id, N'IsUserTable') = 1)
drop table [dbo].[qGOODS_KUT]
GO

if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[qGOODS_LOG]') and OBJECTPROPERTY(id, N'IsUserTable') = 1)
drop table [dbo].[qGOODS_LOG]
GO

if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[qGOODS_RKD]') and OBJECTPROPERTY(id, N'IsUserTable') = 1)
drop table [dbo].[qGOODS_RKD]
GO

if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[qGOODS_RKD_DR]') and OBJECTPROPERTY(id, N'IsUserTable') = 1)
drop table [dbo].[qGOODS_RKD_DR]
GO

if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[qGOODS_SALES]') and OBJECTPROPERTY(id, N'IsUserTable') = 1)
drop table [dbo].[qGOODS_SALES]
GO

if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[qGOODS_SALES_BACK]') and OBJECTPROPERTY(id, N'IsUserTable') = 1)
drop table [dbo].[qGOODS_SALES_BACK]
GO

if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[qGOODS_STOCK]') and OBJECTPROPERTY(id, N'IsUserTable') = 1)
drop table [dbo].[qGOODS_STOCK]
GO

if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[qGOODS_XSED_LOG]') and OBJECTPROPERTY(id, N'IsUserTable') = 1)
drop table [dbo].[qGOODS_XSED_LOG]
GO

if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[qGOODS_YHUI]') and OBJECTPROPERTY(id, N'IsUserTable') = 1)
drop table [dbo].[qGOODS_YHUI]
GO

if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[qGYSHANG]') and OBJECTPROPERTY(id, N'IsUserTable') = 1)
drop table [dbo].[qGYSHANG]
GO

if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[qJDU]') and OBJECTPROPERTY(id, N'IsUserTable') = 1)
drop table [dbo].[qJDU]
GO

if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[qJLIAO]') and OBJECTPROPERTY(id, N'IsUserTable') = 1)
drop table [dbo].[qJLIAO]
GO

if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[qKHU]') and OBJECTPROPERTY(id, N'IsUserTable') = 1)
drop table [dbo].[qKHU]
GO

if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[qMDIAN]') and OBJECTPROPERTY(id, N'IsUserTable') = 1)
drop table [dbo].[qMDIAN]
GO

if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[qQIG]') and OBJECTPROPERTY(id, N'IsUserTable') = 1)
drop table [dbo].[qQIG]
GO

if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[qRKDH_LOG]') and OBJECTPROPERTY(id, N'IsUserTable') = 1)
drop table [dbo].[qRKDH_LOG]
GO

if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[qSLIAO]') and OBJECTPROPERTY(id, N'IsUserTable') = 1)
drop table [dbo].[qSLIAO]
GO

if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[qSSHI]') and OBJECTPROPERTY(id, N'IsUserTable') = 1)
drop table [dbo].[qSSHI]
GO

if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[qSTONG]') and OBJECTPROPERTY(id, N'IsUserTable') = 1)
drop table [dbo].[qSTONG]
GO

if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[qXZUANG]') and OBJECTPROPERTY(id, N'IsUserTable') = 1)
drop table [dbo].[qXZUANG]
GO

if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[qYGONG]') and OBJECTPROPERTY(id, N'IsUserTable') = 1)
drop table [dbo].[qYGONG]
GO

if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[qYGONG_STAT]') and OBJECTPROPERTY(id, N'IsUserTable') = 1)
drop table [dbo].[qYGONG_STAT]
GO

if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[qYSE]') and OBJECTPROPERTY(id, N'IsUserTable') = 1)
drop table [dbo].[qYSE]
GO

CREATE TABLE [dbo].[qCKU] (
	[name] [char] (15) COLLATE Chinese_PRC_CI_AS NOT NULL 
) ON [PRIMARY]
GO

CREATE TABLE [dbo].[qDQU] (
	[id] [int] IDENTITY (1, 1) NOT NULL ,
	[sid] [int] NULL ,
	[name] [char] (15) COLLATE Chinese_PRC_CI_AS NULL ,
	[lv] [int] NULL 
) ON [PRIMARY]
GO

CREATE TABLE [dbo].[qDWEI] (
	[name] [char] (20) COLLATE Chinese_PRC_CI_AS NOT NULL 
) ON [PRIMARY]
GO

CREATE TABLE [dbo].[qGOODS] (
	[tm] [char] (8) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[jliao] [char] (20) COLLATE Chinese_PRC_CI_AS NULL ,
	[sliao] [char] (20) COLLATE Chinese_PRC_CI_AS NULL ,
	[ssi] [char] (20) COLLATE Chinese_PRC_CI_AS NULL ,
	[qkou] [float] NULL ,
	[zshu] [char] (30) COLLATE Chinese_PRC_CI_AS NULL ,
	[kus] [char] (10) COLLATE Chinese_PRC_CI_AS NULL ,
	[hhao] [char] (20) COLLATE Chinese_PRC_CI_AS NULL ,
	[dwei] [char] (2) COLLATE Chinese_PRC_CI_AS NULL ,
	[sliang] [int] NULL ,
	[jianz] [float] NULL ,
	[jinz] [float] NULL ,
	[pjianz] [float] NULL ,
	[blu] [float] NULL ,
	[zsz] [float] NULL ,
	[zss] [int] NULL ,
	[fsz] [float] NULL ,
	[fss] [int] NULL ,
	[cbei] [float] NULL ,
	[xsou] [float] NULL ,
	[bzhu] [text] COLLATE Chinese_PRC_CI_AS NULL ,
	[slbol] [bit] NULL ,
	[imgbol] [bit] NULL ,
	[jdu] [char] (10) COLLATE Chinese_PRC_CI_AS NULL ,
	[yse] [char] (10) COLLATE Chinese_PRC_CI_AS NULL ,
	[xzuang] [char] (10) COLLATE Chinese_PRC_CI_AS NULL ,
	[qgong] [char] (10) COLLATE Chinese_PRC_CI_AS NULL ,
	[xstat] [int] NULL ,
	[cadi] [char] (10) COLLATE Chinese_PRC_CI_AS NULL ,
	[ddh] [char] (10) COLLATE Chinese_PRC_CI_AS NULL ,
	[setTime] [datetime] NULL ,
	[id] [int] IDENTITY (1, 1) NOT NULL ,
	[jja] [float] NULL ,
	[pjja] [float] NULL ,
	[zsja] [float] NULL ,
	[zsje] [float] NULL ,
	[fsja] [float] NULL ,
	[fsje] [float] NULL ,
	[jgdj] [float] NULL ,
	[other] [float] NULL ,
	[jgsh] [float] NULL ,
	[jies] [float] NULL 
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO

CREATE TABLE [dbo].[qGOODS_BACK] (
	[TM] [char] (8) COLLATE Chinese_PRC_CI_AS NULL ,
	[SETTIME] [datetime] NULL ,
	[USER] [char] (10) COLLATE Chinese_PRC_CI_AS NULL ,
	[MDIAN] [char] (50) COLLATE Chinese_PRC_CI_AS NULL ,
	[SLIANG] [int] NULL ,
	[SBM] [char] (14) COLLATE Chinese_PRC_CI_AS NULL ,
	[DH] [char] (10) COLLATE Chinese_PRC_CI_AS NULL ,
	[USERS] [char] (10) COLLATE Chinese_PRC_CI_AS NULL ,
	[ID] [int] IDENTITY (1, 1) NOT NULL 
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO

CREATE TABLE [dbo].[qGOODS_BACK_DR] (
	[DH] [char] (20) COLLATE Chinese_PRC_CI_AS NULL ,
	[USER] [char] (10) COLLATE Chinese_PRC_CI_AS NULL 
) ON [PRIMARY]
GO

CREATE TABLE [dbo].[qGOODS_BACK_TXT] (
	[nTXT] [text] COLLATE Chinese_PRC_CI_AS NULL ,
	[SBM] [char] (14) COLLATE Chinese_PRC_CI_AS NULL 
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO

CREATE TABLE [dbo].[qGOODS_CBEI_LOG] (
	[dh] [char] (10) COLLATE Chinese_PRC_CI_AS NULL ,
	[tm] [char] (8) COLLATE Chinese_PRC_CI_AS NULL ,
	[cbei] [float] NULL ,
	[setdate] [datetime] NULL ,
	[user] [char] (10) COLLATE Chinese_PRC_CI_AS NULL ,
	[stat] [char] (4) COLLATE Chinese_PRC_CI_AS NULL ,
	[jja] [float] NULL ,
	[pjja] [float] NULL ,
	[zsja] [float] NULL ,
	[zsje] [float] NULL ,
	[fsja] [float] NULL ,
	[fsje] [float] NULL ,
	[jgdj] [float] NULL ,
	[other] [float] NULL ,
	[jgsh] [float] NULL ,
	[zshi] [char] (50) COLLATE Chinese_PRC_CI_AS NULL ,
	[fshi] [char] (50) COLLATE Chinese_PRC_CI_AS NULL 
) ON [PRIMARY]
GO

CREATE TABLE [dbo].[qGOODS_CKD] (
	[TM] [char] (8) COLLATE Chinese_PRC_CI_AS NULL ,
	[setTime] [datetime] NULL ,
	[user] [char] (10) COLLATE Chinese_PRC_CI_AS NULL ,
	[xstat] [bit] NULL ,
	[sl] [int] NULL ,
	[mdian] [char] (50) COLLATE Chinese_PRC_CI_AS NULL ,
	[DH] [char] (10) COLLATE Chinese_PRC_CI_AS NULL ,
	[mdianuser] [char] (10) COLLATE Chinese_PRC_CI_AS NULL 
) ON [PRIMARY]
GO

CREATE TABLE [dbo].[qGOODS_CKU] (
	[TM] [char] (8) COLLATE Chinese_PRC_CI_AS NULL ,
	[NAME] [char] (50) COLLATE Chinese_PRC_CI_AS NULL 
) ON [PRIMARY]
GO

CREATE TABLE [dbo].[qGOODS_CKU2] (
	[TM] [char] (8) COLLATE Chinese_PRC_CI_AS NULL ,
	[NAME] [char] (20) COLLATE Chinese_PRC_CI_AS NULL 
) ON [PRIMARY]
GO

CREATE TABLE [dbo].[qGOODS_CUSTJF] (
	[KH] [char] (20) COLLATE Chinese_PRC_CI_AS NULL ,
	[JF] [int] NULL 
) ON [PRIMARY]
GO

CREATE TABLE [dbo].[qGOODS_CUSTJF_LOG] (
	[KH] [char] (20) COLLATE Chinese_PRC_CI_AS NULL ,
	[JF] [int] NULL ,
	[SETTIME] [datetime] NULL ,
	[MDIAN] [char] (50) COLLATE Chinese_PRC_CI_AS NULL ,
	[SBM] [char] (14) COLLATE Chinese_PRC_CI_AS NULL 
) ON [PRIMARY]
GO

CREATE TABLE [dbo].[qGOODS_CUST_ZK] (
	[SBM] [char] (14) COLLATE Chinese_PRC_CI_AS NULL ,
	[ZK] [float] NULL 
) ON [PRIMARY]
GO

CREATE TABLE [dbo].[qGOODS_GYS_LIST] (
	[DH] [char] (9) COLLATE Chinese_PRC_CI_AS NULL ,
	[TM] [char] (8) COLLATE Chinese_PRC_CI_AS NULL ,
	[NAME] [char] (30) COLLATE Chinese_PRC_CI_AS NULL ,
	[SETTIME] [datetime] NULL ,
	[SLIANG] [int] NULL 
) ON [PRIMARY]
GO

CREATE TABLE [dbo].[qGOODS_KUT] (
	[TM] [char] (10) COLLATE Chinese_PRC_CI_AS NULL ,
	[SETTIME] [datetime] NULL ,
	[USER] [char] (10) COLLATE Chinese_PRC_CI_AS NULL ,
	[YY] [text] COLLATE Chinese_PRC_CI_AS NULL ,
	[SBM] [char] (14) COLLATE Chinese_PRC_CI_AS NULL ,
	[SLIANG] [int] NULL 
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO

CREATE TABLE [dbo].[qGOODS_LOG] (
	[tm] [char] (8) COLLATE Chinese_PRC_CI_AS NULL ,
	[stat] [char] (4) COLLATE Chinese_PRC_CI_AS NULL ,
	[user] [char] (10) COLLATE Chinese_PRC_CI_AS NULL ,
	[setdate] [datetime] NULL ,
	[mdian] [char] (50) COLLATE Chinese_PRC_CI_AS NULL ,
	[mdianuser] [char] (10) COLLATE Chinese_PRC_CI_AS NULL ,
	[sj] [float] NULL ,
	[ssj] [float] NULL ,
	[tj] [float] NULL ,
	[dh] [char] (10) COLLATE Chinese_PRC_CI_AS NULL ,
	[cbei] [float] NULL 
) ON [PRIMARY]
GO

CREATE TABLE [dbo].[qGOODS_RKD] (
	[dh] [char] (20) COLLATE Chinese_PRC_CI_AS NULL ,
	[tm] [char] (8) COLLATE Chinese_PRC_CI_AS NULL ,
	[setdate] [datetime] NULL ,
	[user] [char] (10) COLLATE Chinese_PRC_CI_AS NULL ,
	[sl] [int] NULL 
) ON [PRIMARY]
GO

CREATE TABLE [dbo].[qGOODS_RKD_DR] (
	[DH] [char] (20) COLLATE Chinese_PRC_CI_AS NULL ,
	[USER] [char] (10) COLLATE Chinese_PRC_CI_AS NULL 
) ON [PRIMARY]
GO

CREATE TABLE [dbo].[qGOODS_SALES] (
	[TM] [char] (8) COLLATE Chinese_PRC_CI_AS NULL ,
	[CBEI] [float] NULL ,
	[SALE] [float] NULL ,
	[SSALE] [float] NULL ,
	[MDIAN] [char] (50) COLLATE Chinese_PRC_CI_AS NULL ,
	[USER] [char] (10) COLLATE Chinese_PRC_CI_AS NULL ,
	[KHU] [char] (20) COLLATE Chinese_PRC_CI_AS NULL ,
	[SETTIME] [datetime] NULL ,
	[ZKOU] [float] NULL ,
	[SBM] [char] (14) COLLATE Chinese_PRC_CI_AS NULL ,
	[DWEI] [char] (2) COLLATE Chinese_PRC_CI_AS NULL ,
	[SLIANG] [int] NULL ,
	[KEHUID] [char] (20) COLLATE Chinese_PRC_CI_AS NULL ,
	[DQUSEN] [char] (10) COLLATE Chinese_PRC_CI_AS NULL ,
	[DQUSI] [char] (10) COLLATE Chinese_PRC_CI_AS NULL 
) ON [PRIMARY]
GO

CREATE TABLE [dbo].[qGOODS_SALES_BACK] (
	[TM] [char] (8) COLLATE Chinese_PRC_CI_AS NULL ,
	[SETTIME] [datetime] NULL ,
	[SLIANG] [int] NULL ,
	[DWEI] [char] (2) COLLATE Chinese_PRC_CI_AS NULL ,
	[SALE] [float] NULL ,
	[TJIA] [float] NULL ,
	[KHU] [char] (10) COLLATE Chinese_PRC_CI_AS NULL ,
	[USER] [char] (10) COLLATE Chinese_PRC_CI_AS NULL ,
	[ZKOU] [float] NULL ,
	[SSALE] [float] NULL ,
	[MDIAN] [char] (50) COLLATE Chinese_PRC_CI_AS NULL ,
	[BEZU] [text] COLLATE Chinese_PRC_CI_AS NULL 
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO

CREATE TABLE [dbo].[qGOODS_STOCK] (
	[TM] [char] (8) COLLATE Chinese_PRC_CI_AS NULL ,
	[MDIAN] [char] (50) COLLATE Chinese_PRC_CI_AS NULL ,
	[SETDATE] [datetime] NULL 
) ON [PRIMARY]
GO

CREATE TABLE [dbo].[qGOODS_XSED_LOG] (
	[DH] [char] (10) COLLATE Chinese_PRC_CI_AS NULL ,
	[TM] [char] (8) COLLATE Chinese_PRC_CI_AS NULL ,
	[MONY] [float] NULL ,
	[SETDATE] [datetime] NULL ,
	[USER] [char] (10) COLLATE Chinese_PRC_CI_AS NULL ,
	[STAT] [char] (4) COLLATE Chinese_PRC_CI_AS NULL ,
	[BLU] [float] NULL ,
	[CBEI] [float] NULL 
) ON [PRIMARY]
GO

CREATE TABLE [dbo].[qGOODS_YHUI] (
	[SBM] [char] (14) COLLATE Chinese_PRC_CI_AS NULL ,
	[YH] [int] NULL 
) ON [PRIMARY]
GO

CREATE TABLE [dbo].[qGYSHANG] (
	[id] [int] IDENTITY (1, 1) NOT NULL ,
	[name] [char] (50) COLLATE Chinese_PRC_CI_AS NULL ,
	[user] [char] (10) COLLATE Chinese_PRC_CI_AS NULL ,
	[tel] [char] (15) COLLATE Chinese_PRC_CI_AS NULL ,
	[czhen] [char] (15) COLLATE Chinese_PRC_CI_AS NULL ,
	[dzhi] [char] (50) COLLATE Chinese_PRC_CI_AS NULL ,
	[email] [char] (20) COLLATE Chinese_PRC_CI_AS NULL ,
	[dqusen] [char] (10) COLLATE Chinese_PRC_CI_AS NULL ,
	[dqusi] [char] (10) COLLATE Chinese_PRC_CI_AS NULL ,
	[time] [datetime] NULL 
) ON [PRIMARY]
GO

CREATE TABLE [dbo].[qJDU] (
	[name] [char] (20) COLLATE Chinese_PRC_CI_AS NOT NULL 
) ON [PRIMARY]
GO

CREATE TABLE [dbo].[qJLIAO] (
	[name] [char] (20) COLLATE Chinese_PRC_CI_AS NOT NULL 
) ON [PRIMARY]
GO

CREATE TABLE [dbo].[qKHU] (
	[id] [int] IDENTITY (1, 1) NOT NULL ,
	[xin] [char] (4) COLLATE Chinese_PRC_CI_AS NULL ,
	[min] [char] (10) COLLATE Chinese_PRC_CI_AS NULL ,
	[sji] [char] (12) COLLATE Chinese_PRC_CI_AS NULL ,
	[xbie] [char] (2) COLLATE Chinese_PRC_CI_AS NULL ,
	[nlin] [int] NULL ,
	[sri] [datetime] NULL ,
	[email] [char] (30) COLLATE Chinese_PRC_CI_AS NULL ,
	[dqusen] [char] (15) COLLATE Chinese_PRC_CI_AS NULL ,
	[dqusi] [char] (15) COLLATE Chinese_PRC_CI_AS NULL ,
	[dzhi] [char] (50) COLLATE Chinese_PRC_CI_AS NULL ,
	[ahao] [text] COLLATE Chinese_PRC_CI_AS NULL ,
	[zye] [char] (20) COLLATE Chinese_PRC_CI_AS NULL ,
	[khuhao] [char] (20) COLLATE Chinese_PRC_CI_AS NULL ,
	[zk] [float] NULL 
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO

CREATE TABLE [dbo].[qMDIAN] (
	[id] [int] IDENTITY (1, 1) NOT NULL ,
	[name] [char] (50) COLLATE Chinese_PRC_CI_AS NULL ,
	[user] [char] (10) COLLATE Chinese_PRC_CI_AS NULL ,
	[tel] [char] (15) COLLATE Chinese_PRC_CI_AS NULL ,
	[czhen] [char] (15) COLLATE Chinese_PRC_CI_AS NULL ,
	[dzhi] [char] (50) COLLATE Chinese_PRC_CI_AS NULL ,
	[email] [char] (20) COLLATE Chinese_PRC_CI_AS NULL ,
	[dqusen] [char] (10) COLLATE Chinese_PRC_CI_AS NULL ,
	[dqusi] [char] (10) COLLATE Chinese_PRC_CI_AS NULL ,
	[time] [datetime] NULL 
) ON [PRIMARY]
GO

CREATE TABLE [dbo].[qQIG] (
	[name] [char] (15) COLLATE Chinese_PRC_CI_AS NOT NULL 
) ON [PRIMARY]
GO

CREATE TABLE [dbo].[qRKDH_LOG] (
	[NAME] [char] (10) COLLATE Chinese_PRC_CI_AS NULL ,
	[USER] [char] (10) COLLATE Chinese_PRC_CI_AS NULL 
) ON [PRIMARY]
GO

CREATE TABLE [dbo].[qSLIAO] (
	[name] [char] (20) COLLATE Chinese_PRC_CI_AS NOT NULL 
) ON [PRIMARY]
GO

CREATE TABLE [dbo].[qSSHI] (
	[name] [char] (20) COLLATE Chinese_PRC_CI_AS NOT NULL 
) ON [PRIMARY]
GO

CREATE TABLE [dbo].[qSTONG] (
	[id] [int] IDENTITY (1, 1) NOT NULL ,
	[cliid] [char] (8) COLLATE Chinese_PRC_CI_AS NULL ,
	[zfshi] [char] (4) COLLATE Chinese_PRC_CI_AS NULL ,
	[name] [char] (15) COLLATE Chinese_PRC_CI_AS NULL ,
	[sliang] [int] NULL ,
	[zliang] [float] NULL ,
	[xzuang] [char] (15) COLLATE Chinese_PRC_CI_AS NULL ,
	[jdu] [char] (15) COLLATE Chinese_PRC_CI_AS NULL ,
	[qig] [char] (15) COLLATE Chinese_PRC_CI_AS NULL ,
	[stdate] [datetime] NULL 
) ON [PRIMARY]
GO

CREATE TABLE [dbo].[qXZUANG] (
	[name] [char] (15) COLLATE Chinese_PRC_CI_AS NOT NULL 
) ON [PRIMARY]
GO

CREATE TABLE [dbo].[qYGONG] (
	[id] [int] IDENTITY (1, 1) NOT NULL ,
	[sid] [char] (5) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[name] [char] (15) COLLATE Chinese_PRC_CI_AS NULL ,
	[pwd] [char] (32) COLLATE Chinese_PRC_CI_AS NULL ,
	[tel] [char] (15) COLLATE Chinese_PRC_CI_AS NULL ,
	[dqusen] [char] (10) COLLATE Chinese_PRC_CI_AS NULL ,
	[dqusi] [char] (10) COLLATE Chinese_PRC_CI_AS NULL ,
	[jguan] [char] (30) COLLATE Chinese_PRC_CI_AS NULL ,
	[xbie] [char] (2) COLLATE Chinese_PRC_CI_AS NULL ,
	[mdian] [char] (30) COLLATE Chinese_PRC_CI_AS NULL ,
	[time] [datetime] NULL ,
	[sfzhen] [char] (20) COLLATE Chinese_PRC_CI_AS NULL 
) ON [PRIMARY]
GO

CREATE TABLE [dbo].[qYGONG_STAT] (
	[UserID] [char] (10) COLLATE Chinese_PRC_CI_AS NULL ,
	[UserSTAT] [char] (50) COLLATE Chinese_PRC_CI_AS NULL ,
	[UserZK] [float] NULL 
) ON [PRIMARY]
GO

CREATE TABLE [dbo].[qYSE] (
	[name] [char] (15) COLLATE Chinese_PRC_CI_AS NOT NULL 
) ON [PRIMARY]
GO

ALTER TABLE [dbo].[qCKU] ADD 
	CONSTRAINT [PK_qCKU] PRIMARY KEY  CLUSTERED 
	(
		[name]
	)  ON [PRIMARY] 
GO

ALTER TABLE [dbo].[qDWEI] ADD 
	CONSTRAINT [PK_qDWEI] PRIMARY KEY  CLUSTERED 
	(
		[name]
	)  ON [PRIMARY] 
GO

ALTER TABLE [dbo].[qGOODS] ADD 
	CONSTRAINT [DF_qGOODS_setTime] DEFAULT (getdate()) FOR [setTime],
	CONSTRAINT [PK_qGOODS] PRIMARY KEY  CLUSTERED 
	(
		[tm]
	)  ON [PRIMARY] 
GO

ALTER TABLE [dbo].[qGOODS_BACK] ADD 
	CONSTRAINT [DF_qGOODS_BACK_SETTIME] DEFAULT (getdate()) FOR [SETTIME]
GO

ALTER TABLE [dbo].[qGOODS_CBEI_LOG] ADD 
	CONSTRAINT [DF_qGOODS_CBEI_LOG_setdate] DEFAULT (getdate()) FOR [setdate]
GO

ALTER TABLE [dbo].[qGOODS_CKD] ADD 
	CONSTRAINT [DF_qGOODS_CKD_setTime] DEFAULT (getdate()) FOR [setTime]
GO

ALTER TABLE [dbo].[qGOODS_CUSTJF_LOG] ADD 
	CONSTRAINT [DF_qGOODS_CUSTJF_LOG_SETTIME] DEFAULT (getdate()) FOR [SETTIME]
GO

ALTER TABLE [dbo].[qGOODS_CUST_ZK] ADD 
	CONSTRAINT [DF_qGOODS_CUST_ZK_ZK] DEFAULT (1) FOR [ZK]
GO

ALTER TABLE [dbo].[qGOODS_GYS_LIST] ADD 
	CONSTRAINT [DF_qGOODS_GYS_LIST_SETTIME] DEFAULT (getdate()) FOR [SETTIME]
GO

ALTER TABLE [dbo].[qGOODS_KUT] ADD 
	CONSTRAINT [DF_qGOODS_KUT_SETTIME] DEFAULT (getdate()) FOR [SETTIME]
GO

ALTER TABLE [dbo].[qGOODS_LOG] ADD 
	CONSTRAINT [DF_qGOODS_LOG_setdate] DEFAULT (getdate()) FOR [setdate]
GO

ALTER TABLE [dbo].[qGOODS_RKD] ADD 
	CONSTRAINT [DF_qGOODS_A_setdate] DEFAULT (getdate()) FOR [setdate]
GO

ALTER TABLE [dbo].[qGOODS_SALES] ADD 
	CONSTRAINT [DF_qGOODS_SALES_SETTIME] DEFAULT (getdate()) FOR [SETTIME]
GO

ALTER TABLE [dbo].[qGOODS_SALES_BACK] ADD 
	CONSTRAINT [DF_qGOODS_SALES_BACK_SETTIME] DEFAULT (getdate()) FOR [SETTIME]
GO

ALTER TABLE [dbo].[qGOODS_STOCK] ADD 
	CONSTRAINT [DF_qGOODS_STOCK_SETDATE] DEFAULT (getdate()) FOR [SETDATE]
GO

ALTER TABLE [dbo].[qGOODS_XSED_LOG] ADD 
	CONSTRAINT [DF_qGOODS_XSED_LOG_SETDATE] DEFAULT (getdate()) FOR [SETDATE]
GO

ALTER TABLE [dbo].[qGYSHANG] ADD 
	CONSTRAINT [DF_qGYSHANG_time] DEFAULT (getdate()) FOR [time]
GO

ALTER TABLE [dbo].[qJDU] ADD 
	CONSTRAINT [PK_qJDU] PRIMARY KEY  CLUSTERED 
	(
		[name]
	)  ON [PRIMARY] 
GO

ALTER TABLE [dbo].[qJLIAO] ADD 
	CONSTRAINT [PK_qJLIAO] PRIMARY KEY  CLUSTERED 
	(
		[name]
	)  ON [PRIMARY] 
GO

ALTER TABLE [dbo].[qKHU] ADD 
	CONSTRAINT [DF_qKHU_zk] DEFAULT (1) FOR [zk]
GO

ALTER TABLE [dbo].[qMDIAN] ADD 
	CONSTRAINT [DF_qMDIAN_time] DEFAULT (getdate()) FOR [time]
GO

ALTER TABLE [dbo].[qQIG] ADD 
	CONSTRAINT [PK_qQIG] PRIMARY KEY  CLUSTERED 
	(
		[name]
	)  ON [PRIMARY] 
GO

ALTER TABLE [dbo].[qSLIAO] ADD 
	CONSTRAINT [PK_qSLIAO] PRIMARY KEY  CLUSTERED 
	(
		[name]
	)  ON [PRIMARY] 
GO

ALTER TABLE [dbo].[qSSHI] ADD 
	CONSTRAINT [PK_qSSHI] PRIMARY KEY  CLUSTERED 
	(
		[name]
	)  ON [PRIMARY] 
GO

ALTER TABLE [dbo].[qSTONG] ADD 
	CONSTRAINT [DF_qSTONG_stdate] DEFAULT (getdate()) FOR [stdate],
	CONSTRAINT [PK_qSTONG] PRIMARY KEY  CLUSTERED 
	(
		[id]
	)  ON [PRIMARY] 
GO

ALTER TABLE [dbo].[qXZUANG] ADD 
	CONSTRAINT [PK_qXZUANG] PRIMARY KEY  CLUSTERED 
	(
		[name]
	)  ON [PRIMARY] 
GO

ALTER TABLE [dbo].[qYGONG] ADD 
	CONSTRAINT [DF_qYGONG_time] DEFAULT (getdate()) FOR [time],
	CONSTRAINT [PK_qYGONG] PRIMARY KEY  CLUSTERED 
	(
		[sid]
	)  ON [PRIMARY] 
GO

ALTER TABLE [dbo].[qYGONG_STAT] ADD 
	CONSTRAINT [DF_qYGONG_STAT_UserZK] DEFAULT (1.0) FOR [UserZK]
GO

ALTER TABLE [dbo].[qYSE] ADD 
	CONSTRAINT [PK_qYSE] PRIMARY KEY  CLUSTERED 
	(
		[name]
	)  ON [PRIMARY] 
GO

SET QUOTED_IDENTIFIER OFF 
GO
SET ANSI_NULLS OFF 
GO

CREATE PROCEDURE DBO.GOODS_BACK_REK
@USER CHAR(10),
@TM CHAR(10),
@MDIAN CHAR(50)
AS

UPDATE QGOODS_BACK SET DH='{撤退}',[USERS]=@USER WHERE TM=@TM
UPDATE QGOODS SET XSTAT=1 WHERE TM=@TM
INSERT INTO QGOODS_LOG(
TM,STAT,[USER],MDIAN
)VALUES(
@TM,'撤退',@USER,@MDIAN
)
GO
SET QUOTED_IDENTIFIER OFF 
GO
SET ANSI_NULLS ON 
GO

SET QUOTED_IDENTIFIER ON 
GO
SET ANSI_NULLS OFF 
GO


CREATE PROCEDURE QGOODS_BACK_GET
@TM CHAR(8),
@SBM CHAR(14),
@DH CHAR(10),
@USER CHAR(10),
@CKU CHAR(20),
@ID INT
AS

UPDATE QGOODS SET XSTAT=1 WHERE (TM=@TM)
UPDATE QGOODS_CKU SET [NAME]=@CKU WHERE (TM=@TM)
UPDATE QGOODS_BACK SET DH=@DH,USERS=@USER WHERE (TM=@TM) AND ([ID]=@ID)
GO
SET QUOTED_IDENTIFIER OFF 
GO
SET ANSI_NULLS ON 
GO

SET QUOTED_IDENTIFIER ON 
GO
SET ANSI_NULLS OFF 
GO





CREATE PROCEDURE DBO.QZ_CBEI_LOG
@DH CHAR(9) = '000000000',@TM CHAR(8),@CBEI FLOAT,@USER CHAR(10),@STAT CHAR(4),
@JJA FLOAT,@PJJA FLOAT,@ZSJA FLOAT,@ZSJE FLOAT,@FSJA FLOAT,@FSJE FLOAT,@JGDJ FLOAT,@OTHER FLOAT,@JGSH FLOAT
AS

INSERT INTO QGOODS_CBEI_LOG(
DH,TM,CBEI,[USER],STAT,JJA,PJJA,ZSJA,ZSJE,FSJA,FSJE,JGDJ,OTHER,JGSH
)VALUES(
@DH,@TM,@CBEI,@USER,@STAT,@JJA,@PJJA,@ZSJA,@ZSJE,@FSJA,@FSJE,@JGDJ,@OTHER,@JGSH
)




GO
SET QUOTED_IDENTIFIER OFF 
GO
SET ANSI_NULLS ON 
GO

SET QUOTED_IDENTIFIER OFF 
GO
SET ANSI_NULLS OFF 
GO





CREATE PROCEDURE DBO.QZ_CKDEL
@TM CHAR(8),
@USER CHAR(10),
@CKU CHAR(20)
AS

DELETE FROM QGOODS_CKD WHERE TM=@TM
INSERT INTO QGOODS_LOG(
TM,STAT,[USER]
)VALUES(
@TM,'撤单',@USER
)
UPDATE QGOODS SET
XSTAT=1
WHERE TM=@TM

UPDATE QGOODS_CKU SET
[NAME]=@CKU
WHERE TM=@TM




GO
SET QUOTED_IDENTIFIER OFF 
GO
SET ANSI_NULLS ON 
GO

SET QUOTED_IDENTIFIER ON 
GO
SET ANSI_NULLS OFF 
GO


CREATE PROCEDURE DBO.QZ_CKDH_LOG
@TM CHAR(8),
@USER CHAR(10),
@MDIAN CHAR(50),
@DH CHAR(10),
@SL INT,
@RKDH CHAR(20),
@JIES FLOAT,
@RAC CHAR(5)
AS

INSERT INTO QGOODS_CKD(
DH,TM,[USER],[MDIAN],XSTAT,SL
)VALUES(
@DH,@TM,@USER,@MDIAN,1,@SL
)

IF @RAC='TRUE'
BEGIN
IF NOT EXISTS(SELECT DH FROM QGOODS_RKD_DR WHERE DH=@RKDH)
BEGIN
INSERT INTO QGOODS_RKD_DR(
DH,[USER]
)VALUES(
@RKDH,@USER
)
END
END
ELSE IF @RAC='FALSE'
BEGIN
IF NOT EXISTS(SELECT DH FROM QGOODS_BACK_DR WHERE DH=@RKDH)
BEGIN
INSERT INTO QGOODS_BACK_DR(
DH,[USER]
)VALUES(
@RKDH,@USER
)
END
END

UPDATE QGOODS SET XSTAT=0,JIES=@JIES WHERE (TM=@TM)
UPDATE QGOODS_CKU SET [NAME]=@MDIAN WHERE (TM=@TM)


INSERT INTO QGOODS_LOG(
TM,STAT,[USER],MDIAN,DH
)VALUES(
@TM,'发货',@USER,@MDIAN,@DH
)
GO
SET QUOTED_IDENTIFIER OFF 
GO
SET ANSI_NULLS ON 
GO

SET QUOTED_IDENTIFIER ON 
GO
SET ANSI_NULLS OFF 
GO

CREATE PROCEDURE DBO.QZ_CKD_SH
@TM CHAR(8),
@USER CHAR(10),
@MDIAN CHAR(50)
AS

/*
UPDATE QGOODS_CKD SET
XSTAT=0,
MDIANUSER=@USER
WHERE TM=@TM
*/

UPDATE QGOODS SET
XSTAT=1
WHERE TM=@TM

INSERT INTO QGOODS_LOG(
TM,STAT,MDIAN,MDIANUSER
)VALUES(
@TM,'收货',@MDIAN,@USER
)

IF EXISTS(SELECT TM FROM  QGOODS_STOCK WHERE TM=@TM)
BEGIN
DELETE FROM QGOODS_STOCK WHERE (TM=@TM)
INSERT INTO QGOODS_STOCK(
TM,MDIAN
)VALUES(
@TM,@MDIAN
)
END
ELSE
BEGIN
INSERT INTO QGOODS_STOCK(
TM,MDIAN
)VALUES(
@TM,@MDIAN
)
END
GO
SET QUOTED_IDENTIFIER OFF 
GO
SET ANSI_NULLS ON 
GO

SET QUOTED_IDENTIFIER OFF 
GO
SET ANSI_NULLS OFF 
GO





CREATE PROCEDURE DBO.QZ_GDOOSTJ_XSED
@TM CHAR(8),@XSOU INT,@USER CHAR(10),@CBEI FLOAT,@BLU FLOAT
AS

IF EXISTS(SELECT TM FROM QGOODS WHERE TM=@TM)
BEGIN

UPDATE QGOODS SET
XSOU=@XSOU,BLU=@BLU WHERE TM=@TM

INSERT INTO QGOODS_XSED_LOG(
TM,MONY,[USER],STAT,BLU,CBEI
)VALUES(
@TM,@XSOU,@USER,'调价',@BLU,@CBEI
)

END




GO
SET QUOTED_IDENTIFIER OFF 
GO
SET ANSI_NULLS ON 
GO

SET QUOTED_IDENTIFIER ON 
GO
SET ANSI_NULLS OFF 
GO





CREATE PROCEDURE DBO.QZ_GOODSADD
@TM CHAR(8),@JLIAO CHAR(20),@SLIAO CHAR(20),@SSI CHAR(20),@QKOU FLOAT,@ZSHU CHAR(30),@KUS CHAR(10),@HHAO CHAR(20),
@DWEI CHAR(2),@SLIANG INT,@JIANZ FLOAT,@JINZ FLOAT,@PJIANZ FLOAT,@BLU FLOAT,@ZSZ FLOAT,@ZSS INT,@FSZ FLOAT,@FSS INT,
@CBEI FLOAT,@XSOU FLOAT,@BZHU TEXT,@SLBOL BIT,@IMGBOL BIT,@JDU CHAR(10),@YSE CHAR(10),@XZUANG CHAR(10),@QGONG CHAR(10),
@CADI CHAR(10),@RKDH CHAR(9),@DDH CHAR(20),@GYS CHAR(30),@KUC CHAR(20),@USER CHAR(10),
@JJA FLOAT,@PJJA FLOAT,@ZSJA FLOAT,@ZSJE FLOAT,@FSJA FLOAT,@FSJE FLOAT,@JGDJ FLOAT,@OTHER FLOAT,@JGSH FLOAT
AS

IF NOT EXISTS(SELECT TM FROM QGOODS WHERE TM=@TM)
BEGIN

INSERT INTO QGOODS(
TM,JLIAO,SLIAO,SSI,QKOU,ZSHU,KUS,HHAO,DWEI,SLIANG,JIANZ,JINZ,PJIANZ,BLU,ZSZ,ZSS,FSZ,FSS,CBEI,XSOU,BZHU,SLBOL,IMGBOL,JDU,YSE,XZUANG,QGONG,XSTAT,CADI,DDH,JJA,PJJA,ZSJA,ZSJE,FSJA,FSJE,JGDJ,OTHER,JGSH,JIES
)VALUES(
@TM,@JLIAO,@SLIAO,@SSI,@QKOU,@ZSHU,@KUS,@HHAO,@DWEI,@SLIANG,@JIANZ,@JINZ,@PJIANZ,@BLU,@ZSZ,@ZSS,@FSZ,@FSS,@CBEI,@XSOU,@BZHU,@SLBOL,@IMGBOL,@JDU,@YSE,@XZUANG,@QGONG,1,@CADI,@DDH,@JJA,@PJJA,@ZSJA,@ZSJE,@FSJA,@FSJE,@JGDJ,@OTHER,@JGSH,@XSOU
)

EXEC QZ_GOODSADD_RKD @RKDH,@TM,@USER,@SLIANG
EXEC QZ_GOODS_LOG @TM,'入库',@USER,@RKDH,@CBEI
EXEC QZ_CBEI_LOG @RKDH,@TM,@CBEI,@USER,'初始',@JJA,@PJJA,@ZSJA,@ZSJE,@FSJA,@FSJE,@JGDJ,@OTHER,@JGSH
EXEC QZ_GOODS_XSED_LOG @RKDH,@TM,@XSOU,@USER,'初始',@BLU,@CBEI
EXEC QZ_GOODSADD_GYS @RKDH,@TM,@GYS,@SLIANG
EXEC QZ_GOODS_CKU @TM,@KUC

END

GO
SET QUOTED_IDENTIFIER OFF 
GO
SET ANSI_NULLS ON 
GO

SET QUOTED_IDENTIFIER OFF 
GO
SET ANSI_NULLS OFF 
GO





CREATE PROCEDURE DBO.QZ_GOODSADD_GYS
@RKDH CHAR(9),
@TM CHAR(8),
@GYS CHAR(30),
@SL INT
AS

INSERT INTO QGOODS_GYS_LIST(
DH,TM,[NAME],SLIANG
)VALUES(
@RKDH,@TM,@GYS,@SL
)




GO
SET QUOTED_IDENTIFIER OFF 
GO
SET ANSI_NULLS ON 
GO

SET QUOTED_IDENTIFIER OFF 
GO
SET ANSI_NULLS OFF 
GO





CREATE PROCEDURE DBO.QZ_GOODSADD_RKD
@DH CHAR(20),@TM CHAR(8),@USER CHAR(10),@SL INT
AS

INSERT INTO QGOODS_RKD(
DH,TM,[USER],SL
)VALUES(
@DH,@TM,@USER,@SL
)




GO
SET QUOTED_IDENTIFIER OFF 
GO
SET ANSI_NULLS ON 
GO

SET QUOTED_IDENTIFIER OFF 
GO
SET ANSI_NULLS OFF 
GO





CREATE PROCEDURE dbo.QZ_GOODSDEL
@TM CHAR(8),
@USER CHAR(10)
AS

DELETE FROM QGOODS_RKD WHERE TM=@TM
DELETE FROM QGOODS_XSED_LOG WHERE TM=@TM
DELETE FROM QGOODS_GYS_LIST WHERE TM=@TM
DELETE FROM QGOODS_CBEI_LOG WHERE TM=@TM
DELETE FROM QGOODS WHERE TM=@TM
DELETE FROM QGOODS_CKU WHERE TM=@TM
INSERT INTO QGOODS_LOG(
TM,STAT,[USER]
)VALUES(
@TM,'撤单',@USER
)




GO
SET QUOTED_IDENTIFIER OFF 
GO
SET ANSI_NULLS ON 
GO

SET QUOTED_IDENTIFIER OFF 
GO
SET ANSI_NULLS OFF 
GO





CREATE PROCEDURE DBO.QZ_GOODSTJ
@TM CHAR(8),@CBEI FLOAT,@USER CHAR(10),@JJA FLOAT,@PJJA FLOAT,@ZSJA FLOAT,@ZSJE FLOAT,@FSJA FLOAT,@FSJE FLOAT,@JGDJ FLOAT,
@OTHER FLOAT,@JGSH FLOAT,@ZSHI CHAR(50),@FSHI CHAR(50)
AS

IF EXISTS(SELECT TM FROM QGOODS WHERE TM=@TM)
BEGIN

UPDATE QGOODS SET
CBEI=@CBEI,JJA=@JJA,PJJA=@PJJA,ZSJA=@ZSJA,ZSJE=@ZSJE,FSJA=@FSJA,FSJE=@FSJE,JGDJ=@JGDJ,OTHER=@OTHER,JGSH=@JGSH
WHERE TM=@TM

INSERT INTO QGOODS_CBEI_LOG(
TM,CBEI,[USER],STAT,JJA,PJJA,ZSJA,ZSJE,FSJA,FSJE,JGDJ,OTHER,JGSH,ZSHI,FSHI
)VALUES(
@TM,@CBEI,@USER,'调价',@JJA,@PJJA,@ZSJA,@ZSJE,@FSJA,@FSJE,@JGDJ,@OTHER,@JGSH,@ZSHI,@FSHI
)

/*
INSERT INTO QGOODS_LOG(
TM,CBEI,[USER],STAT
)VALUES(
@TM,@CBEI,@USER,'调价'
)
*/
END




GO
SET QUOTED_IDENTIFIER OFF 
GO
SET ANSI_NULLS ON 
GO

SET QUOTED_IDENTIFIER ON 
GO
SET ANSI_NULLS OFF 
GO





CREATE PROCEDURE DBO.QZ_GOODS_BACK
@TM CHAR(8),
@USER CHAR(10),
@MDIAN CHAR(50),
@SLIANG INT,
@SBM CHAR(14),
@NTXT TEXT
AS

INSERT INTO QGOODS_BACK(
TM,[USER],MDIAN,SLIANG,SBM
)VALUES(
@TM,@USER,@MDIAN,@SLIANG,@SBM
)

INSERT INTO QGOODS_LOG(
TM,STAT,MDIAN,MDIANUSER
)VALUES(
@TM,'退货',@MDIAN,@USER
)

UPDATE QGOODS SET
XSTAT=0
WHERE TM=@TM

IF NOT EXISTS(SELECT SBM FROM QGOODS_BACK_TXT WHERE SBM=@SBM)
BEGIN
INSERT INTO QGOODS_BACK_TXT(
SBM,NTXT
)VALUES(
@SBM,@NTXT
)
END
GO
SET QUOTED_IDENTIFIER OFF 
GO
SET ANSI_NULLS ON 
GO

SET QUOTED_IDENTIFIER ON 
GO
SET ANSI_NULLS OFF 
GO





CREATE PROCEDURE DBO.QZ_GOODS_CKU
@TM CHAR(8),
@NAME CHAR(20)
AS

IF NOT EXISTS(SELECT TM FROM QGOODS_CKU WHERE TM=@TM)
BEGIN
INSERT INTO QGOODS_CKU(
TM,[NAME]
)VALUES(
@TM,@NAME
)
INSERT INTO QGOODS_CKU2(
TM,[NAME]
)VALUES(
@TM,@NAME
)
END
ELSE
BEGIN
UPDATE QGOODS_CKU SET
[NAME]=@NAME
WHERE TM = @TM
END

GO
SET QUOTED_IDENTIFIER OFF 
GO
SET ANSI_NULLS ON 
GO

SET QUOTED_IDENTIFIER ON 
GO
SET ANSI_NULLS OFF 
GO





CREATE PROCEDURE QZ_GOODS_KUT
@TM CHAR(8),
@USER CHAR(10),
@YY TEXT,
@SBM CHAR(14),
@SLIANG INT
AS

INSERT INTO QGOODS_KUT(
TM,[USER],YY,SBM,SLIANG
)VALUES(
@TM,@USER,@YY,@SBM,@SLIANG
)

UPDATE QGOODS SET XSTAT=0 WHERE TM=@TM

INSERT INTO QGOODS_LOG(
TM,STAT,[USER]
)VALUES(
@TM,'库退',@USER
)




GO
SET QUOTED_IDENTIFIER OFF 
GO
SET ANSI_NULLS ON 
GO

SET QUOTED_IDENTIFIER ON 
GO
SET ANSI_NULLS OFF 
GO





CREATE PROCEDURE DBO.QZ_GOODS_LOG
@TM CHAR(8),@STAT CHAR(4),@USER CHAR(10),@RKDH CHAR(9),@CBEI FLOAT
AS

INSERT INTO QGOODS_LOG(
TM,STAT,[USER],DH,CBEI
)VALUES(
@TM,@STAT,@USER,@RKDH,@CBEI
)




GO
SET QUOTED_IDENTIFIER OFF 
GO
SET ANSI_NULLS ON 
GO

SET QUOTED_IDENTIFIER ON 
GO
SET ANSI_NULLS OFF 
GO





CREATE PROCEDURE DBO.QZ_GOODS_XSED_LOG
@DH CHAR(10)='000000000',@TM CHAR(8),@MONY FLOAT,@USER CHAR(10),@STAT CHAR(4),@BLU FLOAT,@CBEI FLOAT
AS

INSERT INTO QGOODS_XSED_LOG(
DH,TM,MONY,[USER],STAT,BLU,CBEI
)VALUES(
@DH,@TM,@MONY,@USER,@STAT,@BLU,@CBEI
)




GO
SET QUOTED_IDENTIFIER OFF 
GO
SET ANSI_NULLS ON 
GO

SET QUOTED_IDENTIFIER OFF 
GO
SET ANSI_NULLS OFF 
GO





CREATE PROCEDURE DBO.QZ_KHU_JF
@KHAO CHAR(20),
@JF INT,
@MDIAN CHAR(50)='总部'
AS

IF EXISTS(SELECT KH FROM QGOODS_CUSTJF WHERE KH=@KHAO)
BEGIN

DECLARE @JFNUM INT
SELECT @JFNUM=JF FROM QGOODS_CUSTJF WHERE KH=@KHAO
SET @JFNUM = @JFNUM + @JF
UPDATE QGOODS_CUSTJF SET JF=@JFNUM WHERE KH=@KHAO

END
ELSE
BEGIN

INSERT INTO QGOODS_CUSTJF(
KH,JF
)VALUES(
@KHAO,@JF
)
END

INSERT INTO QGOODS_CUSTJF_LOG(
KH,JF,MDIAN
)VALUES(
@KHAO,@JF,@MDIAN
)


GO
SET QUOTED_IDENTIFIER OFF 
GO
SET ANSI_NULLS ON 
GO

SET QUOTED_IDENTIFIER ON 
GO
SET ANSI_NULLS OFF 
GO





CREATE PROCEDURE DBO.QZ_RKDH_LOG
@NAME CHAR(10),
@USER CHAR(10)
AS

IF NOT EXISTS(SELECT [NAME] FROM QRKDH_LOG WHERE NAME=@NAME)
BEGIN

INSERT INTO QRKDH_LOG(
[NAME],[USER]
)VALUES(
@NAME,@USER
)

END




GO
SET QUOTED_IDENTIFIER OFF 
GO
SET ANSI_NULLS ON 
GO

SET QUOTED_IDENTIFIER OFF 
GO
SET ANSI_NULLS OFF 
GO



CREATE PROCEDURE DBO.QZ_SALES
@TM CHAR(8),
@CBEI FLOAT,
@SALE FLOAT,
@SSALE FLOAT,
@MDIAN CHAR(50),
@USER CHAR(20),
@KHU CHAR(20),
@ZKOU FLOAT,
@SBM CHAR(14),
@SLIANG INT,
@DWEI CHAR(2),
@DQUSEN CHAR(10),
@DQUSI CHAR(10)
AS

DECLARE @KHUID CHAR(20)
IF @KHU <> '普通客户'
BEGIN
SET @KHUID = @KHU
END
ELSE
BEGIN
SET @KHUID = '00000000'
END

IF @KHU <> '普通客户'
BEGIN
DECLARE @XIN CHAR(10)
DECLARE @MIN CHAR(20)
SELECT @XIN=XIN FROM QKHU WHERE KHUHAO=@KHU
SELECT @MIN=[MIN] FROM QKHU WHERE KHUHAO=@KHU
SET @KHU = RTRIM(@XIN) + RTRIM(@MIN)
END

INSERT INTO QGOODS_SALES(
TM,CBEI,SALE,SSALE,MDIAN,[USER],KHU,ZKOU,SBM,SLIANG,DWEI,KEHUID,DQUSEN,DQUSI
)VALUES(
@TM,@CBEI,@SALE,@SSALE,@MDIAN,@USER,@KHU,@ZKOU,@SBM,@SLIANG,@DWEI,@KHUID,@DQUSEN,@DQUSI
)

INSERT INTO QGOODS_LOG(
TM,STAT,MDIAN,[USER],SJ,SSJ,CBEI
)VALUES(
@TM,'销售',@MDIAN,@USER,@SALE,@SSALE,@CBEI
)

UPDATE QGOODS SET
XSTAT=0
WHERE TM=@TM


GO
SET QUOTED_IDENTIFIER OFF 
GO
SET ANSI_NULLS ON 
GO

SET QUOTED_IDENTIFIER OFF 
GO
SET ANSI_NULLS OFF 
GO





CREATE PROCEDURE DBO.QZ_SALE_BACK
@TM CHAR(8),
@SLIANG INT,
@DWEI CHAR(2),
@SALE FLOAT,
@TJIA FLOAT,
@KHU CHAR(10),
@USER CHAR(10),
@ZKOU FLOAT,
@SSALE FLOAT,
@MDIAN CHAR(50)
AS

INSERT INTO QGOODS_SALES_BACK(
TM,SLIANG,DWEI,SALE,TJIA,KHU,[USER],ZKOU,SSALE,MDIAN
)VALUES(
@TM,@SLIANG,@DWEI,@SALE,@TJIA,@KHU,@USER,@ZKOU,@SSALE,@MDIAN
)

UPDATE QGOODS SET XSTAT=1 WHERE (TM=@TM)

INSERT INTO QGOODS_LOG(
TM,STAT,MDIAN,MDIANUSER,SJ,SSJ,TJ
)VALUES(
@TM,'销退',@MDIAN,@USER,@SALE,@SSALE,@TJIA
)
GO
SET QUOTED_IDENTIFIER OFF 
GO
SET ANSI_NULLS ON 
GO

SET QUOTED_IDENTIFIER OFF 
GO
SET ANSI_NULLS OFF 
GO





CREATE PROCEDURE DBO.QZ_YGONG_ADD
@SID CHAR(5),
@NAME CHAR(15),
@PWD CHAR(32),
@TEL CHAR(15),
@JGUAN CHAR(30),
@XBIE CHAR(2),
@MDIAN CHAR(30),
@SFZHEN CHAR(20)
AS

IF NOT EXISTS(SELECT SID FROM QYGONG WHERE (SID=@SID))
BEGIN

INSERT INTO QYGONG(
SID,[NAME],PWD,TEL,JGUAN,XBIE,MDIAN,SFZHEN
)VALUES(
@SID,@NAME,@PWD,@TEL,@JGUAN,@XBIE,@MDIAN,@SFZHEN
)

INSERT INTO QYGONG_STAT(
USERID,USERSTAT,USERZK
)VALUES(
@SID,'true,false,false,false,false,false,false','1.0'
)

END




GO
SET QUOTED_IDENTIFIER OFF 
GO
SET ANSI_NULLS ON 
GO

SET QUOTED_IDENTIFIER ON 
GO
SET ANSI_NULLS OFF 
GO



CREATE PROCEDURE QZ_YHUI
@KHUID CHAR(20),
@SBM CHAR(14),
@JF INT,
@MDIAN CHAR(50),
@ZK FLOAT
AS

IF EXISTS(SELECT KH FROM QGOODS_CUSTJF WHERE KH=@KHUID) AND @KHUID<>''
BEGIN
DECLARE @SJF INT
SET @SJF = @JF * 50
DECLARE @SSJF INT
SELECT @SSJF = JF FROM QGOODS_CUSTJF WHERE KH=@KHUID
SET @SSJF = @SSJF - @SJF
UPDATE QGOODS_CUSTJF SET JF=@SSJF WHERE KH=@KHUID

SET @SJF = -@SJF
INSERT INTO QGOODS_CUSTJF_LOG(
KH,JF,MDIAN,SBM
)VALUES(
@KHUID,@SJF,@MDIAN,@SBM
)
END

INSERT INTO QGOODS_YHUI(
SBM,YH
)VALUES(
@SBM,@JF
)

INSERT INTO QGOODS_CUST_ZK(
SBM,ZK
)VALUES(
@SBM,@ZK
)


GO
SET QUOTED_IDENTIFIER OFF 
GO
SET ANSI_NULLS ON 
GO

