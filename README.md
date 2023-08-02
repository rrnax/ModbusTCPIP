# ModbusTCP/IP

![Static Badge](https://img.shields.io/badge/release-v1.0-green)
[![MIT License](https://img.shields.io/badge/License-MIT-green.svg)](https://choosealicense.com/licenses/mit/)
[![GPLv3 License](https://img.shields.io/badge/License-GPL%20v3-yellow.svg)](https://opensource.org/licenses/)
[![AGPL License](https://img.shields.io/badge/license-AGPL-blue.svg)](http://www.gnu.org/licenses/agpl-3.0)

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

## Instalation

NuGet package with library:
```shell
$ dotnet add package ModbusTCPIP
```
or version:
```shell
$ dotnet add package ModbusTCPIP --version 1.0.0
```

You can also take DLL file from code and add to dependencies or download all code from github.

## How to use

Connect with slave and read holding registers:
```C#
using ModbusTCPIP;

string slaveIP = "192.167.8.11";
int slavePort = 502;
ModbusConnection mySlave = new ModbusConnection(slaveIP, slavePort);
int registerAddress = 1;
int range = 20;

mySlave.Connect();
List<int> slaveRegisters = mySlave.ReadMultipleHoldingRegisters(registerAddress, range);
mySlave.Disconnect();
```

Connect with slave write and then read holding Register values:
```C#
using ModbusTCPIP;

string slaveIP = "192.167.8.11";
int slavePort = 502;
ModbusConnection mySlave = new ModbusConnection(slaveIP, slavePort);
int registerAddress = 1;
int range = 7;
int[] values = { 122, 1111, 334, 1, 7688, 21000, 50 };

mySlave.Connect();
mySlave.WriteMultipleHoldingRegisters(registerAddress, range, values);
List<int> slaveRegisters = mySlave.ReadMultipleHoldingRegisters(registerAddress, range);
mySlave.Disconnect();
```

Create frame for communication:
```C#
using ModbusTCPIP;

int unitId = 1;
int function = 3;
int[] parametrs = { 3, 10 };
int[] values = { 122, 1111, 334, 1, 7688, 21000, 50 };

//Generally
byte[] frame = ModBusFrameCreator.CreateFrame(unitId, function, parametrs);

//Partly
byte[] header = ModBusFrameCreator.CreateMBAPHeader(unitId, 2);
byte[] pduR = ModBusFrameCreator.ReadingPDU(parametrs[0], parametrs[1], function); //For read functions
byte[] pduW = ModBusFrameCreator.MultipleWritingPDU(parametrs[0], parametrs[1], function, values); //For write multiple, but you can also write Single
frame = header.Concat(pduR).ToArray(); //Concat at the end
```

Decode frame:
```C#
using ModbusTCPIP;

int unitId = 1;
int function = 3;
int[] parametrs = { 3, 10 };

byte[] frame = ModBusFrameCreator.CreateFrame(unitId, function, parametrs);
List<int> decoded = new ModbusFrameObject(frame).DecodeRegisters();
```

## Tests
Unit test are available in TestModbusTCPIP.
Code is coverage by tests about 80%.

## License
ModbusTCPIP is released under the MIT license.

