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

var body = matchedAuthors[0].Value.Replace("\r", "").Replace("\n", "");

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
        }
        bodyMatch = bodyMatch.TrimStart().TrimEnd();

        var rgRemoveExtraWhiteSpaces = new Regex(@"(\s{2,})");
        if (rgRemoveExtraWhiteSpaces.IsMatch(bodyMatch))
            bodyMatch = rgRemoveExtraWhiteSpaces.Replace(bodyMatch, " ");
    }
}


