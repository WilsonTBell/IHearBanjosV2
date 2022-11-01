CREATE TABLE [Banjoists] (
  [Id] integer PRIMARY KEY IDENTITY(1, 1),
  [Name] nvarchar(255),
  [Email] nvarchar(255),
  [UserTypeId] integer NOT NULL,
  [FirebaseUserId] nvarchar(255) NOT NULL
)
GO

CREATE TABLE [UserTypes] (
  [Id] integer PRIMARY KEY IDENTITY(1, 1),
  [Name] nvarchar(255)
)
GO

CREATE TABLE [Tabs] (
  [Id] integer PRIMARY KEY IDENTITY(1, 1),
  [Title] nvarchar(255),
  [Image] varbinary(max),
  [Description] nvarchar(255),
  [BanjoistId] integer NOT NULL,
  [TypeId] integer NOT NULL,
  [DifficultyId] integer NOT NULL
)
GO

CREATE TABLE [Types] (
  [Id] integer PRIMARY KEY IDENTITY(1, 1),
  [Name] nvarchar(255)
)
GO

CREATE TABLE [Difficulties] (
  [Id] integer PRIMARY KEY IDENTITY(1, 1),
  [Name] nvarchar(255)
)
GO

CREATE TABLE [BanjoistFavorites] (
  [Id] integer PRIMARY KEY IDENTITY(1, 1),
  [BanjoistId] integer NOT NULL,
  [TabId] integer NOT NULL
)
GO

ALTER TABLE [Tabs] ADD FOREIGN KEY ([DifficultyId]) REFERENCES [Difficulties] ([Id])
GO

ALTER TABLE [Tabs] ADD FOREIGN KEY ([TypeId]) REFERENCES [Types] ([Id])
GO

ALTER TABLE [Tabs] ADD FOREIGN KEY ([BanjoistId]) REFERENCES [Banjoists] ([Id])
GO

ALTER TABLE [BanjoistFavorites] ADD FOREIGN KEY ([BanjoistId]) REFERENCES [Banjoists] ([Id])
GO

ALTER TABLE [BanjoistFavorites] ADD FOREIGN KEY ([TabId]) REFERENCES [Tabs] ([Id]) ON DELETE CASCADE
GO

ALTER TABLE [Banjoists] ADD FOREIGN KEY ([UserTypeId]) REFERENCES [UserTypes] ([Id])
GO
