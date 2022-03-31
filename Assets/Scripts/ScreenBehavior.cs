using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Android;

public class ScreenBehavior : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        // 안드로이드에 카메라 권한이 없다면, 카메라 권한 요청
        if(Permission.HasUserAuthorizedPermission(Permission.Camera)) {
            Permission.RequestUserPermission(Permission.Camera);
        }

        // 안드로이드에 위치 권한이 없다면, 위치 권한 요청
        if(Permission.HasUserAuthorizedPermission(Permission.FineLocation)) {
            Permission.RequestUserPermission(Permission.FineLocation);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void QuitApplication() {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }
}
