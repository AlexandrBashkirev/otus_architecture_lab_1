using System;


namespace otus_architecture_lab1
{
    class Matrix
    {
        #region Variables

        private float[,] values = null;

        #endregion



        #region Properties

        public float this[int row, int column]
        {
            set
            {
                if(IsInexesOutOfBound(row, column))
                {
                    throw new Exception("Index out of bound");
                }

                values[row, column] = value;
            }

            get
            {
                if (IsInexesOutOfBound(row, column))
                {
                    throw new Exception("Index out of bound");
                }

                return values[row, column];
            }
        }


        public int Rows { get; set; }

        public int Columns { get; set; }

        

        #endregion



        #region Class lifecicle

        public Matrix(int rows, int columns)
        {
            Rows = rows;
            Columns = columns;

            values = new float[Rows, Columns];
        }


        public bool IsInexesOutOfBound(int i, int j) => i >= Rows || i < 0 || j >= Columns || j < 0;

        #endregion
    }
}
