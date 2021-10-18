using simple.shared.Models;
using simple.shared.Wrappers;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace simple.shared.Interfaces
{
    public interface IDeveloperService
    {
        Task<List<DeveloperDto>> GetAllAsync();
        Task<CommandResult> CreateAsync(DeveloperDto request);
    }
}
