using CPTech;
using CPTech.CustomORM.Dal;
using CPTech.Extensions;
using CPTech.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System.Linq;

namespace ZJHealth.Controllers
{
    [ApiController]
    [Route("[controller]/[action]")]
    public class UserController : ControllerBase
    {
        private readonly ILogger<UserController> logger;
        private readonly SqlHelper sqlHelper = null;

        public UserController(ILogger<UserController> logger, IConfiguration configuration)
        {
            this.logger = logger;
            this.sqlHelper = new SqlHelper(configuration);
        }

        [HttpGet]
        public ResultModel GetUserList()
        {
            logger.LogInformation("GetTuserList");
            string sql = "select top 10 * from tuser";
            TUser user = sqlHelper.ExcuteSql<TUser>(sql).Where(e => e.UserId == "admin1").FirstOrDefault() ?? throw new NetException(500, "未找到用户");

            return ResultModel.Ok(user);
        }

        [HttpGet]
        public ResultModel GetEmployeeList()
        {
            logger.LogInformation("GetTuserList");
            string sql = "select top 10 * from tuser";
            return ResultModel.Ok(sqlHelper.ExcuteSql(sql).ToList());
        }

        [HttpGet]
        public ResultModel Get()
        {
            string ip = HttpContext.Connection.RemoteIpAddress?.MapToIPv4().ToString();
            return ResultModel.Ok(ip);
        }
    }
}
