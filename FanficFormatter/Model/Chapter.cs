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
    using System;
    using System.Collections.Generic;

    /// <summary>
    ///     A fanfic chapter.
    /// </summary>
    public class Chapter
    {
        public Chapter(
            int number,
            string? synopsis,
            string lastModified,
            List<string>? revisions,
            List<string>? remarks,
            List<string> content,
            Fanfic fanfic)
        {
            Number = number;
            Synopsis = synopsis;
            LastModified = lastModified;
            Revisions = revisions;
            Remarks = remarks;
            Content = content;
            Fanfic = fanfic;
        }

        public int Number { get; }

        public string? Synopsis { get; }

        public string LastModified { get; }

        public List<string>? Revisions { get; }

        public List<string>? Remarks { get; }

        public List<string> Content { get; }

        public Fanfic Fanfic { get; }
    }
}
