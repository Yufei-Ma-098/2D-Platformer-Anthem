# 2D Platformer Anthem
For a detailed walkthrough of the project, click on the link below to watch video on YouTube:  
[Watch the Project Demonstration Video here](https://www.youtube.com/watch?v=wOBohWgKPus)

## Introduction
In the epoch of ancient civilizations, between the 6th and 8th centuries BC, a tribe emerged from the boundaries of known existence. A brave follower from this tribe, our hero, embarked on a pilgrimage that unveiled an uncharted realm. This realm, brimming with extraordinary creatures and precious artifacts of unimaginable craftsmanship, captivated many, evolving the hero's journey into an epic expedition of discovery.

## Game Objectives
A pivotal objective awaits players in this game: 'Locate the Altars and Gather the Fragments'. Three elusive altars are hidden in diverse locations across the map. The task at hand for the players is to pinpoint these altars and retrieve the fragments they hold. 

## Characters Design
The game presents the protagonist and his shadow as core characters, embodied as celestial opposites akin to the sun and the moon. Players interact with two types of NPCs — adversaries posing challenges to progress, and storyline NPCs, who drive the narrative and reveal plot twists.  

## Physical Interaction
Our custom gamepad, structured in three vertically aligned sections, offers an immersive experience. The design integrates game-inspired motifs, with the primary electronic components housed in the lower part. Featuring three joysticks and a well-considered button layout, the controller enhances navigation and gameplay.

## Key Technologies
### 1. Scene Management:
Scene transitions are managed using Unity's `SceneManager`. The `BubbleVideoController` class leverages `LoadSceneAsync` for background loading of the next scene while a video plays.
```csharp
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
Coroutines, sequences of actions executed over time, are widely used in Anthem. Notably, the `ElderDialogue` class uses a Coroutine to control the speed of text rendering in the dialogue boxes.
```csharp
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
The game utilizes Arduino as a peripheral controller to add a tangible, physical element to user inputs. The `ArduinoController` class communicates with the Arduino board, translating the status of specific pins into game commands.
```csharp
if ((arduinoController.rPressed) || Input.GetKeyDown(KeyCode.R)) { /*...*/ }
```

### 4. Singleton Pattern:
The Singleton pattern maintains a consistent game state across different scenes, ensuring the persistence of a single instance of a game object.

### 5. Dialogue System:
Anthem includes a bespoke dialogue system that uses Unity's UI system for displaying dialogue boxes and a text file for retrieving dialogues. The `ElderDialogue` class handles dialogue iterations during the conversation.
```csharp
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


