# Skip Sleep
A simple server-side (or client-side) mod that allows players to skip the night (by sleeping) when only a part of the online users are sleeping. Inspired by the Minecraft [Morpheus](https://www.curseforge.com/minecraft/mc-mods/morpheus) mod.

## Configuration
When running the server (or your game) for the first time after having installed this mod, a config file will be generated in ``<Install directory>\BepInEx\config`` named ``SkipSleep.cfg``. In this config file, you can change the ratio (or threshold) that is needed to skip the night. This ratio is the amount of players that must be sleeping divided by the total amount of online players in order to skip the night. For example, if your servers has 3 players online and you'd like to skip the night when only 1 player is sleeping, a ratio of ``0.33`` or lower would work since then ``(1/3) = 0.3333... > 0.33`` would be sleeping. 

**Make sure the ratio is always greater than 0, since having a ratio of 0 will cause the game to continuously skip days**.

## How to install
Extract the .dll to your ``plugins`` folder located in ``<Valheim server install directory>\BepInEx\``.

## Requirements
 - [BepInExPack for Valheim](https://valheim.thunderstore.io/package/denikson/BepInExPack_Valheim/) installed on the server and/or client

This mod works both as a server-side only mod or as a client-side mod if you are hosting a server from the client.

## Credits
https://github.com/Measurity/ModTemplateValheim - Mod template.
