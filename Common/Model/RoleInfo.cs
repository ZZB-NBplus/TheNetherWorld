using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Model
{
    public class RoleInfo
    {
        public string Name { get; set; } = null!;

        public int Registration { get; set; } = 0;

        public int Judgment { get; set; } = 0;

        public int Logout { get; set; } = 0;

        public int Query { get; set; } = 0;

        public int OperatorLog { get; set; } = 0;


    }
}
