using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Singleton.Start
{
    internal class LogMessage
    {
        public string Message { get; set; }
        public LogType LogType { get; set; }
        public DateTime CreatedAt { get; set; }

        public override string ToString()
        {
            var timpestamp = CreatedAt.ToString("yyyy-MM-dd hh:mm");

            return $"{LogType.ToString().PadLeft(7, ' ')} [{timpestamp}] {Message}";
        }
    }

    public enum LogType
    {
        Info,
        Warning,
        Error
    }
}

