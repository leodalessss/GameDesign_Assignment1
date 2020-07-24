using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ClippyText : MonoBehaviour
{
    public Text text;
    public string[] somethingToSay;
    bool allowSpeech;
    int activateSpeech;
    // Start is called before the first frame update
    void Start()
    {
        activateSpeech = Random.Range(0, 3);
        if (activateSpeech >= 1)
        {
            gameObject.SetActive(false);
        }
        int SelectWhatToSay = Random.Range(0, somethingToSay.Length);
        text.text = somethingToSay[SelectWhatToSay].ToString();
    }

    // Update is called once per frame
    void LateUpdate()
    {
        transform.LookAt(Camera.main.transform);
        transform.Rotate(0, 180, 0);
    }
}
