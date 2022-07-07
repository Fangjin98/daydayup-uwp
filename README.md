<p align="center">
  <a href="#">
    
  </a>
  <img width="128" src="/assets/logo/300x300.png">
</p>

<h1 align="center">
  DayDayUp
</h1>

<p align="center">
  An Light-weight Todo Manager with Time Estimation Tracking.
</p>

<p align="center">
  <a style="text-decoration:none" href="https://svgshare.com/i/ZhY.svg" target="_blank">
    <img src="https://svgshare.com/i/ZhY.svg"/>
  </a>

  <img src="/assets/screenshots/1.png">
</p>

## Introduction

Time estimation is a key need for todo management. We help you to better estimate the completion time of todos via evidence-based scheduling ([EBS](https://fogbugz.com/Evidence-Based-Scheduling/#:~:text=Evidence%20Based%20Scheduling%20or%20EBS%20is%20a%20statistical,the%20probability%20that%20your%20project%20will%20be%20completed)).

### What is the EBS?

> Evidence Based Scheduling or EBS is a statistical algorithm that produces ship date probability distributions. It gathers evidence, mostly from historical timesheet data and provides accurate schedules. EBS produces a probability distribution curve, so that you know for any given date, the probability that your project will be completed.

### How the EBS works?

In DayDayUp, each todo has three attributes related to EBS:

1. _real duration_ : record by DayDayUp, after users finish a todo.
2. _estimated duration_: **set by users** when (after) a todo is created. It means that, this todo is supposed to take _estimated duration_ mins to finish.
3. _predicted duration_: calculate by DayDayUp as the results of the EBS. It is a set of values, representing the bias of _estimated duration_ under different probabilities.

After one todo is created, users can set the _estimated duration_.

For each unfinished todo, DayDayUp adopts Monte Carlo Method to calculate _predicted durations_, based on the bias of _real durations_ and _estimated durations_ of finished todos.

## Building from source

### 1. Prerequisties

- Git
- [Visual Studio 2022](https://visualstudio.microsoft.com/zh-hans/vs/), community edition works.

### 2. Clone the repository

 `git clone https://github.com/Fangjin98/DayDayUp.git`

### 3. Build the project

Open `src/DayDayUp.sln` and hit F5 to compile and run.

## Features

**Complete**:

- _Create Todos_ - Can set estimated duration of todos.
- _Start & Pause Todos_ - Set the status of todos to record the duration.
- _Per-todo Informations_
  - Status
  - Progress
  - Estimated duration
  - Prediction durations
  - Current duration

**In progress**:

- _CLI Support_ - Add todos from the terminal.
- _Multi-language Support_
  - Chinese
- _Data Export_

**Not started**:

- _Multi-device Synchronization_
- _Category_ - Todos can be assigned to different categories.
- _Dashboard_ - Statistics summary.

## Acknowledgments

- [Windows Community Toolkit](https://github.com/CommunityToolkit/MVVM-Samples)
