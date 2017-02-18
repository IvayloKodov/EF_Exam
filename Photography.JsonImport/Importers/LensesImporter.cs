namespace Photography.JsonImport.Importers
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Text;
    using Data.Interfaces;
    using Interfaces;
    using JsonDtos;
    using Models;
    using Newtonsoft.Json;
    
    public class LensesImporter : IImport
    {
        public int Order => 1;

        public string Message => "Importing lenses..";

        public string FileName => "lenses";

        public Action<IUnitOfWork, string> ImportJson
        {
            get
            {
                return
                    (unitOfWork, json) =>
                    {
                        var messages = new StringBuilder();

                        var lensesDtos = JsonConvert.DeserializeObject<IEnumerable<LensDto>>(json);

                        foreach (var lenDto in lensesDtos)
                        {
                            var len = new Len()
                            {
                                Make = lenDto.Make,
                                FocalLength = lenDto.FocalLength,
                                MaxAperture = lenDto.MaxAperture,
                                CompatibleWith = lenDto.CompatibleWith
                            };

                            //Its not ok to save every time but just to pass the validations..
                            try
                            {
                                unitOfWork.LensRepo.Add(len);
                                unitOfWork.Save();
                                messages.AppendLine($"Successfully imported {len.Make} {len.FocalLength} {len.MaxAperture}");
                            }
                            catch (ValidationException)
                            {
                                messages.AppendLine($"Unsuccessfully imported {len.Make} {len.FocalLength} {len.MaxAperture}");
                            }

                        }

                        Console.WriteLine(messages.ToString());
                    };
            }
        }
    }
}