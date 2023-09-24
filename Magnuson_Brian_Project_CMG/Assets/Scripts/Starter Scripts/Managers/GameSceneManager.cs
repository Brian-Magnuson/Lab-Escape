using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameSceneManager : MonoBehaviour
{
    //Like the GameManager, this should be it's own gameobject

    [Tooltip("The black screen transition that will be used")]
    public GameObject Transition;

    [Tooltip("If you want to open this scene with a fade in")]
    public bool startWithFadeIn = true;
    // Start is called before the first frame update
    void Start()
    {
        if (startWithFadeIn)
        {
            StartCoroutine(FadeIn());
        }
    }


    //This function should be called to other scripts so that way you have the transition working
    public void LoadScene(int SceneIndex)
    {
        StartCoroutine(FadeOut());
        StartCoroutine(LoadAsyncScene(SceneIndex));
    }

    IEnumerator LoadAsyncScene(int SceneIndex)
    {
        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(SceneIndex);

        // Wait until the asynchronous scene fully loads
        while (!asyncLoad.isDone)
        {
            yield return null;
        }
    }

    public IEnumerator FadeIn()
    {
        Transition.SetActive(true);
        Transition.GetComponent<Animator>().SetBool("FadeIn", true);
        yield return new WaitForSeconds(1);
        Transition.GetComponent<Animator>().SetBool("FadeIn", false);
        Transition.SetActive(false);
    }

    public IEnumerator FadeOut()
    {
        Transition.SetActive(true);
        Transition.GetComponent<Animator>().SetBool("FadeOut", true);
        yield return new WaitForSeconds(1);
        Transition.GetComponent<Animator>().SetBool("FadeOut", false);
    }
}
