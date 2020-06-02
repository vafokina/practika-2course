
-- --------------------------------------------------
-- Entity Designer DDL Script for SQL Server 2005, 2008, 2012 and Azure
-- --------------------------------------------------
-- Date Created: 05/23/2020 23:17:41
-- Generated from EDMX file: C:\Users\vikon\Desktop\DeliveryServiceWebDBServer\DeliveryServiceWebDBServer\Models\ModelDB.edmx
-- --------------------------------------------------

SET QUOTED_IDENTIFIER OFF;
GO
USE [database];
GO
IF SCHEMA_ID(N'dbo') IS NULL EXECUTE(N'CREATE SCHEMA [dbo]');
GO

-- --------------------------------------------------
-- Dropping existing FOREIGN KEY constraints
-- --------------------------------------------------

IF OBJECT_ID(N'[dbo].[FK_UsersAccountReferences]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[AccountReferences] DROP CONSTRAINT [FK_UsersAccountReferences];
GO
IF OBJECT_ID(N'[dbo].[FK_PersonsAccountReferences]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[AccountReferences] DROP CONSTRAINT [FK_PersonsAccountReferences];
GO
IF OBJECT_ID(N'[dbo].[FK_DistributionCentresAccountReferences]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[AccountReferences] DROP CONSTRAINT [FK_DistributionCentresAccountReferences];
GO
IF OBJECT_ID(N'[dbo].[FK_UsersUserPackages]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[UserPackages] DROP CONSTRAINT [FK_UsersUserPackages];
GO
IF OBJECT_ID(N'[dbo].[FK_PackagesUserPackages]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[UserPackages] DROP CONSTRAINT [FK_PackagesUserPackages];
GO
IF OBJECT_ID(N'[dbo].[FK_CountriesRegions]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Regions] DROP CONSTRAINT [FK_CountriesRegions];
GO
IF OBJECT_ID(N'[dbo].[FK_RegionsCities]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Cities] DROP CONSTRAINT [FK_RegionsCities];
GO
IF OBJECT_ID(N'[dbo].[FK_DistributionCentresCities]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[DistributionCentres] DROP CONSTRAINT [FK_DistributionCentresCities];
GO
IF OBJECT_ID(N'[dbo].[FK_DistributionCentresPersons]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Persons] DROP CONSTRAINT [FK_DistributionCentresPersons];
GO
IF OBJECT_ID(N'[dbo].[FK_CitiesPersons]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Persons] DROP CONSTRAINT [FK_CitiesPersons];
GO
IF OBJECT_ID(N'[dbo].[FK_PersonsPackages]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Packages] DROP CONSTRAINT [FK_PersonsPackages];
GO
IF OBJECT_ID(N'[dbo].[FK_PersonsPackages1]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Packages] DROP CONSTRAINT [FK_PersonsPackages1];
GO
IF OBJECT_ID(N'[dbo].[FK_TariffsPackages]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Packages] DROP CONSTRAINT [FK_TariffsPackages];
GO
IF OBJECT_ID(N'[dbo].[FK_PackagesRecords]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Records] DROP CONSTRAINT [FK_PackagesRecords];
GO
IF OBJECT_ID(N'[dbo].[FK_DistributionCentresRecords]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Records] DROP CONSTRAINT [FK_DistributionCentresRecords];
GO

-- --------------------------------------------------
-- Dropping existing tables
-- --------------------------------------------------

IF OBJECT_ID(N'[dbo].[Users]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Users];
GO
IF OBJECT_ID(N'[dbo].[AccountReferences]', 'U') IS NOT NULL
    DROP TABLE [dbo].[AccountReferences];
GO
IF OBJECT_ID(N'[dbo].[UserPackages]', 'U') IS NOT NULL
    DROP TABLE [dbo].[UserPackages];
GO
IF OBJECT_ID(N'[dbo].[Persons]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Persons];
GO
IF OBJECT_ID(N'[dbo].[Packages]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Packages];
GO
IF OBJECT_ID(N'[dbo].[Tariffs]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Tariffs];
GO
IF OBJECT_ID(N'[dbo].[Records]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Records];
GO
IF OBJECT_ID(N'[dbo].[DistributionCentres]', 'U') IS NOT NULL
    DROP TABLE [dbo].[DistributionCentres];
GO
IF OBJECT_ID(N'[dbo].[Cities]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Cities];
GO
IF OBJECT_ID(N'[dbo].[Regions]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Regions];
GO
IF OBJECT_ID(N'[dbo].[Countries]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Countries];
GO

-- --------------------------------------------------
-- Creating all tables
-- --------------------------------------------------

-- Creating table 'Users'
CREATE TABLE [dbo].[Users] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Login] nvarchar(50)  NOT NULL,
    [Password] nvarchar(50)  NOT NULL,
    [Admin] bit  NOT NULL
);
GO

-- Creating table 'AccountReferences'
CREATE TABLE [dbo].[AccountReferences] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [UserId] int  NOT NULL,
    [PersonId] int  NULL,
    [CentreId] int  NULL
);
GO

-- Creating table 'UserPackages'
CREATE TABLE [dbo].[UserPackages] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [UserId] int  NOT NULL,
    [PackageId] int  NOT NULL
);
GO

-- Creating table 'Persons'
CREATE TABLE [dbo].[Persons] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Name] nvarchar(50)  NULL,
    [MiddleName] nvarchar(50)  NULL,
    [Surname] nvarchar(50)  NULL,
    [Company] nvarchar(50)  NULL,
    [Phone] nvarchar(50)  NULL,
    [Mail] nvarchar(50)  NULL,
    [Index] nvarchar(50)  NULL,
    [CityId] int  NULL,
    [Address] nvarchar(50)  NULL,
    [CentreId] int  NULL,
    [InformingSMS] bit  NOT NULL,
    [InformingMail] bit  NOT NULL
);
GO

-- Creating table 'Packages'
CREATE TABLE [dbo].[Packages] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [PersonIdFrom] int NOT NULL,
    [PersonIdTo] int NOT NULL,
    [TariffId] int NULL,
    [Description] nvarchar(50)  NULL,
    [Weight] float  NULL,
    [Length] float  NULL,
    [Width] float  NULL,
    [Height] float  NULL,
    [NumberOfPackages] int  NOT NULL,
    [Cost] float  NULL,
    [DeclaredValue] float  NULL
);
GO

-- Creating table 'Tariffs'
CREATE TABLE [dbo].[Tariffs] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Name] nvarchar(50)  NOT NULL,
    [Comment] nvarchar(50)  NULL,
    [Formula] nvarchar(50)  NOT NULL
);
GO

-- Creating table 'Records'
CREATE TABLE [dbo].[Records] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [PackageId] int  NOT NULL,
    [CentreId] int  NULL,
    [DateAndTime] datetime  NOT NULL,
    [Status] nvarchar(50)  NOT NULL
);
GO

-- Creating table 'DistributionCentres'
CREATE TABLE [dbo].[DistributionCentres] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Index] nvarchar(50)  NULL,
    [CityId] int  NOT NULL,
    [Address] nvarchar(50)  NOT NULL,
    [Description] nvarchar(50)  NULL
);
GO

-- Creating table 'Cities'
CREATE TABLE [dbo].[Cities] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [NameCity] nvarchar(50)  NOT NULL,
    [RegionId] int  NOT NULL
);
GO

-- Creating table 'Regions'
CREATE TABLE [dbo].[Regions] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [NameRegion] nvarchar(50)  NOT NULL,
    [CountryId] int  NOT NULL
);
GO

-- Creating table 'Countries'
CREATE TABLE [dbo].[Countries] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [NameCountry] nvarchar(50)  NOT NULL
);
GO

-- --------------------------------------------------
-- Creating all PRIMARY KEY constraints
-- --------------------------------------------------

-- Creating primary key on [Id] in table 'Users'
ALTER TABLE [dbo].[Users]
ADD CONSTRAINT [PK_Users]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'AccountReferences'
ALTER TABLE [dbo].[AccountReferences]
ADD CONSTRAINT [PK_AccountReferences]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'UserPackages'
ALTER TABLE [dbo].[UserPackages]
ADD CONSTRAINT [PK_UserPackages]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'Persons'
ALTER TABLE [dbo].[Persons]
ADD CONSTRAINT [PK_Persons]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'Packages'
ALTER TABLE [dbo].[Packages]
ADD CONSTRAINT [PK_Packages]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'Tariffs'
ALTER TABLE [dbo].[Tariffs]
ADD CONSTRAINT [PK_Tariffs]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'Records'
ALTER TABLE [dbo].[Records]
ADD CONSTRAINT [PK_Records]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'DistributionCentres'
ALTER TABLE [dbo].[DistributionCentres]
ADD CONSTRAINT [PK_DistributionCentres]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'Cities'
ALTER TABLE [dbo].[Cities]
ADD CONSTRAINT [PK_Cities]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'Regions'
ALTER TABLE [dbo].[Regions]
ADD CONSTRAINT [PK_Regions]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'Countries'
ALTER TABLE [dbo].[Countries]
ADD CONSTRAINT [PK_Countries]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- --------------------------------------------------
-- Creating all FOREIGN KEY constraints
-- --------------------------------------------------

-- Creating foreign key on [UserId] in table 'AccountReferences'
ALTER TABLE [dbo].[AccountReferences]
ADD CONSTRAINT [FK_UsersAccountReferences]
    FOREIGN KEY ([UserId])
    REFERENCES [dbo].[Users]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_UsersAccountReferences'
CREATE INDEX [IX_FK_UsersAccountReferences]
ON [dbo].[AccountReferences]
    ([UserId]);
GO

-- Creating foreign key on [PersonId] in table 'AccountReferences'
ALTER TABLE [dbo].[AccountReferences]
ADD CONSTRAINT [FK_PersonsAccountReferences]
    FOREIGN KEY ([PersonId])
    REFERENCES [dbo].[Persons]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_PersonsAccountReferences'
CREATE INDEX [IX_FK_PersonsAccountReferences]
ON [dbo].[AccountReferences]
    ([PersonId]);
GO

-- Creating foreign key on [CentreId] in table 'AccountReferences'
ALTER TABLE [dbo].[AccountReferences]
ADD CONSTRAINT [FK_DistributionCentresAccountReferences]
    FOREIGN KEY ([CentreId])
    REFERENCES [dbo].[DistributionCentres]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_DistributionCentresAccountReferences'
CREATE INDEX [IX_FK_DistributionCentresAccountReferences]
ON [dbo].[AccountReferences]
    ([CentreId]);
GO

-- Creating foreign key on [UserId] in table 'UserPackages'
ALTER TABLE [dbo].[UserPackages]
ADD CONSTRAINT [FK_UsersUserPackages]
    FOREIGN KEY ([UserId])
    REFERENCES [dbo].[Users]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_UsersUserPackages'
CREATE INDEX [IX_FK_UsersUserPackages]
ON [dbo].[UserPackages]
    ([UserId]);
GO

-- Creating foreign key on [PackageId] in table 'UserPackages'
ALTER TABLE [dbo].[UserPackages]
ADD CONSTRAINT [FK_PackagesUserPackages]
    FOREIGN KEY ([PackageId])
    REFERENCES [dbo].[Packages]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_PackagesUserPackages'
CREATE INDEX [IX_FK_PackagesUserPackages]
ON [dbo].[UserPackages]
    ([PackageId]);
GO

-- Creating foreign key on [CountryId] in table 'Regions'
ALTER TABLE [dbo].[Regions]
ADD CONSTRAINT [FK_CountriesRegions]
    FOREIGN KEY ([CountryId])
    REFERENCES [dbo].[Countries]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_CountriesRegions'
CREATE INDEX [IX_FK_CountriesRegions]
ON [dbo].[Regions]
    ([CountryId]);
GO

-- Creating foreign key on [RegionId] in table 'Cities'
ALTER TABLE [dbo].[Cities]
ADD CONSTRAINT [FK_RegionsCities]
    FOREIGN KEY ([RegionId])
    REFERENCES [dbo].[Regions]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_RegionsCities'
CREATE INDEX [IX_FK_RegionsCities]
ON [dbo].[Cities]
    ([RegionId]);
GO

-- Creating foreign key on [CityId] in table 'DistributionCentres'
ALTER TABLE [dbo].[DistributionCentres]
ADD CONSTRAINT [FK_DistributionCentresCities]
    FOREIGN KEY ([CityId])
    REFERENCES [dbo].[Cities]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_DistributionCentresCities'
CREATE INDEX [IX_FK_DistributionCentresCities]
ON [dbo].[DistributionCentres]
    ([CityId]);
GO

-- Creating foreign key on [CentreId] in table 'Persons'
ALTER TABLE [dbo].[Persons]
ADD CONSTRAINT [FK_DistributionCentresPersons]
    FOREIGN KEY ([CentreId])
    REFERENCES [dbo].[DistributionCentres]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_DistributionCentresPersons'
CREATE INDEX [IX_FK_DistributionCentresPersons]
ON [dbo].[Persons]
    ([CentreId]);
GO

-- Creating foreign key on [CityId] in table 'Persons'
ALTER TABLE [dbo].[Persons]
ADD CONSTRAINT [FK_CitiesPersons]
    FOREIGN KEY ([CityId])
    REFERENCES [dbo].[Cities]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_CitiesPersons'
CREATE INDEX [IX_FK_CitiesPersons]
ON [dbo].[Persons]
    ([CityId]);
GO

-- Creating foreign key on [PersonIdFrom] in table 'Packages'
ALTER TABLE [dbo].[Packages]
ADD CONSTRAINT [FK_PersonsPackages]
    FOREIGN KEY ([PersonIdFrom])
    REFERENCES [dbo].[Persons]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_PersonsPackages'
CREATE INDEX [IX_FK_PersonsPackages]
ON [dbo].[Packages]
    ([PersonIdFrom]);
GO

-- Creating foreign key on [PersonIdTo] in table 'Packages'
ALTER TABLE [dbo].[Packages]
ADD CONSTRAINT [FK_PersonsPackages1]
    FOREIGN KEY ([PersonIdTo])
    REFERENCES [dbo].[Persons]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_PersonsPackages1'
CREATE INDEX [IX_FK_PersonsPackages1]
ON [dbo].[Packages]
    ([PersonIdTo]);
GO

-- Creating foreign key on [TariffId] in table 'Packages'
ALTER TABLE [dbo].[Packages]
ADD CONSTRAINT [FK_TariffsPackages]
    FOREIGN KEY ([TariffId])
    REFERENCES [dbo].[Tariffs]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_TariffsPackages'
CREATE INDEX [IX_FK_TariffsPackages]
ON [dbo].[Packages]
    ([TariffId]);
GO

-- Creating foreign key on [PackageId] in table 'Records'
ALTER TABLE [dbo].[Records]
ADD CONSTRAINT [FK_PackagesRecords]
    FOREIGN KEY ([PackageId])
    REFERENCES [dbo].[Packages]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_PackagesRecords'
CREATE INDEX [IX_FK_PackagesRecords]
ON [dbo].[Records]
    ([PackageId]);
GO

-- Creating foreign key on [CentreId] in table 'Records'
ALTER TABLE [dbo].[Records]
ADD CONSTRAINT [FK_DistributionCentresRecords]
    FOREIGN KEY ([CentreId])
    REFERENCES [dbo].[DistributionCentres]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_DistributionCentresRecords'
CREATE INDEX [IX_FK_DistributionCentresRecords]
ON [dbo].[Records]
    ([CentreId]);
GO

-- --------------------------------------------------
-- Script has ended
-- --------------------------------------------------