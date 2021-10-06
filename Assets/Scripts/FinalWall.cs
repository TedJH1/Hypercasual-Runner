using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FinalWall : MonoBehaviour
{
    public SceneTransition sceneTransition;
    public int scoreVal;
    private void OnCollisionEnter(Collision collision)
    {
        Points.finalScore = scoreVal;
        sceneTransition.StartTransition(3);
    }
}
