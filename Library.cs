using System;
using System.IO;
using System.Linq;

namespace app.Matrix
{
    [Serializable]
    public class Matrix2D : IComparable, IComparable<Matrix2D>
    {
        private double[,] _items;
        public int RowsLength
        {
            get
            {
                return _items.GetLength(0);
            }
        }
        public int ColumnsLength
        {
            get
            {
                return _items.GetLength(1);
            }
        }

        public double[] this[int RowIndex]
        {
            get
            {
                double[] ThisRow = new double[] { ColumnsLength };

                for(int ColumnIndex = 0; ColumnIndex < ColumnsLength; ++ColumnIndex)
                {
                    ThisRow[ColumnIndex] = _items[RowIndex, ColumnIndex];
                }
                return ThisRow;
            }

            //set
            //{
            //    if (value.GetLength(0) == ColumnsLength)
            //    {

            //        _items[RowIndex] = value;
            //    }
            //    else
            //    {
            //        throw new IndexOutOfRangeException();
            //    }
            //}
        }

        public double this[int RowIndex, int ColumnIndex]
        {
            get
            {
                return _items[RowIndex, ColumnIndex];
            }

            set
            {
                _items[RowIndex, ColumnIndex] = value;
            }
        }

        // todo 
        public Matrix2D Clone()
        {
            return new Matrix2D();
        }
        public double[,] GetEnumerator()
        {
            return _items.GetEnumerator();
        }

        public override string ToString()
        {
            string StringMatrix = "[";
            for (int RowIndex = 0; RowIndex < RowsLength; ++RowIndex)
            {
                double[] Columns = this[RowIndex];
                string StringRows = $"[{String.Join(", ", Columns.ToArray())}]";
                ListOfStringRows.Add(StringRows);
            }
            StringMatrix += String.Join(",\n ", ListOfStringRows);
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

            double ThisDeterminant = this.GetDeterminant();
            double OtherDeterminant = matrix.GetDeterminant();

            if (ThisDeterminant > OtherDeterminant)
            {
                return 1;
            }
            else if (ThisDeterminant < OtherDeterminant)
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
            _items = new List<List<double>>();
            for (int rowIndex = 0; rowIndex < rowsCount; ++rowIndex)
            {
                _items.Add(new List<double>());
                for (int _ = 0; _ < columnsCount; ++_)
                {
                    _items[rowIndex].Add(0);
                }
            }
        }

        public Matrix2D(int rowsCount, int columnsCount, int maxNumberForRandom)
        {
            Random RandomObject = new Random();
            _items = new List<List<double>>();

            for (int rowIndex = 0; rowIndex < rowsCount; ++rowIndex)
            {
                _items.Add(new List<double>());
                for (int _ = 0; _ < columnsCount; ++_)
                {
                    double RandomNumber = RandomObject.Next(maxNumberForRandom);
                    _items[rowIndex].Add(RandomNumber);
                }
            }
        }

        public List<double> GetColumn(int ColumnIndex)
        {
            List<double> ThisColumn = new List<double>();
            for (int RowIndex = 0; RowIndex < this.RowsLength; ++RowIndex)
            {
                double CurrentColumnElement = this[RowIndex, ColumnIndex];
                ThisColumn.Add(CurrentColumnElement);
            }

            return ThisColumn;
        }

        private static Matrix2D UniteMatrixes(Matrix2D firstMatrix, Matrix2D secondMatrix, bool plus)
        {
            if (firstMatrix.ColumnsLength != secondMatrix.ColumnsLength || firstMatrix.RowsLength != secondMatrix.RowsLength)
            {
                throw new DifferentMatrixesException("Матрицы не совпадают по размеру");
            }

            for (int RowIndex = 0; RowIndex < firstMatrix.RowsLength; ++RowIndex)
            {
                for (int ColumnIndex = 0; ColumnIndex < firstMatrix.ColumnsLength; ++ColumnIndex)
                {
                    double NewValue = 0;
                    double FirstElement = firstMatrix[RowIndex, ColumnIndex];
                    double SecondElement = secondMatrix[RowIndex, ColumnIndex];

                    if (plus)
                    {
                        NewValue = FirstElement + SecondElement;
                    }
                    else
                    {
                        NewValue = FirstElement - SecondElement;
                    }

                    firstMatrix[RowIndex, ColumnIndex] = NewValue;
                }
            }

            return firstMatrix;

        }

        public static Matrix2D operator +(Matrix2D firstMatrix, Matrix2D secondMatrix)
        {
            return UniteMatrixes(firstMatrix, secondMatrix, true);
        }

        public static Matrix2D operator -(Matrix2D firstMatrix, Matrix2D secondMatrix)
        {
            return UniteMatrixes(firstMatrix, secondMatrix, false);
        }

        public static Matrix2D operator *(Matrix2D matrix, int number)
        {
            for (int RowIndex = 0; RowIndex < matrix.RowsLength; ++RowIndex)
            {
                for (int ColumnIndex = 0; ColumnIndex < matrix.ColumnsLength; ++ColumnIndex)
                {
                    matrix[RowIndex, ColumnIndex] *= number;
                }
            }
            return matrix;
        }

        public static explicit operator bool(Matrix2D matrix)
        {
            return matrix.RowsLength == matrix.ColumnsLength;
        }

