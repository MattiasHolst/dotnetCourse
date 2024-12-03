using AutoMapper;
using DotnetAPI.Data;
using DotnetAPI.Dtos;
using DotnetAPI.Models;
using Microsoft.AspNetCore.Mvc;

namespace DotnetAPI.Controllers;

[ApiController]
[Route("[controller]")]
public class UserEFController(IConfiguration config) : ControllerBase
{
    readonly DataContextEF _entityFramework = new(config);
    IMapper _mapper = new Mapper(new MapperConfiguration(cfg =>
    {
        cfg.CreateMap<UserToAddDto, User>();
    }));


    [HttpGet("GetUsers")]
    public IEnumerable<User> GetUsers()
    {
        IEnumerable<User> users = [.. _entityFramework.Users];
        return users;
    }

    [HttpGet("GetSingleUser/{userId}")]
    public User GetSingleUser(int userId)
    {
        User? user = _entityFramework.Users.Where(u => u.UserId == userId).FirstOrDefault();
        if (user != null)
        {
            return user;
        }
        throw new Exception("Failed to get user");
    }

    [HttpPut("EditUser")]
    public IActionResult EditUser(User user)
    {
        User? userDb = _entityFramework.Users.Where(u => u.UserId == user.UserId).FirstOrDefault();
        if (userDb != null)
        {
            userDb.Active = user.Active;
            userDb.FirstName = user.FirstName;
            userDb.LastName = user.LastName;
            userDb.Email = user.Email;
            userDb.Gender = user.Gender;
            if (_entityFramework.SaveChanges() > 0)
            {
                return Ok();
            }
        }
        throw new Exception("Failed to Update User");
    }

    [HttpPost("AddUser")]
    public IActionResult AddUser(UserToAddDto user)
    {
        User userDb = _mapper.Map<User>(user);

        _entityFramework.Add(userDb);
        if (_entityFramework.SaveChanges() > 0)
        {
            return Ok();
        }
        throw new Exception("Failed to Add User");
    }


    [HttpDelete("DeleteUser/{userId}")]
    public IActionResult DeleteUser(int userId)
    {
        User? userDb = _entityFramework.Users.Where(u => u.UserId == userId).FirstOrDefault();
        if (userDb != null)
        {
            _entityFramework.Users.Remove(userDb);
            if (_entityFramework.SaveChanges() > 0)
            {
                return Ok();
            }
        }
        throw new Exception("Failed to get user");
    }

    [HttpGet("GetUserSalary/{userId}")]
    public UserSalary GetUserSalary(int userId)
    {
        UserSalary? userSalary = _entityFramework.UserSalary.Where(u => u.UserId == userId).FirstOrDefault();
        if (userSalary != null)
        {
            return userSalary;
        }
        throw new Exception("Failed to get userSalary");
    }

    [HttpPut("EditUserSalary")]
    public IActionResult EditUserSalary(UserSalary userSalary)
    {
        UserSalary? userSalaryDb = _entityFramework.UserSalary.Where(u => u.UserId == userSalary.UserId).FirstOrDefault();
        if (userSalaryDb != null)
        {
            userSalaryDb.Salary = userSalary.Salary;
            if (_entityFramework.SaveChanges() > 0)
            {
                return Ok();
            }
        }
        throw new Exception("Failed to Update UserSalary");
    }

    [HttpPost("AddUserSalary")]
    public IActionResult AddUserSalary(UserSalary userSalary)
    {
        _entityFramework.UserSalary.Add(userSalary);
        if (_entityFramework.SaveChanges() > 0)
        {
            return Ok();
        }
        throw new Exception("Failed to Add UserSalary");
    }


    [HttpDelete("DeleteUserSalary/{userId}")]
    public IActionResult DeleteUserSalary(int userId)
    {
        UserSalary? userSalaryDb = _entityFramework.UserSalary.Where(u => u.UserId == userId).FirstOrDefault();
        if (userSalaryDb != null)
        {
            _entityFramework.UserSalary.Remove(userSalaryDb);
            if (_entityFramework.SaveChanges() > 0)
            {
                return Ok();
            }
        }
        throw new Exception("Failed to Delete User Salary");
    }

    [HttpGet("GetUserJobInfo/{userId}")]
    public UserJobInfo GetUserJobInfo(int userId)
    {
        UserJobInfo? userJobInfo = _entityFramework.UserJobInfo.Where(u => u.UserId == userId).FirstOrDefault();
        if (userJobInfo != null)
        {
            return userJobInfo;
        }
        throw new Exception("Failed to get User Job Info");
    }

    [HttpPut("EditUserJobInfo")]
    public IActionResult EditUserJobInfo(UserJobInfo userJobInfo)
    {
        UserJobInfo? userJobInfoDb = _entityFramework.UserJobInfo.Where(u => u.UserId == userJobInfo.UserId).FirstOrDefault();
        if (userJobInfoDb != null)
        {
            userJobInfoDb.JobTitle = userJobInfo.JobTitle;
            userJobInfoDb.Department = userJobInfo.Department;
            if (_entityFramework.SaveChanges() > 0)
            {
                return Ok();
            }
        }
        throw new Exception("Failed to Update User Job Info");
    }

    [HttpPost("AddUserJobInfo")]
    public IActionResult AddUserJobInfo(UserJobInfo userJobInfo)
    {
        _entityFramework.UserJobInfo.Add(userJobInfo);
        if (_entityFramework.SaveChanges() > 0)
        {
            return Ok();
        }
        throw new Exception("Failed to Add User Job Info");
    }


    [HttpDelete("DeleteUserJobInfo/{userId}")]
    public IActionResult DeleteUserJobInfo(int userId)
    {
        UserJobInfo? userJobInfoDb = _entityFramework.UserJobInfo.Where(u => u.UserId == userId).FirstOrDefault();
        if (userJobInfoDb != null)
        {
            _entityFramework.UserJobInfo.Remove(userJobInfoDb);
            if (_entityFramework.SaveChanges() > 0)
            {
                return Ok();
            }
        }
        throw new Exception("Failed to Delete User Job Info");
    }

}
