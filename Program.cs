using Newtonsoft.Json;
using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace WeatherForecast
{
    class Program
    {
		private const string APP_PATH = "http://api.openweathermap.org";
		private static string apiKEY;
		private static string city;
		private static string units;
		private static string mode;

		static void Main(string[] args)
        {
			Console.ForegroundColor = ConsoleColor.Green;
			Console.WriteLine("Сервис для получения прогноза погоды");
			Console.WriteLine("_____________________________________");
			Console.ResetColor();

			try
			{
				while (true)
				{
					if (String.IsNullOrEmpty(apiKEY))
					{
						Console.ForegroundColor = ConsoleColor.DarkGray;
						Console.WriteLine("\nВведите свой персональный ключ (API KEY) для использовать API сервиса https://openweathermap.org:");
						Console.ResetColor();
						apiKEY = Console.ReadLine();
					}

					do
					{
						Console.ForegroundColor = ConsoleColor.DarkGray;
						Console.WriteLine("\nВведите название города на английском (например London):");
						Console.ResetColor();
						city = Console.ReadLine();
					} while (String.IsNullOrEmpty(city));

					try
                    {
						Console.ForegroundColor = ConsoleColor.DarkGray;
						Console.WriteLine("\nДля вывода единиц измерения введите цифру выбранного варианта:");
						Console.WriteLine("1 metric");
						Console.WriteLine("2 imperial");
						Console.ResetColor();
						int intUnits = Convert.ToInt32(Console.ReadLine());
						units = intUnits == 1 ? "metric" : "imperial";
					}
					catch (Exception e)
					{
						Console.WriteLine(e);
					}

					try
					{
						Console.ForegroundColor = ConsoleColor.DarkGray;
						Console.WriteLine("\nДля вывода формата ответа введите цифру выбранного варианта:");
						Console.WriteLine("1 json");
						Console.WriteLine("2 xml");
						Console.ResetColor();
						int intMode = Convert.ToInt32(Console.ReadLine());
						mode = intMode == 1 ? "json" : "xml";
					}
					catch (Exception e)
					{
						Console.WriteLine(e);
					}


					string result = GetValues(apiKEY, city.ToLower(), units, mode);
                    switch (mode)
                    {
						case "json":
							Output.JSON(result);
							Logger.RecordResponse(result);
							break;
						case "xml":
							Output.XML(result);
							Logger.RecordResponse(result);
							break;
						default:
                            break;
                    }

					//city = "";
					units = "";
					mode = "";

					Console.ForegroundColor = ConsoleColor.DarkGray;
					Console.WriteLine("\n_____________________________________");
					Console.WriteLine("\nДля выхода из программы нажмите CTRL+C или нажмите Enter отправьте новый запрос\n");
					Console.ResetColor();
					Console.Read();
				}
			}
			catch (Exception e)
			{
				Console.WriteLine(e);
			}
			finally
			{
				Console.ForegroundColor = ConsoleColor.Red;
				Console.WriteLine("Программа аварийно завершилась");
				Console.ResetColor();
				Console.Read();
			}
		}

		static string GetValues(string apiKEY, string city, string units, string mode)
		{
			using (var client = new HttpClient())
			{
				var response = client.GetAsync(APP_PATH + $"/data/2.5/weather?q={city}&units={units}&mode={mode}&appid={apiKEY}").Result;
				string result = response.Content.ReadAsStringAsync().Result;
				return result;
			}
		}
	}
}
