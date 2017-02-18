namespace Photography.XmlImport.Dtos
{
    using System.Collections.Generic;
    using System.Xml.Serialization;

    public class AccessoriesDto
    {
        [XmlElement("accessory")]
        public List<AccessoryDto> AccessoryDtos { get; set; }
    }
    public class AccessoryDto
    {
        [XmlAttribute("name")]
        public string Name { get; set; }
    }
}