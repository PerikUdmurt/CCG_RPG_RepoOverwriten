using CCG.Gameplay;
using CCG.Infrastructure.Factory;
using CCG.Services.Stack;
using CCG.StaticData.Cards;
using System.Threading.Tasks;
using UnityEngine;
using Zenject;

namespace CCG.Services.Stack
{
    public interface IStackService
    {
        void SetStacksEntryPos(Vector3 position);
        StackOfCard GetStack(DeckType deckType);
        void CleanUp();
        Task<StackOfCard> CreateStack(DeckType deckType);
    }
}