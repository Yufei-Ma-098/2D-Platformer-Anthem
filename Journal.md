# Project Development Journal
## Week 1
In the first week, I began the project by following tutorials on YouTube and referring to the Unity documentation. Initially, I created a simple demo of a 2D side-scrolling jump game.

Based on the demo, I focused on expanding the game to meet its requirements. I started by enhancing the character control system. This involved creating a GameObject for the main character, complete with Rigidbody2D and Collider2D components. By utilizing code, I implemented control mechanisms for the character's movement and jumping capabilities.

To introduce more depth to the game, I implemented a health and damage system. Whenever the main character encountered hazardous terrain or NPCs, I utilized the `TakeDamage()` method to reduce the character's health. If the health reached zero, the game would conclude.

### Challenges Encountered
During this phase, I faced several challenges that required resolution:

**1. Excessive Jump Height:** Initially, the character was capable of jumping too high. To rectify this, I adjusted the jumpForce value. By decreasing this value, I effectively reduced the height of the character's jumps. This adjustment was necessary since jumpForce controlled the force applied during a jump.

**2. Lack of Realistic Gravity:** The character's falling motion appeared unrealistic due to insufficient gravitational influence. To address this issue, I fine-tuned the gravity scale to achieve a more appropriate level of gravitational impact.

**3. Inability to Jump:** A problem emerged where the player suddenly lost the ability to jump. Investigation revealed that I had included a check within the `Jump()` function, ensuring that the character could only jump while on the ground. However, I had neglected to update the `isOnGround` status in the `Grounding()` function, resulting in a perpetual false condition within the `Jump()` function. Consequently, the character became unable to jump.

**4. Inability to Jump on Slopes:** The existing code only recognized the character as being on the ground when the collision's normal vector pointed upwards (`col.contacts[0].normal == Vector2.up`). Although this condition sufficed for flat ground, it posed issues on slopes. To overcome this limitation, I modified the code to check if the `y-component` of the collision normal exceeded a specific threshold (e.g., 0.7) instead of solely relying on the normal being exactly equal to Vector2.up. This adjustment allowed for more flexible determination of the character's grounding status, accommodating slopes effectively.

## Week 2
During the second week, my focus shifted towards enhancing the visual aspects and interactions within the game. Specifically, I concentrated on implementing animations for both NPCs and the player. Additionally, I developed an NPC dialogue system, enabling engaging dialogues between NPCs and the player to enhance immersion.

Furthermore, I introduced a new gameplay element known as the Sacrifice Tower. This component involved collecting fragments, necessitating the implementation of a fragment system. Collecting fragments triggered UI updates, providing visual feedback to the player.

### Challenges Encountered
I encountered a specific challenge during this phase:

**1. Skipping Dialogue Line:** The NPC dialogue system consistently skipped a specific dialogue line. After investigation, I identified the cause as an extra space mistakenly added at the end of one of the dialogue lines. Rectifying this formatting error resolved the issue, ensuring the proper progression of NPC dialogues.  

## Week 3
In the third week, I focused on further refining the game's features to enhance the overall player experience. I decided to replace the existing `camerafollow` script with Unity's Cinemachine package to overcome the limitations encountered with irregular game world boundaries. Cinemachine offered more advanced features and improved control over the camera's movement, resulting in a smoother visual experience.

Additionally, due to the growing complexity of the game, I created a new scene to accommodate the expanded gameplay elements and level design.

### Challenges Encountered
During this phase, I faced the following challenges:

**1. Inability to Load the New Scene:** Initially, I encountered difficulties loading the newly created scene. Upon examination, I discovered that I had erroneously placed the function call to load the scene in the wrong location. Correcting the placement of the function call resolved the issue, allowing for the successful loading of the new scene.

**2. Player Running Outside the Game World at Game Start:** At the beginning of the game, the player character would abruptly move beyond the game world's boundaries. Investigation revealed that the issue stemmed from using the incorrect collider type for the camera's boundary. To rectify this, I replaced the `EdgeCollider` with a more appropriate `PolygonCollider`, accurately defining the game world's boundaries and resolving the problem.

## Week 4
This week's focus was primarily on troubleshooting issues related to the connection between Unity and Arduino within my game project. The objective was to use four buttons on an Arduino board to control the player character in Unity.  

### Challenges Encountered
**1. Game Lag Due to Blocking I/O:** The primary challenge was the blocking I/O in the single-threaded model. It led to the game becoming unresponsive and laggy. This was clearly not viable for the project, and a solution was urgently needed.

**2. No Data Transfer in Multi-threaded Model:** I then switched to a multi-threaded model, hoping it would solve the game lag issue. However, it introduced a new problem: Unity was not receiving any data from the Arduino.

**3. Failure of Unity Plugins:** As the next step, I explored several Unity plugins such as Ardity, Udity, and Uduino. Unfortunately, none of these plugins were able to resolve the connection issue between Unity and Arduino.  Given the challenges, I seeked help from the CCI's technical staff. And with Lieven guidance, I made the following modifications to my `ArduinoController` script:
```
try
{
    serialPort = new SerialPort(portName);
    serialPort.PortName = portName;
    serialPort.BaudRate = 9600;
    serialPort.Parity = Parity.None;
    serialPort.StopBits = StopBits.One;
    serialPort.DataBits = 8;
    serialPort.Handshake = Handshake.None;
    serialPort.RtsEnable = true;
    serialPort.DataReceived += new SerialDataReceivedEventHandler(DataReceivedHandler);
    serialPort.Open();
}
catch (Exception e)
{
}
```
This change in code allowed Unity to successfully receive data from Arduino. (Thank you Lieven!!)
