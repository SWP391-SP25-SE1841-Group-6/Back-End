﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.DTO.Req
{
    public class TestResultAnswerCreateDTO
    {
        public int TestResultId { get; set; }
        public int QuestionId { get; set; }
        public int? Answer { get; set; } // Answer on a scale of 1 to 5
    }
}
