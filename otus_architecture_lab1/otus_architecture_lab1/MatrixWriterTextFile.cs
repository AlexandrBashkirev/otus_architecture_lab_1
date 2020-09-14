

namespace otus_architecture_lab1
{
    class MatrixWriterTextFile : IMatrixWriter
    {
        #region Variables

        private string path;

        #endregion



        #region Class lifecycle

        public MatrixWriterTextFile(string path)
        {
            this.path = path;
        }

        #endregion



        #region Methods

        public void Write(Matrix matrix)
        {

        }

        #endregion
    }
}
