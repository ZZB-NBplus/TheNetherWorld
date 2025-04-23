using System;
using System.Collections.Generic;

namespace DAL;

public partial class LogoutTable
{
    public int Id { get; set; }

    public int UserInfoId { get; set; }

    public DateTime LogoutTime { get; set; }

    public virtual UserInfo UserInfo { get; set; } = null!;
}
