using System.Data;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using DotnetAPI.Data;
using DotnetAPI.Dtos;
using DotnetAPI.Helpers;
using DotnetAPI.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.IdentityModel.Tokens;

namespace DotnetAPI.Controllers
{
    [Authorize]
    [ApiController]
    [Route("[controller]")]
    public class AuthController(IConfiguration config) : ControllerBase
    {
        private readonly DataContextDapper _dapper = new(config);
        private readonly AuthHelper _authHelper = new(config);

        [AllowAnonymous]
        [HttpPost("Register")]
        public IActionResult Register(UserForRegistrationDto userForRegistration)
        {
            if (userForRegistration.Password == userForRegistration.PasswordConfirm)
            {
                string sql = @"
                SELECT Email FROM TutorialAppSchema.Auth WHERE Email = '" + userForRegistration.Email + "'";
                IEnumerable<string> existingUsers = _dapper.LoadData<string>(sql);

                if (existingUsers.Count() == 0)
                {
                    byte[] passwordSalt = new byte[128 / 8];
                    using (RandomNumberGenerator rng = RandomNumberGenerator.Create())
                    {
                        rng.GetNonZeroBytes(passwordSalt);
                    }

                    byte[] passwordHash = _authHelper.GetPasswordHash(passwordSalt, userForRegistration.Password);

                    string sqlAddAuth = @"
                        INSERT INTO TutorialAppSchema.Auth([Email],
                        [PasswordHash],
                        [PasswordSalt]) VALUES ('" + userForRegistration.Email +
                        "', @PasswordHash, @PasswordSalt)";

                    List<SqlParameter> sqlParameters = new List<SqlParameter>();

                    SqlParameter passwordSaltParameter = new("@PasswordSalt", SqlDbType.VarBinary)
                    {
                        Value = passwordSalt
                    };

                    SqlParameter passwordHashParamenter = new("@PasswordHash", SqlDbType.VarBinary)
                    {
                        Value = passwordHash
                    };

                    sqlParameters.Add(passwordSaltParameter);
                    sqlParameters.Add(passwordHashParamenter);

                    if (_dapper.ExecuteSqlWithParameters(sqlAddAuth, sqlParameters))
                    {
                        string sqlAddUser = @"
                        INSERT INTO TutorialAppSchema.Users(
                            [FirstName],
                            [LastName],
                            [Email],
                            [Gender],
                            [Active]) 
                            VALUES('" + userForRegistration.FirstName +
                            "','" + userForRegistration.LastName +
                            "','" + userForRegistration.Email +
                            "','" + userForRegistration.Gender +
                            "', 1)";
                        if (_dapper.ExecuteSql(sqlAddUser)) return Ok();
                        throw new Exception("Failed to add user");
                    }
                    throw new Exception("Failed to Register user");
                }
                throw new Exception("User with this email already exists!");
            }
            throw new Exception("Passwords do not match");
        }
        [AllowAnonymous]
        [HttpPost("Login")]
        public IActionResult Login(UserForLoginDto userForLogin)
        {
            string sqlForHashAndSalt = @"SELECT 
            [PasswordHash],
            [PasswordSalt] FROM TutorialAppSchema.Auth WHERE Email = '"
            + userForLogin.Email + "'";

            UserForLoginConfirmationDto userForConfirmation = _dapper.LoadDataSingle<UserForLoginConfirmationDto>(sqlForHashAndSalt);

            byte[] passwordHash = _authHelper.GetPasswordHash(userForConfirmation.PasswordSalt, userForLogin.Password);

            for (int index = 0; index < passwordHash.Length; index++)
            {
                if (passwordHash[index] != userForConfirmation.PasswordHash[index])
                {
                    return StatusCode(401, "Incorrect password!");
                }
            }

            string userIdSql = @"SELECT
                [UserId]
                FROM TutorialAppSchema.Users WHERE Email = '" + userForLogin.Email + "'";

            int userId = _dapper.LoadDataSingle<int>(userIdSql);

            return Ok(new Dictionary<string, string> {
                {"token", _authHelper.CreateToken(userId)}
                });
        }

        [HttpGet("RefreshToken")]
        public IActionResult RefreshToken()
        {
            string userId = User.FindFirst("userId")?.Value + "";

            string userIdSql = "SELECT UserId FROM TutorialAppSchema.Users where UserId= "
            + userId;

            int userIdFromDB = _dapper.LoadDataSingle<int>(userIdSql);

            return Ok(new Dictionary<string, string> {
                {"token", _authHelper.CreateToken(userIdFromDB)}
                });
        }

    }
}