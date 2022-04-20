﻿using StatisticsAnalysisTool.Common;
using StatisticsAnalysisTool.Models.NetworkModel;
using System;
using System.Collections.Generic;
using System.Reflection;

namespace StatisticsAnalysisTool.Network.Events
{
    public class AttachItemContainerEvent
    {
        public ItemContainerObject ItemContainerObject;
        private readonly long? _objectId;
        private readonly Guid _containerGuid;
        private readonly List<int> _containerSlots = new();

        public AttachItemContainerEvent(Dictionary<byte, object> parameters)
        {
            ConsoleManager.WriteLineForNetworkHandler(GetType().Name, parameters);

            try
            {
                if (parameters.ContainsKey(0))
                {
                    _objectId = parameters[0].ObjectToLong();
                }

                if (parameters.ContainsKey(2))
                {
                    var guid = parameters[2].ObjectToGuid();
                    if (guid != null)
                    {
                        _containerGuid = (Guid)guid;
                    }
                }

                if (parameters.ContainsKey(3) && parameters[3] != null)
                {
                    var valueType = parameters[3].GetType();
                    if (valueType.IsArray && typeof(short[]) == valueType)
                    {
                        var intArray = ((short[])parameters[3]).ToDictionary();

                        foreach (var slot in intArray)
                        {
                            _containerSlots.Add(slot.Value);
                        }
                    }
                    else if (valueType.IsArray && typeof(byte[]) == valueType)
                    {
                        var byteArray = ((byte[])parameters[3]).ToDictionary();

                        foreach (var unused in byteArray)
                        {
                            _containerSlots.Add(0);
                        }
                    }
                }

                ItemContainerObject = new ItemContainerObject(_objectId, _containerGuid, _containerSlots);
            }
            catch (Exception e)
            {
                ConsoleManager.WriteLineForError(MethodBase.GetCurrentMethod()?.DeclaringType, e);
            }
        }
    }
}