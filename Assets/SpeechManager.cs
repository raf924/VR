using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Windows.Speech;

public class SpeechManager : MonoBehaviour
{
    private KeywordRecognizer _keywordRecognizer = null;
    private readonly Dictionary<string, System.Action> _keywords = new Dictionary<string, System.Action>();
    private bool _visible = false;

    private void Start()
    {
        _keywords.Add("Menu", ToggleVisibility);

        _keywordRecognizer = new KeywordRecognizer(_keywords.Keys.ToArray());
        _keywordRecognizer.OnPhraseRecognized += KeywordRecognizer_OnPhraseRecognized;
        _keywordRecognizer.Start();
    }

    private void ToggleVisibility()
    {
        var holoMenu = GameObject.Find("HoloMenu");
        if (holoMenu == null) return;

        _visible = !_visible;
        var rend = holoMenu.GetComponent<Renderer>();
        if (rend != null) rend.enabled = _visible;

        //holoMenu.transform.localScale = 3.0f;
        holoMenu.transform.position =
            Camera.main.transform.position + Camera.main.transform.forward;
        holoMenu.transform.rotation = new Quaternion(0.0f, Camera.main.transform.rotation.y, 0.0f, Camera.main.transform.rotation.w);
    }

    private void KeywordRecognizer_OnPhraseRecognized(PhraseRecognizedEventArgs args)
    {
        System.Action keywordAction;
        if (_keywords.TryGetValue(args.text, out keywordAction))
        {
            keywordAction.Invoke();
        }
    }
}