🎤 EurovisionTotalizer

EurovisionTotalizer is a web application built with ASP.NET Core MVC (.NET 8), designed to make home Eurovision prediction tournaments more fun and organized.
The app helps track player predictions, calculate points, and display results automatically.

🏆 About the Game

Every year, our group of friends organizes a Eurovision prediction contest — guessing which countries will qualify for the final and predicting the final rankings.

Game Rules

🥈 Semifinals

Each participant predicts which countries will NOT qualify for the final.

For every correctly guessed non-qualifying country, the player earns 1 point.

Predictions are made separately for both semifinals.

🥇 Grand Final

Each participant predicts:

The exact Top 10 ranking

The 3 countries that will finish last

Scoring:

Correct exact placement → 2 points

Off by one place → 1 point

Correctly guessing a country in the bottom three (any order) → 1 point

🧮 Winner

The winner is the player with the highest total score after summing up all semifinal and final points.

💻 Features

Create and manage player profiles

Input predictions for both semifinals and the final

Automatic score calculation based on official results

Real-time leaderboard with total scores

Admin panel to update results and manage tournaments

🛠️ Tech Stack

Framework: ASP.NET Core MVC (.NET 8)

Language: C#

IDE: Visual Studio

Database: SQL Server (or SQLite for testing)

Front-end: Razor Pages, Bootstrap 5, and JavaScript

🚀 Getting Started

Clone the repository:

git clone https://github.com/yourusername/EurovisionTotalizer.git


Open the project in Visual Studio

Update the appsettings.json connection string if needed

Run the database migrations:

dotnet ef database update


Start the project with Ctrl + F5

🧩 Future Improvements

Add OAuth (Google/Microsoft) login for participants

Introduce automatic import of official Eurovision results

Add a public leaderboard page with charts

Support multiple tournament years
