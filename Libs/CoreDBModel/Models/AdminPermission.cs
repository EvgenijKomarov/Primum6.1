using CoreDBModel.Models.Enums;

namespace CoreDBModel.Models
{
    public class AdminPermission: BaseEntity
    {
        public int AdminProfileId { get; set; }

        public Permission Permission { get; set; }

        public int? PromoterAdminProfileId { get; set; }

        public DateTime PromotionDate { get; set; }

        public AdminProfile AdminProfile { get; set; } = null!;

        public AdminProfile? PromoterAdminProfile { get; set; }
    }
}
