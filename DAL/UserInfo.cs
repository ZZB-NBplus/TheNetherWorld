using System;
using System.Collections.Generic;

namespace DAL;

public partial class UserInfo
{
    public int Id { get; set; }

    public string UniqueCode { get; set; } = null!;

    public int? RegistrationState { get; set; }

    public int? JudgmentInfoState { get; set; }

    public int? LogoutState { get; set; }

    public virtual ICollection<JudgmentInfoTable> JudgmentInfoTables { get; set; } = new List<JudgmentInfoTable>();

    public virtual ICollection<LogoutTable> LogoutTables { get; set; } = new List<LogoutTable>();

    public virtual ICollection<RegistrationTable> RegistrationTables { get; set; } = new List<RegistrationTable>();
}
