using System.ComponentModel.DataAnnotations;

namespace GmailTestApp.Model
{
    public class Gmail
    {
        public int Id { get; set; }
        [Required]
        public string Subject { get; set; }
        [Required]
        public string Sender { get; set; }
        public DateTime Date { get; set; } = DateTime.UtcNow;

        public ICollection<GmailLabelMap> GmailLabelMaps { get; set; }
    }
}
