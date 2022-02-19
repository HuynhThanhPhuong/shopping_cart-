using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyLibrary.Model
{
    [Table("Orderdetail")]
    public class Orderdetail
    {
        [Key]
        public int Id { get; set; }
        public int Orderid { get; set; }
        public int quantity { get; set; }
        public int Productid { get; set; }
        public float Price { get; set; }
        public float Amount { get; set; }
    }
}
