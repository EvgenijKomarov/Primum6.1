using System.ComponentModel.DataAnnotations.Schema;

namespace PrimumCore.Models
{
    public class Promocode
    {
        public int PromocodeId { get; set; }

        public int? StudentId {  get; set; }

        public string Code { get; set; } = null!;

        public int CoinsPrice { get; set; }

        public string Title { get; set; } = null!;

        public string Description { get; set; } = null!;

        public StudentProfile? Student {  get; set; }
    }
}
