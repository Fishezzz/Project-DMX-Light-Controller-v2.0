using System;

namespace Logging
{
    /// <summary>
    /// Used for logging across classes, files, namespaces, projects,... in the same solution.<para/>
    /// Based on the Singleton design pattern.
    /// </summary>
    public class Logger
    {
        /// <summary>
        /// Private constructor
        /// </summary>
        private Logger()
        { }

        /// <summary>
        /// Deconstructor, to release all resources when closing
        /// </summary>
        ~Logger()
        {
            writer.Close();
            writer.Dispose();
            writer = null;
        }

        /// <summary>
        /// StreamWriter, used for writing text to the logging fike
        /// </summary>
        private static System.IO.StreamWriter writer;

        /// <summary>
        /// Private unique Logger instance
        /// </summary>
        private static Logger getLogger;
        /// <summary>
        /// Public property of Logger, used to get the unique Logger instance 
        /// </summary>
        public static Logger GetLogger
        {
            get
            {
                // If no instance of logger exists, make a new unique instance
                if (getLogger == null)
                {
                    // Take current DateTime for filename
                    DateTime dt = DateTime.Now;
                    // Initialize StreamWriter for writing to log file
                    writer = new System.IO.StreamWriter($"Logs\\{dt.Year.ToString().PadLeft(4,'0')}{dt.Month.ToString().PadLeft(2, '0')}{dt.Day.ToString().PadLeft(2, '0')}_{dt.Hour.ToString().PadLeft(2, '0')}{dt.Minute.ToString().PadLeft(2, '0')}{dt.Second.ToString().PadLeft(2, '0')}.txt");
                    // Creating unique Logger object
                    getLogger = new Logger();
                }

                // Return the unique Logger instance
                return getLogger;
            }
        }

        /// <summary>
        /// Log info to logging file
        /// </summary>
        /// <param name="info">Info message to be logged</param>
        public void Log(string info)
        {
            DateTime dt = DateTime.Now;
            writer.WriteLine($"[INFO] {dt.Hour.ToString().PadLeft(2, '0')}:{dt.Minute.ToString().PadLeft(2, '0')}:{dt.Second.ToString().PadLeft(2, '0')} >>>>> {info}");
        }

        /// <summary>
        /// Logging warning to logging file
        /// </summary>
        /// <param name="warning">Warning message to be logged</param>
        public void Warn(string warning)
        {
            DateTime dt = DateTime.Now;
            writer.WriteLine($"[WARNING] {dt.Hour.ToString().PadLeft(2, '0')}:{dt.Minute.ToString().PadLeft(2, '0')}:{dt.Second.ToString().PadLeft(2, '0')} >> {warning}");
        }

        /// <summary>
        /// Logging error to logging file
        /// </summary>
        /// <param name="error">Error message to be logged</param>
        public void Error(string error)
        {
            DateTime dt = DateTime.Now;
            writer.WriteLine($"[ERROR] {dt.Hour.ToString().PadLeft(2, '0')}:{dt.Minute.ToString().PadLeft(2, '0')}:{dt.Second.ToString().PadLeft(2, '0')} >>>> {error}");
        }
    }
}
