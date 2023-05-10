using Microsoft.EntityFrameworkCore;
using MVCENG2.Data;
using MVCENG2.Interfaces;
using MVCENG2.Models;

namespace MVCENG2.Repository
{
    public class StandsStatisticRepository : IStandsStatisticRepository
    {
        private readonly ApplicationDbContext _context;
        private readonly IStandRepository _standsRepository;

        public StandsStatisticRepository (ApplicationDbContext context, IStandRepository standRepository)
        {
            _context = context;
            _standsRepository = standRepository;
        }
        public Task<bool> Add(Statistic stat)
        {
            _context.AddAsync(stat);
            return Save();

        }

        public Task<bool> Delete(Statistic stat)
        {
            _context.Remove(stat);
            return Save();
        }

        public IEnumerable<Statistic> GetAllElementsThatStand(string standsIdentifier)
        {
            IEnumerable<Tand>
            if (_standsRepository.GetAll().Select(k => k.Project).ToList().Contains(standsIdentifier))
            {
                _standsRepository.GetByProjectNameAsync(standsIdentifier).Wait();
            }
            else if (_standsRepository.GetAll().Select(k=>k.Stand_type).ToList().Contains(standsIdentifier))
            {
                _standsRepository.GetByStandTypeAsync(standsIdentifier).Wait();
            }
            else if (_standsRepository.GetAll().Select(k => k.Stand_name).ToList().Contains(standsIdentifier))
            {
                _standsRepository.GetByStandNameAsync(standsIdentifier).Wait();
            }





            return await _context.Statistic.ToListAsync();
            
        }

        public async Task<Statistic> GetByStandNameAsync(string statistic)
        {
            return await _context.Statistic.FirstOrDefaultAsync(i => i.ProductionNumber == statistic); 
        }

        public async Task<bool> Save()
        {
            var saved = await _context.SaveChangesAsync();
            return saved > 0 ? true : false;
        }
        public List<string> GetAllVINs()
        {

            return (List<string>)_context.Statistic.ToList().Select(x => x.VIN);
        }
        public Task<bool> Update(Statistic stat)
        {
            _context.Update(stat);
            return Save();
        }
    }
}
