using BCM.Models;
using JetBrains.Annotations;

namespace BCM.Commands
{
  [UsedImplicitly]
  public class BCPlayersId : BCPlayers
  {
    protected override void Process()
    {
      if (Options.ContainsKey("filter"))
      {
        SendOutput("Error: Can't set filters on this alias command");
        SendOutput(GetHelp());

        return;
      }

      var filters = BCMPlayer.StrFilters.EntityId;

      if (Options.ContainsKey("n"))
      {
        filters += "," + BCMPlayer.StrFilters.Name;
      }

      if (Options.ContainsKey("s"))
      {
        filters += "," + BCMPlayer.StrFilters.SteamId;
      }

      Options.Add("filter", filters);
      var cmd = new BCPlayers();
      cmd.Process(Options, Params);
    }
  }
}
