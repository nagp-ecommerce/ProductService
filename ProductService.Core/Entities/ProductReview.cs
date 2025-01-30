namespace ProductService.Core.Entities
{
    public class ProductReview
    {
     
        public int ProductReviewId { get; set; }
        public int Rating { get; set; }
        public int Comment { get; set; }
        public int UserId { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public List<string> AttachmentUrls { get; set; }
    }
}