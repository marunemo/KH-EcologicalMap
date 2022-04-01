using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TextPopup : MonoBehaviour {

    public GameObject parentObject = null;
    public string resultText = null;

    private float dueTime = 3f;
    private float destroyTIme = 1f;

    private Text popupText = null;
    private Image panelImge = null;

    private Vector4 color;

    // Start is called before the first frame update
    void Start() {
        dueTime = 3f;
        destroyTIme = 1f;

        popupText = this.GetComponent<Text>();
        popupText.text = resultText;

        panelImge = parentObject.GetComponent<Image>();
        panelImge.color = new Vector4(255, 255, 255, 125);
        color = panelImge.color;
    }

    // Update is called once per frame
    void Update() {
        if(dueTime > 0f) {
            dueTime -= Time.deltaTime;
        }
        else {
            destroyTIme -= Time.deltaTime;
            color.w = 125 * destroyTIme;
        }

        if(destroyTIme <= 0f) {
            Destroy(this.gameObject);
        }
    }
}
