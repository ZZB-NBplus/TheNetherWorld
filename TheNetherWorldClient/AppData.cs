using Common.Model;
using TheNetherWorldClient.ViewModel;

namespace TheNetherWorldClient
{
    public static class AppData
    {
        public static string Name { get; set; }

        public static RoleInfo Role { get; set; }

        public static JudgmentViewModel JudgmentViewModel { get; set; }=new JudgmentViewModel();
        public static LogoutViewModel LogoutViewModel { get; set; }=new LogoutViewModel();
        public static MainViewModel MainViewModel { get; set; }=new MainViewModel();
        public static OperatorLogViewModel OperatorLogViewModel { get; set; }=new OperatorLogViewModel();
        public static OperatorViewModel OperatorViewModel { get; set; }=new OperatorViewModel();
        public static RegistrationViewModel RegistrationViewModel { get; set; }=new RegistrationViewModel();
        public static RoleViewModel RoleViewModel { get; set; }=new RoleViewModel();
        public static UserInfoQueryViewModel UserInfoQueryViewModel { get; set; }=new UserInfoQueryViewModel();
    }
}
