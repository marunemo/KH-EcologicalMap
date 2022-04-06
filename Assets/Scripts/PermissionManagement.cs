using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Android;

public class PermissionManagement : MonoBehaviour {

    public GameObject popupPrefab = null;
    public GameObject canvas = null;

    // Awake is always called before any Start functions and also just after a prefab is instantiated.
    private void Awake() {
        // Game Starting Procedure
        this.GetComponent<LocationManagement>().enabled = false;
        this.GetComponent<ARObjectManager>().enabled = false;
    }
    // Start is called before the first frame update
    void Start() {
        Debug.Log("Permission");
        // Camera and Location Permission for android
        if(!(Permission.HasUserAuthorizedPermission(Permission.Camera) && Permission.HasUserAuthorizedPermission(Permission.FineLocation))) {
            string[] permissions = { Permission.Camera, Permission.FineLocation };
            Permission.RequestUserPermissions(permissions);
        }

        // Permission denied message
#if UNITY_EDITOR
        if(true) {
#else
        if(!(Permission.HasUserAuthorizedPermission(Permission.Camera) && Permission.HasUserAuthorizedPermission(Permission.FineLocation))) {
#endif
            GameObject popup = Instantiate(popupPrefab, canvas.transform);
            // get the first child of popup panel
            popup.transform.GetChild(0).GetComponent<TextPopup>().resultText = "Permission Denied";
#if !UNITY_EDITOR
        }
        else {
#endif
            // if all permission is OK, activate gps service.
            this.GetComponent<LocationManagement>().enabled = true;
        }
    }

    // Update is called once per frame
    void Update() {

    }
}
