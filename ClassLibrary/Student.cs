using System;

namespace ClassLibrary
{
    public class Student : IComparable<Student>
    {
        public int StudentID { get; set; }
        public string FullName { get; set; }
        public int YearOfBirth { get; set; }
        public int GroupNumber { get; set; }
        public int CourseLevel { get; set; } // Add CourseLevel property

        public Student(int studentID, string fullName, int yearOfBirth, int groupNumber)
        {
            StudentID = studentID;
            FullName = fullName;
            YearOfBirth = yearOfBirth;
            GroupNumber = groupNumber;
            CourseLevel = 1; // Initialize the course level to 1
        }

        public Student(int studentID, string fullName, int yearOfBirth, int groupNumber, int courseLevel)
        {
            StudentID = studentID;
            FullName = fullName;
            YearOfBirth = yearOfBirth;
            GroupNumber = groupNumber;
            CourseLevel = courseLevel;
        }

        public void TransferToNextCourse()
        {
            CourseLevel++; // Increment the course level
        }

        public int CalculateAge()
        {
            int currentYear = DateTime.Now.Year;
            int age = currentYear - YearOfBirth;
            return age;
        }

        public override string ToString()
        {
            return $"Student ID: {StudentID}, Full Name: {FullName}, Year of Birth: {YearOfBirth}, Group Number: {GroupNumber}, Course Level: {CourseLevel}";
        }

        public int CompareTo(Student? other)
        {
            if (other == null)
            {
                return 1;
            }
            return StudentID.CompareTo(other.StudentID);
        }
    }
}