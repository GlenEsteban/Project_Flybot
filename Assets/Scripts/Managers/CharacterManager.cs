using System.Collections.Generic;
using UnityEngine;

public class CharacterManager : MonoBehaviour {
    // Instances
    public static CharacterManager Instance { get; private set; }

    // Item variables
    private List<Character> _players= new List<Character>();
    private List<Character> _characters = new List<Character>();
    public IReadOnlyList<Character> Followers => _players.AsReadOnly();
    public IReadOnlyList<Character> Enemies => _characters.AsReadOnly();

    // Pickup Items List Public Methods
    public void Add(Character character) {
        _characters.Add(character);
    }
    public void Remove(Character character) {
        _characters.Remove(character);
    }
    public bool Contains(Character character) {
        return _characters.Contains(character);
    }

    private void Awake() {
        if (Instance != null &&  Instance != this) {
            Destroy(this);
        }
        else {
            Instance = this;
        }
    }
}