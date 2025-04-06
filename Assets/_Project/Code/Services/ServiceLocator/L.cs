using System;
using Assets.CourseGame.Develop.DI;

namespace _Project.Code.Services.ServiceLocator
{
    public class L
    {
        public static DIContainer Container
        {
            get => _container ?? throw new InvalidOperationException("ServiceLocator not initialized.");
            set => _container = value;
        }

        private static DIContainer _container;

        public static void Init(DIContainer container)
        {
            _container = container;
        }


        public static DIContainer.Registration Reg<TService>(Func<DIContainer, TService> creator) => 
            _container.RegisterAsSingle(creator);

        public static TService Resolve<TService>() => 
            _container.Resolve<TService>();
    }
}