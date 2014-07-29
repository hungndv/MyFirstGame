using UnityEngine;
using System.Collections;

public class EnemyScript : MonoBehaviour
{
	private bool hasSpawn;
	private MoveScript moveScript;
	private WeaponScript[] weapons;

	void Awake ()
	{
		// Retrieve the weapon only once
		weapons = GetComponentsInChildren<WeaponScript> ();
		// Retrieve scripts to disable when not spawn
		moveScript = GetComponent<MoveScript> ();
	}

	// Use this for initialization
	void Start ()
	{
		hasSpawn = false;

		// Disable everything
		// -- collider
		collider2D.enabled = false;
		// -- moving
		moveScript.enabled = false;
		// -- shot
		foreach (var weapon in weapons) {
			weapon.enabled = false;
		}
	}
	
	// Update is called once per frame
	void Update ()
	{
		if (!hasSpawn) {
			if (renderer.IsVisibleFrom (Camera.main)) {
				Spawn ();
			}
		} else {
			// Auto-fire
			foreach (WeaponScript weapon in weapons) {
				if (weapon != null && weapon.enabled && weapon.CanAttack) {
					weapon.Attack (true);
					SoundEffectsHelper.Instance.MakeEnemyShotSound();
				}
			}
			
			// 4 - Out of the camera ? Destroy the game object.
			if (renderer.IsVisibleFrom (Camera.main) == false) {
				Destroy (gameObject);
			}
		}
	}
	
	private void Spawn ()
	{
		hasSpawn = true;
		
		// Enable everything
		// -- Collider
		collider2D.enabled = true;
		// -- Moving
		moveScript.enabled = true;
		// -- Shooting
		foreach (WeaponScript weapon in weapons) {
			weapon.enabled = true;
		}
	}
}
