﻿// DO WHAT THE FUCK YOU WANT TO PUBLIC LICENSE
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

using System.Text;

namespace FanficFormatter
{
    using System;
    using System.IO;
    using FanficFormatter.Model;

    public static class FanficRenderer
    {
        private const string StyleFolder = "style";

        private const string StyleCss =
            "body {\r\n    font-family: 'Open Sans', sans-serif;\r\n    margin: 0 auto;\r\n    width: 32em;\r\n}\r\n\r\np {\r\n    font-size: 1.1em;\r\n    width: 560px;\r\n    line-height: 1.6em;\r\n}\r\n\r\nimg {\r\n    max-width: 100%;\r\n    height: auto;\r\n}\r\n\r\n.nav {\r\n    display: grid;\r\n    grid-template-columns: 40% 40% 20%;\r\n    grid-template-rows: 100%;\r\n}\r\n\r\n.nav .prev {\r\n    grid-column-start: 1;\r\n}\r\n\r\n.nav .contents {\r\n    grid-column-start: 2;\r\n}\r\n\r\n.nav .next {\r\n    grid-column-start: 3;\r\n}\r\n\r\n.skip {\r\n    text-align: center;\r\n}";

        private const string ResetCss =
            @"html,body,div,form,fieldset,legend,label{margin:0;padding:0;}table{border-collapse:collapse;border-spacing:0;}th,td{text-align:left;vertical-align:top;}h1,h2,h3,h4,h5,h6,th,td,caption{font-weight:normal;}img{border:0;}";

        public static void Render(Fanfic fanfic, string path)
        {
            var styleDir = Path.Join(path, StyleFolder);

            try
            {
                Directory.CreateDirectory(path);
                Directory.CreateDirectory(styleDir);
            }
            catch (Exception e)
            {
                throw new FanficRenderException($"Unable to create path {path}!", e);
            }

            WriteFile(StyleCss, styleDir, "style.css");
            WriteFile(ResetCss, styleDir, "CSS-Mini-Reset-min.css");

            WriteFile(RenderMain(fanfic), path, "index.xhtml");
        }

        private static void WriteFile(string data, string root, string fileName)
        {
            var path = Path.Join(root, fileName);

            try
            {
                File.WriteAllText(path, data);
            }
            catch (Exception e)
            {
                throw new FanficRenderException($"Unable to write to {path}: {e.Message}", e);
            }
        }

        private static string RenderMain(Fanfic fanfic)
        {
            var sb = new StringBuilder();
            sb
                .Append("<?xml version=\"1.0\" encoding=\"UTF-8\"?>\n")
                .Append("<!DOCTYPE html PUBLIC \"-//W3C//DTD XHTML 1.0 Strict//EN\" \"http://www.w3.org/TR/xhtml1/DTD/xhtml1-strict.dtd\">\n")
                .Append(@"<html xmlns=""http://www.w3.org/1999/xhtml"" xml:lang=""en"" lang=""en"">")
                .Append("<head>")
                    .Append(@"<link href=""https://fonts.googleapis.com/css2?family=Open+Sans&amp;display=swap"" rel=""stylesheet"" />")
                    .Append(@"<link rel=""stylesheet"" href=""style/CSS-Mini-Reset-min.css"" />")
                    .Append(@"<link rel=""stylesheet"" href=""style/style.css"" />")
                    .Append($"<title>{fanfic.Title}</title>")
                .Append("</head>")
                .Append("<body>")
                .Append($"<h1>{fanfic.Title}</h1>");

            if (fanfic.Synopsis != null)
            {
                sb.Append($"<p>{fanfic.Synopsis}</p>");
            }

            sb.Append("<h2>Table of Contents</h2>");

            foreach (var chapter in fanfic.Chapters)
            {
                sb.Append($"<p><a href=\"chapter_{chapter.Number}.xhtml\">Chapter {chapter.Number}</a></p>");
            }

            sb.Append("</body>");
            sb.Append("</html>");

            return sb.ToString();
        }
    }
}