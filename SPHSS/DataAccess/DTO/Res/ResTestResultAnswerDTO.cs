using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.DTO.Res
{
    public class ResTestResultAnswerDTO
    {
        public int TestResultId { get; set; }
        public int QuestionId { get; set; }
        public int? Answer { get; set; }
    }
}
