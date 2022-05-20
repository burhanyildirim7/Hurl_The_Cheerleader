using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

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


	private void Start()
	{
		DOTween.Init();
	}


	public void Firlat()
	{
		float power = Adam.instance.power;

		if(UIController.instance.powerSlider.value < 0)
		{
			float etki = -UIController.instance.powerSlider.value;
			power = power - power * etki;
		}
		else if(UIController.instance.powerSlider.value > 0)
		{
			float etki = UIController.instance.powerSlider.value;
			power = power - power * etki;
		}

		float distanceX = 4 + power*2;
		Vector3 targetPosition = new Vector3(distanceX,0,0);
		float jumpPower = 4 + power / 4 ;
		float duration = 1 + power / 5 ;

		transform.DOJump(targetPosition,jumpPower,1,duration).SetEase(Ease.Linear).OnComplete(
			()=> ParaHesapla());
	}

	public void ParaHesapla()
	{
		int yeniPara = (int)transform.position.x;
		GameController.instance.para += yeniPara;
		GameController.instance.currentPara = yeniPara;
		PlayerPrefs.SetInt("para", GameController.instance.para);
		UIController.instance.SetParaText();
		UIController.instance.ActivateWinScreen();

		if(yeniPara > GameController.instance.bestDistance)
		{
			GameController.instance.bestDistance = yeniPara;
			PlayerPrefs.SetInt("best", yeniPara);
			GameController.instance.DrawBestDistanceLine();
		}
		else
		{
			GameController.instance.currentDistance = yeniPara;
			GameController.instance.DrawDistanceLine();
		}
	}

	public void Reset()
	{
		GameController.instance.currentPara = 0;
		transform.position = new Vector3(0,2.35f,0);
	}
}
