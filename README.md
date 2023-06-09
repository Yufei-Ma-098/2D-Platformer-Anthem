# Anthem
## Project Overview
This project is an interactive 3D game developed in Unity. It utilizes the Unity Engine to create engaging gameplay, dialogue, and cutscenes. It also blends interactive gameplay elements with real-world physical interactions, facilitated by an Arduino controller.

## Key Technologies
### 1. Scene Management:
Unity's `SceneManager` is used to handle the transitions between different game scenes. The `BubbleVideoController` class uses `LoadSceneAsync` method from the `SceneManager` to load the next scene asynchronously in the background while a video is playing. Once the video ends, the scene activation is allowed:
```
void Start()
{
    asyncOperation = SceneManager.LoadSceneAsync(nextSceneName);
    asyncOperation.allowSceneActivation = false;
    /*...*/
}
public void OnVideoEnd(VideoPlayer vp)
{
    asyncOperation.allowSceneActivation = true;
}
```

### 2. Coroutines:
Coroutines allow sequences of actions to be executed over time. They're extensively used in the game, especially in the `ElderDialogue` class for the dialogue system. For example, the `SetTextUI` function uses a Coroutine to control the speed of the text rendering in the dialogue boxes:
```
IEnumerator SetTextUI()
{
    /*...*/
    while (!cancelTyping && letter < textList[index].Length - 1)
    {
        textLabel.text += textList[index][letter];
        letter++;
        yield return new WaitForSeconds(textSpeed);
    }
    /*...*/
}
```

### 3. Interaction with External Hardware:
In this game, Arduino is used as a peripheral controller for accepting user inputs, which gives the game a more tangible, physical element. The `ArduinoController` class communicates with the Arduino board, checking the status of specific pins and translating them into game commands. For instance, the `rPressed` property of the `ArduinoController` is used to control player interaction within the game:
```
if ((arduinoController.rPressed) || Input.GetKeyDown(KeyCode.R)) { /*...*/ }
```

### 4. Singleton Pattern:
Singleton pattern is used for managing the game state throughout different scenes. It helps to maintain a single instance of a game object across multiple scenes.

### 5. Dialogue System:
A customized dialogue system is created, which leverages Unity's UI system for displaying dialogue boxes and uses a text file to retrieve the dialogues. The `ElderDialogue` class reads from a  `TextAsset` file, stores the dialogues in a list, and iterates over them during the conversation:
```
void getTextFromFile(TextAsset file)
{
    /*...*/
    var lineData = file.text.Split('\n');
    foreach (var line in lineData)
    {
        textList.Add(line);
    }
}
```

