using System;
using UnityEngine;
using Zenject;

public class UIInstaller : MonoInstaller {

    [SerializeField] private AppEntry _entryPoint;
    
    private Type[] _domainTypes;
    
    public override void InstallBindings() {
        _domainTypes = AppDomain
            .CurrentDomain
            .GetAssembliesTypes();

        BindSources();

        BindStates();

        BindEntry();
    }
    
    private void BindEntry() {
        Container.BindInstance(_entryPoint).AsSingle();
    }

    private void BindStates() {
        Container
            .BindInterfacesAndSelfTo<AppStates>()
            .AsSingle();
    }
    
    private void BindSources() {
        Container
            .BindInstance(FindObjectsOfType<ScreenState>(true))
            .AsSingle();
    }
    
    
}
