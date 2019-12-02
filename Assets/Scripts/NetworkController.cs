using System.Collections;
using System.Collections.Generic;
using SocketIO;
using UnityEngine;

[RequireComponent(typeof(SocketIOComponent))]
public class NetworkController : MonoBehaviour
{
    private SocketIOComponent _socketComponent;
    private MsgPanel _msgPanel;

    private void Start()
    {
        _socketComponent = GetComponent<SocketIOComponent>();
        _msgPanel = FindObjectOfType<MsgPanel>();
        _msgPanel.OnSendMessage += _msgPanel_OnSendMessage;
        ConnectToServer();
    }

    private void ConnectToServer()
    {
        _socketComponent.Connect();
        _socketComponent.On("say_hi", OnHiCallback);
    }

    private void OnHiCallback(SocketIOEvent args)
    {
        _msgPanel.CreateMsg(args.data["msg"].ToString());
    }

    private void _msgPanel_OnSendMessage(string msg)
    {
        var data = new Dictionary<string, string>();
        data.Add("msg", msg);
        _socketComponent.Emit("say_hi", new JSONObject(data));
    }
}
