using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Reflection;
using System.Text;

namespace classes_expert.Services
{
    public static class BlobManipulator
    {

        public static List<string[]> csvReader<T>(string Url, char split = ';')
        {
            string url = Url;

            url = url.Replace("/", ".");
            url = url.Replace("\\", ".");

            List<string[]> lstlines = new List<string[]>();
            string resourceId = $"{Assembly.GetExecutingAssembly().GetName().Name.Replace(" ", "_")}.{url}";
            var assembly = typeof(T).GetTypeInfo().Assembly;
            Stream stream = assembly.GetManifestResourceStream(resourceId);

            using (var reader = new StreamReader(stream))
            {
                reader.ReadLine();
                string line = reader.ReadLine();
                while (line != null)
                {

                    try
                    {
                        string[] LineParts = line.Split(split);

                        lstlines.Add(LineParts);

                    }
                    catch (Exception ex)
                    {

                        Debug.WriteLine($"Exception in line {line} -> {ex}");
                    }
                    line = reader.ReadLine();
                }
            }

            return lstlines;
        }

        public static bool csvWriteLine(string Url, char split = ';', string line ="")
        {
            string url = Url;

            url = url.Replace("/", ".");
            url = url.Replace("\\", ".");

            List<string[]> lstlines = new List<string[]>();
            string resourceId = $"{Assembly.GetExecutingAssembly().GetName().Name.Replace(" ", "_")}.{url}";
            var assembly = typeof(object).GetTypeInfo().Assembly;
            Stream stream = assembly.GetManifestResourceStream(resourceId);

            using (var reader = new StreamWriter(stream))
            {
                reader.WriteLine(line);
            }
            return true;
        }

        public static bool csvWrite(string Url, char split = ';', string body = "")
        {
            string url = Url;

            url = url.Replace("/", ".");
            url = url.Replace("\\", ".");

            List<string[]> lstlines = new List<string[]>();
            string resourceId = $"{Assembly.GetExecutingAssembly().GetName().Name.Replace(" ", "_")}.{url}";
            var assembly = typeof(object).GetTypeInfo().Assembly;
            Stream stream = assembly.GetManifestResourceStream(resourceId);

            using (var reader = new StreamWriter(stream))
            {
                var lines = body.Split(new string[] { "/n" }, StringSplitOptions.None);
                foreach(string line in lines)
                {
                    reader.WriteLine(line);
                }
                
            }
            return true;
        }
    }
}
