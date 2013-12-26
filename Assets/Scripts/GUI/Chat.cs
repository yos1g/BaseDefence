using UnityEngine;
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

    private string inputField = "";

    private ArrayList entries = new ArrayList();
    private Vector2 scrollPosition = Vector2.zero;

    private Rect chatWindow = new Rect(50, 50, 200, 300);


    void OnGUI()
    {
        GUI.skin = skin;
        // todo chat
    }


}
