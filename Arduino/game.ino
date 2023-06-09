const int leftButtonPin = 2;
const int rightButtonPin = 3;
const int jumpButtonPin = 4;
const int RButtonPin = 5;

void setup() 
{
  Serial.begin(115200);

  pinMode(leftButtonPin, INPUT);
  pinMode(rightButtonPin, INPUT);
  pinMode(jumpButtonPin, INPUT);
  pinMode(RButtonPin, INPUT);
}

void loop() 
{
  int leftButtonState = digitalRead(leftButtonPin);
  int rightButtonState = digitalRead(rightButtonPin);
  int jumpButtonState = digitalRead(jumpButtonPin); 
  int RButtonState = digitalRead(RButtonPin);

  if (leftButtonState == HIGH) 
  {
    Serial.println("LEFT");
    delay(0.001);
  } 
  
  else if (rightButtonState == HIGH) 
  {
    Serial.println("RIGHT");
    delay(0.001);
  } 
  
  else if (jumpButtonState == HIGH) 
  {
    Serial.println("JUMP");
    delay(500);
  } 
  
  else if (RButtonState == HIGH) 
  {
    Serial.println("DIALOGUE");
    delay(100);
  }
}
