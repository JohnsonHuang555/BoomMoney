using UnityEngine;

public class UnitManager : StaticInstance<UnitManager>
{
    public void SpawnPlayers()
    {
        //var players = new string[] { "Fire" };

        //foreach (var player in players)
        //{

        //}
        SpawnUnit(CharacterType.Fire, new Vector3(1, 0, 0));
    }

    private void SpawnUnit(CharacterType c, Vector3 pos)
    {
        var fireScriptable = ResourceSystem.Instance.GetCharacter(c);
        var spawned = Instantiate(fireScriptable.Prefab, pos, Quaternion.identity);

        // Apply possible modifications here such as potion boosts, team synergies, etc
        var stats = fireScriptable.BaseStats;

        spawned.SetStats(stats);
    }
}
