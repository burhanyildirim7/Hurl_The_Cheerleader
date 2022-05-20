using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    public static GameController instance; // singleton yapisi icin gerekli ornek ayrintilar icin BeniOku 22. satirdan itibaren bak.

    [HideInInspector]public int score, elmas,para; // ayrintilar icin benioku 9. satirdan itibaren bak

    [HideInInspector] public bool isContinue;  // ayrintilar icin beni oku 19. satirdan itibaren bak


	private void Awake()
	{
        if (instance == null) instance = this;
        //else Destroy(this);
	}

	void Start()
    {
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

}
