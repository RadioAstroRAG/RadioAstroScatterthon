using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MeteorWatch
{
    class RmobFile
    {
        private DateTime monthAndYear = new DateTime();

        // To be consistent with the file structure.
        List<string[]> listOfHoursAcrossDates = new List<string[]>();

        public RmobFile(DateTime monthAndYear)
        {
            this.monthAndYear = monthAndYear;

            MakeHoursGrid();
        }
        
        public void SetDataForDay(DateTime timestamp, Dictionary<int, int> countPerHour)
        {
            // This data is for one day.
            int day = timestamp.Day;
            // Go through each index corresponding to that day only...
            foreach(KeyValuePair<int, int> hourCount in countPerHour)
            {
                listOfHoursAcrossDates[hourCount.Key][day] = hourCount.Value.ToString();
            }
        }

        public Dictionary<DateTime, Dictionary<int, int>> GetMonthsData()
        {
            Dictionary<DateTime, Dictionary<int, int>> collectionOfDays = new Dictionary<DateTime, Dictionary<int, int>>();
                        
            Dictionary<int, int> daysData = new Dictionary<int, int>();

            // for each hour of the day...
            for (int hour = 0; hour < 24; hour++)
            {
                // Index in the listOfHoursAcrossDates indicates the hour.
                string[] specificHour = listOfHoursAcrossDates[hour];

                // The corresponding string[] contains 31 elements, with an hour per date in the month.
                int daysInMonth = DateTime.DaysInMonth(monthAndYear.Year, monthAndYear.Month);

                for (int date = 0; date < daysInMonth; date++)
                {
                    DateTime dateForData = monthAndYear.AddDays(date);

                    if (!collectionOfDays.ContainsKey(dateForData))
                    {
                        collectionOfDays.Add(dateForData, new Dictionary<int, int>());
                    }

                    int meteorCount = -1;

                    // This is due to the formatting of RMOB files,
                    // where each line begines with the number of the hour
                    // as opposed to a count of meteors in the hour...
                    string readCount = specificHour[date + 1];

                    if (!string.IsNullOrEmpty(readCount))
                    {
                        meteorCount = int.Parse(readCount);
                    }

                    collectionOfDays[dateForData].Add(hour, meteorCount);
                }
            }

            return collectionOfDays;
        }

        public Dictionary<DateTime, Dictionary<int, int>> RetreiveHistoricalData(string whereFrom)
        {
            Dictionary<DateTime, Dictionary<int, int>> keyValue = new Dictionary<DateTime,Dictionary<int,int>>();

            if (File.Exists(whereFrom))
            {
                LoadFile(whereFrom);

            }
            return keyValue;
        }

        public void LoadFile(string fileName)
        {
            // It should have been clear anyway...
            listOfHoursAcrossDates.Clear();
            MakeHoursGrid();

            string[] lines = File.ReadAllLines(fileName);

            if (lines[0].StartsWith("UT|1|2|3") && lines[1].StartsWith("---|u|"))
            {
                // There's a high chance that the rest of the data is valid.
                // So, skip the first two headers..
                for (int index = 2; index < 26; index++)
                {
                    string[] hoursAcrossDates = lines[index].Split('|');
                    listOfHoursAcrossDates[index - 2] = hoursAcrossDates;
                }
            }
        }

        public string MakeFile()
        {
            string header1 = "UT|1|2|3|4|5|6|7|8|9|10|11|12|13|14|15|16|17|18|19|20|21|22|23|24|25|26|27|28|29|30|31";
            string header2 = "---|u|----|----|----|----|----|----|----|----|----|----|----|----|----|----|----|----|----|----|----|----|----|----|----|----";

            StringBuilder fileBuilder = new StringBuilder();
            fileBuilder.AppendLine(header1);
            fileBuilder.AppendLine(header2);

            for(int index = 0; index < 24; index++)
            {
                string lineOfCounts = string.Join("|", listOfHoursAcrossDates[index]);
                fileBuilder.AppendLine(lineOfCounts);
            }

            return fileBuilder.ToString();
        }

        private void MakeHoursGrid()
        {
            #region Initialise arrays
            string[] hour0 = new string[] { "0" };
            string[] hour1 = new string[] { "1"};
            string[] hour2 = new string[] { "2" };
            string[] hour3 = new string[] { "3"};
            string[] hour4 = new string[] { "4" };
            string[] hour5 = new string[] { "5" };
            string[] hour6 = new string[] { "6" };
            string[] hour7 = new string[] { "7" };
            string[] hour8 = new string[] { "8" };
            string[] hour9 = new string[] { "9" };
            string[] hour10 = new string[] { "10"};
            string[] hour11 = new string[] { "11"};
            string[] hour12 = new string[] { "12"};
            string[] hour13 = new string[] { "13"};
            string[] hour14 = new string[] { "14"};
            string[] hour15 = new string[] { "15"};
            string[] hour16 = new string[] { "16"};
            string[] hour17 = new string[] { "17"};
            string[] hour18 = new string[] { "18"};
            string[] hour19 = new string[] { "19"};
            string[] hour20 = new string[] { "20"};
            string[] hour21 = new string[] { "21"};
            string[] hour22 = new string[] { "22"};
            string[] hour23 = new string[] { "23"};
            #endregion

            #region Set array size to 32 elements

            Array.Resize(ref hour0, 32);
            Array.Resize(ref hour1, 32);
            Array.Resize(ref hour2, 32);
            Array.Resize(ref hour3, 32);
            Array.Resize(ref hour4, 32);
            Array.Resize(ref hour5, 32);
            Array.Resize(ref hour6, 32);
            Array.Resize(ref hour7, 32);
            Array.Resize(ref hour8, 32);
            Array.Resize(ref hour9, 32);
            Array.Resize(ref hour10, 32);
            Array.Resize(ref hour11, 32);
            Array.Resize(ref hour12, 32);
            Array.Resize(ref hour13, 32);
            Array.Resize(ref hour14, 32);
            Array.Resize(ref hour15, 32);
            Array.Resize(ref hour16, 32);
            Array.Resize(ref hour17, 32);
            Array.Resize(ref hour18, 32);
            Array.Resize(ref hour19, 32);
            Array.Resize(ref hour20, 32);
            Array.Resize(ref hour21, 32);
            Array.Resize(ref hour22, 32);
            Array.Resize(ref hour23, 32);
            #endregion

            #region List the hours agaisnt dates...
            listOfHoursAcrossDates.Add(hour0);
            listOfHoursAcrossDates.Add(hour1);
            listOfHoursAcrossDates.Add(hour2);
            listOfHoursAcrossDates.Add(hour3);
            listOfHoursAcrossDates.Add(hour4);
            listOfHoursAcrossDates.Add(hour5);
            listOfHoursAcrossDates.Add(hour6);
            listOfHoursAcrossDates.Add(hour7);
            listOfHoursAcrossDates.Add(hour8);
            listOfHoursAcrossDates.Add(hour9);
            listOfHoursAcrossDates.Add(hour10);
            listOfHoursAcrossDates.Add(hour11);
            listOfHoursAcrossDates.Add(hour12);
            listOfHoursAcrossDates.Add(hour13);
            listOfHoursAcrossDates.Add(hour14);
            listOfHoursAcrossDates.Add(hour15);
            listOfHoursAcrossDates.Add(hour16);
            listOfHoursAcrossDates.Add(hour17);
            listOfHoursAcrossDates.Add(hour18);
            listOfHoursAcrossDates.Add(hour19);
            listOfHoursAcrossDates.Add(hour20);
            listOfHoursAcrossDates.Add(hour21);
            listOfHoursAcrossDates.Add(hour22);
            listOfHoursAcrossDates.Add(hour23);
            #endregion
        }
    }
}
