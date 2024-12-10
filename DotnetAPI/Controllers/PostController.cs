using System.Data;
using Dapper;
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
            string stringParameters = "";
            DynamicParameters sqlParameters = new();

            if (postId != 0)
            {
                stringParameters += ", @PostId=@PostIdParameter";
                sqlParameters.Add("@PostIdParameter", postId, DbType.Int32);
            }
            if (userId != 0)
            {
                stringParameters += ", @UserId=@UserIdParameter";
                sqlParameters.Add("@UserIdParameter", userId, DbType.Int32);
            }
            if (searchParam != "None")
            {
                stringParameters += ", @SearchValue=@SearchValueParameter";
                sqlParameters.Add("@SearchValueParameter", searchParam, DbType.String);
            }

            if (stringParameters.Length > 0)
            {
                sql += stringParameters.Substring(1);
            }

            return _dapper.LoadDataWithParameters<Post>(sql, sqlParameters);
        }

        [HttpGet("GetMyPosts")]
        public IEnumerable<Post> GetMyPosts()
        {
            string sql = "EXEC TutorialAppSchema.spPosts_Get @UserId=@UserIdParameter";

            DynamicParameters sqlParameters = new();
            sqlParameters.Add("@UserIdParameter", User.FindFirst("userId")?.Value, DbType.Int32);


            return _dapper.LoadDataWithParameters<Post>(sql, sqlParameters);
        }

        [HttpPut("UpsertPost")]
        public IActionResult UpsertPost(Post post)
        {
            string sql = @"EXEC TutorialAppSchema.spPosts_Upsert 
            @UserId=@UserIdParameter, 
            @PostTitle=@PostTitleParameter, 
            @PostContent=@PostContentParameter";

            DynamicParameters sqlParameters = new();
            sqlParameters.Add("@UserIdParameter", User.FindFirst("userId")?.Value, DbType.Int32);
            sqlParameters.Add("@PostTitleParameter", post.PostTitle, DbType.String);
            sqlParameters.Add("@PostContentParameter", post.PostContent, DbType.String);

            if (post.PostId > 0)
            {
                sql += ", @PostId=@PostIdParameter";
                sqlParameters.Add("@PostIdParameter", post.PostId, DbType.Int32);

            }


            if (_dapper.ExecuteSqlWithParameters(sql, sqlParameters)) return Ok();
            throw new Exception("Failed to Upsert Post");
        }

        [HttpDelete("Post/{postId}")]
        public IActionResult DeletePost(int postId)
        {
            string sql = @"EXEC TutorialAppSchema.spPost_Delete 
            @PostId=@PostIdParameter, 
            @UserId = @UserIdParameter";

            DynamicParameters sqlParameters = new();
            sqlParameters.Add("@UserIdParameter", User.FindFirst("userId")?.Value, DbType.Int32);
            sqlParameters.Add("@PostIdParameter", postId, DbType.Int32);

            if (_dapper.ExecuteSqlWithParameters(sql, sqlParameters)) return Ok();
            throw new Exception("Failed to Delete Post");
        }


    }
}