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
            SimpleServiceLocator.Instance.RegisterService<ICommandExecutor>(new ThreadCommandExecutor());
        }


        static async Task RunApp()
        {
            Matrix matrixA = new MatrixReaderTextFile("matrixA").Read();
            Matrix matrixB = new MatrixReaderTextFile("matrixB").Read();

            MatrixMult matrixMult = new MatrixMult(matrixA, matrixB);

            await matrixMult.Solve();

            new MatrixWriterTextFile("matrixC").Write(matrixMult.Result);
        }
    }
}
