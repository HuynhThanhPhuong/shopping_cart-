using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyLibrary.Model
{
    [Table("Slider")]
    public class Slider
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public String Name { get; set; }
        public String Link { get; set; }

        [Required]
        public String Position { get; set; }
        public String Img { get; set; }
        public int Orders { get; set; }
        public DateTime? Create_At { get; set; }
        public int? Create_By { get; set; }
        public DateTime? Update_At { get; set; }
        public int? Update_By { get; set; }
        public int Status { get; set; }

    }
}
