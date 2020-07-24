using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TheBigShaderAccessor : MonoBehaviour
{
#pragma warning disable CS0414

	[SerializeField] private bool _testMode = true;

	Material _material;

	#region Dissolve
	float _fade = 1f;
	int _noiseScale = 6;
	Color _glowColor;
	#endregion

	#region ColorMove
	int _displacement = 1;
	int _displacementScale = 6;
	#endregion

	void Start()
	{
		_material = GetComponent<SpriteRenderer>()?.material;
	}

	void Update()
	{
		if (_testMode)
			TestMode();
	}

	#region Dissolve
	public void SetFade(float fade) => _material.SetFloat("_Fade", fade);
	public void SetNoiseScale(int noiseScale) => _material.SetFloat("_NoiseScale", noiseScale);
	public void SetGlowColor(Color glowColor) => _material.SetColor("_GlowColor", glowColor);
	#endregion

	#region ColorMove
	public void SetDisplacement(int displacement) => _material.SetInt("_Displacement", displacement);
	#endregion

	private void TestMode()
	{
		_fade = Mathf.Abs(Mathf.Sin(Time.time));
		SetFade(_fade);

		_displacement = (int)(Mathf.Sin(Time.time) * 64f);
		SetDisplacement(_displacement);
	}


	private IEnumerator BloomFlash(float intensity, float duration = 4f)
	{
		for (float ticker = 0; ticker <= duration; ticker += Time.fixedDeltaTime)
		{
			//_currentBloom += (ticker < duration / 2f ? 1f : -1f) * intensity * (Time.fixedDeltaTime / duration);
			yield return new WaitForFixedUpdate();
		}
	}

	//public void ShortBloomFlash() => StartCoroutine(BloomFlash(_maxBloom, 0.2f));
}
