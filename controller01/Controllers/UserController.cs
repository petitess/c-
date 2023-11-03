using DotnetAPI.Data;
using DotnetAPI.Models;
using Microsoft.AspNetCore.Mvc;

namespace DotnetAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UserController : ControllerBase
    {
        DataContextDapper _dapper;
        public UserController(IConfiguration config) {
            //Console.WriteLine(config.GetConnectionString("DefaultConnection"));
            _dapper = new DataContextDapper(config);
        }

        [HttpGet("TestConnection")]

        public DateTime TestConnection()
        {
            return _dapper.LoadDataSingle<DateTime>("SELECT GETDATE()");
        }
        //THis is an endpoint
        [HttpGet("GetUsers")]
        public IEnumerable<User> GetUsers()
        {
            string sql = @"SELECT [UserId],
                            [FirstName],
                            [LastName],
                            [Email],
                            [Gender],
                            [Active] 
                        FROM TutorialAppSchema.Users";
            IEnumerable <User> users = _dapper.LoadData<User>(sql);
            return users;
        }
        
        [HttpGet("GetSingleUser/{UserId}")]
        public User GetSingleUser(int UserId)
        {
            string sql = @"SELECT [UserId],
                            [FirstName],
                            [LastName],
                            [Email],
                            [Gender],
                            [Active] 
                         FROM TutorialAppSchema.Users
                            WHERE UserId = " + UserId.ToString();
            User user = _dapper.LoadDataSingle<User>(sql);
            return user;
            
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
            if (_dapper.ExecuteSql(sql))
            {
                return Ok();
            }
            throw new Exception("Failed to update user");
        }

        [HttpPost("AddUser")]
        public IActionResult AddUser(User user)
        {
            string sql = @"
                    INSERT INTO TutorialAppSchema.Users(
                        [FirstName],
                        [LastName],
                        [Email],
                        [Gender],
                        [Active]
                    ) VALUES (" +
                        "'" + user.FirstName +
                        "', '" + user.LastName +
                        "', '" + user.Email +
                        "', '" + user.Gender +
                        "', '" + user.Active +
                    "')";
            Console.WriteLine(sql);
            if (_dapper.ExecuteSql(sql))
            {
                return Ok();
            }
            throw new Exception("Failed to update user");
        }
    }
}