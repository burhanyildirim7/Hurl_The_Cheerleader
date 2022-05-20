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

    [Header("Adamlar")]
    public List<GameObject> Adamlar = new();

    [Header("income Artis Miktari")]
    public float incomeArtisMiktari;
    [Header("power Artis Miktari")]
    public int powerArtisMiktari;


    [Header("Diger Degiskenler")]
    public int power;
    public float income;
    public int incomeFiyati, powerFiyati;


    void Start()
    {
        SetAdamModel();

        PlayerPrefs.DeleteAll();


        income = PlayerPrefs.GetFloat("income");
        power = PlayerPrefs.GetInt("power");
        GameController.instance.para = PlayerPrefs.GetInt("para");
        GameController.instance.para = 100000;

        if(income == 0)
		{
            income = 1;
            PlayerPrefs.SetFloat("income",income);
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
            PlayerPrefs.SetFloat("income", income);
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
            SetAdamModel();
        }
	}


    void SetAdamModel()
	{
        if((power - 1) % 5 == 0)
		{
            int index = (power - 1) / 5;
            Debug.Log(index);
            foreach (GameObject obj in Adamlar)
            {
                obj.SetActive(false);
            }
            if(index == 0)Adamlar[index].SetActive(true);
            else if (index == 1)
            {
                Adamlar[index].SetActive(true);
                Adamlar[index-1].SetActive(true);
            }
            else if(index >= 2 && index < 45)
			{
                Adamlar[index].SetActive(true);
                Adamlar[index - 1].SetActive(true);
                Adamlar[index - 2].SetActive(true);
            }
        }    
    }

}
