using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Security;


public class Shop : MonoBehaviour
{
    [SerializeField] private KnifeItem itemPrefab;
    [SerializeField] private GameObject shopContainer;

    [Header("Text")]
    [SerializeField] private Text counter;
    [SerializeField] private Text price;

    [Header("Knives")]
    [SerializeField] private Image knifeUnlocked;
    [SerializeField] private Image knifeLocked;

    [Header("Manager")]
    [SerializeField] private MenuManager menuManager;

    [Header("Button")]
    //[SerializeField] private GameObject btnSelected;
    [SerializeField] private GameObject btnUnlocked;
    
    private List<KnifeItem> shopItems;

    public Knife[] knifeList;
    public KnifeItem KnifeItemSelected { get; set; }

    private KnifeItem SelectedItem
    {
        get
        {
            return shopItems.Find(x => x.IsSelected);
        }
    }

    private void Start()
    {
        Setup();
    }

    private void Update()
    {
        price.text = SelectedItem.Price.ToString();
    }

    private void Setup()
    {
        shopItems = new List<KnifeItem>();
        itemPrefab.Price = 0;
        for(int i = 0; i < knifeList.Length; i++)
        {
            itemPrefab.Price += 100;
            KnifeItem item = Instantiate(itemPrefab, shopContainer.transform);
            item.Setup(i, this);
            shopItems.Add(item);
        }
        shopItems[GameManager.Instance.SelectedKnife].OnClick();
        
    }

    public void UnlockKnife()
    {
        //SoundManager.Instance.PlayButton();
        if(GameManager.Instance.TotalApple > SelectedItem.Price)
        {
            GameManager.Instance.TotalApple -= SelectedItem.Price;
            SelectedItem.IsUnlocked = true;
            SelectedItem.UpdateUI();
            GameManager.Instance.SelectedKnife = SelectedItem.Index;
            SoundManager.Instance.PlayUnlock();
            UpdateShopUI();
        }
    }

    public void SelectedKnife()
    {
        SelectedItem.UpdateUI();
        GameManager.Instance.SelectedKnife = SelectedItem.Index;
    }

    public void UpdateShopUI()
    {
        if(SelectedItem.IsUnlocked)
        {
            if(btnUnlocked.activeSelf)
            {
                btnUnlocked.SetActive(false);
            }
        } 
        else
        {
            btnUnlocked.SetActive(true);
        }

        knifeUnlocked.sprite = SelectedItem.KnifeImage.sprite;
        knifeLocked.sprite = SelectedItem.KnifeImage.sprite;

        knifeUnlocked.gameObject.SetActive(SelectedItem.IsUnlocked);
        knifeLocked.gameObject.SetActive(!SelectedItem.IsUnlocked);

        int itemsUnlocked = 0;
        itemsUnlocked = shopItems.FindAll(x => x.IsUnlocked).Count;

        counter.text = itemsUnlocked + "/" + knifeList.Length;
        GameManager.Instance.SelectedKnifePrefab = knifeList[GameManager.Instance.SelectedKnife];

        if(menuManager != null)
        {
            menuManager.SelectedKnife.sprite = GameManager.Instance.SelectedKnifePrefab.GetComponent<SpriteRenderer>().sprite;
        }
        
    }
}
