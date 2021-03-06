﻿using UnityEngine;
using System;
using System.Collections;

/// <summary>
/// component 'Chat'
/// 
/// </summary>
[AddComponentMenu("Networks/Chat")]
[RequireComponent(typeof(NetworkView))]
public class Chat : BaseGuiWindow
{

    public GUISkin skin;

    public bool showChat = false;

    private string inputField = "";

    private ArrayList entries = new ArrayList();

    private Vector2 scrollPosition = Vector2.zero;

    private Rect chatWindow = new Rect(50, 50, 500, 600);

	private bool needFocus = false;

	private PlayerManager pManager;

	void Start()
	{
		if (skin == null) {
			Debug.LogWarning("Chat Manager is disabled: skin is null.");
		}
		pManager = GameObject.FindGameObjectWithTag(GameTags.PlayerManager.GetTagName()).GetComponent<PlayerManager>();
		enabled = false;
	}


	public void Enable()
	{
		enabled = true;
	}

    void OnGUI()
    {

        GUI.skin = skin;
		if (GUI.Button(new Rect(25, 25, 75, 25), showChat ? "Close" : "Open"))
		{
			showChat = !showChat;

            if (!showChat)
                Close();

            if (showChat)
                StartCoroutine(Focus());
        }

        if (showChat)
            chatWindow = GUI.Window(1, chatWindow, ChatWindow, ""); // whisper windows?
    }


    IEnumerator Focus()
    { 
        yield return new WaitForSeconds(.5f);
		needFocus = true;
    }

    void Close()
    {
        showChat = false;
    }

    void ChatWindow(int id)
    {
        //GUIStyle closeButtonStyle = GUI.skin.GetStyle("close_button");
        //if (GUI.Button(new Rect(4, 4, closeButtonStyle.normal.background.width, closeButtonStyle.normal.background.height), "", "close_button"))
        //    Close();

		//DoLabel(new Rect(0,0, 100, 100), "Chat");
		DoWindowTitle (chatWindow, "Chat");
        #region Scroll View
        scrollPosition =  GUILayout.BeginScrollView(scrollPosition);
        foreach (ChatEntry entry in entries)
	    {
		    GUILayout.BeginHorizontal();
		    if (!entry.isMine)
		    {
			    GUILayout.FlexibleSpace ();
			    //GUILayout.Label (entry.GetMessage(), "chat_rightaligned");
				GUILayout.Label (entry.GetMessage());
		    }
		    else
		    {
                //GUILayout.Label(entry.GetMessage(), "chat_leftaligned");
				GUILayout.Label (entry.GetMessage());
				//GUILayout.FlexibleSpace ();
		    }
		    GUILayout.EndHorizontal();
		    GUILayout.Space(3);
	    }
        GUILayout.EndScrollView ();

        #endregion

        if (CanSubmit())
        {
			Debug.Log("Tadam");
            Send(Network.player, inputField, 1);
			//MessageManager(inputField, 0);
        }

        GUI.SetNextControlName("Chat");
        inputField = GUILayout.TextField(inputField);
        GUI.DragWindow();

		if (needFocus)
		{
			GUI.FocusControl("Chat");
			needFocus = false;
		}
    }

    bool CanSubmit()
    {
        if (Event.current.type == EventType.keyDown && Event.current.character.ToString () == Environment.NewLine && inputField.Length > 0)
			return true;
				
        return false;
    }

	void MessageManager(string inputField, int mine)
	{
        string[] cmd = inputField.Split(':');
		if (cmd.Length == 0)
			networkView.RPC("Send", RPCMode.Others, Network.player, inputField, mine);

		if (pManager.playerWithNameExist(cmd[0].ToString())) {
			string remotePlayerName = cmd[0].ToString();
			cmd[0] = pManager.getPlayer(Network.player).name;
            string message = string.Join("/", cmd).ToString();
            networkView.RPC("Send", pManager[remotePlayerName].networkPlayer, Network.player, message, mine);
		}

		inputField = "";
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
