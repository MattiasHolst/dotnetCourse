using DotnetAPI.Data;
using DotnetAPI.Dtos;
using DotnetAPI.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DotnetAPI.Controllers
{
    [Authorize]
    [ApiController]
    [Route("[controller]")]
    public class PostController(IConfiguration config) : ControllerBase
    {
        private readonly DataContextDapper _dapper = new(config);

        [HttpGet("Posts")]
        public IEnumerable<Post> GetPosts()
        {
            string sql = @"SELECT 
            [PostId],
            [UserId],
            [PostTitle],
            [PostContent],
            [PostCreated],
            [PostUpdated] FROM TutorialAppSchema.Posts";

            return _dapper.LoadData<Post>(sql);
        }

        [HttpGet("GetPost/{postId}")]
        public Post GetPost(int postId)
        {
            string sql = @"
            SELECT  
                [PostId],
                [UserId],
                [PostTitle],
                [PostContent],
                [PostCreated],
                [PostUpdated]
            FROM  TutorialAppSchema.Posts
                WHERE PostId = " + postId.ToString();

            return _dapper.LoadDataSingle<Post>(sql);
        }

        [HttpGet("GetPostByUser/{userId}")]
        public IEnumerable<Post> GetPostByUser(int userId)
        {
            string sql = @"
            SELECT  
                [PostId],
                [UserId],
                [PostTitle],
                [PostContent],
                [PostCreated],
                [PostUpdated]
            FROM  TutorialAppSchema.Posts
                WHERE UserId = " + userId.ToString();

            return _dapper.LoadData<Post>(sql);
        }

        [HttpGet("GetMyPosts")]
        public IEnumerable<Post> GetMyPosts()
        {
            string sql = @"
            SELECT  
                [PostId],
                [UserId],
                [PostTitle],
                [PostContent],
                [PostCreated],
                [PostUpdated]
            FROM  TutorialAppSchema.Posts
                WHERE UserId = " + User.FindFirst("userId")?.Value;

            return _dapper.LoadData<Post>(sql);
        }

        [HttpGet("PostsBySearch/{searchParam}")]
        public IEnumerable<Post> PostsBySearch(string searchParam)
        {
            string sql = @"
            SELECT  
                [PostId],
                [UserId],
                [PostTitle],
                [PostContent],
                [PostCreated],
                [PostUpdated]
            FROM  TutorialAppSchema.Posts
                WHERE PostTitle LIKE '%" + searchParam + @"%' 
                    OR PostContent LIKE '%" + searchParam + "%'";

            return _dapper.LoadData<Post>(sql);
        }

        [HttpPost("Post")]
        public IActionResult AddPost(PostToAddDto post)
        {
            string sql = @"
            INSERT INTO TutorialAppSchema.Posts(
                [UserId],
                [PostTitle],
                [PostContent],
                [PostCreated],
                [PostUpdated]) 
                VALUES(" + User.FindFirst("userId")?.Value +
                    ",'" + post.PostTitle +
                    "','" + post.PostContent +
                    "', GETDATE(), GETDATE())";


            if (_dapper.ExecuteSql(sql)) return Ok();
            throw new Exception("Failed to Add Post");
        }

        [HttpPut("Post")]
        public IActionResult EditPost(PostToEditDto post)
        {
            string sql = @"
            UPDATE TutorialAppSchema.Posts 
                SET PostContent = '" + post.PostContent +
                "', PostTitle = '" + post.PostTitle +
                @"', PostUpdated = GETDATE()
            WHERE PostId = " + post.PostId.ToString() +
            " AND UserId = " + User.FindFirst("userId")?.Value;


            if (_dapper.ExecuteSql(sql)) return Ok();
            throw new Exception("Failed to Update Post");
        }

        [HttpDelete("Post/{postId}")]
        public IActionResult DeletePost(int postId)
        {
            string sql = @"DELETE FROM TutorialAppSchema.Posts 
            WHERE PostId = " + postId.ToString() +
            " AND UserId = " + User.FindFirst("userId")?.Value;

            if (_dapper.ExecuteSql(sql)) return Ok();
            throw new Exception("Failed to Delete Post");
        }


    }
}