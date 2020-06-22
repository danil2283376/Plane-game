using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Reflection;
using UnityEngine;
using Qiwi;
using Qiwi.BillPayments.Client;
using System;
using Qiwi.BillPayments.Model;
using Qiwi.BillPayments.Model.Out;
using Qiwi.BillPayments.Model.In;
using System.Threading;

public class StartBuyProccess : MonoBehaviour
{
    public decimal SumTransaction = 0;

    public bool transactionComleted = false;

    public bool paid = false;

    BillPaymentsClient client = BillPaymentsClientFactory.Create(
    secretKey: "eyJ2ZXJzaW9uIjoiUDJQIiwiZGF0YSI6eyJwYXlpbl9tZXJjaGFudF9zaXRlX3VpZCI6IjU5Z29icC0wMCIsInVzZXJfaWQiOiI3OTI3NDI1NTQ1MCIsInNlY3JldCI6ImZiZmM1YmQ4NDViYTU0YTlhNmZmYmNlMzQ4OTQ3Mzc3OTk0ZThiN2FhNDg5YjZjYTk0ODU1NjRkNzkxNTA1MjUifX0="
    );

    // Запускаю отдельный поток
    public void StartProccess()
    {
        Thread accessToWeb = new Thread(CheckStatus);
        accessToWeb.Start();
    }
    // ПОПРОБЫВАТЬ ЧЕРЕЗ МНОГОПОТОЧНОСТЬ ПРОВЕРЯТЬ СОСТОЯНИЕ ОБЪЕКТА НА СЕРВЕРЕ

    public void CheckStatus()
    {
        var form = client.CreateBill(
            info: new CreateBillInfo
            {
                BillId = Guid.NewGuid().ToString(),
                Amount = new MoneyAmount
                {
                    ValueDecimal = 1.0m,
                    CurrencyEnum = CurrencyEnum.Rub
                },
                Comment = "Оплата вещи из игры",
                ExpirationDateTime = DateTime.Now.AddDays(45),
                Customer = new Customer
                {
                    Email = "danil.vasiliev98@bk.ru",
                    Account = Guid.NewGuid().ToString(),
                    Phone = "792751287912"
                },
            }
            );
        Process.Start(form.PayUrl.ToString());

        string status = "";
        while(true)
        {
            if (form.Status.ValueString == "PAID")
            {
                status = form.Status.ValueString;
                break;
            }
        }


    }
}
