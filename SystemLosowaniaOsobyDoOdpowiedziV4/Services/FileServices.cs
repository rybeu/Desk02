using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SystemLosowaniaOsobyDoOdpowiedziV4.Models;

namespace SystemLosowaniaOsobyDoOdpowiedziV4.Services
{
    public class FileService
    {
        private string basePath = @"C:\Users\Hanna\source\repos\SystemLosowaniaOsobyDoOdpowiedziV4\Classes";

        public FileService()
        {
            if (!Directory.Exists(basePath))
                Directory.CreateDirectory(basePath);
        }

        public void SaveClass(SchoolClass schoolClass)
        {
            string path = Path.Combine(basePath, schoolClass.ClassName + ".txt");

            StringBuilder sb = new StringBuilder();

            foreach (var student in schoolClass.Students)
            {
                sb.AppendLine($"{student.Number};{student.Name}");
            }

            File.WriteAllText(path, sb.ToString());
        }

        public SchoolClass LoadClass(string className)
        {
            string path = Path.Combine(basePath, className + ".txt");

            SchoolClass schoolClass = new SchoolClass();
            schoolClass.ClassName = className;

            if (!File.Exists(path))
                return schoolClass;

            var lines = File.ReadAllLines(path);

            foreach (var line in lines)
            {
                var parts = line.Split(';');

                schoolClass.Students.Add(new Students
                {
                    Number = int.Parse(parts[0]),
                    Name = parts[1]
                });
            }

            return schoolClass;
        }

        public List<string> GetClasses()
        {
            return Directory.GetFiles(basePath, "*.txt")
                .Select(f => Path.GetFileNameWithoutExtension(f))
                .ToList();
        }

        public bool ClassExists(string className)
        {
            string path = Path.Combine(basePath, className + ".txt");
            return File.Exists(path);
        }
    }
}