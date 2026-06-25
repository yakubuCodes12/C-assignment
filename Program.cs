using System;

namespace StudentResultsProcessingSystem
{
    class Program
    {
        // Student data arrays
        static string[] names = new string[3];
        static string[] ids = new string[3];
        static string[] programmes = new string[3];
        static string[] levels = new string[3];
        static double[,] scores = new double[3, 5];
        static double[] totals = new double[3];
        static double[] averages = new double[3];
        static string[] grades = new string[3];
        static string[] statuses = new string[3];
        static bool dataEntered = false;

        static string[] courses = {
            "Programming with C#",
            "Database Systems",
            "Computer Networks",
            "Web Development",
            "Mathematics for Computing"
        };

        static void Main(string[] args)
        {
            int choice;

            do
            {
                Console.WriteLine("\n===== STUDENT RESULTS PROCESSING SYSTEM =====");
                Console.WriteLine("1. Enter Student Results");
                Console.WriteLine("2. View Student Report");
                Console.WriteLine("3. Exit");
                Console.Write("Choose an option: ");

                while (!int.TryParse(Console.ReadLine(), out choice) || choice < 1 || choice > 3)
                {
                    Console.Write("Invalid option. Enter 1, 2, or 3: ");
                }

                switch (choice)
                {
                    case 1:
                        EnterStudentResults();
                        break;
                    case 2:
                        ViewStudentReport();
                        break;
                    case 3:
                        Console.WriteLine("\nThank you for using the Student Results Processing System.");
                        break;
                }

            } while (choice != 3);
        }

        static void EnterStudentResults()
        {
            for (int i = 0; i < 3; i++)
            {
                Console.WriteLine($"\nEnter details for Student {i + 1}");

                Console.Write("Enter full name: ");
                names[i] = Console.ReadLine();

                Console.Write("Enter student ID: ");
                ids[i] = Console.ReadLine();

                Console.Write("Enter programme: ");
                programmes[i] = Console.ReadLine();

                Console.Write("Enter level: ");
                levels[i] = Console.ReadLine();

                double total = 0;

                for (int j = 0; j < 5; j++)
                {
                    double score;
                    bool valid = false;

                    do
                    {
                        Console.Write($"Enter score for {courses[j]}: ");
                        if (double.TryParse(Console.ReadLine(), out score) && score >= 0 && score <= 100)
                        {
                            valid = true;
                        }
                        else
                        {
                            Console.WriteLine("Invalid score. Score must be between 0 and 100.");
                        }
                    } while (!valid);

                    scores[i, j] = score;
                    total += score;
                }

                totals[i] = total;
                averages[i] = total / 5.0;
                grades[i] = GetGrade(averages[i]);
                statuses[i] = averages[i] >= 50 ? "Passed" : "Failed";
            }

            dataEntered = true;
            Console.WriteLine("\nStudent results entered successfully!");
        }

        static void ViewStudentReport()
        {
            if (!dataEntered)
            {
                Console.WriteLine("\nNo data entered yet. Please select Option 1 first.");
                return;
            }

            Console.WriteLine("\n========== STUDENT RESULTS REPORT ==========");

            for (int i = 0; i < 3; i++)
            {
                Console.WriteLine("\n---------------------------------------------");
                Console.WriteLine($"Student Name    : {names[i]}");
                Console.WriteLine($"Student ID      : {ids[i]}");
                Console.WriteLine($"Programme       : {programmes[i]}");
                Console.WriteLine($"Level           : {levels[i]}");
                Console.WriteLine("---------------------------------------------");

                for (int j = 0; j < 5; j++)
                {
                    Console.WriteLine($"{courses[j],-30}: {scores[i, j]}");
                }

                Console.WriteLine("---------------------------------------------");
                Console.WriteLine($"{"Total Score",-30}: {totals[i]}");
                Console.WriteLine($"{"Average Score",-30}: {averages[i]:F2}");
                Console.WriteLine($"{"Grade",-30}: {grades[i]}");
                Console.WriteLine($"{"Status",-30}: {statuses[i]}");
            }

            // Bonus: Best student
            int bestIndex = 0;
            int worstIndex = 0;
            double classTotal = 0;

            for (int i = 1; i < 3; i++)
            {
                if (averages[i] > averages[bestIndex]) bestIndex = i;
                if (averages[i] < averages[worstIndex]) worstIndex = i;
                classTotal += averages[i];
            }
            classTotal += averages[0];

            Console.WriteLine("\n========== CLASS SUMMARY ===================");
            Console.WriteLine($"Best Student    : {names[bestIndex]} (Avg: {averages[bestIndex]:F2})");
            Console.WriteLine($"Lowest Student  : {names[worstIndex]} (Avg: {averages[worstIndex]:F2})");
            Console.WriteLine($"Class Average   : {classTotal / 3:F2}");
            Console.WriteLine("=============================================");
        }

        static string GetGrade(double average)
        {
            if (average >= 80) return "A";
            else if (average >= 70) return "B";
            else if (average >= 60) return "C";
            else if (average >= 50) return "D";
            else return "F";
        }
    }
}

