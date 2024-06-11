# MazeGenerator

This project is made to get better knowledge of different algorithms for maze generation and solving different mazes.

## Recursive Backtracking

The recursive backtracking algorithm is a popular method for generating mazes. Here's a brief summary:

1. **Starting Point**: Choose a random starting cell in the grid.
2. **Mark as Visited**: Mark this cell as part of the maze.
3. **Random Neighbors**: From this cell, randomly select one of its unvisited neighboring cells.
4. **Move and Remove Wall**: Move to the chosen neighbor, remove the wall between the two cells, and mark the new cell as visited.
5. **Repeat**: Continue this process from the new cell, always moving to a random unvisited neighbor, removing the wall, and marking it as visited.
6. **Backtrack**: If a cell has no unvisited neighbors, backtrack to the previous cell and repeat the process from there.
7. **Finish**: The algorithm ends when there are no more cells to visit, resulting in a fully generated maze with a single connected path from any cell to any other cell.

This method creates a perfect maze, meaning there is exactly one path between any two points, with no loops or isolated sections.

### Advantages

- **Simplicity**: The algorithm is straightforward and easy to implement.
- **Perfect Maze**: It generates a perfect maze, meaning there is exactly one path between any two points, with no loops or isolated sections.
- **Visually Appealing Mazes**: The mazes created by this algorithm have a good balance of open areas and tight passages, making them visually interesting and fun to navigate.
- **Guaranteed Solution**: Since every cell is visited exactly once and connected, there's always a path from the start to the end of the maze.
- **Control Over Complexity**: The complexity of the maze can be adjusted by changing the randomness of neighbor selection, leading to either simpler or more complex mazes.

### Disadvantages

- **Memory Usage**: The recursive implementation can use a significant amount of memory for the call stack, especially for large mazes, which may lead to stack overflow issues.
- **Backtracking Overhead**: While backtracking, the algorithm might revisit many cells, which can be inefficient for very large mazes.
- **Randomness Dependency**: The quality and complexity of the maze depend heavily on the randomness of neighbor selection. Poor random number generation can lead to predictable or less interesting mazes.
- **No Natural Bias**: The algorithm does not naturally create mazes with specific features like more open areas or specific patterns without additional modification.

### Time Complexity

- **Time Complexity**: The time complexity of the recursive backtracking algorithm is \(O(N)\), where \(N\) is the total number of cells in the maze. This is because each cell is visited exactly once during the maze generation process.
- **Space Complexity**: The space complexity is \(O(N)\) due to the call stack used in the recursive implementation. This can be a limiting factor for very large mazes, as it may lead to stack overflow issues.

### Summary

The recursive backtracking algorithm is a simple and effective method for generating perfect mazes. It offers control over maze complexity and guarantees a solution path from start to finish. However, it can be memory-intensive due to its recursive nature and the randomness dependency can affect the quality of the generated mazes. Despite these drawbacks, it remains a popular choice for maze generation due to its ease of implementation and the visually appealing mazes it produces.


## References
https://aryanab.medium.com/maze-generation-recursive-backtracking-5981bc5cc766
https://www.shiksha.com/online-courses/articles/introduction-to-backtracking/#:~:text=Advantages%20of%20Backtracking,-Backtracking%20has%20a&text=The%20step%2Dby%2Dstep%20representation,lines%20of%20recursive%20function%20code.
