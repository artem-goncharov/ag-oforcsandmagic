# ag-oforcsandmagic
"Of Orcs and Magic" testing

Menu scene:
- Start game button
- Input fields with information about game settings (Player health and attack; enemies health, attack, movement speed, spawn frequency)

Game scene:
- Player is located in the center. He is not moving, only turning towards cursor.
- If you press LMB the player starts to shoot continuously with the same time interval between shots.
- Enemies are moving towards player from both directions.
- Score is added for killing enemies, score value is displayed in game.
- After earning 5000 score the "Victory" text is shown, then we go back to main menu.
- If player's health reduces to zero, then "Epic Fail" text is shown and we go back to main menu.
- Player and enemies have healthbars.
- Enemies can overlap each other and player. In this case player must be able to kill them.
- If player dies, he disappears with scream.

Types of enemies:
- Bomb. Fast movement, low health, huge attack damage. During impact with player deals damage to player and dissappears.
- Infantry. Slow movement, huge health, medium attack damage. When arriving to player the last starts to receive damage continuously with the same time interval.
- Shooter. Medium speed, medium health, low attack damage. After arriving half-way towards the player, shooter stops and begins to shoot continuously with the same time interval between shots.
