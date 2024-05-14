using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;


public class Event
{
    private event Action _event = delegate {  };
    private string _logMessage = null;

    public Event(string logMessage = null)
    {
        _logMessage = logMessage;
    }
    public void Invoke()
    {
        _event?.Invoke();
        if (_logMessage != null)
        {
            Debug.Log($"Event invoked: {_logMessage}");
        }
    }
    
    /// <summary>
    /// Subscribe event. Usually done in OnEnable()
    /// </summary>
    /// <param name="listener">The method name to be invoked in response to the event. </param>
    public void Subscribe(Action listener)
    {
        _event -= listener; //Remove subscriber in case already subscribed. If not subscribed, does nothing.
                            //Not safe for multi-threading
        _event += listener;
    }
    public void Unsubscribe(Action listener)
    {
        _event -= listener;
    }

    public void AddInvokeLog(string message)
    {
        _logMessage = message;
    }
}

/// <summary>
/// Event with a parameter
/// </summary>
/// <typeparam name="T"></typeparam>
public class Event<T>
{
    private event Action<T> _event = delegate{  };
    private string _logMessage = null;
    
    public Event(string logMessage = null)
    {
        _logMessage = logMessage;
    }
    public void Invoke(T parameter)
    {
        _event?.Invoke(parameter);
        if (_logMessage != null)
        {
            Debug.Log($"Event invoked: {_logMessage}\nParameter: {parameter.ToString()}");
        }
    }
    
    public void Subscribe(Action<T> listener)
    {
        _event -= listener; //Remove subscriber in case already subscribed. If not subscribed, does nothing.
        //Not safe for multi-threading
        _event += listener;
    }
    public void Unsubscribe(Action<T> listener)
    {
        _event -= listener;
    }
    
    public void AddInvokeLog(string message)
    {
        _logMessage = message;
    }
}

public class Event<T1, T2>
{
    private event Action<T1, T2> _event = delegate {  };
    private string _logMessage = null;
    
    public Event(string logMessage = null)
    {
        _logMessage = logMessage;
    }
    public void Invoke(T1 parameter1, T2 parameter2)
    {
        _event?.Invoke(parameter1, parameter2);
        if (_logMessage != null)
        {
            Debug.Log($"Event invoked: {_logMessage}\nParameters: {parameter1.ToString()}, {parameter2.ToString()}");
        }
    }

    public void Subscribe(Action<T1, T2> listener)
    {
        _event -= listener; //Remove subscriber in case already subscribed. If not subscribed, does nothing.
        //Not safe for multi-threading
        _event += listener;
    }
    public void Unsubscribe(Action<T1, T2> listener)
    {
        _event -= listener;
    }
    
    public void AddInvokeLog(string message)
    {
        _logMessage = message;
    }
}