// This Source Code Form is subject to the terms of the Mozilla Public
// License, v. 2.0. If a copy of the MPL was not distributed with this
// file, You can obtain one at https://mozilla.org/MPL/2.0/.

namespace MMKiwi.GdalNet.CHelpers;

[NativeMarshalling(typeof(GdalHandleMarshallerIn<CStringList, CStringList.MarshalHandle>))]
internal unsafe sealed partial class CStringList:IConstructibleWrapper<CStringList, CStringList.MarshalHandle>
{
    internal MarshalHandle Handle { get; private set; }

    MarshalHandle IHasHandle<MarshalHandle>.Handle => Handle;

    private CStringList(MarshalHandle handle) { Handle = handle; }

    internal sealed partial class MarshalHandle : GdalInternalHandle, IConstructibleHandle<MarshalHandle>
    {
        public MarshalHandle(bool ownsHandle) : base(ownsHandle)
        {
        } 

        static MarshalHandle IConstructibleHandle<MarshalHandle>.Construct(bool ownsHandle) 
            => new(ownsHandle);

        protected override bool ReleaseHandle()
        {
            lock (ReentrantLock)
            {
                if (base.IsInvalid)
                    return false;
                GdalError.ResetErrors();
                Interop.CSLDestroy(handle);
                return GdalError.LastError is not null && GdalError.LastError.Severity is not GdalCplErr.Failure or GdalCplErr.Fatal ;
            }
        }
    }

    static CStringList? IConstructibleWrapper<CStringList, MarshalHandle>.Construct(MarshalHandle handle)
        => new(handle);
}
