using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyLibrary.Model
{
    [Table("Contact")]
    public class Contact
    {
        [Key]
        public int Id { get; set; }
        public String Fullname { get; set; }
        public String Email { get; set; }
        public String Phone { get; set; }
        public String Title { get; set; }
        public String Detail { get; set; }
        public String Replaydetail { get; set; }
        public String Slug { get; set; }
        public String metadesc { get; set; }
        public DateTime? Create_At { get; set; }
        public int? Create_By { get; set; }
        public DateTime? Update_At { get; set; }
        public int? Update_By { get; set; }
        public int Status { get; set; }

    }
}
