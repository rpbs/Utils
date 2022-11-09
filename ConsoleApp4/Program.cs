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
    var allMatchTags = new List<string>();

    var tags = HtmlTags.ContainerTags;
    var upperCase = HtmlTags.ContainerTags.Select(x => x.ToUpper()).ToArray();

    if (bodyMatch is not null)
    {
        CaptureTags(bodyMatch, ref allMatchTags, tags);
        CaptureTags(bodyMatch, ref allMatchTags, upperCase);

        ExtractValueFromTags(ref allMatchTags, tags);
        ExtractValueFromTags(ref allMatchTags, upperCase);       

        bodyMatch = bodyMatch.TrimStart().TrimEnd();

        var rgRemoveExtraWhiteSpaces = new Regex(@"(\s{2,})");
        if (rgRemoveExtraWhiteSpaces.IsMatch(bodyMatch))
            bodyMatch = rgRemoveExtraWhiteSpaces.Replace(bodyMatch, " ");
    }

    static void CaptureTags(string bodyMatch, ref List<string> allMatchTags, string[] containerTags)
    {
        foreach (var tag in HtmlTags.ContainerTags)
        {
            // <p\s.+?\">.+?<\/p>
            var pattern = @$"<{tag}\s.+?>.+?<\/{tag}>";
            var rgTagWithProperties = new Regex(pattern);
            var rgTagNormal = new Regex(@$"<{tag}>.+?<\/{tag}>");

            if (rgTagWithProperties.IsMatch(bodyMatch))
            {
                allMatchTags.AddRange(
                    rgTagWithProperties
                    .Matches(bodyMatch)
                    .Select(x => x.Value).ToList());
            }


            if (rgTagNormal.IsMatch(bodyMatch))
            {
                allMatchTags.AddRange(
                    rgTagNormal
                    .Matches(bodyMatch)
                    .Select(x => x.Value).ToList());
            }

        }
    }
}

static void ExtractValueFromTags(ref List<string> allMatchTags, string[] tags)
{
    foreach (var tag in tags)
    {
        for (int i = 0; i < allMatchTags.Count; i++)
        {
            var tagContent = allMatchTags[i];
            var pattern = @$"<{tag}\s.+?>";
            var rg = new Regex(pattern);
            if (rg.IsMatch(tagContent))
            {
                tagContent = rg.Replace(tagContent, "");
            }
            pattern = @$"<\/{tag}>";
            rg = new Regex(pattern);
            if (rg.IsMatch(tagContent))
            {
                tagContent = rg.Replace(tagContent, "");
            }
            allMatchTags[i] = tagContent;
        };
    }
}
/*
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
                       RemoveTags(matches);
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
}*/