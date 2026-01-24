using PrimumPlatformModel.Models.Enums;

namespace PrimumCore.Models
{
    public class IncendentLog
    {
        public int LogId { get; set; }

        public int AdminProfileId { get; set; }

        public string Description { get; set; }

        public bool IsRevisioned { get; set; } = false;

        public virtual AdminProfile AdminProfile { get; set; } = null!;
    }
}
