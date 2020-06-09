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

using System.Text;

namespace FanficFormatter
{
    using System;
    using System.IO;
    using FanficFormatter.Model;

    public static class FanficRenderer
    {
        private const string StyleFolder = "style";
        private const string ImageFolder = "image";

        private const string StyleCss =
            "body {\r\n    font-family: 'Open Sans', sans-serif;\r\n    margin: 0 auto;\r\n    width: 32em;\r\n}\r\n\r\np {\r\n    font-size: 1.1em;\r\n    width: 560px;\r\n    line-height: 1.6em;\r\n}\r\n\r\nimg {\r\n    max-width: 100%;\r\n    height: auto;\r\n}\r\n\r\n.nav {\r\n    display: grid;\r\n    grid-template-columns: 40% 40% 20%;\r\n    grid-template-rows: 100%;\r\n}\r\n\r\n.nav .prev {\r\n    grid-column-start: 1;\r\n}\r\n\r\n.nav .contents {\r\n    grid-column-start: 2;\r\n}\r\n\r\n.nav .next {\r\n    grid-column-start: 3;\r\n}\r\n\r\n.skip {\r\n    text-align: center;\r\n}";

        private const string ResetCss =
            @"html,body,div,form,fieldset,legend,label{margin:0;padding:0;}table{border-collapse:collapse;border-spacing:0;}th,td{text-align:left;vertical-align:top;}h1,h2,h3,h4,h5,h6,th,td,caption{font-weight:normal;}img{border:0;}";

        public static void Render(Fanfic fanfic, string path)
        {
            var styleDir = Path.Join(path, StyleFolder);
            var imageDir = Path.Join(path, ImageFolder);

            try
            {
                Directory.CreateDirectory(path);
                Directory.CreateDirectory(styleDir);
                Directory.CreateDirectory(imageDir);
            }
            catch (Exception e)
            {
                throw new FanficRenderException($"Unable to create path {path}!", e);
            }

            WriteFile(StyleCss, styleDir, "style.css");
            WriteFile(ResetCss, styleDir, "CSS-Mini-Reset-min.css");

            if (fanfic.HeaderImage != null)
            {
                WriteHeader(fanfic.HeaderImage, imageDir);
            }

            WriteFile(RenderMain(fanfic), path, "index.xhtml");

            foreach (var chapter in fanfic.Chapters)
            {
                WriteFile(RenderChapter(chapter), path, $"chapter_{chapter.Number}.xhtml");
            }
        }

        private static void WriteHeader(string headerImage, string imageDir)
        {
            var extension = Path.GetExtension(headerImage);
            var headerDestination = Path.Join(imageDir, $"header{extension}");
            try
            {
                File.Copy(headerImage, headerDestination);
            }
            catch (Exception e)
            {
                throw new FanficRenderException($"Unable to copy header image to {headerDestination}!", e);
            }
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

        private static string RenderChapter(Chapter chapter)
        {
            var sb = new StringBuilder();

            var nav = RenderNav(chapter);

            sb
                .Append("<?xml version=\"1.0\" encoding=\"UTF-8\"?>\n")
                .Append("<!DOCTYPE html PUBLIC \"-//W3C//DTD XHTML 1.0 Strict//EN\" \"http://www.w3.org/TR/xhtml1/DTD/xhtml1-strict.dtd\">\n")
                .Append(@"<html xmlns=""http://www.w3.org/1999/xhtml"" xml:lang=""en"" lang=""en"">")
                .Append("<head>")
                    .Append(@"<link href=""https://fonts.googleapis.com/css2?family=Open+Sans&amp;display=swap"" rel=""stylesheet"" />")
                    .Append(@"<link rel=""stylesheet"" href=""style/CSS-Mini-Reset-min.css"" />")
                    .Append(@"<link rel=""stylesheet"" href=""style/style.css"" />")
                    .Append($"<title>{chapter.Fanfic.Title} - Chapter {chapter.Number}</title>")
                .Append("</head>")
                .Append("<body>")
                .Append("<header>")
                .Append($"<h1>{chapter.Fanfic.Title}</h1>")
                .Append(nav);

            if (chapter.Synopsis != null)
            {
                sb.Append("<h2>Synopsis</h2>");
                sb.Append($"<p>{chapter.Synopsis}</p>");
            }

            sb.Append("</header>");

            sb.Append("<main>");
            sb.Append($"<h2>Chapter {chapter.Number}</h2>");

            foreach (var line in chapter.Content)
            {
                if (line.Equals("* * *"))
                {
                    sb.Append("<p class=\"skip\">* * *</p>");
                }
                else
                {
                    sb.Append($"<p>{line}</p>");
                }
            }

            sb.Append("</main>");

            sb.Append("<footer>");
            sb.Append(nav);
            sb.Append($"<p>Last modified: {chapter.LastModified}</p>");
            if (chapter.Revisions != null)
            {
                sb.Append("<h2>Revision History</h2>");
                sb.Append($"<p>Current Revision: {chapter.Revisions.Count + 1}</p>");
                for (var ii = 0; ii < chapter.Revisions.Count; ++ii)
                {
                    sb.Append($"<p>{ii + 1}: {chapter.Revisions[ii]}</p>");
                }
            }

            if (chapter.Remarks != null)
            {
                sb.Append("<h2>Remarks</h2>");
                foreach (var remark in chapter.Remarks)
                {
                    sb.Append($"<p>{remark}</p>");
                }
            }

            sb.Append($"<p>Characters Copyright © Sega, Writing licensed under {chapter.Fanfic.License}</p>");

            sb.Append("</footer>");
            sb.Append("</body>");
            sb.Append("</html>");

            return sb.ToString();
        }

        private static StringBuilder RenderNav(Chapter chapter)
        {
            var prevChapter = chapter.Number - 1;
            var nextChapter = chapter.Number + 1;

            var sb = new StringBuilder();
            sb.Append(@"<div class=""nav"">");
            if (chapter.Fanfic.HasChapter(prevChapter))
            {
                sb.Append($@"<p class=""prev""><a href=""chapter_{prevChapter}.xhtml"">Previous Chapter</a></p>");
            }

            sb.Append(@"<p class=""contents""><a href=""index.xhtml"">Contents</a></p>");

            if (chapter.Fanfic.HasChapter(nextChapter))
            {
                sb.Append($@"<p class=""next""><a href=""chapter_{nextChapter}.xhtml"">Next Chapter</a></p>");
            }

            sb.Append("</div>");

            return sb;
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

            if (fanfic.HeaderImage != null)
            {
                var extension = Path.GetExtension(fanfic.HeaderImage);
                sb.Append($@"<img src=""image/header{extension}"" alt=""{fanfic.HeaderAlt}""/>");
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
