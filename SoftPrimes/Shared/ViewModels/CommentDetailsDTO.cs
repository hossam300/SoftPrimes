namespace SoftPrimes.Shared.ViewModels
{
    public class CommentDetailsDTO
    {
        public int Id { get; set; }
        public string CommentByNameAr { get; set; }
        public string CommentByNameEn { get; set; }
        public string Text { get; set; }
        public byte[] ProfileImage{ get; set; }

    }
}