using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

/// <summary>
/// component 'MainMenu'
/// 
/// </summary>
[AddComponentMenu("Networks/MainMenu")]
[RequireComponent(typeof(NetworkView))]
public class MainMenu : BaseGuiWindow
{

	public Rect window;
	public Rect windowNetwork;

	public GUISkin skin;
	
	public bool isGameMenu = true;
	public bool isNetworkMenu = false;
	public bool isPauseMenu = false;
	public bool isSettingMenu = false;
	public bool isUserMenu = false;
	public bool isStatsMenu = false;
	public bool isFindGame = false;

	private string userName = "Player";
	private string ip = "127.0.0.1";
	private string port = "27080";
	private string serverName = "Game Defense";
	protected string gameName = "BaseDefense";

    void Start()
    {
		if (skin == null)
			enabled = false;

		this.networkView.observed = this;

		window.x = Screen.width / 2 - window.width / 2;
		window.y = Screen.height / 2 - window.height / 2 - 50.0f;

		windowNetwork.x = window.x;
		windowNetwork.y = window.y;
    }

	void Update()
	{

	}

	void HideAll()
	{
		isGameMenu = false;
		isNetworkMenu = false;
		isPauseMenu = false;
		isSettingMenu = false;
		isUserMenu = false;
		isStatsMenu = false;
		isFindGame = false;
	}

    void OnGUI()
    {
		GUI.skin = skin;

		if (isGameMenu)
		{
			window = GUI.Window(0, window, GameMenu, "");
		} else if (isNetworkMenu)
		{
			windowNetwork = GUI.Window(1, windowNetwork, NetworkMenu, "");
		}
    }

    void GameMenu(int id)
    {
		if (!isGameMenu)
			return;

		DoWindowTitle (window, "Base Defense");
		//DoLabel(new Rect(25.0f, 105.0f, window.width - 50.0f, 25.0f), "Username:");
		//userName = GUI.TextField(new Rect(window.width / 2 - 145.0f, 155.0f, 290, 66), userName);
		if (DoButton(new Rect(window.width / 2 - 100.0f, 110.0f, 200.0f, 60.0f), "Single")) 
		{
			HideAll();
			this.isGameMenu = true;
		}
		if (DoButton(new Rect(window.width / 2 - 100.0f, 160.0f, 200.0f, 60.0f), "Cooperative")) 
		{
			HideAll();
			this.isGameMenu = true;
		}
		if (DoButton(new Rect(window.width / 2 - 100.0f, 210.0f, 200.0f, 60.0f), "Multiplayer")) 
		{
			HideAll();
			this.isNetworkMenu = true;
		}
		if (DoButton(new Rect(window.width / 2 - 100.0f, 260.0f, 200.0f, 60.0f), "Settings")) 
		{
			HideAll();
			this.isSettingMenu = true;
		}
		if (DoButton(new Rect(window.width / 2 - 100.0f, 310.0f, 200.0f, 60.0f), "Exit")) 
		{
			Application.Quit();
		}
    }

	void NetworkMenu(int id)
	{	
		if(!isNetworkMenu)
			return;

		DoWindowTitle (windowNetwork, "Multiplayer");

		DoLabel(new Rect(25.0f, 105.0f, windowNetwork.width - 50.0f, 10.0f), "Name:");
		userName = GUI.TextField(new Rect(windowNetwork.width / 2 - 145.0f, 145.0f, 290, 65), userName);

		DoLabel(new Rect(35.0f, 195.0f, 200.0f, 10.0f), "Ip:");
		ip = GUI.TextField(new Rect(windowNetwork.width / 2 - 145.0f, 235.0f, 200, 65), ip);

		DoLabel(new Rect(245.0f, 195.0f, 100.0f, 10.0f), "Port:");
		port = GUI.TextField(new Rect(windowNetwork.width / 2 + 50.0f, 235.0f, 100, 65), port);

		DoLabel(new Rect(25.0f, 285.0f, windowNetwork.width - 50.0f, 10.0f), "Game Name:");
		serverName = GUI.TextField(new Rect(windowNetwork.width / 2 - 145.0f, 325.0f, 290, 65), serverName);

		if (DoButton(new Rect(windowNetwork.width / 2 - 110.0f, 395.0f, windowNetwork.width - 180.0f, 55.0f), "Create Game"))
		{
			HideAll();
			PlayerPrefs.SetString("PlayerName", userName);
			Network.InitializeServer(10, Convert.ToInt32(port), false);
			MasterServer.RegisterHost(gameName, serverName + ":" + userName);
		}

		if (DoButton(new Rect(windowNetwork.width / 2 - 110.0f, 440.0f, windowNetwork.width - 180.0f, 55.0f), "Find Game"))
		{
			HideAll();
			this.isFindGame = true;
		}

		if (DoButton(new Rect(windowNetwork.width / 2 - 110.0f, 485.0f, windowNetwork.width - 180.0f, 55.0f), "Back"))
		{
			HideAll();
			this.isGameMenu = true;
		}
	}
}
