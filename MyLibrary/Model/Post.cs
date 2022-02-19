using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyLibrary.Model
{
    [Table("Post")]
    public class Post
    {
        [Key]
        public int Id { get; set; }

        public int? Topicid { get; set; }

        public String Title { get; set; }

        public String Slug { get; set; }

        public String Detail { get; set; }

        public String Img { get; set; }

        public String Type { get; set; }

        [Required]
        [StringLength(155)]
        public String Metadesc { get; set; }

        [Required]
        [StringLength(155)]
        public String Metakey { get; set; }

        public DateTime? Create_At { get; set; }

        public int? Create_By { get; set; }

        public DateTime? Update_At { get; set; }

        public int? Update_By { get; set; }

        public int Status { get; set; }


    }
}
