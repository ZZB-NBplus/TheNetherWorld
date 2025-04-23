using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Model
{
    public class JudgmentInfoData
    {
        public string UniqueCode { get; set; } = null!;
        public DateTime? JudgmentTime { get; set; }

        public string? Name { get; set; }

        public string? Sex { get; set; }

        public string? Lifespan { get; set; }

        public DateTime? BirthTime { get; set; }

        public DateTime? DeathTime { get; set; }

        public string? DeathCause { get; set; }

        public string? PreDeathBehaviorRecord { get; set; }

        public string? JudgmentInfo { get; set; }
    }
}
