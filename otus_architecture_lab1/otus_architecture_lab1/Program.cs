using System.Threading.Tasks;


namespace otus_architecture_lab1
{
    class Program
    {
        static void Main(string[] args)
        {
            ConfigureApp();

            RunApp().GetAwaiter().GetResult();

            SimpleServiceLocator.Instance.Dispose();
        }


        static void ConfigureApp()
        {
            SimpleServiceLocator.Instance.RegisterService<ILogger>(new FileLogger("log.txt"));
            SimpleServiceLocator.Instance.RegisterService<ICommandExecutor>(new ThreadCommandExecutor());
        }


        static async Task RunApp()
        {
            Matrix matrixA = new MatrixReaderTextFile("matrixA.txt").Read();
            Matrix matrixB = new MatrixReaderTextFile("matrixB.txt").Read();

            MatrixMultCommand matrixMult = new MatrixMultCommand(matrixA, matrixB);
            matrixMult.SetResultCallback((isSuccess, result) =>
            {
                new MatrixWriterTextFile("matrixC.txt").Write(result as Matrix);
            });
            SimpleServiceLocator.Instance.GetService<ICommandExecutor>().Execute(matrixMult);

            await matrixMult.Wait();
        }
    }
}
