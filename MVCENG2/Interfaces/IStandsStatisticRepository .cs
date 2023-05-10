using MVCENG2.Models;

namespace MVCENG2.Interfaces
{
    public interface IStandsStatisticRepository
    {
        Task<IEnumerable<Statistic>> GetAllElementsThatStand(string stands);
        Task<Statistic> GetByStandNameAsync(string statistic);
        Task<bool> Add(Statistic stand);
        Task<bool> Update(Statistic stand);
        Task<bool> Delete(Statistic stand);
        List<string> GetAllVINs();
        Task<bool> Save();



    }
}
