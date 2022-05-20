using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Adam : MonoBehaviour
{
	#region SINGLETON
	public static Adam instance;
	private void Awake()
	{
        if (instance == null) instance = this;
        else Destroy(instance);
	}
    #endregion

    [Header("Adam Modeli Parentleri")]
    public GameObject Adam1, Adam2, Adam3;

    public List<GameObject> Adamlar = new();

    [Header("income Artis Miktari")]
    public int incomeArtisMiktari;
    [Header("power Artis Miktari")]
    public int powerArtisMiktari;


    [Header("Diger Degiskenler")]
    public int power;
    public int income;
    public int incomeFiyati, powerFiyati;


    void Start()
    {
        PlayerPrefs.DeleteAll();


        income = PlayerPrefs.GetInt("income");
        power = PlayerPrefs.GetInt("power");
        GameController.instance.para = PlayerPrefs.GetInt("para");

        if(income == 0)
		{
            income = 1;
            PlayerPrefs.SetInt("income",income);
            PlayerPrefs.SetInt("incomefiyat",10);
		}

        if(power == 0)
		{
            power = 1;
            PlayerPrefs.SetInt("power", power);
            PlayerPrefs.SetInt("powerfiyat", 10);
        }

        incomeFiyati = PlayerPrefs.GetInt("incomefiyat");
        powerFiyati = PlayerPrefs.GetInt("powerfiyat");

        UIController.instance.SetAllText();
    }

    public void IncreaseIncome()
	{
        if(GameController.instance.para >= incomeFiyati)
		{
            income += incomeArtisMiktari;
            PlayerPrefs.SetInt("income", income);
            incomeFiyati += 10;
            PlayerPrefs.SetInt("incomefiayti", incomeFiyati);
            UIController.instance.SetAllText();
        }
       
    }

    public void IncreasePower()
	{
        if(GameController.instance.para >= powerFiyati)
		{
            power += powerArtisMiktari;
            PlayerPrefs.SetInt("power", power);
            powerFiyati += 10;
            PlayerPrefs.SetInt("powerfiyati", powerFiyati);
            UIController.instance.SetAllText();
        }
	}


    void SetAdamModel()
	{
        if(power <= 5)
		{
            Adam1.SetActive(true);
            Adam2.SetActive(false);
            Adam3.SetActive(false);
		}
        else if(power <= 10)
		{
            Adam1.SetActive(true);
            Adam2.SetActive(true);
            Adam3.SetActive(false);
        }
        else if(power <= 15)
		{
            Adam1.SetActive(true);
            Adam2.SetActive(true);
            Adam3.SetActive(true);
        }

        if(power == 21)
		{
            Adam1.transform.GetChild(0).gameObject.SetActive(false);
            Adam1.transform.GetChild(1).gameObject.SetActive(true);
		}
        if (power == 26)
        {
            Adam2.transform.GetChild(0).gameObject.SetActive(false);
            Adam2.transform.GetChild(1).gameObject.SetActive(true);
        }
        if (power == 31)
        {
            Adam3.transform.GetChild(0).gameObject.SetActive(false);
            Adam3.transform.GetChild(1).gameObject.SetActive(true);
        }
        if (power == 36)
        {
            Adam1.transform.GetChild(1).gameObject.SetActive(false);
            Adam1.transform.GetChild(0).gameObject.SetActive(true);
        }
        if (power == 41)
        {
            Adam2.transform.GetChild(0).gameObject.SetActive(false);
            Adam2.transform.GetChild(1).gameObject.SetActive(true);
        }
        if (power == 46)
        {
            Adam3.transform.GetChild(0).gameObject.SetActive(false);
            Adam3.transform.GetChild(1).gameObject.SetActive(true);
        }



    }

    void Update()
    {
        
    }
}
