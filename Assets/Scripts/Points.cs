using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Points : MonoBehaviour
{
    public static int finalScore;
    public TextMeshProUGUI text;

    private void Start()
    {
        text.text = "Score:\n" + finalScore;
    }
}
