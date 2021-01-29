using Logic.Shapes;
using System;

namespace Logic.Shapes.Naming
{
    public class Base26AlphabetNumbers : ITriangleNameGenerator
    {
        /// <summary>
        /// Converts the given number to its equivalent base 26 alphabet string equivalent
        /// e.g. 1=A, 26=Z, 27=AA, 79=CA
        /// </summary>
        /// <param name="number">Number to convert</param>
        /// <remarks>http://www.minus40.info/sky/alphabetcountdec.html</remarks>
        /// <returns>Base 26 alphabetic </returns>
        public string NumberToName(int number)
        {
            if (number <= 0) { throw new ArgumentException("Number must be greater than 0", nameof(number)); }

            string columnString = "";
            decimal columnNumber = number;
            while (columnNumber > 0)
            {
                decimal currentLetterNumber = (columnNumber - 1) % 26;
                char currentLetter = (char)(currentLetterNumber + 65);
                columnString = currentLetter + columnString;
                columnNumber = (columnNumber - (currentLetterNumber + 1)) / 26;
            }

            return columnString;
        }
    }
}
