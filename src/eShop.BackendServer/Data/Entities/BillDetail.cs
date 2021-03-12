using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;

namespace eShop.BackendServer.Data.Entities
{
    [Table("BillDetails")]
    public class BillDetail
    {
        public int ProductId { get; set; }
        public int BillId { get; set; }
        [Column(TypeName = "decimal(18,2)")]
        public decimal Price { get; set; }

        public int Quantity { get; set; }

        [ForeignKey("ProductId")]
        public virtual Product Product { set; get; }

        [ForeignKey("BillId")]
        public virtual Bill Bill { set; get; }
    }
}