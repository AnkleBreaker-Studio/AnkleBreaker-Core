namespace AnkleBreaker.Core.MasterDelegates
{
    // 1 Ref
    public delegate void ActionRef<T1>(ref T1 arg1);
    public delegate void ActionRef<in T1, T2>(T1 arg1, ref T2 arg2);
    public delegate void ActionRef<in T1, in T2, T3>(T1 arg1, T2 arg2, ref T3 arg3);
    public delegate void ActionRef<in T1, in T2, in T3, T4>(T1 arg1, T2 arg2, T3 arg3, ref T4 arg4);
    public delegate void ActionRef<in T1, in T2, in T3, in T4, T5>(T1 arg1, T2 arg2, T3 arg3, T4 arg4, ref T5 arg5);
    public delegate void ActionRef<in T1, in T2, in T3, in T4, in T5, T6>(T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5, ref T6 arg6);

    // 2 Ref
    public delegate void ActionRefRef<T1, T2>(ref T1 arg1, ref T2 arg2);
    public delegate void ActionRefRef<in T1, T2, T3>(T1 arg1, ref T2 arg2, ref T3 arg3);
    public delegate void ActionRefRef<in T1, in T2, T3, T4>(T1 arg1, T2 arg2, ref T3 arg3, ref T4 arg4);
    public delegate void ActionRefRef<in T1, in T2, in T3, T4, T5>(T1 arg1, T2 arg2, T3 arg3, ref T4 arg4, ref T5 arg5);
    public delegate void ActionRefRef<in T1, in T2, in T3, in T4, T5, T6>(T1 arg1, T2 arg2, T3 arg3, T4 arg4, ref T5 arg5, ref T6 arg6);
    
    // 3 Ref
    public delegate void ActionRefRefRef<T1, T2, T3>(ref T1 arg1, ref T2 arg2, ref T3 arg3);
    public delegate void ActionRefRefRef<in T1, T2, T3, T4>(T1 arg1, ref T2 arg2, ref T3 arg3, ref T4 arg4);
    public delegate void ActionRefRefRef<in T1, in T2, T3, T4, T5>(T1 arg1, T2 arg2, ref T3 arg3, ref T4 arg4, ref T5 arg5);
    public delegate void ActionRefRefRef<in T1, in T2, in T3, T4, T5, T6>(T1 arg1, T2 arg2, T3 arg3, ref T4 arg4, ref T5 arg5, ref T6 arg6);
    
    // 1 In
    public delegate void ActionIn<T1>(in T1 arg1);
    public delegate void ActionIn<in T1, T2>(T1 arg1, in T2 arg2);
    public delegate void ActionIn<in T1, in T2, T3>(T1 arg1, T2 arg2, in T3 arg3);
    public delegate void ActionIn<in T1, in T2, in T3, T4>(T1 arg1, T2 arg2, T3 arg3, in T4 arg4);
    public delegate void ActionIn<in T1, in T2, in T3, in T4, T5>(T1 arg1, T2 arg2, T3 arg3, T4 arg4, in T5 arg5);
    
    // 2 In
    public delegate void ActionInIn<T1, T2>(in T1 arg1, in T2 arg2);
    public delegate void ActionInIn<in T1, T2, T3>(T1 arg1, in T2 arg2, in T3 arg3);
    public delegate void ActionInIn<in T1, in T2, T3, T4>(T1 arg1, T2 arg2, in T3 arg3, in T4 arg4);
    public delegate void ActionInIn<in T1, in T2, in T3, T4, T5>(T1 arg1, T2 arg2, T3 arg3, in T4 arg4, in T5 arg5);
    
    // 1 In & 1 Ref
    public delegate void ActionInRef<T1, T2>(in T1 arg1, ref T2 arg2);
    public delegate void ActionInRef<in T1, T2, T3>(T1 arg1, in T2 arg2, ref T3 arg3);
    public delegate void ActionInRef<in T1, in T2, T3, T4>(T1 arg1, T2 arg2, in T3 arg3, ref T4 arg4);
    public delegate void ActionInRef<in T1, in T2, in T3, T4, T5>(T1 arg1, T2 arg2, T3 arg3, in T4 arg4, ref T5 arg5);
    
    // 2 In & 1 Ref
    public delegate void ActionInInRef<T1, T2, T3>(in T1 arg1, in T2 arg2, ref T3 arg3);
    public delegate void ActionInInRef<in T1, T2, T3, T4>(T1 arg1, in T2 arg2, in T3 arg3, ref T4 arg4);
    public delegate void ActionInInRef<in T1, in T2, T3, T4, T5>(T1 arg1, T2 arg2, in T3 arg3, in T4 arg4, ref T5 arg5);
    
    // 1 In & 2 Ref
    public delegate void ActionInRefRef<T1, T2, T3>(in T1 arg1, ref T2 arg2, ref T3 arg3);
    public delegate void ActionInRefRef<in T1, T2, T3, T4>(T1 arg1, in T2 arg2, ref T3 arg3, ref T4 arg4);
    public delegate void ActionInRefRef<in T1, in T2, T3, T4, T5>(T1 arg1, T2 arg2, in T3 arg3, ref T4 arg4, ref T5 arg5);
}