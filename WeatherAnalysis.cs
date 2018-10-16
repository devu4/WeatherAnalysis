using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;

namespace WeatherAnalysis
{
    class WeatherAnalysis
    {
        static void Main(string[] args)
        {
            //read all files and convert to 2d array
            string[] month = System.IO.File.ReadAllLines(@"data\Month.txt");
            string[] year = System.IO.File.ReadAllLines(@"data\Year.txt");
            string[] tMax = System.IO.File.ReadAllLines(@"data\WS1_TMax.txt");
            string[] tMin = System.IO.File.ReadAllLines(@"data\WS1_TMin.txt");
            string[] airFrost = System.IO.File.ReadAllLines(@"data\WS1_AF.txt");
            string[] rain = System.IO.File.ReadAllLines(@"data\WS1_Rain.txt");
            string[] sun = System.IO.File.ReadAllLines(@"data\WS1_Sun.txt");

            double[,] weatherArray = new double[month.Length, 7];

            //initiate weatherArray
            for (int i = 0; i < month.Length; i++)
            {
                weatherArray[i, 0] = ConvertMonthToInt(month[i]);
                weatherArray[i, 1] = Convert.ToDouble(year[i]);
                weatherArray[i, 2] = Convert.ToDouble(tMax[i]);
                weatherArray[i, 3] = Convert.ToDouble(tMin[i]);
                weatherArray[i, 4] = Convert.ToDouble(airFrost[i]);
                weatherArray[i, 5] = Convert.ToDouble(rain[i]);
                weatherArray[i, 6] = Convert.ToDouble(sun[i]);
            }

            //initiate needed variables
            string actionMode = "";
            bool fileChosen = false; bool fileActionChosen = false; bool monthEntered = false;
            string fileAction = ""; string monthInput = "";
            int fileToAnalyse = -1;
            string htmlStart = ""; string htmlEnd = ""; string htmlMiddle = "";

            // General Title for Program
            Console.WriteLine("------------------------------------------------------------------------------------");
            Console.WriteLine("David's Weather Analyser");
            Console.WriteLine("------------------------------------------------------------------------------------");
            
            //Main Menu 
            bool actionEntered = false;
            while (!actionEntered) //loop when false.
            {
                Console.WriteLine("-- Main Menu --\nPlease choice an item!");
                Console.WriteLine("------------------------------------------------------------------------------------");
                Console.WriteLine("1. Analyse single data file");
                Console.WriteLine("2. Search by Month");
                Console.WriteLine("3. Search by Year");
                Console.WriteLine("------------------------------------------------------------------------------------");
                actionMode = Console.ReadLine();

                if (actionMode == "1")
                {
                    actionEntered = true; //stop loop
                }
                else if (actionMode == "2")
                {
                    actionEntered = true; //stop loop
                }
                else if (actionMode == "3")
                {
                    actionEntered = true; //stop loop
                }
                else
                {
                    Console.WriteLine("Wrong Value, please enter 1,2 or 3!");
                    Console.WriteLine("-----------------------------------------------------------------------------------");
                }
            }

            if (actionMode == "1") //show second menu for all arrays
            {
                while (!fileChosen)
                {
                    Console.WriteLine("Please select a file to Analyse");
                    Console.WriteLine("-----------------------------------------------------------------------------------");
                    Console.WriteLine("1. Month.txt");
                    Console.WriteLine("2. Year.txt");
                    Console.WriteLine("3. WS1_TMax.txt");
                    Console.WriteLine("4. WS1_TMin.txt");
                    Console.WriteLine("5. WS1_AF.txt");
                    Console.WriteLine("6. WS1_Rain.txt");
                    Console.WriteLine("7. WS1_Sun.txt");
                    Console.WriteLine("-----------------------------------------------------------------------------------");
                    string fileInput = Console.ReadLine();

                    //set which array to analyse or show error if wrong value is input
                    if (fileInput == "1")
                    {
                        fileToAnalyse = 0; //set which array to analyse
                        fileChosen = true;
                    }
                    else if (fileInput == "2")
                    {
                        fileToAnalyse = 1;
                        fileChosen = true;
                    }
                    else if (fileInput == "3")
                    {
                        fileToAnalyse = 2;
                        fileChosen = true;
                    }
                    else if (fileInput == "4")
                    {
                        fileToAnalyse = 3;
                        fileChosen = true;
                    }
                    else if (fileInput == "5")
                    {
                        fileToAnalyse = 4;
                        fileChosen = true;
                    }
                    else if (fileInput == "6")
                    {
                        fileToAnalyse = 5;
                        fileChosen = true;
                    }
                    else if (fileInput == "7")
                    {
                        fileToAnalyse = 6;
                        fileChosen = true;
                    }
                    else
                    {
                        Console.WriteLine("Wrong Value, please enter 1, 2, 3, 4, 5, 6 or 7!");
                        Console.WriteLine("-----------------------------------------------------------------------------------");
                    }
                }

                while (!fileActionChosen) //Function choosing menu
                {
                    Console.WriteLine("What would you like to do with this data?");
                    Console.WriteLine("-----------------------------------------------------------------------------------");
                    Console.WriteLine("1. Sort asc");
                    Console.WriteLine("2. Sort desc");
                    Console.WriteLine("3. Find Max Value");
                    Console.WriteLine("4. Find Min Value");
                    Console.WriteLine("-----------------------------------------------------------------------------------");
                    fileAction = Console.ReadLine();

                    if (fileAction == "1")
                    {
                        Quick_Sort(weatherArray, 0, weatherArray.GetLength(0) - 1, fileToAnalyse); //sort data

                        htmlStart = System.IO.File.ReadAllText(@"Templates\HTMLStartTemplate.txt"); //loads the start of html template
                        htmlEnd = System.IO.File.ReadAllText(@"Templates\HTMLEndTemplate.txt"); //loads the end of html template
                        htmlEnd = String.Format(htmlEnd, "<style>table tbody tr td:nth-child(" + (fileToAnalyse + 1) + "){font-weight:600;}</style>");

                        for (int shift = 0; shift < weatherArray.GetLength(0); shift++) //output sorted array into HTML Page
                        {
                            string message = String.Format(@"<tr>
							<td>{0}</td>
							<td>{1}</td>
							<td>{2}</td>
							<td>{3}</td>
							<td>{4}</td>
							<td>{5}</td>
							<td>{6}</td>
						</tr>", ConvertInttoMonth(weatherArray[shift, 0]), weatherArray[shift, 1], weatherArray[shift, 2], weatherArray[shift, 3], weatherArray[shift, 4], weatherArray[shift, 5], weatherArray[shift, 6]);

                            htmlMiddle += message;//append middle html to what was before to create a full length page

                        }


                        string[] lines = { htmlStart, htmlMiddle, htmlEnd }; //array of lines to go into output html page
                        System.IO.File.WriteAllLines(@"WeatherOutput.html", lines); //insert lines array into output html page

                        System.Diagnostics.Process.Start(@"WeatherOutput.html"); //opens webpage in default application

                        fileActionChosen = true;

                    }
                    else if (fileAction == "2")
                    {
                        Quick_Sort(weatherArray, 0, weatherArray.GetLength(0) - 1, fileToAnalyse);

                        htmlStart = System.IO.File.ReadAllText(@"Templates\HTMLStartTemplate.txt"); //loads the start of html template
                        htmlEnd = System.IO.File.ReadAllText(@"Templates\HTMLEndTemplate.txt"); //loads the end of html template
                        htmlEnd = String.Format(htmlEnd, "<style>table tbody tr td:nth-child(" + (fileToAnalyse + 1) + "){font-weight:600;}</style>");

                        for (int shift = weatherArray.GetLength(0) - 1; shift >= 0; --shift)//output sorted array into HTML Page
                        {
                            string message = String.Format(@"<tr>
							<td>{0}</td>
							<td>{1}</td>
							<td>{2}</td>
							<td>{3}</td>
							<td>{4}</td>
							<td>{5}</td>
							<td>{6}</td>
						</tr>", ConvertInttoMonth(weatherArray[shift, 0]), weatherArray[shift, 1], weatherArray[shift, 2], weatherArray[shift, 3], weatherArray[shift, 4], weatherArray[shift, 5], weatherArray[shift, 6]);

                            htmlMiddle += message;//append middle html to what was before to create a full length page

                        }


                        string[] lines = { htmlStart, htmlMiddle, htmlEnd }; //array of lines to go into output html page
                        System.IO.File.WriteAllLines(@"WeatherOutput.html", lines); //insert lines array into output html page

                        System.Diagnostics.Process.Start(@"WeatherOutput.html"); //opens webpage in default application

                        fileActionChosen = true;
                    }
                    else if (fileAction == "3")
                    {
                        Quick_Sort(weatherArray, 0, weatherArray.GetLength(0) - 1, fileToAnalyse);

                        htmlStart = System.IO.File.ReadAllText(@"Templates\HTMLStartTemplate.txt"); //loads the start of html template
                        htmlEnd = System.IO.File.ReadAllText(@"Templates\HTMLEndTemplate.txt"); //loads the end of html template
                        htmlEnd = String.Format(htmlEnd, "<style>table tbody tr td:nth-child(" + (fileToAnalyse + 1) + "){font-weight:600;}</style>");

                        List<int> numberList = new List<int>(); //create list to store all max values
                        numberList.Add(weatherArray.GetLength(0)-1);

                        for (int shift = weatherArray.GetLength(0) - 2; shift >= 0; --shift) //find all max value and add to list
                        {
                            if ((weatherArray[weatherArray.GetLength(0) - 1, fileToAnalyse]) <= (weatherArray[shift, fileToAnalyse]))
                            {
                                numberList.Add(shift);
                            }
                        }

                        foreach (int element in numberList) //Output to HTML page
                        {
                            string message = String.Format(@"<tr>
							<td>{0}</td>
							<td>{1}</td>
							<td>{2}</td>
							<td>{3}</td>
							<td>{4}</td>
							<td>{5}</td>
							<td>{6}</td>
						</tr>", ConvertInttoMonth(weatherArray[element, 0]), weatherArray[element, 1], weatherArray[element, 2], weatherArray[element, 3], weatherArray[element, 4], weatherArray[element, 5], weatherArray[element, 6]);

                            htmlMiddle += message;//append middle html to what was before to create a full length page
                        }


                        string[] lines = { htmlStart, htmlMiddle, htmlEnd }; //array of lines to go into output html page
                        System.IO.File.WriteAllLines(@"WeatherOutput.html", lines); //insert lines array into output html page

                        System.Diagnostics.Process.Start(@"WeatherOutput.html"); //opens webpage in default application

                        fileActionChosen = true;
                    }
                    else if (fileAction == "4")
                    {
                        Quick_Sort(weatherArray, 0, weatherArray.GetLength(0) - 1, fileToAnalyse);

                        htmlStart = System.IO.File.ReadAllText(@"Templates\HTMLStartTemplate.txt"); //loads the start of html template
                        htmlEnd = System.IO.File.ReadAllText(@"Templates\HTMLEndTemplate.txt"); //loads the end of html template
                        htmlEnd = String.Format(htmlEnd, "<style>table tbody tr td:nth-child(" + (fileToAnalyse + 1) + "){font-weight:600;}</style>");

                        List<int> numberList = new List<int>();
                        numberList.Add(0);

                        for (int shift = 1; shift < weatherArray.GetLength(0); shift++)
                        {
                            if ((weatherArray[0, fileToAnalyse]) >= (weatherArray[shift, fileToAnalyse]))
                            {
                                numberList.Add(shift);
                            }
                        }

                        foreach (int element in numberList)
                        {
                            string message = String.Format(@"<tr>
							<td>{0}</td>
							<td>{1}</td>
							<td>{2}</td>
							<td>{3}</td>
							<td>{4}</td>
							<td>{5}</td>
							<td>{6}</td>
						</tr>", ConvertInttoMonth(weatherArray[element, 0]), weatherArray[element, 1], weatherArray[element, 2], weatherArray[element, 3], weatherArray[element, 4], weatherArray[element, 5], weatherArray[element, 6]);

                            htmlMiddle += message;//append middle html to what was before to create a full length page
                        }


                        string[] lines = { htmlStart, htmlMiddle, htmlEnd }; //array of lines to go into output html page
                        System.IO.File.WriteAllLines(@"WeatherOutput.html", lines); //insert lines array into output html page

                        System.Diagnostics.Process.Start(@"WeatherOutput.html"); //opens webpage in default application

                        fileActionChosen = true;
                    }
                    else
                    {
                        Console.WriteLine("Wrong Value, please enter 1, 2, 3 or 4!");
                        Console.WriteLine("-----------------------------------------------------------------------------------");
                    }
                }

            }
            else if (actionMode == "2")
            {
                while (!monthEntered)
                {
                    Console.WriteLine("-----------------------------------------------------------------------------------");
                    Console.WriteLine("Please enter the month you are searching for as a integer where January is 1 and December is 12.");
                    Console.WriteLine("-----------------------------------------------------------------------------------");
                    monthInput = Console.ReadLine();
                    //check value is a month
                    if (monthInput == "1" || monthInput == "2" || monthInput == "3" || monthInput == "4" || monthInput == "5" || monthInput == "6" || monthInput == "7" || monthInput == "8" || monthInput == "9" || monthInput == "10" || monthInput == "11" || monthInput == "12")
                        monthEntered = true;
                    else
                    {
                        Console.WriteLine("Wrong Value, please choice a value from 1 to 12.");
                        Console.WriteLine("-----------------------------------------------------------------------------------");
                    }
                }
                //First sort array, using month column to sort
                Quick_Sort(weatherArray, 0, weatherArray.GetLength(0) - 1, 0);
                int indexValue = BinarySearch(weatherArray, Convert.ToInt32(monthInput), 0, weatherArray.GetLength(0) - 1, 0);

                htmlStart = System.IO.File.ReadAllText(@"Templates\HTMLStartTemplate.txt"); //loads the start of html template
                htmlEnd = System.IO.File.ReadAllText(@"Templates\HTMLEndTemplate.txt"); //loads the end of html template
                htmlEnd = String.Format(htmlEnd, "<style>table tbody tr td:nth-child(" + (1) + "){font-weight:600;}</style>");

                if (indexValue != -1)
                {
                    List<int> numberList = new List<int>(); //create list to store search years
                    numberList.Add(indexValue); //add the index already found


                    for (int shift = indexValue+1; shift < weatherArray.GetLength(0); shift++) //look forwards from index
                    {
                        if ((weatherArray[indexValue, 0]) == (weatherArray[shift, 0])) //if value is same as index found
                        {
                            numberList.Add(shift); // then add to number list
                        }
                    }

                    for (int shift = indexValue-1; shift > -1; --shift) //look backwards from index
                    {
                        if ((weatherArray[indexValue, 0]) == (weatherArray[shift, 0]))
                        {
                            numberList.Add(shift);
                        }
                    }

                    foreach (int element in numberList) //loop through number list and display
                    {
                        string message = String.Format(@"<tr>
							<td>{0}</td>
							<td>{1}</td>
							<td>{2}</td>
							<td>{3}</td>
							<td>{4}</td>
							<td>{5}</td>
							<td>{6}</td>
						</tr>", ConvertInttoMonth(weatherArray[element, 0]), weatherArray[element, 1], weatherArray[element, 2], weatherArray[element, 3], weatherArray[element, 4], weatherArray[element, 5], weatherArray[element, 6]);

                        htmlMiddle += message;//append middle html to what was before to create a full length page
                    }
                }
                else
                {
                    htmlMiddle = "<tr><td colspan='7'>Value you selected could not be found!</td></tr>";
                }
                string[] lines = { htmlStart, htmlMiddle, htmlEnd }; //array of lines to go into output html page
                System.IO.File.WriteAllLines(@"WeatherOutput.html", lines); //insert lines array into output html page

                System.Diagnostics.Process.Start(@"WeatherOutput.html"); //opens webpage in default application

            }
            else
            {
                while (!monthEntered)
                {
                    Console.WriteLine("-----------------------------------------------------------------------------------");
                    Console.WriteLine("Please enter the year you are searching for as a integer in form YYYY");
                    Console.WriteLine("-----------------------------------------------------------------------------------");
                    monthInput = Console.ReadLine();
                    //check value is a month

                    Match match = Regex.Match(monthInput, @"^[0-9]{4}$");

                    // Here we check the Match instance.
                    if (match.Success)
                    {
                        monthEntered = true;
                    }
                    else
                    {
                        Console.WriteLine("Wrong Value, please choice a integer with format YYYY.");
                        Console.WriteLine("-----------------------------------------------------------------------------------");
                    }
                }
                //First sort array, using month column to sort
                Quick_Sort(weatherArray, 0, weatherArray.GetLength(0) - 1, 1);
                int indexValue = BinarySearch(weatherArray, Convert.ToInt32(monthInput), 0, weatherArray.GetLength(0) - 1, 1);

                htmlStart = System.IO.File.ReadAllText(@"Templates\HTMLStartTemplate.txt"); //loads the start of html template
                htmlEnd = System.IO.File.ReadAllText(@"Templates\HTMLEndTemplate.txt"); //loads the end of html template
                htmlEnd = String.Format(htmlEnd, "<style>table tbody tr td:nth-child(2){font-weight:600;}</style>");

                if (indexValue != -1)
                {
                    List<int> numberList = new List<int>();
                    numberList.Add(indexValue);


                    for (int shift = indexValue + 1; shift < weatherArray.GetLength(0); shift++)
                    {
                        if ((weatherArray[indexValue, 1]) == (weatherArray[shift, 1]))
                        {
                            numberList.Add(shift);
                        }
                    }

                    for (int shift = indexValue - 1; shift > -1; --shift)
                    {
                        if ((weatherArray[indexValue, 1]) == (weatherArray[shift, 1]))
                        {
                            numberList.Add(shift);
                        }
                    }

                    foreach (int element in numberList)
                    {
                        string message = String.Format(@"<tr>
							<td>{0}</td>
							<td>{1}</td>
							<td>{2}</td>
							<td>{3}</td>
							<td>{4}</td>
							<td>{5}</td>
							<td>{6}</td>
						</tr>", ConvertInttoMonth(weatherArray[element, 0]), weatherArray[element, 1], weatherArray[element, 2], weatherArray[element, 3], weatherArray[element, 4], weatherArray[element, 5], weatherArray[element, 6]);

                        htmlMiddle += message;//append middle html to what was before to create a full length page
                    }
                }
                else
                {
                    htmlMiddle = "<tr><td colspan='7'>No results for the year you selected could be found!</td></tr>";
                }
                string[] lines = { htmlStart, htmlMiddle, htmlEnd }; //array of lines to go into output html page
                System.IO.File.WriteAllLines(@"WeatherOutput.html", lines); //insert lines array into output html page

                System.Diagnostics.Process.Start(@"WeatherOutput.html"); //opens webpage in default application
            }

            Console.WriteLine("------------------------------------------------------------------------------------");
            Console.WriteLine("Thank You for using the analyser, Goodbye!");
            Console.WriteLine("------------------------------------------------------------------------------------");
            Console.ReadKey();
        }

        /// <summary>
        /// Convert Month name to int value
        /// </summary>
        /// <param name="month">Month name as string e.g. January</param>
        /// <returns></returns>
        static int ConvertMonthToInt( string month )
        {
            int newMonth = 0;

            switch(month)
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
        /// <summary>
        /// Convert an integer from 1-12 into a month string
        /// </summary>
        /// <param name="month"></param>
        /// <returns></returns>
        static string ConvertInttoMonth(double month)
        {
            string newMonth = "";

            switch ((int)month) 
            {
                case 1:
                    newMonth = "January";
                    break;
                case 2:
                    newMonth = "February";
                    break;
                case 3:
                    newMonth = "March";
                    break;
                case 4:
                    newMonth = "April";
                    break;
                case 5:
                    newMonth = "May";
                    break;
                case 6:
                    newMonth = "June";
                    break;
                case 7:
                    newMonth = "July";
                    break;
                case 8:
                    newMonth = "August";
                    break;
                case 9:
                    newMonth = "September";
                    break;
                case 10:
                    newMonth = "October";
                    break;
                case 11:
                    newMonth = "November";
                    break;
                case 12:
                    newMonth = "December";
                    break;
            }

            return newMonth;
        }

        /// <summary>
        /// Quick Sort Method
        /// </summary>
        /// <param name="data">2d array of weather data</param>
        /// <param name="left">key of array element to the left</param>
        /// <param name="right">key of array element to the Right</param>
        /// <param name="key">key of array element to sort</param>
        public static void Quick_Sort(double[,] data, int left, int right, int key)
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

                    for(int k = 0; k < 7; k++)
                    {
                        temp[0, k] = data[i, k];
                        data[i, k] = data[j, k];
                        data[j, k] = temp[0, k];
                    }

                    i++;
                    j--;

                }
            } while (i <= j);
            if (left < j) Quick_Sort(data, left, j, key);
            if (i < right) Quick_Sort(data, i, right, key);
        }

        /// <summary>
        /// Binary search array and find index for key
        /// </summary>
        /// <param name="data"></param>
        /// <param name="key"></param>
        /// <param name="min"></param>
        /// <param name="max"></param>
        /// <param name="col"></param>
        /// <returns></returns>
        public static int BinarySearch(double[,] data, int key, int min, int max, int col)
        {
            if (min > max)
            {
                return -1;
            }
            else
            {
                int mid = (min + max) / 2;
                if (key == data[mid, col])
                {
                    return ++mid;
                }
                else if (key < data[mid, col])
                {
                    return BinarySearch(data, key, min, mid - 1, col);
                }
                else
                {
                    return BinarySearch(data, key, mid + 1, max, col);
                }
            }
        }
        
    }
}
