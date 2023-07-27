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
