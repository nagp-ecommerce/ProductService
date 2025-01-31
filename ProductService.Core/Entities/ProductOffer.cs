using System.ComponentModel.DataAnnotations;

namespace ProductService.Core.Entities
{
    public class ProductOffer
    {
        [Key]
        public Guid OfferId { get; set; }
        public string CouponCode { get; set; }
        public string Description { get; set; }
        public string OfferType { get; set; }
        public string ApplicableOn { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }

    }
}