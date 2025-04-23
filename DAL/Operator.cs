using System;
using System.Collections.Generic;

namespace DAL;

public partial class Operator
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public string Password { get; set; } = null!;

    public int RoleId { get; set; }

    public virtual ICollection<OperationLog> OperationLogs { get; set; } = new List<OperationLog>();

    public virtual Role Role { get; set; } = null!;
}
