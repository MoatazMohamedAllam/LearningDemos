using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace CustomLogger
{
    public class CustomLogger
    {
        private readonly string _loggerFileName;
        private CustomLogger(string loggerFileName)
        {
            _loggerFileName = loggerFileName;
        }
        private static string GetLogFolder()
        {
            return @"D:\Work\Uploader_Log\Logs";
        }

        public void LogInfo(string message, [System.Runtime.CompilerServices.CallerMemberName] string memberName = "",
                                                    [System.Runtime.CompilerServices.CallerFilePath] string sourceFilePath = "",
                                                    [System.Runtime.CompilerServices.CallerLineNumber] int sourceLineNumber = 0)
        {
            WriteToFile(LogLevel.Info, message, memberName, sourceFilePath, sourceLineNumber);
        }


        public void LogError(string message, [System.Runtime.CompilerServices.CallerMemberName] string memberName = "",
                                                    [System.Runtime.CompilerServices.CallerFilePath] string sourceFilePath = "",
                                                    [System.Runtime.CompilerServices.CallerLineNumber] int sourceLineNumber = 0)
        {
            WriteToFile(LogLevel.Error, message, memberName, sourceFilePath);
        }


        public void LogFatal(string message, [System.Runtime.CompilerServices.CallerMemberName] string memberName = "",
                                                   [System.Runtime.CompilerServices.CallerFilePath] string sourceFilePath = "",
                                                   [System.Runtime.CompilerServices.CallerLineNumber] int sourceLineNumber = 0)
        {
            WriteToFile(LogLevel.Fatal, message, memberName, sourceFilePath);
        }

        public void LogAction(string message, [System.Runtime.CompilerServices.CallerMemberName] string memberName = "",
                                                 [System.Runtime.CompilerServices.CallerFilePath] string sourceFilePath = "",
                                                 [System.Runtime.CompilerServices.CallerLineNumber] int sourceLineNumber = 0)
        {
            WriteToFile(LogLevel.Action, message, memberName, sourceFilePath);
        }

        public void LogWarning(string message, [System.Runtime.CompilerServices.CallerMemberName] string memberName = "",
                                                 [System.Runtime.CompilerServices.CallerFilePath] string sourceFilePath = "",
                                                 [System.Runtime.CompilerServices.CallerLineNumber] int sourceLineNumber = 0)
        {
            WriteToFile(LogLevel.Warning, message, memberName, sourceFilePath);
        }

        public  void WriteToFile(LogLevel logLevel ,string message, [System.Runtime.CompilerServices.CallerMemberName]string memberName = "",
                                                    [System.Runtime.CompilerServices.CallerFilePath] string sourceFilePath = "",
                                                    [System.Runtime.CompilerServices.CallerLineNumber] int sourceLineNumber=0)
        {
            try
            {
                string directoryPath = Path.Combine(GetLogFolder(), DateTime.Now.ToString("yyyy-MM-dd"), _loggerFileName);
                if (!Directory.Exists(directoryPath))
                {
                    Directory.CreateDirectory(directoryPath);
                }
                using (StreamWriter sw = new StreamWriter(Path.Combine(directoryPath, _loggerFileName + DateTime.Now.ToString("yyyy-MM-dd HH") +
                    (logLevel == LogLevel.Error ? "_Exception": string.Empty) + ".txt")))
                {
                    sw.WriteLine(DateTime.Now.ToString("HH:mm:ss.fff") + " || " + sourceFilePath + "." + memberName + ":" + sourceLineNumber + " || "+ message);
                }
            }
            catch (Exception)
            {
            }
            

        }


        static readonly Dictionary<string, CustomLogger> customLoggerDictionary = new Dictionary<string, CustomLogger>();
        public static CustomLogger CreateInstance(string loggerName)
        {
            if (string.IsNullOrWhiteSpace(loggerName))
            {
                loggerName = "General";
            }
            loggerName = loggerName.Trim();
            lock (customLoggerDictionary)
            {
                var kVP = customLoggerDictionary.FirstOrDefault(x => x.Key.ToLower() == loggerName.ToLower());
                if (string.IsNullOrWhiteSpace(kVP.Key))
                {
                    customLoggerDictionary.Add(loggerName, new CustomLogger(loggerName));
                }

                return customLoggerDictionary.FirstOrDefault(x=>x.Key.ToLower() == loggerName.ToLower()).Value;
            }

        }
    }
}
