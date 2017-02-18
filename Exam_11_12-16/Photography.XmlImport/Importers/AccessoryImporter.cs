namespace Photography.XmlImport.Importers
{
    using System;
    using System.IO;
    using System.Linq;
    using System.Xml.Serialization;
    using Data.Interfaces;
    using Dtos;
    using Interfaces;
    using Models;

    public class AccessoryImporter //: IImport
    {
        public string FilePath => "../../XmlFiles/accessories.xml";

        public int Order => 1;

        public string Message => "Importing Accessories";

        public Action<IUnitOfWork, StreamReader> Import
        {
            get
            {
                return
                    (unitOfWork, reader) =>
                    {
                        var root = new XmlRootAttribute("accessories");
                        var serializer = new XmlSerializer(typeof(AccessoriesDto), root);

                        var accessories = ((AccessoriesDto)serializer.Deserialize(reader))
                            .AccessoryDtos
                            .Select(ad => new Accessory()
                            {
                                Name = ad.Name
                            }).ToList();

                        var photographers = unitOfWork.PhotographersRepo.GetAll().ToList();
                        var rnd = new Random();

                        foreach (var accessory in accessories)
                        {
                            accessory.Owner = photographers[rnd.Next(0, photographers.Count)];
                        }

                        unitOfWork.AccessoriesRepo.AddRange(accessories);

                        unitOfWork.Save();
                    };
            }
        }
    }
}