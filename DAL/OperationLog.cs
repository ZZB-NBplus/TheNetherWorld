using System;
using System.Collections.Generic;

namespace DAL;

public partial class OperationLog
{
    public int Id { get; set; }

    public int OperatorId { get; set; }

    public DateTime OperationTime { get; set; }

    public string Info { get; set; } = null!;

    public virtual Operator Operator { get; set; } = null!;
}
