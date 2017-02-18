namespace Photography.Models
{
    using System.ComponentModel.DataAnnotations;

    public class Len
    {
        [Key]
        public int Id { get; set; }

        public string Make { get; set; }

        public int? FocalLength { get; set; }

        public decimal? MaxAperture { get; set; }

        public string CompatibleWith { get; set; }

        public int? OwnerId { get; set; }

        public virtual Photographer Owner { get; set; }
    }
}