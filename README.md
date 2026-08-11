# Tower Defence

A tower-defense game built in **Unity** with C#. Enemies advance along a waypoint path and the player places and upgrades towers to stop them across escalating waves.

## Gameplay

- **Towers** — Archer, Cannon, Mage, Chain-Lightning, and a Barrier/path-blocker, each with its own targeting and projectile behaviour.
- **Heroes** — deployable hero units (Knight, Dragon, Princess) with distinct abilities.
- **Enemies** — path-following enemies driven by a wave manager that scales difficulty over time.

## Project layout

```
Scripts/
├── Enemies/       # Enemy stats and waypoint movement
├── Heroes/        # Hero base class + individual heroes
├── Managers/      # GameManager, WaveManager
├── Pathfinding/   # Waypoint nodes
├── Projectiles/   # Cannonball, mage projectile, base projectile
├── Towers/        # Tower types + placement logic
└── UI/            # UIManager
```

> This repository contains the C# gameplay scripts. Open the folder as part of a Unity project to build and play.

## Tech

- Unity (C#)
