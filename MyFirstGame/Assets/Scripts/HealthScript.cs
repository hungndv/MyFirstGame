using UnityEngine;
using System.Collections;

public class HealthScript : MonoBehaviour
{

	public int hp = 1;
	public bool isEnemy = true;

	// Use this for initialization
	void Start ()
	{
	
	}
	
	// Update is called once per frame
	void Update ()
	{
	
	}

	void Damage (int damageCount)
	{
		hp -= damageCount;
		if (hp <= 0) {
			SpecialEffectsHelper.Instance.Explosion(transform.position);
			SoundEffectsHelper.Instance.MakeExplosionSound();

			// Dead!
			Destroy (gameObject);
		}
	}

	void OnTriggerEnter2D (Collider2D otherCollider)
	{
		// Is this a shot?
		ShotScript shot = otherCollider.gameObject.GetComponent<ShotScript> ();
		if (shot != null) {
			// Avoid friendly fire
			if (shot.isEnemyShot != isEnemy) {
				Damage (shot.damage);
				// Destroy the shot
				Destroy (shot.gameObject); // Always remember to target the GameObject,
				// otherwise you will just remove the script
			}
		}
	}
}
