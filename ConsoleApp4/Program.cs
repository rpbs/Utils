// See https://aka.ms/new-console-template for more information
using ConsoleApp4;
using System.Text.RegularExpressions;

string pattern = @"<body(>|.*)>((.|\n)*)</body>";
// Create a Regex  
Regex rg = new(pattern);

// Long string  
string authors =  File.ReadAllText("test.txt");
// Get all matches  
MatchCollection matchedAuthors = rg.Matches(authors);

int found = 0;

var body = matchedAuthors[0].Value.Replace("\r", "").Replace("\n", "");

NewMethod(ref body, ref found);

Console.WriteLine(body.TrimStart().TrimEnd());

static void NewMethod(ref string bodyMatch, ref int found)
{
    if (bodyMatch is not null)
    {
        foreach (var tag in HtmlTags.Tags)
        {
            var rg = new Regex($"<{tag}\\s.+?\">");
            if (rg.IsMatch(bodyMatch))
            {
                bodyMatch = rg.Replace(bodyMatch, "");
                found++;
            }
            rg = new Regex($"<{tag}>");
            if (rg.IsMatch(bodyMatch))
            {
                bodyMatch = rg.Replace(bodyMatch, "");
                found++;
            }
            rg = new Regex(@$"</{tag}>");
            if (rg.IsMatch(bodyMatch))
            {
                bodyMatch = rg.Replace(bodyMatch, "");
                found++;
            }
        }
    }
}


