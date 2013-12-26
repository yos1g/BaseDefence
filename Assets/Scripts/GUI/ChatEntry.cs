using UnityEngine;

class ChatEntry
{
    public NetworkPlayer sender;
    public string message;
    public bool isMine;

    public ChatEntry(NetworkPlayer nSender, string nMessage, int nIsMine)
    {
        sender = nSender;
        message = nMessage;
        isMine = (nIsMine == 0 ? true : false);
    }

    public string getSenderName()
    {
        return GameObject.FindGameObjectWithTag(GameTags.PlayerManager.GetTagName()).GetComponent<PlayerManager>().getPlayer(sender).name.ToString();
    }
}