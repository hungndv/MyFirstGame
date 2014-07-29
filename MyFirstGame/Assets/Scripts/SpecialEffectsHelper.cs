using UnityEngine;
using System.Collections;

/// <summary>
/// Creating instance of particles from code with no effort
/// </summary>
public class SpecialEffectsHelper : MonoBehaviour
{
	/// <summary>
	/// Singleton
	/// </summary>
	public static SpecialEffectsHelper Instance;
	public ParticleSystem smokeEffect;

	void Awake ()
	{
		// Register the singleton
		if (Instance != null) {
			Debug.LogError ("Multiple instances of SpecialEffectsHelper!");
		}
		
		Instance = this;
	}

	/// <summary>
	/// Create an explosion at the given location
	/// </summary>
	/// <param name="position"></param>
	public void Explosion (Vector3 position)
	{
		// Smoke on the water
		Instantiate (smokeEffect, position);
		
		// Tu tu tu, tu tu tudu
		
		// Fire in the sky
		//instantiate (fireEffect, position);
	}

	// Use this for initialization
	void Start ()
	{
	
	}
	
	// Update is called once per frame
	void Update ()
	{
	
	}

	/// <summary>
	/// Instantiate a Particle system from prefab
	/// </summary>
	/// <param name="prefab"></param>
	/// <returns></returns>
	private ParticleSystem Instantiate (ParticleSystem prefab, Vector3 position)
	{
		ParticleSystem newParticleSystem = Instantiate (
			prefab,
			position,
			Quaternion.identity
		) as ParticleSystem;

		newParticleSystem.renderer.sortingLayerName = "Character Layer";

		// Make sure it will be destroyed
		Destroy (
			newParticleSystem.gameObject,
			newParticleSystem.startLifetime
		);
		
		return newParticleSystem;
	}
}
