using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace DataSibTerminal.Models
{
    public class Message
    {
     

        [Column("fk_ticket_id")]
        public int Fk_ticket_id { get; set; }

        
        [Column("user_role")]
        [StringLength(50)]
        public string User_role { get; set; }
        
        [Column("massage")]
        public string massage { get; set; }
        
        [Column("send_time")]

        public DateTime send_time { get; set; }
    }
}
