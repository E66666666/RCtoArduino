int motorA = 10;
int motorB = 9;
int steerL = 5;
int steerR = 6;

String s = "";
String message = "";
String markerBegin = ">";
String markerSeparate = ":";
String markerEnd = ";";
String cmd = "";
String data = "";

void setup()
{
  Serial.begin(9600);
  pinMode(motorA, OUTPUT);
  pinMode(motorB, OUTPUT);
  pinMode(steerR, OUTPUT);
  pinMode(steerL, OUTPUT);
}

void loop()
{
  if (Serial.available() > 0)
  {
    s+=(char) Serial.read();
  }
  if (s.endsWith(markerEnd))
  {
    message = s;
    s = "";
  }
  parseMessage(message);
  
  if (cmd == "FWD")
  {
    forward(data);
    clearData();
  }
  
  if (cmd == "BWD")
  {
    backward(data);
    clearData();
  }
  
  if (cmd == "LEFT")
  {
    steerLeft(data);
    clearData();
  }
  
  if (cmd == "RITE")
  {
    steerRite(data);
    clearData();
  }
  
  if (cmd == "BRK")
  {
    digitalWrite(motorA, LOW);
    digitalWrite(motorB, LOW);
  }  
  if (message.endsWith(markerEnd))
  {
    clearData();
  }
}

void parseMessage(String message)
{
    cmd = message.substring(message.indexOf(markerBegin)+1, message.indexOf(markerSeparate));
    data = message.substring(message.indexOf(markerSeparate)+1, message.indexOf(markerEnd));
}

void clearData()
{
  cmd = "";
  data = "";
  message = "";
}

void forward(String data)
{
  int power = data.toInt();
  digitalWrite(motorB, LOW);
  analogWrite(motorA, power);
}

void backward(String data)
{
  int power = data.toInt();
  digitalWrite(motorA, LOW);
  analogWrite(motorB, power);
}

void steerLeft(String data)
{
  int power = data.toInt();
  digitalWrite(steerR, LOW);
  analogWrite(steerL, power);
}

void steerRite(String data)
{
  int power = data.toInt();
  digitalWrite(steerL, LOW);
  analogWrite(steerR, power);
}
