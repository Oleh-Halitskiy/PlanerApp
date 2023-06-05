# Planer App

This project was created during Spring semester in VIKO (Lithuania, Vilnius) for Web Services subject final task.

## Overview

The Calendar Notes App is a Windows application designed to provide users with a convenient and user-friendly calendar interface combined with a note-taking feature. The primary purpose of this app is to allow users to manage their schedules effectively and keep track of important events and tasks.

The app leverages the power of SQL database for storing and retrieving data, ensuring that user notes are securely saved and readily accessible. It utilizes the REST (Representational State Transfer) architectural style to enable seamless communication between the client application and the backend server.

Written in C# programming language, the Calendar Notes App showcases the capabilities of the Windows Presentation Foundation (WPF) framework. This technology allows for the creation of visually appealing and interactive user interfaces, enhancing the overall user experience.

## How to Setup the Project

**Prerequisites**:
1. Visual Studio 2022
2. .NET 6 (possible to run on .NET 7 but not tested)
3. SQL Server Management Studio Management Studio 19 with SQL Server (SQL Express used for this app)
4. Windows 10/11

**Importing the database**:
1. Open SQL Server Management Studio Management Studio 19
2. Connect to your server
3. Right click on your "Databases" folder
4. Select "Import Data-tier Application"
5. Follow wizard to import and select "database.bacpac" from checkout folder

**Installation**:
1. Clone repo
2. Import database
3. Open "PlanerApp.sln"
4. When in Visual Studio right click on solution => Properties => Startup Project => Multiple startup projects
5. Change action to "Start" for "RESTServer" and "WPF_FrontEnd" projects and press "Apply"
6. Press "Start" on top of Visual Studio window
7. Profit, you good to go

**Usage**:
To use the app, you would need to register. When you first launch the application, you will see the login window, there will be a link for registration if you don't have an account. After registration, you would need to login to see the main window.
In main window you can select dates and add notes to them with plus sign button. The interface of main window is minimalistic and intuitive so you won't have problems. (I hope)

## Showcase of Design





### Showcase of login window
![image](https://github.com/Oleh-Halitskiy/PlanerApp/assets/73580619/fa98cb68-ed02-4290-804d-151dbdff69e5)







### Showcase of registration window
![image](https://github.com/Oleh-Halitskiy/PlanerApp/assets/73580619/efe2cf46-2c81-4492-ae40-76023ebac1f1)







### Showcase of main window
![image](https://github.com/Oleh-Halitskiy/PlanerApp/assets/73580619/4c001868-d08d-4c3b-a6ea-3f304e228a80)
