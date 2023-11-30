using System;
using System.Collections.Generic;
using System.Linq;
using ClassLibrary;
using Tree;

namespace StudentDemo
{
    public class Program
    {
        static List<Student> studentList = new List<Student>();
        static Student[] studentArray = new Student[3];
        static BinaryTree<Student> studentTree = new BinaryTree<Student>();

        static void Main(string[] args)
        {
            bool exit = false;

            while (!exit)
            {
                Console.WriteLine("Choose an operation:");
                Console.WriteLine("1. Add Student");
                Console.WriteLine("2. Remove Student");
                Console.WriteLine("3. Update Student");
                Console.WriteLine("4. Find Student");
                Console.WriteLine("5. Traverse Students (Binary Tree)");
                Console.WriteLine("6. Traverse Students (Simple Array)");
                Console.WriteLine("7. Exit");

                string choice = Console.ReadLine();

                try
                {
                    switch (choice)
                    {
                        case "1":
                            // Add Student
                            Console.WriteLine("Adding a new student...");
                            AddStudent();
                            break;
                        case "2":
                            // Remove Student
                            Console.WriteLine("Removing a student...");
                            RemoveStudent();
                            break;
                        case "3":
                            // Update Student
                            Console.WriteLine("Updating a student...");
                            UpdateStudent();
                            break;
                        case "4":
                            // Find Student
                            Console.WriteLine("Finding a student...");
                            FindStudent();
                            break;
                        case "5":
                            // Traverse Students (Binary Tree)
                            Console.WriteLine("Traversing students (Binary Tree):");
                            foreach (Student student in studentTree)
                            {
                                Console.WriteLine(student);
                            }
                            break;
                        case "6":
                            // Traverse Students (Simple Array)
                            Console.WriteLine("Traversing students (Simple Array):");
                            foreach (Student student in studentArray)
                            {
                                if (student != null)
                                {
                                    Console.WriteLine(student);
                                }
                            }
                            break;
                        case "7":
                            // Exit
                            exit = true;
                            break;
                        default:
                            Console.WriteLine("Invalid choice. Please enter a valid option.");
                            break;
                    }
                }
                catch (Exception e)
                {
                    Console.WriteLine($"Error: {e.Message}");
                }
            }
        }

        static void AddStudent()
        {
            Console.Write("Enter Student ID (4-7 digits): ");
            int id = ValidateStudentId(Console.ReadLine());

            Console.Write("Enter Full Name (letters and spaces only): ");
            string name = ValidateNameWithSpaces(Console.ReadLine());

            Console.Write("Enter Year of Birth (>= 1990): ");
            int yearOfBirth = ValidateYearOfBirth(Console.ReadLine());

            Console.Write("Enter Group Number (2-3 digits): ");
            int groupNumber = ValidateGroupNumber(Console.ReadLine());

            Console.Write("Enter Course Level (1-10): ");
            int courseLevel = ValidateCourseLevel(Console.ReadLine());

            Student newStudent = new Student(id, name, yearOfBirth, groupNumber, courseLevel);
            studentList.Add(newStudent);
            studentTree.Insert(newStudent); // Add student to the binary tree
            AddStudentToArray(newStudent); // Add student to the simple array
            Console.WriteLine("Student added successfully!");
        }

        static void RemoveStudent()
        {
            Console.Write("Enter Student ID to remove (4-7 digits): ");
            int id = ValidateStudentId(Console.ReadLine());

            Student studentToRemove = studentList.Find(s => s.StudentID == id);

            if (studentToRemove != null)
            {
                studentList.Remove(studentToRemove);
                studentTree.Remove(studentToRemove); // Remove student from the binary tree
                RemoveStudentFromArray(studentToRemove); // Remove student from the simple array
                Console.WriteLine("Student removed successfully!");
            }
            else
            {
                throw new Exception("Student not found.");
            }
        }

