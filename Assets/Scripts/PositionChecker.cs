using System;
using UnityEngine;

public class PositionChecker : MonoBehaviour
{
    [SerializeField]
    private Spawner spawner;
    private Vector3 previousObjPosition;
    private Vector3 currentObjPosition;
    private Vector3 currentScale;

    private GameObject prevObj;
    private GameObject cuted;
    
    public void PreviousPositionSave(GameObject previousPlate)
    {
        prevObj = previousPlate;
        previousObjPosition = prevObj.transform.position;
    }

    public void CheckPosition(GameObject lastPlate)
    {
        currentScale = lastPlate.transform.localScale;
        currentObjPosition = lastPlate.transform.position;
        Vector3 cutedScale = new Vector3();
        Vector3 cutedPos = new Vector3();
        
        Vector3 distance = previousObjPosition - currentObjPosition;
        
        float scaleNewX = lastPlate.transform.localScale.x - Math.Abs(distance.x);
        float scaleNewZ = lastPlate.transform.localScale.z - Math.Abs(distance.z);
        
        if (currentScale.x < Math.Abs(distance.x) || currentScale.z < Math.Abs(distance.z))
        {
            print("gamover");
            onGameOver?.Invoke(true);
            lastPlate.transform.localScale = new Vector3(scaleNewX,1,scaleNewZ);
        }
        else
        {
            lastPlate.transform.localScale = new Vector3(scaleNewX, 1, scaleNewZ);
            cutedScale = new Vector3(currentScale.x - scaleNewX, 1, currentScale.z - scaleNewZ);
            if(cutedScale.x == 0) cutedScale.x = currentScale.x;
            if (cutedScale.z == 0) cutedScale.z = currentScale.z;
        }

        if (cutedScale.z == currentScale.z)
        {
            if (lastPlate.transform.position.x < previousObjPosition.x || lastPlate.transform.position.z < previousObjPosition.z)
            {
                lastPlate.transform.position = new Vector3(currentObjPosition.x + distance.x / 2, currentObjPosition.y, currentObjPosition.z + distance.z / 2);
                cutedPos = new Vector3(
                    lastPlate.transform.position.x - ((lastPlate.transform.localScale.x + distance.x) / 2),
                    lastPlate.transform.position.y,
                    lastPlate.transform.position.z);

            }
            else
            {
                lastPlate.transform.position = new Vector3(currentObjPosition.x - Math.Abs(distance.x / 2), currentObjPosition.y, currentObjPosition.z - Math.Abs(distance.z / 2));
                cutedPos = new Vector3(
                    lastPlate.transform.position.x + ((lastPlate.transform.localScale.x + Math.Abs(distance.x)) / 2),
                    lastPlate.transform.position.y,
                    lastPlate.transform.position.z);
            }
        }
        else
        {
            if (lastPlate.transform.position.x < previousObjPosition.x || lastPlate.transform.position.z < previousObjPosition.z)
            {
                lastPlate.transform.position = new Vector3(currentObjPosition.x + distance.x / 2, currentObjPosition.y, currentObjPosition.z + distance.z / 2);
                cutedPos = new Vector3(
                    lastPlate.transform.position.x,
                    lastPlate.transform.position.y,
                    lastPlate.transform.position.z - (lastPlate.transform.localScale.z + distance.z) / 2);

            }
            else
            {
                lastPlate.transform.position = new Vector3(currentObjPosition.x - Math.Abs(distance.x / 2), currentObjPosition.y, currentObjPosition.z - Math.Abs(distance.z / 2));
                cutedPos = new Vector3(
                    lastPlate.transform.position.x,
                    lastPlate.transform.position.y,
                    lastPlate.transform.position.z + ((lastPlate.transform.localScale.z + Math.Abs(distance.z)) / 2));
            }
        }
        cuted = lastPlate;
        
        spawner.SpawnRemainingPiece(lastPlate);
        spawner.SpawnFallingPiece(cuted, cutedPos, cutedScale);
        
    }

    public delegate void Gamover(bool instance);
    public event Gamover onGameOver;
}

