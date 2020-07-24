using System.Collections;
using System.Collections.Generic;
using System.Runtime.Remoting.Messaging;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

namespace Andification
{
	public class PPHandler : MonoBehaviour
	{
		[SerializeField] private bool _testMode = false;

		private UnityEngine.Rendering.VolumeProfile _volumeProfile;
		private Bloom _bloom;
		private ChromaticAberration _chromAber;
		private Vignette _vignette;

		[SerializeField] private float _minBloom = 0.3f;
		[SerializeField] private float _minChromAber = 0f;
		[SerializeField] private float _minVignette = 0.01f;

		[SerializeField] private float _maxBloom = 3f;
		[SerializeField] private float _maxChromAber = 5f;
		[SerializeField] private float _maxVignette = 1f;

		private float _currentBloomBF = 0f;
		private float _currentChromAberBF = 0f;
		private float _currentVignetteBF = 0f;

		private float _currentBloom { get => _currentBloomBF; set => _currentBloomBF = Mathf.Clamp(value, _minBloom, _maxBloom); }
		private float _currentChromAber { get => _currentChromAberBF; set => _currentChromAberBF = Mathf.Clamp(value, _minChromAber, _maxChromAber); }
		private float _currentVignette { get => _currentVignetteBF; set => _currentVignetteBF = Mathf.Clamp(value, _minVignette, _maxVignette); }

		void Start()
		{
			_volumeProfile = GetComponent<UnityEngine.Rendering.Volume>()?.profile;
			if (!_volumeProfile) throw new System.NullReferenceException(nameof(UnityEngine.Rendering.VolumeProfile));

			_volumeProfile.TryGet<Bloom>(out _bloom);
			_volumeProfile.TryGet<ChromaticAberration>(out _chromAber);
			_volumeProfile.TryGet<Vignette>(out _vignette);

			if (null != Runtime.GameData.s_instance?.CurrentLive?.OnValueChange)
			{
				Runtime.GameData.s_instance.CurrentLive.OnValueChange += ShortColorFlash;
				Runtime.GameData.s_instance.CurrentLive.OnValueChange += ShortBloomFlash;
			}
		}

		void FixedUpdate()
		{
			if (_testMode)
				TestMode();

			float decay = Time.fixedDeltaTime * 0.01f;
			_currentBloom -= decay;
			_currentChromAber -= decay;
			_currentVignette -= decay;

			SetAll();
		}

		void OnDisable()
		{
			if (null != Runtime.GameData.s_instance?.CurrentLive?.OnValueChange)
			{
				Runtime.GameData.s_instance.CurrentLive.OnValueChange -= ShortColorFlash;
				Runtime.GameData.s_instance.CurrentLive.OnValueChange -= ShortBloomFlash;
			}
		}

		private void SetAll()
		{
			SetBloom(_currentBloom);
			SetChromAber(_currentChromAber);
			SetVignette(_currentVignette);
		}

		private void SetBloom(float newIntensity) => _bloom.intensity.Override(newIntensity);
		private void SetChromAber(float newCA) => _chromAber.intensity.Override(newCA);
		private void SetVignette(float newVignette) => _vignette.smoothness.Override(newVignette);

		void TestMode()
		{
			SetBloom(Mathf.Abs(Mathf.Sin(Time.time * Mathf.PI * 16f)));
			SetChromAber(Mathf.Abs(Mathf.Cos(Time.time)) * 16f);
			SetVignette(Mathf.Abs(Mathf.Sin(Time.time)));
		}

		private IEnumerator BloomFlash(float intensity, float duration = 4f)
		{
			for (float ticker = 0; ticker <= duration; ticker += Time.fixedDeltaTime)
			{
				_currentBloom += (ticker < duration / 2f ? 1f : -1f) * intensity * (Time.fixedDeltaTime / duration);
				yield return new WaitForFixedUpdate();
			}
		}
		private IEnumerator ChromAberFlash(float intensity, float duration = 4f)
		{
			for (float ticker = 0; ticker <= duration; ticker += Time.fixedDeltaTime)
			{
				_currentChromAber += (ticker < duration / 2f ? 1f : -1f) * intensity * (Time.fixedDeltaTime / duration);
				yield return new WaitForFixedUpdate();
			}
		}
		private IEnumerator VignetteFlash(float intensity, float duration = 4f)
		{
			for (float ticker = 0; ticker <= duration; ticker += Time.fixedDeltaTime)
			{
				_currentVignette += (ticker < duration / 2f ? 1f : -1f) * intensity * (Time.fixedDeltaTime / duration);
				yield return new WaitForFixedUpdate();
			}
		}

		public void ShortBloomFlash() => StartCoroutine(BloomFlash(_maxBloom, 0.5f));
		public void ShortColorFlash() => StartCoroutine(ChromAberFlash(1, 0.5f));
		public void ShorVignetteFlash() => StartCoroutine(VignetteFlash(_maxVignette, 0.5f));
	}
}
