using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorldBehaviors : MonoBehaviour
{
    [SerializeField] AgentController agent_controller;
    [SerializeField] ZombieController zombie_controller;
    [SerializeField] public List<ZombieController> spawnedZombieList = new List<ZombieController>();
    [SerializeField] int zombie_count;

    [SerializeField] Transform environment_location;

    [SerializeField] int time_between_spawns = 1;
    [SerializeField] Transform spawn_zone_1;
    [SerializeField] Transform spawn_zone_2;
    [SerializeField] Transform spawn_zone_3;
    [SerializeField] Transform spawn_zone_4;


    public void spawnAgent()
    {
        agent_controller.transform.localPosition = new Vector3(0f, 1.02f, 0f);
    }

    public void callSpawnZombie()
    {
        StartCoroutine(spawnZombie());
    }
    IEnumerator spawnZombie()
    {
        Vector3 spawn_zone_1_coords = spawn_zone_1.transform.position;
        Vector3 spawn_zone_2_coords = spawn_zone_2.transform.position;
        Vector3 spawn_zone_3_coords = spawn_zone_3.transform.position;
        Vector3 spawn_zone_4_coords = spawn_zone_4.transform.position;


        if (spawnedZombieList.Count != 0)
        {
            removeZombie(spawnedZombieList);
        }

        for(int i = 0; i < zombie_count; i++)
        {
            //   (size/2)-1
            Vector3 random_spawn_zone_1_coords = spawn_zone_1_coords + new Vector3(
                Random.Range(-4.3695f, 4.3695f), 1f, Random.Range(-3.96f, 3.96f));
            Vector3 random_spawn_zone_2_coords = spawn_zone_2_coords + new Vector3(
                Random.Range(-4.3695f, 4.3695f), 1f, Random.Range(-3.96f, 3.96f));
            Vector3 random_spawn_zone_3_coords = spawn_zone_3_coords + new Vector3(
                Random.Range(-4.3695f, 4.3695f), 1f, Random.Range(-3.96f, 3.96f));
            Vector3 random_spawn_zone_4_coords = spawn_zone_4_coords + new Vector3(
                Random.Range(-4.3695f, 4.3695f), 1f, Random.Range(-3.96f, 3.96f));

            Vector3[] random_area = {
                random_spawn_zone_1_coords, random_spawn_zone_2_coords,
                random_spawn_zone_3_coords, random_spawn_zone_4_coords
            };

            int counter = 0;
            bool distanceGood;

            ZombieController new_zombie = Instantiate(zombie_controller);
            new_zombie.target = agent_controller.transform;
            new_zombie.world_behaviors = this;
            new_zombie.transform.parent = environment_location;

            Vector3 zombie_location = random_area[Random.Range(0, random_area.Length)];

            if(spawnedZombieList.Count == 0)
            {
                new_zombie.moveZombie(zombie_location);
                spawnedZombieList.Add(new_zombie);
                yield return new WaitForSeconds(time_between_spawns);
            }
            else if(spawnedZombieList.Count != 0)
            {
                for(int k = 0; k < spawnedZombieList.Count; k++)
                {
                    if(counter < 10)
                    {
                        distanceGood = CheckOverlap(zombie_location, spawnedZombieList[k].transform.localPosition, 1f);
                        if(distanceGood == false)
                        {
                            zombie_location = random_area[Random.Range(0, random_area.Length)];
                            k--;
                        }
                        counter++;
                    }
                    else
                    {
                        k = spawnedZombieList.Count;
                    }
                }
                new_zombie.moveZombie(zombie_location);
                spawnedZombieList.Add(new_zombie);
                yield return new WaitForSeconds(time_between_spawns);
            }
        }    
    }

    public bool CheckOverlap(Vector3 objectWeWantToAvoidOverlapping, Vector3 alreadyExistingObject, float minDistanceWanted)
    {
        float distance_between_objects = Vector3.Distance(objectWeWantToAvoidOverlapping, alreadyExistingObject);
        if(minDistanceWanted <= distance_between_objects)
        {
            return true;
        }
        return false;
    }

    public int getZombieCount()
    {
        return spawnedZombieList.Count;
    }

    public void removeZombie(List<ZombieController> to_be_deleted_game_object_list)
    {
        foreach (ZombieController i in to_be_deleted_game_object_list)
        {
            Destroy(i.gameObject);
        }
        to_be_deleted_game_object_list.Clear();
    }
}
