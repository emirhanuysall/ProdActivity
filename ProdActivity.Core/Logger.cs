using ProdActivity.Entities;
using ProdActivity.DataAccess;
using ProdActivity.DataAccess.Repositories;
using System;

namespace ProdActivity.Core
{
    public static class Logger
    {
        private static readonly LogRepository _logRepository;

        static Logger()
        {
            _logRepository = new LogRepository(new AppDbContext());
        }

        public static void Info(string message)
        {
            Log("Info", message);
        }

        public static void Error(string message)
        {
            Log("Error", message);
        }

        public static void Warning(string message)
        {
            Log("Warning", message);
        }

        private static void Log(string logType, string message)
        {
            try
            {
                var log = new Log
                {
                    Date = DateTime.Now,
                    LogType = logType,
                    Message = message
                };

                _logRepository.Add(log);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Loglama hatası: {ex.Message}");
            }
        }
    }
}
