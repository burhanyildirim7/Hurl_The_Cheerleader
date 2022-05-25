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


	public List<Transform> adamElleri = new();
	public Transform currentAdamEli;
	[HideInInspector] public bool ucuyorum;
	public GameObject confetiPrefab;
	float etki;
	public Animator ponponAnim;



	private void Start()
	{
		DOTween.Init();
		ucuyorum = false;
	}

	private void Update()
	{
		if (currentAdamEli != null && !ucuyorum)
		{
			transform.position = currentAdamEli.transform.position + new Vector3(-.4f,.5f,0);
		}
	}


	public void Firlat()
	{
		ucuyorum = true;
		ponponAnim.SetTrigger("flip");
		float power = Adam.instance.power;
		etki = 1;
		if(UIController.instance.powerSlider.value < 0)
		{
			etki = -UIController.instance.powerSlider.value;
			power = power - power * etki;
		}
		else if(UIController.instance.powerSlider.value > 0)
		{
			etki = UIController.instance.powerSlider.value;
			power = power - power * etki;
		}

		float distanceX = 4 + power*5f;
		Vector3 targetPosition = new Vector3(distanceX,.95f,0);
		float jumpPower = 6 + power / 5 ;
		float duration = 1.1f + power / 10 ;

		transform.DOJump(targetPosition,jumpPower,1,duration).SetEase(Ease.Linear).OnComplete(
			()=> ParaHesapla());
		StartCoroutine(KonAnim(duration-.7f));
	}

	public void ParaHesapla()
	{
		Instantiate(confetiPrefab,transform.position + new Vector3(0,0,1),Quaternion.identity);
		Instantiate(confetiPrefab,transform.position + new Vector3(0,0,-1),Quaternion.identity);
		int yeniPara = (int)(transform.position.x*Adam.instance.income);
		GameController.instance.para += yeniPara*2;
		GameController.instance.currentPara = yeniPara*2;
		PlayerPrefs.SetInt("para", GameController.instance.para);
		//UIController.instance.SetParaText();
		UIController.instance.ActivateWinScreen();

		float yeniDistance = transform.position.x;
		if(yeniDistance > GameController.instance.bestDistance)
		{
			GameController.instance.bestDistance = yeniDistance;
			PlayerPrefs.SetInt("best", (int)yeniDistance);
			StartCoroutine(GameController.instance.DrawBestDistanceLine());
		
		}
		else
		{
			GameController.instance.currentDistance = yeniDistance;
			StartCoroutine(GameController.instance.DrawDistanceLine());
		}
	}

	public void Reset()
	{
		ucuyorum = false;
		GameController.instance.currentPara = 0;
		transform.position = new Vector3(0,2.35f,0);
	}

	public IEnumerator KonAnim(float time)
	{
		yield return new WaitForSeconds(time);
		ponponAnim.SetTrigger("kon");
	}
}
