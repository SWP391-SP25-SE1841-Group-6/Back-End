using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.DTO.Req
{
    public class TestResultCreateDTO
    {
        public int StudentId { get; set; }
        public int TestId { get; set; }
        public IEnumerable<TestResultAnswerCreateDTO>  Answers { get; set; }
    }
}
