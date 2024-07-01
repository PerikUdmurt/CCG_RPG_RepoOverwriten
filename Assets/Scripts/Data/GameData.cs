using CCG.Data;
using System;
using System.Collections.Generic;

[Serializable]
public class GameData
{
    public List<CardData> availableCards;
    public List<CardData> cardReset;

    public GameData()
    {
        availableCards = new List<CardData>();
        cardReset = new List<CardData>();
    }
}
