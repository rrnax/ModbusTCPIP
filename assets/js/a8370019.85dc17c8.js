"use strict";(self.webpackChunkdocs=self.webpackChunkdocs||[]).push([[432],{3905:(e,t,n)=>{n.d(t,{Zo:()=>u,kt:()=>k});var r=n(7294);function l(e,t,n){return t in e?Object.defineProperty(e,t,{value:n,enumerable:!0,configurable:!0,writable:!0}):e[t]=n,e}function o(e,t){var n=Object.keys(e);if(Object.getOwnPropertySymbols){var r=Object.getOwnPropertySymbols(e);t&&(r=r.filter((function(t){return Object.getOwnPropertyDescriptor(e,t).enumerable}))),n.push.apply(n,r)}return n}function i(e){for(var t=1;t<arguments.length;t++){var n=null!=arguments[t]?arguments[t]:{};t%2?o(Object(n),!0).forEach((function(t){l(e,t,n[t])})):Object.getOwnPropertyDescriptors?Object.defineProperties(e,Object.getOwnPropertyDescriptors(n)):o(Object(n)).forEach((function(t){Object.defineProperty(e,t,Object.getOwnPropertyDescriptor(n,t))}))}return e}function a(e,t){if(null==e)return{};var n,r,l=function(e,t){if(null==e)return{};var n,r,l={},o=Object.keys(e);for(r=0;r<o.length;r++)n=o[r],t.indexOf(n)>=0||(l[n]=e[n]);return l}(e,t);if(Object.getOwnPropertySymbols){var o=Object.getOwnPropertySymbols(e);for(r=0;r<o.length;r++)n=o[r],t.indexOf(n)>=0||Object.prototype.propertyIsEnumerable.call(e,n)&&(l[n]=e[n])}return l}var s=r.createContext({}),p=function(e){var t=r.useContext(s),n=t;return e&&(n="function"==typeof e?e(t):i(i({},t),e)),n},u=function(e){var t=p(e.components);return r.createElement(s.Provider,{value:t},e.children)},c="mdxType",d={inlineCode:"code",wrapper:function(e){var t=e.children;return r.createElement(r.Fragment,{},t)}},g=r.forwardRef((function(e,t){var n=e.components,l=e.mdxType,o=e.originalType,s=e.parentName,u=a(e,["components","mdxType","originalType","parentName"]),c=p(n),g=l,k=c["".concat(s,".").concat(g)]||c[g]||d[g]||o;return n?r.createElement(k,i(i({ref:t},u),{},{components:n})):r.createElement(k,i({ref:t},u))}));function k(e,t){var n=arguments,l=t&&t.mdxType;if("string"==typeof e||l){var o=n.length,i=new Array(o);i[0]=g;var a={};for(var s in t)hasOwnProperty.call(t,s)&&(a[s]=t[s]);a.originalType=e,a[c]="string"==typeof e?e:l,i[1]=a;for(var p=2;p<o;p++)i[p]=n[p];return r.createElement.apply(null,i)}return r.createElement.apply(null,n)}g.displayName="MDXCreateElement"},3340:(e,t,n)=>{n.r(t),n.d(t,{assets:()=>s,contentTitle:()=>i,default:()=>d,frontMatter:()=>o,metadata:()=>a,toc:()=>p});var r=n(7462),l=(n(7294),n(3905));const o={sidebar_position:2},i=void 0,a={unversionedId:"ModbusConnection",id:"ModbusConnection",title:"ModbusConnection",description:"Class Definition",source:"@site/docs/ModbusConnection.md",sourceDirName:".",slug:"/ModbusConnection",permalink:"/ModbusConnection",draft:!1,tags:[],version:"current",sidebarPosition:2,frontMatter:{sidebar_position:2},sidebar:"tutorialSidebar",previous:{title:"ModbusTCP_IP",permalink:"/"},next:{title:"ModbusFrameObject",permalink:"/ModbusFrameObject"}},s={},p=[{value:"Class Definition",id:"class-definition",level:3},{value:"Constructors",id:"constructors",level:3},{value:"Properites",id:"properites",level:3},{value:"Methods",id:"methods",level:3}],u={toc:p},c="wrapper";function d(e){let{components:t,...n}=e;return(0,l.kt)(c,(0,r.Z)({},u,n,{components:t,mdxType:"MDXLayout"}),(0,l.kt)("h3",{id:"class-definition"},"Class Definition"),(0,l.kt)("p",null,"ModbusConnection class make and represents connection with slave."),(0,l.kt)("pre",null,(0,l.kt)("code",{parentName:"pre",className:"language-csharp"},"public class ModbusConnection;\n")),(0,l.kt)("h3",{id:"constructors"},"Constructors"),(0,l.kt)("pre",null,(0,l.kt)("code",{parentName:"pre",className:"language-csharp"},"public ModbusConnection()\npublic ModbusConnection(string ip)\npublic ModbusConnection(string ip, int port)\npublic ModbusConnection(string ip, int port, int unitId)\n")),(0,l.kt)("p",null,"Default ip is on loop back, default port is 502, default unit id is 1."),(0,l.kt)("h3",{id:"properites"},"Properites"),(0,l.kt)("p",null,"Cover all information about connected slave."),(0,l.kt)("pre",null,(0,l.kt)("code",{parentName:"pre",className:"language-csharp"},"string slaveIp;\nint slavePort;\nint slaveUnitId;\nbool Connnected;\n")),(0,l.kt)("h3",{id:"methods"},"Methods"),(0,l.kt)("pre",null,(0,l.kt)("code",{parentName:"pre",className:"language-csharp"},"Connect()\n")),(0,l.kt)("p",null,"Return type: void"),(0,l.kt)("p",null,"Get connected with slave or throw Socekt Exception."),(0,l.kt)("hr",null),(0,l.kt)("pre",null,(0,l.kt)("code",{parentName:"pre",className:"language-csharp"},"Disconnect() \n")),(0,l.kt)("p",null,"Return type: void"),(0,l.kt)("p",null,"Brake connection with slave and close Sockets."),(0,l.kt)("hr",null),(0,l.kt)("pre",null,(0,l.kt)("code",{parentName:"pre",className:"language-csharp"},"ReadCoils(int firstCoilAddress, int rangeOfCoils)\n")),(0,l.kt)("p",null,"Return type: ",(0,l.kt)("inlineCode",{parentName:"p"},"List<bool>")),(0,l.kt)("p",null,"Read range of coils from slave, Modbus function code 1."),(0,l.kt)("hr",null),(0,l.kt)("pre",null,(0,l.kt)("code",{parentName:"pre",className:"language-csharp"},"ReadDiscreteInputs(int firstInputAddress, int rangeOfDiscreteInputs)\n")),(0,l.kt)("p",null,"Return type: ",(0,l.kt)("inlineCode",{parentName:"p"},"List<bool>")),(0,l.kt)("p",null,"Read Discrete Inputs On/Off from slave, Modbus function code 2."),(0,l.kt)("hr",null),(0,l.kt)("pre",null,(0,l.kt)("code",{parentName:"pre",className:"language-csharp"},"ReadMultipleHoldingRegisters(int firstRegisterAddress, int rangeOfRegisters)\n")),(0,l.kt)("p",null,"Return type: ",(0,l.kt)("inlineCode",{parentName:"p"},"List<int>")),(0,l.kt)("p",null,"Read Holding resgisters from slave, Modbus function code 3."),(0,l.kt)("hr",null),(0,l.kt)("pre",null,(0,l.kt)("code",{parentName:"pre",className:"language-csharp"},"ReadInputRegisters(int firstRegisterAddress, int rangeOfRegisters)\n")),(0,l.kt)("p",null,"Return type: ",(0,l.kt)("inlineCode",{parentName:"p"},"List<int>")),(0,l.kt)("p",null,"Read Input regsisters from slave, Modbus function code 4."),(0,l.kt)("hr",null),(0,l.kt)("pre",null,(0,l.kt)("code",{parentName:"pre",className:"language-csharp"},"WriteSingleCoil(int coilAddress, int coilValue)\n")),(0,l.kt)("p",null,"Return type: ",(0,l.kt)("inlineCode",{parentName:"p"},"bool")),(0,l.kt)("p",null,"Write one coil by addres and given value, function code 5."),(0,l.kt)("hr",null),(0,l.kt)("pre",null,(0,l.kt)("code",{parentName:"pre",className:"language-csharp"},"WriteSingleHoldingRegister(int holdingRegisterAddress, int holdingRegisterValue)\n")),(0,l.kt)("p",null,"Return type: ",(0,l.kt)("inlineCode",{parentName:"p"},"bool")),(0,l.kt)("p",null,"Write one specific holding register by address and given value, Modbus function code 6."),(0,l.kt)("hr",null),(0,l.kt)("pre",null,(0,l.kt)("code",{parentName:"pre",className:"language-csharp"},"WriteMultipleCoils(int startCoilAddress, int rangeOfCoils, int[] coilsValues)\n")),(0,l.kt)("p",null,"Return type: ",(0,l.kt)("inlineCode",{parentName:"p"}," bool")),(0,l.kt)("p",null,"Write range of coils with specific values in array, Modbus function code 15."),(0,l.kt)("hr",null),(0,l.kt)("pre",null,(0,l.kt)("code",{parentName:"pre",className:"language-csharp"},"WriteMultipleHoldingRegisters(int startRegisterAddress, int rangeOfRegisters, int[] registersValues)\n")),(0,l.kt)("p",null,"Return type: ",(0,l.kt)("inlineCode",{parentName:"p"},"bool")),(0,l.kt)("p",null,"Write range of holding registers with sepecific values in array, Modbus function code 16."),(0,l.kt)("hr",null),(0,l.kt)("pre",null,(0,l.kt)("code",{parentName:"pre",className:"language-csharp"},"ReadFrame(byte[] frame)\n")),(0,l.kt)("p",null,"Return type: void"),(0,l.kt)("p",null,"Write to conslo modbus Frame given in bytes array."))}d.isMDXComponent=!0}}]);