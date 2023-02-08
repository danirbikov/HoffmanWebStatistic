using MVCENG2.Models;

namespace MVCENG2.Interfaces
{
    public interface IDateFunctions
    {
        public Task<IEnumerable<Statistic>> GetValuesTimeInterval(Stand stand, DateTime datetime_before, DateTime datetime_after);

    }
}
