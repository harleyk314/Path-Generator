using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MazeMaker : MonoBehaviour
{

    //Easy solution to unsuccessful paths:
    //Keep track of all path that is traversed.
    //If it is impossible to move, mark that square as a bad path and trace back to the last point where you can make a legal move
    //Keep doing this whenever there is an issue.
    //(Use a different colour brick for drawing those bad paths)

    public GameObject wall;
    public GameObject horWall;
    public GameObject verWall;
    public GameObject adventureLine; //aka, the solution

    public GameObject deadEndRoom; //none of these 4 are used.
    public GameObject straightAwayRoom;
    public GameObject leftDoorRoom;
    public GameObject rightDoorRoom;

    public GameObject bigWall;
    public GameObject patchWall1;
    public GameObject patchWall2;
    public GameObject pathBlock;


    public Vector3 origin;
    public Vector3 offsetMarginX;
    public Vector3 offsetMarginY;
    public Vector3 offsetMarginZ;

    public int heightMultiplier = 0; //0 prevents height dilation

    //must be odd numbers
    public static int maze_width = 37;
    public static int maze_length = 37;

    public bool path_is_ok;

    public int currentHeight = 0;

    public int[,] path = new int[maze_width * maze_length,3]; //note that every path location has an x and y, hence the need to specify both.
    public int pathIndex = 0;


    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("error 1");
        path_is_ok = true;

        offsetMarginX = new Vector3(200, 0, 0);
        offsetMarginY = new Vector3(0, 40, 0);
        offsetMarginZ = new Vector3(0, 0, 200);
        origin = new Vector3(0, 0, 0);

        int offsetX = 0;
        int offsetY = 0;
        int offsetZ = 0;

        //Specifies the size of the maze. All dimensions must be odd, in order for the walls to work correctly.
        int[,] maze = new int[maze_width, maze_length];

        //Stores the expected height of each block, if applicable
        int[,] heights = new int[maze_width, maze_length];

        //Step 1: Iterate through the maze and add a block for any double-even square.

        for (int l = 0; l < maze_width; l++)
        {
            for (int m = 0; m < maze_length; m++)
            {
                if (l % 2 == 0 && m % 2 == 0)
                {
                    if (l == 0 || l == maze_width - 1 || m == 0 || m == maze_length - 1)
                    { 
                        maze[l, m] = -2; //This is just for clarity of the outside of the maze
                    } else 
                    {
                        maze[l, m] = -1;
                    }
                }

            }

        }

        //Step 2: Define the entrance and exit
        //For now, we will set them to the end corners, for ease of use.

        //The start block will have the value 1
        //The end block will have the value 2

        maze[0, 1] = 1;
        maze[1, 1] = 1;

        //path stuff

        path[0, 0] = 0;
        path[0, 1] = 1;
        path[1, 0] = 1;
        path[1, 1] = 1;

        pathIndex = 2;


        maze[maze_width - 1, maze_length - 2] = 2;
        maze[maze_width - 2, maze_length - 2] = 2;

        //Step 3: Place side walls everywhere except where and entrance or exit block is clearly marked

        for (int i = 0; i < maze_width; i++)
        {
            for (int j = 0; j < maze_length; j++)
            {
                if (i == 0 || i == maze_width - 1 || j == 0 || j == maze_length - 1)
                {
                    if (maze[i,j] == 0) {

                        //if square is white (empty)
                        maze[i, j] = -2;
                    }
                    
                }

            }

        }

        Debug.Log("error 2");

        //Step 4: Draw a path from the start to finish. If it doesn't work, reset and try again.

        int[] possible_directions = { 0, 0, 0, 0 }; //For down, up, left, right respectively

        int[,] maze_copy = maze.Clone() as int[,];

        //Go to the start
        //should be 1, 1
        int x = 1;
        int y = 1;

        //There are a number of possible directions to travel in. We will need to create an maze of all directions, and then randomly choose one to proceed with.

        //string[] path_options = { "u", "d", "l", "r" }; //I don't think we need this.
        int no_of_options;

        while (true)
        {
            //Debug.Log("Strange error, " + maze_copy[1,1]);
            no_of_options = 0;

            //Before anything, let's check we can't get to the exit immediately.
            if (x >= 0 && x < maze_width && (y - 2) > 0 && (y - 2) < maze_length)
            {
                if (maze_copy[x, y - 2] == 2)
                {
                    Debug.Log("error 3");
                    //Solution is to go down
                    y -= 1;
                    maze_copy[x, y] = 3;
                    y -= 1;
                    maze_copy[x, y] = 3;
                    Debug.Log("error 4");

                    path[pathIndex, 0] = x;
                    path[pathIndex, 1] = y;
                    pathIndex++;

                    break;
                }
            }
            if (x >= 0 && x < maze_width && (y + 2) > 0 && (y + 2) < maze_length)
            {
                if (maze_copy[x, y + 2] == 2)
                {
                    Debug.Log("error 5");
                    //Solution is to go up
                    y += 1;
                    maze_copy[x, y] = 3;
                    y += 1;
                    maze_copy[x, y] = 3;
                    Debug.Log("error 6");

                    path[pathIndex, 0] = x;
                    path[pathIndex, 1] = y;
                    pathIndex++;

                    break;
                }
            }
            if ((x - 2) >= 0 && (x - 2) < maze_width && y > 0 && y < maze_length)
            {
                if (maze_copy[x - 2, y] == 2)
                {
                    Debug.Log("error 7");
                    //Solution is to go left
                    x -= 1;
                    maze_copy[x, y] = 3;
                    x -= 1;
                    maze_copy[x, y] = 3;
                    Debug.Log("error 8");

                    path[pathIndex, 0] = x;
                    path[pathIndex, 1] = y;
                    pathIndex++;

                    break;
                }
            }
            if ((x + 2) >= 0 && (x + 2) < maze_width && y > 0 && y < maze_length)
            {
                if (maze_copy[x + 2, y] == 2)
                {
                    Debug.Log("error 9");
                    //Solution is to go right
                    x += 1;
                    maze_copy[x, y] = 3;
                    x += 1;
                    maze_copy[x, y] = 3;
                    Debug.Log("error 10");

                    path[pathIndex, 0] = x;
                    path[pathIndex, 1] = y;
                    pathIndex++;

                    break;
                }
            }
            Debug.Log("error 11");
            //Now we can generate paths   

            //Minor thing to improve
            possible_directions[0] = 0;
            possible_directions[1] = 0;
            possible_directions[2] = 0;
            possible_directions[3] = 0;

            if (x >= 0 && x < maze_width && (y - 2) > 0 && (y - 2) < maze_length)
            {
                if (maze_copy[x, y - 1] == 0 && maze_copy[x, y - 2] == 0)
                {
                    //One option is to go down. 
                    possible_directions[0] = 1;
                    no_of_options += 1;
                }
            }
            if (x >= 0 && x < maze_width && (y + 2) > 0 && (y + 2) < maze_length)
            {
                if (maze_copy[x, y + 1] == 0 && maze_copy[x, y + 2] == 0)
                {
                    //One option is to go up
                    possible_directions[1] = 1;
                    no_of_options += 1;
                }
            }
            if ((x - 2) >= 0 && (x - 2) < maze_width && y > 0 && y < maze_length)
            {
                if (maze_copy[x - 1, y] == 0 && maze_copy[x - 2, y] == 0)
                {
                    //One option is to go left
                    possible_directions[2] = 1;
                    no_of_options += 1;
                }
            }
            if ((x + 2) >= 0 && (x + 2) < maze_width && y > 0 && y < maze_length)
            {
                if (maze_copy[x + 1, y] == 0 && maze_copy[x + 2, y] == 0)
                {
                    //One option is to go right
                    possible_directions[3] = 1;
                    no_of_options += 1;
                }
            }

            

            //Choose a direction randomly

            //If there are no available directions, do not continue.

            if (no_of_options == 0)
            {
                //go back one block and place a solid block instead

                maze_copy[x, y] = -3;

                path[pathIndex,0] = -1;
                path[pathIndex, 0] = 0;

                pathIndex -= 1;

                int newX = path[pathIndex, 0];
                int newY = path[pathIndex, 1];

                maze_copy[(x + newX)/2, (y + newY)/2] = -4; //removing the inbetween squares

                x = path[pathIndex, 0];
                y = path[pathIndex, 1];

                



                path_is_ok = false;
                Debug.Log("Strange path " + x + ", " + y);
                //Time.timeScale = 0;
                //spawn a capsule at (0,0,0)

                //break;
            }
            else
            {
                //Debug.Log("No. of options is " + no_of_options);

                //TODO: randomly choose a direction to travel in
                //See line 92 for info on how the directions are chosen.
                int rInt = Random.Range(0, no_of_options);

                //Iterate through the possible_directions maze rInt times (whenever you reach a positive value)
                //THIS COULD POTENTIALLY CAUSE BUGS

                int tester = -1;

                for (int index = 0; index <= rInt; index++)
                {
                    while (true)
                    {
                        tester += 1;

                        if (possible_directions[tester] == 1)
                        {
                            break;
                        }
                        
                        if (tester > 3)
                        {
                            //throw new ArgumentException("Value is too high", nameof(index), ex);
                            //spawn a sphere at (0,0,0)
                            Debug.Log("error 12");
                        }
                    }
                }

                //If all is working, boundary checking should NOT be required.
                //But let's assume it's broken and throw and exception

                //if (x <0 || x >= maze_width || y < 0 || y >= maze_length)
                if (true)
                {
                    Debug.Log("error 13 " + x + ", " + y + ": " + possible_directions[0] + possible_directions[1] + possible_directions[2] + possible_directions[3] + ", Tester: " + tester + ", rInt: " + rInt);
                }

                if (tester == 0)
                {
                    //Create a path downward
                    y -= 1;
                    maze_copy[x, y] = 3;

                    currentHeight -= 1;
                    heights[x, y] = currentHeight;

                    y -= 1;
                    maze_copy[x, y] = 3;

                    currentHeight -= 1;
                    heights[x, y] = currentHeight;

                    path[pathIndex, 0] = x;
                    path[pathIndex, 1] = y;
                    pathIndex++;
                }
                else if (tester == 1)
                {
                    //Create a path upward
                    y += 1;
                    maze_copy[x, y] = 3;

                    currentHeight -= 1;
                    heights[x, y] = currentHeight;

                    y += 1;
                    maze_copy[x, y] = 3;

                    currentHeight -= 1;
                    heights[x, y] = currentHeight;

                    path[pathIndex, 0] = x;
                    path[pathIndex, 1] = y;
                    pathIndex++;
                }
                else if (tester == 2)
                {
                    //Create a path leftward
                    x -= 1;
                    maze_copy[x, y] = 3;

                    currentHeight -= 1;
                    heights[x, y] = currentHeight;

                    x -= 1;
                    maze_copy[x, y] = 3;

                    currentHeight -= 1;
                    heights[x, y] = currentHeight;

                    path[pathIndex, 0] = x;
                    path[pathIndex, 1] = y;
                    pathIndex++;
                }
                else if (tester == 3)
                {
                    //Create a path rightward
                    x += 1;
                    maze_copy[x, y] = 3;

                    currentHeight -= 1;
                    heights[x, y] = currentHeight;

                    x += 1;
                    maze_copy[x, y] = 3;

                    currentHeight -= 1;
                    heights[x, y] = currentHeight;

                    path[pathIndex, 0] = x;
                    path[pathIndex, 1] = y;
                    pathIndex++;
                }
                Debug.Log("error 14");
            }
        }

        Debug.Log("error 15");
        //Step 10: Spawn the maze

        if (true) //checking a path was found successfully //if path_is_ok
        {
            for (int i = 0; i < maze_width; i++)
            {
                for (int j = 0; j < maze_length; j++)
                {
                    if (maze_copy[i, j] == 1 || maze_copy[i, j] == 2 || maze_copy[i, j] == 3)
                    
                    {
                      Instantiate(pathBlock, origin + (i * offsetMarginX) + (j * offsetMarginZ) + (heights[i,j] * offsetMarginY * heightMultiplier), transform.rotation);
                    }

                    if (maze_copy[i, j] == -2) //UPDATE: -2 is now the walls
                    //only using 3 for debugging. -1 is the main value to use.
                    {
                        Instantiate(bigWall, origin + (i * offsetMarginX) + (j * offsetMarginZ), transform.rotation);
                    }

                    if (maze_copy[i, j] == -3)
                    //only using 3 for debugging. -1 is the main value to use.
                    {
                        Instantiate(bigWall, origin + (i * offsetMarginX) + (j * offsetMarginZ), transform.rotation);
                    }
                    if (maze_copy[i, j] == -4)
                    //only using 3 for debugging. -1 is the main value to use.
                    {
                        Instantiate(bigWall, origin + (i * offsetMarginX) + (j * offsetMarginZ), transform.rotation);
                    }

                    bool blockFits = false;

                    if (maze_copy[i, j] == 3 || maze_copy[i, j] == 1 || maze_copy[i, j] == 2) 
                        //if empty or a start or exit block, don't place a wall block.
                    {
                        continue;
                    }
                    if (i > 0)
                    {
                        if (maze_copy[i-1, j] > 0)
                        {
                            blockFits = true;
                        }
                    }
                    if (i < maze_width - 1)
                    {
                        if (maze_copy[i + 1, j] > 0)
                        {
                            blockFits = true;
                        }
                    }
                    if (j > 0)
                    {
                        if (maze_copy[i, j - 1] > 0)
                        {
                            blockFits = true;
                        }
                    }
                    if (j < maze_length - 1)
                    {
                        if (maze_copy[i, j + 1] > 0)
                        {
                            blockFits = true;
                        }
                    }

                    if (blockFits == true)
                    {
                        Instantiate(bigWall, origin + (i * offsetMarginX) + (j * offsetMarginZ), transform.rotation);
                    }

                }

            }
        } else
        {
            //Time.timeScale = 0;
        }
        Debug.Log("error 16");
}

    // Update is called once per frame
    void Update()
    {
        
    }
}
