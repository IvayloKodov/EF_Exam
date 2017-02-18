namespace Photography.Models.Cameras
{
    using System.ComponentModel.DataAnnotations.Schema;

    [Table("MirrorlessCamers")]
    public class MirrorlessCamera : Camera
    {
        public string MaxVideoResolution { get; set; }

        public int? MaxFrameRate { get; set; }
    }
}