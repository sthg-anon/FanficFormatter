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

namespace FanficFormatter.Model.Json
{
    using System.Collections.Generic;
    using Newtonsoft.Json;

    /// <summary>
    ///     Contains common information about the fanfic.
    /// </summary>
    public class JsonFanficDescriptor
    {
        /// <summary>
        ///     Initializes a new instance of the <see cref="JsonFanficDescriptor"/> class.
        /// </summary>
        /// <param name="title">The fanfic title.</param>
        /// <param name="license">The creative commons license abbreviation.</param>
        /// <param name="synopsis">The fanfic synopsis.</param>
        /// <param name="chapters">The list of chapters.</param>
        [JsonConstructor]
        public JsonFanficDescriptor(string title, string license, string? synopsis, List<JsonChapterInfo> chapters)
        {
            Title = title;
            License = license;
            Synopsis = synopsis;
            Chapters = chapters;
        }

        /// <summary>
        ///     Gets the fanfic title.
        /// </summary>
        [JsonRequired]
        public string Title { get; }

        /// <summary>
        ///     Gets the creative commons license abbreviation.
        /// </summary>
        [JsonRequired]
        public string License { get; }

        /// <summary>
        ///     Gets the synopsis.
        /// </summary>
        public string? Synopsis { get; }

        /// <summary>
        ///     Gets the list of chapters.
        /// </summary>
        public List<JsonChapterInfo> Chapters { get; }
    }
}
