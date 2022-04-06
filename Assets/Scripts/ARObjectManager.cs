using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
class ARObject {
    public float latitude;
    public float longitude;
    public string prefabName;

    public ARObject(float lat, float lng, string name) {
        this.latitude = lat;
        this.longitude = lng;
        this.prefabName = name;
    }
}

[System.Serializable]
class ARObjectArray {
    public ARObject[] objectData;
}

public class ARObjectManager : MonoBehaviour {
    private LocationManagement LocationComponent = null;
    private List<ARObject> objectList = new List<ARObject>();
    private Dictionary<string, GameObject> prefabMap = new Dictionary<string, GameObject>();

    public GameObject AROriginMaster = null;
    public GameObject DistanceText = null;
    public string jsonFileName = "LocationData";

    // Start is called before the first frame update
    void Start() {
        LocationComponent = this.GetComponent<LocationManagement>();

        TextAsset jsonFile = Resources.Load("Storage/" + jsonFileName) as TextAsset;
        if(jsonFile != null) {
            ARObjectArray ARObjects = JsonUtility.FromJson<ARObjectArray>(jsonFile.ToString());
            foreach(ARObject arObj in ARObjects.objectData) {
                objectList.Add(arObj);
            }
        }

        foreach(ARObject obj in objectList) {
            GameObject objPrefab = Resources.Load<GameObject>("Prefabs/" + obj.prefabName);
            if(objPrefab == null) continue;

            // 1' = 111km (<=> 1m = 0.00001')
            GameObject prefabObject = Instantiate(
                    objPrefab,
                    LocationComponent.getRelativePosition(obj.latitude, obj.longitude) * 10000,
                    Quaternion.identity,
                    AROriginMaster.transform
                );

            prefabObject.name = obj.prefabName;
            //prefabObject.SetActive(false);
            prefabMap.Add(obj.prefabName, prefabObject);

            Instantiate(
                    DistanceText,
                    prefabObject.transform.position + new Vector3(0, 1.5f, 0),
                    Quaternion.identity,
                    prefabObject.transform
                )
                .GetComponent<LocationTextBehavior>()
                .setCoordinate(obj.latitude, obj.longitude);
        }
    }

    // Update is called once per frame
    void Update() {
        /*
        foreach(ARObject obj in objectList) {
            // if distance of AR object within 20 meter
            if(LocationComponent.getLocationDistance(obj.latitude, obj.longitude) <= 0.02f) {
                if(prefabMap.ContainsKey(obj.prefabName) && !prefabMap[obj.prefabName].activeSelf)
                    prefabMap[obj.prefabName].SetActive(true);
            }
            else {
                if(prefabMap.ContainsKey(obj.prefabName) && prefabMap[obj.prefabName].activeSelf)
                    prefabMap[obj.prefabName].SetActive(false);
            }
        }
        */
    }
}
