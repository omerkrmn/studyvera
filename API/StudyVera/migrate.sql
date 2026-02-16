IF OBJECT_ID(N'[__EFMigrationsHistory]') IS NULL
BEGIN
    CREATE TABLE [__EFMigrationsHistory] (
        [MigrationId] nvarchar(150) NOT NULL,
        [ProductVersion] nvarchar(32) NOT NULL,
        CONSTRAINT [PK___EFMigrationsHistory] PRIMARY KEY ([MigrationId])
    );
END;
GO

BEGIN TRANSACTION;
IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20260215173046__init'
)
BEGIN
    CREATE TABLE [AspNetRoles] (
        [Id] uniqueidentifier NOT NULL,
        [Name] nvarchar(256) NULL,
        [NormalizedName] nvarchar(256) NULL,
        [ConcurrencyStamp] nvarchar(max) NULL,
        CONSTRAINT [PK_AspNetRoles] PRIMARY KEY ([Id])
    );
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20260215173046__init'
)
BEGIN
    CREATE TABLE [AspNetUsers] (
        [Id] uniqueidentifier NOT NULL,
        [FirstName] nvarchar(max) NOT NULL,
        [LastName] nvarchar(max) NOT NULL,
        [CreatedAt] datetime2 NOT NULL,
        [RefreshToken] nvarchar(max) NULL,
        [RefreshTokenExpiryTime] datetime2 NOT NULL,
        [TargetExam] int NOT NULL,
        [UserName] nvarchar(256) NULL,
        [NormalizedUserName] nvarchar(256) NULL,
        [Email] nvarchar(256) NULL,
        [NormalizedEmail] nvarchar(256) NULL,
        [EmailConfirmed] bit NOT NULL,
        [PasswordHash] nvarchar(max) NULL,
        [SecurityStamp] nvarchar(max) NULL,
        [ConcurrencyStamp] nvarchar(max) NULL,
        [PhoneNumber] nvarchar(max) NULL,
        [PhoneNumberConfirmed] bit NOT NULL,
        [TwoFactorEnabled] bit NOT NULL,
        [LockoutEnd] datetimeoffset NULL,
        [LockoutEnabled] bit NOT NULL,
        [AccessFailedCount] int NOT NULL,
        CONSTRAINT [PK_AspNetUsers] PRIMARY KEY ([Id])
    );
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20260215173046__init'
)
BEGIN
    CREATE TABLE [Exams] (
        [Id] int NOT NULL IDENTITY,
        [Name] nvarchar(max) NOT NULL,
        [Description] nvarchar(max) NOT NULL,
        [ExamDate] datetime2 NOT NULL,
        CONSTRAINT [PK_Exams] PRIMARY KEY ([Id])
    );
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20260215173046__init'
)
BEGIN
    CREATE TABLE [AspNetRoleClaims] (
        [Id] int NOT NULL IDENTITY,
        [RoleId] uniqueidentifier NOT NULL,
        [ClaimType] nvarchar(max) NULL,
        [ClaimValue] nvarchar(max) NULL,
        CONSTRAINT [PK_AspNetRoleClaims] PRIMARY KEY ([Id]),
        CONSTRAINT [FK_AspNetRoleClaims_AspNetRoles_RoleId] FOREIGN KEY ([RoleId]) REFERENCES [AspNetRoles] ([Id]) ON DELETE CASCADE
    );
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20260215173046__init'
)
BEGIN
    CREATE TABLE [AspNetUserClaims] (
        [Id] int NOT NULL IDENTITY,
        [UserId] uniqueidentifier NOT NULL,
        [ClaimType] nvarchar(max) NULL,
        [ClaimValue] nvarchar(max) NULL,
        CONSTRAINT [PK_AspNetUserClaims] PRIMARY KEY ([Id]),
        CONSTRAINT [FK_AspNetUserClaims_AspNetUsers_UserId] FOREIGN KEY ([UserId]) REFERENCES [AspNetUsers] ([Id]) ON DELETE CASCADE
    );
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20260215173046__init'
)
BEGIN
    CREATE TABLE [AspNetUserLogins] (
        [LoginProvider] nvarchar(450) NOT NULL,
        [ProviderKey] nvarchar(450) NOT NULL,
        [ProviderDisplayName] nvarchar(max) NULL,
        [UserId] uniqueidentifier NOT NULL,
        CONSTRAINT [PK_AspNetUserLogins] PRIMARY KEY ([LoginProvider], [ProviderKey]),
        CONSTRAINT [FK_AspNetUserLogins_AspNetUsers_UserId] FOREIGN KEY ([UserId]) REFERENCES [AspNetUsers] ([Id]) ON DELETE CASCADE
    );
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20260215173046__init'
)
BEGIN
    CREATE TABLE [AspNetUserRoles] (
        [UserId] uniqueidentifier NOT NULL,
        [RoleId] uniqueidentifier NOT NULL,
        CONSTRAINT [PK_AspNetUserRoles] PRIMARY KEY ([UserId], [RoleId]),
        CONSTRAINT [FK_AspNetUserRoles_AspNetRoles_RoleId] FOREIGN KEY ([RoleId]) REFERENCES [AspNetRoles] ([Id]) ON DELETE CASCADE,
        CONSTRAINT [FK_AspNetUserRoles_AspNetUsers_UserId] FOREIGN KEY ([UserId]) REFERENCES [AspNetUsers] ([Id]) ON DELETE CASCADE
    );
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20260215173046__init'
)
BEGIN
    CREATE TABLE [AspNetUserTokens] (
        [UserId] uniqueidentifier NOT NULL,
        [LoginProvider] nvarchar(450) NOT NULL,
        [Name] nvarchar(450) NOT NULL,
        [Value] nvarchar(max) NULL,
        CONSTRAINT [PK_AspNetUserTokens] PRIMARY KEY ([UserId], [LoginProvider], [Name]),
        CONSTRAINT [FK_AspNetUserTokens_AspNetUsers_UserId] FOREIGN KEY ([UserId]) REFERENCES [AspNetUsers] ([Id]) ON DELETE CASCADE
    );
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20260215173046__init'
)
BEGIN
    CREATE TABLE [ProfileStats] (
        [Id] int NOT NULL IDENTITY,
        [UserId] uniqueidentifier NOT NULL,
        [Score] int NOT NULL,
        [CurrentStreak] int NOT NULL,
        [BestStreak] int NOT NULL,
        [LastActivityDate] datetime2 NULL,
        CONSTRAINT [PK_ProfileStats] PRIMARY KEY ([Id]),
        CONSTRAINT [FK_ProfileStats_AspNetUsers_UserId] FOREIGN KEY ([UserId]) REFERENCES [AspNetUsers] ([Id]) ON DELETE CASCADE
    );
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20260215173046__init'
)
BEGIN
    CREATE TABLE [UserActivityHistories] (
        [Id] int NOT NULL IDENTITY,
        [ActivityDate] datetime2 NOT NULL,
        [ActivityType] int NULL,
        [Description] nvarchar(max) NOT NULL,
        [UserId] uniqueidentifier NOT NULL,
        CONSTRAINT [PK_UserActivityHistories] PRIMARY KEY ([Id]),
        CONSTRAINT [FK_UserActivityHistories_AspNetUsers_UserId] FOREIGN KEY ([UserId]) REFERENCES [AspNetUsers] ([Id]) ON DELETE CASCADE
    );
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20260215173046__init'
)
BEGIN
    CREATE TABLE [UserSettings] (
        [Id] int NOT NULL IDENTITY,
        [UserId] uniqueidentifier NOT NULL,
        [WeeklyQuestionGoal] int NOT NULL,
        [DailyStudyMinuteGoal] int NOT NULL,
        [DailyReminderHour] int NOT NULL,
        [IsProfilePublic] bit NOT NULL,
        [ShowRankInLeaderboard] bit NOT NULL,
        [AllowFriendRequests] bit NOT NULL,
        [Theme] nvarchar(max) NOT NULL,
        [Language] nvarchar(max) NOT NULL,
        [CurrentTitle] nvarchar(max) NOT NULL,
        [CreatedAt] datetime2 NOT NULL,
        [UpdatedAt] datetime2 NOT NULL,
        CONSTRAINT [PK_UserSettings] PRIMARY KEY ([Id]),
        CONSTRAINT [FK_UserSettings_AspNetUsers_UserId] FOREIGN KEY ([UserId]) REFERENCES [AspNetUsers] ([Id]) ON DELETE CASCADE
    );
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20260215173046__init'
)
BEGIN
    CREATE TABLE [UserWeeklyGoals] (
        [Id] int NOT NULL IDENTITY,
        [UserId] uniqueidentifier NOT NULL,
        [WeekStartDate] datetime2 NOT NULL,
        [TargetQuestionCount] int NOT NULL,
        [TargetStudyMinutes] int NOT NULL,
        [CurrentQuestionCount] int NOT NULL,
        [CurrentStudyMinutes] int NOT NULL,
        [AppUserId] uniqueidentifier NULL,
        CONSTRAINT [PK_UserWeeklyGoals] PRIMARY KEY ([Id]),
        CONSTRAINT [FK_UserWeeklyGoals_AspNetUsers_AppUserId] FOREIGN KEY ([AppUserId]) REFERENCES [AspNetUsers] ([Id])
    );
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20260215173046__init'
)
BEGIN
    CREATE TABLE [Lessons] (
        [Id] int NOT NULL IDENTITY,
        [Name] nvarchar(max) NOT NULL,
        [ExamId] int NOT NULL,
        CONSTRAINT [PK_Lessons] PRIMARY KEY ([Id]),
        CONSTRAINT [FK_Lessons_Exams_ExamId] FOREIGN KEY ([ExamId]) REFERENCES [Exams] ([Id]) ON DELETE CASCADE
    );
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20260215173046__init'
)
BEGIN
    CREATE TABLE [Topics] (
        [Id] int NOT NULL IDENTITY,
        [Name] nvarchar(max) NOT NULL,
        [Priority] tinyint NOT NULL,
        [OrderIndex] int NOT NULL,
        [LessonId] int NOT NULL,
        CONSTRAINT [PK_Topics] PRIMARY KEY ([Id]),
        CONSTRAINT [FK_Topics_Lessons_LessonId] FOREIGN KEY ([LessonId]) REFERENCES [Lessons] ([Id]) ON DELETE CASCADE
    );
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20260215173046__init'
)
BEGIN
    CREATE TABLE [LessonSchedules] (
        [Id] int NOT NULL IDENTITY,
        [UserId] uniqueidentifier NOT NULL,
        [LessonId] int NOT NULL,
        [TopicId] int NULL,
        [DayOfWeek] int NOT NULL,
        [StartTime] time NULL,
        [EndTime] time NULL,
        [CreatedAt] datetime2 NOT NULL,
        [LastUpdatedAt] datetime2 NOT NULL,
        CONSTRAINT [PK_LessonSchedules] PRIMARY KEY ([Id]),
        CONSTRAINT [FK_LessonSchedules_AspNetUsers_UserId] FOREIGN KEY ([UserId]) REFERENCES [AspNetUsers] ([Id]) ON DELETE CASCADE,
        CONSTRAINT [FK_LessonSchedules_Lessons_LessonId] FOREIGN KEY ([LessonId]) REFERENCES [Lessons] ([Id]) ON DELETE CASCADE,
        CONSTRAINT [FK_LessonSchedules_Topics_TopicId] FOREIGN KEY ([TopicId]) REFERENCES [Topics] ([Id])
    );
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20260215173046__init'
)
BEGIN
    CREATE TABLE [UserLessonProgresses] (
        [Id] int NOT NULL IDENTITY,
        [UserId] uniqueidentifier NOT NULL,
        [TopicId] int NOT NULL,
        [ProgressStatus] int NOT NULL,
        [LastUpdated] datetime2 NOT NULL,
        CONSTRAINT [PK_UserLessonProgresses] PRIMARY KEY ([Id]),
        CONSTRAINT [FK_UserLessonProgresses_AspNetUsers_UserId] FOREIGN KEY ([UserId]) REFERENCES [AspNetUsers] ([Id]) ON DELETE CASCADE,
        CONSTRAINT [FK_UserLessonProgresses_Topics_TopicId] FOREIGN KEY ([TopicId]) REFERENCES [Topics] ([Id]) ON DELETE CASCADE
    );
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20260215173046__init'
)
BEGIN
    CREATE TABLE [UserQuestionStats] (
        [Id] int NOT NULL IDENTITY,
        [UserId] uniqueidentifier NOT NULL,
        [TopicId] int NOT NULL,
        [TotalSolvedCount] int NOT NULL,
        [TotalCorrectCount] int NOT NULL,
        [LastAttemptAt] datetime2 NOT NULL,
        CONSTRAINT [PK_UserQuestionStats] PRIMARY KEY ([Id]),
        CONSTRAINT [FK_UserQuestionStats_AspNetUsers_UserId] FOREIGN KEY ([UserId]) REFERENCES [AspNetUsers] ([Id]) ON DELETE CASCADE,
        CONSTRAINT [FK_UserQuestionStats_Topics_TopicId] FOREIGN KEY ([TopicId]) REFERENCES [Topics] ([Id]) ON DELETE CASCADE
    );
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20260215173046__init'
)
BEGIN
    CREATE TABLE [QuestionStatDetails] (
        [Id] int NOT NULL IDENTITY,
        [UserQuestionStatId] int NOT NULL,
        [SolvedCount] int NOT NULL,
        [CorrectCount] int NOT NULL,
        [AttemptedAt] datetime2 NOT NULL,
        CONSTRAINT [PK_QuestionStatDetails] PRIMARY KEY ([Id]),
        CONSTRAINT [FK_QuestionStatDetails_UserQuestionStats_UserQuestionStatId] FOREIGN KEY ([UserQuestionStatId]) REFERENCES [UserQuestionStats] ([Id]) ON DELETE CASCADE
    );
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20260215173046__init'
)
BEGIN
    IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'Id', N'ConcurrencyStamp', N'Name', N'NormalizedName') AND [object_id] = OBJECT_ID(N'[AspNetRoles]'))
        SET IDENTITY_INSERT [AspNetRoles] ON;
    EXEC(N'INSERT INTO [AspNetRoles] ([Id], [ConcurrencyStamp], [Name], [NormalizedName])
    VALUES (''c4a760a8-5b3d-4d3b-9a9f-1f1e4f1e4f1e'', NULL, N''User'', N''USER''),
    (''d290f1ee-6c54-4b01-90e6-d701748f0851'', NULL, N''Admin'', N''ADMIN'')');
    IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'Id', N'ConcurrencyStamp', N'Name', N'NormalizedName') AND [object_id] = OBJECT_ID(N'[AspNetRoles]'))
        SET IDENTITY_INSERT [AspNetRoles] OFF;
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20260215173046__init'
)
BEGIN
    IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'Id', N'Description', N'ExamDate', N'Name') AND [object_id] = OBJECT_ID(N'[Exams]'))
        SET IDENTITY_INSERT [Exams] ON;
    EXEC(N'INSERT INTO [Exams] ([Id], [Description], [ExamDate], [Name])
    VALUES (1, N''Yükseköğretim Kurumları Sınavı'', ''2026-06-20T00:00:00.0000000'', N''TYT''),
    (2, N''Yükseköğretim Kurumları Sınavı'', ''2026-06-21T00:00:00.0000000'', N''AYT''),
    (3, N''Dikey Geçiş Sınavı'', ''2026-07-19T00:00:00.0000000'', N''DGS''),
    (4, N''Kamu Personeli Seçme Sınavı'', ''2026-06-16T00:00:00.0000000'', N''KPSS''),
    (5, N''Akademik Personel ve Lisansüstü Eğitimi Giriş Sınavı'', ''2026-05-10T00:00:00.0000000'', N''ALES'')');
    IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'Id', N'Description', N'ExamDate', N'Name') AND [object_id] = OBJECT_ID(N'[Exams]'))
        SET IDENTITY_INSERT [Exams] OFF;
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20260215173046__init'
)
BEGIN
    IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'Id', N'ExamId', N'Name') AND [object_id] = OBJECT_ID(N'[Lessons]'))
        SET IDENTITY_INSERT [Lessons] ON;
    EXEC(N'INSERT INTO [Lessons] ([Id], [ExamId], [Name])
    VALUES (1, 1, N''Türkçe''),
    (2, 3, N''Türkçe''),
    (3, 1, N''Matematik''),
    (4, 2, N''Matematik''),
    (5, 3, N''Matematik''),
    (6, 1, N''Geometri''),
    (7, 2, N''Geometri''),
    (8, 3, N''Geometri''),
    (9, 1, N''Fizik''),
    (10, 2, N''Fizik''),
    (11, 1, N''Kimya''),
    (12, 2, N''Kimya''),
    (13, 1, N''Biyoloji''),
    (14, 2, N''Biyoloji''),
    (15, 1, N''Tarih''),
    (16, 2, N''Tarih''),
    (17, 3, N''Tarih''),
    (18, 1, N''Çoğrafya''),
    (19, 2, N''Çoğrafya''),
    (20, 3, N''Çoğrafya''),
    (21, 1, N''Felsefe''),
    (22, 2, N''Felsefe''),
    (23, 1, N''Din Kültürü ve Ahlak Bilgisi Konuları''),
    (24, 2, N''Din Kültürü ve Ahlak Bilgisi Konuları''),
    (25, 2, N''Edebiyat''),
    (26, 3, N''Vatandaşlık'')');
    IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'Id', N'ExamId', N'Name') AND [object_id] = OBJECT_ID(N'[Lessons]'))
        SET IDENTITY_INSERT [Lessons] OFF;
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20260215173046__init'
)
BEGIN
    CREATE INDEX [IX_AspNetRoleClaims_RoleId] ON [AspNetRoleClaims] ([RoleId]);
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20260215173046__init'
)
BEGIN
    EXEC(N'CREATE UNIQUE INDEX [RoleNameIndex] ON [AspNetRoles] ([NormalizedName]) WHERE [NormalizedName] IS NOT NULL');
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20260215173046__init'
)
BEGIN
    CREATE INDEX [IX_AspNetUserClaims_UserId] ON [AspNetUserClaims] ([UserId]);
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20260215173046__init'
)
BEGIN
    CREATE INDEX [IX_AspNetUserLogins_UserId] ON [AspNetUserLogins] ([UserId]);
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20260215173046__init'
)
BEGIN
    CREATE INDEX [IX_AspNetUserRoles_RoleId] ON [AspNetUserRoles] ([RoleId]);
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20260215173046__init'
)
BEGIN
    CREATE INDEX [EmailIndex] ON [AspNetUsers] ([NormalizedEmail]);
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20260215173046__init'
)
BEGIN
    EXEC(N'CREATE UNIQUE INDEX [UserNameIndex] ON [AspNetUsers] ([NormalizedUserName]) WHERE [NormalizedUserName] IS NOT NULL');
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20260215173046__init'
)
BEGIN
    CREATE INDEX [IX_Lessons_ExamId] ON [Lessons] ([ExamId]);
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20260215173046__init'
)
BEGIN
    CREATE INDEX [IX_LessonSchedules_LessonId] ON [LessonSchedules] ([LessonId]);
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20260215173046__init'
)
BEGIN
    CREATE INDEX [IX_LessonSchedules_TopicId] ON [LessonSchedules] ([TopicId]);
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20260215173046__init'
)
BEGIN
    CREATE INDEX [IX_LessonSchedules_UserId] ON [LessonSchedules] ([UserId]);
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20260215173046__init'
)
BEGIN
    CREATE UNIQUE INDEX [IX_ProfileStats_UserId] ON [ProfileStats] ([UserId]);
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20260215173046__init'
)
BEGIN
    CREATE INDEX [IX_QuestionStatDetails_UserQuestionStatId] ON [QuestionStatDetails] ([UserQuestionStatId]);
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20260215173046__init'
)
BEGIN
    CREATE INDEX [IX_Topics_LessonId] ON [Topics] ([LessonId]);
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20260215173046__init'
)
BEGIN
    CREATE INDEX [IX_UserActivityHistories_UserId] ON [UserActivityHistories] ([UserId]);
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20260215173046__init'
)
BEGIN
    CREATE INDEX [IX_UserLessonProgresses_TopicId] ON [UserLessonProgresses] ([TopicId]);
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20260215173046__init'
)
BEGIN
    CREATE INDEX [IX_UserLessonProgresses_UserId] ON [UserLessonProgresses] ([UserId]);
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20260215173046__init'
)
BEGIN
    CREATE INDEX [IX_UserQuestionStats_TopicId] ON [UserQuestionStats] ([TopicId]);
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20260215173046__init'
)
BEGIN
    CREATE INDEX [IX_UserQuestionStats_UserId] ON [UserQuestionStats] ([UserId]);
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20260215173046__init'
)
BEGIN
    CREATE UNIQUE INDEX [IX_UserSettings_UserId] ON [UserSettings] ([UserId]);
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20260215173046__init'
)
BEGIN
    CREATE INDEX [IX_UserWeeklyGoals_AppUserId] ON [UserWeeklyGoals] ([AppUserId]);
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20260215173046__init'
)
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20260215173046__init', N'9.0.10');
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20260215175117__added_seed_topic_data'
)
BEGIN
    IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'Id', N'LessonId', N'Name', N'OrderIndex', N'Priority') AND [object_id] = OBJECT_ID(N'[Topics]'))
        SET IDENTITY_INSERT [Topics] ON;
    EXEC(N'INSERT INTO [Topics] ([Id], [LessonId], [Name], [OrderIndex], [Priority])
    VALUES (1, 2, N''Sözcükte Anlam'', 1, CAST(3 AS tinyint)),
    (2, 2, N''Cümlede Anlam'', 2, CAST(3 AS tinyint)),
    (3, 2, N''Sözcük Türleri'', 3, CAST(3 AS tinyint)),
    (4, 2, N''Sözcükte Yapı'', 4, CAST(3 AS tinyint)),
    (5, 2, N''Cümlenin Ögeleri'', 5, CAST(3 AS tinyint)),
    (6, 2, N''Cümle Türleri'', 6, CAST(3 AS tinyint)),
    (7, 2, N''Dil Bilgisi Ses Olayları'', 7, CAST(3 AS tinyint)),
    (8, 2, N''Yazım Kuralları'', 8, CAST(3 AS tinyint)),
    (9, 2, N''Noktalama İşaretleri'', 9, CAST(3 AS tinyint)),
    (10, 2, N''Anlatım Bozuklukları'', 10, CAST(3 AS tinyint)),
    (11, 2, N''Paragrafta Anlam'', 11, CAST(3 AS tinyint)),
    (12, 2, N''Paragrafta Anlatım Biçimi'', 12, CAST(3 AS tinyint)),
    (13, 2, N''Sözel Mantık'', 13, CAST(3 AS tinyint)),
    (14, 5, N''Temel Kavramlar'', 1, CAST(3 AS tinyint)),
    (15, 5, N''Rasyonel Sayılar - Ondalıklı Sayılar'', 2, CAST(3 AS tinyint)),
    (16, 5, N''Basit Eşitsizlikler'', 3, CAST(3 AS tinyint)),
    (17, 5, N''Mutlak Değer'', 4, CAST(3 AS tinyint)),
    (18, 5, N''Üslü Sayılar'', 5, CAST(3 AS tinyint)),
    (19, 5, N''Köklü Sayılar'', 6, CAST(3 AS tinyint)),
    (20, 5, N''Çarpanlara Ayırma'', 7, CAST(3 AS tinyint)),
    (21, 5, N''Oran-Orantı'', 8, CAST(3 AS tinyint)),
    (22, 5, N''Denklem Çözme'', 9, CAST(3 AS tinyint)),
    (23, 5, N''Problemler'', 10, CAST(3 AS tinyint)),
    (24, 5, N''Kümeler'', 11, CAST(3 AS tinyint)),
    (25, 5, N''Fonksiyonlar'', 12, CAST(3 AS tinyint)),
    (26, 5, N''İşlem'', 13, CAST(3 AS tinyint)),
    (27, 5, N''Permütasyon'', 14, CAST(3 AS tinyint)),
    (28, 5, N''Kombinasyon'', 15, CAST(3 AS tinyint)),
    (29, 5, N''Olasılık'', 16, CAST(3 AS tinyint)),
    (30, 5, N''Sayısal Mantık'', 17, CAST(3 AS tinyint)),
    (31, 8, N''Geometrik Kavramlar ve Açılar'', 1, CAST(3 AS tinyint)),
    (32, 8, N''Çokgenler ve Dörtgenler'', 2, CAST(3 AS tinyint)),
    (33, 8, N''Çember ve Daire'', 3, CAST(3 AS tinyint)),
    (34, 8, N''Analitik Geometri'', 4, CAST(3 AS tinyint)),
    (35, 8, N''Katı Cisimler'', 5, CAST(3 AS tinyint)),
    (36, 17, N''İslamiyet Öncesi Türk Tarihi'', 1, CAST(3 AS tinyint)),
    (37, 17, N''İlk Türk-İslam Devletleri ve Beylikleri'', 2, CAST(3 AS tinyint)),
    (38, 17, N''Osmanlı Devleti Kuruluş ve Yükselme Dönemleri'', 3, CAST(3 AS tinyint)),
    (39, 17, N''Osmanlı Devleti''''nde Kültür ve Uygarlık'', 4, CAST(3 AS tinyint)),
    (40, 17, N''XVII. Yüzyılda Osmanlı Devleti (Duraklama)'', 5, CAST(3 AS tinyint)),
    (41, 17, N''XVIII. Yüzyılda Osmanlı Devleti (Gerileme)'', 6, CAST(3 AS tinyint)),
    (42, 17, N''XIX. Yüzyılda Osmanlı Devleti (Dağılma)'', 7, CAST(3 AS tinyint));
    INSERT INTO [Topics] ([Id], [LessonId], [Name], [OrderIndex], [Priority])
    VALUES (43, 17, N''XX. Yüzyılda Osmanlı Devleti'', 8, CAST(3 AS tinyint)),
    (44, 17, N''Kurtuluş Savaşı Hazırlık Dönemi'', 9, CAST(3 AS tinyint)),
    (45, 17, N''I. TBMM Dönemi'', 10, CAST(3 AS tinyint)),
    (46, 17, N''Kurtuluş Savaşı Muharebeler Dönemi'', 11, CAST(3 AS tinyint)),
    (47, 17, N''Atatürk İnkılapları'', 12, CAST(3 AS tinyint)),
    (48, 17, N''Atatürk İlkeleri'', 13, CAST(3 AS tinyint)),
    (49, 17, N''Partiler ve Partileşme Dönemi'', 14, CAST(0 AS tinyint)),
    (50, 17, N''Atatürk Dönemi Türk Dış Politikası'', 15, CAST(3 AS tinyint)),
    (51, 17, N''Atatürk Sonrası Dönem'', 16, CAST(3 AS tinyint)),
    (52, 17, N''Atatürk''''ün Hayatı ve Kişiliği'', 17, CAST(3 AS tinyint)),
    (53, 20, N''Türkiye''''nin Coğrafi Konumu'', 1, CAST(3 AS tinyint)),
    (54, 20, N''Türkiye''''nin İklimi ve Bitki Örtüsü'', 2, CAST(3 AS tinyint)),
    (55, 20, N''Türkiye''''nin Fiziki Özellikleri'', 3, CAST(3 AS tinyint)),
    (56, 20, N''Türkiye''''de Nüfus ve Yerleşme'', 4, CAST(3 AS tinyint)),
    (57, 20, N''Tarım'', 5, CAST(3 AS tinyint)),
    (58, 20, N''Hayvancılık'', 6, CAST(3 AS tinyint)),
    (59, 20, N''Madenler ve Enerji Kaynakları'', 7, CAST(3 AS tinyint)),
    (60, 20, N''Sanayi ve Endüstri'', 8, CAST(3 AS tinyint)),
    (61, 20, N''Ulaşım'', 9, CAST(3 AS tinyint)),
    (62, 20, N''Ticaret'', 10, CAST(3 AS tinyint)),
    (63, 20, N''Turizm'', 11, CAST(3 AS tinyint)),
    (64, 20, N''Bölgeler Coğrafyası'', 12, CAST(3 AS tinyint)),
    (65, 26, N''Temel Hukuk Kavramları'', 1, CAST(3 AS tinyint)),
    (66, 26, N''Anayasal Kavramlar'', 2, CAST(3 AS tinyint)),
    (67, 26, N''Türk Anayasa Tarihi'', 3, CAST(3 AS tinyint)),
    (68, 26, N''Temel Hak ve Ödevler'', 4, CAST(3 AS tinyint)),
    (69, 26, N''Yasama'', 5, CAST(3 AS tinyint)),
    (70, 26, N''Yürütme'', 6, CAST(3 AS tinyint)),
    (71, 26, N''Yargı'', 7, CAST(3 AS tinyint)),
    (72, 26, N''İdare Hukuku'', 8, CAST(3 AS tinyint))');
    IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'Id', N'LessonId', N'Name', N'OrderIndex', N'Priority') AND [object_id] = OBJECT_ID(N'[Topics]'))
        SET IDENTITY_INSERT [Topics] OFF;
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20260215175117__added_seed_topic_data'
)
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20260215175117__added_seed_topic_data', N'9.0.10');
END;

COMMIT;
GO

