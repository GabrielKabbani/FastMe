using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NeverEnding : MonoBehaviour{
    public GameObject[] availableRooms;
    public GameObject[] availableObjects;
    public List<GameObject> currentRooms;
    public List<GameObject> objects;

    private float screenWidthInPoints;
    public float objectsMinDistance = 5.0f;
    public float objectsMaxDistance = 10.0f;
    public float objectsMinY = -1.4f;
    public float objectsMaxY = 1.4f;
    public float objectsMinRotation = -45.0f;
    public float objectsMaxRotation = 45.0f;

    void Start(){
        float height = 2.0f * Camera.main.orthographicSize;
        screenWidthInPoints = height * Camera.main.aspect;

        StartCoroutine(NeverEndingCheck());

    }

    void Update(){
    }

    void AddObject(float lastObjectX){
        int randomIndex = Random.Range(0, availableObjects.Length);
        GameObject obj = (GameObject)Instantiate(availableObjects[randomIndex]);
        float objectPositionX = lastObjectX + Random.Range(objectsMinDistance, objectsMaxDistance);
        float randomY = Random.Range(objectsMinY, objectsMaxY);
        obj.transform.position = new Vector3(objectPositionX,randomY,0); 
        float rotation = Random.Range(objectsMinRotation, objectsMaxRotation);

        obj.transform.rotation = Quaternion.Euler(Vector3.forward * rotation);
        objects.Add(obj);            
    }

    void GenerateObjectsIfRequired(){
        float playerX = transform.position.x;
        float removeObjectsX = playerX - screenWidthInPoints;
        float addObjectX = playerX + screenWidthInPoints;
        float farthestObjectX = 0;
        List<GameObject> objectsToRemove = new List<GameObject>();

        foreach (var obj in objects){

            float objX = obj.transform.position.x;
            farthestObjectX = Mathf.Max(farthestObjectX, objX);

            if (objX < removeObjectsX){           
                objectsToRemove.Add(obj);
            }
        }

        foreach (var obj in objectsToRemove){
            objects.Remove(obj);
            Destroy(obj);
        }

        if (farthestObjectX < addObjectX){
            AddObject(farthestObjectX);
        }
    }

    void AddJungle(float farthestRoomEndX){
        GameObject room = (GameObject)Instantiate(availableRooms[0]);

        float roomWidth = room.transform.Find("floor").localScale.x;
        float roomCenter = farthestRoomEndX + roomWidth * 0.48f;
        
        room.transform.position = new Vector3(roomCenter, 0, 0);
        currentRooms.Add(room);
    }

    private void GenerateJungleIfRequired(){
        bool addRooms = true;
        float playerX = transform.position.x;
        float removeRoomX = playerX - screenWidthInPoints;
        float addRoomX = playerX + screenWidthInPoints;
        float farthestRoomEndX = 0;
        List<GameObject> roomsToRemove = new List<GameObject>();

        foreach(var room in currentRooms){
            float roomWidth = room.transform.Find("floor").localScale.x;
            float roomStartX = room.transform.position.x - (roomWidth * 0.5f);
            float roomEndX = roomStartX + roomWidth;

            if (roomStartX > addRoomX){
                addRooms = false;
            }

            if (roomEndX < removeRoomX){
                roomsToRemove.Add(room);
            }

            farthestRoomEndX = Mathf.Max(farthestRoomEndX, roomEndX);
        }
        // Debug.Log("farthestRoomEndX: " + farthestRoomEndX);
        int contador = 0;
        foreach(var room in roomsToRemove){
            // Used to skip the first prefab, for some reason it was deleting the original form of the clones, so in this way he only starts to delete the clones, not the original.
            if (contador == 0){
                contador ++;
                continue;
            }
            
            currentRooms.Remove(room);
            Destroy(room);
        }

        if (addRooms){
            AddJungle(farthestRoomEndX);
        }
    }

    private IEnumerator NeverEndingCheck(){
        while (true){
            GenerateJungleIfRequired();
            GenerateObjectsIfRequired();
            yield return new WaitForSeconds(0.25f);
        }
    }
}
