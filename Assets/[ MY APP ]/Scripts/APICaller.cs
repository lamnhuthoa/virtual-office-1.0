using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;
using TMPro;
using System.Text.RegularExpressions;
using Meta.WitAi.TTS.Utilities;

[System.Serializable]
public class AIResponse
{
    public string response;
}

public class APICaller : MonoBehaviour
{
    [SerializeField] private TTSSpeaker _speaker;
    public TMP_InputField userQuestion;
    public Button sendButton;
    public RectTransform userTextMessage, botTextMessage;
    public ScrollRect scroll;
    bool isAnswerReceived = false;

    private float height;

    public void SendQuestion()
    {
        string question = userQuestion.text; 
        if (!string.IsNullOrEmpty(question))
        {
            userQuestion.text = "";
            SendMessageToChat(question, userTextMessage);
            StartCoroutine(Upload(question));
        }
    }

    IEnumerator Upload(string question)
    {
        Debug.Log("Calling ChatGPT service...");
        using (UnityWebRequest www = UnityWebRequest.Post("http://localhost:8000/ask", "{ \"question\": \"" + question + "\"}", "application/json"))
        {
            sendButton.interactable = false;

            yield return www.SendWebRequest();

            if (www.result != UnityWebRequest.Result.Success || www.isNetworkError || www.isHttpError)
            {
                Debug.LogError(www.error);
                string errorMessage = "Sorry. I cannot answer this question.";
                SendMessageToChat(errorMessage, botTextMessage);

                sendButton.interactable = true;
            }
            else
            {
                string jsonData = www.downloadHandler.text;
                AIResponse aiResponse = JsonUtility.FromJson<AIResponse>(jsonData);
                Debug.Log("ChatGPT response: " + aiResponse.response);
                
                string textResponse = aiResponse.response;
                SendMessageToChat(textResponse, botTextMessage);
                
                StartCoroutine(SpeakAsync(textResponse));
            }
        }
    }

    private void Update() 
    {   
        if(!_speaker.IsSpeaking && isAnswerReceived)
        {
            sendButton.interactable = true;
            isAnswerReceived = false;
        } else if(_speaker.IsSpeaking) {
            sendButton.interactable = false;
        }
    }

    private IEnumerator SpeakAsync(string responseText)
    {
        yield return _speaker.SpeakQueuedAsync(new string[] { responseText });
        isAnswerReceived = true;
    }

    public void SendMessageToChat(string textResponse, RectTransform textMessagePrefab) {
        scroll.content.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, 0);

        var item = Instantiate(textMessagePrefab, scroll.content);
        item.GetChild(0).GetChild(0).GetComponent<TMP_Text>().text = textResponse;
        item.anchoredPosition = new Vector2(0, -height);
        LayoutRebuilder.ForceRebuildLayoutImmediate(item);
        height += item.sizeDelta.y;
        scroll.content.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, height);
        scroll.verticalNormalizedPosition = 0;
    }

    void ChangeButtonText (Button sendButton, string textContent) {
        TextMeshProUGUI buttonText = sendButton.GetComponentInChildren<TextMeshProUGUI>();
        if(buttonText != null) {
            buttonText.text = textContent;
        }
    }
}