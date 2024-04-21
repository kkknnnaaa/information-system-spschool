using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json;
using System.Text.Unicode;
using System.IO;
using System.Text.Encodings.Web;

namespace spschool
{
    public class ListSportsmanJson
    {
        public List<Sportsman> bd;

        public ListSportsmanJson(ListSportsmanJson sportsmans = null)
        {
            if (sportsmans != null)
            {
                bd = sportsmans.bd;
            }
            else
            {
                bd = new List<Sportsman>();
            }
        }
        public string WriteToFile(string fileName)
        {
            string result = "";

            try
            {
                var options = new JsonSerializerOptions()
                {
                    Encoder = JavaScriptEncoder.Create(UnicodeRanges.BasicLatin, UnicodeRanges.LetterlikeSymbols, UnicodeRanges.Cyrillic),
                    WriteIndented = true
                };
                string json = JsonSerializer.Serialize(bd, options);
                File.WriteAllText(fileName, json, Encoding.UTF8);
            }
            catch (Exception er)
            {
                result = er.Message;
            }

            return result;
        }

        public string ReadFromFile(string fileName)
        {
            string result = "";
            try
            {
                bd.Clear();
                string jsonString = File.ReadAllText(fileName, Encoding.UTF8);
                bd = JsonSerializer.Deserialize<List<Sportsman>>(jsonString);
            }
            catch (Exception er)
            {
                result = er.Message;
            }
            return result;
        }
    }
}
