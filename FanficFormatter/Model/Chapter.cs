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
            DateTime lastModified,
            List<string>? revisions,
            string? remarks,
            bool hasNext,
            bool hasPrevious,
            List<string> content)
        {
            Number = number;
            Synopsis = synopsis;
            LastModified = lastModified;
            Revisions = revisions;
            Remarks = remarks;
            HasNext = hasNext;
            HasPrevious = hasPrevious;
            Content = content;
        }

        public int Number { get; }

        public string? Synopsis { get; }

        public DateTime LastModified { get; }

        public List<string>? Revisions { get; }

        public string? Remarks { get; }

        public bool HasNext { get; }

        public bool HasPrevious { get; }

        public List<string> Content { get; }
    }
}
