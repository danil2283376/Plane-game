using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveAction : TimeAction
{
    private Vector3 _moveFrom, _moveTo;

    public void MoveWithTime(float time, Vector3 target, TimeActionFinish finish)
    {
        _moveFrom = transform.localPosition;
        _moveTo = target;
        base.Delay(time, finish);
    }

    public void MoveWithSpeed(float speed, Vector3 target, TimeActionFinish finish)
    {
        MoveWithTime(Vector3.Distance(transform.localPosition, target)/speed, target, finish);
    }

    protected override void UpdateWay(float way)
    {
        transform.localPosition = Vector3.Lerp(_moveFrom, _moveTo, way);
    }
}
