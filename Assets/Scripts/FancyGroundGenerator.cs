using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FancyGroundGenerator : MonoBehaviour
{
    public GameObject groundBlock;

    public Vector3 origin;
    public Vector3 offsetMarginX;
    public Vector3 offsetMarginY;
    public Vector3 offsetMarginZ;

    public int ground_width;
    public int ground_length;

    // Start is called before the first frame update
    void Start()
    {
        offsetMarginX = new Vector3(5, 0, 0);
        offsetMarginY = new Vector3(0, 3, 0);
        offsetMarginZ = new Vector3(0, 0, 5);
        origin = new Vector3(0, 0, 0);

        ground_width = 50;
        ground_length = 50;

        //Reference points for determining block height
        int point1 = 0;
        int point2 = 0;

        int[,] groundGrid = new int[ground_width, ground_length];

        //instantiation time
        for (int l = 0; l < ground_width; l++)
        {
            for (int m = 0; m < ground_length; m++)
            {
                if (l == 0 && m == 0)
                {
                    //For the first block, set the height to 0
                    groundGrid[l, m] = 0;
                }
                else if (l == 0)
                {
                    int rInt = Random.Range(-1, 2);
                    groundGrid[l, m] = groundGrid[l, m - 1] + rInt;
                }
                else if (m == 0)
                {
                    int rInt = Random.Range(-1, 2);
                    groundGrid[l, m] = groundGrid[l - 1, m] + rInt;
                }
                else
                {
                    //Use 2 reference points to deduce the possible heights
                    point1 = groundGrid[l, m - 1];
                    point2 = groundGrid[l - 1, m];

                    if (point1 == point2)
                    {
                        //Any of the 3 points is fine
                        int rInt = Random.Range(-1, 2);
                        groundGrid[l, m] = point1 + rInt;
                    }
                    else if (point1 + 1 == point2)
                    {
                        //Only 2 possible points work
                        int rInt = Random.Range(0, 2);
                        groundGrid[l, m] = point1 + rInt;
                    }
                    else if (point1 - 1 == point2)
                    {
                        //Only 2 possible points work
                        int rInt = Random.Range(0, 2);
                        groundGrid[l, m] = point2 + rInt;
                    }
                    else
                    {
                        //The gap is 2. Hence, only the average of the 2 points works
                        groundGrid[l, m] = (point1 + point2) / 2;
                    }
                }

                //Instantiate the block
                Instantiate(groundBlock, origin + (l * offsetMarginX) + (m * offsetMarginZ) + (groundGrid[l, m] * offsetMarginY), transform.rotation);
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
