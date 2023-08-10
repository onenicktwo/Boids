using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Collections.Generic;

public static class SaveSystem
{
    // Path where the save file will be stored.
    static string path = Application.persistentDataPath + "/flocks.save";

    // Save the current state of flocks to a file.
    public static void SaveFlocks(List<GameManager.Flock> flocks)
    {
        BinaryFormatter formatter = new BinaryFormatter();
        FileStream stream = new FileStream(path, FileMode.Create);
        string json = JsonUtility.ToJson(new FlockData(flocks));
        formatter.Serialize(stream, json);
        stream.Close();
    }

    // Load the saved flocks.
    public static List<GameManager.Flock> LoadFlocks()
    {
        if (File.Exists(path))
        {
            BinaryFormatter formatter = new BinaryFormatter();
            FileStream stream = new FileStream(path, FileMode.Open);
            string jsonString = formatter.Deserialize(stream) as string;
            FlockData data = JsonUtility.FromJson<FlockData>(jsonString);
            stream.Close();
            return data.ToFlockList();
        }
        else
        {
            Debug.LogError("Save file not found at " + path);
            return null;
        }
    }
}

// Blueprint for saving the flock data.
[System.Serializable]
public class FlockData
{
    public List<FlockSerializable> flocks;

    // Convert the list of flocks to a serializable format.
    public FlockData(List<GameManager.Flock> flocks)
    {
        this.flocks = new List<FlockSerializable>();
        foreach (var flock in flocks)
        {
            this.flocks.Add(new FlockSerializable(flock));
        }
    }

    // Convert the serialized data back to a list of flocks.
    public List<GameManager.Flock> ToFlockList()
    {
        List<GameManager.Flock> flockList = new List<GameManager.Flock>();
        foreach (var flockData in flocks)
        {
            flockList.Add(flockData.ToFlock());
        }
        return flockList;
    }
}

// Data structure for saving individual flock information.
[System.Serializable]
public class FlockSerializable
{
    public string flockID;
    public float alignmentWeight;
    public float cohesionWeight;
    public float separationWeight;
    public float initEnergy;
    public float speed;
    public float sightRadius;
    public int particleCount;
    public Color flockColor;

    // Convert the flock to serializable data.
    public FlockSerializable(GameManager.Flock flock)
    {
        flockID = flock.flockID;
        alignmentWeight = flock.alignmentWeight;
        cohesionWeight = flock.cohesionWeight;
        separationWeight = flock.separationWeight;
        initEnergy = flock.initEnergy;
        speed = flock.speed;
        sightRadius = flock.sightRadius;
        particleCount = flock.particleCount;
        flockColor = flock.flockColor;
    }

    // Convert the data back to into a flock.
    public GameManager.Flock ToFlock()
    {
        return new GameManager.Flock
        {
            flockID = this.flockID,
            alignmentWeight = this.alignmentWeight,
            cohesionWeight = this.cohesionWeight,
            separationWeight = this.separationWeight,
            initEnergy = this.initEnergy,
            speed = this.speed,
            sightRadius = this.sightRadius,
            particleCount = this.particleCount,
            flockColor = this.flockColor
        };
    }
}