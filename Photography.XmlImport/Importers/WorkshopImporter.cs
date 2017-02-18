namespace Photography.XmlImport.Importers
{
    using System;
    using System.Data.Entity.Validation;
    using System.IO;
    using System.Linq;
    using System.Xml.Serialization;
    using Data.Interfaces;
    using Dtos;
    using Interfaces;
    using Models;

    public class WorkshopImporter : IImport
    {
        public string FilePath => "../../XmlFiles/workshops.xml";

        public int Order => 2;
        public string Message => "Importing workshops";

        public Action<IUnitOfWork, StreamReader> Import
        {
            get
            {
                return (unitOfWork, reader) =>
                {
                    var root = new XmlRootAttribute("workshops");
                    var serializer = new XmlSerializer(typeof(WorkshopsDto), root);

                    var workshopsDto = (WorkshopsDto)serializer.Deserialize(reader);

                    foreach (var workshopDto in workshopsDto.WorkshopDtos)
                    {
                        if (workshopDto.Name == null ||
                            workshopDto.Location == null ||
                            workshopDto.Price == 0 ||
                            workshopDto.Trainer == null)
                        {
                            Console.WriteLine($"Error. Invalid data provided");
                        }

                        var participants = unitOfWork.PhotographersRepo.GetAll().ToList();

                        var fullParticipantsName = participants.Select(p => p.FirstName + " " + p.LastName).ToArray();

                        if (!fullParticipantsName.Contains(workshopDto.Trainer))
                        {
                            Console.WriteLine($"There is no such trainer in database!");
                            continue;
                        }
                        var trainer =
                            participants.FirstOrDefault(p => p.FirstName + " " + p.LastName == workshopDto.Trainer);

                        var workshop = new Workshop()
                        {
                            Name = workshopDto.Name,
                            Location = workshopDto.Location,
                            EndDate = workshopDto.EndDateSpecified ? workshopDto.EndDate : (DateTime?)null,
                            StartDate = workshopDto.StartDateSpecified ? workshopDto.StartDate : (DateTime?)null,
                            PricePerParticipant = workshopDto.Price,
                            Trainer = trainer
                        };

                        foreach (var participantDto in workshopDto.ParticipantDtos)
                        {

                            var participantFullName = participantDto.FirstName + " " + participantDto.LastName;

                            if (!fullParticipantsName.Contains(participantFullName))
                            {
                                continue;
                            }
                            var participant = participants.FirstOrDefault(p => p.FirstName + " " + p.LastName == participantFullName);

                            workshop.Participants.Add(participant);
                        }

                        unitOfWork.WorkshopsRepo.Add(workshop);
                        
                        try
                        {
                            unitOfWork.Save();
                            Console.WriteLine($"Successfully imported {workshop.Name}");
                        }
                        catch (DbEntityValidationException)
                        {
                            Console.WriteLine($"Invalid data.");
                        }
                    }
                };
            }
        }
    }
}