using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CountKills : MonoBehaviour
{
    // СДЕЛАТЬ СЧЕТЧИК ПРОТИВНИКОВ!
    public Text Count;
    public int countKills { get; set; }
    //public AlgoritmSpawnMobs 
    public bool nextlvl = false;

    // для общего кол - ва
    public int total;

    public void Kills()
    {
        countKills++;
    }
    // Найти где минусуется на 5
    public void CountKillsEnemy()
    {
        if (countKills != total)
            Count.text = $"{countKills} / {total}";
           
        else
            NextLvl();
    }

    public void NextLvl()
    {
        if ((total % total) == 0 && !nextlvl)
        {
            Count.text = "Boss!";

        }
        else
        {
            // анимация о том что следующий уровень!
            Count.text = "NEXT LVL!";
        }
    }

    public int NeedKills()
    {
        return total - countKills;
    }
}
