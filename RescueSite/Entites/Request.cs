using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RescueSite.Entites
{
    public class Request
    {
        [Key]

        public int Id { get; set; }
        public int StatusId { get; set; } = 1;

        [ForeignKey("StatusId")]
        public RequestStatus RequestStatus { get; set; }

        public int UserId { get; set; }

        [ForeignKey("UserId")]
        public User User { get; set; }

        [ForeignKey("WinchId")]
        public int WinchId { get; set; }
        public Winch Winch { get; set; }
        public string carNum { get; set; }
        public string CarDetails { get; set; }
        public string Location { get; set; }
        public double Price { get; set; }
        public DateTime Date { get; set; }= DateTime.Now;
    }
}
