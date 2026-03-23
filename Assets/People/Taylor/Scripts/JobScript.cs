using UnityEngine;
using Unity.Jobs;
using Unity.Collections;
using Unity.Mathematics;
public class JobScript : MonoBehaviour
{
    [SerializeField] private bool useJob = true;
    private void Update()
    {
        float startTime = Time.realtimeSinceStartup;
        ReallyToughTask();
        if (useJob)
        {
             startTime = Time.realtimeSinceStartup;
            ReallyToughTaskWithJob();
        }
        else
        {
            startTime = Time.realtimeSinceStartup;
            ReallyToughTask();
        }

        JobHandle jobHandle = ReallyToughTaskWithJob();
        jobHandle.Complete();
        Debug.Log(((Time.realtimeSinceStartup - startTime) * 1000f) + " ms");
    }

    private void ReallyToughTask()
    {
        float value = 0f;
        for (int i = 0; i < 50000; i++)
        {
            value += math.sqrt(i);
        }
    }

    //schedule job here
    private JobHandle ReallyToughTaskWithJob()
    {
        ReallyToughJob job = new ReallyToughJob();
        return job.Schedule();
    }
}

//struct makes all info for job n shi
public struct ReallyToughJob : IJob
{
    public void Execute()
    {
        float value = 0f;
        for (int i = 0; i < 50000; i++)
        {
            value += math.sqrt(i);
        }
    }
}