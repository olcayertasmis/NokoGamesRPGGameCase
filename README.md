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



## Post-Prototype Refinements

Following the initial 48-hour development period, the following targeted refinements were implemented to ensure the project meets the desired quality standards:

* **Damage Scaling:** Fixed an issue where `Fireball` skill damage did not scale with `UpgradeManager` stats.



## Technical Assessment & Roadmap

Due to the 48-hour development constraint, the focus was prioritized on MVP functionality. The following areas are identified for further refinement:


1. **Fireball Tracking:** Projectile movement requires further optimization for real-time dynamic target tracking.


2. **Animation Polish:** State-machine tuning is required for improved transition fluidity.


3. **VFX/SFX & Game Feel:** The architecture is fully prepared for additional VFX/SFX integration.


4. **Performance/Pooling:** Extending the `ObjectPool` architecture to all projectiles is planned for optimized memory management.



## Setup & Build Instructions

1. **Environment:** Open this project using **Unity 6000.3.10f1**.


2. **Scene:** Open the "CaseScene" from the `_CaseAssets` folder.


3. **Download Build:** You can download the latest Android build (APK) directly from this link: [Download Project Build](https://www.google.com/search?q=https://drive.google.com/drive/folders/15twKmVNqwPVFcsKd4zLMWq2GyLPefq3i%3Fusp%3Dsharing)


---

### Developer Note

This prototype was developed with an emphasis on code cleanliness, modularity, and system stability. By focusing on a robust core architecture, I have ensured that the identified areas for refinement can be easily integrated without refactoring the existing codebase.
