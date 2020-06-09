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

using System.Linq;

namespace FanficFormatter.Model
{
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using FanficFormatter.Model.Json;

    public class Fanfic
    {
        private readonly List<Chapter> _chapters = new List<Chapter>();

        public Fanfic(string title, string license, string? synopsis, string? headerImage, string? headerAlt)
        {
            Title = title;
            License = license;
            Synopsis = synopsis;
            HeaderImage = headerImage;
            HeaderAlt = headerAlt;
        }

        public string Title { get; }

        public string License { get; }

        public string? Synopsis { get; }

        public string? HeaderImage { get; }

        public string? HeaderAlt { get; }

        public void AddChapter(JsonChapterInfo jsonChapter, List<string> content)
        {
            var chapter = new Chapter(
                jsonChapter.Number,
                jsonChapter.Synopsis,
                jsonChapter.LastModified,
                jsonChapter.Revisions,
                jsonChapter.Remarks,
                content,
                this);
            _chapters.Add(chapter);
        }

        public ReadOnlyCollection<Chapter> Chapters => _chapters.AsReadOnly();

        public bool HasChapter(int number)
        {
            return _chapters.Any(c => c.Number == number);
        }
    }
}
