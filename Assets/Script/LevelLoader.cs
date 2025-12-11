using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelLoader : MonoBehaviour
{
    public Animator transition;

    public float transitionTime = 1f;
    public void LoadLevelByName(string name)
    {
        StartCoroutine(LoadLevel(name));
    } 

    IEnumerator LoadLevel(string name) 
    {
        transition.SetTrigger("Start");

        yield return new WaitForSeconds(transitionTime); 

        SceneManager.LoadScene(name);
    }
}   
