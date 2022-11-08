// See https://aka.ms/new-console-template for more information
using ConsoleApp4;
using System.Text.RegularExpressions;

string html = File.ReadAllText("test1.txt");

html = html.Replace("'", "\"");

string patternLowerCase = @"<body(>|.*)>((.|\n)*)</body>";

string patternUpperCase = @"<BODY(>|.*)>((.|\n)*)</BODY>";

var body = "";

Regex rg = new(patternLowerCase);

if (rg.IsMatch(html))
{
    var matchedAuthors = rg.Matches(html);
    body = matchedAuthors[0].Value.Replace("\r", "").Replace("\n", "");
}

rg = new(patternUpperCase);

if (rg.IsMatch(html))
{
    var matchedAuthors = rg.Matches(html);
    body = matchedAuthors[0].Value.Replace("\r", "").Replace("\n", "");
}

NewMethod(ref body);

var trimed = body.TrimStart().TrimEnd(); 

Console.WriteLine(trimed);

static void NewMethod(ref string bodyMatch)
{
    if (bodyMatch is not null)
    {
        foreach (var tag in HtmlTags.Tags)
        {
            var rgTagWithProperties = new Regex($"<{tag}\\s.+?\">");
            var rgTagNormal = new Regex($"<{tag}>");
            var rgTagClosing = new Regex(@$"</{tag}>");

            if (rgTagWithProperties.IsMatch(bodyMatch))
                bodyMatch = rgTagWithProperties.Replace(bodyMatch, "");

            if (rgTagNormal.IsMatch(bodyMatch))
                bodyMatch = rgTagNormal.Replace(bodyMatch, "");

            if (rgTagClosing.IsMatch(bodyMatch))            
                bodyMatch = rgTagClosing.Replace(bodyMatch, "");

            var rgCommentOpen = new Regex(@$"<!--");
            var rgCommentClose = new Regex(@$"-->");

            if (rgCommentOpen.IsMatch(bodyMatch))
                bodyMatch = rgCommentOpen.Replace(bodyMatch, "");

            if (rgCommentClose.IsMatch(bodyMatch))
                bodyMatch = rgCommentClose.Replace(bodyMatch, "");
        }
        foreach (var tag in HtmlTags.Tags.Select(x => x.ToUpper()))
        {
            var rgTagWithProperties = new Regex($"<{tag}\\s.+?\">");
            var rgTagNormal = new Regex($"<{tag}>");
            var rgTagClosing = new Regex(@$"</{tag}>");

            if (rgTagWithProperties.IsMatch(bodyMatch))
                bodyMatch = rgTagWithProperties.Replace(bodyMatch, "");

            if (rgTagNormal.IsMatch(bodyMatch))
                bodyMatch = rgTagNormal.Replace(bodyMatch, "");

            if (rgTagClosing.IsMatch(bodyMatch))
                bodyMatch = rgTagClosing.Replace(bodyMatch, "");

            var rgCommentOpen = new Regex(@$"<{tag}");
            var rgCommentClose = new Regex(@$"{tag}>");

            if (rgCommentOpen.IsMatch(bodyMatch))
                bodyMatch = rgCommentOpen.Replace(bodyMatch, "");

            if (rgCommentClose.IsMatch(bodyMatch))
                bodyMatch = rgCommentClose.Replace(bodyMatch, "");
        }

        bodyMatch = bodyMatch.TrimStart().TrimEnd();

        var rgRemoveExtraWhiteSpaces = new Regex(@"(\s{2,})");
        if (rgRemoveExtraWhiteSpaces.IsMatch(bodyMatch))
            bodyMatch = rgRemoveExtraWhiteSpaces.Replace(bodyMatch, " ");
    }
}


