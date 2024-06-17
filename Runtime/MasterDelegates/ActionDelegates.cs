using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AnkleBreaker.Core.MasterDelegates
{
    // 0 Ref
    public delegate void ActionValValValValVal<T1, T2, T3, T4, T5>(T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5);
    public delegate void ActionValValValValValVal<T1, T2, T3, T4, T5, T6>(T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5, T6 arg6);

    // 1 Ending Ref
    public delegate void ActionRef<T1>(ref T1 arg1);
    public delegate void ActionValRef<T1, T2>(T1 arg1, ref T2 arg2);
    public delegate void ActionValValRef<T1, T2, T3>(T1 arg1, T2 arg2, ref T3 arg3);
    public delegate void ActionValValValRef<T1, T2, T3, T4>(T1 arg1, T2 arg2, T3 arg3, ref T4 arg4);
    public delegate void ActionValValValValRef<T1, T2, T3, T4, T5>(T1 arg1, T2 arg2, T3 arg3, T4 arg4, ref T5 arg5);
    public delegate void ActionValValValValValRef<T1, T2, T3, T4, T5, T6>(T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5, ref T6 arg6);

    // 1 Ref 1 rank before end
    public delegate void ActionValRefVal<T1, T2, T3>(T1 arg1, ref T2 arg2, T3 arg3);
    public delegate void ActionValValRefVal<T1, T2, T3, T4>(T1 arg1, T2 arg2, ref T3 arg3, T4 arg4);

    // 2 Ending Ref
    public delegate void ActionRefRef<T1, T2>(ref T1 arg1, ref T2 arg2);
    public delegate void ActionRefRefRef<T1, T2,T3>(ref T1 arg1, ref T2 arg2,ref T3 arg3);
    public delegate void ActionValRefRef<T1, T2, T3>(T1 arg1, ref T2 arg2, ref T3 arg3);
    public delegate void ActionValValRefRef<T1, T2, T3, T4>(T1 arg1, T2 arg2, ref T3 arg3, ref T4 arg4);
    public delegate void ActionValValValRefRef<T1, T2, T3, T4, T5>(T1 arg1, T2 arg2, T3 arg3, ref T4 arg4, ref T5 arg5);

    // 1 Ending in
    public delegate void ActionValIn<T1, T2>(T1 arg1, in T2 arg2);
    public delegate void ActionValValIn<T1, T2, T3>(T1 arg1, T2 arg2, in T3 arg3);

    // Misc
    public delegate void ActionValInRef<T1, T2, T3>(T1 arg1, in T2 arg2, ref T3 arg3);
    public delegate void ActionValInValValRef<T1, T2, T3, T4, T5>(T1 arg1, in T2 arg2, T3 arg3, T4 arg4, ref T5 arg5);
    public delegate void ActionValValRefValVal<T1, T2, T3, T4, T5>(T1 arg1, T2 arg2, ref T3 arg3, T4 arg4, T5 arg5);
    public delegate void ActionValValRefValValVal<T1, T2, T3, T4, T5, T6>(T1 arg1, T2 arg2, ref T3 arg3, T4 arg4, T5 arg5, T6 arg);
}
