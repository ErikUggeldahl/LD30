using UnityEngine;
using System.Collections;

public class CameraControl: MonoBehaviour
{	
	public void Update()
	{
		float vertical = 0f;
		if (Input.GetKey(KeyCode.W))
			vertical += 1f;
		if (Input.GetKey(KeyCode.S))
			vertical += -1f;

		float horizontal = 0f;
		if (Input.GetKey(KeyCode.A))
			horizontal -= 1f;
		if (Input.GetKey(KeyCode.D))
			horizontal += 1f;

		transform.Translate(new Vector3(horizontal, vertical, 0f).normalized * 10f * Time.deltaTime);
	}
}
