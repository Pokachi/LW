using System;
using UnityEngine;

[Serializable]
public class Player : ScriptableObject {
    public PlayerColor color;
    public int id;

    public Player(int id) {
        this.id = id;
    }
}
