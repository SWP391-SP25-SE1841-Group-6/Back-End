﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess
{
    public class ResFormat<T>
    {
        public T Data { get; set; }
        public bool Success { get; set; } = true;
        public string Message { get; set; } = null!;

    }
    public class PaginationModel<T>
    {
        public int Page { get; set; }
        public int TotalPage { get; set; }
        public int TotalRecords { get; set; }
        public List<T> ListData { get; set; }
    }
}
