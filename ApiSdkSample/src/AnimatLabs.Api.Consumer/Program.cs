using AnimatLabs.Api.Consumer;
using AnimatLabs.Api.Models;
using AnimatLabs.Api.Sdk;
using Refit;
using System.Diagnostics.CodeAnalysis;

[ExcludeFromCodeCoverage]
internal class Program
{
    private static async Task Main(string[] args)
    {
        var serviceClient = RestService.For<IStudentsService>("https://localhost:7128/api/");
        var studentsService = new StudentsServiceConsumer(serviceClient);
        bool exit = false;

        while (!exit)
        {
            DisplayMenu();
            int choice = GetMenuChoice();

            switch (choice)
            {
                case 1:
                    await ReadAllStudents(studentsService);
                    break;

                case 2:
                    await CreateStudent(studentsService);
                    break;

                case 3:
                    await ReadStudent(studentsService);
                    break;

                case 4:
                    await UpdateStudent(studentsService);
                    break;

                case 5:
                    await DeleteStudent(studentsService);
                    break;

                case 6:
                    exit = true;
                    break;

                default:
                    Console.WriteLine("Invalid choice. Try again.");
                    break;
            }

            Console.WriteLine();
        }
    }

    private static void DisplayMenu()
    {
        Console.WriteLine("Student Menu");
        Console.WriteLine("1. Read All Students");
        Console.WriteLine("2. Create Student");
        Console.WriteLine("3. Read Student");
        Console.WriteLine("4. Update Student");
        Console.WriteLine("5. Delete Student");
        Console.WriteLine("6. Exit");
        Console.Write("Enter your choice: ");
    }

    private static int GetMenuChoice()
    {
        int choice;
        while (!int.TryParse(Console.ReadLine(), out choice))
        {
            Console.WriteLine("Invalid input. Please enter a valid number.");
        }

        return choice;
    }

    private static async Task ReadAllStudents(StudentsServiceConsumer studentsService)
    {
        var students = await studentsService.ReadAllStudents();
        if (students != null)
        {
            foreach (var student in students)
            {
                Console.WriteLine($"{student.Id} - {student.FirstName} {student.LastName}");
            }
        }
        else
        {
            Console.WriteLine("No students found.");
        }
    }

    private static async Task CreateStudent(StudentsServiceConsumer studentsService)
    {
        Console.Write("Enter the first name: ");
        var firstName = Console.ReadLine();
        Console.Write("Enter the last name: ");
        var lastName = Console.ReadLine();
        var newStudent = new Student { Id = Guid.NewGuid(), FirstName = firstName, LastName = lastName };
        var createdId = await studentsService.CreateStudent(newStudent);
        Console.WriteLine($"New student created with ID: {createdId}");
    }

    private static async Task ReadStudent(StudentsServiceConsumer studentsService)
    {
        Console.Write("Enter the student ID: ");
        if (Guid.TryParse(Console.ReadLine(), out Guid id))
        {
            var student = await studentsService.ReadStudent(id);
            if (student != null)
            {
                Console.WriteLine($"Student ID: {student.Id}");
                Console.WriteLine($"First Name: {student.FirstName}");
                Console.WriteLine($"Last Name: {student.LastName}");
            }
            else
            {
                Console.WriteLine("Student not found.");
            }
        }
        else
        {
            Console.WriteLine("Invalid student ID.");
        }
    }

    private static async Task UpdateStudent(StudentsServiceConsumer studentsService)
    {
        Console.Write("Enter the student ID to update: ");
        if (Guid.TryParse(Console.ReadLine(), out Guid updateId))
        {
            var updatedStudent = await studentsService.ReadStudent(updateId);
            if (updatedStudent != null)
            {
                Console.Write("Enter the new first name: ");
                updatedStudent.FirstName = Console.ReadLine();
                Console.Write("Enter the new last name: ");
                updatedStudent.LastName = Console.ReadLine();
                var isUpdated = await studentsService.UpdateStudent(updateId, updatedStudent);
                if (isUpdated)
                {
                    Console.WriteLine("Student updated successfully.");
                }
                else
                {
                    Console.WriteLine("Failed to update student.");
                }
            }
            else
            {
                Console.WriteLine("Student not found.");
            }
        }
        else
        {
            Console.WriteLine("Invalid student ID.");
        }
    }

    private static async Task DeleteStudent(StudentsServiceConsumer studentsService)
    {
        Console.Write("Enter the student ID to delete: ");
        if (Guid.TryParse(Console.ReadLine(), out Guid deleteId))
        {
            var isDeleted = await studentsService.DeleteStudent(deleteId);
            if (isDeleted)
            {
                Console.WriteLine("Student deleted successfully.");
            }
            else
            {
                Console.WriteLine("Failed to delete student.");
            }
        }
        else
        {
            Console.WriteLine("Invalid student ID.");
        }
    }
}