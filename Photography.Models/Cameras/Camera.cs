namespace Photography.Models.Cameras
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public abstract class Camera
    {
        private ICollection<Photographer> photographersPrimaryCam;
        private ICollection<Photographer> photographersSecondaryCam;

        protected Camera()
        {
            this.photographersPrimaryCam = new HashSet<Photographer>();
            this.photographersSecondaryCam = new HashSet<Photographer>();
        }

        [Key]
        public int Id { get; set; }

        [Required]
        public string Make { get; set; }

        [Required]
        public string Model { get; set; }

        public bool IsFullFrame { get; set; }

        [Required]
        [Range(100, int.MaxValue)]
        public int MinIso { get; set; }
        
        public int? MaxIso { get; set; }

        public virtual ICollection<Photographer> PhotographersPrimaryCam
        {
            get { return this.photographersPrimaryCam; }
            set { this.photographersPrimaryCam = value; }
        }

        public virtual ICollection<Photographer> PhotographersSecondaryCam
        {
            get { return this.photographersSecondaryCam; }
            set { this.photographersSecondaryCam = value; }
        }
    }
}