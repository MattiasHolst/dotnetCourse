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

        [HttpGet("Posts/{postId}/{userId}/{searchParam}")]
        public IEnumerable<Post> GetPosts(int postId = 0, int userId = 0, string searchParam = "None")
        {
            string sql = @"EXEC TutorialAppSchema.spPosts_Get";
            string parameters = "";
            if (postId != 0)
            {
                parameters += ", @PostId=" + postId.ToString();
            }
            if (userId != 0)
            {
                parameters += ", @UserId=" + userId.ToString();
            }
            if (searchParam != "None")
            {
                parameters += ", @SearchValue='" + searchParam + "'";
            }

            if (parameters.Length > 0)
            {
                sql += parameters.Substring(1);
            }

            return _dapper.LoadData<Post>(sql);
        }

        [HttpGet("GetMyPosts")]
        public IEnumerable<Post> GetMyPosts()
        {
            string sql = "EXEC TutorialAppSchema.spPosts_Get @UserId=" + User.FindFirst("userId")?.Value;

            return _dapper.LoadData<Post>(sql);
        }

        [HttpPut("UpsertPost")]
        public IActionResult UpsertPost(Post post)
        {
            string sql = @"EXEC TutorialAppSchema.spPosts_Upsert 
            @UserId=" + this.User.FindFirst("userId")?.Value +
            ", @PostTitle='" + post.PostTitle +
            "', @PostContent='" + post.PostContent + "'";

            if (post.PostId > 0)
            {
                sql += ", @PostId=" + post.PostId.ToString();
            }


            if (_dapper.ExecuteSql(sql)) return Ok();
            throw new Exception("Failed to Upsert Post");
        }

        [HttpDelete("Post/{postId}")]
        public IActionResult DeletePost(int postId)
        {
            string sql = @"EXEC TutorialAppSchema.spPost_Delete @PostId=" + postId.ToString() +
            ", @UserId = " + User.FindFirst("userId")?.Value;

            if (_dapper.ExecuteSql(sql)) return Ok();
            throw new Exception("Failed to Delete Post");
        }


    }
}