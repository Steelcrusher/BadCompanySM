using BCM.PersistentData;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using BCM.Models;
using JetBrains.Annotations;

namespace BCM.Commands
{
  [UsedImplicitly]
  public class BCPlayerFiles : BCCommandAbstract
  {
    protected override void Process()
    {
      var players = new List<BCMPlayerDataFile>();
      var path = GameUtils.GetPlayerDataDir();
      var files = GetFiles(path);

      if (files != null)
      {
        foreach (var file in files)
        {
          if (file.Extension != ".ttp") continue;
          var pdf = new BCMPlayerDataFile
          {
            SteamId = file.Name.Substring(0, file.Name.Length - file.Extension.Length),
            LastWrite = file.LastWriteTimeUtc.ToUtcStr()
          };
          var player = PersistentContainer.Instance.Players[pdf.SteamId, false];
          if (player != null)
          {
            pdf.Name = player.Name;
            pdf.LastOnline = player.LastOnline.ToUtcStr();
            pdf.IsOnline = player.IsOnline;
            pdf.LastLogPos = player.LastLogoutPos?.ToString();
          }

          players.Add(pdf);
        }
      }
      if (Options.ContainsKey("min"))
      {
        SendJson(players.Select(player => new[]
          {
            player.Name, player.SteamId, player.IsOnline.ToString(), player.LastOnline, player.LastWrite, player.LastLogPos
          }
        ).ToList());
      }
      else
      {
        SendJson(players);
      }
    }

    [CanBeNull]
    private static FileSystemInfo[] GetFiles(string path)
    {
      var root = new DirectoryInfo(path);
      return !root.Exists ? null : root.GetFileSystemInfos();
    }
  }
}
