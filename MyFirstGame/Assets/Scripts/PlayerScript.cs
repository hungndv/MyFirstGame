using UnityEngine;
using System.Collections;

/// <summary>
/// Player controller and behaviors.
/// </summary>
public class PlayerScript : MonoBehaviour
{

	/// <summary>
	/// 1 - The speed of the ship
	/// </summary>
	public Vector2 speed = new Vector2 (50, 50);

	/// <summary>
	/// 2 - Store the movement
	/// </summary>
	private Vector2 movement;

	// Use this for initialization
	void Start ()
	{
	
	}
	
	// Update is called once per frame
	void Update ()
	{
		// 3 - Retrieve asis information
		float inputX = Input.GetAxis ("Horizontal");
		float inputY = Input.GetAxis ("Vertical");

		// 4 - movement per direction
		movement = new Vector2 (speed.x * inputX, speed.y * inputY);

		// 5 - shooting
		bool shoot = Input.GetButtonDown ("Fire1");
		shoot |= Input.GetButtonDown ("Fire2");

		if (shoot) {
			WeaponScript weapon = GetComponent<WeaponScript> ();
			if (weapon != null) {
				// false because the player is not an enemy
				weapon.Attack (false);
				SoundEffectsHelper.Instance.MakePlayerShotSound();
			}
		}

		// 6 - Make sure we are not outside the camera bounds
		var dist = (transform.position - Camera.main.transform.position).z;
		
		var leftBorder = Camera.main.ViewportToWorldPoint (
			new Vector3 (0, 0, dist)
		).x;
		
		var rightBorder = Camera.main.ViewportToWorldPoint (
			new Vector3 (1, 0, dist)
		).x;
		
		var topBorder = Camera.main.ViewportToWorldPoint (
			new Vector3 (0, 0, dist)
		).y;
		
		var bottomBorder = Camera.main.ViewportToWorldPoint (
			new Vector3 (0, 1, dist)
		).y;
		
		transform.position = new Vector3 (
			Mathf.Clamp (transform.position.x, leftBorder, rightBorder),
			Mathf.Clamp (transform.position.y, topBorder, bottomBorder),
			transform.position.z
		);
	}

	void FixedUpdate ()
	{
		// 5 - Move the game object
		rigidbody2D.velocity = movement;
	}

	void OnDestroy()
	{
		// Game Over.
		// Add the script to the parent because the current game
		// object is likely going to be destroyed immediately.
		transform.parent.gameObject.AddComponent<GameOverScript>();
	}
}
