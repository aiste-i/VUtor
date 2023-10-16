﻿using Microsoft.IdentityModel.Tokens;
using System.Data;
using System.Globalization;
using System.Reflection.Emit;

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
        public int courseName { get; set; }
        public int courseYear { get; set; }
        public CourseData()
        {
            courseName = 0;
            courseYear = 0;
        }
        public CourseData(int courseName, int courseYear)
        {
            this.courseName = courseName;
            this.courseYear = courseYear;
        }
        public CourseData(string conversionString)
        {
            if (!conversionString.IsNullOrEmpty())
            {
                string[] values = conversionString.Split(' ');
                courseName = int.Parse(values[0]);
                courseYear = int.Parse(values[1]);
            }
            else
            {
                courseName = 0;
                courseYear = 0;
            }
        }

        public string GetName()
        {
            var nameEnum = (CourseName)courseName;

            return nameEnum.ToString();
        }

        public string GetYear()
        {
            var yearEnum = (CourseYear)courseYear;

            return yearEnum.ToString();
        }

        public int[] ToArray()
        {
            return [courseName, courseYear];
        }

        public override string ToString()
        {
            return courseName + " " + courseYear;
        }

        // IEquatable 
        public bool Equals(CourseData other)
        {
            if (courseName == other.courseName && courseYear == other.courseYear)
                return true;
            else return false;
        }
    }
}