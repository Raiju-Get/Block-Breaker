using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level : MonoBehaviour
{
    [SerializeField] int breakableBlock; // for Debugging
    SceneLoader sceneLoader;

    private void Start()
    {
        sceneLoader = FindObjectOfType<SceneLoader>();
    }
    public void CountBlocks()
    {
        breakableBlock++;
    }

    public void BlockDestoryed()
    {
        breakableBlock--;
        if (breakableBlock <= 0)
        {
            sceneLoader.LoadNextScene();
        }
    }
}
