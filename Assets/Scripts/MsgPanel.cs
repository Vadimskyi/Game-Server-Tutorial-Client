using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MsgPanel : MonoBehaviour
{
    public event Action<string> OnSendMessage = delegate { };

    [SerializeField] private Transform _panelContent;
    [SerializeField] private InputField _input;
    [SerializeField] private Button _sendMsg;
    [SerializeField] private Text _msgPrefab;

    void Start()
    {
        _sendMsg.onClick.RemoveAllListeners();
        _sendMsg.onClick.AddListener(() =>
        {
            OnSendMessage.Invoke(_input.text);
            _input.text = string.Empty;
        });
    }

    public void CreateMsg(string msg)
    {
        var obj = Instantiate(_msgPrefab);
        obj.text = msg;
        obj.transform.SetParent(_panelContent, false);
        obj.gameObject.SetActive(true);
    }
}
