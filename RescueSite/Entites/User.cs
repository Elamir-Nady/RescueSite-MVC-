using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RescueSite.Entites
{
    public class User
    {
        [Key]
        public int Id{ get; set; }
        public string FullName{ get; set; }
        public string Email{ get; set; }
        public string Password{ get; set; }
        public string Phone{ get; set; }
        [ForeignKey("RoleId")]
        public int RoleId { get; set; }

        public Role Role { get; set; }
    }
}
