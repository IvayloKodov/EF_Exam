namespace Photography.JsonImport.Importers
{
    using System;
    using System.Collections.Generic;
    using System.Text;
    using Data.Interfaces;
    using Interfaces;
    using JsonDtos;
    using Models.Cameras;
    using Newtonsoft.Json;
    
    public class CamerasImporter : IImport
    {
        public int Order => 2;

        public string Message => "Importing cameras";

        public string FileName => "cameras";

        public Action<IUnitOfWork, string> ImportJson
        {
            get
            {
                return
                    (unitOfWork, json) =>
                    {
                        var messages = new StringBuilder();
                        var cameraDtos = JsonConvert.DeserializeObject<IEnumerable<CameraDto>>(json);

                        foreach (var cameraDto in cameraDtos)
                        {
                            if (string.IsNullOrEmpty(cameraDto.Type) ||
                                string.IsNullOrEmpty(cameraDto.Make) ||
                                string.IsNullOrEmpty(cameraDto.Model) ||
                                cameraDto.MinIso == 0)
                            {
                                messages.AppendLine($"Error. Invalid data provided");
                                continue;
                            }

                            Camera camera = default(Camera);

                            switch (cameraDto.Type.ToLower())
                            {
                                case "dslr":
                                    camera = new DslrCamera()
                                    {
                                        Make = cameraDto.Make,
                                        Model = cameraDto.Model,
                                        IsFullFrame = cameraDto.IsFullFrame,
                                        MinIso = cameraDto.MinIso,
                                        MaxIso = cameraDto.MaxIso,
                                        MaxShutterSpeed = cameraDto.MaxShutterSpeed
                                    };
                                    break;
                                case "mirrorless":
                                    camera = new MirrorlessCamera()
                                    {
                                        Make = cameraDto.Make,
                                        Model = cameraDto.Model,
                                        MinIso = cameraDto.MinIso,
                                        MaxIso = cameraDto.MaxIso,
                                        IsFullFrame = cameraDto.IsFullFrame,
                                        MaxFrameRate = cameraDto.MaxFrameRate,
                                        MaxVideoResolution = cameraDto.MaxVideoResolution
                                    };
                                    break;
                            }

                            unitOfWork.CamerasRepo.Add(camera);
                            
                            messages.AppendLine($"Successfully imported {cameraDto.Type} {cameraDto.Make} {cameraDto.Model}");
                        }

                        unitOfWork.Save();
                        Console.WriteLine(messages.ToString());
                    };
            }
        }
    }
}