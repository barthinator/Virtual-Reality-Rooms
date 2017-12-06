using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Room : MonoBehaviour
{

    public string roomName;
    public bool isLeft = false;
    public bool isRight = false;
    bool wasTaken;
    public bool parentRoom;
    GameObject room;

    public Room(string name)
    {
        roomName = name;
        wasTaken = false;
    }

    private void Start()
    {
        room = this.gameObject;
    }

    public GameObject GetGameObject()
    {
        return room;
    }

    public string getDirection()
    {
        if (isLeft)
        {
            return "Left";
        }
        else
        {
            return "Right";
        }
    }

    public void printFormat()
    {
        Debug.Log(roomName + " " + getDirection());
    }

}
