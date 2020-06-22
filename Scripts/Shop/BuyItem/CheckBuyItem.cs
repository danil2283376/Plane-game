using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;

public class CheckBuyItem : AbstractCheckBuy
{
    //public Text textNameItem;

    //public CheckBuyItem() { }

    //public CheckBuyItem(Text textNameItem)
    //{
    //    this.textNameItem = textNameItem;
    //}
    public GameObject rootItem;
    public bool activeItemShop;

    private void Awake()
    {
        if (activeItemShop)
            ActivateItemShop();
        else
            ActivateItemGame();
    }

    public void ActivateItemShop()
    {
        int countItem = rootItem.transform.childCount;

        for (int i = 0; i < countItem; i++)
        {
            GameObject father = rootItem.transform.GetChild(i).gameObject;
            GameObject child = father.transform.GetChild(0).gameObject;

            base.textNameItem = child.GetComponentInChildren<Text>().text;
            if (base.ExistWord())
            {
                father.SetActive(false);
            }
        }
    }

    public void ActivateItemGame()
    {
        int countItem = rootItem.transform.childCount;

        for (int i = 0; i < countItem; i++)
        {
            GameObject child = rootItem.transform.GetChild(i).gameObject;

            base.textNameItem = child.name;

            if (base.ExistWord())
            {
                child.SetActive(true);
            }
        }
    }
}
