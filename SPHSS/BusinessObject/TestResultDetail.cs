using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessObject
{
    public class TestResultDetail
    {
        public int TestResultDetailId { get; set; }
        public string? Qtype { get; set; }
        public int TestResultId { get; set; }
        public double? ScoreType { get; set; }
        public bool? IsDeleted { get; set; }
        public virtual TestResult? TestResult { get; set; }
    }
}
