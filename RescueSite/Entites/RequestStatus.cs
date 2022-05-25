using System.ComponentModel.DataAnnotations;

namespace RescueSite.Entites
{
    public class RequestStatus
    {
        [Key]
        public int Id { get; set; }
        public string Status { get; set; }
    }
}
