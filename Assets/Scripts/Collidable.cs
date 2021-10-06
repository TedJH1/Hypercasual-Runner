using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collidable : MonoBehaviour
{
    public SceneTransition sceneTransition;
    private void Start()
    {
        sceneTransition = GameObject.Find("Canvas").GetComponent<SceneTransition>();
    }
    private void OnCollisionEnter(Collision collision)
    {
        sceneTransition.StartTransition(2);
    }
}
