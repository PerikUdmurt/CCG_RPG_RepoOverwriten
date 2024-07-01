using CCG.Gameplay;
using CCG.Infrastructure.AssetProvider;
using CCG.Infrastructure.Factory;
using CCG.StaticData.Cards;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

namespace CCG.Services.Stack
{
    public class StackService : IStackService
    {
        private readonly IAssetProvider _assetProvider;
        private readonly CustomFactory<StackOfCard> _stackFactory;
        private Vector3 _stackEntryPos;
        private Dictionary<DeckType, StackOfCard> _stacks;

        public StackService(IAssetProvider assetProvider, CustomFactory<StackOfCard> stackFactory)
        {
            _stacks = new Dictionary<DeckType, StackOfCard>();
            _assetProvider = assetProvider;
            _stackFactory = stackFactory;
        }

        public async Task<StackOfCard> CreateStack(DeckType deckType)
        {
            GameObject resource = await _stackFactory.CreatePrefab(AssetPath.Stack);
            resource.TryGetComponent(out StackOfCard stack);
            stack.SetImage(await _assetProvider.Load<Sprite>($"{deckType}Stack"));
            _stacks.Add(deckType, stack);
            resource.transform.position = new Vector3(_stackEntryPos.x + (0.65f * (_stacks.Count - 1)), _stackEntryPos.y, _stackEntryPos.z);
            return stack;
        }

        public StackOfCard GetStack(DeckType deckType) => _stacks[deckType];

        public void SetStacksEntryPos(Vector3 position)
        {
            _stackEntryPos = position;
        }

        public void CleanUp()
        {
            foreach (var stack in _stacks.Values)
            {
                GameObject.Destroy(stack.gameObject);
            }
            _stacks.Clear();
        }
    }
}