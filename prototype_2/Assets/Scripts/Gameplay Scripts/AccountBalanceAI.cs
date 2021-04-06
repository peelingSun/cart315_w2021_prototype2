using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public sealed class AccountBalanceAI : MonoBehaviour
{
    private const int NB_DAYS_IN_WEEK = 7;
    private const int NB_HOURS_IN_DAY = 12; // Working hours
    private const int NB_SECONDS_IN_HOUR = 3600;
    private const int NB_TICKS_IN_HOUR = 360; // Times account balance is updated    
    public static GameObject moneyUI;
    public static GameObject cubFoodUI;
    public static GameObject totalBalanceUI;
    public static GameObject netChangeUI;    
    public static GameObject gameOverUI;

    // Gameloop
    public static int cubFood; // Needed to feed cubs and therefore train them
    public static float money = 0; // Start at 1000
    public static int totalGain; // Daily
    public static int totalUpcost = 8; // Each 10 seconds -- TODO count it per Building actual costs defined
    public static float netChange = 0;
    // Ledger variables
    public static int foodEarnedToday = 0;
    public static int moneyEarnedToday = 0;
    public static int cubsSold = 0;
    public static int workersTotalExperienceGained = 0;


    private void OnEnable() 
    {        
        SceneController.onClockTicked += UpdateTotalBalance;
    }

    private void OnDisable() 
    {
        SceneController.onClockTicked -= UpdateTotalBalance;
    }

    private void Start()
    {
        moneyUI = GameObject.FindGameObjectWithTag("moneyCount");
        cubFoodUI = GameObject.FindGameObjectWithTag("foodCount");
        totalBalanceUI = GameObject.FindGameObjectWithTag("totalBalance"); 
        netChangeUI = GameObject.FindGameObjectWithTag("netChange");     
        gameOverUI = GameObject.FindGameObjectWithTag("gameOverUI"); 
    }

    // Can be a negative value for withdraw operations
    public static void UpdateMoney(int value) 
    {
        money += value;
        UpdateTotalBalance();
    }

    public static void UpdateFood(int value)
    {
        cubFood += value;
        UpdateTotalBalance();
    }

    public static void UpdateTotalBalance()
    {
        Debug.Log("Updating total balance");
        //netChange = (totalGain - (totalUpcost / NB_DAYS_IN_WEEK / NB_HOURS_IN_DAY / NB_SECONDS_IN_HOUR / NB_TICKS_IN_HOUR));
        //netChange = (totalGain - totalUpcost);
        Debug.Log("Net change: " + netChange);
        //money += netChange;
        UpdateAccountBalanceUI();
    }
    
    public static void UpdateAccountBalanceUI()
    {
        // Update UI
        moneyUI.GetComponent<TextMeshProUGUI>().SetText($"Coin x{money}");
        cubFoodUI.GetComponent<TextMeshProUGUI>().SetText($"Food x{cubFood}");
        //totalBalanceUI.GetComponent<TextMeshProUGUI>().SetText($"Total Upcost: {totalUpcost * 6 * 60}$ / Hour");       
        //netChangeUI.GetComponent<TextMeshProUGUI>().SetText($"Net Change: {netChange}$ / 10 seconds");
    }
}
