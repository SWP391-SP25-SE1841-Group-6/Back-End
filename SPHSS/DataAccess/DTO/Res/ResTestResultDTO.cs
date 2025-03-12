using DataAccess.DTO.Req;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.DTO.Res
{
    public class ResTestResultDTO
    {
        public int TestResultId { get; set; }
        public int StudentId { get; set; }
        public int TestId { get; set; }
        public DateTime TestDate { get; set; }
        public double Score { get; set; }
        public IEnumerable<ResTestResultDetailDTO> resTestResultDetailDTO { get; set; }
    }
}
