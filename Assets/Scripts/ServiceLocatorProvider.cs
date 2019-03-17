using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Player;
using InputProviders;

public class ServiceLocatorProvider : SingletonMonoBehaviour<ServiceLocatorProvider>
{
    public ServiceLocator inputCurrent{ get; private set; }
    /// <summary>
    /// Awake is called when the script instance is being loaded.
    /// </summary>
    protected override void Awake()
    {
        base.Awake();
        inputCurrent = new ServiceLocator();
        inputCurrent.Register<IInputProvider>(new UnityInputProvider());
    }
}