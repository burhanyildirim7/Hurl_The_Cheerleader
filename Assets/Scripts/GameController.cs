using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class GameController : MonoBehaviour
{
    public static GameController instance; // singleton yapisi icin gerekli ornek ayrintilar icin BeniOku 22. satirdan itibaren bak.

    [HideInInspector]public int score, elmas,para,currentPara; // ayrintilar icin benioku 9. satirdan itibaren bak

    [HideInInspector] public bool isContinue;  // ayrintilar icin beni oku 19. satirdan itibaren bak
    [HideInInspector] public bool sliderTime;
    [HideInInspector] public float bestDistance, currentDistance;
    public GameObject distanceUIObj;

    public GameObject bestDistanceLineObj, bestDistanceTextObj, distanceLineObj, distanceTextObj;


	private void Awake()
	{
        if (instance == null) instance = this;
        //else Destroy(this);
	}

	void Start()
    {
        bestDistance = PlayerPrefs.GetInt("best");
        if(bestDistance > 0) StartCoroutine(DrawBestDistanceLine());
        isContinue = false;
    }


    public void SetScore(int eklenecekScore)
	{
        if(PlayerController.instance.collectibleVarMi) score += eklenecekScore;
        // Eðer oyunda collectible yok ise developer kendi score sistemini yazmalý...

    }


    public void SetElmas(int eklenecekElmas)
    {
        elmas += eklenecekElmas;
        // buradaki elmas artýnca totalScore da otomatik olarak artacak.. bu sebeple asagidaki kodlar eklendi.
        PlayerPrefs.SetInt("totalElmas", PlayerPrefs.GetInt("totalElmas" + eklenecekElmas));
       // UIController.instance.SetTotalElmasText(); // totalElmaslarýn yazili oldugu texti
    }


    public void ScoreCarp(int katsayi)
	{
        if (PlayerController.instance.xVarMi) score *= katsayi;
        else score = 1 * score;
        PlayerPrefs.SetInt("totalScore", PlayerPrefs.GetInt("totalScore") + score);
    }

    public IEnumerator DrawBestDistanceLine()
	{
        yield return new WaitForSeconds(.6f);
        bestDistanceLineObj.transform.position = new(bestDistance, .54f, 0);
        BestDistanceObjAnim();
    }

    public IEnumerator DrawDistanceLine()
	{
        yield return new WaitForSeconds(.6f);
        distanceLineObj.transform.position = new(currentDistance,.54f,0);
        DistanceObjAnim();
	}

    public void BestDistanceObjAnim()
	{
        
        bestDistanceTextObj.SetActive(true);
        distanceTextObj.SetActive(false);
        distanceUIObj.transform.position = PonPonKiz.instance.transform.position + new Vector3(0, 5, -2);
        Vector3 position = PonPonKiz.instance.transform.position + new Vector3(0,.5f,-2);
        distanceUIObj.transform.DOMove(position,1).SetEase(Ease.OutBounce);
        UIController.instance.SetBestDistanceObjText();
	}

    public void DistanceObjAnim()
	{
        bestDistanceTextObj.SetActive(false);
        distanceTextObj.SetActive(true);
        distanceUIObj.transform.position = PonPonKiz.instance.transform.position + new Vector3(0, 5, -2);
        Vector3 position = PonPonKiz.instance.transform.position + new Vector3(0, .5f, -2);
        distanceUIObj.transform.DOMove(position, 1).SetEase(Ease.OutBounce);
        UIController.instance.SetDistanceObjText();
    }

 
}
