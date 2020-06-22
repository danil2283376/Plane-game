using Qiwi.BillPayments.Client;
using Qiwi.BillPayments.Model;
using Qiwi.BillPayments.Model.In;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Threading;
using UnityEngine;
using UnityEngine.UI;

public class ShopClass : MonoBehaviour
{
    public Text textNameItem;
    public float payment;

    private decimal pay;

    private int countItem;
    private void Start()
    {
        GameObject item = GameObject.Find("ItemShop");
        countItem = item.transform.childCount;

        GameObject[] itemShop = new GameObject[countItem];
        for (int i = 0; i < countItem; i++)
        {

        }
    }

    public void ClickButton()
    {
        StartBuyProccess();
    }

    public void StartBuyProccess()
    {
        this.gameObject.SetActive(false);
        Thread payment = new Thread(CheckStatus);
        payment.Start();
    }
    // создаю форму о для покупки предмета
    private void CheckStatus()
    {
        pay = Convert.ToDecimal(payment);
        BillPaymentsClient client = BillPaymentsClientFactory.Create(
            secretKey: "eyJ2ZXJzaW9uIjoiUDJQIiwiZGF0YSI6eyJwYXlpbl9tZXJjaGFudF9zaXRlX3VpZCI6IjU5Z29icC0wMCIsInVzZXJfaWQiOiI3OTI3NDI1NTQ1MCIsInNlY3JldCI6ImZiZmM1YmQ4NDViYTU0YTlhNmZmYmNlMzQ4OTQ3Mzc3OTk0ZThiN2FhNDg5YjZjYTk0ODU1NjRkNzkxNTA1MjUifX0="
            );

        var form = client.CreateBill(
            info: new CreateBillInfo
            {
                BillId = Guid.NewGuid().ToString(),
                Amount = new MoneyAmount
                {
                    ValueDecimal = pay,
                    CurrencyEnum = CurrencyEnum.Rub
                },
                Comment = textNameItem.text,
                ExpirationDateTime = DateTime.Now.AddDays(45),
                Customer = new Customer
                {
                    Email = "danil.vasiliev98@bk.ru",
                    Account = Guid.NewGuid().ToString(),
                    Phone = "792751287912"
                },
            }
            );
        print(form.PayUrl.ToString());
        Process.Start(form.PayUrl.ToString());
        //using (StreamWriter sw = new StreamWriter(Application.streamingAssetsPath + "/Report.txt", true, System.Text.Encoding.Default))
        //{
        //    sw.Write(textNameItem.text);
        //}

        string status = "";
        while (true)
        {
            var responseStatus = client.GetBillInfo(billId: form.BillId);
            if (responseStatus.Status.ValueString == "PAID")
            {
                //if (status == "PAID")
                //{
                    // получаю путь к txt
                    string path = Application.streamingAssetsPath + "/Report.txt";

                    using (StreamWriter sw = new StreamWriter(path, true, System.Text.Encoding.Default))
                    {
                        sw.Write(textNameItem.text);
                    }
                //}
                status = form.Status.ValueString;
                break;
            }
        }
    }
    // СДЕЛАТЬ ПРОВЕРКУ О ПРЕДМЕТЕ В TXT ФАЙЛЕ ДЛЯ ТОГО ЧТО БЫ НЕБЫЛО НЕСКОЛЬКО ПРЕДМЕТОВ, ДОДЕЛАТЬ МАГАЗИН, И ИСПРАВИТЬ БАГИ В ИГРЕ
}
