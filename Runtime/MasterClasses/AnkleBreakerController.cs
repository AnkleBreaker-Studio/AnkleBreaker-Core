using UnityEngine;

namespace AnkleBreaker.Core.MasterClasses
{
    /// <summary>Base class for Controllers (the triad's body): no bus registration, no
    /// readiness — driven by its Manager, pushes intent via HandlerData Request helpers.</summary>
    public abstract class AnkleBreakerController : MonoBehaviour
    {
    }
}
