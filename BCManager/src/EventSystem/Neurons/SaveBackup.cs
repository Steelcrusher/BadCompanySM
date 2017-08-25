﻿namespace BCM.Neurons
{
  public class SaveBackup : NeuronAbstract
  {
    public SaveBackup()
    {
    }
    public override void Fire(int b)
    {
      //create a timestamped zip archive of the save game (players, regions, settings files, etc)

      Log.Out(Config.ModPrefix + " SaveBackup");
    }
  }
}
