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
    public GameObject currentAdam1,currentAdam2,currentAdam3;
    [Header("income Artis Miktari")]
    public float incomeArtisMiktari;
    [Header("power Artis Miktari")]
    public int powerArtisMiktari;


    [Header("Diger Degiskenler")]
    public int power;
    public float income;
    public int incomeFiyati, powerFiyati;
    public GameObject adamEfecti;


    void Start()
    {


        PlayerPrefs.DeleteAll();

        income = PlayerPrefs.GetFloat("income");
        power = PlayerPrefs.GetInt("power");
        incomeFiyati = PlayerPrefs.GetInt("incomefiyati");
        powerFiyati = PlayerPrefs.GetInt("powerfiyati");
        GameController.instance.para = PlayerPrefs.GetInt("para");
        GameController.instance.para = 100000000;

        if(income == 0)
		{
            income = 1;
            PlayerPrefs.SetFloat("income",income);
            PlayerPrefs.SetInt("incomefiyati",10);
		}

        if(power == 0)
		{
            power = 1;
            PlayerPrefs.SetInt("power", power);
            PlayerPrefs.SetInt("powerfiyati", 10);
        }

        if (powerFiyati == 0)
        {
            powerFiyati = 10;
            PlayerPrefs.SetInt("powerfiyati", 10);
        }

        if (incomeFiyati == 0)
        {
            incomeFiyati = 10;
            PlayerPrefs.SetInt("incomefiyati", 10);
        }


        UIController.instance.SetAllText();
        SetAdamModelForStarting();
    }

    public void IncreaseIncome()
	{
        if(GameController.instance.para >= incomeFiyati)
		{
            income += incomeArtisMiktari;
            PlayerPrefs.SetFloat("income", income);

            GameController.instance.para -= incomeFiyati;
            incomeFiyati = incomeFiyati + 15; ;
            PlayerPrefs.SetInt("incomefiyati", incomeFiyati);
            PlayerPrefs.SetInt("para", GameController.instance.para);
            UIController.instance.SetAllText();
        }   
    }

    public void IncreasePower()
	{
        if(GameController.instance.para >= powerFiyati)
		{
            power += powerArtisMiktari;
            PlayerPrefs.SetInt("power", power);
            GameController.instance.para -= powerFiyati;
            powerFiyati = powerFiyati + 15;
            PlayerPrefs.SetInt("powerfiyati", powerFiyati);
            PlayerPrefs.SetInt("para", GameController.instance.para);     
            UIController.instance.SetAllText();
            SetAdamModel();
        }
	}

    void SetAdamModelForStarting()
	{
        int index = (power - 1) / 5;
        if (index < 45)
        {
            foreach (GameObject obj in Adamlar)
            {
                obj.SetActive(false);
            }
        }

        if (index == 0) Adamlar[index].SetActive(true);
        else if (index == 1)
        {
            Adamlar[index].SetActive(true);
            Adamlar[index - 1].SetActive(true);
        }
        else if (index >= 2 && index < 45)
        {
            Adamlar[index].SetActive(true);
            Adamlar[index - 1].SetActive(true);
            Adamlar[index - 2].SetActive(true);
        }
        else if(index >= 45)
		{
            Adamlar[44].SetActive(true);
            Adamlar[43].SetActive(true);
            Adamlar[42].SetActive(true);
		}

        for (int i = 0; i < transform.GetChild(0).transform.childCount; i++)
        {
            if (transform.GetChild(0).transform.GetChild(i).gameObject.activeInHierarchy)
            {
                currentAdam1 = transform.GetChild(0).transform.GetChild(i).gameObject;
                PonPonKiz.instance.currentAdamEli = PonPonKiz.instance.adamElleri[i];
                //if (power > 1) Instantiate(adamEfecti, currentAdam1.transform.position + new Vector3(0, 2, 0), Quaternion.identity);
            }
        }
        for (int i = 0; i < transform.GetChild(1).transform.childCount; i++)
        {
            if (transform.GetChild(1).transform.GetChild(i).gameObject.activeInHierarchy)
            {
                currentAdam2 = transform.GetChild(1).transform.GetChild(i).gameObject;
                //if (power > 1) Instantiate(adamEfecti, currentAdam2.transform.position + new Vector3(0, 2, 0), Quaternion.identity);
            }

        }
        for (int i = 0; i < transform.GetChild(2).transform.childCount; i++)
        {
            if (transform.GetChild(2).transform.GetChild(i).gameObject.activeInHierarchy)
            {
                currentAdam3 = transform.GetChild(2).transform.GetChild(i).gameObject;
                //if (power > 1) Instantiate(adamEfecti, currentAdam3.transform.position + new Vector3(0, 2, 0), Quaternion.identity);
            }

        }

    }


    void SetAdamModel()
	{
        if((power - 1) % 5 == 0 )
		{
            int index = (power - 1) / 5;
			if (index < 45)
			{
                foreach (GameObject obj in Adamlar)
                {
                    obj.SetActive(false);
                }
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

			for (int i = 0; i < transform.GetChild(0).transform.childCount; i++)
			{
                if (transform.GetChild(0).transform.GetChild(i).gameObject.activeInHierarchy)
				{
                    currentAdam1 = transform.GetChild(0).transform.GetChild(i).gameObject;
                    PonPonKiz.instance.currentAdamEli = PonPonKiz.instance.adamElleri[i];
                    if (power > 1) Instantiate(adamEfecti,currentAdam1.transform.position+ new Vector3(0, 2, 0), Quaternion.identity);
                }             
			}
            for (int i = 0; i < transform.GetChild(1).transform.childCount; i++)
            {
                if (transform.GetChild(1).transform.GetChild(i).gameObject.activeInHierarchy)
				{
                    currentAdam2 = transform.GetChild(1).transform.GetChild(i).gameObject;
                    if (power > 1) Instantiate(adamEfecti, currentAdam2.transform.position+new Vector3(0, 2, 0), Quaternion.identity);
                }
                    
            }
            for (int i = 0; i < transform.GetChild(2).transform.childCount; i++)
            {
                if (transform.GetChild(2).transform.GetChild(i).gameObject.activeInHierarchy)
				{
                    currentAdam3 = transform.GetChild(2).transform.GetChild(i).gameObject;
                    if (power > 1) Instantiate(adamEfecti, currentAdam3.transform.position + new Vector3(0,2,0), Quaternion.identity);
                }
                   
            }


            
        }    
    }

}
