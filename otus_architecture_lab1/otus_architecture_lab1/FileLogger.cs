using System.IO;


namespace otus_architecture_lab1
{
    class FileLogger : ILogger
    {
        #region Variables

        private StreamWriter file;
        private readonly object lockObj = new object();

        #endregion



        #region Class lifecycle

        public FileLogger(string filePath)
        {
            file = new StreamWriter(filePath);
        }


        public void Dispose()
        {
            file.Close();
            file.Dispose();
        }

        #endregion



        #region Methods

        public void Log(string message)
        {
            lock(lockObj)
            {
                file.WriteLine(message);
            }
        }

        #endregion
    }
}
