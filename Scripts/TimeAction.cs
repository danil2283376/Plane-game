using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public delegate void TimeActionFinish();

public class TimeAction : MonoBehaviour
{
    protected float ActionTime, ActionTimer;
    protected TimeActionFinish Finish;

    public virtual void Delay(float time, TimeActionFinish finish)
    {
        ActionTimer = 0;
        ActionTime = time;
        Finish = finish;
    }

    private void Update()
    {
        ActionTimer += Time.deltaTime;
        float Way = ActionTimer / ActionTime;
        if (Way < 1)
            UpdateWay(Way);
        else
        {
            UpdateWay(1);
            if (Finish != null)
                Finish();
            Destroy(this);
        }
    }

    protected virtual void UpdateWay(float way) { }
}
