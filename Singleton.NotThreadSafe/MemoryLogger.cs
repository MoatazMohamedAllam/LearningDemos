using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Singleton.Start
{
    internal class MemoryLogger
    {
        private int _infoCount;
        private int _errorCount;
        private int _warningCount;
        private List<LogMessage> _logs = new List<LogMessage>();
        private static MemoryLogger _instance;
        public IReadOnlyCollection<LogMessage> Logs => _logs;

        private MemoryLogger()
        {
        }

        private void Log(string message, LogType logType)
        {
            _logs.Add(new LogMessage
            {
                Message = message,
                LogType = logType,
                CreatedAt = DateTime.Now,
            });
        }

        public static MemoryLogger GetInstance()
        {
            if(_instance == null)
            {
                _instance= new MemoryLogger();

            }
            return _instance;
        }

        public void LogInfo(string message)
        {
            _infoCount++;
            Log(message, LogType.Info);
        }

        public void LogWarning(string message)
        {
            _warningCount++;
            Log(message, LogType.Warning);
        }

        public void LogError(string message)
        {
            _errorCount++;
            Log(message, LogType.Error);
        }

        public void ShowLog()
        {
            _logs.ForEach(x => Console.WriteLine(x));
            Console.WriteLine($"-------------------------------");

            Console.WriteLine($"Info ({_infoCount}), Warning ({_warningCount}), Error ({_errorCount})");
        }
    }
}
