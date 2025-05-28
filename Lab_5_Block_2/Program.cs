using System;
using System.Text;

namespace Block_2
{
    struct Student
    {
        public string LastName;
        public string Gender;
        public int InformaticsGrade;

        public Student(string lastName, string gender, int informaticsGrade)
        {
            LastName = lastName;
            Gender = gender;
            InformaticsGrade = informaticsGrade;
        }

        public static bool TryParseFromCsvLine(string csvLine, out Student student)
        {
            student = default; 
            string[] parts = csvLine.Trim().Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);

            if (parts.Length != 9)
            {
                return false;
            }

            string lastName = parts[0].Trim();
            string gender = parts[3].Trim();

            if (int.TryParse(parts[7].Trim(), out int grade))
            {
                student = new Student(lastName, gender, grade);
                return true;
            }
            else
            {
                return false;
            }
        }
    }
    class Program
    {
        static void Main(string[] args)
        {
            Console.OutputEncoding = System.Text.Encoding.Unicode;

            string filePath = "input.txt";
            try
            {
                string[] lines = File.ReadAllLines(filePath, Encoding.UTF8);

                List<Student> allStudents = new List<Student>();

                foreach (string line in lines)
                {
                    if (Student.TryParseFromCsvLine(line, out Student student))
                    {
                        allStudents.Add(student);
                    }
                    else
                    {
                        Console.WriteLine($"Неправильний формат рядка або помилка парсингу даних: \"{line}\"");
                    }
                }

                List<string> result = new List<string>();

                foreach (Student student in allStudents)
                {
                    if ((student.Gender == "F" || student.Gender == "Ж") && student.InformaticsGrade == 5)
                    {
                        result.Add(student.LastName);
                    }
                }

                if (result.Count > 0)
                {
                    Console.WriteLine("Прізвища студенток жіночої статі, що мають \"5\" з інформатики.");
                    foreach (string lastName in result)
                    {
                        Console.WriteLine(lastName);
                    }
                }
                else
                {
                    Console.WriteLine("Не знайдено студенток жіночої статі з оцінкою \"5\" з інформатики.");
                }
            }
            catch (FileNotFoundException)
            {
                Console.WriteLine($"Файл '{filePath}' не знайдено.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Виникла помилка: {ex.Message}");
            }
        }
    }
}