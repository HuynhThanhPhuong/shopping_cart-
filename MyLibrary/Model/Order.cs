using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyLibrary.Model
{
    [Table("Order")]
    public class Order
    {
        [Key]
        public int Id { get; set; }
        public int Userid { get; set; }
        public String Code { get; set; }
        public String Deliveryaddress { get; set; }
        public String Deliveryname { get; set; }
        public String Deliveryemail { get; set; }
        public String Deliveryphone { get; set; }
        public int Createdate { get; set; }
        public int Exportdate { get; set; }
        public String metadesc { get; set; }
        public int? Create_At { get; set; }
        public int? Create_By { get; set; }
        public int? Update_At { get; set; }
        public int? Update_By { get; set; }
        public int Status { get; set; }


    }
}
