﻿using System;
using System.Collections;
using System.Collections.Generic;
using static CitizenFX.Core.Native.API;

namespace VStancer.Client
{
    public class KvpEnumerable : IEnumerable<string>
    {
        public string prefix { get; set; } = string.Empty;

        public KvpEnumerable(string kvpprefix)
        {
            this.prefix = kvpprefix;
        }

        public IEnumerator<string> GetEnumerator()
        {
            int handle = StartFindKvp(prefix);

            if (handle != -1)
            {
                string kvp;
                do
                {
                    kvp = FindKvp(handle);

                    if (kvp != null)
                        yield return kvp;
                }
                while (kvp != null);
                EndFindKvp(handle);
            }
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }

    public class VehicleEnumerable : IEnumerable<int>
    {
        public IEnumerator<int> GetEnumerator()
        {
            int entity = -1;
            int handle = FindFirstVehicle(ref entity);

            if (handle != -1)
            {
                do yield return entity;
                while (FindNextVehicle(handle, ref entity));

                EndFindVehicle(handle);
            }
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}
