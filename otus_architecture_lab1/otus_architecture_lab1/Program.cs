using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace otus_architecture_lab1
{
    class Program
    {
        static async Task Main(string[] args)
        {
            SimpleServiceLocator.Instance.RegisterService<ICommandExecutor>(new ThreadCommandExecutor());

            Matrix matrixA = new MatrixReaderTextFile().Read();
            Matrix matrixB = new MatrixReaderTextFile().Read();


            MatrixMult matrixMult = new MatrixMult(matrixA, matrixB);

            await matrixMult.Solve();

            new MatrixWriterTextFile().Write(matrixMult.Result);
        }
    }
}
