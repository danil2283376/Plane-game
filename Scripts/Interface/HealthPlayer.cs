using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthPlayer : MonoBehaviour
{
    public Text hpBar;

    // отнять жизни от индикатора!
    public void SubtractionHP(int damageCount, int hp)
    {
        if (hp > 0)
            hpBar.text = "+ " + (hp - damageCount);
        else
            hpBar.text = "DIE!";
    }

    public void ColorHP()
    {
        // сделать смену цвета
    }
}
