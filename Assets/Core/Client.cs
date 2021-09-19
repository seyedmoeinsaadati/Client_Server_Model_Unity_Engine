using UnityEngine;
using static Server;

public class Client : MonoBehaviour
{
    static Server server;

    public static int requestNumber;

    public string spawerName;

    private void Start()
    {
        server = FindObjectOfType<Server>();
    }

    public void Request()
    {
        server.Request(requestNumber, OnReceiveData);
    }

    public void OnReceiveData(Data data)
    {
        requestNumber++;
        GameObject gameObject = new GameObject(data.id + data.name);
        gameObject.transform.position = data.position;
        Debug.Log("a new game object created.");
    }

    private void Update()
    {
        Request();
    }

}