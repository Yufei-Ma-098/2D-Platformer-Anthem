using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DoormanDialogue : MonoBehaviour
{
    [Header("UI components")]
    public Text textLabel;
    public Image faceImage;

    [Header("text")]
    public TextAsset textFile;
    public int index;
    public float textSpeed;

    [Header("profilePicture")]
    public Sprite faceA, faceB;

    [Header("VideoPlayer")]
    public TowerVideoController towerVideoController;

    bool textFinished;
    bool cancelTyping;

    List<string> textList = new List<string>();

    public Animator animator;
    public PlayerController playerController;

    void Awake()
    {
        getTextFromFile(textFile);
        animator = GameObject.Find("Doorman").GetComponent<Animator>();
    }

    private void OnEnable()
    {
        playerController.canMove = false;
        textFinished = true;
        StartCoroutine(SetTextUI());
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            if (index == textList.Count)
            {
                playerController.canMove = true;
                gameObject.SetActive(false);
                index = 0;

                // Play the video
                towerVideoController.PlayVideo();
            }
            else if (textFinished && !cancelTyping)
            {
                StartCoroutine(SetTextUI());
            }
            else if (!textFinished)
            {
                cancelTyping = !cancelTyping;
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
