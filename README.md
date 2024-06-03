# TimeCraft

## Introduction

This is a time management application that allows you to organize your tasks and meetings efficiently. It comes in two versions: a text-based console interface and a graphical WPF interface, and you can switch between them as needed.

## What Is Implemented

* User Registration and Login - includes basic validation and authorization.

* Interactive Calendar - available in the console version, it allows you to view your schedule in a calendar format.

* Task and Meeting Management - you can add, view, edit, and delete tasks and meetings for any given day.
* Details Display - you can view detailed information about each task or meeting.

* Pagination - implemented in the console version for easy navigation through tasks and meetings.

* Filtering and Sorting - available in the WPF version, it allows you to filter tasks and meetings by date and priority, and sort them by time as needed.

## Technologies

* C# - used for the core development of the application.

* .NET Core - used for the console version of the application.

* WPF (Windows Presentation Foundation) - used for the graphical version of the application.

* MaterialDesignThemes - used for styling the WPF application.

* Spectre.Console - used for creating the console interface.

## How To Run

1. Clone the repository: `https://github.com/Przemigiusz/TimeCraft.git`
2. Navigate into the project directory: `cd TimeCraft`
3. Build the solution: `dotnet build`
4. Run the launcher: `dotnet run --project ./Launcher/Launcher.csproj`
