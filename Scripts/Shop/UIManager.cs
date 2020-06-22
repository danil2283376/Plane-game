using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
    private StartBuyProccess buyProccess;

    private void Start()
    {
        buyProccess = GetComponent<StartBuyProccess>();
    }

    public void ExitMainMenu()
    {
        SceneManager.LoadScene("Menu");
    }

    //public void BuyGun()
    //{
    //    ShopClass shopClass = GetComponent<ShopClass>();
    //    shopClass.ClickButton();
    //}

    //public void BuyHp()
    //{
    //    //buyProccess.StartProccess();
    //    ShopClass shopClass = GetComponent<ShopClass>();
    //    shopClass.ClickButton();
    //}
}
