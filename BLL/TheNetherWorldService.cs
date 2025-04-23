using Common.Model;
using DAL;
using Microsoft.EntityFrameworkCore;
using System.Data;

namespace BLL
{
    public class TheNetherWorldService : ITheNetherWorldService
    {
        /// <summary>
        /// 添加审判信息记录
        /// </summary>
        /// <returns></returns>
        // 添加审判信息
        public int AddJudgmentInfo(JudgmentInfoData judgmentInfoData, string operatorName)
        {

            // 判断审判信息是否为空
            if (judgmentInfoData == null) return -1;
            // 使用TheNetherWorldContext数据库上下文
            using (TheNetherWorldContext db = new TheNetherWorldContext())
            {
                try
                {
                    // 根据uniqueCode查询用户信息
                    var userInfo = db.UserInfos.Where(x => x.UniqueCode == judgmentInfoData.UniqueCode).FirstOrDefault();
                    // 如果用户信息为空，返回-2
                    if (userInfo == null)
                    {
                        return -2;
                    }
                    // 根据operatorName查询操作员信息
                    var operatorInfo = db.Operators.Where(x => x.Name == operatorName).FirstOrDefault();
                    // 如果操作员信息为空，返回-3
                    if (operatorInfo==null)
                    {
                        return -3;
                    }

                    // 根据userInfoId查询审判信息表是否存在
                    var judgmentInfoTableExist = db.JudgmentInfoTables.Where(x=>x.UserInfoId== userInfo.Id).FirstOrDefault();
                    // 如果审判信息表存在，返回-4
                    if (judgmentInfoTableExist != null)
                    {
                        return -4;
                    }

                    // 创建操作日志
                    OperationLog operationLog = new OperationLog();
                    operationLog.OperationTime = DateTime.Now;
                    operationLog.Info = "添加审判信息记录" + judgmentInfoData.UniqueCode;
                    operationLog.Operator = operatorInfo;
                    db.OperationLogs.Add(operationLog);

                    // 创建审判信息表
                    JudgmentInfoTable judgmentInfoTable = new JudgmentInfoTable();
                    judgmentInfoTable.JudgmentTime = DateTime.Now;
                    judgmentInfoTable.Name = judgmentInfoData.Name;
                    judgmentInfoTable.Sex= judgmentInfoData.Sex;
                    judgmentInfoTable.Lifespan= judgmentInfoData.Lifespan;
                    judgmentInfoTable.BirthTime= judgmentInfoData.BirthTime;
                    judgmentInfoTable.DeathTime= judgmentInfoData.DeathTime;
                    judgmentInfoTable.DeathCause= judgmentInfoData.DeathCause;
                    judgmentInfoTable.PreDeathBehaviorRecord= judgmentInfoData.PreDeathBehaviorRecord;
                    judgmentInfoTable.JudgmentInfo = judgmentInfoData.JudgmentInfo;
                    judgmentInfoTable.UserInfo = userInfo;

                    db.JudgmentInfoTables.Add(judgmentInfoTable);
                    userInfo.JudgmentInfoState = 1;

                    db.SaveChanges();

                    return 0;

                }
                catch (Exception)
                {

                    return -5;
                }
            }
        }

        /// <summary>
        /// 添加生灵登出记录
        /// </summary>
        /// <returns></returns>
        public int AddLogoutInfo(string uniqueCode, string operatorName)
        {
            if (string.IsNullOrEmpty(uniqueCode)) return -1;

            using (TheNetherWorldContext db = new TheNetherWorldContext())
            {
                try
                {
                    var userInfo = db.UserInfos.Where(x => x.UniqueCode == uniqueCode).FirstOrDefault();
                    if (userInfo==null)
                    {
                        return -2;
                    }
                    var operatorInfo = db.Operators.Where(x => x.Name == operatorName).FirstOrDefault();
                    if (operatorInfo == null)
                    {
                        return -3;
                    }
                    OperationLog operationLog = new OperationLog();
                    operationLog.OperationTime = DateTime.Now;
                    operationLog.Info = "添加审判信息记录" + uniqueCode;
                    operationLog.Operator = operatorInfo;
                    db.OperationLogs.Add(operationLog);


                    userInfo.LogoutState = 1;

                    LogoutTable logoutTable=new LogoutTable();
                    logoutTable.LogoutTime=DateTime.Now;
                    logoutTable.UserInfo=userInfo;

                    db.LogoutTables.Add(logoutTable);


                    db.SaveChanges();
                    return 0;
                }
                catch (Exception)
                {
                    return -4;
                }

            }
        }


