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

    // TODO: playser struct
    public static CharacterName[] Players = new CharacterName[] { CharacterName.Fire, CharacterName.Green }; 
}
