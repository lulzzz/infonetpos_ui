using MetroLog;
using MetroLog.Layouts;
using MetroLog.Targets;
using System;
using System.Globalization;
using System.Text;
using System.Threading.Tasks;

namespace Infonet.CStoreCommander.Logging
{

    public static class InfonetLogManager
    {
        private static StreamingFileTarget _fileLogTarget;
        static InfonetLogManager()
        {
            //setup log configuration
            _fileLogTarget = new StreamingFileTarget(new LogLayout());

            //keep X days of logs
            _fileLogTarget.RetainDays = 7;
#if DEBUG
            LogManagerFactory.DefaultConfiguration.AddTarget(LogLevel.Trace, LogLevel.Fatal, _fileLogTarget);
            LogManagerFactory.DefaultConfiguration.AddTarget(LogLevel.Fatal, LogLevel.Fatal, new SnapshotFileTarget(new LogLayout()));
#else
            LogManagerFactory.DefaultConfiguration.AddTarget(LogLevel.Info, LogLevel.Fatal, _fileLogTarget);
            LogManagerFactory.DefaultConfiguration.AddTarget(LogLevel.Fatal, LogLevel.Fatal, new SnapshotFileTarget(new LogLayout()));
#endif
        }

        public static InfonetLog GetLogger<T>()
        {
            return new InfonetLog(typeof(T), LogManagerFactory.DefaultLogManager.GetLogger(typeof(T)));
        }

        public static InfonetLog GetLogger(Type type)
        {
            return new InfonetLog(type, LogManagerFactory.DefaultLogManager.GetLogger(type));
        }

        public static async Task FlushAsync()
        {
            if (_fileLogTarget != null)
            {
                await _fileLogTarget.CloseAllOpenFiles();
            }
        }

        private class LogLayout : Layout
        {
            public override string GetFormattedString(LogWriteContext context, LogEventInfo info)
            {
                var builder = new StringBuilder();
                builder.Append("[");
                builder.Append(info.TimeStamp.LocalDateTime.ToString("yyyy-MM-dd hh:mm:ss.fff ttt", CultureInfo.InvariantCulture));
                builder.Append("] [");
                builder.Append(Environment.CurrentManagedThreadId);
                builder.Append("] ");
                builder.Append(info.Level.ToString().ToUpper());
                builder.Append(": ");
                builder.Append(info.Logger);
                if (!info.Message.StartsWith("."))
                    builder.Append(" ");

                builder.Append(info.Message);
                if (info.Exception != null)
                {
                    builder.Append(" --> ");
                    builder.Append(info.Exception);

                    if (info.Exception.InnerException != null)
                    {
                        builder.Append(" inner --> ");
                        builder.Append(info.Exception.InnerException);
                    }
                }

                return builder.ToString();
            }
        }
    }



    public class InfonetLog
    {
        private readonly Type _type;
        private WeakReference<ILogger> _base;


        public InfonetLog(Type type, ILogger log)
        {
            _type = type;
            _base = new WeakReference<ILogger>(log);
        }

        private ILogger GetBase()
        {
            ILogger log;
            _base.TryGetTarget(out log);

            if (log == null)
            {
                log = LogManagerFactory.DefaultLogManager.GetLogger(_type);
                _base = new WeakReference<ILogger>(log);
            }

            return log;
        }

        public string Name => GetBase().Name;

        public bool IsTraceEnabled => GetBase().IsTraceEnabled;

        public bool IsDebugEnabled => GetBase().IsDebugEnabled;

        public bool IsInfoEnabled => GetBase().IsInfoEnabled;

        public bool IsWarnEnabled => GetBase().IsWarnEnabled;

        public bool IsErrorEnabled => GetBase().IsErrorEnabled;

        public bool IsFatalEnabled => GetBase().IsFatalEnabled;

        public bool IsEnabled(LogLevel level)
        {
            return GetBase().IsEnabled(level);
        }


        private string FormatMessage(string message, string memberName, int sourceLineNumber)
        {
            if (!string.IsNullOrEmpty(memberName) && sourceLineNumber != 0)
                return string.Format(".{0} line:{1} {2}", memberName, sourceLineNumber, message);
            else
                return message;
        }

        public void Trace(string message,
            Exception ex = null,
            [System.Runtime.CompilerServices.CallerMemberName] string memberName = "",
            [System.Runtime.CompilerServices.CallerLineNumber] int sourceLineNumber = 0)
        {
            GetBase().Trace(FormatMessage(message, memberName, sourceLineNumber), ex);
        }



        public void Debug(string message, Exception ex = null,
            [System.Runtime.CompilerServices.CallerMemberName] string memberName = "",
            [System.Runtime.CompilerServices.CallerLineNumber] int sourceLineNumber = 0)
        {
            GetBase().Debug(FormatMessage(message, memberName, sourceLineNumber), ex);
        }



        public void Info(string message, Exception ex = null,
            [System.Runtime.CompilerServices.CallerMemberName] string memberName = "",
            [System.Runtime.CompilerServices.CallerLineNumber] int sourceLineNumber = 0)
        {
            GetBase().Info(FormatMessage(message, memberName, sourceLineNumber), ex);
        }



        public void Warn(Exception ex,
            [System.Runtime.CompilerServices.CallerMemberName] string memberName = "",
            [System.Runtime.CompilerServices.CallerLineNumber] int sourceLineNumber = 0)
        {
            GetBase().Warn(FormatMessage("", memberName, sourceLineNumber), ex);
        }

        public void Warn(string message, Exception ex = null,
            [System.Runtime.CompilerServices.CallerMemberName] string memberName = "",
            [System.Runtime.CompilerServices.CallerLineNumber] int sourceLineNumber = 0)
        {
            GetBase().Warn(FormatMessage(message, memberName, sourceLineNumber), ex);
        }


        public void Error(Exception ex,
            [System.Runtime.CompilerServices.CallerMemberName] string memberName = "",
            [System.Runtime.CompilerServices.CallerLineNumber] int sourceLineNumber = 0)
        {
            GetBase().Error(FormatMessage("", memberName, sourceLineNumber), ex);
        }

        public void Error(string message, Exception ex = null,
            [System.Runtime.CompilerServices.CallerMemberName] string memberName = "",
            [System.Runtime.CompilerServices.CallerLineNumber] int sourceLineNumber = 0)
        {
            GetBase().Error(FormatMessage(message, memberName, sourceLineNumber), ex);
        }



        public void Fatal(string message, Exception ex = null,
            [System.Runtime.CompilerServices.CallerMemberName] string memberName = "",
            [System.Runtime.CompilerServices.CallerLineNumber] int sourceLineNumber = 0)
        {
            GetBase().Fatal(FormatMessage(message, memberName, sourceLineNumber), ex);
        }


    }
}
