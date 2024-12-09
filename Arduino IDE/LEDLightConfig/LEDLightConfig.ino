//===============================================
// Libraries used in the program
#include <FastLED.h> //uses the FastLED library by Daniel Garcia

//===============================================
// Try to not mess with these variables as the are used throughout the code
String data; //
int Pin; //
String chipset; //
String colorOrder; //
int numLEDS; 
char d1;

//===============================================

void setup() {
  Serial.begin(9600); // Start serial set to desired baude rate and in the software you can set it to anything that you need.
  pinMode(13, OUTPUT);
  While (!Serial) {
    
  }

}
//Most of my methods are probably very poor and could be improved upon and I will be open to improvements that could be made.
void SetupCheck() {
    data = Serial.readString();
    d1 = data.charAt(0);
    switch (d1) {
      case 'U':
      
      break;

      case 'P'
    }
}

void loop() {
  if (Serial.available())

}
