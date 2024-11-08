# Unity-Custom-Analytics-System

A custom, scalable analytics solution for Unity apps, written in C#. This system is designed to efficiently track user interactions and performance metrics using a modular, pattern-driven architecture. Inspired by Firebase Analytics, this project allows for flexible customization and control over analytics data.

## Features

- **Event Tracking**: Log custom events with dynamic data.
- **Screen Tracking**: Track user navigation between screens.
- **User Engagement Metrics**: Track user sessions and engagement duration.
- **Offline Data Storage**: Save data locally and sync when online.
- **Modular Architecture**: Extendable using patterns like Singleton, Factory, and Strategy.

## Design Patterns Used

- **Singleton**: Ensures a single instance of the analytics manager.
- **Observer**: Decouples event generation from event handling.
- **Factory**: Creates different types of analytics events.
- **Command**: Encapsulates tracking actions for batching.
- **Strategy**: Chooses storage methods based on connectivity.
- **Decorator**: Adds optional data processing like encryption.

## Installation

1. Clone the repository to your Unity project:
   ```bash
   git clone https://github.com/your-username/Unity-Custom-Analytics-System.git
