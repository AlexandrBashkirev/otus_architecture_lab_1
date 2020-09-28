using System;
using System.IO;


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
            if(!File.Exists(path))
            {
                throw new Exception($"File dosen't exist {path}");
            }

            using (StreamReader file = File.OpenText(path))
            {
                int row = 0;
                if(!Int32.TryParse(file.ReadLine(), out row))
                {
                    throw new Exception($"Cant parce matrix from file: {path}");
                }

                int column = 0;
                if (!Int32.TryParse(file.ReadLine(), out column))
                {
                    throw new Exception($"Cant parce matrix from file: {path}");
                }

                Matrix result = new Matrix(row, column);

                int i = 0;
                while (!file.EndOfStream)
                {
                    string line = file.ReadLine();
                    string [] rowValues = line.Split(new char[] { ',' });

                    int j = 0;
                    foreach(string valueStr in rowValues)
                    {
                        int value = 0;
                        if (!Int32.TryParse(valueStr, out value))
                        {
                            throw new Exception($"Cant parce matrix from file: {path}");
                        }

                        result[i, j] = value;
                        j++;
                    }
                    i++;
                }

                return result;
            }
        }

        #endregion
    }
}
