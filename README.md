# Noko Game - Technical Case Study

This repository contains the prototype developed for the Noko Game Developer Technical Case. The project demonstrates an Idle RPG gameplay loop, modular upgrade systems, and a skill-based combat mechanic.

## Project Overview

* **Development Time:** ~48 hours
* **Unity Version:** 6000.3.10f1
* **Platform:** Mobile


* **Target Weapon:** Mage Staff (Fireball, Freeze, Arcane Shield)



## Implemented Features

* **Idle Loop:** Autonomous enemy spawning, target detection, and combat interaction.


* **Upgrade System:** Fully functional economy integration for Damage, Max HP, Attack Speed, and Movement Speed.


* **Skill System:** Three distinct mage abilities with modular design.


* **Architecture:** Component-based system designed for modularity and maintainability.



## Technical Assessment & Refinement Roadmap

Due to the 48-hour development constraint, the focus was prioritized on MVP (Minimum Viable Product) functionality. The following areas have been identified for post-prototype refinement:

1. **Camera Jitter:** Minor jitter observed in follow logic. The system is scheduled for synchronization between `FixedUpdate` (physics) and camera tracking to ensure perfect fluidity.
2. **Fireball Tracking:** The projectile movement logic is functional but requires further optimization for real-time dynamic target tracking.
3. **Animation Polish:** Current animation state transitions are functional but require state-machine tuning for improved fluidity.
4. **VFX/SFX & Game Feel:** The architecture is fully prepared for additional VFX/SFX integration. Core systems are in place, but advanced "juice" (camera shake, polish tweens) was deferred to prioritize core system stability.
5. **Performance/Pooling:** While core pooling is implemented, extending the `ObjectPool` architecture to all project projectiles is planned for optimized memory management.

## Setup & Build Instructions

1. **Environment:** Open this project using **Unity 6000.3.10f1**.
2. **Scene:** Open the "CaseScene"





---

### Developer Note

This prototype was developed with an emphasis on code cleanliness, modularity, and system stability. By focusing on a robust core architecture, I have ensured that the identified areas for refinement can be easily integrated without refactoring the existing codebase.
