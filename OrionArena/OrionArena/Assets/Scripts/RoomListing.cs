using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;
using Photon.Realtime;
using System;

public class RoomListing : MonoBehaviourPunCallbacks
{
    public Transform Grid;
    public GameObject RoomNamePrefab;

    public override void OnRoomListUpdate(List<RoomInfo> roomList)
    {
        foreach (RoomInfo room in roomList)
        {
            if (room.RemovedFromList)
            {
                DeleteRoom(room);
            }
            else
            {
                AddRoom(room);
            }

        }
    }

    private void AddRoom(RoomInfo room)
    {
        print("Add Room: " + room.Name);
        GameObject obj = Instantiate(RoomNamePrefab, new Vector2(0, 0), Quaternion.identity);
        obj.transform.SetParent(Grid.transform, false);
        obj.GetComponentInChildren<Text>().text = room.Name;
    }

    private void DeleteRoom(RoomInfo room)
    {
        print("Delete Room: " + room.Name);

        int roomCount = Grid.childCount;
        for (int i = 0; i < roomCount; i++)
        {
            if (Grid.GetChild(i).gameObject.GetComponentInChildren<Text>().text == room.Name)
            {
                Destroy(Grid.GetChild(i).gameObject);
            }
        }
    }
}
