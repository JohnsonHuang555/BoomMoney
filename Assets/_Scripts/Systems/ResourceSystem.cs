using System.Collections.Generic;
using System.Linq;
using UnityEngine;

/// <summary>
/// One repository for all scriptable objects. Create your query methods here to keep your business logic clean.
/// I make this a MonoBehaviour as sometimes I add some debug/development references in the editor.
/// If you don't feel free to make this a standard class
/// </summary>
public class ResourceSystem : StaticInstance<ResourceSystem>
{
    public List<ScriptableExampleHero> ExampleHeroes { get; private set; }
    private Dictionary<ExampleHeroType, ScriptableExampleHero> _ExampleHeroesDict;

    public List<ScriptableCharacter> Characters { get; private set; }
    private Dictionary<CharacterType, ScriptableCharacter> CharactersDict;

    protected override void Awake()
    {
        base.Awake();
        AssembleResources();
    }

    private void AssembleResources()
    {
        Characters = Resources.LoadAll<ScriptableCharacter>("Characters").ToList();
        CharactersDict = Characters.ToDictionary(r => r.CharacterType, r => r);
    }

    public ScriptableExampleHero GetExampleHero(ExampleHeroType t) => _ExampleHeroesDict[t];
    public ScriptableExampleHero GetRandomHero() => ExampleHeroes[Random.Range(0, ExampleHeroes.Count)];
    public ScriptableCharacter GetCharacter(CharacterType c) => CharactersDict[c];
}
