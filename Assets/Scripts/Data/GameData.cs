using CCG.Gameplay;
using System;
using System.Collections.Generic;

[Serializable]
public class GameData
{
    public List<CardData> cards;
    public GameData()
    {
        cards = new List<CardData>();
    }
}
