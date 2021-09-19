using System;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class Server : MonoBehaviour
{

    Queue<ThreadInfo<GameObject>> requsetQueue = new Queue<ThreadInfo<GameObject>>();

    public void Request(string gameObjName, Action<GameObject> callback)
    {
        ThreadStart thread = delegate
        {
            RequestThread(gameObjName, callback);
        };

        new Thread(thread).Start();
    }

    private void RequestThread(string gameObjName, Action<GameObject> callback)
    {
        GameObject gameObject = new GameObject(gameObjName);
        lock (requsetQueue)
        {
            requsetQueue.Enqueue(new ThreadInfo<GameObject>(gameObject, callback));
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
                ThreadInfo<GameObject> threadInfo = requsetQueue.Dequeue();
                threadInfo.callback(threadInfo.parameter);
            }
        }
    }
}