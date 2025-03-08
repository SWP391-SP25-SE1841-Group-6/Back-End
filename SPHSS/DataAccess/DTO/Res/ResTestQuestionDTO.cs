using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.DTO.Res
{
    public class ResTestQuestionDTO
    {
        public int TestQuestionId { get; set; }
        public int QuestionId { get; set; }

        public int? QtypeId { get; set; }

        public string? Question1 { get; set; }

        public bool? IsDeleted { get; set; }

        public string? Qtype { get; set; }
    }
}
