# Design Documentation Conventions

## Shorthands:
### Elements
Fire (F), Earth (E), Water (W), Air (A), Neutral (N), Generic (#)

Energy Cost is denoted in \{curly brackets\}
Crafting Cost is denoted in \[square brackets\]
Energy Cards have no Energy Cost

## Card Template:

\<Name\>: \{\<Energy Cost\>\} \[\<Crafting Cost\>\] -\> \<Quantity Produced\> 
- List of Effects

e.g. \\
Steam Attack: {1FW} \[FW\] -> 3
- Deal 8 damage
- Draw a card
- Add {FW}

## Enemy Template:

\<Name\>: \{\<HP\>\} \[\<Drops>\]
- List of Moves (Pseudocode by turn)

e.g.\\
Wildfire Elemental: {60} [3xF, 3xA]
- For x = 0 to 2
  - Deal (1 + x * 0.3) * RANDOM([15, 25]) Damage
- Deal 100 Damage
- Die

## Encounter Template:

\<Name\>: \[\<Extra Drops (if any)>\] 
- \<Number\> Enemies

e.g.\\
Forest Fire: \[FA\]
- 2 Wildfire Elemental
- 2 Treefolk
