using System.Data;
using Dapper;
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
        string sql = @"EXEC TutorialAppSchema.spUser_Upsert
                @FirstName = @FirstNameParameter, 
                @LastName = @LastNameParameter, 
                @Email = @EmailParameter, 
                @Gender = @GenderParameter, 
                @Active = @ActiveParameter, 
                @JobTitle = @JobTitleParameter, 
                @Department = @DepartmentParameter, 
                @Salary = @SalaryParameter, 
                @UserId = @UserIdParameter";

        DynamicParameters sqlParameters = new();
        sqlParameters.Add("@FirstNameParameter", userDetail.FirstName, DbType.String);
        sqlParameters.Add("@LastNameParameter", userDetail.LastName, DbType.String);
        sqlParameters.Add("@EmailParameter", userDetail.Email, DbType.String);
        sqlParameters.Add("@GenderParameter", userDetail.Gender, DbType.String);
        sqlParameters.Add("@ActiveParameter", userDetail.Active, DbType.Boolean);
        sqlParameters.Add("@JobTitleParameter", userDetail.JobTitle, DbType.String);
        sqlParameters.Add("@DepartmentParameter", userDetail.Department, DbType.String);
        sqlParameters.Add("@SalaryParameter", userDetail.Salary, DbType.Decimal);
        sqlParameters.Add("@UserIdParameter", userDetail.UserId, DbType.Int32);


        if (_dapper.ExecuteSqlWithParameters(sql, sqlParameters)) return Ok();
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
