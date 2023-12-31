﻿using HoffmanWebstatistic.Data;
using HoffmanWebstatistic.Models.Hoffman;

namespace HoffmanWebstatistic.Repository
{
    public class OperatorsRepository
    {
        private readonly ApplicationDbContext _context;
        public OperatorsRepository(ApplicationDbContext context)
        {
            _context = context;
        }
        public bool Add(Operator @operator)
        {
            _context.Add(@operator);
            return Save();

        }

        public bool Delete(Operator @operator)
        {
            _context.Remove(@operator);
            return Save();
        }


        public List<Operator> GetAll()
        {
            return _context.operators.Where(k=>k.InactiveMark=="FALSE").ToList();        
        }
        public List<Operator> GetAllWithInactive()
        {
            return _context.operators.ToList();
        }

        public bool UnactiveOperator(int operatorID)
        {
            Operator @operator = _context.operators.Where(k => k.Id==operatorID).FirstOrDefault();
            @operator.InactiveMark = "TRUE";

            return Save();
        }

        public bool InactiveOperator(int operatorID)
        {
            Operator @operator = _context.operators.Where(k => k.Id == operatorID).FirstOrDefault();
            @operator.InactiveMark = "FALSE";

            return Save();
        }

        public bool EditOperator(Operator operatorObject)
        {
            Operator @operator = _context.operators.Where(k => k.Id == operatorObject.Id).FirstOrDefault();

            @operator.OLogin=operatorObject.OLogin;
            @operator.OPassword= operatorObject.OPassword;
            @operator.ODescription= operatorObject.ODescription;

            return Save();

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

        public Operator GetOperatorByID(int operatorID)
        {
            var operatorObject = _context.operators.Where(k => k.Id == operatorID).FirstOrDefault();
            return operatorObject;
            
        }

        public bool OperatorAnyByLogin(string operatorLogin)
        {
            return _context.operators.Any(k => k.OLogin == operatorLogin);

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
