# 2DSteamWorld
This is my 2D unity game about exploring and managing item in the dungeon in the steampunk world.

"The project is not yet complete"

The core functions for exploring part have already been vaguely implemented still need plenty of adjusting and changing.

[GameConcept]
- The game is 2D side-scroll game about exploring dungeon based on steampunk style. player always move foward choosing either moving up, down or forward.
- The game will pressuring player to take decision in the limited time. Taking too long to think will punish player by moving player forward. when player reach the end of the level (use all the available step) player will encounter boss. completing the level grant player a point to upgrade charater for the next level.
- Upgrading character will make them stronger but enemies in the higher level floor are also stronger.
- Player can choose the challenge before starting the level, choosing harder challenger will grant player a better reward at the end of the level.

[Already implemented function]
- Moving player.
- Collecting items.
- Working buff/debuff for player
- Weapons/Armor
- Auto-generate the map level.
- Fighting scene.
- Time-based action(like the old FF series where monster/player's weapon have different cooldown on action) Ps. only auto attack is available

This version is playable to the part where player can move in the stage, fight enemy and collect/use item.

[Currently workign on]
- Character design/animation
- Background illustration
- Enemy design/animation

[How to play]
- The game only allows player to move foward using arrows keys(right, up or down).
- Selecting up/down will move player forward too.
- There is only Box and one enemy type.
- Collecting box will grant player a random item.
- Player's item box can contain only 3 item. and can be used by pressing number 1, 2 and 3 respectively. colleting new item will replace the oldest item in the item box.
- Right now, Player can use item only in exploring phase.
- Player can be effected by 3 buff and 1 debuff. The buffs stay only 5 turns. getting new buff will replace the oldest buff. 
- Random Item can be character Buff/Debuff and Usable Item
- When entering battle scene, there's a invisible time counter for player and enemy before taking action(Cooldown). Each weapon/enemies have difference cooldown(haven't implement yet)
- At the moment, player can only use normal attack(press A) when system announce Plyaer's turn. (While it's player's turn, enemy can still attack player if their action is off CDs.)

Concept arts can be found here.
https://drive.google.com/drive/folders/1LP6KiX1K0ySPnITzOu6dUe_e68-I64ok?usp=sharing
