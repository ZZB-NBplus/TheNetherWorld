using BLL;
using Common.Model;
using Microsoft.AspNetCore.Mvc;

namespace TheNetherWorldWebApi.Controllers
{
    [Route("[controller]/[Action]")]
    [ApiController]
    public class TheNetherWorldController : ControllerBase
    {
        private readonly ITheNetherWorldService _theNetherWorldService;
        public TheNetherWorldController(ITheNetherWorldService theNetherWorldService) 
        {
            _theNetherWorldService = theNetherWorldService;
        }
        /// <summary>
        /// 获取生灵登记记录数量
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public JsonResult GetRegistrationCount()
        {
            var dbcode = _theNetherWorldService.GetRegistrationCount();
            
            return new JsonResult(dbcode);
        }

        /// <summary>
        /// 添加生灵登记记录
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public JsonResult AddRegistrationInfo(string uniqueCode, string operatorName)
        {
            var dbcode = _theNetherWorldService.AddRegistrationInfo(uniqueCode, operatorName);

            return new JsonResult(dbcode);
        }

        /// <summary>
        /// 获取生灵登出记录数量
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public JsonResult GetLogoutCount()
        {
            var dbcode = _theNetherWorldService.GetLogoutCount();

            return new JsonResult(dbcode);
        }


        /// <summary>
        /// 添加生灵登出记录
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public JsonResult AddLogoutInfo(string uniqueCode, string operatorName)
        {
            var dbcode = _theNetherWorldService.AddLogoutInfo(uniqueCode, operatorName);
            return new JsonResult(dbcode);
        }

        ///// <summary>
        ///// 获取未登出的生灵信息
        ///// </summary>
        ///// <returns></returns>
        [HttpGet]
        public JsonResult GetNotLogoutInfo()
        {
            List<string> results;
            var dbcode = _theNetherWorldService.GetNotLogoutInfo(out results);
            return new JsonResult(results);
        }

        ///// <summary>
        ///// 获取生灵审判记录数量
        ///// </summary>
        ///// <returns></returns>
        [HttpGet]
        public JsonResult GetJudgmentInfoCount()
        {
            var dbcode = _theNetherWorldService.GetJudgmentInfoCount();
            return new JsonResult(dbcode);
        }

        ///// <summary>
        ///// 添加审判信息记录
        ///// </summary>
        ///// <returns></returns>
        [HttpPost]
        public JsonResult AddJudgmentInfo(JudgmentInfoData judgmentInfoData, string operatorName)
        {
            var dbcode = _theNetherWorldService.AddJudgmentInfo(judgmentInfoData, operatorName);
            return new JsonResult(dbcode);
        }

        ///// <summary>
        ///// 获取未审判的生灵信息
        ///// </summary>
        ///// <returns></returns>
        [HttpGet]
        public JsonResult GetNotJudgmentInfo()
        {
            List<string> results;
            var dbcode = _theNetherWorldService.GetNotJudgmentInfo(out results);
            return new JsonResult(results);
        }


        ///// <summary>
        ///// 获取生灵信息
        ///// </summary>
        ///// <param name="uniqueCode"></param>
        ///// <param name="userInfo"></param>
        ///// <returns></returns>.
        [HttpGet]
        public JsonResult GetUserInfo(string uniqueCode)
        {
            UserInfoData results;
            var dbcode = _theNetherWorldService.GetUserInfo(uniqueCode, out results);
            return new JsonResult(results);
        }

        /// <summary>
        ///// 获取所有生灵信息
        ///// </summary>
        ///// <returns></returns>
        [HttpGet]
        public JsonResult GetAllUserInfo()
        {
            List<UserInfoData> results;
            var dbcode = _theNetherWorldService.GetAllUserInfo(out results);
            return new JsonResult(results);
        }

        ///// <summary>
        ///// 查询所有的操作日志记录信息
        ///// </summary>
        ///// <returns></returns>
        [HttpGet]
        public JsonResult GetOperationLogInfo()
        {
            
            List<OperationLogInfo> results;
            var dbcode = _theNetherWorldService.GetOperationLogInfo(out results);
            return new JsonResult(results);
        }

        ///// <summary>
        ///// 修改角色权限
        ///// </summary>
        ///// <returns></returns>
        [HttpPost]
        public JsonResult UpdateRoleInfo(RoleInfo roleInfo, string operatorName)
        {
            
            var dbcode = _theNetherWorldService.UpdateRoleInfo(roleInfo, operatorName);
            return new JsonResult(dbcode);
        }

        ///// <summary>
        ///// 添加操作者信息
        ///// </summary>
        ///// <param name="operatorInfo"></param>
        ///// <returns></returns>
        [HttpPost]
        public JsonResult AddOperatorInfo(OperatorInfo operatorInfo, string operatorName)
        {
            
            var dbcode = _theNetherWorldService.AddOperatorInfo(operatorInfo, operatorName);
            return new JsonResult(dbcode);
        }

        ///// <summary>
        ///// 修改操作者信息
        ///// </summary>
        ///// <param name="operatorInfo"></param>
        ///// <returns></returns>
        [HttpPost]
        public JsonResult UpdateOperatorInfo(OperatorInfo operatorInfo, string operatorName)
        {
            var dbcode = _theNetherWorldService.UpdateOperatorInfo(operatorInfo, operatorName);
            return new JsonResult(dbcode);
        }

        ///// <summary>
        ///// 删除操作者信息
        ///// </summary>
        ///// <param name="operatorInfo"></param>
        ///// <returns></returns>
        [HttpGet]
        public JsonResult DeleteOperatorInfo(string targetName, string operatorName)
        {
            
            var dbcode = _theNetherWorldService.DeleteOperatorInfo(targetName, operatorName);
            return new JsonResult(dbcode);
        }

        ///// <summary>
        ///// 查询所有角色信息
        ///// </summary>
        ///// <param name=""></param>
        ///// <returns></returns>
        [HttpGet]
        public JsonResult GetAllRoleInfo()
        {
            List<RoleInfo> results;
            
            var dbcode = _theNetherWorldService.GetAllRoleInfo(out results);
            return new JsonResult(results);
        }

        ///// <summary>
        ///// 查询所有操作者信息
        ///// </summary>
        ///// <param name=""></param>
        ///// <returns></returns>
        [HttpGet]
        public JsonResult GetAllOperatorInfo()
        {
            List<OperatorInfo> results;
            
            var dbcode = _theNetherWorldService.GetAllOperatorInfo(out results);
            return new JsonResult(results);
        }

        /// <summary>
        /// 登录
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public JsonResult Login(string name,string password)
        {

            var dbcode = _theNetherWorldService.Login(name, password, out RoleInfo role);
            return new JsonResult(role);
        }
    }
}
