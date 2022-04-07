using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TempPositionMng : MonoBehaviour
{
    public GameObject ResultText;

    Text text;
    // Start is called before the first frame update
    void Start()
    {
        text = ResultText.GetComponent<Text>();
    }

    // Update is called once per frame
    void Update()
    {
        string result = "";
        for(int i = 0; i < transform.childCount; i++) {
            Transform child = this.transform.GetChild(i);
            result += "" + child.name + " : " + child.position.x + ", " + child.position.z + "\n";
            result += "(" + child.transform.rotation.x + ", " + child.transform.rotation.y + ", " + child.transform.rotation.z + ")";
        }
        text.text = result;
    }
}
