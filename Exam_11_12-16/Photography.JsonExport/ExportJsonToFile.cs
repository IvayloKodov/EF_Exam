namespace Photography.JsonExport
{
    using System.IO;
    using Newtonsoft.Json;

    public class ExportJsonToFile
    {
        public static string ExportQueryToFile<T>(T query, string filepath)
        {
            var json = JsonConvert.SerializeObject(query, Formatting.Indented);
            File.WriteAllText(filepath, json);

            return json;
        }
    }
}