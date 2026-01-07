using Design;
using Game.Sim;
using UnityEngine;
using Utils;

namespace Game.View
{
    public class GameView : MonoBehaviour
    {
        [SerializeField] FighterView _fighter1;
        [SerializeField] FighterView _fighter2;
        private CharacterConfig[] _characters;

        public void Init(CharacterConfig[] characters)
        {
            _characters = characters;
            _fighter1.Init(characters[0]);
            _fighter2.Init(characters[1]);
        }

        public void Render(in GameState state)
        {
            _fighter1.Render(state.Frame, state.Fighters[0]);
            _fighter2.Render(state.Frame, state.Fighters[1]);
        }
    }
}