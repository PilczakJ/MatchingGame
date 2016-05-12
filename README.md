# MatchingGame

## Description
Problem given to solve by Neuroscouting.  
Based on the given descriptions, I made a game which I call QuickDice. This game is one which is themed to be like a casino with red dice on a green felt table. It measures the accuracy and reaction time of the player by initially showing a target dice side to the player and then a series of six random "rolls" which expect the player to hit the space bar when they see the target image. The player may set the number of trials of the game from the main menu and the score for each trial will be reported once they are all completed.

## How to Play
Upon starting the game, there will be three buttons on the screen. Two of these buttons allow you to set the number of trials which you want to play and the third will start the game.

Once each trial starts, an image of the side of a dice will show up. The object is to get the highest score by pressing space as soon as you see the target image of the trial each time you see it. There may be trials where the image never appears and other trials where it appears more than once. Your best reaction time and number of misses or times you hit space on the wrong image will be tracked. Your Your statistics for each trial will be displayed together on the score screen along with a final score which is the your accuracy percentage (as a number e.g 100% = 100) is multiplied by 1/your best reaction time of the trial to determine a final score for the trial.

## Organization
The entirety of the game is put into two scripts: the ScoreKeeper and the Target.

The Scorekeeper class is the one that controls the entirety of the UI along with score tracking and the swapping between the title screen, the in-game screen, and the score screen.

The Target is what holds the functionallity of the game such as input, tracking the timing of the images appearing, and keeping the scorekeeper updated on the statistics of the player.
