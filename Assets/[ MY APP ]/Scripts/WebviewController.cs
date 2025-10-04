using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Vuplex.WebView;

public class WebviewController : MonoBehaviour
{
    public CanvasWebViewPrefab canvasWebView;
    public GameObject parentCanvas;
    public Button closeButton;
    public Button backButton;
    public Button forwardButton;

    void Start()
    {
        closeButton.onClick.AddListener(CloseBrowser);
        backButton.onClick.AddListener(NavigateBack);
        forwardButton.onClick.AddListener(NavigateForward);
    }

    void CloseBrowser()
    {
        parentCanvas.SetActive(false);
    }

    async void NavigateBack()
    {
        Task<bool> canGoBack = canvasWebView.WebView.CanGoBack();
        bool isAbleToGoBack = await canGoBack;

        if (isAbleToGoBack)
        {
            canvasWebView.WebView.GoBack();
        }
    }

    async void NavigateForward()
    {
        Task<bool> canGoForward = canvasWebView.WebView.CanGoForward();
        bool isAbleToGoForward = await canGoForward;

        if (isAbleToGoForward)
        {
            canvasWebView.WebView.GoForward();
        }
    }
}
