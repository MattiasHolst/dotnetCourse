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

    [HttpPut("EditUser")]
    public IActionResult EditUser(User user)
    {
        string sql = @"
            UPDATE TutorialAppSchema.Users 
                SET [FirstName] = '" + user.FirstName +
                "', [LastName] = '" + user.LastName +
                "', [Email] = '" + user.Email +
                "', [Gender] = '" + user.Gender +
                "', [Active] = '" + user.Active +
                "' WHERE UserId = " + user.UserId;

        Console.WriteLine(sql);

        if (_dapper.ExecuteSql(sql)) return Ok();
        throw new Exception("Failed to Update User");
    }

    [HttpPost("AddUser")]
    public IActionResult AddUser(UserToAddDto user)
    {
        string sql = @"
            INSERT INTO TutorialAppSchema.Users(
                [FirstName],
                [LastName],
                [Email],
                [Gender],
                [Active]) 
                VALUES('" + user.FirstName +
                "','" + user.LastName +
                "','" + user.Email +
                "','" + user.Gender +
                "','" + user.Active + "')";


        Console.WriteLine(sql);

        if (_dapper.ExecuteSql(sql)) return Ok();
        throw new Exception("Failed to Add User");
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

    [HttpPost("AddUserSalary")]
    public IActionResult AddUserSalary(UserSalary user)
    {
        string sql = @"
            INSERT INTO TutorialAppSchema.UserSalary(
                UserId,
                Salary
            ) VALUES(" + user.UserId + ", " + user.Salary + ")";


        if (_dapper.ExecuteSql(sql)) return Ok();
        throw new Exception("Failed to Add UserSalary");
    }

    [HttpPut("EditUserSalary")]
    public IActionResult EditUserSalary(UserSalary user)
    {
        string sql = @"
            UPDATE TutorialAppSchema.UserSalary 
                SET [Salary] = " + user.Salary +
                " WHERE UserId = " + user.UserId;

        if (_dapper.ExecuteSql(sql)) return Ok();
        throw new Exception("Failed to Update UserSalary");
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


    [HttpPost("AddUserJobInfo")]
    public IActionResult AddUserJobInfo(UserJobInfo user)
    {
        string sql = @"
            INSERT INTO TutorialAppSchema.UserJobInfo(
                UserId,
                JobTitle,
                Department)
                VALUES(" + user.UserId +
                ", '" + user.JobTitle +
                "','" + user.Department +
                "')";


        if (_dapper.ExecuteSql(sql)) return Ok();
        throw new Exception("Failed to Add UserJobInfo");
    }

    [HttpPut("EditUserJobInfo")]
    public IActionResult EditUserJobInfo(UserJobInfo user)
    {
        string sql = @"
            UPDATE TutorialAppSchema.UserJobInfo 
                 SET [JobTitle] = '" + user.JobTitle +
                "', [Department] = '" + user.Department +
                "' WHERE UserId = " + user.UserId;

        if (_dapper.ExecuteSql(sql)) return Ok();
        throw new Exception("Failed to Update UserJobInfo");
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
