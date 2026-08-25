-- Plato Assistant persists its user preferences and conversations in a SQL Server database. 
-- The database must have the following tables present and owned by dbo.

-- Also be sure to grant Insert, Update, Delete, and Select permissions to the 
-- user login passed in the connection string. Use these table definitions:

CREATE TABLE PlatoConversationAppSettings (
	OwnerId uniqueidentifier NULL,
    Theme NVARCHAR(50) NOT NULL default 'color',
	Zoom FLOAT NULL default 1,
	CommandSideBarPinned bit not null default 1,
	ConversationSideBarPinned bit not null default 1

);
go

CREATE TABLE PlatoConversations (
	OwnerId uniqueidentifier NULL, -- Analyzer IDs or
	EntityOwnerId uniqueidentifier NULL,
    ConversationId UNIQUEIDENTIFIER NOT NULL PRIMARY KEY,
    TabId NVARCHAR(100) NOT NULL,
	Theme NVARCHAR(50) NOT NULL DEFAULT 'dark',
	Title NVARCHAR(200),
	DisplayOrder INT NULL,
    LastAccessedAt DATETIME2 NULL,
	CreatedAt DATETIME2 NOT NULL,
    UpdatedAt DATETIME2 NULL,
	IsOpen bit NOT NULL default 0
);
go

CREATE TABLE PlatoConversationSummaries
(
    ConversationId      nvarchar(100) NOT NULL PRIMARY KEY,
    SummaryText         nvarchar(max) NOT NULL,
    ThroughTurnId       nvarchar(100) NULL,
    ThroughCreatedAt    datetime2 NULL,
    UpdatedAt           datetime2 NOT NULL DEFAULT SYSUTCDATETIME(),
)
go


CREATE TABLE PlatoConversationTurns (
    TurnId NVARCHAR(100) NOT NULL PRIMARY KEY,
    ConversationId UNIQUEIDENTIFIER NOT NULL,
    TabId NVARCHAR(100) NOT NULL,
    Role NVARCHAR(20) NOT NULL,        -- user | assistant
    Markdown NVARCHAR(MAX) NULL,
    Model NVARCHAR(100) NULL,
	FileSearch bit NULL,
	WebSearch bit NULL,
    Reasoning NVARCHAR(50) NULL,
    Elapsed NVARCHAR(20) NULL,
    Finalized BIT NOT NULL DEFAULT 0,
    CreatedAt DATETIME2 NOT NULL,
    UpdatedAt DATETIME2 NULL,
    ParentUserTurnId NVARCHAR(100) NULL,
	[Hidden] bit,
	IsWelcome bit,
	[Messages] nvarchar(max),
	PromptTokens int NULL
);
go


CREATE TABLE PlatoConversationArtifacts (
    ArtifactId UNIQUEIDENTIFIER NOT NULL PRIMARY KEY,
    ConversationId UNIQUEIDENTIFIER NOT NULL,
    TabId NVARCHAR(100) NOT NULL,
    TurnId NVARCHAR(100) NOT NULL,
    Path NVARCHAR(500) NOT NULL,
    Name NVARCHAR(255) NULL,
    SizeBytes BIGINT NULL,
    CreatedAt DATETIME2 NOT NULL,
	Content varbinary(max),
	ContentType NVARCHAR(120) NULL
);
go

