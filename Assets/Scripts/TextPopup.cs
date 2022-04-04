using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TextPopup : MonoBehaviour {

    public string resultText = null;

    private float dueTime = 2f;
    private float destroyTIme = 1f;

    private Text popupText = null;
    private Image panelImge = null;

    // Start is called before the first frame update
    void Start() {
        dueTime = 2f;
        destroyTIme = 1f;

        popupText = this.GetComponent<Text>();
        popupText.text = resultText;
        popupText.color = new Color32(50, 50, 50, 255);

        panelImge = GetComponentInParent<Image>();
        panelImge.color = new Color32(255, 255, 255, 125);
    }

    // Update is called once per frame
    void Update() {
        if(dueTime > 0f) {
            dueTime -= Time.deltaTime;
        }
        else {
            destroyTIme -= Time.deltaTime;
            popupText.color = new Color32(50, 50, 50, (byte)(255 * destroyTIme));
            panelImge.color = new Color32(255, 255, 255, (byte)(125 * destroyTIme));
        }

        if(destroyTIme <= 0f) {
            Destroy(this.transform.parent.gameObject);
        }
    }
}
