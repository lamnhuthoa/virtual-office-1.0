using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using TMPro;

public class WitAIController : MonoBehaviour
{
    public TMP_InputField inputField;

    StringBuilder text = new StringBuilder();

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnPartialTranscription(string _text)
    {
        inputField.text = text.ToString() + _text;
        inputField.caretPosition = inputField.text.Length;
    }

    public void OnFullTranscription(string _text)
    {
        text.Append(_text + " ");
    }

    public void Clear()
    {
        text.Clear();
    }
}
