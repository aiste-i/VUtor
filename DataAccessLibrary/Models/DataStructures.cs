using Microsoft.IdentityModel.Tokens;
using System.Globalization;

namespace DataAccessLibrary.Models
{
    public record profileCreationDate
    {
        DateTime Date;

        public profileCreationDate()
        {
            CultureInfo culture = new CultureInfo("en-US");
            Thread.CurrentThread.CurrentCulture = culture;
            Date = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day);
        }

        public profileCreationDate(string dateAsString)
        {
            if (!dateAsString.IsNullOrEmpty())
            {
                Date = DateTime.ParseExact(dateAsString, "MM/dd/yyyy", CultureInfo.CurrentCulture);
            }
            else
            {
                Date = DateTime.MinValue;
            }
        }

        public override string ToString()
        {
            CultureInfo culture = new CultureInfo("en-US");
            Thread.CurrentThread.CurrentCulture = culture;
            return Date.ToShortDateString();
        }
    }
    public enum CourseName : int
    {
        None = 0,
        PS = 1,
        ISI = 2,
        VDA = 3,
        MMT = 4,
        IT = 5,
        FDM = 6,
        DM = 7
    }
    public enum CourseYear : int
    {
        None = 0,
        I = 1,
        II = 2,
        III = 3,
        IV = 4
    }
    public struct CourseData : IEquatable<CourseData>
    {
        public int CourseName { get; set; }
        public int CourseYear { get; set; }
        public CourseData()
        {
            CourseName = 0;
            CourseYear = 0;
        }
        public CourseData(int courseName, int courseYear)
        {
            this.CourseName = courseName;
            this.CourseYear = courseYear;
        }
        public CourseData(string conversionString)
        {
            if (!conversionString.IsNullOrEmpty())
            {
                string[] values = conversionString.Split(' ');
                CourseName = int.Parse(values[0]);
                CourseYear = int.Parse(values[1]);
            }
            else
            {
                CourseName = 0;
                CourseYear = 0;
            }
        }

        public string GetName()
        {
            var nameEnum = (CourseName)CourseName;

            return nameEnum.ToString();
        }

        public string GetYear()
        {
            var yearEnum = (CourseYear)CourseYear;

            return yearEnum.ToString();
        }

        public override string ToString()
        {
            return CourseName + " " + CourseYear;
        }

        // IEquatable 
        public bool Equals(CourseData other)
        {
            if (CourseName == other.CourseName && CourseYear == other.CourseYear)
                return true;
            else return false;
        }
    }
}