using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyLibrary.Model
{

    [Table("Products")]
    public class Products
    {
        [Key]
        public int Id { get; set; }

        public int Catid { get; set; }

        [Required]
        public String Name { get; set; }

        public String Slug { get; set; }


        [Required]
        public String Detail { get; set; }

        public double PriceBuy { get; set; }

        [Required]
        [StringLength(255)]
        public String Img { get; set; }

        [Required]
        [StringLength(155)]
        public String Metakey { get; set; }


        [Required]
        [StringLength(155)]
        public String Metadesc { get; set; }

        public int? Create_By { get; set; }

        public DateTime? Create_At { get; set; }

        public int? Update_By { get; set; }

        public DateTime? Update_At { get; set; }

        public int Status { get; set; }


    }
}
