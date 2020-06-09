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
    /// <summary>
    ///     Contains common information about the fanfic.
    /// </summary>
    public class FanficDescriptor
    {
        public FanficDescriptor(string title, string license, string synopsis)
        {
            Title = title;
            License = license;
            Synopsis = synopsis;
        }

        public string Title { get; }

        public string License { get; }

        public string Synopsis { get; }
    }
}
