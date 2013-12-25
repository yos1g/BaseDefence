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

    void Awake()
    {
        this.networkView.observed = this;
    }
}
