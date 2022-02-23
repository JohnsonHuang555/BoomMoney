using System.Collections;
using System.Collections.Generic;

/// <summary>
///  ���ո�ơA�C���]�w�����
/// </summary>
public class TestData
{
    // ��ܪ��a��
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
