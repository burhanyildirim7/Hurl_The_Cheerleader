using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationKey : MonoBehaviour
{
    public void Firlat()
	{
		GetComponent<Animator>().speed = 5f;
		PonPonKiz.instance.Firlat();
	}

	public void SpeedNormalized()
	{
		GetComponent<Animator>().speed = 1f;
		GetComponent<Animator>().SetTrigger("idle");
		
	}
}
