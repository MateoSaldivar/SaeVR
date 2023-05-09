using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UIRef : MonoBehaviour
{
    // Start is called before the first frame update
    public static UIRef instance;
    public TMP_Text ScoreText;
    public TMP_Text HPText;
    public TMP_Text AmmoText;

    void Awake()
    {
        instance = this;
    }


}
