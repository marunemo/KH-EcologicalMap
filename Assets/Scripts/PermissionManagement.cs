using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Android;

public class PermissionManagement : MonoBehaviour {

    public GameObject popupPrefab = null;
    public GameObject canvas = null;

    // Start is called before the first frame update
    void Start() {
        // Camera Permission for android
        if(!Permission.HasUserAuthorizedPermission(Permission.Camera)) {
            Permission.RequestUserPermission(Permission.Camera);
        }

        // Location Permission for android
        if(!Permission.HasUserAuthorizedPermission(Permission.FineLocation)) {
            Permission.RequestUserPermission(Permission.FineLocation);
        }

        // Permission denied message
        if(!(Permission.HasUserAuthorizedPermission(Permission.Camera) && Permission.HasUserAuthorizedPermission(Permission.FineLocation)) || true) {
            Debug.Log("created");
            GameObject popup = Instantiate(popupPrefab, canvas.transform);
            // get the first child of popup panel
            popup.transform.GetChild(0).GetComponent<TextPopup>().resultText = "Permission Denied";
        }
    }

    // Update is called once per frame
    void Update() {

    }
}
