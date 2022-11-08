// See https://aka.ms/new-console-template for more information
using ConsoleApp4;
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
    if (bodyMatch is not null)
    {
        foreach (var tag in HtmlTags.ContainerTags)
        {
            // <p\s.+?\">.+?<\/p>
            var pattern = "$@"<{tag}\s.+?\">.+?<\/{tag}>";
            var rgTagWithProperties = new Regex();
            var rgTagNormal = new Regex($"<{tag}>.+?<//{tag}>");

            if (rgTagWithProperties.IsMatch(bodyMatch))
                matches.Add(rgTagWithProperties.Matches(bodyMatch));

            if (rgTagNormal.IsMatch(bodyMatch))
                matches.Add(rgTagNormal.Matches(bodyMatch));

        }
        foreach (var tag in HtmlTags.Tags.Select(x => x.ToUpper()))
        {
            var rgTagWithProperties = new Regex($"<{tag}\\s.+?\"><//{tag}>");
            var rgTagNormal = new Regex($"<{tag}><//{tag}>");

            if (rgTagWithProperties.IsMatch(bodyMatch))
                matches.Add(rgTagWithProperties.Matches(bodyMatch));

            if (rgTagNormal.IsMatch(bodyMatch))
                matches.Add(rgTagNormal.Matches(bodyMatch));
        }

        foreach (var match in matches)
        {
            foreach (var values in match)
            {
                Console.WriteLine(values);
            }
        }

        bodyMatch = bodyMatch.TrimStart().TrimEnd();

        var rgRemoveExtraWhiteSpaces = new Regex(@"(\s{2,})");
        if (rgRemoveExtraWhiteSpaces.IsMatch(bodyMatch))
            bodyMatch = rgRemoveExtraWhiteSpaces.Replace(bodyMatch, " ");
    }
}


