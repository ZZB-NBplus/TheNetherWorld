using System;
using System.Collections.Generic;

namespace DAL;

public partial class RegistrationTable
{
    public int Id { get; set; }

    public int UserInfoId { get; set; }

    public DateTime RegistrationTime { get; set; }

    public virtual UserInfo UserInfo { get; set; } = null!;
}
