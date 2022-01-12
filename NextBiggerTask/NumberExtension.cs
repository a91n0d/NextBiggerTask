using System;

namespace NextBiggerTask
{
    public static class NumberExtension
    {
        /// <summary>
        /// Finds the nearest largest integer consisting of the digits of the given positive integer number and null if no such number exists.
        /// </summary>
        /// <param name="number">Source number.</param>
        /// <returns>
        /// The nearest largest integer consisting of the digits  of the given positive integer and null if no such number exists.
        /// </returns>
        /// <exception cref="ArgumentException">Thrown when source number is less than 0.</exception>
        public static int? NextBiggerThan(int number)
        {
            // Решение только циклами без применения массивов и строк.
            if (number < 0)
            {
                throw new ArgumentException("source number is less than 0.", nameof(number));
            }

            // Start from the right most digit and find the first digit that is smaller than the digit next to it.
            int tempNumber = number;
            int pivot = 1;
            int step = 1;
            while (tempNumber > 9)
            {
                step *= 10;
                if (tempNumber % 10 > (tempNumber % 100) / 10)
                {
                    pivot = step;
                    break;
                }

                tempNumber /= 10;
            }

            // If no such number exists => return null.
            if (pivot == 1)
            {
                return null;
            }

            // Swap smaller digit and separation number at leftSideNumber and rightSideNumber.
            int i = (number % pivot) / (pivot / 10);
            int j = (number % (pivot * 10)) / pivot;
            int leftSideNumber = ((number / (pivot * 10)) * 10) + i;
            int rightSideNumber = (number % (pivot / 10)) + (pivot / 10 * j);

            // Sorted right side number.
            int sortedRightSideNumber = 0;
            for (int k = 0; k <= 9; k++)
            {
                int tempRightSideNumber = rightSideNumber;
                while (tempRightSideNumber > 0)
                {
                    int digit = tempRightSideNumber % 10;
                    if (digit == k)
                    {
                        sortedRightSideNumber *= 10;
                        sortedRightSideNumber += digit;
                    }

                    tempRightSideNumber /= 10;
                }
            }

            int nextBigger = (leftSideNumber * pivot) + sortedRightSideNumber;

            // Check overflow int type.
            if (nextBigger < 0)
            {
                return null;
            }

            return nextBigger;
        }
    }
}
