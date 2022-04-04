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
    private List<ARObject> objectList;

    // Start is called before the first frame update
    void Start() {
        ARObject temp = new ARObject(35.981264f, 126.675968f, "Cube");
        objectList.Add(temp);
    }

    // Update is called once per frame
    void Update() {
        foreach(ARObject obj in objectList) {
            // if object position is within 100 meter
            if((this.GetComponent<LocationManagement>().getLocationDistance(obj.latitude, obj.longitude) < 0.1f)) {

            }
        }
    }
}
