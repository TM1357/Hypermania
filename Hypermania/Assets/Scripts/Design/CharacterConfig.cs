using Design.Animation;
using Game;
using UnityEngine;

namespace Design
{
    [CreateAssetMenu(menuName = "Hypermania/Character Config")]
    public class CharacterConfig : ScriptableObject
    {
        public Character Character;
        public AnimatorOverrideController AnimationController;
        public float Speed;
        public float JumpVelocity;
        public HitboxData Walk;
        public HitboxData Idle;
        public HitboxData LightAttack;
        public HitboxData Jump;
        // TODO: many more
    }
}
