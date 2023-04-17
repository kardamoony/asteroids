using System;
using System.Collections.Generic;
using Asteroids.UILayer.MVVM;
using UnityEngine;

namespace Asteroids.UILayer.Views.AttemptsView
{
    public class AttemptsViewModel : UIViewModel<AttemptsModel>
    {
        public int AttemptsLeft => Model.AttemptsCount;

        private List<GameObject> _instantiatedPrefabs;
        
        //todo: to base class?
        public void InstantiateWidget(string id, Action<Transform> callback)
        {
            Model.Factory.Get<Transform>(id, callback);
        }

        public void DestroyWidget(GameObject gameObject)
        {
            Model.Factory.Release(gameObject, false);
        }
    }
}