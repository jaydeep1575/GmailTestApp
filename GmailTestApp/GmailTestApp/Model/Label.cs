using System.ComponentModel.DataAnnotations;

namespace GmailTestApp.Model
{
    public class Label
    {
        public int Id { get; set; }
        [Required]
        public string LableName { get; set; }
        [Required]
        public string ColorCode { get; set; }
        public string? Description { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        public ICollection<GmailLabelMap>? GmailLabelMaps { get; set; }
    }
}
