using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LocationTextBehavior : MonoBehaviour {
    public float latitude = 0;
    public float longitude = 0;

    private TextMesh textMesh = null;
    private GameObject gameManager = null;

    // Start is called before the first frame update
    void Start() {
        textMesh = this.GetComponent<TextMesh>();
        gameManager = GameObject.Find("GameManager");
    }

    // Update is called once per frame
    void Update() {
        // Since getLocationDistance(Haversine formula) use km units, multiply 1000 to calculate by meters
        textMesh.text = (gameManager.GetComponent<LocationManagement>().getLocationDistance(latitude, longitude) * 1000).ToString() + "m";
    }

    public void setCoordinate(float lat, float lng) {
        this.latitude = lat;
        this.longitude = lng;
    }
}
