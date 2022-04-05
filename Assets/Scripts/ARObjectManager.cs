using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

public class ARObjectManager : MonoBehaviour {
    private LocationManagement LocationComponent = null;
    private List<ARObject> objectList = new List<ARObject>();
    private Dictionary<string, GameObject> prefabMap = new Dictionary<string, GameObject>();

    public GameObject AROriginMaster = null;

    // Start is called before the first frame update
    void Start() {
        LocationComponent = this.GetComponent<LocationManagement>();

        objectList.Add(new ARObject(35.981264f, 126.675968f, "Cube"));

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
            prefabObject.SetActive(false);
            prefabMap.Add(obj.prefabName, prefabObject);
        }
    }

    // Update is called once per frame
    void Update() {
        foreach(ARObject obj in objectList) {
            // if distance of AR object within 10 meter
            if(LocationComponent.getLocationDistance(obj.latitude, obj.longitude) <= 0.01f) {
                if(prefabMap.ContainsKey(obj.prefabName) && !prefabMap[obj.prefabName].activeSelf)
                    prefabMap[obj.prefabName].SetActive(true);
            }
            else {
                if(prefabMap.ContainsKey(obj.prefabName) && prefabMap[obj.prefabName].activeSelf)
                    prefabMap[obj.prefabName].SetActive(false);
            }
        }
    }
}
