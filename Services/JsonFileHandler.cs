using Newtonsoft.Json;
using W4_assignment_template.Interfaces;
using W4_assignment_template.Models;



namespace W4_assignment_template.Services;

// Handles reading and writing character data in JSON format
// Implements the IFileHandler interface
public class JsonFileHandler : IFileHandler
{
    public List<Character> ReadCharacters(string filePath)
    {
        if (!File.Exists(filePath))
            return new List<Character>();

        string json = File.ReadAllText(filePath);
        var characters = JsonConvert.DeserializeObject<List<Character>>(json);
        return characters ?? new List<Character>();
    }

    public void WriteCharacters(string filePath, List<Character> characters)
    {
        string json = JsonConvert.SerializeObject(characters, Formatting.Indented);
        File.WriteAllText(filePath, json);
    }
}