using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Runtime.InteropServices.WindowsRuntime;

public class KnifeItem : MonoBehaviour
{
    public static KnifeItem Instance;

    [Header ("Images")]
    [SerializeField] private Image backroundImage;
    [SerializeField] private Image knifeImage;
    [SerializeField] private GameObject selectedImage;

    [Header ("Colors")]
    [SerializeField] private Color knifeUnlockColor;
    [SerializeField] private Color knifeLockColor;
    [SerializeField] private Color knifeUnlockBackgroundColor;
    [SerializeField] private Color knfieLockBackgroundColor;

    [SerializeField] private int price;

    private Shop shop;
    private Knife knife;

    private const string KNIFE_UNLOCKED = "KnifeUnlocked";
    public int Index {get; set;}

    public bool IsUnlocked
    {
        get
        {
            if(Index == 0)
            {
                return true;
            }
            return PlayerPrefs.GetInt(KNIFE_UNLOCKED + Index, 0) == 1;
        }
        set
        {
            PlayerPrefs.SetInt(KNIFE_UNLOCKED + Index, value ? 1 : 0);
        }
    }

    public bool IsSelected
    {
        get => selectedImage.activeSelf;
        set
        {
            if(value)
            {
                if(shop.KnifeItemSelected != null)
                {
                    shop.KnifeItemSelected.IsSelected = false;
                }
                shop.KnifeItemSelected = this;
            }
            selectedImage.SetActive(value);
        }
    }

    public int Price
    {
        get 
        {
            return price;
        }
        set
        {
            price = value;
        }
    }
    public Image KnifeImage => knifeImage;

    public void Setup(int index, Shop shop)
    {
        this.shop = shop;
        Index = index;
        knife = this.shop.knifeList[Index];
        knifeImage.sprite = knife.GetComponent<SpriteRenderer>().sprite;
        UpdateUI();
    }
    
    public void OnClick()
    {
        if(IsUnlocked && IsSelected)
        {
            GeneralUI.Instance.closeShop();
        }
        if(!IsSelected)
        {        
            IsSelected = true;
        }
        if(IsUnlocked)
        {
            GameManager.Instance.SelectedKnife = Index;
        }
        shop.UpdateShopUI();
    }

    public void UpdateUI()
    {
        backroundImage.color = IsUnlocked ? knifeUnlockBackgroundColor : knfieLockBackgroundColor;
        if(!IsUnlocked)
        {
            knifeImage.color = new Color(0f,0f,0f,255f);
        }
        else
        {
            knifeImage.color = new Color(255f,255f,255f,255f);
        }
        //knifeImage.GetComponent<Mask>().enabled = IsUnlocked;
        knifeImage.transform.GetChild(0).GetComponent<Image>().color = IsUnlocked ? knifeUnlockColor : knifeLockColor;
        knifeImage.transform.GetChild(0).gameObject.SetActive(!IsUnlocked);
    }
}
