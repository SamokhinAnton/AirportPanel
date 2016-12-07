using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AirportPanel
{
    public static class FileHelper
    {
        public static void WriteLines(string path, string[] lines, string fileFieldsName)
        {
            using (StreamWriter sw = new StreamWriter(path, false, Encoding.Default))
            {
                sw.WriteLine(fileFieldsName);
                foreach (var line in lines)
                {
                    sw.WriteLine(line);
                }
            }
        }

        public static string[] ReadLines(string path)
        {
            var allLines = File.ReadAllLines(path, Encoding.Default).Where(l => !string.IsNullOrEmpty(l)).ToArray();
            var fileFieldsName = allLines[0].Split('|');
            var content = allLines.Where(l => !string.IsNullOrEmpty(l)).Skip(1).ToArray();
            return allLines;
        }
    }
}
