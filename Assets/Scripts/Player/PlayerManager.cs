using UnityEngine;
using System.Collections;
using System.Collections.Generic;

/// <summary>
/// Managing Network Player and Sync.
/// </summary>
public class PlayerManager : MonoBehaviour
{

    public List<BasePlayer> players = new List<BasePlayer>();

    public NetworkPlayer networkPlayer;

    public StateManager currentState;
    public List<StateManager> tasks = new List<StateManager>();

    public BasePlayer currentPlayer;

    public int this[int networkID]
    {
        get
        {
            for (int i = 0; i < players.Count; i++) { 
                if (players[i].network == networkID)
                    return players[i].network;
            }
            return 0;
        }
    }

    public int this[NetworkPlayer player]
    {
        get
        {
            for (int i = 0; i < players.Count; i++) { 
                if (players[i].network == int.Parse(player.ToString()))
                    return players[i].network;
            }
            return 0;
        }
    }

    public int findPlayerWithNetworkId(NetworkPlayer player)
    {
        for (int i = 0; i < players.Count; i++)
        {
            if (players[i].network == int.Parse(player.ToString()))
                return i;
        }
        return -1;
    }

    public bool playerWithNetworkIdExist(NetworkPlayer player)
    {
        for (int i = 0; i < players.Count; i++)
        {
            if (players[i].network == int.Parse(player.ToString()))
                return true;
        }
        return false;
    }

    void Awake()
    {
        this.networkView.observed = this;
    }

    
    void Update()
    {
        if (tasks.Count == 0)
            return;

        foreach(StateManager task in tasks)
        {
            switch (task) { 
                case StateManager.NameChanged:
                    networkView.RPC("RPC_ChangePlayerName", RPCMode.AllBuffered, Network.player, currentPlayer.name);
                    break;
                case StateManager.ColorChanged:
                    networkView.RPC("RPC_ChangePlayerColor", RPCMode.AllBuffered, Network.player, currentPlayer.color.ToString());
                    break;
                case StateManager.ScoreChanged:
                    networkView.RPC("RPC_ChangePlayerScore", RPCMode.AllBuffered, Network.player, currentPlayer.playerScore);
                    break;
                case StateManager.StateChanged:
                    networkView.RPC("RPC_ChangePlayerState", RPCMode.AllBuffered, Network.player, currentPlayer.state);
                    break;
                default:
                    tasks.Remove(task);
                    break;
            }
            tasks.Remove(task);
        }
    }

    public BasePlayer getPlayer(NetworkPlayer player)
    {
        for (int i = 0; i < players.Count; i++) {
            if (players[i].network == int.Parse(player.ToString()))
                return players[i];
        }
        return currentPlayer;
    }


    void OnPlayerConnected(NetworkPlayer player)
    {
        networkView.RPC("RPC_AddPlayer", RPCMode.AllBuffered, player);
    }


    void OnPlayerDisconnected(NetworkPlayer player)
    {
        networkView.RPC("RPC_RemovePlayer", RPCMode.AllBuffered, player);
    }

    [RPC]
    void RPC_AddPlayer(NetworkPlayer player)
    {
        BasePlayer newBasePlayer = new BasePlayer(int.Parse(player.ToString()));
        players.Add(newBasePlayer);
    }

    [RPC]
    void RPC_RemovePlayer(NetworkPlayer player)
    {
        if (playerWithNetworkIdExist(player))
            players.RemoveAt(findPlayerWithNetworkId(player));
    }

    [RPC]
    void RPC_ChangePlayerName(NetworkPlayer player, string newName)
    {
        if (!playerWithNetworkIdExist(player))
            return;

        BasePlayer remotePlayer = this.getPlayer(player);
        remotePlayer.name = newName;
    }

    [RPC]
    void RPC_ChangePlayerColor(NetworkPlayer player, string color)
    {
        if (!playerWithNetworkIdExist(player))
            return;

        BasePlayer remotePlayer = this.getPlayer(player);
        remotePlayer.color = ColorHelper.ToRGB(color);
    }

    [RPC]
    void RPC_ChangePlayerScore(NetworkPlayer player, int newScore)
    {
        if (!playerWithNetworkIdExist(player))
            return;

        BasePlayer remotePlayer = this.getPlayer(player);
        remotePlayer.playerScore = newScore;
    }

    [RPC]
    void RPC_ChangePlayerState(NetworkPlayer player, int newState)
    {
        if (!playerWithNetworkIdExist(player))
            return;

        BasePlayer remotePlayer = this.getPlayer(player);
        remotePlayer.state = (StatePlayer)newState;
    }
}
