using System;
using System.Collections.Generic;

namespace DAL;

public partial class Role
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public int Registration { get; set; }

    public int Judgment { get; set; }

    public int Logout { get; set; }

    public int Query { get; set; }

    public int OperatorLog { get; set; }

    public virtual ICollection<Operator> Operators { get; set; } = new List<Operator>();
}