        /// <summary>
        /// 添加操作者信息
        /// </summary>
        /// <param name="operatorInfo"></param>
        /// <returns></returns>
        public int AddOperatorInfo(OperatorInfo operatorInfo, string operatorName)
        {
            if (operatorInfo==null)
            {
                return -1;
            }

            using (TheNetherWorldContext db = new TheNetherWorldContext())
            {
                try
                {
                    
                    var operatorIsExist = db.Operators.Where(x => x.Name == operatorInfo.Name).FirstOrDefault();
                    if (operatorIsExist != null) return -2;

                    var currentoperatorInfo = db.Operators.Where(x => x.Name == operatorName).FirstOrDefault();
                    if (currentoperatorInfo == null) return -3;
                    
                    var role = db.Roles.Where(x => x.Name == operatorInfo.RoleName).FirstOrDefault();
                    if (role == null) return -4;


                    OperationLog operationLog = new OperationLog();
                    operationLog.OperationTime = DateTime.Now;
                    operationLog.Info = "添加操作者:" + operatorInfo.Name;
                    operationLog.Operator = currentoperatorInfo;
                    db.OperationLogs.Add(operationLog);

                    Operator operatorData=new Operator();
                    operatorData.Name = operatorInfo.Name;
                    operatorData.Password=operatorInfo.Password;
                    operatorData.Role= role;

                    db.Operators.Add(operatorData); 

                    db.SaveChanges();

                    return 0;
                }
                catch (Exception)
                {

                    return -5;
                }
                
            }

        }

        /// <summary>
        /// 添加生灵登记记录
        /// </summary>
        /// <returns></returns>
        public int AddRegistrationInfo(string uniqueCode, string operatorName)
        {
            if (string.IsNullOrEmpty(uniqueCode)) return -1;
            
            using (TheNetherWorldContext db=new TheNetherWorldContext())
            {
                try
                {
                    var userInfo = db.UserInfos.Where(x => x.UniqueCode == uniqueCode).FirstOrDefault();
                    if (userInfo!=null)
                    {
                        return -2;
                    }
                    var operatorInfo = db.Operators.Where(x => x.Name == operatorName).FirstOrDefault();
                    if (operatorInfo == null)
                    {
                        return -3;
                    }
                    OperationLog operationLog = new OperationLog();
                    operationLog.OperationTime = DateTime.Now;
                    operationLog.Info = "添加生灵信息记录" + uniqueCode;
                    operationLog.Operator = operatorInfo;
                    db.OperationLogs.Add(operationLog);

                    userInfo = new UserInfo();
                    userInfo.UniqueCode= uniqueCode;
                    userInfo.RegistrationState = 1;
                    db.UserInfos.Add(userInfo);

                    RegistrationTable registrationTable = new RegistrationTable();
                    registrationTable.UserInfo = userInfo;
                    registrationTable.RegistrationTime=DateTime.Now;
                    db.RegistrationTables.Add(registrationTable);

                    db.SaveChanges();
                    return 0;
                }
                catch (Exception)
                {
                    return -2;
                }
                
            }
        }

        /// <summary>
        /// 删除操作者信息
        /// </summary>
        /// <param name="operatorInfo"></param>
        /// <returns></returns>
        public int DeleteOperatorInfo(string targetName, string operatorName)
        {
            if (string.IsNullOrEmpty(targetName))
            {
                return -1;
            }
            using (TheNetherWorldContext db = new TheNetherWorldContext())
            {
                try
                {
                    var operatorinfo = db.Operators.Where(x => x.Name == targetName).FirstOrDefault();
                    if (operatorinfo == null) return -2;

                    var currentoperatorInfo = db.Operators.Where(x => x.Name == operatorName).FirstOrDefault();
                    if (currentoperatorInfo == null) return -3;


                    OperationLog operationLog = new OperationLog();
                    operationLog.OperationTime = DateTime.Now;
                    operationLog.Info = "删除操作者:" + operatorinfo.Name;
                    operationLog.Operator = currentoperatorInfo;
                    db.OperationLogs.Add(operationLog);

                    db.Operators.Remove(operatorinfo);

                    db.SaveChanges();

                    return 0;
                }
                catch (Exception)
                {

                    return -5;
                }

            }

        }

        

        /// <summary>
        /// 获取生灵审判记录数量
        /// </summary>
        /// <returns></returns>
        public int GetJudgmentInfoCount()
        {
            using (TheNetherWorldContext db = new TheNetherWorldContext())
            {
                try
                {
                    int count = db.JudgmentInfoTables.Count();
                    if (count == 0)
                    {
                        return -1;
                    }
                    return count;
                }
                catch (Exception)
                {
                    return -2;
                }

            }
        }

