using W4_assignment_template.Interfaces;
using W4_assignment_template.Models;
using W4_assignment_template.Services;

namespace W4_assignment_template;

class Program
{
    // The current file handler strategy (CSV or JSON), the in-memory list of characters,
    // and the current file format and path.
    static IFileHandler fileHandler;
    static List<Character> characters;
    static string fileFormat = "CSV";
    static string filePath = "Files/input.csv";

    static void Main()
    {
        
        SetFileHandler(fileFormat);

        while (true)
        {
            Console.WriteLine("Menu");
            Console.WriteLine("1. Display Characters");
            Console.WriteLine("2. Add Character");
            Console.WriteLine("3. Level Up Character");          
            Console.WriteLine($"4. Change File Format (currently set to {fileFormat})");
            Console.WriteLine("5. Exit");
            Console.Write("Enter your choice: ");
            string choice = Console.ReadLine();

            switch (choice)
            {
                case "1":
                    DisplayAllCharacters();
                    break;
                case "2":
                    AddCharacter();
                    break;
                case "3":
                    LevelUpCharacter();
                    break;
                case "4":
                    ChangeFileFormat();
                    break;
                case "5": 
                    fileHandler.WriteCharacters(filePath, characters);
                    return;
                default:
                    Console.WriteLine("Invalid choice. Please try again.");
                    break;
            }
        }
    }

    // Display all characters in the in-memory list
    static void DisplayAllCharacters()
    {
        foreach (var character in characters)
        {
            Console.WriteLine($"Name: {character.Name}, Class: {character.Class}, Level: {character.Level}, HP: {character.HP}, Equipment: {string.Join(", ", character.Equipment)}");
        }
    }

    // Add a new character based on user input
    static void AddCharacter()
    {
        // Get user input for the new character
        Console.Write("Enter your character's name: ");
        string name = Console.ReadLine();

        Console.Write("Enter your character's class: ");
        string characterClass = Console.ReadLine();

        Console.Write("Enter your character's level: ");
        int level = int.Parse(Console.ReadLine());

        Console.Write("Enter your character's hit points: ");
        int hp = int.Parse(Console.ReadLine());

        // Collect a fixed number of equipment items (3)
        string[] equipment = new string[3];
        for (int i = 0; i < equipment.Length; i++)
        {
            Console.Write($"Enter equipment item {i + 1}: ");
            equipment[i] = Console.ReadLine().Trim();
        }

        // Create the new character object
        var character = new Character
        {
            Name = name,
            Class = characterClass,
            Level = level,
            HP = hp,
            Equipment = equipment.ToList() // Convert array to list for storage
        };

        // Add the new character to the in-memory list
        characters.Add(character);

        Console.WriteLine("\nCharacter added successfully!\n");
    }

    // Prompts the user for a character name and levels up that character if found
    static void LevelUpCharacter()
    {
        Console.Write("Enter the name of the character to level up: ");
        string nameToLevelUp = Console.ReadLine();

        var character = characters.Find(c => c.Name.Equals(nameToLevelUp, StringComparison.OrdinalIgnoreCase));
        if (character != null)
        {
            character.Level++;
            Console.WriteLine($"\n{character.Name} leveled up to {character.Level}!\n");
        }
        else
        {
            Console.WriteLine("Character not found.");
        }
    }

    // Sets the file handler strategy (CSV or JSON) and loads characters from the specified format
    // Also saves current data before switching formats
    static void SetFileHandler(string format)
    {
        // Save current data before switching formats
        if (fileHandler != null && characters != null)
        {
            fileHandler.WriteCharacters(filePath, characters);
        }

        if (format == "CSV")
        {
            fileHandler = new CsvFileHandler();
            filePath = "Files/input.csv";
        }
        else
        {
            fileHandler = new JsonFileHandler();
            filePath = "Files/input.json";
        }
        characters = fileHandler.ReadCharacters(filePath);
    }

    // Allows the user to switch between CSV and JSON file formats at runtime
    // Saves current data before switching formats
    static void ChangeFileFormat()
    {
        fileHandler.WriteCharacters(filePath, characters);

        Console.Write("Enter file format (CSV/JSON): ");
        string input = Console.ReadLine().Trim().ToUpper();
        if (input == "CSV" || input == "JSON")
        {
            fileFormat = input;
            SetFileHandler(fileFormat);
            Console.WriteLine($"Switched to {fileFormat} format.\n");
        }
        else
        {
            Console.WriteLine("Invalid format. Please enter 'CSV' or 'JSON'.");
        }
    }
}