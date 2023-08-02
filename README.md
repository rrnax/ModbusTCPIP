# ModbusTCP/IP

![Static Badge](https://img.shields.io/badge/release-v1.0-green)


Library writed in C# about Modbus protocole via TCP/IP connection.
This library allows connection with slave by Sockets and communication with this slave by Modbus protocol using function's codes in Modbus frames. Also project gives classes to create and decode Modbus frames with error handling. Library inlcudes unit tests for better understanding of classes and their methods. Project currently are implemented these function codes from modbus:
  
* Read Coils on Modbus
* Read Discrete Inputs on Modbus
* Read Multiple Holding Registers on Modbus
* Read Input Registers on Modbus
* Write Single Coil on Modbus
* Write Single Holding Register on Modbus
* Write Multiple Coils on Modbus
* Write Multiple Holding Registers on Modbus

Also, like I noticed before you can create your own modbus frames using "creator" class witch inlcudes:
* Header part of frame
* Read PDU
* Write PDU

## Technologies

* .NET 6.0 

Lot of systems used to Modbus protocol are writen in languages liek C++, C and C#, so .NET is the most clearly choice for that project because provide simply creation of DLL.

## What's Next

It was chalageous to understand order of bits in frames about that protocole, but after all i get it and implemented various modbus functions.
Planning in the future:
* Implement less popular functons (8,11,14,17,22,23,43)
* Make interface for user's own functions
* Make Modbus master desktop application
