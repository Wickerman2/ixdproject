// NeoPixel Ring simple sketch (c) 2013 Shae Erisson
// released under the GPLv3 license to match the rest of the AdaFruit NeoPixel library

#include <Adafruit_NeoPixel.h>

#ifdef __AVR__
  #include <avr/power.h>
  #include <Servo.h>
  

#endif

#define PIN            7
#define DWN            8
#define TURN_TIME 200

#define NUMPIXELS      60
#define DWNNUMPIXELS      60


Servo myservo;


//Variables
int addPos = 0; 

Adafruit_NeoPixel pixels = Adafruit_NeoPixel(NUMPIXELS, PIN, NEO_GRB + NEO_KHZ800);
Adafruit_NeoPixel unten = Adafruit_NeoPixel(DWNNUMPIXELS, DWN, NEO_GRB + NEO_KHZ800);


int delayval = 30; // delay for half a second

void setup() {
    Serial.begin(19200); // opens serial port, sets data rate to 9600 bps
    pinMode(13, OUTPUT);
#if defined (__AVR_ATtiny85__)
  if (F_CPU == 16000000) clock_prescale_set(clock_div_1);
#endif
  // End of trinket special code

  pixels.setBrightness(250);
  unten.setBrightness(250);

  pixels.begin(); // This initializes the NeoPixel library.
  unten.begin();

  myservo.attach(11);
  myservo.write(90);

}

void loop() {

  for( int i=0;i<NUMPIXELS;i++){
    pixels.setPixelColor(i, pixels.Color(255,215,0)); // Moderately bright green color.
    unten.setPixelColor(i, pixels.Color(255,69,0)); // Moderately bright green color.
    unten.show(); // This sends the updated pixel color to the hardware.
    pixels.show(); // This sends the updated pixel color to the hardware.
    delay(delayval); // Delay for a period of time (in milliseconds).   
  }

if (Serial.available() > 0) {                  
    Serial.read();
    int incomingByte = Serial.parseInt();

    if (incomingByte == 1) {    
              int pos = 160;
              myservo.write(pos);             
              digitalWrite(LED_BUILTIN, HIGH);           
              delay(TURN_TIME);             
              digitalWrite(LED_BUILTIN, LOW);            
              delay(TURN_TIME);
              myservo.write(90);             
    }
  }
}




  
