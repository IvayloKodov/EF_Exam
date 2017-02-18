namespace Photography.JsonImport.Importers
{
    using System;
    using System.Collections.Generic;
    using System.Data.Entity.Validation;
    using System.Linq;
    using System.Text;
    using Data.Interfaces;
    using Interfaces;
    using JsonDtos;
    using Models;
    using Newtonsoft.Json;

    public class PhotographersImporter : IImport
    {
        public int Order => 3;

        public string Message => "Importing photographers";

        public string FileName => "photographers";

        public Action<IUnitOfWork, string> ImportJson
        {
            get
            {
                return
                    (unitOfWork, json) =>
                    {
                        var messages = new StringBuilder();

                        var photographerDtos = JsonConvert.DeserializeObject<IEnumerable<PhotographerDto>>(json);

                        var cameras = unitOfWork.CamerasRepo.GetAll().ToList();
                        Random rnd = new Random();

                        foreach (var photographerDto in photographerDtos)
                        {
                            if (photographerDto.FirstName == null ||
                                photographerDto.LastName == null)
                            {
                                messages.AppendLine($"Error. Invalid data provided");
                                continue;
                            }

                            var photographer = new Photographer()
                            {
                                FirstName = photographerDto.FirstName,
                                LastName = photographerDto.LastName,
                                Phone = photographerDto.Phone,
                                PrimaryCamera = cameras[rnd.Next(0, cameras.Count)],
                                SecondaryCamera = cameras[rnd.Next(0, cameras.Count)],
                            };

                            var lensIds = unitOfWork.LensRepo.GetAll().Select(l => l.Id).ToList();

                            int lenseCounter = 0;
                            foreach (var lenseDtoId in photographerDto.Lenses)
                            {
                                if (lensIds.Contains(lenseDtoId))
                                {
                                    var lense = unitOfWork.LensRepo.FindById(lenseDtoId);

                                    var firstCameraMake = photographer.PrimaryCamera.Make.ToLower();
                                    var secondCameraMake = photographer.SecondaryCamera.Make.ToLower();

                                    if (lense.CompatibleWith.ToLower() == firstCameraMake ||
                                        lense.CompatibleWith.ToLower() == secondCameraMake)
                                    {
                                        photographer.Lens.Add(lense);
                                        lenseCounter++;
                                    }
                                }
                            }

                            unitOfWork.PhotographersRepo.Add(photographer);

                            try
                            {
                                unitOfWork.Save();
                                messages.AppendLine($"Successfully imported {photographerDto.FirstName + " " + photographerDto.LastName} | Lenses: {lenseCounter}");
                            }
                            catch (DbEntityValidationException ex)
                            {
                                messages.AppendLine("Error. Invalid data provided");
                            }
                        }

                        Console.WriteLine(messages.ToString());
                    };
            }
        }
    }
}