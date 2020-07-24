using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DissolveAccessor : MonoBehaviour
{
#pragma warning disable CS0414

	Material _material;

	float _fade = 1f;
	int scale = 6;
	Color color;

	// Start is called before the first frame update
	void Start()
	{
		_material = GetComponent<SpriteRenderer>()?.material;
	}

	// Update is called once per frame
	void Update()
	{
		_fade = Mathf.Abs(Mathf.Sin(Time.time));
		SetFade(_fade);
	}

	public void SetFade(float fade) => _material.SetFloat("_Fade", fade);
	public void SetNoiseScale(int scale) => _material.SetFloat("_NoiseScale", scale);
	public void SetGlowColor(Color color) => _material.SetColor("_GlowColor", color);
}
