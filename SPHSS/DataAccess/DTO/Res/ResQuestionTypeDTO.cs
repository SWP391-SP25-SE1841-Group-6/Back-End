using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.DTO.Res
{
    public class ResQuestionTypeDTO
    {
        public int QtypeId { get; set; }

        public string? Qtype { get; set; }

        public bool? IsDeleted { get; set; }
    }
}
