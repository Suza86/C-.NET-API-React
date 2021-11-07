CREATE TABLE [dbo].[HeistMember]
(
	[HeistMemberId] INT NOT NULL PRIMARY KEY, 
    [Name] NCHAR(30) NULL, 
    [Sex] NCHAR(1) NULL, 
    [Email] NCHAR(30) NULL, 
    [Skills] NCHAR(20) NULL, 
    [MainSkill] NCHAR(20) NULL, 
    [Status] NCHAR(20) NULL
)
