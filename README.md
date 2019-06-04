# Project DMX Light Controller v2

[![BSD licensed](https://img.shields.io/github/license/Fishezzz/Project-DMX-Light-Controller-v2.0.svg)](https://github.com/Fishezzz/Project-DMX-Light-Controller-v2.0/blob/master/LICENSE)
[![Releases](https://img.shields.io/github/release/Fishezzz/Project-DMX-Light-Controller-v2.0.svg)](https://github.com/Fishezzz/Project-DMX-Light-Controller-v2.0/releases)
[![CodeFactor](https://www.codefactor.io/repository/github/fishezzz/project-dmx-light-controller-v2.0/badge)](https://www.codefactor.io/repository/github/fishezzz/project-dmx-light-controller-v2.0)

## Summary ##
This is my final project for the subject [L OOP 3](http://onderwijsaanbod.vives-zuid.be/syllabi/n/V3M028N.htm#activetab=doelstellingen_idp1887728) (Object Oriented Programming) at my school.<br>
I got this subject in the first semester of the 2nd year of my [professional bachelor](https://www.vives.be/nl/opleidingen/iwt/ict) in [Electronics-ICT](http://onderwijsaanbod.vives-zuid.be/opleidingen/n/SC_52334925.htm#bl=02,04).

This project is a remake of [Project DMX Light Controller](https://github.com/Fishezzz/Project-DMX-Light-Controller), my project in the first semester of the 1st year.

## Project requirements and self-evaluation ##
### Project requirements ###
>The requirements for the project, given by the teacher.

1. The project consists of the realization of a personal idea, implemented in a C# WPF Application. This may be combined with other self-written software, for example: Android app, C-code for a microcontroller, a website, ...
2. The app is crash resistant through thoughtful exception handling.
3. The code is made up of logical blocks (methods) that stimulate the code.
4. You make correct and well-considered use of self-written delegates and events.
5. Data is stored in an external file via serialization.
6. At least one design pattern is used (for example Singleton, MVVM, factory, ...).
7. Polymorphism is used in at least one place.
8. You work with asynchronous methods where necessary (whether or not multi-threaded).
9. The application meets the expectations (smooth and correct operation).
10. The application is divided into different "layers". For example by means of self-written classes and their derivatives (which may be in a Class library).

### Self-evaluation ###
>Evaluation my own work, whether I fulfilled the project requirements.

- [x] 1. The project consists of the realization of a personal idea, implemented in a C# WPF Application. This may be combined with other self-written software, for example: Android app, C-code for a microcontroller, a website, ...<br>
_Check! The idea was to remake my [previous project](https://github.com/Fishezzz/Project-DMX-Light-Controller) and add more flexibility and functionality._
- [x] 2. The app is crash resistant through thoughtful exception handling.<br>
_Check! I have some self-made exceptions, but most of it is preventing exceptions by using if's in critical places._
- [x] 3. The code is made up of logical blocks (methods) that stimulate the code.<br>
_Check!_
- [x] 4. You make correct and well-considered use of self-written delegates and events.<br>
_Check! Delegats and events for transfering data from windows to the mainWindow, and for DataBinding._
- [x] 5. Data is stored in an external file via serialization.<br>
_Check! Data is stored and read into the program. The JSON file contains all 'available' devices, with their characteristics, and makes for easy modification, addition and deletion of devices without changing code._
- [x] 6. At least one design pattern is used (for example Singleton, MVVM, factory, ...).<br>
_Check! The Singleton design pattern is used for the `Logger` class. There is also something that looks like MVVM, but is not 100% MVVM._
- [x] 7. Polymorphism is used in at least one place.<br>
_Check! DmxDevice is the base class for the classes of all the types of DMX devices that are used._
- [ ] 8. You work with asynchronous methods where necessary (whether or not multi-threaded).<br>
_I don't have asynchronous methods, nor use multi-threading, but I found no place to use any of those things._
- [x] 9. The application meets the expectations (smooth and correct operation).<br>
_Check!_
- [x] 10. The application is divided into different "layers". For example by means of self-written classes and their derivatives (which may be in a Class library).<br>
_Check! I have 2 Class libraries, `Logger` and `DMX`. The last one contains all classes for DMX devices and all the `TabItem`'s that have a device specific GUI._

- - - -

&not; Fishezzz &nbsp; &laquo; 4 June 2019 &raquo;
