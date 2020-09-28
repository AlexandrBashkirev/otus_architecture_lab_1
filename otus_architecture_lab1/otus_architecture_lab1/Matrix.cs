using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace otus_architecture_lab1
{
    class Matrix
    {
        #region Variables

        private float[,] values = null;

        #endregion



        #region Properties

        public float this[int i, int j]
        {
            set
            {
                if(IsInexesOutOfBound(i, j))
                {
                    throw new Exception("Index out of bound");
                }

                values[i, j] = value;
            }

            get
            {
                if (IsInexesOutOfBound(i, j))
                {
                    throw new Exception("Index out of bound");
                }

                return values[i, j];
            }
        }


        public int SizeI { get; set; }

        public int SizeJ { get; set; }

        

        #endregion



        #region Class lifecicle

        public Matrix(int sizeI, int sizeJ)
        {
            SizeI = sizeI;
            SizeJ = sizeJ;

            values = new float[sizeI, sizeJ];
        }


        public bool IsInexesOutOfBound(int i, int j) => i >= SizeI || i < 0 || j >= SizeJ || j < 0;

        #endregion
    }
}
