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
    private List<ARObject> objectList = new List<ARObject>();

    public GameObject AROriginMaster = null;
    public GameObject Cube = null;

    // Start is called before the first frame update
    void Start() {
        objectList.Add(new ARObject(35.981264f, 126.675968f, "Cube"));

        foreach(ARObject obj in objectList) {
            Instantiate(Cube,
                    this.GetComponent<LocationManagement>().getRelativePosition(0, 0, obj.latitude, obj.longitude),
                    Quaternion.identity,
                    AROriginMaster.transform
                ).SetActive(false);
        }
    }

    // Update is called once per frame
    void Update() {
        
    }
}
