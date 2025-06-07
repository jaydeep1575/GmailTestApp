namespace GmailTestApp.DTO
{
    public class BulkLabelDto
    {
        public int LabelId { get; set; }
        public List<int> GmailIds { get; set; }
    }
}
