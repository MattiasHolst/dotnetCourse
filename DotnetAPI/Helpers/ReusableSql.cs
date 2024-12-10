using System.Data;
using Dapper;
using DotnetAPI.Data;
using DotnetAPI.Models;
using Microsoft.AspNetCore.Mvc;

namespace DotnetAPI.Helpers
{
    public class ReusableSql(IConfiguration config)
    {

        private readonly DataContextDapper _dapper = new(config);
        public bool UpsertUser(UserDetail userDetail)
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


            return _dapper.ExecuteSqlWithParameters(sql, sqlParameters);
        }
    }
}