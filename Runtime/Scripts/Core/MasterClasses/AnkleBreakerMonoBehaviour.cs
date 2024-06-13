using UnityEngine;
using AnkleBreaker.Core.MasterInterfaces;

namespace AnkleBreaker.Core.MasterClasses
{
    public abstract class AnkleBreakerMonoBehaviour : MonoBehaviour, IIsReady
    {
        #region Properties
        [field: SerializeField, Tooltip("Set to true once OnStartClient/Server is over")]
        public bool IsLocallyReady { get; private set; }
        #endregion

        #region Initialization
        public virtual void Start()
        {
            EventHandlerRegister();
            IsLocallyReady = true;
        }

        public virtual void OnDestroy()
        {
            EventHandlerUnRegister();
        }

        protected abstract void EventHandlerRegister();
        protected abstract void EventHandlerUnRegister();
        #endregion
    }
}