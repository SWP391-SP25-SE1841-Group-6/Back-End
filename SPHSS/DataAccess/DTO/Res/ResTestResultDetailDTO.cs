using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.DTO.Res
{
    public class ResTestResultDetailDTO
    {
        public int TestResultDetailId { get; set; }
        public int TestResultId { get; set; }
        public int QtypeId { get; set; }
        public decimal ScoreType { get; set; }
    }
}
