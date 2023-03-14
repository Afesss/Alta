using Architecture.Services;
using UnityEngine;
using Zenject;

namespace Architecture
{
    public class Boot : MonoInstaller
    {
        [SerializeField] private ServiceRegistrator services;
        
        public override void InstallBindings()
        {
            Container.BindInstance(services).AsSingle();
            services.Initialize();
        }
    }
}