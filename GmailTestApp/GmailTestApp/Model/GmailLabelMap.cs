namespace GmailTestApp.Model
{
    public class GmailLabelMap
    {
        public int GmailId { get; set; }
        public Gmail Gmail { get; set; }

        public int LabelId { get; set; }
        public Label Label { get; set; }
    }
}
