namespace DataSibTerminal.Models
{
    public class ErrorViewModel
    {
        public string? RequestId { get; set; }
        DateTime DateTime { get; set; }    
        public bool ShowRequestId => !string.IsNullOrEmpty(RequestId);
        
    }
    
}