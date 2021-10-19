using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;
using NewWorldLib;
using NewWorldLib.Datasheets;
using NewWorldLib.Paks;

void ReadPaks()
{
    Console.WriteLine("Read Paks");
    var dir = OperatingSystem.IsWindows()
        ? @"D:\SteamLibrary\steamapps\common\New World\assets"
        : "/Users/razfriman/Downloads/NEW WORLD/assets";
    var outputDir = "extracted";
    var files = Directory.GetFiles(dir, "*.pak", SearchOption.AllDirectories);
    var entries = new HashSet<string>();

    foreach (var file in files)
    { 
        var pakFile = PakFile.Parse(file);
        
       // pakFile.Save(outputDir);
        foreach (var entryName in pakFile.Entries.Keys)
        {
            var entry = pakFile.Entries[entryName];
            entries.Add(entryName);
            
            //Open stream again
            var stream = File.OpenRead(file);
            entry.Reader = new BinaryReader(stream);

            
            if (entry.Method != 15)
            {
                Console.WriteLine(entryName);
                entry.Save(outputDir);
            }
        }
    }

    File.WriteAllText("files.txt", JsonSerializer.Serialize(entries));
}

void ReadDatasheets()
{
    Console.WriteLine("Read Datasheets");
    var file = "/Users/razfriman/Downloads/datatables/javelindata_tradeskillmining.datasheet";
    var datasheet = Datasheet.Parse(file);
    var json = JsonSerializer.Serialize(datasheet, new JsonSerializerOptions()
    {
        WriteIndented = true
    });
    Console.WriteLine(json);
}

ReadPaks();
//ReadDatasheets();

Console.WriteLine("Done");