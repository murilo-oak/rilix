using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Unity.VisualScripting;
using UnityEngine;

namespace RilixHalloweenChallenge
{
    public class SkyboxAnimator : MonoBehaviour
    {
        [Header("Animation Settings")]
        [Min(0)]
        [SerializeField] float _animationDurationSec;

        [Header("Skybox Material")]
        [SerializeField] Material _skyboxMaterial;
        
        [Header("Start Skybox Settings")]
        [SerializeField] Cubemap _cubemapA;
        [SerializeField] Light _lightA;
        [SerializeField] float _endLightIntensityA;
        [SerializeField] Color _fogColorA;
        
        [Header("End Skybox Settings")]
        [SerializeField] Cubemap _cubemapB;
        [SerializeField] Light _lightB;
        [SerializeField] float _endLightIntensityB;
        [SerializeField] Color _fogColorB;

        public static event Action<float> OnValueChanged;
        public static event Action OnTaskCompleted;

        private CancellationTokenSource _tokenSource;


        private void Awake()
        {
            SetCubeMapTextures();
        }

        private async void OnEnable()
        {
            SetStartAnimation();
            await InterpolateValueAsync(_animationDurationSec, _tokenSource.Token);
        }

        private void Start()
        {
            OnTaskCompleted += SetEndAnimation;
            OnValueChanged += UpdateLightSceneSettings;
        }

        void SetCubeMapTextures()
        {
            _skyboxMaterial.SetTexture("_Tex", _cubemapA);
            _skyboxMaterial.SetTexture("_TexB", _cubemapB);
        }

        private void SetStartAnimation() 
        {
            SetStartSettingsSkyboxMaterial();

            SetStartSettingsLights();

            _tokenSource = new CancellationTokenSource();
        }
        
        private void SetEndAnimation() 
        { 
            _lightA.enabled = false;
        }

        private void UpdateLightSceneSettings(float interpolator)
        {
            _lightA.intensity = Mathf.Lerp(_endLightIntensityA, 0, interpolator);
            _lightB.intensity = Mathf.Lerp(0, _endLightIntensityB, interpolator);

            _skyboxMaterial.SetFloat("_Interpolator", interpolator);
            
            RenderSettings.fogColor = Vector4.Lerp(_fogColorA, _fogColorB, interpolator);
        }

        void SetStartSettingsSkyboxMaterial() 
        {
            _skyboxMaterial.SetFloat("_Interpolator", 0);
        }

        void SetStartSettingsLights() 
        {
            _lightB.intensity = 0;

            _lightA.enabled = true;
            _lightB.enabled = true;
        }

        private static async Task InterpolateValueAsync(float duration, CancellationToken token)
        {
            float elapsedTime = 0f;

            float interpolator;

            while (elapsedTime < duration)
            {
                if (token.IsCancellationRequested)
                    return;

                interpolator = Mathf.Clamp01(elapsedTime / duration);

                OnValueChanged?.Invoke(interpolator);

                elapsedTime += Time.deltaTime;
                await Task.Yield();
            }

            if (!token.IsCancellationRequested)
            {
                OnValueChanged?.Invoke(1);
                OnTaskCompleted?.Invoke();
            }
        }

        private void OnDisable()
        {
            CleanupToken();
        }
        private void OnDestroy()
        {
            CleanupToken();
        }

        private void CleanupToken()
        {
            if (_tokenSource != null && !_tokenSource.IsCancellationRequested)
            {
                _tokenSource.Cancel();
            }
            _tokenSource?.Dispose();
            _tokenSource = null;
        }
    }

}
