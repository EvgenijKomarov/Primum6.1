using System.ComponentModel.DataAnnotations.Schema;

namespace CoreDBModel.Models
{
    public class Promocode: BaseEntity
    {
        public int? StudentId {  get; set; }

        public string Code { get; set; } = null!;

        public int CoinsPrice { get; set; }

        public string Title { get; set; } = null!;

        public string Description { get; set; } = null!;

        public StudentProfile? Student {  get; set; }
    }
}
