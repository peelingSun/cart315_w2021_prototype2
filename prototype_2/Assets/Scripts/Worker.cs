using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
[RequireComponent(typeof(NavMeshAgent))]
public class Worker : MonoBehaviour
{
    #region Basic information
    private string location;
    public string Location { get { return location; } }
    private string name;
    public string Name { get { return name;} set { name = value;} }
    private static int id = 0;
    public int Id { get { return id;} }
    #endregion

    #region Progression variables
    private int level = 0;
    public int Level { get { return level;} }    
    private int experience = 0;
    public int Experience { get { return experience;} }
    private int stamina = 15; // Very weak workers initially, player needs to progress/upgrade!
    public int Stamina { get { return stamina;} }
    #endregion

    #region Task processing variables (can be progressed too)
    private float workBatchProcessingSpeed = 0.2f; // Very slow at work initially => Progression
    public float WorkBatchProcessingSpeed = 0.2f;
    private int rawBatchWorkPower = 1; // base tick per batch => Progression
    public int RawBatchWorkPower { get {return rawBatchWorkPower;} }    
    public bool multiTaskEnabled = false; // Special ability?
    #endregion

    #region Status flags
    private bool isWorking = false;
    public bool IsWorking { get { return isWorking; } set { isWorking = value; } }
    // Can't have work without a task, can have a task and not be working
    private Task currentTask;
    public Task CurrentTask { get { return currentTask; } set { currentTask = value; } }
    #endregion

    #region Events
    // Task finished event
    public delegate void OnTaskFinished(Worker thisWorker, Task currentTask);
    public static event OnTaskFinished onTaskFinished;
    #endregion

    #region Required components
    private NavMeshAgent agent;
    #endregion

    #region SCRIPTABLE OBJECT Locations
    public Workfield WORKFIELD_1;
    #endregion

    private void Start()
    {
        agent = GetComponent<NavMeshAgent>();
    }

    public void MoveToDestination(Vector3 dest)
    {
        agent.SetDestination(dest);
    }

    public void InternalMoveToDestination(string location)
    {
        this.location = location;
    }

    public void SetTask(Task task)
    {
        currentTask = task;
    }

    public void CheckLocationAction()
    {
        switch(location)
        {
            case "WORKFIELD_1":
                // 1. Request task from TaskController, fetch any pending task if tasks exist
                isWorking = true;
                StartCoroutine("StartWorking");            
            break;
            default:
            break;
        }
    }
    // Should this be called every next tick delay? Maybe a reason to do it is if the player somehow buffs the worker's work speed, one needs to recalculate
    // or if the player simply changes the number of progress hours required
    public float CalculateCurrentTaskProgressRequired()
    {   
        // 1 clock tick / 10 seconds
        // hence if nexttickdelay = 3s, then there are 3 1/3 work ticks / 1 clock tick or 10 seconds real time or 30 mins in-game
        // hence 6 2/3 work ticks / 1 hour in-game
        float workBatchesPerHour = SceneController.tickInterval / CurrentTask.WorkBatchNextTickDelay * (60 / SceneController.minutesIncrementPerTick);
        float taskProgressPerHour = workBatchProcessingSpeed * rawBatchWorkPower * workBatchesPerHour;
        print("Work batches per hour: " + workBatchesPerHour);
        print("Task Progress per hour: " + taskProgressPerHour);
        print("Current Task Progress Required : " + taskProgressPerHour * currentTask.ProgressHoursRequired);
        return taskProgressPerHour * currentTask.ProgressHoursRequired;
    }

    public IEnumerator StartWorking()
    {
        if(!isWorking || currentTask == null) {
            yield break;
        }
        yield return new WaitForSeconds(currentTask.WorkBatchNextTickDelay);
        float progress = workBatchProcessingSpeed * rawBatchWorkPower; // 0.2 * 1 at level 0
        currentTask.CurrentWorkBatchProgress += progress;
        ++currentTask.CurrentWorkBatch;
        --stamina;

        print(name + $"Worker {id} work batch log:\n Actual Task Progress {currentTask.CurrentWorkBatchProgress/CalculateCurrentTaskProgressRequired()*100}%, \nBatch completion: {currentTask.CurrentWorkBatch/currentTask.CurrentWorkBatchLimit*100}% completed.");
        // Check worker stamina before restarting
        if(HasStaminaLeft()) {
            // Stop condition 1: task progress required calculated is met
            if(currentTask.CurrentWorkBatchProgress >= CalculateCurrentTaskProgressRequired()) 
            {
                onTaskFinished(this, currentTask);
                StopWorking();
                yield break;
            }
            // Stop condition 2: work batch limit is met
            if(currentTask.CurrentWorkBatch < currentTask.CurrentWorkBatchLimit) 
            {
                StartCoroutine("RestartWorking");
            } else
            {
                print("Current work batch limit completed");
                StopWorking();
            }
        } else { // Stop condition 3: stamina out
            print("Out of stamina, MASTER");
            StopWorking();
        }
    }

    public void StopWorking()
    {
        if(isWorking) {
            isWorking = false;
        }
    }

    public bool HasStaminaLeft()
    {
        return stamina > 0;
    }

    public IEnumerator RestartWorking()
    {
        yield return new WaitForSeconds(0.0f);
        StartCoroutine("StartWorking");
    }

    public string ToString()
    {
        return $"Name: {name} \nId: {id} \nLevel: {level} \nExperience: {experience} \nLocation: {location}";
    }

    public override bool Equals(object obj)
    {        
        if (obj == null || GetType() != obj.GetType())
        {
            return false;
        }
        Worker otherWorker = (Worker) obj;
        if(otherWorker.Name != name || otherWorker.Id != id || otherWorker.Experience != experience || otherWorker.Level != level) {
            return false;
        }
        return true;
    }

    public override int GetHashCode()
    {
        return base.GetHashCode();
    }
}
