namespace AnkleBreaker.Core.MasterClasses
{
    /// <summary>
    /// Base class for Controllers (the "body" of the Manager/HandlerData/Controller triad).
    /// A Controller never subscribes to the event bus: registration is sealed empty, so
    /// overriding it is a compile error. It is driven by its Manager through a direct
    /// intra-feature reference and pushes intent via the feature HandlerData Request helpers.
    /// </summary>
    public abstract class AnkleBreakerController : AnkleBreakerMonoBehaviour
    {
        protected sealed override void EventHandlerRegister() { }
        protected sealed override void EventHandlerUnRegister() { }
    }
}
