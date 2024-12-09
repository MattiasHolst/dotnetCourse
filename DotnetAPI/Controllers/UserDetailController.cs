using DotnetAPI.Data;
using DotnetAPI.Dtos;
using DotnetAPI.Models;
using Microsoft.AspNetCore.Mvc;

namespace DotnetAPI.Controllers;

[ApiController]
[Route("[controller]")]
public class UserDetailController(IConfiguration config) : ControllerBase
{
    readonly DataContextDapper _dapper = new(config);


    [HttpGet("GetUsers/{userId}/{isActive}")]
    public IEnumerable<UserDetail> GetUsers(int userId, bool isActive)
    {
        string sql = "EXEC TutorialAppSchema.spUsers_Get";
        string parameters = "";
        if (userId != 0)
        {
            parameters += ", @UserId=" + userId.ToString();
        }
        if (isActive)
        {
            parameters += ", @Active=" + isActive;
        }

        if (parameters.Length > 0)
        {

            sql += parameters.Substring(1);
        }

        return _dapper.LoadData<UserDetail>(sql);

    }

    [HttpPut("UpsertUser")]
    public IActionResult UpsertUser(UserDetail userDetail)
    {
        string sql = @"EXEC TutorialAppSchema.spUser_Upsert
                @FirstName = '" + userDetail.FirstName +
                "', @LastName = '" + userDetail.LastName +
                "', @Email = '" + userDetail.Email +
                "', @Gender = '" + userDetail.Gender +
                "', @Active = '" + userDetail.Active +
                "', @JobTitle = '" + userDetail.JobTitle +
                "', @Department = '" + userDetail.Department +
                "', @Salary = '" + userDetail.Salary +
                "', @UserId = " + userDetail.UserId;

        if (_dapper.ExecuteSql(sql)) return Ok();
        throw new Exception("Failed to Update User");
    }

    [HttpDelete("DeleteUser/{userId}")]
    public IActionResult DeleteUser(int userId)
    {
        string sql = @"
            DELETE FROM TutorialAppSchema.Users
                WHERE UserId = " + userId.ToString();

        if (_dapper.ExecuteSql(sql)) return Ok();

        throw new Exception("Failed to Delete User");
    }

    [HttpDelete("DeleteUserSalary/{userId}")]
    public IActionResult DeleteUserSalary(int userId)
    {
        string sql = @"
            DELETE FROM TutorialAppSchema.UserSalary
                WHERE UserId = " + userId.ToString();

        if (_dapper.ExecuteSql(sql)) return Ok();

        throw new Exception("Failed to Delete UserSalary");
    }

    [HttpDelete("DeleteUserJobInfo/{userId}")]
    public IActionResult DeleteUserJobInfo(int userId)
    {
        string sql = @"
            DELETE FROM TutorialAppSchema.UserJobInfo
                WHERE UserId = " + userId.ToString();

        if (_dapper.ExecuteSql(sql)) return Ok();

        throw new Exception("Failed to Delete UserJobInfo");
    }

}
