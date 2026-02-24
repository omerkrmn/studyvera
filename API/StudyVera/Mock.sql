BEGIN TRANSACTION;
ALTER TABLE [Lessons] ADD [ExamQuestionCount] int NOT NULL DEFAULT 0;

CREATE TABLE [UserMockExams] (
    [Id] int NOT NULL IDENTITY,
    [UserId] uniqueidentifier NOT NULL,
    [ExamId] int NOT NULL,
    [Publisher] nvarchar(max) NULL,
    [ExamName] nvarchar(max) NULL,
    [ExamDate] datetime2 NOT NULL,
    [TotalNet] float NOT NULL,
    [TotalCorrect] int NOT NULL,
    [TotalWrong] int NOT NULL,
    [TotalEmpty] int NOT NULL,
    CONSTRAINT [PK_UserMockExams] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_UserMockExams_AspNetUsers_UserId] FOREIGN KEY ([UserId]) REFERENCES [AspNetUsers] ([Id]) ON DELETE CASCADE,
    CONSTRAINT [FK_UserMockExams_Exams_ExamId] FOREIGN KEY ([ExamId]) REFERENCES [Exams] ([Id]) ON DELETE CASCADE
);

CREATE TABLE [UserMockExamDetails] (
    [Id] int NOT NULL IDENTITY,
    [UserMockExamId] int NOT NULL,
    [LessonId] int NOT NULL,
    [CorrectCount] int NOT NULL,
    [WrongCount] int NOT NULL,
    [EmptyCount] int NOT NULL,
    CONSTRAINT [PK_UserMockExamDetails] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_UserMockExamDetails_Lessons_LessonId] FOREIGN KEY ([LessonId]) REFERENCES [Lessons] ([Id]) ON DELETE NO ACTION,
    CONSTRAINT [FK_UserMockExamDetails_UserMockExams_UserMockExamId] FOREIGN KEY ([UserMockExamId]) REFERENCES [UserMockExams] ([Id])
);

UPDATE [Lessons] SET [ExamQuestionCount] = 0
WHERE [Id] = 2;
SELECT @@ROWCOUNT;


UPDATE [Lessons] SET [ExamQuestionCount] = 0
WHERE [Id] = 5;
SELECT @@ROWCOUNT;


UPDATE [Lessons] SET [ExamQuestionCount] = 0
WHERE [Id] = 8;
SELECT @@ROWCOUNT;


UPDATE [Lessons] SET [ExamQuestionCount] = 0
WHERE [Id] = 17;
SELECT @@ROWCOUNT;


UPDATE [Lessons] SET [ExamQuestionCount] = 0
WHERE [Id] = 20;
SELECT @@ROWCOUNT;


UPDATE [Lessons] SET [ExamQuestionCount] = 0
WHERE [Id] = 26;
SELECT @@ROWCOUNT;


CREATE INDEX [IX_UserMockExamDetails_LessonId] ON [UserMockExamDetails] ([LessonId]);

CREATE INDEX [IX_UserMockExamDetails_UserMockExamId] ON [UserMockExamDetails] ([UserMockExamId]);

CREATE INDEX [IX_UserMockExams_ExamId] ON [UserMockExams] ([ExamId]);

CREATE INDEX [IX_UserMockExams_UserId] ON [UserMockExams] ([UserId]);

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20260222103707__Mock_feature_1', N'9.0.10');

COMMIT;
GO

