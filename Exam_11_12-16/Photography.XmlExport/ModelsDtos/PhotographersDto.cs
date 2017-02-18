namespace Photography.XmlExport.ModelsDtos
{
    using System.Collections.Generic;
    using System.Xml.Serialization;

    [XmlType("photographer")]
    public class PhotographersDto
    {
        [XmlAttribute("name")]
        public string Name { get; set; }

        [XmlAttribute("primary-camera")]
        public string PrimaryCamera { get; set; }

        [XmlArray("lenses")]
        [XmlArrayItem("len")]
        public List<LenDto> LenDtos { get; set; }
    }

    public class LenDto
    {
        [XmlAttribute("make")]
        public string Make { get; set; }

        [XmlAttribute("focal-length")]
        public string FocalLength { get; set; }

        [XmlAttribute("max-aperture")]
        public string MaxApeture { get; set; }
    }
}