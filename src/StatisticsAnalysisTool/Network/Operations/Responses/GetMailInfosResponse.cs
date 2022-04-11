﻿using log4net;
using StatisticsAnalysisTool.Common;
using StatisticsAnalysisTool.Models;
using StatisticsAnalysisTool.Network.Manager;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using StatisticsAnalysisTool.Models.NetworkModel;

namespace StatisticsAnalysisTool.Network.Operations.Responses
{
    public class GetMailInfosResponse
    {
        private static readonly ILog Log = LogManager.GetLogger(MethodBase.GetCurrentMethod()?.DeclaringType);

        public List<MailInfoObject> MailInfos = new();

        public GetMailInfosResponse(Dictionary<byte, object> parameters)
        {
            ConsoleManager.WriteLineForNetworkHandler(GetType().Name, parameters);

            MailInfos.Clear();

            try
            {
                if (parameters.ContainsKey(3) && parameters[3] != null && parameters.ContainsKey(6) && parameters[6] != null && parameters.ContainsKey(10) && parameters[10] != null)
                {
                    var mailIdArray = ((long[])parameters[3]).ToArray();
                    var clusterIndexArray = ((string[])parameters[6]).ToArray();
                    var mailTypeArray = ((string[])parameters[10]).ToArray();

                    var length = Utilities.GetHighestLength(mailIdArray, clusterIndexArray, mailTypeArray);

                    for (var i = 0; i < length; i++)
                    {
                        var mailId = mailIdArray[i];
                        var clusterIndex = mailTypeArray[i];
                        var mailType = mailTypeArray[i];

                        MailInfos.Add(new MailInfoObject(mailId, clusterIndex, MailController.ConvertToMailType(mailType)));
                    }
                }
            }
            catch (Exception e)
            {
                Log.Error(nameof(GetMailInfosResponse), e);
            }
        }
    }
}