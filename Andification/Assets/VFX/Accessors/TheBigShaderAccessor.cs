using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TheBigShaderAccessor : MonoBehaviour
{
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
}
