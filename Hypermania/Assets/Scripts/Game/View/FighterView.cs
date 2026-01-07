using System.Collections.Generic;
using Design;
using Game.Sim;
using UnityEngine;
using Utils;

namespace Game.View
{
    [RequireComponent(typeof(SpriteRenderer), typeof(Animator))]
    public class FighterView : MonoBehaviour
    {
        private Animator _animator;
        private SpriteRenderer _spriteRenderer;
        private CharacterConfig _characterConfig;

        private void Awake()
        {
            _animator = GetComponent<Animator>();
            _animator.speed = 0f;

            _spriteRenderer = GetComponent<SpriteRenderer>();
        }

        public void Init(CharacterConfig characterConfig)
        {
            _characterConfig = characterConfig;
            _animator.runtimeAnimatorController = characterConfig.AnimationController;
        }

        public void Render(Frame frame, in FighterState state)
        {
            Vector3 pos = transform.position;
            pos.x = state.Position.x;
            pos.y = state.Position.y;
            transform.position = pos;

            _spriteRenderer.flipX = state.FacingDirection.x < 0f;

            int diffFrames = frame - state.ModeSt;
            int totalFrames = _characterConfig.Walk.TotalTicks;
            string animation = GetAnimationFromState(frame, state, out var normalized);

            // by default loop anims, the fighter state should not remain in a move state for more than the move animation has
            normalized -= Mathf.Floor(normalized);

            _animator.Play(animation, 0, normalized);
            _animator.Update(0f); // force pose evaluation this frame while paused
        }

        private string GetAnimationFromState(Frame frame, in FighterState state, out float duration)
        {
            if (state.Mode == FighterMode.Attacking)
            {
                if (state.AttackType == FighterAttackType.Light)
                {
                    duration = (float)(frame - state.ModeSt) / _characterConfig.LightAttack.TotalTicks;
                    return "LightAttack";
                }
            }

            if (state.Mode == FighterMode.Neutral)
            {
                if (state.Location == FighterLocation.Airborne)
                {
                    duration = (float)(frame - state.LocationSt) / _characterConfig.Jump.TotalTicks;
                    return "Jump";
                }
                if (state.Velocity.magnitude > 0.01f)
                {
                    duration = (float)(frame - state.ModeSt) / _characterConfig.Walk.TotalTicks;
                    return "Walk";
                }
            }
            duration = (float)(frame - state.ModeSt) / _characterConfig.Idle.TotalTicks;
            return "Idle";
        }
    }
}
