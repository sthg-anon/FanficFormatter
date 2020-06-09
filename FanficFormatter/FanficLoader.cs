// DO WHAT THE FUCK YOU WANT TO PUBLIC LICENSE
// Version 2, December 2004
//
// Copyright (C) 2020 sthg anon (sthg.anon@gmail.com)
//
// Everyone is permitted to copy and distribute verbatim or modified
// copies of this license document, and changing it is allowed as long
// as the name is changed.
//
// DO WHAT THE FUCK YOU WANT TO PUBLIC LICENSE
// TERMS AND CONDITIONS FOR COPYING, DISTRIBUTION AND MODIFICATION
//
// 0. You just DO WHAT THE FUCK YOU WANT TO.

namespace FanficFormatter
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using FanficFormatter.Model;
    using FanficFormatter.Model.Json;
    using Newtonsoft.Json;
    using Serilog;

    public static class FanficLoader
    {
        private const string FanficJsonName = "fanfic.json";

        private static readonly ILogger Log = Serilog.Log.ForContext(typeof(FanficLoader));

        public static Fanfic Load(string path)
        {
            var descriptor = LoadDescriptor(path);
            var fanfic = ConvertFanfic(descriptor);

            foreach (var jsonChapter in descriptor.Chapters)
            {
                var contentFilePath = Path.Join(path, jsonChapter.ContentFile);

                List<string> content;
                try
                {
                    content = LoadChapterContent(contentFilePath);
                }
                catch (FanficLoadException e)
                {
                    Log.Error(e, "Failed to load chapter {number} content!", jsonChapter.Number);
                    continue;
                }

                fanfic.AddChapter(jsonChapter, content);
            }

            return fanfic;
        }

        private static List<string> LoadChapterContent(string path)
        {
            if (!File.Exists(path))
            {
                throw new FanficLoadException($"File {path} not found!");
            }

            string[] lines;
            try
            {
                lines = File.ReadAllLines(path);
            }
            catch (Exception e)
            {
                throw new FanficLoadException($"Failed to read file {path}!", e);
            }

            return lines.Where(line => !string.IsNullOrWhiteSpace(line)).ToList();
        }

        private static Fanfic ConvertFanfic(JsonFanficDescriptor descriptor)
        {
            return new Fanfic(descriptor.Title, descriptor.License, descriptor.Synopsis);
        }

        private static JsonFanficDescriptor LoadDescriptor(string path)
        {
            var jsonPath = Path.Join(path, FanficJsonName);
            if (!File.Exists(jsonPath))
            {
                throw new FanficLoadException($"Unable to find {jsonPath}!");
            }

            string jsonData;
            try
            {
                jsonData = File.ReadAllText(jsonPath);
            }
            catch (Exception e)
            {
                throw new FanficLoadException($"Unable to read file {jsonPath}: {e.Message}", e);
            }

            JsonFanficDescriptor descriptor;
            try
            {
                descriptor = JsonConvert.DeserializeObject<JsonFanficDescriptor>(jsonData);
            }
            catch (Exception e)
            {
                throw new FanficLoadException($"Unable to parse {jsonPath}: {e.Message}", e);
            }

            if (descriptor == null)
            {
                throw new FanficLoadException($"File {jsonPath} is empty!");
            }

            return descriptor;
        }
    }
}
