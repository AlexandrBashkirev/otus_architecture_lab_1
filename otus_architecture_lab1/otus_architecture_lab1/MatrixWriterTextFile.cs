

using System;
using System.IO;

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
            using (StreamWriter file = new StreamWriter(path))
            {
                file.WriteLine(matrix.Rows.ToString());
                file.WriteLine(matrix.Columns.ToString());

                for(int i = 0; i < matrix.Rows; i++)
                {
                    string line = "";

                    for(int j = 0; j < matrix.Columns; j++)
                    {
                        line += matrix[i, j];

                        if(j < matrix.Columns -1)
                        {
                            line += ";";
                        }
                    }
                    file.WriteLine(line);
                }
            }
        }

        #endregion
    }
}