        public static bool operator true(Matrix2D matrix)
        {
            return matrix.RowsLength == matrix.ColumnsLength;
        }

        public static bool operator false(Matrix2D matrix)
        {
            return !(matrix.RowsLength == matrix.ColumnsLength);
        }

        public static Matrix2D operator *(Matrix2D firstMatrix, Matrix2D secondMatrix)
        {
            if (firstMatrix.ColumnsLength != secondMatrix.RowsLength)
            {
                throw new DifferentMatrixesException("Матрицы не совпадают по размеру");
            }

            Matrix2D NewMatrix = new Matrix2D(firstMatrix.RowsLength, secondMatrix.ColumnsLength);

            for (int FirstRowIndex = 0; FirstRowIndex < firstMatrix.RowsLength; ++FirstRowIndex)
            {
                List<double> FirstRow = firstMatrix[FirstRowIndex];

                for (int SecondColumnIndex = 0; SecondColumnIndex < secondMatrix.ColumnsLength; ++SecondColumnIndex)
                {
                    List<double> SecondColumn = secondMatrix.GetColumn(SecondColumnIndex);
                    double SumElement = 0;

                    for (int CurrentElementIndex = 0; CurrentElementIndex < secondMatrix.RowsLength; ++CurrentElementIndex)
                    {
                        double CurrentElement = FirstRow[CurrentElementIndex] * SecondColumn[CurrentElementIndex];
                        SumElement += CurrentElement;
                    }

                    NewMatrix[FirstRowIndex, SecondColumnIndex] = SumElement;
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

        public void RemoveRowAt(int RowIndex)
        {
            _items.RemoveAt(RowIndex);
        }

        public void RemoveColumnAt(int ColumnIndex)
        {
            for (int RowIndex = 0; RowIndex < this.RowsLength; ++RowIndex)
            {
                this[RowIndex].RemoveAt(ColumnIndex);
            }
        }

        private static Matrix2D GetSubMatrix(int columnIndex, Matrix2D matrix)
        {
            Matrix2D NewMatrix = matrix.Clone() as Matrix2D;
            NewMatrix.RemoveRowAt(0);
            NewMatrix.RemoveColumnAt(columnIndex);

            return NewMatrix;
        }

        private static double _GetDeterminant(Matrix2D Matrix)
        {
            if (Matrix.RowsLength != Matrix.ColumnsLength)
            {
                throw new NotASquareException("Матрица не квадратная");
            }

            double Determinant = 0;

            if (Matrix.RowsLength == 1)
            {
                Determinant = Matrix[0, 0];
            }
            else if (Matrix.RowsLength == 2)
            {
                Determinant = Matrix[0, 0] * Matrix[1, 1] - Matrix[0, 1] * Matrix[1, 0];
            }
            else
            {
                for (int ColumnIndex = 0; ColumnIndex < Matrix.ColumnsLength; ++ColumnIndex)
                {
                    double Minor = Math.Pow(-1, ColumnIndex);
                    double ColumnNumber = Minor * Matrix[0, ColumnIndex];
                    Matrix2D SubMatrix = GetSubMatrix(ColumnIndex, Matrix);

                    Determinant += ColumnNumber * _GetDeterminant(SubMatrix);
                }
            }

            return Determinant;
        }

        public Matrix2D Reverse()
        {
            if (RowsLength != ColumnsLength)
            {
                throw new NotASquareException("");
            }

            if (GetDeterminant() > 0)
            {
                Matrix2D OnesMatrix = new Matrix2D(RowsLength, RowsLength);
                for (int RowColumnIndex = 0; RowColumnIndex < RowsLength; ++RowColumnIndex)
                {
                    OnesMatrix[RowColumnIndex, RowColumnIndex] = 1;
                }

                int RowIndex = 0;
                while (RowIndex < RowsLength + 1)
                {

                    if (RowIndex != 0)
                    {
                        int PreviousColumnIndex = RowIndex - 1;
                        for (int NextRowIndex = 0; NextRowIndex < RowsLength; ++NextRowIndex)
                        {
                            if (NextRowIndex == PreviousColumnIndex)
                            {
                                continue;
                            }
                            double PreviousElement = this[NextRowIndex, PreviousColumnIndex];
                            for (int ColumnIndex = 0; ColumnIndex < ColumnsLength; ++ColumnIndex)
                            {
                                this[NextRowIndex, ColumnIndex] -= this[PreviousColumnIndex, ColumnIndex] * PreviousElement;
                                OnesMatrix[NextRowIndex, ColumnIndex] -= OnesMatrix[PreviousColumnIndex, ColumnIndex] * PreviousElement;
                            }
                        }
                    }

                    if (RowIndex == RowsLength)
                    {
                        break;
                    }
                    double LeadElement = this[RowIndex, RowIndex];
                    this[RowIndex] = this[RowIndex].Select(x => x / LeadElement).ToList();
                    OnesMatrix[RowIndex] = OnesMatrix[RowIndex].Select(x => x / LeadElement).ToList();

                    RowIndex++;
                }
                return OnesMatrix;
            }

            return this;
        }

        public double GetDeterminant()
        {
            return _GetDeterminant(this);
        }

    }
}
