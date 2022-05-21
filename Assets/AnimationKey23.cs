using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationKey23 : MonoBehaviour
{
	public void Firlat()
	{
		GetComponent<Animator>().speed = 5f;
	}

	public void SpeedNormalized()
	{
		GetComponent<Animator>().speed = 1f;
		GetComponent<Animator>().SetTrigger("idle");

	}
}
