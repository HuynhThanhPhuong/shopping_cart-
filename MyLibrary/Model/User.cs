using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyLibrary.Model
{
    [Table("User")]
    public class User
    {
        [Key]
        public int Id { get; set; }

        public String Fullname { get; set; }

        public String Username { get; set; }

        public String Password { get; set; }

        public String Email { get; set; }

        public String Phone { get; set; }

        public String Address { get; set; }

        public String Img { get; set; }

        public int Gender { get; set; }

        public int Roles { get; set; }
        public DateTime? Create_At { get; set; }
        public int? Create_By { get; set; }
        public DateTime? Update_At { get; set; }
        public int? Update_By { get; set; }
        public int Status { get; set; }


    }
}
