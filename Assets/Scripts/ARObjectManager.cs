using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;

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
    public Vector3 northAngle = Vector3.zero;

    // Start is called before the first frame update
    void Start() {
        Debug.Log("AR");
        LocationComponent = this.GetComponent<LocationManagement>();

        TextAsset jsonFile = Resources.Load("Storage/" + jsonFileName) as TextAsset;
        if(jsonFile != null) {
            ARObjectArray ARObjects = JsonUtility.FromJson<ARObjectArray>(jsonFile.ToString());
            foreach(ARObject arObj in ARObjects.objectData) {
                objectList.Add(arObj);
            }
        }

        GameObject ARCoordinate = new GameObject("AR Coordinate");
        ARCoordinate.transform.SetParent(AROriginMaster.transform);
        ARCoordinate.transform.position = Vector3.zero;
        ARCoordinate.transform.eulerAngles = northAngle;
        ARCoordinate.transform.rotation = Quaternion.Euler(0, ARCoordinate.transform.rotation.y, 0);

        foreach(ARObject obj in objectList) {
            GameObject objPrefab = Resources.Load<GameObject>("Prefabs/" + obj.prefabName);
            if(objPrefab == null) continue;

            // Since getRelativePosition(Haversine formula) use km units, multiply 1000 to calculate by meters(1 point in unity coordinate)
            GameObject prefabObject = Instantiate(
                    objPrefab,
                    ARCoordinate.transform
                );
            prefabObject.transform.localPosition = LocationComponent.getRelativePosition(obj.latitude, obj.longitude) * 1000;
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

        // AROriginMaster.GetComponent<ARSessionOrigin>().MakeContentAppearAt(ARCoordinate.transform, ARCoordinate.transform.position, ARCoordinate.transform.rotation);
    }

    // Update is called once per frame
    void Update() {
        foreach(ARObject obj in objectList) {
            /*
            // if distance of AR object within 20 meter
            if(LocationComponent.getLocationDistance(obj.latitude, obj.longitude) <= 0.02f) {
                if(prefabMap.ContainsKey(obj.prefabName) && !prefabMap[obj.prefabName].activeSelf)
                    prefabMap[obj.prefabName].SetActive(true);
            }
            else {
                if(prefabMap.ContainsKey(obj.prefabName) && prefabMap[obj.prefabName].activeSelf)
                    prefabMap[obj.prefabName].SetActive(false);
            }
            */
        }
    }
}
