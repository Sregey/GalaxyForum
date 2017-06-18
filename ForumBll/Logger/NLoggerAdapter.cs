using System;
using NLog;

namespace ForumBll.Logger
{
    public class NLoggerAdapter : ILogger
    {
        private readonly NLog.Logger logger = LogManager.GetCurrentClassLogger();

        public void Debug(string message)
        {
            logger.Debug(message);
        }

        public void Debug(string message, Exception e)
        {
            logger.Debug(e, message);
        }

        public void Error(string message, Exception e)
        {
            logger.Error(e, message);
        }

        public void Fatal(string message, Exception e)
        {
            logger.Fatal(e, message);
        }

        public void Info(string message)
        {
            logger.Info(message);
        }

        public void Info(string message, Exception e)
        {
            logger.Info(e, message);
        }

        public void Trace(string message)
        {
            logger.Trace(message);
        }

        public void Trace(string message, Exception e)
        {
            logger.Trace(e, message);
        }

        public void Warn(string message)
        {
            logger.Warn(message);
        }

        public void Warn(string message, Exception e)
        {
            logger.Warn(e, message);
        }
    }
}
