using simple.shared.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace simple.shared.Interfaces
{
    public interface IDeveloperRepository
    {
        Task<List<Developer>> GetAllAsync();
        Task CreateAsync(Developer entity);
    }
}
