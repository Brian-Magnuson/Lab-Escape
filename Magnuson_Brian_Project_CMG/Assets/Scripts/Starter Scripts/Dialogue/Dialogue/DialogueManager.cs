using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System;
using TMPro;


/********************
 * DIALOGUE MANAGER *
 ********************
 * This Dialogue Manager is what links your dialogue which is sent by the Dialogue Trigger to Unity
 *
 * The Dialogue Manager navigates the sent text and prints it to text objects in the canvas and will toggle
 * the Dialogue Box when appropriate
 * 
 * A script by Michael O'Connell, extended by Benjamin Cohen, further extended by Eric Bejleri, and then extended even FURTHER again by Benjamin Cohen
 */

public class DialogueManager : MonoBehaviour
{
    [Header("UI Elements")]
    [Tooltip("your fancy canvas image that holds your text objects")]
    public GameObject DialogueUI;


    [Tooltip("Your text body")]
    public TMP_Text TextBox; // the text body
    [Tooltip("the text body of the name you want to display")]
    public TMP_Text NameText; // the text body of the name you want to display
    [Tooltip("Image where the speaker images will appear")]
    public Image speaker; //Image where the speaker images will appear
    [Tooltip("This is the little indicator that the next text can be shown (add a floater to it for added effect)")]
    public GameObject continueImage;

    [Tooltip("This is whether or not you want scrolling text in your game, or instant text")]
    public bool isScrollingText = true;
    [Tooltip("Seconds per letter")]
    public float typeSpeed = 0.01f;


    // private bool isOpen; // represents if the dialogue box is open or closed

    private Queue<string> inputStream = new Queue<string>(); // stores dialogue


    /* IMPORTANT IMPORTANT IMPORTANT IMPORTANT */
    /*This variable will store the script we need to access to stop player movement! If you are using a different movement script
     * make sure to change this variable appropriately and to make sure the movement script has a way to prevent character movement!
    */
    private PlayerMovement playerMovement;

    /* ^ IMPORTANT IMPORTANT IMPORTANT IMPORTANT ^*/

    [HideInInspector]
    public DialogueTrigger currentTrigger;

    private bool levelBool = false;
    private int levelIndex;

    private bool isInDialouge = false;
    private bool isTyping = false;
    private bool cancelTyping = false;

    [Header("Dialogue Image")]
    [Tooltip("Invisible/Placeholder sprite for when no one is talking")]
    public Sprite invisSprite;

    [Tooltip("SpeakerLibrary Object goes in here to access your entire list of speakers")]
    public SpeakerLibrary speakerLibrary;
    [HideInInspector]
    public List<string> speakerSpriteNames;



    [Header("Options")]
    public bool freezePlayerOnDialogue = true;

    private void Start()
    {

        foreach (SpeakerLibrary.SpriteInfo info in speakerLibrary.speakerLibrary)
        {
            speakerSpriteNames.Add(info.name);
        }
        speaker.sprite = invisSprite;
    }

    private void FreezePlayer()
    {
        playerMovement = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerMovement>();
        playerMovement.disabled = true;
    }

    private void UnFreezePlayer()
    {
        playerMovement.disabled = false;
    }

    public void StartDialogue(Queue<string> dialogue)
    {
        isInDialouge = true;
        speaker.sprite = invisSprite; //Clear the speaker
        DialogueUI.SetActive(true);
        continueImage.SetActive(false);
        if (freezePlayerOnDialogue)
        {
            FreezePlayer();
        }


        // open the dialogue box
        // isOpen = true;
        inputStream = dialogue; // store the dialogue from dialogue trigger
        PrintDialogue(); // Prints out the first line of dialogue
    }

    public void AdvanceDialogue() // call when a player presses a button in Dialogue Trigger
    {
        PrintDialogue();
    }

    private void PrintDialogue()
    {
        if (inputStream.Peek().Contains("EndQueue")) // special phrase to stop dialogue
        { 
            // Clear Queue
            if (!isTyping)
            {
                inputStream.Dequeue();
                EndDialogue();
            }
            else
            {
                cancelTyping = true;
            }
        }
        else if (inputStream.Peek().Contains("[NAME=")) //Set the name of the speaker
        {
            string name = inputStream.Peek();
            name = inputStream.Dequeue().Substring(name.IndexOf('=') + 1, name.IndexOf(']') - (name.IndexOf('=') + 1));
            NameText.text = name;
            PrintDialogue(); // print the rest of this line
        }
        else if (inputStream.Peek().Contains("[LEVEL=")) //On dialogue finish, go to following level
        {
            string part = inputStream.Peek();
            string level = inputStream.Dequeue().Substring(part.IndexOf('=') + 1, part.IndexOf(']') - (part.IndexOf('=') + 1));
            int levelIndex = Convert.ToInt32(level); //Convert string to integer
            levelBool = true;
            PrintDialogue(); // print the rest of this line
        }
        else if (inputStream.Peek().Contains("[SPEAKERSPRITE="))//The sprite of the speaker if you have it
        {
            string part = inputStream.Peek();
            string spriteName = inputStream.Dequeue().Substring(part.IndexOf('=') + 1, part.IndexOf(']') - (part.IndexOf('=') + 1));
            if (spriteName != "")
            {
                speaker.sprite = speakerLibrary.speakerLibrary[speakerSpriteNames.IndexOf(spriteName)].sprite; //sets the speaker sprite to corresponding sprite
            }
            else
            {
                speaker.sprite = invisSprite;
            }
            PrintDialogue(); // print the rest of this line


        }
        else
        {
            if (isScrollingText)//This deals with all the scrolling text
            {
                if (!isTyping)
                {
                    string textString = inputStream.Dequeue();
                    StartCoroutine(TextScroll(textString));
                }
                else if (isTyping && !cancelTyping)
                {
                    cancelTyping = true;
                }
            }
            else
            {
                TextBox.text = inputStream.Dequeue();
                continueImage.SetActive(true);
            }
        }


    }

    private IEnumerator TextScroll(string lineOfText)//We got a coroutine here to actually deal with the text 
    {
        continueImage.SetActive(false);
        int letter = 0;
        TextBox.text = "";
        isTyping = true;
        cancelTyping = false;
        while (isTyping && !cancelTyping && (letter < lineOfText.Length - 1))
        {
            TextBox.text += lineOfText[letter];
            letter++;
            yield return new WaitForSeconds(typeSpeed);
        }

        TextBox.text = lineOfText;
        continueImage.SetActive(true);
        isTyping = false;
        cancelTyping = false;
    }

    //string HandleColorSyntax(string lineOfText, int letter) //If you want to be fancy you can try to implement this function and put it in the coroutine if you want colors in your text

    public void EndDialogue()
    {
        TextBox.text = "";
        NameText.text = "";
        inputStream.Clear();
        DialogueUI.SetActive(false);

        isInDialouge = false;
        cancelTyping = false;
        isTyping = false;
        // isOpen = false;
        if (freezePlayerOnDialogue)
        {
            UnFreezePlayer();
        }
        if (levelBool)
        {
            GameObject.FindObjectOfType<GameSceneManager>().LoadScene(levelIndex);
        }
        if (currentTrigger.singleUseDialogue)
        {
            currentTrigger.hasBeenUsed = true;
        }
        inputStream.Clear();
    }
}
