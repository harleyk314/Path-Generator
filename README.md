# Path-Generator
A walking simulator of a long, winding path that is procedurally generated, and is unique on each playthrough.

![Maze generator gameplay 2](https://github.com/harleyk314/Path-Generator/assets/58278456/237454ca-cf43-4c4f-893b-d9597f35c893)

### Main Controls
- Use the arrow keys to move.
- Pressing ‘Q’ alternates between walking speed and driving speed.
### Extra Controls
- Holding ‘Z’ shows a second camera perspective.
- Tap ‘O’ repeatedly to fly

### Development
- I generated the long, winding path and its walls using cloning and procedural generation.
- The algorithm for the path generation is somewhat complex, but here’s a simplification:
1. Draw a path within a box from the start to the finish by randomly choosing a direction to move in each time.
2. Whenever a collision is inevitable, traverse back to the last reasonable point, and mark any squares along that path as inaccessible. 

### Further Improvements
- The game could be made more interesting by using music and sound effects, multiple paths, enemies, and menus.
- Finally, implementing object collisions for all relevant interactions would be required for most games. I would use online tutorials and Unity’s debugging tools as necessary.

