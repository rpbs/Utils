// See https://aka.ms/new-console-template for more information
using ConsoleApp4;
using System.Security.Cryptography;
using System.Text.RegularExpressions;



IDictionary<string, string> test = new Dictionary<string, string>
{
    ["a"] = "a",

    ["b"] = "b"
};

string pattern = @"<body(>|.*)>((.|\n)*)</body>";
// Create a Regex  
Regex rg = new(pattern);

// Long string  
string authors = File.ReadAllText("test.txt");
// Get all matches  
MatchCollection matchedAuthors = rg.Matches(authors);

int found = 0;

var asd = matchedAuthors[0].Value;

NewMethod(ref asd, ref found);

static void NewMethod(ref string bodyMatch, ref int found)
{
    if (found == 3)
    {
        Console.WriteLine(bodyMatch);

        return;
    }
    var body = "";
    if (bodyMatch is not null)
    {        
        foreach (var tag in HtmlTags.Tags)
        {
            var rg = new Regex($"<{tag}(>|.*)>((.|\n)*)</{tag}>");
            var matchedAuthors = rg.Matches(bodyMatch);

            for (int count = 0; count < matchedAuthors.Count; count++)
            {
                body += matchedAuthors[count].Value
                    .Replace($"<{tag}>", "")
                    .Replace($"</{tag}>", "")
                    .Replace("\r", "")
                    .Replace("\n", "");
                // remover all tags here at body
                rg = new Regex($"<{tag}\\s.+?\">");
                if (rg.IsMatch(body))
                {
                    bodyMatch = rg.Replace(body, "");
                    found++;
                    NewMethod(ref bodyMatch, ref found);
                }
                rg = new Regex($"<//{tag}>");
                if (rg.IsMatch(body))
                {
                    bodyMatch = rg.Replace(body, "");
                    found++;
                    NewMethod(ref bodyMatch, ref found);
                }
                 
                rg = new Regex("");

            }            
        }
        
    }
}


