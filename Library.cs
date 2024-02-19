using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace app.Matrix
{
    public class Matrix2D : IComparable, IComparable<Matrix2D>
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

        public List<int> this[int RowIndex]
        {
            get
            {
                return _items[RowIndex];
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

        public IEnumerator<List<int>> GetEnumerator()
        {
            return _items.GetEnumerator();
        }

        public override string ToString()
        {
            string StringMatrix = "[";
            foreach (List<int> Columns in this)
            {
                string StringColumns = "[" + String.Join(", ", Columns.ToArray()) + "]";
                StringMatrix += StringColumns;
            }
            StringMatrix += "]";

            return StringMatrix;
        }

        public int CompareTo(object obj)
        {
            return 0;
        }

        public int CompareTo(Matrix2D matrix)
        {
            if (matrix is null)
            {
                return 1;
            }

            int ThisDeterminant = this.GetDeterminant();
            int MatrixDeterminant = matrix.GetDeterminant();

            if (ThisDeterminant > MatrixDeterminant)
            {
                return 1;
            }
            else if (ThisDeterminant < MatrixDeterminant)
            {
                return -1;
            }
            return 0;
        }

        public static int Compare(Matrix2D first, Matrix2D second)
        {
            return first.CompareTo(second);
        }

        public override bool Equals(object obj)
        {
            if (obj is Matrix2D)
            {
                Matrix2D matrix = obj as Matrix2D;
                for (int RowIndex = 0; RowIndex < matrix.RowsLength; ++RowIndex)
                {
                    if (this[RowIndex] != matrix[RowIndex])
                    {
                        return false;
                    }
                }
                return true;
            }
            return false;
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }

        public Matrix2D(int rowsCount, int columnsCount)
        {
            Random RandomObject = new Random();
            _items = new List<List<int>>();

            for (int rowIndex = 0; rowIndex < rowsCount; ++rowIndex)
            {
                _items[0] = new List<int>();
                for (int _ = 0; _ < columnsCount; ++_)
                {
                    int RandomNumber = RandomObject.Next();
                    _items[0].Add(RandomNumber);
                }
            }
        }

        public List<int> GetRow(int ColumnIndex)
        {
            List<int> ThisRow = new List<int>();
            for (int RowIndex = 0; RowIndex < this.RowsLength; ++RowIndex)
            {
                int CurrentRowElement = this[RowIndex, ColumnIndex];
                ThisRow.Add(CurrentRowElement);
            }

            return ThisRow;
        }

        private static Matrix2D _UniteMatrixes(Matrix2D firstMatrix, Matrix2D secondMatrix, bool plus)
        {
            if (firstMatrix.ColumnsLength != secondMatrix.ColumnsLength || firstMatrix.RowsLength != secondMatrix.RowsLength)
            {
                throw new DifferentMatrixesException();
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

        public static Matrix2D operator *(Matrix2D firstMatrix, Matrix2D secondMatrix)
        {
            int FirstColumnCount = firstMatrix.ColumnsLength;
            int SecondRowsCount = secondMatrix.RowsLength;

            if (FirstColumnCount != SecondRowsCount)
            {
                throw new DifferentMatrixesException();
            }

            Matrix2D NewMatrix = new Matrix2D(FirstColumnCount, SecondRowsCount);

            for (int FirstRowsIndex = 0; FirstRowsIndex < firstMatrix.RowsLength; ++FirstRowsIndex)
            {
                List<int> FirstColumns = firstMatrix[FirstRowsIndex];

                for (int SecondRowIndex = 0; SecondRowIndex < secondMatrix.RowsLength; ++SecondRowIndex)
                {
                    List<int> SecondRows = secondMatrix.GetRow(FirstRowsIndex);
                    int SumElement = 0;

                    for (int CurrentElementIndex = 0; CurrentElementIndex < FirstColumns.Count; ++CurrentElementIndex)
                    {
                        int CurrentElement = FirstColumns[CurrentElementIndex] * SecondRows[CurrentElementIndex];
                        SumElement += CurrentElement;
                    }

                    NewMatrix[FirstRowsIndex, SecondRowIndex] = SumElement;
                }
            }

            return NewMatrix;
        }

        public static bool operator ==(Matrix2D firstMatrix, Matrix2D secondMatrix)
        {
            return Equals(firstMatrix, secondMatrix);
        }

        public static bool operator !=(Matrix2D firstMatrix, Matrix2D secondMatrix)
        {
            return !Equals(firstMatrix, secondMatrix);
        }

        public static bool operator >(Matrix2D firstMatrix, Matrix2D secondMatrix)
        {
            return firstMatrix.CompareTo(secondMatrix) == 1;
        }

        public static bool operator <(Matrix2D firstMatrix, Matrix2D secondMatrix)
        {
            return firstMatrix.CompareTo(secondMatrix) == -1;
        }

        public static bool operator >=(Matrix2D firstMatrix, Matrix2D secondMatrix)
        {
            int Result = firstMatrix.CompareTo(secondMatrix);
            return Result == 1 || Result == 0;
        }

        public static bool operator <=(Matrix2D firstMatrix, Matrix2D secondMatrix)
        {
            int Result = firstMatrix.CompareTo(secondMatrix);
            return Result == -1 || Result == 0;
        }

        public int GetDeterminant()
        {
            if (this.RowsLength != this.ColumnsLength)
            {
                throw new NotASquareException();
            }

            return 1;
        }

    }
}
