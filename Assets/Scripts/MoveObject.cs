using System;
using System.Collections;
using UnityEngine;

public class MoveObject : MonoBehaviour
{

    [SerializeField]
    private PositionChecker positionChecker;

    private GameObject movingObject;
    private float timeToMove = 3f;
    private Vector3 endPoint;
    private Vector3 startPoint;
    public Coroutine coroutine;

    public void EndPoint(Vector3 start)
    {
        if (Math.Abs(start.x) == 12)
        {
            endPoint = new Vector3(-startPoint.x,startPoint.y, startPoint.z);
        }
        else
        {
            endPoint = new Vector3(startPoint.x, startPoint.y, -startPoint.z);
        }
        
    }
    public void StartRoutine(GameObject obj)
    {
        movingObject = obj;
        coroutine = StartCoroutine(Move(movingObject));
    }

    public void StopRoutine()
    {
        if (coroutine != null)
        {
            positionChecker.CheckPosition(movingObject);
            StopCoroutine(coroutine);
        }
        
    }
    IEnumerator Move(GameObject plate)
    {
        float time = 0;
        startPoint = plate.transform.position;
        if (Math.Abs(startPoint.x) == 12)
        {
            endPoint = new Vector3(-startPoint.x, startPoint.y, startPoint.z);
        }
        else
        {
            endPoint = new Vector3(startPoint.x, startPoint.y, -startPoint.z);
        }
        while (time < timeToMove)
        {
            time += Time.deltaTime;
            float normalizedTime = time / timeToMove;
            plate.transform.position = Vector3.Lerp(startPoint, endPoint, normalizedTime);
            yield return null;
            if (time >= timeToMove)
            {
                startPoint = plate.transform.position;
                if (Math.Abs(startPoint.x) == 12)
                {
                    endPoint = new Vector3(-startPoint.x, startPoint.y, startPoint.z);
                }
                else
                {
                    endPoint = new Vector3(startPoint.x, startPoint.y, -startPoint.z);
                };
                time = 0;
            }
        }
    }
    
}