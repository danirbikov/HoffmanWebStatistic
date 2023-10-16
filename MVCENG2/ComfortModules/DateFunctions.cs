﻿using Microsoft.AspNetCore.Mvc;
using HoffmanWebstatistic.Interfaces;
using HoffmanWebstatistic.Models;
using HoffmanWebstatistic.Repository;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;
using HoffmanWebstatistic.Models.Hoffman;

namespace HoffmanWebstatistic
{
    public static class DateFunctions 
    {

        public static DateTime GetLastTestDateForStand(Stand stand, JsonHeadersRepository _testJsonHeaderRepository)
        {
            var lastTestDatesElements = _testJsonHeaderRepository.GetAllElementsForRead().Where(k => k.StandId == stand.Id).OrderByDescending(k => k.Created);
            
            if (lastTestDatesElements.Any())
            {
                return lastTestDatesElements.FirstOrDefault().Created;               
            }
            else
            {
                return DateTime.MinValue;
            }
        }

        public static int GetAllTestsCountForStand(Stand stand, JsonHeadersRepository _testJsonHeaderRepository)
        {
            return _testJsonHeaderRepository.GetAllElementsForRead().Where(k => k.StandId == stand.Id).Count();
        }

        public static int GetCarsCountLastmonth(Stand stand, JsonHeadersRepository _testJsonHeaderRepository)
        {
            DateTime lastMonth = DateTime.Now.AddMonths(-1);
            return  _testJsonHeaderRepository.GetAllElementsForRead()
                .Where(k => k.StandId == stand.Id)
                .Where(x => x.Created >= lastMonth)
                .GroupBy(t => t.VIN)
                .Select(t => t.FirstOrDefault()).AsEnumerable()               
                .Count();
                                      
        }
    }
}