        /// <summary>
        /// 获取生灵登出记录数量
        /// </summary>
        /// <returns></returns>
        public int GetLogoutCount()
        {
            using (TheNetherWorldContext db = new TheNetherWorldContext())
            {
                try
                {
                    int count = db.LogoutTables.Count();
                    if (count == 0)
                    {
                        return -1;
                    }
                    return count;
                }
                catch (Exception)
                {
                    return -2;
                }

            }
        }

        /// <summary>
        /// 获取未审判的生灵信息
        /// </summary>
        /// <returns></returns>
        public int GetNotJudgmentInfo(out List<string> judgmentInfoDatas)
        {
            judgmentInfoDatas = new List<string>();
            using (TheNetherWorldContext db = new TheNetherWorldContext())
            {
                try
                {
                    var userInfos = db.UserInfos.Where(x=>x.JudgmentInfoState!=1);
                    foreach (var item in userInfos)
                    {
                        judgmentInfoDatas.Add(item.UniqueCode);
                    }

                    return 0;
                }
                catch (Exception)
                {
                    return -2;
                }

            }
        }

        /// <summary>
        /// 获取未登出的生灵信息
        /// </summary>
        /// <returns></returns>
        public int GetNotLogoutInfo(out List<string> userInfoDatas)
        {
            userInfoDatas = new List<string>();
            using (TheNetherWorldContext db = new TheNetherWorldContext())
            {
                try
                {
                    var userInfos = db.UserInfos.Where(x => x.LogoutState != 1);
                    foreach (var item in userInfos)
                    {
                        userInfoDatas.Add(item.UniqueCode);
                    }

                    return 0;
                }
                catch (Exception)
                {
                    return -2;
                }

            }
        }

        /// <summary>
        /// 查询所有的操作日志记录信息
        /// </summary>
        /// <returns></returns>
        public int GetOperationLogInfo(out List<OperationLogInfo> operationLogInfos)
        {
            operationLogInfos = new List<OperationLogInfo>();
            using (TheNetherWorldContext db = new TheNetherWorldContext())
            {
                try
                {
                    var operationLogs = db.OperationLogs.Include(o => o.Operator).ToList();

                    foreach (var operationLog in operationLogs)
                    {
                        OperationLogInfo operationLogInfo = new OperationLogInfo();

                        operationLogInfo.OperatorName= operationLog.Operator.Name;
                        operationLogInfo.OperationTime = operationLog.OperationTime;
                        operationLogInfo.Info= operationLog.Info;

                        operationLogInfos.Add(operationLogInfo);
                    }
                    return 0;
                }
                catch (Exception)
                {
                    return -2;
                }

            }
        }

        /// <summary>
        /// 获取生灵登记记录数量
        /// </summary>
        /// <returns></returns>
        public int GetRegistrationCount()
        {

            using (TheNetherWorldContext db = new TheNetherWorldContext())
            {
                try
                {
                    int count=db.RegistrationTables.Count();
                    if (count==0)
                    {
                        return -1;
                    }
                    return count;
                }
                catch (Exception)
                {
                    return -2;
                }

            }
        }

