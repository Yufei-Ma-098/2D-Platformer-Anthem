using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ElderDialogue : MonoBehaviour
{
    [Header("UI components")]
    public GameObject talkUI;
    public Text textLabel;
    public Image faceImage;

    [Header("text")]
    public TextAsset textFile;
    public int index;
    public float textSpeed;

    [Header("profilePicture")]
    public Sprite faceA, faceB;

    bool textFinished;
    bool cancelTyping;

    List<string> textList = new List<string>();

    public Animator animator;
    public PlayerController playerController;

    public ArduinoController arduinoController;

    void Awake()
    {
        getTextFromFile(textFile);
        animator = GameObject.Find("Elder").GetComponent<Animator>();
        arduinoController = GameObject.FindObjectOfType<ArduinoController>();
    }

    private void OnEnable()
    {
        playerController.canMove = false;
        textFinished = true;
        StartCoroutine(SetTextUI());
    }

    void Update()
    {
        if ((arduinoController.rPressed) || Input.GetKeyDown(KeyCode.R))
        {
            if (index == textList.Count)
            {
                playerController.canMove = true;
                gameObject.SetActive(false);
                talkUI.SetActive(false);  // Hide the dialogue UI when dialogue ends
                Debug.Log("Dialogue ended, hiding UI.");
                animator.SetBool("Hitting", false);
                index = 0;
                arduinoController.rPressed = false;  // Reset rPressed when done
            }
            else
            {
                if (textFinished && !cancelTyping)
                {
                    StartCoroutine(SetTextUI());
                }
                else if (!textFinished)
                {
                    cancelTyping = !cancelTyping;
                }
            }
        }
    }


    void getTextFromFile(TextAsset file)
    {
        textList.Clear();
        index = 0;

        var lineData = file.text.Split('\n');

        foreach (var line in lineData)
        {
            textList.Add(line);
        }
    }

    IEnumerator SetTextUI()
    {
        arduinoController.rPressed = false;
        animator.SetBool("Hitting", true);

        textFinished = false;
        textLabel.text = "";

        switch (textList[index])
        {
            case "A":
                faceImage.sprite = faceA;
                index++;
                break;

            case "B":
                faceImage.sprite = faceB;
                index++;
                break;
        }

        int letter = 0;
        while (!cancelTyping && letter < textList[index].Length - 1)
        {
            textLabel.text += textList[index][letter];
            letter++;
            yield return new WaitForSeconds(textSpeed);
        }
        textLabel.text = textList[index];
        cancelTyping = false;
        textFinished = true;
        index++;
    }
}
