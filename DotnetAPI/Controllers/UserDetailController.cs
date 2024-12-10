using System.Data;
using Dapper;
using DotnetAPI.Data;
using DotnetAPI.Dtos;
using DotnetAPI.Helpers;
using DotnetAPI.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DotnetAPI.Controllers;

[Authorize]
[ApiController]
[Route("[controller]")]
public class UserDetailController(IConfiguration config) : ControllerBase
{
    readonly DataContextDapper _dapper = new(config);
    readonly ReusableSql _reusableSql = new(config);


    [HttpGet("GetUsers/{userId}/{isActive}")]
    public IEnumerable<UserDetail> GetUsers(int userId, bool isActive)
    {
        string sql = "EXEC TutorialAppSchema.spUsers_Get";
        string stringParameters = "";
        DynamicParameters sqlParameters = new();
        if (userId != 0)
        {
            stringParameters += ", @UserId=@UserIdParameter";
            sqlParameters.Add("@UserIdParameter", userId, DbType.Int32);
        }
        if (isActive)
        {
            stringParameters += ", @Active=@ActiveParameter";
            sqlParameters.Add("@ActiveParameter", isActive, DbType.Boolean);
        }

        if (stringParameters.Length > 0)
        {
            sql += stringParameters.Substring(1);
        }

        return _dapper.LoadDataWithParameters<UserDetail>(sql, sqlParameters);

    }

    [HttpPut("UpsertUser")]
    public IActionResult UpsertUser(UserDetail userDetail)
    {
        if (_reusableSql.UpsertUser(userDetail)) return Ok();
        throw new Exception("Failed to Update User");
    }

    [HttpDelete("DeleteUser/{userId}")]
    public IActionResult DeleteUser(int userId)
    {
        string sql = "EXEC TutorialAppSchema.spUser_Delete @UserId = @UserIdParameter";

        DynamicParameters sqlParameters = new();
        sqlParameters.Add("@UserIdParameter", userId, DbType.Int32);

        if (_dapper.ExecuteSqlWithParameters(sql, sqlParameters)) return Ok();

        throw new Exception("Failed to Delete User");
    }
}