        /// <summary>
        /// 获取生灵信息
        /// </summary>
        /// <param name="uniqueCode"></param>
        /// <param name="userInfo"></param>
        /// <returns></returns>
        public int GetUserInfo(string uniqueCode, out UserInfoData userInfoData)
        {
            userInfoData=new UserInfoData();
            if (string.IsNullOrEmpty(uniqueCode)) return -1;

            using (TheNetherWorldContext db = new TheNetherWorldContext())
            {
                try
                {
                    var userinfo = db.UserInfos.Where(x=>x.UniqueCode==uniqueCode).FirstOrDefault();
                    if (userinfo==null) return -2;

                    userInfoData.UniqueCode= userinfo.UniqueCode;
                    userInfoData.RegistrationState= userinfo.RegistrationState;
                    userInfoData.JudgmentInfoState= userinfo.JudgmentInfoState;
                    userInfoData.LogoutState= userinfo.LogoutState;

                    var registration = db.RegistrationTables.Where(x=>x.UserInfoId== userinfo.Id).FirstOrDefault();
                    if (registration!=null)
                    {
                        userInfoData.RegistrationTime = registration.RegistrationTime;
                    }

                    var judgmentInfo = db.JudgmentInfoTables.Where(x=>x.UserInfoId== userinfo.Id).FirstOrDefault();
                    if (judgmentInfo!=null)
                    {
                        userInfoData.Name= judgmentInfo.Name;
                        userInfoData.Sex= judgmentInfo.Sex;
                        userInfoData.Lifespan= judgmentInfo.Lifespan;
                        userInfoData.BirthTime= judgmentInfo.BirthTime;
                        userInfoData.DeathTime= judgmentInfo.DeathTime;
                        userInfoData.DeathCause= judgmentInfo.DeathCause;
                        userInfoData.PreDeathBehaviorRecord= judgmentInfo.PreDeathBehaviorRecord;
                        userInfoData.JudgmentTime = judgmentInfo.JudgmentTime;
                        userInfoData.JudgmentInfo= judgmentInfo.JudgmentInfo;   
                    }

                    var logoutinfo = db.LogoutTables.Where(x=>x.UserInfoId== userinfo.Id).FirstOrDefault();
                    if (logoutinfo != null) 
                    {
                        userInfoData.LogoutTime= logoutinfo.LogoutTime;
                    }

                    return 0;

                }
                catch (Exception)
                {

                    return -3;
                }
            }
        }

        /// <summary>
        /// 获取所有生灵信息
        /// </summary>
        /// <returns></returns>
        public int GetAllUserInfo(out List<UserInfoData> userInfoDatas)
        {
            userInfoDatas = new List<UserInfoData>();
            using (TheNetherWorldContext db = new TheNetherWorldContext())
            {
                try
                {
                    var userInfos = db.UserInfos.ToList();
                    foreach (var item in userInfos)
                    {
                        UserInfoData userInfoData = new UserInfoData();

                        userInfoData.UniqueCode = item.UniqueCode;
                        userInfoData.JudgmentInfoState = item.JudgmentInfoState;
                        userInfoData.LogoutState = item.LogoutState;
                        userInfoData.RegistrationState = item.RegistrationState;

                        var registrationInfo = db.RegistrationTables.Where(x => x.UserInfoId == item.Id).FirstOrDefault();
                        if (registrationInfo!=null)
                        {
                            userInfoData.RegistrationTime = registrationInfo.RegistrationTime;
                        }

                        var judgmentInfo = db.JudgmentInfoTables.Where(x=>x.UserInfoId==item.Id).FirstOrDefault();
                        if (judgmentInfo!=null)
                        {
                            userInfoData.JudgmentTime = judgmentInfo.JudgmentTime;
                            userInfoData.Name=judgmentInfo.Name;
                            userInfoData.Sex=judgmentInfo.Sex;
                            userInfoData.Lifespan=judgmentInfo.Lifespan;
                            userInfoData.BirthTime=judgmentInfo.BirthTime;
                            userInfoData.DeathTime=judgmentInfo.DeathTime;
                            userInfoData.DeathCause=judgmentInfo.DeathCause;
                            userInfoData.PreDeathBehaviorRecord = judgmentInfo.PreDeathBehaviorRecord;
                            userInfoData.JudgmentInfo = judgmentInfo.JudgmentInfo;
                        }

                        var logoutInfo = db.LogoutTables.Where(x=>x.UserInfoId== item.Id).FirstOrDefault();
                        if (logoutInfo != null) 
                        {
                            userInfoData.LogoutTime=logoutInfo.LogoutTime;
                        }

                        userInfoDatas.Add(userInfoData);
                    }

                    return 0;
                }
                catch (Exception)
                {
                    return -1;
                }

            }

        }

        /// <summary>
        /// 修改操作者信息
        /// </summary>
        /// <param name="operatorInfo"></param>
        /// <returns></returns>
        public int UpdateOperatorInfo(OperatorInfo operatorInfo, string operatorName)
        {
            if (operatorInfo == null)
            {
                return -1;
            }

            using (TheNetherWorldContext db = new TheNetherWorldContext())
            {
                try
                {
                    var operatorExist = db.Operators.Where(x => x.Name == operatorInfo.Name).FirstOrDefault();
                    if (operatorExist == null) return -2;

                    var currentoperatorInfo = db.Operators.Where(x => x.Name == operatorName).FirstOrDefault();
                    if (currentoperatorInfo == null) return -3;

                    var role = db.Roles.Where(x => x.Name == operatorInfo.RoleName).FirstOrDefault();
                    if (role == null) return -4;

                    OperationLog operationLog = new OperationLog();
                    operationLog.OperationTime = DateTime.Now;
                    operationLog.Info = "修改操作者信息:" + operatorInfo.Name;
                    operationLog.Operator = currentoperatorInfo;
                    db.OperationLogs.Add(operationLog);

                    operatorExist.Password= operatorInfo.Password;
                    operatorExist.Role= role;

                    db.SaveChanges();
                    return 0;
                }
                catch (Exception)
                {

                    return -5;
                }
                
                
            }
        }

