// See https://aka.ms/new-console-template for more information
Console.WriteLine("Hello, World!");

var testes = new string[] {
    "Fri, 21 Oct 2022 23:25:59 +0000",
    "21 Oct 2022 18:23:25 -0500",
    "Fri, 21 Oct 2022 18:17:16 -0500",
    "21 Oct 2022 18:18:38 -0500",
    "21 Oct 2022 18:16:48 -0500",
    "Fri, 21 Oct 2022 19:21:07 -0400",
    "21 Oct 2022 18:16:21 -0500",
    "21 Oct 2022 18:11:30 -0500",
    "21 Oct 2022 18:10:01 -0500",
    "21 Oct 2022 18:27:37 -0500",
    "Fri, 21 Oct 2022 19:09:03 -0400",
    "Fri, 21 Oct 2022 18:16:45 -0500 (CDT)",
    "Fri, 21 Oct 2022 18:17:10 -0500 (CDT)",
    "Fri, 21 Oct 2022 18:11:26 -0400",
    "21 Oct 2022 17:59:08 -0500",
    "Fri, 21 Oct 2022 22:28:47 +0000",
    "Fri, 21 Oct 2022 16:04:02 -0700",
    "21 Oct 2022 17:35:36 -0500",
    "21 Oct 2022 18:20:02 -0500",
    "21 Oct 2022 18:05:36 -0500",
    "21 Oct 2022 18:07:23 -0500",
    "Fri, 21 Oct 2022 22:42:49 +0000",
    "Fri, 21 Oct 2022 16:03:53 -0700",
    "Fri, 21 Oct 2022 16:39:41 -0600",
    "Fri, 21 Oct 2022 17:52:32 -0500 (CDT)",
    "Fri, 21 Oct 2022 16:39:41 -0600",
    "21 Oct 2022 18:03:24 -0500",
    "Fri, 21 Oct 2022 16:39:41 -0600",
    "21 Oct 2022 17:31:42 -0500",
    "21 Oct 2022 17:15:08 -0500",
    "Fri, 21 Oct 2022 17:03:35 -0600",
    "21 Oct 2022 17:22:22 -0500",
    "Fri, 21 Oct 2022 22:17:57 +0000 (UTC)",
    "Fri, 21 Oct 2022 21:02:03 +0000",
    "Fri, 21 Oct 2022 22:13:07 +0000",
    "21 Oct 2022 17:25:15 -0500",
    "Fri, 21 Oct 2022 20:57:24 +0000",
    "21 Oct 2022 18:08:04 -0500",
    "Fri, 21 Oct 2022 22:26:41 +0000",
    "Fri, 21 Oct 2022 22:43:46 +0000",
    "Fri, 21 Oct 2022 23:10:19 -0000",
    "Fri, 21 Oct 2022 18:16:47 -0500 (CDT)",
    "Fri, 21 Oct 2022 22:26:40 +0000",
    "Fri, 21 Oct 2022 22:17:27 +0000 (UTC)",
    "Fri, 21 Oct 2022 22:17:26 +0000 (UTC)",
    "Fri, 21 Oct 2022 16:39:41 -0600",
    "Fri, 21 Oct 2022 22:17:25 +0000 (UTC)",
    "Fri, 21 Oct 2022 22:17:57 +0000 (UTC)",
    "Fri, 21 Oct 2022 22:17:27 +0000 (UTC)",
    "21 Oct 2022 18:54:24 -0400" };

int nao = 0;
int sim = 0;
foreach (var test in testes)
{
    var nova = test.Replace("(CDT)", "").Replace("(UTC)", "");
    if (DateTime.TryParse(nova, out DateTime r))
        sim++;
    else { 
        nao++;
        Console.WriteLine(nova);
        var x = DateTimeOffset.TryParse(nova, out DateTimeOffset xx);
        Console.WriteLine(x);
    }

}

Console.WriteLine(sim);
Console.WriteLine(nao);