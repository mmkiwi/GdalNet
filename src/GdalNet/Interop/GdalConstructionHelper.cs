namespace MMKiwi.GdalNet.Interop;

internal static class GdalConstructionHelper
{
    public static TRes Construct<TRes, THandle>(THandle handle)
        where TRes : class, IConstructableWrapper<TRes, THandle>
        where THandle : GdalInternalHandle
    {
        if (handle.IsInvalid)
            throw new InvalidOperationException("Cannot marshal null handle");
        return TRes.Construct(handle);
    }

    public static TRes? ConstructNullable<TRes, THandle>(THandle handle)
        where TRes : class, IConstructableWrapper<TRes, THandle>
        where THandle : GdalInternalHandle
    {
        return handle.IsInvalid ? null : TRes.Construct(handle);
    }

    public static THandle GetNullHandle<THandle>()
        where THandle : GdalInternalHandle, IConstructableHandle<THandle>
    {
        return THandle.Construct(false);
    }
}
