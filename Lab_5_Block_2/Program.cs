using System;
using System.Text;

namespace Block_2
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.OutputEncoding = System.Text.Encoding.Unicode;

            string filePath = "input.txt";
            try
            {
                string[] lines = File.ReadAllLines(filePath, Encoding.UTF8);

                List<string> result = new List<string>();

                foreach(string line in lines)
                {
                    string[] parts = line.Split(',');

                    if (parts.Length == 9)
                    {
                        string lastName = parts[0].Trim();
                        string gender = parts[3].Trim();

                        if (int.TryParse(parts[7].Trim(), out int grade))
                        {
                            if ((gender == "F" || gender == "Ж") && grade == 5)
                            {
                                result.Add(lastName);
                            }
                        }
                    }
                    else
                    {
                        Console.WriteLine($"Неправильний формат рядка");
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