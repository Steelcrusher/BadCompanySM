using BCM.Models;

namespace BCM.Commands
{
  public class ListPlayersBag : ListPlayers
  {
    public override void displayPlayer(PlayerInfo _pInfo)
    {
      string output = "\n";
      output += new ClientInfoList(_pInfo).DisplayShort();
      output += new BagList(_pInfo).Display();

      SendOutput(output);
    }
  }
}
