using UnityEngine;
using System;
using System.Collections;

/// <summary>
/// component 'Chat'
/// 
/// </summary>
[AddComponentMenu("Networks/Chat")]
[RequireComponent(typeof(NetworkView))]
public class Chat : MonoBehaviour
{

    public GUISkin skin;

    public bool showChat = false;

    private string inputField;

    private ArrayList entries = new ArrayList();

    private Vector2 scrollPosition = Vector2.zero;

    private Rect chatWindow = new Rect(50, 50, 200, 300);


    void OnGUI()
    {
        GUI.skin = skin;
       
        if (GUI.Button(new Rect(Screen.width - 100, Screen.height - 30, 90, 20), showChat ? "Close" : "Open"))
        {
            if (showChat)
                Close();

            if (!showChat)
                StartCoroutine(Focus());

            showChat = !showChat;
        }

        if (showChat)
            chatWindow = GUI.Window(1, chatWindow, ChatWindow, "Chat"); // whisper windows?
    }


    IEnumerator Focus()
    { 
        yield return new WaitForSeconds(0.5f);
        GUI.FocusControl("Chat");
    }

    void Close()
    {
        showChat = false;
    }

    void ChatWindow(int id)
    {
        GUIStyle closeButtonStyle = GUI.skin.GetStyle("close_button");
        if (GUI.Button(new Rect(4, 4, closeButtonStyle.normal.background.width, closeButtonStyle.normal.background.height), "", "close_button"))
            Close();


        #region Scroll View

        scrollPosition =  GUILayout.BeginScrollView(scrollPosition);
        foreach (ChatEntry entry in entries)
	    {
		    GUILayout.BeginHorizontal();
		    if (!entry.isMine)
		    {
			    GUILayout.FlexibleSpace ();
			    GUILayout.Label (entry.GetMessage(), "chat_rightaligned");
		    }
		    else
		    {
                GUILayout.Label(entry.GetMessage(), "chat_leftaligned");
			    GUILayout.FlexibleSpace ();
		    }
		    GUILayout.EndHorizontal();
		    GUILayout.Space(3);
	    }
        GUILayout.EndScrollView ();

        #endregion

        if (CanSubmit())
        {
            Send(Network.player, inputField, 1);
            // todo whisper
            networkView.RPC("Send", RPCMode.Others, Network.player, inputField, 0);
            // global
            inputField = "";
        }

        GUI.SetNextControlName("Chat");
        inputField = GUILayout.TextField(inputField);
        GUI.DragWindow();
    }

    bool CanSubmit()
    {
        if (Event.current.type == EventType.keyDown && Event.current.character.ToString() == Environment.NewLine && inputField.Length > 0)
            return true;

        return false;
    }

    [RPC]
    void Send(NetworkPlayer owner, string message, int mine)
    {
        ChatEntry entry = new ChatEntry(owner, message, mine);
        entries.Add(entry);

        if (entries.Count > 50)
            entries.RemoveAt(0);

        scrollPosition.y = 1000000;
        showChat = true;
    }
}
