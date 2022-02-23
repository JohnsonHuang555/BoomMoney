using System.Collections;
using System.Collections.Generic;

/// <summary>
///  測試資料，遊戲設定假資料
/// </summary>
public class TestData
{
    // 選擇的地圖
    public static Overworld Overworld = Overworld.Classic;
    public static WorldSize WorldSize = WorldSize.Small;

    public static Dictionary<CharacterName, Player> GetPlayers()
    {
        Dictionary<CharacterName, Player> players = new();
        players.Add(CharacterName.Fire, new Player { Id = 1, PlayOrder = 0 });
        players.Add(CharacterName.Green, new Player { Id = 2, PlayOrder = 1 });
        return players;
    }
}
