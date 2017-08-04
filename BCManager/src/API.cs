﻿using BCM.PersistentData;
using System;
using System.Collections.Generic;
using System.Reflection;

namespace BCM
{
  public class API : ModApiAbstract
  {
    public static EntitySpawner entitySpawner = new EntitySpawner();
    public static bool IsAlive = false;

    public API()
    {
      Config.Init();
      if (Config.logCache)
      {
        LogCache.Instance.GetType();
      }
      Heartbeat.Start();
    }

    public override void GameUpdate()
    {
      if (IsAlive)
      {
        entitySpawner.ProcessSpawnQueue();
      }
    }

    public override void GameAwake()
    {
      StateManager.Awake();
      IsAlive = true;
    }

    public override void GameShutdown()
    {
      StateManager.Shutdown();
    }

    public override void SavePlayerData(ClientInfo _cInfo, PlayerDataFile _playerDataFile)
    {
      DataManager.SavePlayerData(_cInfo, _playerDataFile);
    }

    public override void PlayerLogin(ClientInfo _cInfo, string _compatibilityVersion)
    {

    }

    public override void PlayerSpawning(ClientInfo _cInfo, int _chunkViewDim, PlayerProfile _playerProfile)
    {
      try
      {
        var player = PersistentContainer.Instance.Players[_cInfo.playerId, true];
        if (player != null)
        {
          player.SetOnline(_cInfo);
        }
        PersistentContainer.Instance.Save();
      }
      catch (Exception e)
      {
        Log.Out(Config.ModPrefix + " Error in " + GetType().Name + "." + MethodBase.GetCurrentMethod().Name + ": " + e);
      }
    }

    public override void PlayerDisconnected(ClientInfo _cInfo, bool _bShutdown)
    {
      try
      {
        Player p = PersistentContainer.Instance.Players[_cInfo.playerId, true];
        if (p != null)
        {
          p.SetOffline();
        }
        else
        {
          //Log.Out("" + Config.ModPrefix + " Disconnected player not found in client list...");
        }
        PersistentContainer.Instance.Save();
      }
      catch (Exception e)
      {
        Log.Out("" + Config.ModPrefix + " Error in " + GetType().Name + "." + MethodBase.GetCurrentMethod().Name + ": " + e);
      }
    }

    //public override bool ChatMessage(ClientInfo _cInfo, EnumGameMessages _type, string _msg, string _mainName, bool _localizeMain, string _secondaryName, bool _localizeSecondary)
    //{
    //  return ChatHookExample.Hook(_cInfo, _type, _msg, _mainName);
    //}

    //public override void CalcChunkColorsDone(Chunk _chunk) {
    //}

    //public override void GameStartDone() {
    //}

    public override void PlayerSpawnedInWorld(ClientInfo _cInfo, RespawnType _respawnReason, Vector3i _pos)
    {
      //_cInfo.SendPackage(new NetPackageConsoleCmdClient("dm", true));
      //Log.Out(Config.ModPrefix + " Player Spawned: " + _cInfo.entityId + " @" + _pos.x.ToString() + " " + _pos.y.ToString() + " " + _pos.z.ToString());
    }

  }
}
