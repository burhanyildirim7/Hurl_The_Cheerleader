using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    public static GameController instance; // singleton yapisi icin gerekli ornek ayrintilar icin BeniOku 22. satirdan itibaren bak.

    [HideInInspector]public int score, elmas,para,currentPara; // ayrintilar icin benioku 9. satirdan itibaren bak

    [HideInInspector] public bool isContinue;  // ayrintilar icin beni oku 19. satirdan itibaren bak
    [HideInInspector] public bool sliderTime;
    [HideInInspector] public int bestDistance, currentDistance;

    public GameObject bestDistanceLineObj, bestDistanceTextObj, distanceLineObj, distanceTextObj;


	private void Awake()
	{
        if (instance == null) instance = this;
        //else Destroy(this);
	}

	void Start()
    {
        bestDistance = PlayerPrefs.GetInt("best");
        DrawBestDistanceLine();
        isContinue = false;
    }


    public void SetScore(int eklenecekScore)
	{
        if(PlayerController.instance.collectibleVarMi) score += eklenecekScore;
        // E�er oyunda collectible yok ise developer kendi score sistemini yazmal�...

    }


    public void SetElmas(int eklenecekElmas)
    {
        elmas += eklenecekElmas;
        // buradaki elmas art�nca totalScore da otomatik olarak artacak.. bu sebeple asagidaki kodlar eklendi.
        PlayerPrefs.SetInt("totalElmas", PlayerPrefs.GetInt("totalElmas" + eklenecekElmas));
       // UIController.instance.SetTotalElmasText(); // totalElmaslar�n yazili oldugu texti
    }


    public void ScoreCarp(int katsayi)
	{
        if (PlayerController.instance.xVarMi) score *= katsayi;
        else score = 1 * score;
        PlayerPrefs.SetInt("totalScore", PlayerPrefs.GetInt("totalScore") + score);
    }

    public void DrawBestDistanceLine()
	{
        bestDistanceLineObj.transform.position = new(bestDistance, .55f, 0);
    }

    public void DrawDistanceLine()
	{
        distanceLineObj.transform.position = new(currentDistance,.55f,0);
	}
}
