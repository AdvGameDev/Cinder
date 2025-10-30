# Project Pitch: Cinder

(I promise this is still essentially a potion game)

**Tagline:** From the embers, forge anew.

## Game Summary
Cinder is a narrative-driven roguelite deckbuilder that merges deep strategic deckbuilding with a dynamic crafting system. The player embodies a once-powerful cosmic creator, now reduced to a final, fading spark of consciousness imprisoned within a mysterious matchbox—an artifact known only as the "last ember." To escape its prison and reignite the fires of creation, the Patron must mentally possess the unwitting mortals who find its artifact, compelling these "vessels" on a desperate pilgrimage to the heart of a silent, stagnant world.

The core gameplay loop is a cycle of innovation and consumption. Players will guide their vessel through tactical, turn-based combat, but victory yields not new powers, but the conceptual components of reality. Between battles, players will use these components to inscribe powerful, consumable talismans and enhance the artifact itself. Every power is fleeting, every tool is temporary, forcing the player to constantly create, adapt, and ultimately decide the fate of a universe poised between a painful existence and a perfect, painless peace.

## Story & Lore
In the beginning, there was the **Primordial Flame**, a being of pure innovation whose nature was to introduce new concepts to the universe—change, choice, and life itself. Its first creations, the **Firstborn**, were the first to experience consciousness, and thus, the first to experience suffering. Horrified by the endless potential for pain their creator would unleash, they came to believe that individuality was a curse.

Driven by a tragic, compassionate philosophy, they sought to enact **The Grand Concordance**—a ritual to merge all consciousness into a single, unified, and painless whole. When the Primordial Flame refused to undo its creation, its children betrayed it, seeking to seize its creative power to enforce their great mercy. To protect the spark of innovation, the Patron shed its cosmic form, condensing its very essence into the "last ember."

This act saved the spark but doomed the universe. With the source of novelty removed, history has stalled. The world has fallen into a gray, timeless stasis, slowly being "harmonized" by the Firstborn—now calling themselves **The Chorus of Concordance**. The player's quest is to guide a vessel to **The Silent Forge**, the Patron's former throne, and reignite the flame. This is not just an act of freedom, but a profound choice: to restore the beautiful, terrible reality of existence, or to allow the Chorus to finally bring peace to a suffering world.

## High-level Game Design

- **Type:** Narrative-driven Roguelike Deckbuilder with a heavy emphasis on crafting and resource management. It combines the tactical depth of games like *Slay the Spire* with the rich character-driven storytelling and complex antagonist of a title like **Hades**.
- **Target:** Fans of strategic card games, roguelikes, and crafting systems. Players who enjoy deep, philosophical narratives and making choices with significant consequences will be the primary audience.
- **Tone:** The tone is one of tragic grandeur and desperate hope. It is a somber, philosophical journey through a dying world. The antagonists are not evil, but compassionate extremists, making encounters feel more like tragic confrontations than righteous battles.
- **Theme:** The central theme of the game is **impermanence**. The game explores whether life, with all of its inherent potential for both joy and suffering, is preferable to a perfect, painless, but ultimately empty state of being. The player's final action is not just a gameplay victory but the ultimate answer to this question.

## Low-level Game Design

* **Core Mechanics/Gameplay:**
    * **The Artifact & Sparks:** Combat is turn-based. Instead of energy, the player uses the **"last ember"** artifact, a matchbox that provides a set number of **"sparks"** (magical matches) each turn.
    * **Inscribing & Burning Talismans:** Players do not find cards. They use ingredients to **inscribe talismans** based on known glyphs. Playing a card is the act of using a "spark" to **burn a talisman**, which consumes it permanently. The deck must be constantly replenished through crafting.
    * **Reverse-Engineering Glyphs:** Players learn new crafting components by finding unique conceptual items from enemies (e.g., a "Shard of Perfect Frost"). At rest sites, the Patron can **reverse-engineer** these items to unlock their core **"Conceptual Glyph"** (e.g., "Frost"), which can then be used in crafting for the remainder of the run.
    * **Infused Embers:** "Potions" are replaced by **"Infused Embers."** Players use special reagents to treat a match from their artifact. Using this special match to burn a talisman adds a powerful, one-time bonus effect (e.g., "Heal for 20 HP" or "Gain 10 Block") on top of the talisman's own effect.

* **Camera Perspective/UI:** The game is presented in 2D. The UI will be minimalist and thematic, styled after an inventor's journal filled with schematics, anatomical drawings, and frantic notes. The "last ember" matchbox will be a constant, visible element on the screen, showing the remaining sparks for the turn.

* **Levels:** The game takes place in **The Silent Forge**. This procedurally generated world is presented as a map with branching paths. The environments are eerily beautiful and melancholic—grand workshops now silent, frozen gardens, and libraries of unwritten books. The enemies are the **"Harmonized,"** agents of the Chorus who move with a serene, unified purpose.

* **Art/Music:** The art style is defined by contrast: the world is rendered in a desaturated, almost monochrome palette, reflecting the universal stasis. The only vibrant colors come from the Patron's power—the brilliant, fiery flash of a burning talisman. The music is sparse and ambient, swelling during combat with beautiful and tragic choral arrangements that represent the voice of the Harmonized.

* **Challenges:** The primary challenge is balancing the intricate economy of ingredients, Glyphs, and talisman power. Another significant challenge is conveying the deep, philosophical narrative through gameplay and world design, ensuring the player understands the weight of their final choice.

## Mechanical Analysis

- Puzzles: 
  1. how do I make the best move given what's in my hand?
  2. how do I make best set of spells given the amount of ingredients I have?
  3. how do I pick my future encounters to optimize my current build?
  4. how greedy can I get by committing as little ingredients as possible while still being able to survive to the next heal / craft stop.

## Advantages
