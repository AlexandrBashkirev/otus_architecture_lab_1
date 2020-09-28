
namespace otus_architecture_lab1
{
    class MatrixReaderTextFile : IMatrixReader
    {
        #region Variables

        private string path;

        #endregion



        #region Class lifecycle

        public MatrixReaderTextFile(string path)
        {
            this.path = path;
        }

        #endregion



        #region Methods

        public Matrix Read()
        {
            return new Matrix(1,1);
        }

        #endregion
    }
}
