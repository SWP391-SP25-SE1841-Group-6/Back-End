using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.DTO.Req
{
    public class TestResultDetailCreateDTO
    {
        public int TestResultId { get; set; }
        public int QtypeId { get; set; }
        public decimal ScoreType { get; set; } // The score for each question type
    }
}
