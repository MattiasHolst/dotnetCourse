using DotnetAPI.Data;
using DotnetAPI.Dtos;
using DotnetAPI.Models;
using Microsoft.AspNetCore.Mvc;

namespace DotnetAPI.Controllers;

[ApiController]
[Route("[controller]")]
public class UserController(IConfiguration config) : ControllerBase
{
    readonly DataContextDapper _dapper = new(config);


    [HttpGet("GetUsers")]
    public IEnumerable<User> GetUsers()
    {
        string sql = @"
            SELECT  [UserId]
                , [FirstName]
                , [LastName]
                , [Email]
                , [Gender]
                , [Active]
            FROM  TutorialAppSchema.Users;";
        return _dapper.LoadData<User>(sql);

    }

    [HttpGet("GetSingleUser/{userId}")]
    public User GetSingleUser(int userId)
    {
        string sql = @"
            SELECT  [UserId]
                , [FirstName]
                , [LastName]
                , [Email]
                , [Gender]
                , [Active]
            FROM  TutorialAppSchema.Users
                WHERE UserId = " + userId.ToString();
        return _dapper.LoadDataSingle<User>(sql);
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

    [HttpGet("GetUserSalary/{userId}")]
    public UserSalary GetUserSalary(int userId)
    {
        string sql = @"
            SELECT  [UserId]
                , [Salary]
                , [AvgSalary]
            FROM  TutorialAppSchema.UserSalary
                WHERE UserId = " + userId.ToString();
        return _dapper.LoadDataSingle<UserSalary>(sql);

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


    [HttpGet("GetUserJobInfo/{userId}")]
    public UserJobInfo GetUserJobInfo(int userId)
    {
        string sql = @"
            SELECT  [UserId]
                    , [JobTitle]
                    , [Department]
            FROM  TutorialAppSchema.UserJobInfo
                WHERE UserId = " + userId.ToString();
        return _dapper.LoadDataSingle<UserJobInfo>(sql);

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
