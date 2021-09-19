using System;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using static Client;

// rename gameObject
public class Server : MonoBehaviour
{
    public Queue<ThreadInfo<Data>> requsetQueue = new Queue<ThreadInfo<Data>>();

    public void Request(int requestNumber, Action<Data> callback)
    {
        ThreadStart thread = delegate
        {
            RequestThread(requestNumber, callback);
        };
        new Thread(thread).Start();
    }

    private void RequestThread(int requestNumber, Action<Data> callback)
    {
        Data newData = new Data(requestNumber, "Game Object", Vector3.zero);

        lock (requsetQueue)
        {
            requsetQueue.Enqueue(new ThreadInfo<Data>(newData, callback));
        }
    }

    private void Update()
    {
        DequeueQueue();
    }

    private void DequeueQueue()
    {
        if (requsetQueue.Count > 0)
        {
            for (int i = 0; i < requsetQueue.Count; i++)
            {
                ThreadInfo<Data> threadInfo = requsetQueue.Dequeue();
                threadInfo.callback(threadInfo.parameter);
            }
        }
    }

    [System.Serializable]
    public struct Data
    {
        public int id;
        public string name;
        public Vector3 position;

        public Data(int id, string name, Vector3 position)
        {
            this.id = id;
            this.name = name;
            this.position = position;
        }
    }
}
