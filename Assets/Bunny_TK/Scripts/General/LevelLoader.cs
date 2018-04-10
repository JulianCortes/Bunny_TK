﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelLoader : MonoBehaviour
{
    public void LoadLevelByName(string name)
    {
        SceneManager.LoadScene(name);
    }
    public void LoadLevelByIndex(int index)
    {
        SceneManager.LoadScene(index);
    }
}
