namespace Photography.XmlImport.Dtos
{
    using System;
    using System.Collections.Generic;
    using System.Xml.Serialization;
    using Models;

    public class WorkshopsDto
    {
        [XmlElement("workshop")]
        public List<WorkshopDto> WorkshopDtos { get; set; }
    }

    public class WorkshopDto
    {
        [XmlAttribute("name")]
        public string Name { get; set; }

        [XmlAttribute("start-date")]
        public DateTime StartDate { get; set; }

        [XmlIgnore]
        public bool StartDateSpecified { get; set; }

        [XmlAttribute("end-date")]
        public DateTime EndDate { get; set; }

        [XmlIgnore]
        public bool EndDateSpecified { get; set; }

        [XmlAttribute("location")]
        public string Location { get; set; }

        [XmlAttribute("price")]
        public decimal Price { get; set; }

        [XmlElement("trainer")]
        public string Trainer { get; set; }

        [XmlArray("participants")]
        [XmlArrayItem("participant")]
        public List<ParticipantDto> ParticipantDtos { get; set; }
    }

    public class ParticipantDto
    {
        [XmlAttribute("first-name")]
        public string FirstName { get; set; }

        [XmlAttribute("last-name")]
        public string LastName { get; set; }
    }
}