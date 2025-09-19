using Newtonsoft.Json;

namespace W4_assignment_template.Models;

public class Character
{
    [JsonProperty("name")]
    public string Name { get; set; }

    [JsonProperty("class")]
    public string Class { get; set; }

    [JsonProperty("level")]
    public int Level { get; set; }

    [JsonProperty("hp")]
    public int HP { get; set; }

    [JsonProperty("equipment")]
    public List<string> Equipment { get; set; }
}