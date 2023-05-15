using Microsoft.EntityFrameworkCore;
using MVCENG2.Data;
using MVCENG2.Interfaces;
using MVCENG2.Models.Siemens;

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
            List<string> listStands = null;

            if (_standsRepository.GetAll().Select(k => k.Project).ToList().Contains(standsIdentifier))
            {
                listStands = _standsRepository.GetAll().Where(k => k.Project == standsIdentifier).Select(k=>k.StandName).ToList();
            }
            else if (_standsRepository.GetAll().Select(k=>k.StandType).ToList().Contains(standsIdentifier))
            {
                listStands = _standsRepository.GetAll().Where(k => k.StandType == standsIdentifier).Select(k => k.StandName).ToList();
            }
            else if (_standsRepository.GetAll().Select(k => k.StandName).ToList().Contains(standsIdentifier))
            {
                listStands = _standsRepository.GetAll().Where(k => k.StandName == standsIdentifier).Select(k => k.StandName).ToList();
            }

            return _context.Statistic.Where(k =>listStands.Contains(k.Client)) ;
            
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
