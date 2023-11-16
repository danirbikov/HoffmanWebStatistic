﻿using Microsoft.EntityFrameworkCore;
using HoffmanWebstatistic.Data;
using HoffmanWebstatistic.Models.Hoffman;

namespace HoffmanWebstatistic.Repository
{
    public class Mes2SupTelegramsRepository
    {
        private readonly ApplicationDbContext _context;
        public Mes2SupTelegramsRepository(ApplicationDbContext context)
        {
            _context = context;
        }
       
        public List<Mes2supTelegram> GetAll()
        {
            return _context.mes2sup_telegrams.ToList();
        }
        public int GetTelegramsCount()
        {
            return _context.mes2sup_telegrams.Count();
        }

        public Mes2supTelegram GetTelegramByTgName(string telegramFileName)
        {
            return _context.mes2sup_telegrams.Where(k => k.TgFilename == telegramFileName).FirstOrDefault() ;
        }
        public bool Any(string telegramFileName)
        {
            return _context.mes2sup_telegrams.Where(k=>k.TgFilename==telegramFileName).Any();
        }

        public bool Add(Mes2supTelegram mes2supTelegram)
        {
            _context.Add(mes2supTelegram);
            return Save();
        }

        public bool Save()
        {
            var saved = _context.SaveChanges();
            return saved > 0 ? true : false;
        }
    }
}
