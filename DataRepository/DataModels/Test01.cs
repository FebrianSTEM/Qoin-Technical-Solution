using Dapper.Contrib.Extensions;
using System;
using System.Collections.Generic;

namespace Data.Models
{
    [Table("Test01")]
    public class Test01
    {
        [Key]
        public int Id { get; set; }
        public string Nama { get; set; }
        public int Status { get; set; }
        public DateTime? Created { get; set; }
        public DateTime? Updated { get; set; }
    }
}