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

using System.Diagnostics;

namespace FanficFormatter
{
    using FanficFormatter.Model;
    using Serilog;

    /// <summary>
    ///     Contains the main program entry point.
    /// </summary>
    internal static class Program
    {
        private const string InDir = "TestFic";
        private const string outDir = "TestFicHtml";

        private static void Main(string[] args)
        {
            Log.Logger = new LoggerConfiguration()
                .WriteTo.Console()
                .CreateLogger();

            try
            {
                var fanfic = FanficLoader.Load(InDir);
                FanficRenderer.Render(fanfic, outDir);
            }
            catch (FanficLoadException e)
            {
                Log.Fatal(e, "Unable to load fanfic!");
            }
            catch (FanficRenderException e)
            {
                Log.Fatal(e, "Unable to write fanfic!");
            }
        }
    }
}
