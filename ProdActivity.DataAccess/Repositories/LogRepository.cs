using ProdActivity.Entities;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ProdActivity.DataAccess.Repositories
{
    public class LogRepository
    {
        private readonly AppDbContext _context;

        public LogRepository(AppDbContext context)
        {
            _context = context;
        }

        public void Add(Log log)
        {
            _context.Logs.Add(log);
            _context.SaveChanges();
        }

        public List<Log> GetAll()
        {
            return _context.Logs.ToList();
        }

        public List<Log> GetByDateRange(DateTime startDate, DateTime endDate)
        {
            return _context.Logs
                .Where(l => l.Date >= startDate && l.Date <= endDate)
                .ToList();
        }
    }
} 