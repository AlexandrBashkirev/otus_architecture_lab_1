using System.Threading.Tasks;


namespace otus_architecture_lab1
{
    class Program
    {
        static void Main(string[] args)
        {
            ConfigureApp();

            RunApp().GetAwaiter().GetResult();
        }


        static void ConfigureApp()
        {
            SimpleServiceLocator.Instance.RegisterService<ICommandExecutor>(new SequenceCommandExecutor());
        }


        static async Task RunApp()
        {
            Matrix matrixA = new MatrixReaderTextFile("matrixA.txt").Read();
            Matrix matrixB = new MatrixReaderTextFile("matrixB.txt").Read();

            MatrixMult matrixMult = new MatrixMult(matrixA, matrixB);

            matrixMult.Run();

            await matrixMult.Wait();

            new MatrixWriterTextFile("matrixC.txt").Write(matrixMult.Result);
        }
    }
}