        static void UpdateStudent()
        {
            Console.Write("Enter Student ID to update (4-7 digits): ");
            int id = ValidateStudentId(Console.ReadLine());

            Student studentToUpdate = studentList.Find(s => s.StudentID == id);

            if (studentToUpdate != null)
            {
                Console.Write("Enter Full Name (letters and spaces only): ");
                string name = ValidateNameWithSpaces(Console.ReadLine());

                Console.Write("Enter Year of Birth (>= 1990): ");
                int yearOfBirth = ValidateYearOfBirth(Console.ReadLine());

                Console.Write("Enter Group Number (2-3 digits): ");
                int groupNumber = ValidateGroupNumber(Console.ReadLine());

                Console.Write("Enter Course Level (1-10): ");
                int courseLevel = ValidateCourseLevel(Console.ReadLine());

                studentList.Remove(studentToUpdate);
                studentTree.Remove(studentToUpdate); // Remove the old student from the binary tree

                Student updatedStudent = new Student(id, name, yearOfBirth, groupNumber, courseLevel);
                studentList.Add(updatedStudent);
                studentTree.Insert(updatedStudent); // Insert the updated student into the binary tree
                UpdateStudentInArray(updatedStudent); // Update the student in the simple array

                Console.WriteLine("Student updated successfully!");
            }
            else
            {
                throw new Exception("Student not found.");
            }
        }

        static void FindStudent()
        {
            Console.Write("Enter Student ID to find (4-7 digits): ");
            int id = ValidateStudentId(Console.ReadLine());

            Student studentToFind = studentList.Find(s => s.StudentID == id);

            if (studentToFind != null)
            {
                Console.WriteLine($"Found Student: {studentToFind}");
            }
            else
            {
                throw new Exception("Student not found.");
            }
        }

        static int ValidateStudentId(string input)
        {
            if (int.TryParse(input, out int id) && input.Length >= 4 && input.Length <= 7)
            {
                return id;
            }
            throw new Exception("Student ID must be a number between 4-7 digits.");
        }

        static string ValidateNameWithSpaces(string input)
        {
            if (string.IsNullOrWhiteSpace(input) || !input.All(c => char.IsLetter(c) || char.IsWhiteSpace(c)))
            {
                throw new Exception("Invalid name format. Please enter letters and spaces only.");
            }

            return input;
        }

        static int ValidateYearOfBirth(string input)
        {
            if (int.TryParse(input, out int yearOfBirth) && yearOfBirth >= 1990)
            {
                return yearOfBirth;
            }
            throw new Exception("Year of Birth must be a number and greater than or equal to 1990.");
        }

        static int ValidateGroupNumber(string input)
        {
            if (int.TryParse(input, out int groupNumber) && input.Length >= 2 && input.Length <= 3)
            {
                return groupNumber;
            }
            throw new Exception("Group Number must be a number between 2-3 digits.");
        }
        
        static int ValidateCourseLevel(string input)
        {
            if (int.TryParse(input, out int courseLevel) && courseLevel >= 1 && courseLevel <= 10)
            {
                return courseLevel;
            }
            throw new Exception("Course Level must be a number between 1 and 10.");
        }

        static void AddStudentToArray(Student student)
        {
            for (int i = 0; i < studentArray.Length; i++)
            {
                if (studentArray[i] == null)
                {
                    studentArray[i] = student;
                    return; 
                }
            }
        }

        static void RemoveStudentFromArray(Student student)
        {
            for (int i = 0; i < studentArray.Length; i++)
            {
                if (studentArray[i] == student)
                {
                    studentArray[i] = null;
                    return; 
                }
            }
        }

        static void UpdateStudentInArray(Student updatedStudent)
        {
            for (int i = 0; i < studentArray.Length; i++)
            {
                if (studentArray[i] != null && studentArray[i].StudentID == updatedStudent.StudentID)
                {
                    studentArray[i] = updatedStudent;
                    return; 
                }
            }
        }
    }
}
