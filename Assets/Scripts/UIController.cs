using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIController : MonoBehaviour
{
    public static UIController instance; // Singleton yapisi icin gerekli ornek

    public GameObject TapToStartPanel, LoosePanel, GamePanel, WinPanel, winScreenEffectObject, winScreenCoinImage, startScreenCoinImage, scoreEffect;
    public Text  winScreenScoreText, levelNoText, totalElmasText;
    public Animator ScoreTextAnim;
    public Text incomeText, powerText, incomeFiyatText, powerFiyatText,paraText;
    public Slider powerSlider;
    



    // singleton yapisi burada kuruluyor.
    private void Awake()
    {
        if (instance == null) instance = this;
        //else Destroy(this);
    }

    private void Start()
    {
        StartUI();
    }

    // Oyun ilk acildiginda calisacak olan ui fonksiyonu. 
    public void StartUI()
    {
        ActivateTapToStartScreen();
        SetParaText();
    }

    public void SetLevelText(int levelNo)
    {
        levelNoText.text = "Level " + levelNo.ToString();
    }

    public void TapToStartButtonClick()
    {

        GameController.instance.isContinue = true;
        GameController.instance.sliderTime = true;
        StartCoroutine(AnimatePowerSlider());
        TapToStartPanel.SetActive(false);
        GamePanel.SetActive(true);
        
    }

    public void RestartButtonClick()
    {
        GamePanel.SetActive(false);
        LoosePanel.SetActive(false);
        TapToStartPanel.SetActive(true);
        LevelController.instance.RestartLevelEvents();

    }



    public void NextLevelButtonClick()
    {

        TapToStartPanel.SetActive(true);
        WinPanel.SetActive(false);
        GamePanel.SetActive(false);
        StartCoroutine(StartScreenCoinEffect());
        PonPonKiz.instance.Reset();
    }

    public void IncomeButtonClick()
	{
        Adam.instance.IncreaseIncome();
	}

    public void PowerButtonClick()
	{
        Adam.instance.IncreasePower();
	}

    public void FirlatButtonClick()
	{
        GameController.instance.sliderTime = false;
        PonPonKiz.instance.Firlat();
        GamePanel.SetActive(false);

    }

    public void SetParaText()
	{
        paraText.text = GameController.instance.para.ToString();
	}

    public IEnumerator AnimatePowerSlider()
	{
        float artis = .01f;

		while (GameController.instance.sliderTime)
		{
            powerSlider.value += artis;
            if (powerSlider.value >= .5f || powerSlider.value <= -.5f) artis = -artis;
            yield return new WaitForSeconds(.01f);
		}
	}







    public void WinScreenScore()
    {
        winScreenScoreText.text = GameController.instance.score.ToString();
    }

    public void SetTotalElmasText()
    {
        totalElmasText.text = PlayerPrefs.GetInt("totalElmas").ToString();
    }

    public void ActivateWinScreen()
    {
        GamePanel.SetActive(false);
        StartCoroutine(WinScreenDelay());
    }

    IEnumerator WinScreenDelay()
    {
        yield return new WaitForSeconds(2);
        WinPanel.SetActive(true);
        winScreenScoreText.text = "0";
        int sayac = 0;
        while (sayac < GameController.instance.currentPara)
        {
            sayac += 1;
            if (sayac % 2 == 0)
            {
                GameObject effectObj = Instantiate(winScreenEffectObject, new Vector3(144, 400, 0), Quaternion.identity, winScreenCoinImage.transform);
                effectObj.transform.localPosition = new Vector3(144, 300, 0);
                effectObj.transform.localRotation = Quaternion.Euler(0, 0, winScreenCoinImage.transform.localRotation.z);
                effectObj.GetComponent<Image>().sprite = winScreenCoinImage.GetComponent<Image>().sprite;
                effectObj.transform.localScale = Vector3.one * .2f;
                StartCoroutine(WinScreenEffect(effectObj));
            }
            winScreenScoreText.text = sayac.ToString();
            yield return new WaitForSeconds(.05f);
        }
    }

    IEnumerator WinScreenEffect(GameObject effectObj)
    {
        float sayac = 0;
        float scale = 0;
        while (Vector2.Distance(effectObj.transform.position, winScreenCoinImage.transform.position) > 0.05f)
        {
            effectObj.transform.position = Vector2.Lerp(effectObj.transform.position, winScreenCoinImage.transform.position, sayac);
            scale = Mathf.Lerp(effectObj.transform.localScale.x, winScreenCoinImage.transform.localScale.x, sayac);
            effectObj.transform.localScale = Vector3.one * scale;
            sayac += .02f;
            yield return new WaitForSeconds(.015f);
        }
        Destroy(effectObj);
    }

    IEnumerator StartScreenCoinEffect()
    {
        startScreenCoinImage.GetComponent<Image>().sprite = winScreenCoinImage.GetComponent<Image>().sprite;
        startScreenCoinImage.SetActive(true);
        float sayac = 0;
        int adet = 0;
        while (Vector2.Distance(startScreenCoinImage.transform.position, paraText.transform.position) >= 5f)
        {
            adet++;
            sayac += .01f;
            startScreenCoinImage.transform.position = Vector2.Lerp(startScreenCoinImage.transform.position, paraText.transform.position, sayac);
            yield return new WaitForSeconds(.025f);
            if (adet % 3 == 0)
            {
                GameObject coin = Instantiate(winScreenEffectObject, startScreenCoinImage.transform.position, Quaternion.identity, TapToStartPanel.transform);
                coin.GetComponent<Image>().sprite = winScreenCoinImage.GetComponent<Image>().sprite;
                coin.transform.rotation = startScreenCoinImage.transform.rotation;
                StartCoroutine(StartScreenCoinsDissolve(coin));
            }
        }
        Instantiate(scoreEffect, new Vector3(1.34f, 5.43F, -1.15F), Quaternion.identity);

        startScreenCoinImage.SetActive(false);
        startScreenCoinImage.transform.localPosition = new Vector3(0, -446, 0);
    }

    IEnumerator StartScreenCoinsDissolve(GameObject obj)
    {
        Color tempColor = obj.GetComponent<Image>().color;
        while (obj.transform.localScale.x > .2f)
        {
            obj.transform.localScale = new Vector3(obj.transform.localScale.x - .05f, obj.transform.localScale.y - .05f, obj.transform.localScale.z - .05f);
            tempColor.a = tempColor.a - .05f;
            obj.GetComponent<Image>().color = tempColor;
            yield return new WaitForSeconds(.03f);
        }
        Destroy(obj);
    }

    public void ActivateLooseScreen()
    {
        GamePanel.SetActive(false);
        LoosePanel.SetActive(true);
    }


    public void ActivateGameScreen()
    {
        GamePanel.SetActive(true);
        TapToStartPanel.SetActive(false);

    }

    public void ActivateTapToStartScreen()
    {
        TapToStartPanel.SetActive(true);
        WinPanel.SetActive(false);
        LoosePanel.SetActive(false);
        GamePanel.SetActive(false);

    }


    public void SetAllText()
	{

	}


}
