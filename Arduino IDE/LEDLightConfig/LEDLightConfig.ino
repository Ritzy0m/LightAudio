#include <FastLED.h> //uses the FastLED library by Daniel Garcia

String data;
char d1;

void setup() {
  Serial.begin(9600); // Start serial set to desired baude rate and in the software you can set it to anything that you need.
  pinMode(13, OUTPUT);


}

void loop() {
  if (Serial.available()) 
  {
    data = Serial.readString();
    d1 = data.charAt(0);
    Serial.println(data);
    if (d1 == 'a') 
    {
      digitalWrite(13, HIGH);
    }
    else if (d1 == 'A')
    {
      digitalWrite(13, LOW);
    }
  }

}
