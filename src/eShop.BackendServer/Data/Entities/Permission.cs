using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace eShop.BackendServer.Data.Entities
{
    [Table("Permissions")]
    public class Permission
    {
        [MaxLength(50)]
        [Column(TypeName = "varchar(50)")]
        public string FunctionId { get; set; }

        [MaxLength(50)]
        [Column(TypeName = "varchar(50)")]
        public string RoleId { get; set; }

        [MaxLength(50)]
        [Column(TypeName = "varchar(50)")]
        public string CommandId { get; set; }

        [ForeignKey("FunctionId")]
        public virtual Function Function { set; get; }

        [ForeignKey("CommandId")]
        public virtual Command Command { set; get; }

        [ForeignKey("RoleId")]
        public virtual UserRole UserRole { set; get; }
    }
}