using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyLibrary.Model
{
    [Table("Link")]
    public class Link
    {
        [Key]
        public int Id { get; set; }
        public String Slug { get; set; }
        public String TypeLink { get; set; }
        public String Tableld { get; set; }
        public int Status { get; set; }

    }
}
