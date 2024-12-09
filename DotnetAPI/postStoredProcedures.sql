USE DotNetCourseDatabase
GO

CREATE OR ALTER PROCEDURE TutorialAppSchema.spPosts_Get
/* EXEC TutorialAppSchema.spPosts_Get @UserId = 1004 , @SearchValue='post' */
/* EXEC TutorialAppSchema.spPosts_Get @PostId=4 */
    @UserId INT = NULL
    , @SearchValue NVARCHAR(MAX) = NULL
    , @PostId INT = NULL
AS
BEGIN
    SELECT [Posts].[PostId],
        [Posts].[UserId],
        [Posts].[PostTitle],
        [Posts].[PostContent],
        [Posts].[PostCreated],
        [Posts].[PostUpdated] 
    FROM TutorialAppSchema.Posts AS Posts
        WHERE Posts.UserId = ISNULL(@UserId, Posts.UserId)
            AND Posts.PostId = ISNULL(@PostId, Posts.PostId)
            AND (@SearchValue IS NULL
                OR Posts.PostContent LIKE '%' + @SearchValue + '%'
                OR Posts.PostTitle LIKE '%' + @SearchValue + '%')
END


USE DotNetCourseDatabase
GO

CREATE PROCEDURE TutorialAppSchema.spPosts_Upsert
    @UserId INT
    , @PostId INT = NULL
    , @PostTitle NVARCHAR(255)
    , @PostContent NVARCHAR(MAX)
AS
BEGIN
    IF NOT EXISTS (SELECT * FROM TutorialAppSchema.Posts WHERE PostId = @PostId)
        BEGIN
            INSERT INTO TutorialAppSchema.Posts(
                UserId,
                PostTitle,
                PostContent,
                PostCreated,
                PostUpdated
            ) VALUES (
                @UserId,
                @PostTitle,
                @PostContent,
                GETDATE(),
                GETDATE()
            )
        END
    ELSE
        BEGIN
            UPDATE TutorialAppSchema.Posts
                SET PostTitle = @PostTitle,
                    PostContent = @PostContent,
                    PostUpdated = GETDATE()
                WHERE PostId = @PostId
        END
END

USE DotNetCourseDatabase
GO

CREATE OR ALTER PROCEDURE TutorialAppSchema.spPost_Delete
    @PostId INT
    , @UserId INT
AS
BEGIN
    DELETE FROM TutorialAppSchema.Posts 
        WHERE PostId = @PostId
        AND UserId = @UserId
END