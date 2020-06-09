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
        [JsonConstructor]
        public JsonFanficDescriptor(string title, string license, string synopsis)
        {
            Title = title;
            License = license;
            Synopsis = synopsis;
        }

        /// <summary>
        ///     Gets the fanfic title.
        /// </summary>
        public string Title { get; }

        /// <summary>
        ///     Gets the creative commons license abbreviation.
        /// </summary>
        public string License { get; }

        /// <summary>
        ///     Gets the synopsis.
        /// </summary>
        public string Synopsis { get; }
    }
}
