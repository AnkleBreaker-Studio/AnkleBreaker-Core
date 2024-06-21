namespace AnkleBreaker.Core.MasterDelegates
{
    // 1 Ending Ref
    public delegate void ActionRef<T1>(ref T1 arg1);
    public delegate void ActionRef<in T1, T2>(T1 arg1, ref T2 arg2);
    public delegate void ActionRef<in T1, T2, T3>(T1 arg1, in T2 arg2, ref T3 arg3);
    public delegate void ActionRef<in T1, T2, T3, T4>(T1 arg1, in T2 arg2, in T3 arg3, ref T4 arg4);
    public delegate void ActionRef<in T1, in T2, in T3, in T4, T5>(T1 arg1, T2 arg2, T3 arg3, T4 arg4, ref T5 arg5);
    public delegate void ActionRef<in T1, in T2, in T3, in T4, in T5, T6>(T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5, ref T6 arg6);

    // 2 Ending Ref
    public delegate void ActionRefRef<T1, T2>(ref T1 arg1, ref T2 arg2);
    public delegate void ActionRefRef<in T1, T2, T3>(T1 arg1, ref T2 arg2, ref T3 arg3);
    public delegate void ActionRefRef<in T1, in T2, T3, T4>(T1 arg1, T2 arg2, ref T3 arg3, ref T4 arg4);
    public delegate void ActionRefRef<in T1, in T2, in T3, T4, T5>(T1 arg1, T2 arg2, T3 arg3, ref T4 arg4, ref T5 arg5);
    public delegate void ActionRefRef<in T1, in T2, in T3, in T4, T5, T6>(T1 arg1, T2 arg2, T3 arg3, T4 arg4, ref T5 arg5, ref T6 arg6);
    
    // 3 Ending Ref
    public delegate void ActionRefRefRef<T1, T2, T3>(ref T1 arg1, ref T2 arg2, ref T3 arg3);
    public delegate void ActionRefRefRef<in T1, T2, T3, T4>(T1 arg1, ref T2 arg2, ref T3 arg3, ref T4 arg4);
    public delegate void ActionRefRefRef<in T1, in T2, T3, T4, T5>(T1 arg1, T2 arg2, ref T3 arg3, ref T4 arg4, ref T5 arg5);
    public delegate void ActionRefRefRef<in T1, in T2, in T3, T4, T5, T6>(T1 arg1, T2 arg2, T3 arg3, ref T4 arg4, ref T5 arg5, ref T6 arg6);
}