using Microsoft.AspNetCore.Mvc;
using MVCENG2.Interfaces;
using MVCENG2.Models;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace MVCENG2.ComfortModules
{
    public class DateFunctions 
    {
        private readonly IStandRepository _standRepository;
        private readonly IStandsStatisticRepository _statisticRepository;
        public DateFunctions(IStandRepository standRepository, IStandsStatisticRepository statisticRepository, ITestReportRepository testReportRepository)
        {
            _standRepository = standRepository;
            _statisticRepository = statisticRepository;
        }

        public async Task<IEnumerable<Statistic>> GetValuesTimeInterval(Stand stand, DateTime datetime_before, DateTime datetime_after)
        {
            int count_value = 0;
            IEnumerable<Statistic> stands_statistic = await _statisticRepository.GetAllElementsThatStand(stand.Stand_name);
            IEnumerable<Statistic> stands_statistic_after_sorting = new List<Statistic>();
            foreach (Statistic stand_object in stands_statistic)
            {
                DateTime stands_test_date=DateTime.ParseExact(stand_object.TestEnd, "yyyy-MM-dd HH:mm:ss",
                                       System.Globalization.CultureInfo.InvariantCulture);
                if (stands_test_date>=datetime_before && stands_test_date<=datetime_after)
                {
                    stands_statistic_after_sorting.Append(stand_object);
                }
            }

            return stands_statistic_after_sorting;
        }
    }
}