        // <summary>
        /// 修改角色权限
        /// </summary>
        /// <returns></returns>
        public int UpdateRoleInfo(RoleInfo roleInfo, string operatorName)
        {
            if (roleInfo == null)
            {
                return -1;
            }
            using (TheNetherWorldContext db = new TheNetherWorldContext())
            {
                try
                {
                    var role = db.Roles.Where(x => x.Name == roleInfo.Name).FirstOrDefault();
                    if (role==null)
                    {
                        return -2;
                    }

                    var operatorInfo = db.Operators.Where(x => x.Name == operatorName).FirstOrDefault();
                    if (operatorInfo == null)
                    {
                        return -3;
                    }
                    OperationLog operationLog = new OperationLog();
                    operationLog.OperationTime = DateTime.Now;
                    operationLog.Info = "修改角色权限:" + roleInfo.Name;
                    operationLog.Operator = operatorInfo;
                    db.OperationLogs.Add(operationLog);

                    role.Name = roleInfo.Name;
                    role.Registration= roleInfo.Registration;
                    role.Judgment= roleInfo.Judgment;
                    role.Logout= roleInfo.Logout;
                    role.Query= roleInfo.Query;
                    role.OperatorLog = roleInfo.OperatorLog;


                    db.SaveChanges();

                    return 0;
                }
                catch (Exception)
                {
                    return -4;
                }

            }
        }

        /// <summary>
        /// 查询所有角色信息
        /// </summary>
        /// <param name=""></param>
        /// <returns></returns>
        public int GetAllRoleInfo(out List<RoleInfo> roleInfos)
        {
            roleInfos = new List<RoleInfo>();
            using (TheNetherWorldContext db = new TheNetherWorldContext())
            {
                try
                {
                    var roles = db.Roles.ToList();
                    foreach (var role in roles)
                    {
                        RoleInfo roleInfo=new RoleInfo();
                        roleInfo.Name = role.Name;
                        roleInfo.Registration= role.Registration;
                        roleInfo.Judgment= role.Judgment;
                        roleInfo.Logout= role.Logout;
                        roleInfo.Query= role.Query;
                        roleInfo.OperatorLog= role.OperatorLog;

                        roleInfos.Add(roleInfo);
                    }
                    return 0;
                }
                catch (Exception)
                {

                    return -1;
                }
            }
        }


        /// <summary>
        /// 查询所有操作者信息
        /// </summary>
        /// <param name=""></param>
        /// <returns></returns>
        public int GetAllOperatorInfo( out List<OperatorInfo> operatorInfos)
        {
            operatorInfos = new List<OperatorInfo>();

            using (TheNetherWorldContext db = new TheNetherWorldContext())
            {
                try
                {
                    var operators = db.Operators.Include(o => o.Role).ToList();
                    foreach (var item in operators)
                    {
                        OperatorInfo operatorInfo = new OperatorInfo();
                        operatorInfo.Name= item.Name;
                        operatorInfo.Password= item.Password;
                        operatorInfo.RoleName = item.Role.Name;
                        operatorInfos.Add(operatorInfo);
                    }
                    return 0;
                }
                catch (Exception)
                {

                    return -1;
                }
            }
        }

        /// <summary>
        /// 登录
        /// </summary>
        /// <returns></returns>
        public int Login(string name,string password, out RoleInfo role)
        {
            role =new RoleInfo();
            using (TheNetherWorldContext db = new TheNetherWorldContext())
            {
                try
                {
                    var operatorinfo = db.Operators.Include(x=>x.Role).Where(x => x.Name == name && x.Password == password).FirstOrDefault();
                    if (operatorinfo == null)
                    {
                        return -2;
                    }

                    role.Name= operatorinfo.Role.Name;
                    role.Registration= operatorinfo.Role.Registration;
                    role.Judgment= operatorinfo.Role.Judgment;
                    role.Logout= operatorinfo.Role.Logout;
                    role.Query= operatorinfo.Role.Query;
                    role.OperatorLog= operatorinfo.Role.OperatorLog;

                    return 0;
                }
                catch (Exception)
                {

                    return -1;
                }
            }
        }
    }
}
