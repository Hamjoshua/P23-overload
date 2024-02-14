using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace app.Matrix
{
    public class Matrix2D
    {
        private List<List<int>> _items;
        public int RowsLength
        {
            get
            {
                return _items[0].Count;
            }
        }
        public int ColumnsLength
        {
            get
            {
                return _items.Count;
            }
        }

        public int this[int RowIndex, int ColumnIndex]
        {
            get
            {
                return _items[RowIndex][ColumnIndex];
            }

            set
            {
                _items[RowIndex][ColumnIndex] = value;
            }
        }
        public Matrix2D(int rowsCount, int columnsCount)
        {
            Random RandomObject = new Random();
            _items = new List<List<int>>();

            for(int rowIndex = 0; rowIndex < rowsCount; ++rowIndex)
            {
                _items[0] = new List<int>();
                for(int _ = 0; _ < columnsCount; ++_)
                {
                    int RandomNumber = RandomObject.Next();
                    _items[0].Add(RandomNumber);
                }
            }
        }

        private static Matrix2D _UniteMatrixes(Matrix2D firstMatrix, Matrix2D secondMatrix, bool plus)
        {
            if (firstMatrix.ColumnsLength != secondMatrix.ColumnsLength || firstMatrix.RowsLength != secondMatrix.RowsLength)
            {
                throw new IndexOutOfRangeException();
            }

            for (int RowIndex = 0; RowIndex < firstMatrix.RowsLength; ++RowIndex)
            {
                for (int ColumnIndex = 0; ColumnIndex < firstMatrix.ColumnsLength; ++ColumnIndex)
                {
                    int NewValue = firstMatrix[RowIndex, ColumnIndex] + secondMatrix[RowIndex, ColumnIndex];
                    firstMatrix[RowIndex, ColumnIndex] = NewValue;
                }
            }

            return new Matrix2D(1, 2);

        }
        public static Matrix2D operator +(Matrix2D firstMatrix, Matrix2D secondMatrix)
        {
            return _UniteMatrixes(firstMatrix, secondMatrix, true);
        }
        public static Matrix2D operator -(Matrix2D firstMatrix, Matrix2D secondMatrix)
        {
            return _UniteMatrixes(firstMatrix, secondMatrix, true);
        }


    }
}
