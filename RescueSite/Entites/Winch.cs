using System.ComponentModel.DataAnnotations;

namespace RescueSite.Entites
{
    public class Winch
    {
        [Key]
        public int Id { get; set; }
        public string Details { get; set; }
        public string WinchNumber { get; set; }
        public string Phone { get; set; }
        public bool Booke { get; set; }=false;
    }
}
