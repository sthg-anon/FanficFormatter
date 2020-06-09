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
        /// <summary>
        ///     The chapter number (chapter 1, chapter 2, chapter n, ...).
        /// </summary>
        public int Number { get; }

        public string Synopsis { get; }

        public List<string> Lines { get; }

        public string CurrentRevision { get; }

        public DateTime LastModified { get; }

        public List<string> Revisions { get; }

        public string Remarks { get; }
    }
}
