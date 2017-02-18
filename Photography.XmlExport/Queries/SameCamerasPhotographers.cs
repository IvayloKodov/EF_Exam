namespace Photography.XmlExport.Queries
{
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Xml.Serialization;
    using Data.Interfaces;
    using Interfaces;
    using ModelsDtos;

    public class SameCamerasPhotographers : IQuery
    {
        public int Order => 1;

        public string FileName => "same-cameras-photographers";

        public void ExecuteQuery(IUnitOfWork unitOfWork, TextWriter writer)
        {
            var photographers = unitOfWork.PhotographersRepo
                .GetAll()
                .Select(p => new PhotographersDto()
                {
                    Name = p.FirstName + p.LastName,
                    PrimaryCamera = p.PrimaryCamera.Make + " " + p.PrimaryCamera.Model,
                    LenDtos = p.Lens
                        .Select(l => new LenDto()
                        {
                            Make = l.Make,
                            FocalLength = l.FocalLength.ToString(),
                            MaxApeture = l.MaxAperture.ToString()
                        })
                        .ToList()
                }).ToList();


            var root = new XmlRootAttribute("photographers");
            var xmlns = new XmlSerializerNamespaces();
            xmlns.Add("", "");

            var serializer = new XmlSerializer(typeof(List<PhotographersDto>), root);
            serializer.Serialize(writer, photographers, xmlns);
        }
    }
}