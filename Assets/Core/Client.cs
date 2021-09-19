using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Client : MonoBehaviour
{
    static Server server;

    public string spawerName;

    private void Start()
    {
        server = FindObjectOfType<Server>();
    }

    public void Request()
    {
        server.Request(spawerName, OnReceiveData);
    }

    public void OnReceiveData(GameObject newObj)
    {
        print("Gameobject Received.");
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.N))
        {
            Request();
        }
    }

}