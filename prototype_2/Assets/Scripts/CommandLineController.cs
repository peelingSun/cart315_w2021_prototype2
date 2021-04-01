using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;

public class CommandLineController : MonoBehaviour
{
    public static TMP_InputField commandLine;

    public void Start()
    {
        commandLine = GameObject.FindGameObjectWithTag("CommandLine").GetComponent<TMP_InputField>();
    }

    public static void ParseCommand()
    {
        string? validatedCommand = CommandLineExecutor.ValidateCommand(commandLine.text);
        if (validatedCommand != null)
        {
            CommandLineExecutor.ExecuteCommand(validatedCommand);
        }
    }

    public class CommandLineActions
    {
        public static void Feed(List<string> args)
        {
            if(args == null)
            {
                // Feed all case
                Debug.Log("Feeding all cubs");
                
            } else
            {
                foreach(string arg in args)
                {
                    Debug.Log($"{arg}");
                }
            }
        }

        public static void Rest(List<string> args)
        {
            throw new NotImplementedException();
        }

        public static void Exercise(List<string> args)
        {
            throw new NotImplementedException();
        }

        public static void Work(List<string> args)
        {
            throw new NotImplementedException();
        }

        public static void Sell(List<string> args)
        {
            throw new NotImplementedException();
        }

        public static void Examine(List<string> args)
        {
            throw new NotImplementedException();
        }

        public static void Buy(List<string> args)
        {
            if (args == null)
            {
                // Feed all case
                Debug.Log("Buying all that is possible within the boundaries of personal material resources.");
            }
            else
            {
                int qty = default;
                if (int.TryParse(args[0], out qty))
                {
                    if(qty > 0 && args.Count > 1)
                    {
                        // Buying qty of X
                        for(int i = 1; i < args.Count; i++)
                        {
                            if (args[i].Equals("chicken") || args[i].Equals("sheep") ||
                                args[i].Equals("cow") || args[i].Equals("duck") ||
                                args[i].Equals("fox") || args[i].Equals("pig") ||
                                args[i].Equals("wolf"))
                            {
                                Debug.Log("Buying a silly cub!");
                                if(!Main.currentCubRoosterFull)
                                {
                                    Main.LevelController.GenerateNewCub(qty, args[i]);
                                } else
                                {
                                    Debug.Log("Cub rooster is full");
                                }
                            } else
                            {
                                Debug.Log("Type of goods doesn't exist.");
                            }                            
                        }
                    }
                }
            }
        }
    }

    public class CommandLineExecutor
    {
        public CommandLineExecutor() { }

        public static string ValidateCommand(string commandLineText)
        {
            if(commandLineText.Length <= 0 || commandLineText == null)
            {
                return null;
            }
            commandLineText = commandLineText.Trim().ToLower().Replace(" ", string.Empty);
            return commandLineText;
        }

        public static void ExecuteCommand(string commandLineText)
        {
            List<string> args = new List<string>();
            int argsStartIndex = commandLineText.IndexOf('(');
            int argsEndIndex = commandLineText.IndexOf(')');
            int argsRange = argsEndIndex - argsStartIndex - 1;
            try
            {
                if (argsStartIndex != -1)
                {
                    if (commandLineText[argsStartIndex + 1] != ')')
                    {
                        int argCount = commandLineText.IndexOf(',');
                        if(argCount == -1)
                        {
                            // Single arg
                            args.Add(commandLineText.Substring(argsStartIndex + 1, argsRange));
                        } else
                        {
                            string[] _args = commandLineText.Substring(argsStartIndex + 1, argsRange).Split(',');
                            args.AddRange(_args);
                        }
                    } else
                    {
                        commandLineText = commandLineText.Replace('(', ' ').Replace(')', ' ');
                    }
                }
            } catch(System.ArgumentOutOfRangeException e)
            {
                Debug.LogError(e.Message);
            } catch(System.Exception e)
            {
                Debug.LogError(e.Message);
            }

            string methodCall = commandLineText.Substring(0, argsStartIndex);
            switch (methodCall)
            {
                case "feed":
                    Debug.Log("Feeding: ");
                    CommandLineActions.Feed(args);
                    break;
                case "rest":
                    Debug.Log("Resting: ");
                    CommandLineActions.Rest(args);
                    break;
                case "exercise":
                    Debug.Log("Exercising: ");
                    CommandLineActions.Exercise(args);
                    break;
                case "work":
                    Debug.Log("Working: ");
                    CommandLineActions.Work(args);
                    break;
                case "sell":
                    Debug.Log("Selling: ");
                    CommandLineActions.Sell(args);
                    break;
                case "buy":
                    Debug.Log("Buying: ");
                    CommandLineActions.Buy(args);
                    break;
                default:
                    break;
            }
            commandLine.text = "";
        }
    }
}
