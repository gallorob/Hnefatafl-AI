namespace HnefataflAI.Commons.DataTypes
{
    /// <summary>
    /// The T Matrix class
    /// </summary>
    /// <typeparam name="T">Generic type</typeparam>
    public class Matrix<T>
    {
        /// <summary>
        /// The actual matrix, containing all the values
        /// </summary>
        private readonly T[,] ActualMatrix;
        /// <summary>
        /// Create a new generic T Matrix given its dimensions
        /// </summary>
        /// <param name="rowNum">Number of rows</param>
        /// <param name="colNum">Number of columns</param>
        public Matrix(int rowNum, int colNum)
        {
            this.ActualMatrix = new T[rowNum, colNum];
        }
        /// <summary>
        /// Create a new generic T Matrix given its matrix
        /// </summary>
        /// <param name="newMatrix">The actual matrix</param>
        public Matrix(T[,] newMatrix)
        {
            this.ActualMatrix = newMatrix;
        }
        /// <summary>
        /// Get a copy of the actual matrix
        /// </summary>
        /// <returns>A copy of the actual matrix</returns>
        public T[,] GetMatrixCopy()
        {
            return (T[,]) this.ActualMatrix.Clone();
        }
        /// <summary>
        /// Get the number of rows
        /// </summary>
        /// <returns>The number of rows</returns>
        public int RowsNumber()
        {
            return this.ActualMatrix.GetLength(0);
        }/// <summary>
         /// Get the number of columns
         /// </summary>
         /// <returns>The number of columns</returns>
        public int ColumnsNumber()
        {
            return this.ActualMatrix.GetLength(1);
        }
        /// <summary>
        /// Sets the value in the matrix
        /// </summary>
        /// <param name="rowNum">Row number</param>
        /// <param name="colNum">Column number</param>
        /// <param name="value">Value to be set</param>
        public void Set(int rowNum, int colNum, T value)
        {
            this.ActualMatrix[rowNum, colNum] = value;
        }
        /// <summary>
        /// Get the value at the specified position in the matrix
        /// </summary>
        /// <param name="rowNum">Row number</param>
        /// <param name="colNum">Column number</param>
        /// <returns>The value at the specified position</returns>
        public T At(int rowNum, int colNum)
        {
            return this.ActualMatrix[rowNum, colNum];
        }
        /// <summary>
        /// Get the row specified
        /// </summary>
        /// <param name="rowNum">Row number</param>
        /// <returns>The whole row</returns>
        public T[] GetRow(int rowNum)
        {
            return GetRow(rowNum, this.RowsNumber());
        }
        /// <summary>
        /// Get the row specified and cut at the specified length
        /// </summary>
        /// <param name="rowNum">Row number</param>
        /// <param name="range">Length desired</param>
        /// <returns>The row with the specified length</returns>
        public T[] GetRow(int rowNum, int range)
        {
            return GetRow(rowNum, range, 0);
        }
        /// <summary>
        /// Get the row specified and cut at the specified length, starting from the specified index
        /// </summary>
        /// <param name="rowNum">Row number</param>
        /// <param name="range">Length desired</param>
        /// <param name="begin">Beginning index</param>
        /// <returns>The row with the specified length beginning at the specified index</returns>
        public T[] GetRow(int rowNum, int range, int begin)
        {
            T[] row = new T[range];
            for (int i = 0; i < range; i++)
            {
                row[i] = this.ActualMatrix[rowNum, i + begin];
            }
            return row;
        }
        /// <summary>
        /// Get the column specified
        /// </summary>
        /// <param name="colNum">Column number</param>
        /// <returns>The whole column</returns>
        public T[] GetCol(int colNum)
        {
            return GetCol(colNum, this.ColumnsNumber());
        }
        /// <summary>
        /// Get the column specified and cut at the specified length
        /// </summary>
        /// <param name="colNum">Column number</param>
        /// <param name="range">Length desired</param>
        /// <returns>The column with the specified length</returns>
        public T[] GetCol(int colNum, int range)
        {
            return GetCol(colNum, range, 0);
        }
        /// <summary>
        /// Get the column specified and cut at the specified length, starting from the specified index
        /// </summary>
        /// <param name="colNum">Column number</param>
        /// <param name="range">Length desired</param>
        /// <param name="begin">Beginning index</param>
        /// <returns>The column with the specified length beginning at the specified index</returns>
        public T[] GetCol(int colNum, int range, int begin)
        {
            T[] col = new T[range];
            for (int i = 0; i < range; i++)
            {
                col[i] = this.ActualMatrix[i + begin, colNum];
            }
            return col;
        }
        /// <summary>
        /// Get the squared sub-matrix of the matrix
        /// </summary>
        /// <param name="rowNum">Row number</param>
        /// <param name="colNum">Column number</param>
        /// <param name="range">The dimension of the sub-matrix</param>
        /// <returns>The squared sub-matrix of the matrix</returns>
        public T[,] GetRange(int rowNum, int colNum, int range)
        {
            return GetRange(rowNum, colNum, range, range);
        }
        /// <summary>
        /// Get the sub-matrix of the matrix
        /// </summary>
        /// <param name="rowNum">Row number</param>
        /// <param name="colNum">Column number</param>
        /// <param name="rowRange">The rows dimension of the sub-matrix</param>
        /// <param name="colRange">The columns dimension of the sub-matrix</param>
        /// <returns>The sub-matrix of the matrix</returns>
        public T[,] GetRange(int rowNum, int colNum, int rowRange, int colRange)
        {
            T[,] square = new T[rowRange, colRange];
            for (int i = 0; i < rowRange; i++)
            {
                for (int j = 0; j < colRange; j++)
                {
                    square[i, j] = this.ActualMatrix[i + rowNum, j + colNum];
                }
            }
            return square;
        }
    }
}
