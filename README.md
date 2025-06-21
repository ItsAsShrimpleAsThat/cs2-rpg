# cs2-rpg
This is a "bot" (ish) for CS2 that lets you play a lil rpg game in chat!!! This won't get you VAC banned because it's not touching the game's memory- it's just using some really strange workarounds to read/send chat messages.

Very much thanks to [nemmy](https://github.com/Pandaptable) for making [nembot](https://github.com/Pandaptable/nembot), which I "borrowed" a lot of code from.

## Setup
Very very good news!! There isn't much setup required.

#### Step 1 - making CS2 log its console to a file
* Right click on CS2 in your steam library and go to properties
* In the General tab, go to the launch options and add "-condebug" if it isn't there already
<br>&nbsp;
* This makes CS2 constantly output it's console to this file, which cs2-rpg uses to read chat.

#### Step 2 - make sure you have correct paths
* If you are on Windows and have CS installed to the default location, this *should* already be done (but double check to make sure)
<br>&nbsp;
* Go to the installation directory of CS2 by right clicking it in your steam library and clicking on Manage > Browse local files
* If it is ```C:\Program Files (x86)\Steam\steamapps\common\Counter-Strike Global Offensive``` then you're good to go. Otherwise, you'll have to go to Constants.cs and change the csLogPath variable and the csCFGPath variable. Remind me later on to make it just read from a file.

#### Step 3 - Starting the bot in the **main menu**
* One important thing about this bot is that it requires sv_cheats to be on **when you start it**. The bot makes heavy use of exec_async, which for some reason its cheat protected, so you need to start the bot in the main menu, where cheats are allowed.
<br>&nbsp;
* To create the CFG files that the game needs, just run the cs2-rpg program once, and it'll automatically create them. Then, when you've successfully created the cfg files, you can stop the program and go to the cs2 console and run "exec cs2rpg/startbot". If you see a giant spam of execing cs2rpg stuff, then you know its working.

You can then just run the program in a game wowie.
