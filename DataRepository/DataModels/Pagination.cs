using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Data.Models
{
    public class Pagination<T>
    {
        public int CurrentPage { get; set; }
        public IEnumerable<T> Data { get; set; }
        public int TotalRecords { get; set; }
    }
}
