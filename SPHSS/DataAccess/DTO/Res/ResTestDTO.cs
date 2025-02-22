using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.DTO.Res
{
    public class ResTestDTO
    {
        public int TestId { get; set; }
        public string TestName { get; set; }

        public DateTime? DateCreated { get; set; }

        public DateTime? DateUpdated { get; set; }

        public bool? IsDeleted { get; set; }
        public IEnumerable<ResQuestionDTO> ListQuestions { get; set; }
    }
}
