using UnityEngine;

public class BasePlayer 
{

    /// <summary>
    /// Network ID
    /// </summary>
    public int network;

    /// <summary>
    /// Player Name
    /// </summary>
    public string name;

    /// <summary>
    /// Player Score
    /// </summary>
    public int playerScore;

    /// <summary>
    /// Player Color
    /// </summary>
    public Color color;

    /// <summary>
    /// Player State
    /// </summary>
    public StatePlayer state;

    /// <summary>
    /// NetworkPlayer
    /// </summary>
    public NetworkPlayer networkPlayer;

    /// <summary>
    /// Creating Player Base - for using in networking sync.
    /// </summary>
    /// <param name="networkID">Network ID</param>
    /// <param name="playerName">Player Name</param>
    /// <param name="playerColor">Player Color</param>

    public BasePlayer(NetworkPlayer player)
    {
        state = StatePlayer.Respawn;
        playerScore = 0;
        network = int.Parse(player.ToString());
        networkPlayer = player;
        name = null;
        color = Color.black;
    }
}
