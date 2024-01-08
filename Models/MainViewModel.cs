    namespace DataSibTerminal.Models
{
    public class MainViewModel
    {
        public List<Ticket> Tickets {get;set;}
        public List<Message> Messages {get;set;}
        public Ticket ticket { get; set; }
        
        public Message message { get; set; }
        
        public  int id { get; set; }
         
    }
}
