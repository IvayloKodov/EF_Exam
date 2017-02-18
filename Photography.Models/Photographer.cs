namespace Photography.Models
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using Attributes;
    using Cameras;

    public class Photographer
    {
        private ICollection<Len> lens;
        private ICollection<Accessory> accessories;
        private ICollection<Workshop> workshops;
        private ICollection<Workshop> trainerWorkshops;

        public Photographer()
        {
            this.lens = new HashSet<Len>();
            this.accessories = new HashSet<Accessory>();
            this.workshops = new HashSet<Workshop>();
            this.trainerWorkshops = new HashSet<Workshop>();
        }

        [Key]
        public int Id { get; set; }

        [Required]
        public string FirstName { get; set; }

        [Required]
        [MinLength(2)]
        [MaxLength(50)]
        public string LastName { get; set; }

        [PhoneValidator]
        public string Phone { get; set; }

        [ForeignKey("PrimaryCamera"), Column(Order = 0)]
        public int PrimaryCameraId { get; set; }

        [Required]
        [InverseProperty("PhotographersPrimaryCam")]
        public virtual Camera PrimaryCamera { get; set; }

        [ForeignKey("SecondaryCamera"), Column(Order = 1)]
        public int SecondaryCameraId { get; set; }

        [Required]
        [InverseProperty("PhotographersSecondaryCam")]
        public virtual Camera SecondaryCamera { get; set; }

        public virtual ICollection<Len> Lens
        {
            get { return this.lens; }
            set { this.lens = value; }
        }

        public virtual ICollection<Accessory> Accessories
        {
            get { return this.accessories; }
            set { this.accessories = value; }
        }

        public virtual ICollection<Workshop> Workshops
        {
            get { return this.workshops; }
            set { this.workshops = value; }
        }

        public virtual ICollection<Workshop> TrainerWorkshops
        {
            get { return this.trainerWorkshops; }
            set { this.trainerWorkshops = value; }
        }
    }
}