using CoreConnection.Enums;
using PrimumPlatformModel.Models.Enums;

namespace PrimumCore.Models
{
    public class IncidentLog
    {
        public int LogId { get; set; }

        public int AdminProfileId { get; set; }

        public string Description { get; set; }

        public DateTime DecisionDate { get; set; }

        public bool IsRevisioned { get; set; } = false;

        public virtual AdminProfile AdminProfile { get; set; } = null!;

        public IncidentMeaningDto? Meaning {  get; set; }

        public int? ObjectId { get; set; }
    }
}
