using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WeatherAnalysis
{
    class WeatherAnalysis
    {
        public static void Main(string[] args)
        {
            int numComparer = 0;
            //read all files and convert to 2d array
            string[] month = System.IO.File.ReadAllLines(@"data\Month.txt");
            string[] year = System.IO.File.ReadAllLines(@"data\Year.txt");
            string[] tMax = System.IO.File.ReadAllLines(@"data\WS1_TMax.txt");
            string[] tMin = System.IO.File.ReadAllLines(@"data\WS1_TMin.txt");
            string[] airFrost = System.IO.File.ReadAllLines(@"data\WS1_AF.txt");
            string[] rain = System.IO.File.ReadAllLines(@"data\WS1_Rain.txt");
            string[] sun = System.IO.File.ReadAllLines(@"data\WS1_Sun.txt");

            double[,] input = new double[month.Length, 7];

            for (int i = 0; i < month.Length; i++)
            {
                input[i, 0] = ConvertMonthToInt(month[i]);
                input[i, 1] = Convert.ToDouble(year[i]);
                input[i, 2] = Convert.ToDouble(tMax[i]);
                input[i, 3] = Convert.ToDouble(tMin[i]);
                input[i, 4] = Convert.ToDouble(airFrost[i]);
                input[i, 5] = Convert.ToDouble(rain[i]);
                input[i, 6] = Convert.ToDouble(sun[i]);
            }
            
            Quick_Sort(input, 0, input.GetLength(0)-1, 5, ref numComparer);
            //HeapSort(weatherArray, 2);
           // InsertionSort(input, 5, ref numComparer);

            

            //BubbleSort(input, 4, ref numComparer);

            // Console.WriteLine(input[LinearSearch(input, 17.2, 2), 2]);

            Console.WriteLine("{0} {1}", input[1021,2], input[1021, 3]);
            Console.WriteLine("{0}", numComparer);
            Console.Read();
        }

     /*   public static void HeapSort(double[,] input, int key)
        {
            //Build-Max-Heap
            int heapSize = input.GetLength(0);
            for (int p = (heapSize - 1) / 2; p >= 0; p--)
                MaxHeapify(input, heapSize, p, key);

            for (int i = input.GetLength(0) - 1; i > 0; i--)
            {
                //Swap
                double temp = input[i, 0];input[i, 0] = input[0, 0];input[0, 0] = temp;
                temp = input[i, 1]; input[i, 1] = input[0, 1]; input[0, 1] = temp;
                temp = input[i, 2]; input[i, 2] = input[0, 2]; input[0, 2] = temp;
                temp = input[i, 3]; input[i, 3] = input[0, 3]; input[0, 3] = temp;
                temp = input[i, 4]; input[i, 4] = input[0, 4]; input[0, 4] = temp;
                temp = input[i, 5]; input[i, 5] = input[0, 5]; input[0, 5] = temp;
                temp = input[i, 6]; input[i, 6] = input[0, 6]; input[0, 6] = temp;

                heapSize--;
                MaxHeapify(input, heapSize, 0, key);
                
            }
        }
*/

        public static void InsertionSort(double[,] input, int key, ref int numCompare)
        {
            double[,] position = new double[1, 7];

            for (int j = 1; j < input.GetLength(0); j++)
            {
                position[0, 0] = input[j, 0];
                position[0, 1] = input[j, 1];
                position[0, 2] = input[j, 2];
                position[0, 3] = input[j, 3];
                position[0, 4] = input[j, 4];
                position[0, 5] = input[j, 5];
                position[0, 6] = input[j, 6];

                for (int i = j; i > 0; i--)
                {
                    if (input[i - 1, key] > position[0, key])
                    {
                        //Swap
                        input[i, 0] = input[i - 1, 0];
                        input[i, 1] = input[i - 1, 1];
                        input[i, 2] = input[i - 1, 2];
                        input[i, 3] = input[i - 1, 3];
                        input[i, 4] = input[i - 1, 4];
                        input[i, 5] = input[i - 1, 5];
                        input[i, 6] = input[i - 1, 6];

                        input[i - 1, 0] = position[0, 0];
                        input[i - 1, 1] = position[0, 1];
                        input[i - 1, 2] = position[0, 2];
                        input[i - 1, 3] = position[0, 3];
                        input[i - 1, 4] = position[0, 4];
                        input[i - 1, 5] = position[0, 5];
                        input[i - 1, 6] = position[0, 6];
                        numCompare++;
                    }
                    else { numCompare++; }

                }
            }
        }

        public static void BubbleSort(double[,] input, int key, ref int numCompare)
        {
            double[,] temp = new double[1, 7];

            for (int i = 0; i < input.GetLength(0) - 1; i++)
            {
                for (int j = input.GetLength(0) - 1; j > i; j--)
                {
                    if (input[j, key] < input[j - 1, key])
                    {
                        //Swap
                        temp[0, 0] = input[j - 1, 0];
                        temp[0, 1] = input[j - 1, 1];
                        temp[0, 2] = input[j - 1, 2];
                        temp[0, 3] = input[j - 1, 3];
                        temp[0, 4] = input[j - 1, 4];
                        temp[0, 5] = input[j - 1, 5];
                        temp[0, 6] = input[j - 1, 6];

                        input[j - 1, 0] = input[j, 0];
                        input[j - 1, 1] = input[j, 1];
                        input[j - 1, 2] = input[j, 2];
                        input[j - 1, 3] = input[j, 3];
                        input[j - 1, 4] = input[j, 4];
                        input[j - 1, 5] = input[j, 5];
                        input[j - 1, 6] = input[j, 6];

                        input[j, 0] = temp[0, 0];
                        input[j, 1] = temp[0, 1];
                        input[j, 2] = temp[0, 2];
                        input[j, 3] = temp[0, 3];
                        input[j, 4] = temp[0, 4];
                        input[j, 5] = temp[0, 5];
                        input[j, 6] = temp[0, 6];
                        numCompare++;
                    }
                    else { numCompare++; }
                }
            }
        }

        public static int LinearSearch(double[,] data, double value, int key)
        {
            for (int i = 0; i < data.Length; i++)
            {
                if (data[i, key] == value)
                    return i;
            }

            return -1;
        }

        public static void Quick_Sort(double[,] data, int left, int right, int key, ref int numCompare)
        {
            int i, j;
            double pivot;
            double[,] temp = new double[1, 7];
            i = left;
            j = right;
            pivot = data[(left + right) / 2, key];
            do
            {
                while ((data[i, key] < pivot) && (i < right)) i++;
                while ((pivot < data[j, key]) && (j > left)) j--;
                if (i <= j)
                {

                    for (int k = 0; k < 7; k++)
                    {
                        temp[0, k] = data[i, k];
                        data[i, k] = data[j, k];
                        data[j, k] = temp[0, k];
                    }

                    i++;
                    j--;
                    numCompare++;

                }
                else { numCompare++; }
            } while (i <= j);
            if (left < j) Quick_Sort(data, left, j, key, ref numCompare);
            else { numCompare++; }
            if (i < right) Quick_Sort(data, i, right, key, ref numCompare);
            else { numCompare++; }
        }

        static int ConvertMonthToInt(string month)
        {
            int newMonth = 0;

            switch (month)
            {
                case "January":
                case "january":
                    newMonth = 1;
                    break;
                case "February":
                case "february":
                    newMonth = 2;
                    break;
                case "March":
                case "march":
                    newMonth = 3;
                    break;
                case "April":
                case "april":
                    newMonth = 4;
                    break;
                case "May":
                case "may":
                    newMonth = 5;
                    break;
                case "June":
                case "june":
                    newMonth = 6;
                    break;
                case "July":
                case "july":
                    newMonth = 7;
                    break;
                case "August":
                case "august":
                    newMonth = 8;
                    break;
                case "September":
                case "september":
                    newMonth = 9;
                    break;
                case "October":
                case "october":
                    newMonth = 10;
                    break;
                case "November":
                case "november":
                    newMonth = 11;
                    break;
                case "December":
                case "december":
                    newMonth = 12;
                    break;
            }

            return newMonth;
        }

    }
}
