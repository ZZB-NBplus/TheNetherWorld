using Common.Model;
using DAL;

namespace BLL
{
    public interface ITheNetherWorldService
    {
        /// <summary>
        /// 获取生灵登记记录数量
        /// </summary>
        /// <returns></returns>
        public int GetRegistrationCount();

        /// <summary>
        /// 添加生灵登记记录
        /// </summary>
        /// <returns></returns>
        public int AddRegistrationInfo(string uniqueCode,string operatorName);

        /// <summary>
        /// 获取生灵登出记录数量
        /// </summary>
        /// <returns></returns>
        public int GetLogoutCount();

        /// <summary>
        /// 添加生灵登出记录
        /// </summary>
        /// <returns></returns>
        public int AddLogoutInfo(string uniqueCode, string operatorName);

        /// <summary>
        /// 获取未登出的生灵信息
        /// </summary>
        /// <returns></returns>
        public int GetNotLogoutInfo(out List<string> userInfoDatas)
;

        /// <summary>
        /// 获取生灵审判记录数量
        /// </summary>
        /// <returns></returns>
        public int GetJudgmentInfoCount();

        /// <summary>
        /// 添加审判信息记录
        /// </summary>
        /// <returns></returns>
        public int AddJudgmentInfo(JudgmentInfoData judgmentInfoData, string operatorName);

        /// <summary>
        /// 获取未审判的生灵信息
        /// </summary>
        /// <returns></returns>
        public int GetNotJudgmentInfo(out List<string> judgmentInfoDatas);

        /// <summary>
        /// 获取生灵信息
        /// </summary>
        /// <param name="uniqueCode"></param>
        /// <param name="userInfo"></param>
        /// <returns></returns>
        public int GetUserInfo(string uniqueCode, out UserInfoData userInfoData);

        /// <summary>
        /// 获取所有生灵信息
        /// </summary>
        /// <returns></returns>
        public int GetAllUserInfo(out List<UserInfoData> userInfoDatas);

        /// <summary>
        /// 查询所有的操作日志记录信息
        /// </summary>
        /// <returns></returns>
        public int GetOperationLogInfo(out List<OperationLogInfo> operationLogInfos);



        /// <summary>
        /// 修改角色权限
        /// </summary>
        /// <returns></returns>
        public int UpdateRoleInfo(RoleInfo roleInfo, string operatorName);

        /// <summary>
        /// 添加操作者信息
        /// </summary>
        /// <param name="operatorInfo"></param>
        /// <returns></returns>
        public int AddOperatorInfo(OperatorInfo operatorInfo, string operatorName);

        /// <summary>
        /// 修改操作者信息
        /// </summary>
        /// <param name="operatorInfo"></param>
        /// <returns></returns>
        public int UpdateOperatorInfo(OperatorInfo operatorInfo, string operatorName);

        /// <summary>
        /// 删除操作者信息
        /// </summary>
        /// <param name="operatorInfo"></param>
        /// <returns></returns>
        public int DeleteOperatorInfo(string targetName, string operatorName);

        /// <summary>
        /// 查询所有角色信息
        /// </summary>
        /// <param name=""></param>
        /// <returns></returns>
        public int GetAllRoleInfo(out List<RoleInfo> roleInfos);

        /// <summary>
        /// 查询所有操作者信息
        /// </summary>
        /// <param name=""></param>
        /// <returns></returns>
        public int GetAllOperatorInfo( out List<OperatorInfo> operatorInfos);


        /// <summary>
        /// 登录
        /// </summary>
        /// <returns></returns>
        public int Login(string name, string password, out RoleInfo role);
    }
}
