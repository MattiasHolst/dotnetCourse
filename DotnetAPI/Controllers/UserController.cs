using DotnetAPI.Data;
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

}
