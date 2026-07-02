using UnityEngine;

namespace AnkleBreaker.Core.MasterClasses
{
    /// <summary>
    /// Base class for Controllers (the "body" of the Manager/HandlerData/Controller triad).
    /// A Controller never subscribes to the event bus — this base carries none of the
    /// registration/readiness machinery (no EventHandlerRegister, no IIsReady): there is
    /// nothing to override, nothing to subscribe with. It is driven by its Manager through a
    /// direct intra-feature reference and pushes intent via the feature HandlerData Request
    /// helpers.
    /// </summary>
    public abstract class AnkleBreakerController : MonoBehaviour
    {
    }
}
