using System;
using System.Collections.Generic;

namespace DAL;

public partial class JudgmentInfoTable
{
    public int Id { get; set; }

    public int UserInfoId { get; set; }

    public DateTime? JudgmentTime { get; set; }

    public string? Name { get; set; }

    public string? Sex { get; set; }

    public string? Lifespan { get; set; }

    public DateTime? BirthTime { get; set; }

    public DateTime? DeathTime { get; set; }

    public string? DeathCause { get; set; }

    public string? PreDeathBehaviorRecord { get; set; }

    public string? JudgmentInfo { get; set; }

    public virtual UserInfo UserInfo { get; set; } = null!;
}
