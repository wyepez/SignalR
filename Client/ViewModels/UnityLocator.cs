﻿using Client.Services;
using Unity;
using Unity.Lifetime;

namespace Client.ViewModels
{
    public class UnityLocator
    {
        private readonly UnityContainer unityContainer = new UnityContainer();

        public MainPageViewModel MainPageViewModel => unityContainer.Resolve<MainPageViewModel>();
        public ChatRoomPageViewModel ChatRoomPageViewModel => unityContainer.Resolve<ChatRoomPageViewModel>();

        public UnityLocator()
        {
            unityContainer.RegisterType<IHub, Hub>(new ContainerControlledLifetimeManager());
        }
    }
}
