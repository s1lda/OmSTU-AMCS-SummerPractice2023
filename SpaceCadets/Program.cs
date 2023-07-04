using System;
using System.Collections.Generic;
using System.IO;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace SpaceCadets
{
    class Cadet
    {
        public string Name { get; set; }
        public string Group { get; set; }
        public string Discipline { get; set; }
        public int Mark { get; set; }
    }

    class Program
    {
        static void Main(string[] args)
        {
            if (args.Length < 2 || args.Length % 2 != 0)
            {
                Console.WriteLine("Некорректное количество аргументов. Пожалуйста, укажите пути к JSON файлам и соответствующие пути для выходных файлов.");
                return;
            }

            for (int i = 0; i < args.Length; i += 2)
            {
                string inputFile = args[i];
                string outputFile = args[i + 1];

                ProcessFile(inputFile, outputFile);
            }

            Console.WriteLine("Все файлы обработаны.");
        }

        static void ProcessFile(string inputFile, string outputFile)
        {
            try
            {
                string json = File.ReadAllText(inputFile);

                JObject data = JObject.Parse(json);

                string taskName = (string)data["taskName"];
                JArray students = (JArray)data["data"];

                List<Cadet> cadets = new List<Cadet>();

                foreach (JObject student in students)
                {
                    string name = (string)student["name"];
                    string group = (string)student["group"];
                    string discipline = (string)student["discipline"];
                    int mark = (int)student["mark"];

                    Cadet cadet = new Cadet
                    {
                        Name = name,
                        Group = group,
                        Discipline = discipline,
                        Mark = mark
                    };

                    cadets.Add(cadet);
                }

                if (taskName == "GetStudentsWithHighestGPA")
                {
                    var highestGPA = cadets.GroupBy(c => c.Name)
                                          .Select(g => new
                                          {
                                              Cadet = g.Key,
                                              GPA = g.Average(c => c.Mark)
                                          })
                                          .Where(g => g.GPA == cadets.Max(c => c.Mark))
                                          .ToList();

                    JObject highestGpaData = new JObject();
                    highestGpaData["Response"] = JArray.FromObject(highestGPA);

                    File.WriteAllText(outputFile, highestGpaData.ToString());
                    Console.WriteLine("Результаты сохранены в файл: " + outputFile);
                }
                else if (taskName == "CalculateGPAByDiscipline")
                {
                    var averageMarksByDiscipline = cadets.GroupBy(c => c.Discipline)
                                                         .Select(g => new JObject(new JProperty(g.Key, g.Average(c => c.Mark))))
                                                         .ToList();

                    JObject gpaByDisciplineData = new JObject(new JProperty("Response", new JArray(averageMarksByDiscipline)));

                    File.WriteAllText(outputFile, gpaByDisciplineData.ToString());
                    Console.WriteLine("Результаты сохранены в файл: " + outputFile);
                }
                else if (taskName == "GetBestGroupsByDiscipline")
                {
                    var bestGroupByDiscipline = cadets.GroupBy(c => c.Discipline)
                                                      .Select(g => new
                                                      {
                                                          Discipline = g.Key,
                                                          Group = g.GroupBy(c => c.Group)
                                                                    .Select(gg => new
                                                                    {
                                                                        Group = gg.Key,
                                                                        AverageMark = gg.Average(c => c.Mark)
                                                                    })
                                                                    .OrderByDescending(gg => gg.AverageMark)
                                                                    .FirstOrDefault()
                                                      })
                                                      .ToList();

                    JObject bestGroupsByDisciplineData = new JObject();
                    JArray bestGroups = new JArray();

                    foreach (var discipline in bestGroupByDiscipline)
                    {
                        JObject groupObj = new JObject(
                            new JProperty("Discipline", discipline.Discipline),
                            new JProperty("Group", discipline.Group.Group),
                            new JProperty("GPA", discipline.Group.AverageMark)
                        );

                        bestGroups.Add(groupObj);
                    }

                    bestGroupsByDisciplineData["Response"] = bestGroups;

                    File.WriteAllText(outputFile, bestGroupsByDisciplineData.ToString());
                    Console.WriteLine("Результаты сохранены в файл: " + outputFile);
                }
                else
                {
                    Console.WriteLine("Неверное имя задачи: " + taskName);
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("Ошибка при обработке файла " + inputFile + ": " + e.Message);
            }
        }
    }
}