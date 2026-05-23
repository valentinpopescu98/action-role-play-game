# action-role-play-game

An ARPG demo built in Unity (C#), covering every major feature representative of the genre. The goal was to implement a complete, playable slice of an action RPG — not a tutorial project, but a self-directed feature integration across combat, inventory, NPCs, quests, and level design.

**[► Watch the demo on YouTube](https://www.youtube.com/watch?v=fyq7AajjWpU)**

---

## Features

### Combat
- Real-time melee and ranged combat
- Fist as default weapon when nothing is equipped
- Weapon-specific attack and damage values
- Ranged weapons emit bullet tracer effects and fire effects on hit
- Player auto-orients toward enemy when shooting

### Inventory & Items
- Minecraft-style inventory — items must be in the inventory to be equipped or used
- Equipment slots for weapons (sword, gun, etc.) — unequipping restores fist
- Consumables — potions with either fixed HP restore (+300 HP) or full HP fill, depending on type
- Item drops from enemies on death

### Enemies
- 10 different zombie types, each visually distinct
- Healthbar appears above enemies when the player enters proximity
- Melee and ranged enemy behavior
- Enemy aggro at a configurable distance threshold

### NPCs & Quests
- Friendly NPCs that turn to face the player during conversation
- Quest system with multiple quest types:
  - **Dialog quests** — talk to an NPC for a gold reward
  - **Kill quests** — defeat a number of enemies for an item reward
  - **Gather quests** — deliver items from inventory for a weapon reward

### Day/Night Cycle
- Dynamic sun and moon on opposite sides — when one rises, the other sets
- Real-time shadow casting, visible on character and terrain
- Lava material on the volcano emits light, visibly illuminating the crater at night

### Level Design
- Central hub with roads leading outward to distinct zones
- Dead forest with zombie spawns — atmosphere matches the horror tone
- Pyramidal mountain and volcanic crater with emissive lava texture
- Terrain height variation — hills and a mountain, not flat
- Multiple terrain texture layers
- 20 different tree types for a more natural-looking forest

---

## Tech Stack

| Layer | Technology |
|---|---|
| Engine | Unity |
| Language | C# |
| Platform | PC |

---

## Running the project

Open in Unity (2020+), load the main scene from `Assets/Scenes`, and hit Play.
