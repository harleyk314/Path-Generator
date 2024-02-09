using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{

    public GameObject building;
    public GameObject superBuilding;
    public GameObject superDuperBuilding;
    public GameObject roof;
    public GameObject park;

    public Vector3 origin;
    public Vector3 offsetX;
    public Vector3 offsetY;
    public Vector3 offsetZ;

    // Start is called before the first frame update
    void Start()
    {
        building.SetActive(true);
        superBuilding.SetActive(true);
        superDuperBuilding.SetActive(true);
        roof.SetActive(true);
        park.SetActive(true);

        origin = new Vector3(-200, 0, -200);

        offsetX = new Vector3(200, 0, 0);
        offsetY = new Vector3(0, 50, 0);
        offsetZ = new Vector3(0, 0, 200);

        for (int i = 0; i < 24; i++)
        {
            for (int j = 0; j < 24; j++)
            {

                if (i % 6 < 3 && j % 6 < 3)
                {
                    //generate a house or large house
                    Instantiate(building, origin + (i * offsetX) + (j * offsetZ), transform.rotation);

                    if (i % 3 == 0 && j % 3 == 0)
                    {
                        Instantiate(building, origin + (i * offsetX) + offsetY + (j * offsetZ), transform.rotation);

                        Instantiate(roof, origin + (i * offsetX) + offsetY + (j * offsetZ), transform.rotation);
                    }
                    else
                    {


                        Instantiate(roof, origin + (i * offsetX) + (j * offsetZ), transform.rotation);
                    }
                }

                if (i % 6 == 4 && j % 6 == 4)
                {
                    //generate a super building

                    if (i == 10 && j == 10)
                    {
                        //generate a super duper building
                        Instantiate(superDuperBuilding, origin + (i * offsetX) + (j * offsetZ), transform.rotation);
                    }
                    else
                    {
                        //generate a super building
                        Instantiate(superBuilding, origin + (i * offsetX) + (j * offsetZ), transform.rotation);
                    }
                }

                if ((i % 6 == 4 && j % 6 == 1) || (i % 6 == 1 && j % 6 == 4))
                {
                    //generate a park
                    Instantiate(park, origin + (i * offsetX) + (j * offsetZ), transform.rotation);
                }

            }
            
        }

        building.SetActive(false);
        superBuilding.SetActive(false);
        superDuperBuilding.SetActive(false);
        roof.SetActive(false);
        park.SetActive(false);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        
    }
}
