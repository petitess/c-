using DotnetAPI2.Data;
using DotnetAPI2.Models;
using Microsoft.AspNetCore.Mvc;
using System.Reflection;
using System;
using Dapper;
using System.Data;
using System.Data.SqlClient;
using DotnetAPI2.Helpers;
using Microsoft.AspNetCore.Authorization;

namespace DotnetAPI2.Controllers
{
    [Authorize]
    [ApiController]
    [Route("[controller]")]
    public class UserCompleteController : ControllerBase
    {
        private readonly DataContextDapper _dapper;
        private readonly ReusableSql _reusableSql;
        public UserCompleteController(IConfiguration config)
        {
            _dapper = new DataContextDapper(config);
            _reusableSql = new ReusableSql(config);
        }

        [HttpGet("GetUsers/{UserId}/{isActive}")]
        public IEnumerable<UserComplete> GetUsers(int UserId, bool isActive)
        {
            string sql = @"EXEC TutorialAppSchema.spUsers_Get";
            string stringParameters = "";

            DynamicParameters sqlParameters = new DynamicParameters();

            if (UserId != 0) 
            {
                stringParameters += ", @UserId=@UserIdParameter";
                sqlParameters.Add("@UserIdParameter", UserId, DbType.Int32);
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

            IEnumerable<UserComplete> users = _dapper.LoadDataWithParameters<UserComplete>(sql, sqlParameters);
            return users;
        }

        [HttpPut("UpsertUser")]
        public IActionResult UpsertUser(UserComplete user)
        {
            if (_reusableSql.UpsertUser(user))
            {
                return Ok();
            }
            throw new Exception("Failed to update user");
        }

        [HttpDelete("DeleteUser/{UserId}")]
        public IActionResult DeleteUser(int userId)
        {
            string sql = @"EXEC TutorialAppSchema.spUser_Delete
                           @UserId = @UserIdParameter";

            DynamicParameters sqlParameters = new DynamicParameters();
            sqlParameters.Add("@UserIdParameter", userId, DbType.Int32);

            if (_dapper.ExecuteSqlWithParameters(sql, sqlParameters))
            {
                return Ok();
            }
            throw new Exception("Failed to delete user");
        }
    }
}