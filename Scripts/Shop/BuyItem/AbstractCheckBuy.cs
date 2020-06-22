using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;

public abstract class AbstractCheckBuy : MonoBehaviour
{
    public string textNameItem { get; set; }

    public bool ExistWord()
    {
        string line;
        bool existWord = false;
        using (FileStream f = new FileStream(Application.streamingAssetsPath + "/Report.txt", FileMode.Open))
        {
            using (StreamReader sr = new StreamReader(f, System.Text.Encoding.Default))
            {
                while ((line = sr.ReadLine()) != null)
                {
                    if (line.Contains(textNameItem))
                    {
                        existWord = true;
                        break;
                    }
                }
            }
        }
        return existWord;
    }
}
