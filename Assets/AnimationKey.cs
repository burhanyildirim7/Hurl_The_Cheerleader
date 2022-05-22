using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationKey : MonoBehaviour
{
    public void Firlat()
	{
		
		PonPonKiz.instance.Firlat();
	}

	public void SpeedNormalized()
	{
		GetComponent<Animator>().speed = 1f;
		GetComponent<Animator>().SetTrigger("idle");
		
	}

	public void IncreaseSpeed()
	{
		GetComponent<Animator>().speed = 5f;
	}
}
