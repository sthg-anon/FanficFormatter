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

namespace FanficFormatter.Model
{
    using System.Collections.Generic;

    public class Fanfic
    {
        public Fanfic(string title, string license, string? synopsis, List<Chapter> chapters)
        {
            Title = title;
            License = license;
            Synopsis = synopsis;
            Chapters = chapters;
        }

        public string Title { get; }

        public string License { get; }

        public string? Synopsis { get; }

        public List<Chapter> Chapters { get; }
    }
}
