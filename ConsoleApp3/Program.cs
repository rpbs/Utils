// See https://aka.ms/new-console-template for more information
using System.Text;

var x1 = "PGh0bWw-PGhlYWQ-PG1ldGEgaHR0cC1lcXVpdj0iQ29udGVudC1UeXBlIiBjb250ZW50PSJ0ZXh0L2h0bWw7IGNoYXJzZXQ9dXMtYXNjaWkiPjwvaGVhZD48Ym9keSBzdHlsZT0id29yZC13cmFwOiBicmVhay13b3JkOyAtd2Via2l0LW5ic3AtbW9kZTogc3BhY2U7IGxpbmUtYnJlYWs6IGFmdGVyLXdoaXRlLXNwYWNlOyIgY2xhc3M9IiI-PC9ib2R5PjwvaHRtbD4=";

Console.WriteLine(Base64UrlDecode(x1));

Console.WriteLine("");

var x2 = "PGh0bWw-PGJvZHkgc3R5bGU9IndvcmQtd3JhcDogYnJlYWstd29yZDsgLXdlYmtpdC1uYnNwLW1vZGU6IHNwYWNlOyBsaW5lLWJyZWFrOiBhZnRlci13aGl0ZS1zcGFjZTsiPjxoZWFkPjxtZXRhIGh0dHAtZXF1aXY9IkNvbnRlbnQtVHlwZSIgY29udGVudD0idGV4dC9odG1sOyBjaGFyc2V0PXVzLWFzY2lpIj48L2hlYWQ-PC9ib2R5PjwvaHRtbD4=";

Console.WriteLine(Base64UrlDecode(x2));

static string Base64UrlDecode(string data)
{
    string incoming = data.Replace('_', '/').Replace('-', '+');
    switch (data.Length % 4)
    {
        case 2: incoming += "=="; break;
        case 3: incoming += "="; break;
    }
    byte[] bytes = Convert.FromBase64String(incoming);
    string stringToClean = Encoding.ASCII.GetString(bytes);
    return stringToClean.Replace("=\"", "='").Replace("\"", "'").Replace("\r\n", "").Replace("\t", "");
}

var base1 = "SGVsbG8=";
var base2 = "V29ybGQ=";
var array1 = Convert.FromBase64String(base1);
var array2 = Convert.FromBase64String(base2);
var comb = Combine(array1, array2);
var data = Encoding.Default.GetString(comb);
Console.WriteLine(data);

static byte[] Combine(byte[] first, byte[] second)
{
    return first.Concat(second).ToArray();
}