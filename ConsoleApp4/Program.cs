// See https://aka.ms/new-console-template for more information
using ConsoleApp4;
using System.Runtime.InteropServices;
using System.Text;
using System.Text.RegularExpressions;

string html = File.ReadAllText("test.txt");

html = html.Replace("'", "\"")
    .Replace("3D","")
    .Replace("\r", "")
    .Replace("\n", "")
    .Replace(">=20",">");


NewMethod(ref html);

var trimed = html.TrimStart().TrimEnd(); 

//Console.WriteLine(trimed);

static void NewMethod(ref string bodyMatch)
{
    var matches = new List<MatchCollection>();
    var content = new List<string>();

    if (bodyMatch is not null)
    {
        foreach (var tag in HtmlTags.ContainerTags)
        {
            // <p\s.+?\">.+?<\/p>
            var pattern = @$"<{tag}\s.+?>.+?<\/{tag}>";
            var rgTagWithProperties = new Regex(pattern);
            var rgTagNormal = new Regex(@$"<{tag}>.+?<\/{tag}>");

            if (rgTagWithProperties.IsMatch(bodyMatch))
                matches.Add(rgTagWithProperties.Matches(bodyMatch));

            if (rgTagNormal.IsMatch(bodyMatch))
                matches.Add(rgTagNormal.Matches(bodyMatch));

        }
        foreach (var tag in HtmlTags.ContainerTags.Select(x => x.ToUpper()))
        {
            var rgTagWithProperties = new Regex($"<{tag}\\s.+?\"><//{tag}>");
            var rgTagNormal = new Regex($"<{tag}><//{tag}>");

            if (rgTagWithProperties.IsMatch(bodyMatch))
                matches.Add(rgTagWithProperties.Matches(bodyMatch));

            if (rgTagNormal.IsMatch(bodyMatch))
                matches.Add(rgTagNormal.Matches(bodyMatch));
        }

        RemoveTags(ref matches);

        var contentx = new List<string>();

        foreach (var matchs in matches)
        {
            foreach (var collection in matches)
            {
                foreach (var tag in HtmlTags.ContainerTags)
                {
                    var plaiText = GetPlainText(tag);
                }
            }
        }

        bodyMatch = bodyMatch.TrimStart().TrimEnd();

        var rgRemoveExtraWhiteSpaces = new Regex(@"(\s{2,})");
        if (rgRemoveExtraWhiteSpaces.IsMatch(bodyMatch))
            bodyMatch = rgRemoveExtraWhiteSpaces.Replace(bodyMatch, " ");
    }
}

static void RemoveTags(ref List<MatchCollection> matches)
{

    var totalMatchs = 0;
    foreach (var matchs in matches)
    {
        foreach (var collection in matches)
        {
            foreach (var tag in HtmlTags.ContainerTags)
            {
                foreach (Match tags in collection)
                {
                    var tagUpper = tag.ToUpper();

                    var pattern = @$"<{tag}\s.+?>";
                    var patternUpper = @$"<{tagUpper}\s.+?>";
                    var rg = new Regex(pattern);
                    var rgUpper = new Regex(pattern);
                    string replaced = null;
                    // 
                    if (rg.IsMatch(tags.Value) || rgUpper.IsMatch(tags.Value))
                    {
                        replaced = rg.Replace(tags.Value, "");
                        RemoveTags(matches)
                    }

                    pattern = @$"<\/{tag}>";
                    patternUpper = @$"<\/{tagUpper}>";

                    rg = new Regex(pattern);
                    rgUpper = new Regex(patternUpper);
                    if (replaced is not null && rg.IsMatch(replaced) && rgUpper.IsMatch(replaced))
                    {
                        replaced = rg.Replace(replaced, "");
                    }

                    if (replaced is not null) { 
                        content.Add(replaced);
                        totalMatchs++;
                    }

                    if (totalMatchs > 0)
                    {

                    }
                }
            }
        }
    }

    foreach (var item in content)
    {
        Console.WriteLine(item);
    }
}