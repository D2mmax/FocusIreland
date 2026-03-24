# Dialogue System — Team Guide

## Overview

When the player walks up to an NPC and presses E, the game cuts to a 2D dialogue screen showing the player and NPC as sprites against a background. The player reads the NPC's line and picks from up to 3 choices. Each choice can affect the player's **EHC stats** (Energy, Hope, Connection) and lead to a different response node. After the first full conversation, repeat visits show a short one-liner instead of the full tree.

---

## The EHC System

There are 3 stats, each ranging from **0 to 100**, starting at **50**:

| Stat | What it represents |
|---|---|
| Energy | How much capacity the player has to cope day to day |
| Hope | The player's belief that things can get better |
| Connection | How supported and connected the player feels |

Each stat has **5 sprites** representing its current level:
- Index 0 = 81-100 (full)
- Index 1 = 61-80
- Index 2 = 41-60
- Index 3 = 21-40
- Index 4 = 0-20 (empty)

**When setting EHC deltas on choices, use meaningful values:**
- Small effect:  +/- 10
- Medium effect: +/- 20
- Strong effect: +/- 30

Small values like +/-5 may not visibly change the sprite at all.

---

## Creating a New NPC Conversation

You need to create **2 assets** per NPC, then set up the **NPC GameObject**.

### Step 1 — Create the DialogueSceneConfig

In the Project window:
Right-click > Create > FocusIreland > Dialogue Scene Config

**Naming convention:** DSC_[SceneName]_[CharacterName]
Example: DSC_School_MsMurphy

Save it in: Assets/ScriptableObjects/Configs/

Fill in the Inspector:
- **NPC Name** — the name shown above dialogue text
- **NPC Sprite** — the NPC's portrait sprite
- **Player Sprite** — the player's portrait sprite
- **Background Sprite** — the scene background image
- **Repeat Line** — short line shown on all visits after the first

---

### Step 2 — Create the DialogueTree

In the Project window:
Right-click > Create > FocusIreland > Dialogue Tree

**Naming convention:** DT_[SceneName]_[CharacterName]
Example: DT_School_MsMurphy

Save it in: Assets/ScriptableObjects/Trees/

Set **Start Node ID** to: start

Then add nodes using the + button on the Nodes list.

---

## How Nodes Work

Each node is one beat of conversation.

| Field | What to put |
|---|---|
| Node ID | A unique text ID e.g. start, response_a, end |
| Speaker | NPC or Player |
| Dialogue Text | The line shown on screen |
| Choices | Up to 3 options the player can pick |

Each Choice has:

| Field | What to put |
|---|---|
| Choice Text | The button text the player sees |
| Next Node ID | ID of the node to go to. Leave empty to end the conversation. |
| EHC Effect | How much Energy, Hope, Connection changes (positive or negative) |

---

## Example Tree Structure

Node: start
  NPC: "Hey, you alright? You seem quiet today."
  Choice 1: "I'm fine"          > goes to: tired   EHC: Connection +10
  Choice 2: "Things are hard"   > goes to: tough   EHC: Hope +20, Connection +25
  Choice 3: "Leave me alone"    > goes to: alone   EHC: Connection -20, Hope -10

Node: tired
  NPC: "Okay. I'm here if you need me."
  No choices — player clicks close button to exit

Node: tough
  NPC: "That sounds rough. You're not alone in this."
  No choices — player clicks close button to exit

Node: alone
  NPC: "Oh... sorry. I didn't mean to bother you."
  No choices — player clicks close button to exit

IMPORTANT: Every Next Node ID must exactly match a real Node ID in the tree
or the conversation will silently end early. Double check spelling.

---

## Step 3 — Set Up the NPC GameObject

1. In the Hierarchy, right-click > Create Empty, rename to the character's name
2. Add a Box Collider — tick Is Trigger, set a reasonable size
3. Add the NPCInteraction script
4. In the Inspector, drag in:
   - Scene Config  > your DSC_ asset
   - Dialogue Tree > your DT_ asset
5. Optionally add a child GameObject as an Interact Prompt and drag it into the slot
6. Save the NPC as a prefab in Assets/Prefabs/NPCs/

---

## Testing

Open the DialogueTestScene. Every NPC in the game should have a copy here.
Walk the player up to the NPC, press E, and verify:
- Correct background and sprites appear
- All choices lead to the correct response nodes
- EHC sprites update correctly after choices
- Repeat visit shows the correct one-liner

---

## Quick Reference

Asset naming:
  DT_[Scene]_[Character]   e.g. DT_Street_Stranger
  DSC_[Scene]_[Character]  e.g. DSC_Street_Stranger

Folder structure:
  Assets/
    ScriptableObjects/
      Trees/    -- all DT_ assets
      Configs/  -- all DSC_ assets
    Prefabs/
      NPCs/     -- all NPC prefabs
