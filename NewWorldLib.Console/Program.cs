using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;
using NewWorldLib.Datasheets;
using NewWorldLib.Paks;

static void ReadPaks()
{
    Console.WriteLine("Read Paks");
    var dir = OperatingSystem.IsWindows()
        ? @"D:\SteamLibrary\steamapps\common\New World\assets"
        : "/Users/razfriman/Downloads/NEW WORLD/assets";
    const string outputDir = "extracted";
    var files = Directory.GetFiles(dir, "*.pak", SearchOption.AllDirectories);
    var entries = new HashSet<string>();
    
    foreach (var file in files)
    {
        var pakFile = PakFile.Parse(file);

        var dataSheets = pakFile.Entries
            .Where(x => x.Key.Contains(@".datasheet") || x.Key.Contains(@".json") || x.Key.Contains(@".xml"))
            .ToList();
        
        foreach (var entry in dataSheets)
        {
            var entryValue = entry.Value;
            entries.Add(entry.Key);
        
            //Open stream again
            var stream = File.OpenRead(file);
            entryValue.Reader = new BinaryReader(stream);
            
            Console.WriteLine(entry.Key);
            entryValue.Save(outputDir);
        }
    }

    File.WriteAllText("files.txt", JsonSerializer.Serialize(entries));
}

static void ReadDatasheets()
{
    Console.WriteLine("Read Datasheets");
    var allDataSheets = Directory.GetFiles(Directory.GetCurrentDirectory(), "*.datasheet", SearchOption.AllDirectories);

    foreach (var dataSheet in allDataSheets)
    {
        var datasheetParsed = Datasheet.Parse(dataSheet);
        var json = JsonSerializer.Serialize(datasheetParsed, new JsonSerializerOptions()
        {
            WriteIndented = true
        });

        var pos = dataSheet.LastIndexOf("\\", StringComparison.Ordinal) + 1;
        var name = dataSheet.Substring(pos, dataSheet.Length - pos);
        
        var fullPath = Path.Combine("jsons",  name.Replace(".datasheet", ".json"));
        Directory.CreateDirectory(Path.GetDirectoryName(fullPath) ?? string.Empty);
        File.WriteAllText(fullPath, json);
        
        Console.WriteLine(json);
    }
}

ReadPaks();
ReadDatasheets();

Console.WriteLine("Done");