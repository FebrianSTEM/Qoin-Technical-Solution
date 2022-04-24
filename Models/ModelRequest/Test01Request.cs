using System.ComponentModel.DataAnnotations;

namespace Models.ModelRequest
{
    public class Test01Request
    {
        [StringLength(100)]
        public string Nama { get; set; }

        [Range(0, 6)]
        public int Status { get; set; }
    }
}
