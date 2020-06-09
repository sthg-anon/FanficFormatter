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
    using System;
    using System.Collections.Generic;
    using Newtonsoft.Json;

    /// <summary>
    ///     Metadata about a fanfic chapter.
    /// </summary>
    public class JsonChapterInfo
    {
        /// <summary>
        ///     Initializes a new instance of the <see cref="JsonChapterInfo"/> class.
        /// </summary>
        /// <param name="number">The chapter number.</param>
        /// <param name="synopsis">The synopsis.</param>
        /// <param name="lastModified">The last modified date.</param>
        /// <param name="revisions">The revision remarks.</param>
        /// <param name="remarks">Remarks about this chapter.</param>
        [JsonConstructor]
        public JsonChapterInfo(
            int number,
            string? synopsis,
            DateTime lastModified,
            List<string>? revisions,
            string? remarks,
            string contentFile)
        {
            Number = number;
            Synopsis = synopsis;
            LastModified = lastModified;
            Revisions = revisions;
            Remarks = remarks;
            ContentFile = contentFile;
        }

        /// <summary>
        ///     Gets the chapter number (chapter 1, chapter 2, chapter n, ...).
        /// </summary>
        [JsonRequired]
        public int Number { get; }

        /// <summary>
        ///     Gets the chapter synopsis.
        /// </summary>
        public string? Synopsis { get; }

        /// <summary>
        ///     Gets the last modified time.
        /// </summary>
        [JsonRequired]
        public DateTime LastModified { get; }

        /// <summary>
        ///     Gets the list of revision remarks.
        /// </summary>
        public List<string>? Revisions { get; }

        /// <summary>
        ///     Gets the remarks about the chapter itself.
        /// </summary>
        public string? Remarks { get; }

        [JsonRequired]
        public string ContentFile { get; }
    }
}
