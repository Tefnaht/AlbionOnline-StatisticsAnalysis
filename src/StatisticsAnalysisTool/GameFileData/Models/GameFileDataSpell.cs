using System.Collections.Generic;

namespace StatisticsAnalysisTool.GameFileData.Models;

public class GameFileDataSpell
{
    public int Index { get; set; }
    public string UniqueName { get; set; }
    public string Target { get; set; }
    public string Category { get; set; }
    public Dictionary<string, string> LocalizedNames { get; init; }
    public Dictionary<string, string> LocalizedDescriptions { get; init; }
}