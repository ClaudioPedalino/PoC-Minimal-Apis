using Microsoft.EntityFrameworkCore;
using simple.shared.Entities;
using simple.shared.Interfaces;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace simple.shared.Data.Repositories
{
    public class DeveloperRepository : IDeveloperRepository
    {
        private readonly DataContext _context;

        public DeveloperRepository(DataContext context)
        {
            _context = context;
        }

        public async Task<List<Developer>> GetAllAsync()
        {
            return await _context.Developers
                .Take(20)
                .ToListAsync();
        }

        public async Task CreateAsync(Developer entity)
        {
            await _context.AddAsync(entity);
            await _context.SaveChangesAsync();
        }
    }
}
