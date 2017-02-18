namespace Photography.Models
{
    using System.ComponentModel.DataAnnotations;

    public class Accessory
    {
        [Key]
        public int Id { get; set; }

        public string Name { get; set; }

        public int? OwnerId { get; set; }

        public virtual Photographer Owner { get; set; }
    }
}