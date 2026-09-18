BEGIN TRANSACTION;
CREATE TABLE [StudySessions] (
    [Id] int NOT NULL IDENTITY,
    [UserId] uniqueidentifier NOT NULL,
    [LessonId] int NULL,
    [TopicId] int NULL,
    [StartTime] datetime2 NOT NULL,
    [EndTime] datetime2 NOT NULL,
    [DurationMinutes] int NOT NULL,
    [Note] nvarchar(max) NULL,
    [IsCompleted] bit NOT NULL,
    CONSTRAINT [PK_StudySessions] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_StudySessions_Lessons_LessonId] FOREIGN KEY ([LessonId]) REFERENCES [Lessons] ([Id]),
    CONSTRAINT [FK_StudySessions_Topics_TopicId] FOREIGN KEY ([TopicId]) REFERENCES [Topics] ([Id])
);

CREATE INDEX [IX_StudySessions_LessonId] ON [StudySessions] ([LessonId]);

CREATE INDEX [IX_StudySessions_TopicId] ON [StudySessions] ([TopicId]);

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20260224112921__addded_StudySession', N'9.0.10');

ALTER TABLE [UserActivityHistories] ADD [LessonId] int NULL;

ALTER TABLE [UserActivityHistories] ADD [TopicId] int NULL;

CREATE INDEX [IX_StudySessions_UserId] ON [StudySessions] ([UserId]);

ALTER TABLE [StudySessions] ADD CONSTRAINT [FK_StudySessions_AspNetUsers_UserId] FOREIGN KEY ([UserId]) REFERENCES [AspNetUsers] ([Id]) ON DELETE CASCADE;

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20260228133328_update_UserActivityHistoryTable_added_2_column', N'9.0.10');

COMMIT;
GO

