using CoreDBModel.Models.Enums;

namespace CoreDBModel.Models
{
    public class IncidentLog: BaseEntity
    {
        public int AdminProfileId { get; set; }

        public string Description { get; set; }

        public DateTime DecisionDate { get; set; }

        public bool IsRevisioned { get; set; } = false;

        public virtual AdminProfile AdminProfile { get; set; } = null!;

        public IncidentMeaning? Meaning {  get; set; }

        public int? ObjectId { get; set; }
    }
}
