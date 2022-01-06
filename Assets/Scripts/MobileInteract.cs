using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MobileInteract : MonoBehaviour {
    // Start is called before the first frame update
    void Start() {

    }

    // Update is called once per frame
    void Update() {
        if(Application.platform == RuntimePlatform.Android) {
            if(Input.GetKey(KeyCode.Escape))
                Application.Quit();
        }
    }

    // ���ø����̼� ����
    public void QuitApplication() {
        Application.Quit();
    }
}