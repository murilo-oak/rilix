using UnityEngine;
using UnityEngine.VFX;
using System.Collections.Generic;

namespace RilixHalloweenChallenge
{
    [System.Serializable]
    public class AnimatorData
    {
        public Animator animator;
        public string animationState;
    }

    [System.Serializable]
    public class AudioData
    {
        public AudioSource source;
        public AudioClip clip;
    }

    [System.Serializable]
    public class VisualEffectData
    {
        public ParticleSystem particleEffect;
        public VisualEffect visualEffect;
    }

    public class EventHandler : MonoBehaviour
    {
        [Header("Event Lifetime Settings")]
        [SerializeField] private float _lifeTime = 5f;

        [Header("Animation Settings")]
        [SerializeField] private List<AnimatorData> _animations = new List<AnimatorData>();
        [SerializeField] private float _animationDelay = 0f;

        [Header("VFXs")]
        [SerializeField] private List<VisualEffectData> _visualEffects = new List<VisualEffectData>();
        [SerializeField] private float _visualEffectDelay = 0f;

        [Header("Sounds")]
        [SerializeField] private List<AudioData> _audioDataList = new List<AudioData>();
        [SerializeField] private float _soundDelay = 0f;

        public void PlayEvent()
        {
            if (_animations.Count > 0)
                Invoke(nameof(PlayAnimations), _animationDelay);

            if (_visualEffects.Count > 0)
                Invoke(nameof(PlayVisualEffects), _visualEffectDelay);

            if (_audioDataList.Count > 0)
                Invoke(nameof(PlaySounds), _soundDelay);

            Invoke(nameof(DisableObject), _lifeTime);
        }

        private void PlayAnimations()
        {
            foreach (var animation in _animations)
            {
                if (animation.animator != null && !string.IsNullOrEmpty(animation.animationState))
                {
                    animation.animator.Play(animation.animationState);
                }
            }
        }

        private void PlayVisualEffects()
        {
            foreach (var effect in _visualEffects)
            {
                if (effect.particleEffect != null)
                    effect.particleEffect.Play();

                if (effect.visualEffect != null)
                    effect.visualEffect.Play();
            }
        }

        private void PlaySounds()
        {
            foreach (var audioData in _audioDataList)
            {
                if (audioData.source != null && audioData.clip != null)
                {
                    audioData.source.PlayOneShot(audioData.clip);
                }
            }
        }

        private void DisableObject()
        {
            gameObject.SetActive(false);
        }
    }
}
