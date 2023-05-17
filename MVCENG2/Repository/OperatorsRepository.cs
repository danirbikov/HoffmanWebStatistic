﻿using Microsoft.EntityFrameworkCore;
using MVCENG2.Data;
using MVCENG2.Interfaces;
using MVCENG2.Models.Hoffman;

namespace MVCENG2.Repository
{
    public class OperatorsRepository
    {
        private readonly ApplicationDbContext _context;
        public OperatorsRepository(ApplicationDbContext context)
        {
            _context = context;
        }
        public bool Add(Operator stand)
        {
            _context.Add(stand);
            return Save();

        }

        public bool Delete(Operator stand)
        {
            _context.Remove(stand);
            return Save();
        }


        public IEnumerable<Operator> GetAll()
        {
            return _context.operators.ToList();
            
        }
        public int GetOperatorIDbyName(string operatorName)
        {
            var operatorObject = _context.operators.Where(k => k.OLogin == operatorName).FirstOrDefault();
            if (operatorObject != null)
            {
                return operatorObject.Id;
            }
            else
            {
                return _context.operators.Where(k => k.OLogin == "UNKNOWN").FirstOrDefault().Id;
            }
        }
        public bool Save()
        {
            var saved = _context.SaveChanges();
            return saved > 0 ? true : false;
        }

        public bool Update(Operator stand)
        {
            _context.Update(stand);
            return Save();
        }
    }
}
