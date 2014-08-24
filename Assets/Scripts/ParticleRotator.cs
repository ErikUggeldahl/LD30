using UnityEngine;
using System.Collections;

public class ParticleRotator : MonoBehaviour
{
	public void Update()
	{
		transform.Rotate(Vector3.up, 180 * Time.deltaTime);
	}
}
