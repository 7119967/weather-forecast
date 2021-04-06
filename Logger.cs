using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace WeatherForecast
{
    class Logger
    {
        public static void RecordResponse(string result)
        {
            try
            {
                using (StreamWriter sw = new StreamWriter(Environment.CurrentDirectory + "\\Log.txt", true, System.Text.Encoding.Default))
                {
                    sw.WriteLine($"Запись добавлена: {DateTime.Now}");
                    sw.WriteLine(result);
                    sw.WriteLine("\n");
                }
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("\nЗапись лог-файла выполнена");
                Console.ResetColor();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }
    }
}
