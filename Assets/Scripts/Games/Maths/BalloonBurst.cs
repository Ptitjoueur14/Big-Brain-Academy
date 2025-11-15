using System.Collections.Generic;
using UnityEngine;

public class BalloonBurst : MonoBehaviour
{
    public List<Balloon> balloons;
    public Balloon lastPoppedBalloon; //Last balloon popped by the player
    public Balloon balloonToSpawn;
    
    public int totalBallons;
    public int balloonCount;

    public void SpawnBalloon()
    {
        if (balloonCount < totalBallons)
        {
            //Spawn balloon in random position after checking if it doesn't overlap with a corner or other balloon
            Balloon balloon = Instantiate(balloonToSpawn, transform);
        }
    }

    public bool AreAllBalloonsPopped()
    {
        //Go to next level
        return balloons.Count == 0;
    }

    public bool PopBallon(Balloon balloon)
    {
        if (lastPoppedBalloon < balloon) 
        {
            //Wrong X
            return false;
        }
        else
        {
            //Destroy balloon
            lastPoppedBalloon = balloon;
            balloons.Remove(balloon);
            Destroy(balloon);
            return true;
        }
    }
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
