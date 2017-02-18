namespace Photography.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    public class Workshop
    {
        private ICollection<Photographer> participants;

        public Workshop()
        {
            this.participants = new HashSet<Photographer>();
        }

        [Key]
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        public DateTime? StartDate { get; set; }

        public DateTime? EndDate { get; set; }

        [Required]
        public string Location { get; set; }

        [Required]
        public decimal PricePerParticipant { get; set; }

        [Required]
        public int TrainerId { get; set; }

        [Required]
        [InverseProperty("TrainerWorkshops")]
        public virtual Photographer Trainer { get; set; }

        public virtual ICollection<Photographer> Participants
        {
            get { return this.participants; }
            set { this.participants = value; }
        }
    }
}