﻿using HoffmanWebstatistic.Data;
using System.IO;
using HoffmanWebstatistic.Models.Hoffman;

namespace HoffmanWebstatistic.Repository
{
    public class StandRepository
    {
        private readonly ApplicationDbContext _context;
        public StandRepository (ApplicationDbContext context)
        {
            _context = context;
        }
        public bool Add(Stand stand)
        {
            _context.Add(stand);
            return Save();

        }

        public List<Stand> GetAll()
        {
            return _context.stands.Where(k=>k.InactiveMark=="FALSE").ToList();
            
        }

        public List<Stand> GetAllWithInactive()
        {
            return _context.stands.ToList();

        }

        public bool UnactiveStand(int standID)
        {
            Stand stand = _context.stands.Where(k => k.Id == standID).FirstOrDefault();
            stand.InactiveMark = "TRUE";

            return Save();

        }

        public bool InactiveStand(int standID)
        {
            Stand stand = _context.stands.Where(k => k.Id == standID).FirstOrDefault();
            stand.InactiveMark = "FALSE";

            return Save();

        }

        public IEnumerable<string> GetStandsTypeName()
        {
            return _context.stands.Select(k=>k.StandType).Where(k=>k!="QNX" && k!="UNKNOWN").Distinct().ToList();

        }
        public Stand GetStandByID(int standID)
        {
            return _context.stands.Where(k => k.Id == standID).FirstOrDefault();

        }

        public bool EditStand(Stand standObject)
        {
            Stand stand = _context.stands.Where(k => k.Id == standObject.Id).FirstOrDefault();

            
            stand.StandName = standObject.StandName;
            stand.StandNameDescription = standObject.StandNameDescription;
            stand.WorkplaceMes = standObject.WorkplaceMes;
            stand.IpAdress = standObject.IpAdress;
            stand.DnsName = standObject.DnsName;
            stand.Placement = standObject.Placement;
            stand.StandType = standObject.StandType;
            stand.Project = standObject.Project;

            return Save();

        }
        /*
        public Stand GetByStandNameAsync(string standName)
        {
            return _context.Stand.Where(g => g.Stand_name == standName);
        }
        public async Task<Stand> GetByStandTypeAsync(string standType)
        {
            return await _context.Stand.FirstOrDefaultAsync(i => i.Stand_type == standType);
        }
        public async Task<Stand> GetByProjectNameAsync(string projectName)
        {
            return await _context.Stand.FirstOrDefaultAsync(i => i.Project == projectName);
        }
        */
        public Stand GetStandbyName(string standName)
        {
            var standObject = _context.stands.Where(k => k.StandName == standName).FirstOrDefault();

            return standObject;
              
        }

        public bool Save()
        {
            var saved = _context.SaveChanges();
            return saved > 0 ? true : false;
        }

        public bool Update(Stand stand)
        {
            _context.Update(stand);
            return Save();
        }
    }
}
