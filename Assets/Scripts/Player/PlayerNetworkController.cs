using UnityEngine;
using System.Collections;

[RequireComponent(typeof(NetworkView))]
public class PlayerNetworkController : MonoBehaviour
{

    #region player properties: Position, Rotation, &PlayerManager

    private float syncLastTime = 0f;
    private float syncDelay = 0f;
    private float syncTime = 0f;

    private Vector3 syncStartPosition = Vector3.zero;
    private Vector3 syncEndPosition = Vector3.zero;

    private Quaternion syncStartBodyRotation = Quaternion.identity;
    private Quaternion syncEndBodyRotation = Quaternion.identity;

    private Quaternion syncStartTowerRotation = Quaternion.identity;
    private Quaternion syncEndTowerRotation = Quaternion.identity;

    public PlayerManager pManager;

    #endregion


    void Start()
    {
        if (!networkView.isMine)
            return;

        this.pManager = GameObject.FindGameObjectWithTag(GameTags.PlayerManager.GetTagName()).GetComponent<PlayerManager>();
		GameObject.FindGameObjectWithTag(GameTags.ChatManager.GetTagName()).GetComponent<Chat>().Enable(); // enable chat for client
    }


    void Awake()
    {
        this.networkView.observed = this;
    }


    void Update()
    {
        Synchronization();
    }


    /// <summary>
    /// Only for RemotePlayers
    /// </summary>
    void Synchronization()
    {
        if (networkView.isMine)
            return;

        syncTime += Time.deltaTime;
        transform.position = Vector3.Slerp(syncStartPosition, syncEndPosition, syncTime / syncDelay);
        transform.rotation = Quaternion.Slerp(syncStartBodyRotation, syncEndBodyRotation, syncTime / syncDelay);

        /// todo: Sync for tower rotation.
    }


    void OnSerializeNetworkView(BitStream stream, NetworkMessageInfo info)
    {
        Vector3 syncPosition = Vector3.zero;
        Quaternion syncRotation = Quaternion.identity;
        Quaternion syncTowerRotation = Quaternion.identity;

        if (stream.isWriting)
        {
            syncPosition = transform.position;
            syncRotation = transform.rotation;
            // syncTowerRotation = ?
            //pManager.getPlayer(Network.player).playerScore;

            stream.Serialize(ref syncPosition);
            stream.Serialize(ref syncRotation);
        }
        else
        {
            stream.Serialize(ref syncPosition);
            stream.Serialize(ref syncRotation);

            syncTime = 0f;
            syncDelay = Time.time - syncLastTime;
            syncLastTime = Time.time;

            syncStartPosition = transform.position;
            syncEndPosition = syncPosition;

            syncStartBodyRotation = transform.rotation;
            syncEndBodyRotation = syncRotation;
        }

    }

    
}