using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GeneratorScript : MonoBehaviour
{
    public GameObject[] availableRooms;
    public List<GameObject> currentRooms;
    private float screenWidthInPoints;

    // Start is called before the first frame update
    void Start()
    {
        float height = 2.0f * Camera.main.orthographicSize;
        screenWidthInPoints = height * Camera.main.aspect;

        StartCoroutine(GeneratorCheck());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void AddRoom(float farthestRoomEndX)
    {
        // Picks a random index of the room type (Prefab) to generate.
        int randomRoomIndex = Random.Range(0, availableRooms.Length);

        // Creates a room object from the array of available rooms using the random index chosen above.
        GameObject room = (GameObject)Instantiate(availableRooms[randomRoomIndex]);

        // Get the size of the floor inside the room
        float roomWidth = room.transform.Find("floor").localScale.x;

        // Set the new room to its correct location
        float roomCenter = farthestRoomEndX + roomWidth * 0.5f;

        // This sets the position of the room.
        room.transform.position = new Vector3(roomCenter, 0, 0);

        // Add the room to the list of current rooms
        currentRooms.Add(room);
    }

    private void GenerateRoomIfRequired()
    {
        // Creates a new list to store rooms that need to be removed.
        List<GameObject> roomsToRemove = new List<GameObject>();

        // This is a flag that shows if you need to add more rooms.
        bool addRooms = true;

        // Saves player position.
        float playerX = transform.position.x;

        // This is the point after which the room should be removed. If room position is behind this point, it needs to be removed.
        float removeRoomX = playerX - screenWidthInPoints;

        //
        float addRoomX = playerX + screenWidthInPoints;

        // You store the point where the level currently ends.
        float farthestRoomEndX = 0;
        foreach (var room in currentRooms)
        {
            // Setting the room parameter.
            float roomWidth = room.transform.Find("floor").localScale.x;
            float roomStartX = room.transform.position.x - (roomWidth * 0.5f);
            float roomEndX = roomStartX + roomWidth;

            // 
            if (roomStartX > addRoomX)
            {
                addRooms = false;
            }
            // Remove the room where out off the screen.
            if (roomEndX < removeRoomX)
            {
                roomsToRemove.Add(room);
            }

            //10
            farthestRoomEndX = Mathf.Max(farthestRoomEndX, roomEndX);
        }
        // This removes rooms that are marked for removal.
        foreach (var room in roomsToRemove)
        {
            currentRooms.Remove(room);
            Destroy(room);
        }
        // AddRoom at farthestRoomEndX.
        if (addRooms)
        {
            AddRoom(farthestRoomEndX);
        }
    }

    private IEnumerator GeneratorCheck()
    {
        while (true)
        {
            GenerateRoomIfRequired();
            yield return new WaitForSeconds(0.25f);
        }
    }

}
