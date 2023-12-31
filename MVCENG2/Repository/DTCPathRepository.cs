﻿using Microsoft.EntityFrameworkCore;
using HoffmanWebstatistic.Data;
using HoffmanWebstatistic.Models.Hoffman;


namespace HoffmanWebstatistic.Repository
{
    public class DTCPathRepository
    {
        private readonly ApplicationDbContext _context;
        public DTCPathRepository(ApplicationDbContext context)
        {
            _context = context;
        }
       

        public DtcsPath GetDtcsPathByStandID(int standId)
        {
            return _context.dtcs_paths.Where(k => k.StandId == standId).FirstOrDefault() ;
        }
        public List<DtcsPath> GetAll()
        {
            return _context.dtcs_paths.ToList();
        }
        public List<DtcsPath> GetAllWithInclude()
        {
            return _context.dtcs_paths.Include(k=>k.Stand).ToList();
        }
        public bool Add(DtcsPath addObject)
        {
            _context.Add(addObject);
            return Save();

        }
        public bool Save()
        {
            var saved = _context.SaveChanges();
            return saved > 0 ? true : false;
        }

    }
}
