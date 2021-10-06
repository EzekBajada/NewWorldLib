﻿using System.Text.Json;
using NewWorldLib;

var dir = @"D:\SteamLibrary\steamapps\common\New World\assets";
var outputDir = "extracted";
var files = Directory.GetFiles(dir, "*.pak", SearchOption.AllDirectories);
var entries = new HashSet<string>();

foreach (var file in files)
{
    using var pakFile = PakFile.Parse(file);
    pakFile.Save(outputDir);
    foreach (var entry in pakFile.Entries.Keys)
    {
        entries.Add(entry);
    }
}

File.WriteAllText("files.txt", JsonSerializer.Serialize(entries));
Console.WriteLine("Done");