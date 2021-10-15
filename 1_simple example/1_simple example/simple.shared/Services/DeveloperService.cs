using simple.shared.Entities;
using simple.shared.Interfaces;
using simple.shared.Models;
using simple.shared.Wrappers;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace simple.shared.Services
{
    public class DeveloperService : IDeveloperService
    {
        private readonly IDeveloperRepository _developerRepository;

        public DeveloperService(IDeveloperRepository developerRepository)
        {
            _developerRepository = developerRepository;
        }

        public async Task<List<DeveloperDto>> GetAllAsync()
        {
            var data = await _developerRepository.GetAllAsync();

            return data.ConvertAll(x => new DeveloperDto()
            {
                Id = x.Id,
                Nombre = x.FirstName,
                Apellido = x.LastName,
                Edad = x.Age,
                Sueldo = x.Salary,
                Experiencia = x.Experience
            });
        }

        public async Task<CommandResult> CreateAsync(DeveloperDto command)
        {
            try
            {
                var entity = new Developer(firstName: command.Nombre,
                                           lastName: command.Apellido,
                                           age: command.Edad,
                                           salary: command.Sueldo,
                                           experience: command.Experiencia);

                await _developerRepository.CreateAsync(entity);
                return CommandResult.Success("Se creó el regitro correctamente");
            }
            catch (Exception ex)
            {

                return CommandResult.Error("Ocurrió un error");
            }
        }
    }
}
