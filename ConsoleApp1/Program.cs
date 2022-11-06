using Google.Apis.Auth.OAuth2;
using Google.Apis.Gmail.v1;

String serviceAccountEmail =
    "33546938917-n0j2c5j7prlea42cj8cspv5usvj0m1i9.apps.googleusercontent.com";


string userEmail1 = "emailreadingtest1@conservice.com";
string userEmail2 = "emailreadingtest2@conservice.com";


ServiceAccountCredential credential1 = new(
    new ServiceAccountCredential.Initializer(serviceAccountEmail)
    {
        User = userEmail1,
        Scopes = new[] { "https://www.googleapis.com/auth/gmail.readonly" }
    }.FromPrivateKey(GetPrivateKey())
);

ServiceAccountCredential credential2 = new(
    new ServiceAccountCredential.Initializer(serviceAccountEmail)
    {
        User = userEmail2,
        Scopes = new[] { "https://www.googleapis.com/auth/gmail.readonly" }
    }.FromPrivateKey(GetPrivateKey())
);



if (credential1.RequestAccessTokenAsync(CancellationToken.None).Result)
{
    GmailService gs = new(
        new Google.Apis.Services.BaseClientService.Initializer()
        {
            HttpClientInitializer = credential1
        }
    );

    var msgList = gs.Users.Messages.List("me");
    //msgList.LabelIds = "INBOX";
    //msgList.Q = "is:read";
    msgList.MaxResults = 50;
    var result = msgList.Execute();


    foreach (var item in result.Messages)
    {
        var m = gs.Users.Messages.Get("me", item.Id).Execute();
        var sub = m.Payload.Headers.First(h => h.Name == "Subject").Value;
        Console.WriteLine(sub);
    }
}

Console.WriteLine("---------------------");

if (credential2.RequestAccessTokenAsync(CancellationToken.None).Result)
{
    GmailService gs = new(
        new Google.Apis.Services.BaseClientService.Initializer()
        {
            HttpClientInitializer = credential2
        }
    );

    var msgList = gs.Users.Messages.List("me");
    //msgList.LabelIds = "INBOX";
    //msgList.Q = "is:read";
    msgList.MaxResults = 50;
    var result = msgList.Execute();


    foreach (var item in result.Messages)
    {
        var m = gs.Users.Messages.Get("me", item.Id).Execute();
        var sub = m.Payload.Headers.First(h => h.Name == "Subject").Value;
        Console.WriteLine(sub);
    }
}

static string GetPrivateKey()
{
    return "-----BEGIN PRIVATE KEY-----\nMIIEvQIBADANBgkqhkiG9w0BAQEFAASCBKcwggSjAgEAAoIBAQDuSq3hnAkFw3WD\nvzM7XS7Hmu8JiwZEaS/QSXd/BmT7xJZzarjGhSZy4ETqwANPdgplDn/F0pVCQvG7\nooOL2TBp8kHk/u87QNkK/c6veqx241G/12973yk/Dtdzlv7GjRGxOIKmMjE9iIXr\nAvvMyuuRuVnq2cGa3MaU5RmMCBRSC6Qo0XNCE7wT2r+MsLRyR2c1P8v0PGDt6f0G\nI139tWczQ4IusjO7867AQoijgnY7EgxS5PsiFBPOfuqIQiXLPvsuV7bTDz/RsRuZ\npGLyXUVgDgow/StqdBbfnrKo/sQUdn+t+cV9irzU/RKJYlpwNtXEDwJM4Md8va/Z\nz1c5NoNdAgMBAAECggEAG4mCxZcoDcP0MRjn3Tzb+pIjfIlV3JMBRQMwbXv6MIQb\n1NOo7bwWYOEc5bnxx/1+nwYJav12ZTViRQo3RHKBX0TxK3rwf3rzegsxluLIymZj\nnJVTW0/DLvfSmxeAcsZ7nGzI4FbdjNxZiXqLSbPLfEgcpYCrmG9Z/XvqM139pytb\nIrHIDSMn6hFBwj6ni/Od7gxB+wnHHH61eKxXQvL4x4anbSNUQ2l3ExdnMtEtVCNY\n47APUhd6C+3JQ6K80pvsUItssY+9V70ylaEHcArqSvDM+PbCFMU5KiKIqDCv4r9m\ns16Upn6P3wwb5h6TtC26ZMynWVPRMaF3M6pN5kz7KwKBgQD6LzvRFDPcSxtajPSC\nochW7aclolvQ4XIsP6p2VeqTFOeqXU/XuylrPlnvvkiI4ugWKdysQ84MOzfmpJFn\nk2Rlp14t0Y3Z/NkFCsmmsd4D2QI2CZCRDxke/0DpPlXfqKx0ALePM8QP7xdaU4jU\ne7QoqLUT0EcQlYFCoZCtC7ndMwKBgQDz1KzqMn+WUyCmIrB9PXxQDwFZw9lbmHcj\n//4r+f21uwibZ1r6GqhpUCtqBXpIBlK51tyAFDYr92egrtc+uNE9iqJaAStxPYca\ntGNGzeEVsOx/RJ8Vt3BVWA2Djp0PGS8mPG9JVUdL+9o6ix3CO4OWHtkkA1Agaw8J\nPuL9yJd9LwKBgQDUOUcLh5CqeUe92z36phQUsUoNYZlzKhq4sFUARsdSdvRBjyAQ\nhyKOZ3jRJt/OxnRLgL1Bn87kj0NPMiBp9RzAImaAhXdIB/VsEoeWEOGc7/OxNRHe\nCymrIqnEcW1U6XdIe+c54ZaaUZ3EsIGshTcBGoNoSFBQIBJjHDxmnkDPAwKBgHrc\nvHCaF34ozvfH3QSOedhhZz3TGPA1iDskn++K3VO4NeMQlmAWFKxUUDvxHKe0P/US\nljMXfCB1XssxrpYVTKHk/F+73ScHaM+E+81Oqk9iwhhy/HJV542pe83Bi9RyzTZI\nF49tICrichkzxj/7+xcm3iVuYe6j44S2ImzcqbDTAoGAdSJdXyCRk3XzDghmwHtd\n7/EjT/sLZrE0fSjIjvShvwsnGLk8fx+xRxbVuioHwyAGY256XtpwAC11U5Ie7KGg\nfbpyi5Wair1SayOMOSkHLxx8PQ8D+QuOhHisbnWWBByRfq4KEcE3icGFpULdOM15\nrkuN84bV1rNroGRo/LNaKjI=\n-----END PRIVATE KEY-----\n";
}

//AIzaSyAKqi-ZvgMStNjxsRjsChs7chb1T4-6b4s
//AIzaSyBmJisjNauyQlU4i8UxQGUcXsGrtIXscwo
