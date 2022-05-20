using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PonPonKiz : MonoBehaviour
{
	#region SINGLETON
	public static PonPonKiz instance;
	private void Awake()
	{
		if (instance == null) instance = this;
		else Destroy(instance);
	}
	#endregion

	public void Firlat()
	{

	}
}
