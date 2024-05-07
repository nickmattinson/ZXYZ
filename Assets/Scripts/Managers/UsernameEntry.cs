using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UsernameEntry : MonoBehaviour
{
    public TextMeshProUGUI output;
    public TMP_InputField username;


    public void ButtonDemo()
    {
        output.text = username.text;
    }

}
