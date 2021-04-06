using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;
using Newtonsoft.Json;

namespace WeatherForecast
{
    class Output
    {
		private static void PrintItem(XmlElement item, int indent = 0)
		{
			Console.Write($"{new string('\t', indent)}{item.LocalName} ");

			foreach (XmlAttribute attr in item.Attributes)
			{
				Console.Write($"[{attr.InnerText}]");
			}

			foreach (var child in item.ChildNodes)
			{
				if (child is XmlElement node)
				{
					Console.WriteLine();
					PrintItem(node, indent + 1);
				}

				if (child is XmlText text)
				{
					Console.Write($"- {text.InnerText}");
				}
			}
		}

		public static void XML(string result)
		{
			XmlDocument xml = new XmlDocument();
			try
			{
				xml.LoadXml(result);
			}
			catch (Exception e)
			{
				Console.WriteLine(e);
			}
			var root = xml.DocumentElement;
			Console.ForegroundColor = ConsoleColor.DarkYellow;
			Console.WriteLine($"\nXML----------------------------------");
			PrintItem(root);
			Console.WriteLine("");
			Console.ResetColor();
		}

		public static void JSON (string result)
		{
			var response = Newtonsoft.Json.Linq.JObject.Parse(result);
			Console.ForegroundColor = ConsoleColor.DarkYellow;
			Console.WriteLine($"\nJSON---------------------------------");
			Console.WriteLine($"Id: {response["id"]}");
			Console.WriteLine($"Name: {response["name"]}");
			Console.WriteLine($"Country: {response["sys"]["country"]}");
			Console.WriteLine($"Temperature: {response["main"]["temp"]}");
			Console.WriteLine($"Humidity: {response["main"]["humidity"]}");
			Console.WriteLine($"Pressure: {response["main"]["pressure"]}");
			Console.WriteLine($"Wind speed: {response["wind"]["speed"]}");
			Console.WriteLine($"Clouds all: {response["clouds"]["all"]}");
			Console.WriteLine($"Weather description: {response["weather"][0]["description"]}");
			Console.WriteLine("");
			Console.ResetColor();
		}
	}
}
