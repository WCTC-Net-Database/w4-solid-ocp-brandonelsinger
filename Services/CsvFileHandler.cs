using W4_assignment_template.Interfaces;
using W4_assignment_template.Models;

namespace W4_assignment_template.Services;

// Handles reading and writing character data in CSV format
// Implements the IFileHandler interface
public class CsvFileHandler : IFileHandler
{
    public List<Character> ReadCharacters(string filePath)
    {
        var characters = new List<Character>();
        var lines = File.ReadAllLines(filePath);

        for (int i = 1; i < lines.Length; i++)
        {
            string line = lines[i];
            if (string.IsNullOrWhiteSpace(line)) continue;

            string name;
            int commaIndex;
            string[] fields;

            if (line.StartsWith("\""))
            {
                int closingQuoteIndex = line.IndexOf("\"", 1);
                name = line.Substring(1, closingQuoteIndex - 1);
                commaIndex = line.IndexOf(",", closingQuoteIndex);
                string remainingFields = line.Substring(commaIndex + 1);
                fields = remainingFields.Split(',');
            }
            else
            {
                commaIndex = line.IndexOf(",");
                name = line.Substring(0, commaIndex);
                string remainingFields = line.Substring(commaIndex + 1);
                fields = remainingFields.Split(',');
            }

            if (fields.Length < 4) continue;

            string characterClass = fields[0];
            int level = int.Parse(fields[1]);
            int hp = int.Parse(fields[2]);
            List<string> equipment = fields[3].Split('|').ToList();

            characters.Add(new Character
            {
                Name = name,
                Class = characterClass,
                Level = level,
                HP = hp,
                Equipment = equipment
            });
        }

        return characters;
    }

    public void WriteCharacters(string filePath, List<Character> characters)
    {
        using var writer = new StreamWriter(filePath, false);
        writer.WriteLine("Name,Class,Level,HP,Equipment");

        foreach (var character in characters)
        {
            string name = character.Name.Contains(",") ? $"\"{character.Name}\"" : character.Name;
            string formatEquipment = string.Join("|", character.Equipment);
            writer.WriteLine($"{name},{character.Class},{character.Level},{character.HP},{formatEquipment}");
        }
    }
}