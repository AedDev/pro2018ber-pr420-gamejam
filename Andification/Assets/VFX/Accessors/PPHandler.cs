using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

public class PPHandler : MonoBehaviour
{
	private UnityEngine.Rendering.VolumeProfile _volumeProfile;
	private Bloom _bloom;
	private ChromaticAberration _chromAber;
	private Vignette _vignette;

	void Start()
	{
		_volumeProfile = GetComponent<UnityEngine.Rendering.Volume>()?.profile;
		if (!_volumeProfile) throw new System.NullReferenceException(nameof(UnityEngine.Rendering.VolumeProfile));

		_volumeProfile.TryGet<Bloom>(out _bloom);
		_volumeProfile.TryGet<ChromaticAberration>(out _chromAber);
		_volumeProfile.TryGet<Vignette>(out _vignette);
	}

	void Update()
	{
		SetBloom(Mathf.Abs(Mathf.Sin(Time.time * Mathf.PI * 16f)));
		SetChromAber(Mathf.Abs(Mathf.Cos(Time.time))*16f);
		SetVignette(Mathf.Abs(Mathf.Sin(Time.time)));
	}

	public void SetBloom(float newIntensity) => _bloom.intensity.Override(newIntensity);
	public void SetChromAber(float newCA) => _chromAber.intensity.Override(newCA);
	public void SetVignette(float newVignette) => _vignette.intensity.Override(newVignette);
}
