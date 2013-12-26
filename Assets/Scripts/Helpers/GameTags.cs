using System;

public enum GameTags
{
    PlayerManager,
    Player,
    RemotePlayer
}

/// <summary>
/// Before using> All tags must be defined in Unity TagManager
/// </summary>
public static class GameTagsExtensions
{
    public static string GetTagName(this GameTags val)
    {
        return Enum.GetName(typeof(GameTags), val);
    }
}

