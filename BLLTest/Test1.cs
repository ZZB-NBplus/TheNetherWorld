using BLL;
using Common.Model;
using DAL;
using System.Data;

namespace BLLTest
{
    [TestClass]
    public sealed class Test1
    {
        [TestMethod]
        public void TestAddRegistrationInfo()
        {
            ITheNetherWorldService theNetherWorldService = new TheNetherWorldService();

            string uniqueCode = "0017";
            string operatorName = "小黑";

            theNetherWorldService.AddRegistrationInfo(uniqueCode,operatorName);
        }

        [TestMethod]
        public void TestGetRegistrationCount()
        {
            ITheNetherWorldService theNetherWorldService = new TheNetherWorldService();

            int count= theNetherWorldService.GetRegistrationCount();
        }

        [TestMethod]
        public void TestGetLogoutCount()
        {
            ITheNetherWorldService theNetherWorldService = new TheNetherWorldService();

            int count = theNetherWorldService.GetLogoutCount();
        }

        [TestMethod]
        public void TestAddLogoutInfo()
        {
            ITheNetherWorldService theNetherWorldService = new TheNetherWorldService();

            string uniqueCode = "0016";
            string operatorName = "小黑";
            int count = theNetherWorldService.AddLogoutInfo(uniqueCode, operatorName);
        }

        [TestMethod]
        public void TestAddJudgmentInfo()
        {
            ITheNetherWorldService theNetherWorldService = new TheNetherWorldService();

            
            string uniqueCode = "0017";
            string operatorName = "小黑";

            JudgmentInfoData judgmentInfoData = new JudgmentInfoData();
            judgmentInfoData.Name = "小六";
            judgmentInfoData.UniqueCode = "0017";

            int count = theNetherWorldService.AddJudgmentInfo(judgmentInfoData, operatorName);
        }

        [TestMethod]
        public void TestAddOperatorInfo()
        {
            ITheNetherWorldService theNetherWorldService = new TheNetherWorldService();

            OperatorInfo operatorInfo= new OperatorInfo();
            operatorInfo.Name ="小七";
            operatorInfo.Password="00123456";

            string operatorName = "小黑";

            int count = theNetherWorldService.AddOperatorInfo(operatorInfo, operatorName);
        }

        [TestMethod]
        public void TestDeleteOperatorInfo()
        {
            ITheNetherWorldService theNetherWorldService = new TheNetherWorldService();


            string targetName = "小七";

            string operatorName = "小黑";

            int count = theNetherWorldService.DeleteOperatorInfo(targetName, operatorName);
        }


        [TestMethod]
        public void TestGetJudgmentInfoCount()
        {
            ITheNetherWorldService theNetherWorldService = new TheNetherWorldService();


            int count = theNetherWorldService.GetJudgmentInfoCount();
        }

        [TestMethod]
        public void TestUpdateRoleInfo()
        {
            ITheNetherWorldService theNetherWorldService = new TheNetherWorldService();
            RoleInfo roleInfo=new RoleInfo();
            roleInfo.Name = "鬼差";
            roleInfo.Registration = 1;

            string operatorName = "包拯";

            int count = theNetherWorldService.UpdateRoleInfo(roleInfo, operatorName);
        }

        [TestMethod]
        public void TestUpdateOperatorInfo()
        {
            ITheNetherWorldService theNetherWorldService = new TheNetherWorldService();
            OperatorInfo operatorInfo = new OperatorInfo();
            operatorInfo.Name = "小马";
            operatorInfo.Password = "33333";

            string operatorName = "包拯";

            int count = theNetherWorldService.UpdateOperatorInfo(operatorInfo, operatorName);
        }

        [TestMethod]
        public void TestGetUserInfo()
        {
            ITheNetherWorldService theNetherWorldService = new TheNetherWorldService();
            string uniqueCode = "0016"; ;
            UserInfoData userInfoData=new UserInfoData();

            int count = theNetherWorldService.GetUserInfo(uniqueCode, out userInfoData);
        }
        [TestMethod]
        public void TestGetNotJudgmentInfo()
        {
            ITheNetherWorldService theNetherWorldService = new TheNetherWorldService();

            List<string> userInfoDatas;
            int count = theNetherWorldService.GetNotJudgmentInfo(out  userInfoDatas);
        }

        [TestMethod]
        public void TestGetAllUserInfo()
        {
            ITheNetherWorldService theNetherWorldService = new TheNetherWorldService();

            List<UserInfoData> userInfoDatas;
            int count = theNetherWorldService.GetAllUserInfo(out userInfoDatas);
        }

        [TestMethod]
        public void TestGetAllRoleInfo()
        {
            ITheNetherWorldService theNetherWorldService = new TheNetherWorldService();

            List<RoleInfo> roleInfos;
            int count = theNetherWorldService.GetAllRoleInfo(out roleInfos);
        }

        [TestMethod]
        public void TestGetAllOperatorInfo()
        {
            ITheNetherWorldService theNetherWorldService = new TheNetherWorldService();

            List<OperatorInfo> operatorInfos;
            int count = theNetherWorldService.GetAllOperatorInfo(out operatorInfos);
        }
    }
}
