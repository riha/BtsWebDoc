﻿using System.Collections.Generic;
using System.Linq;
using System.Xml;
using btswebdoc.CmdClient.Extensions;
using btswebdoc.Model;

namespace btswebdoc.CmdClient.ModelTransformers
{
    class SendPortGroupModelTransformer
    {
        internal static SendPortGroup TransformModel(Microsoft.BizTalk.ExplorerOM.SendPortGroup omSendPortGroup)
        {
            var sendPortGroup = new SendPortGroup();

            sendPortGroup.Name = omSendPortGroup.Name;
            sendPortGroup.Description = omSendPortGroup.Description;
            sendPortGroup.FilterGroups = FilterGroupTransformer.CreateFilterGroups(omSendPortGroup.Filter);

            return sendPortGroup;
        }

        internal static void SetReferences(SendPortGroup sendPortGroup, BizTalkArtifacts artifacts, Microsoft.BizTalk.ExplorerOM.SendPortGroup omSendPortGroup)
        {
            sendPortGroup.Application = artifacts.Applications[omSendPortGroup.Application.Id()];
            //sendPortGroup.SendPorts.AddRange(artifacts.SendPorts.Where(s => s.Key ==  );
            //sendPortGroup.SendPorts = artifacts.SendPortGroups[omSendPortGroup.Id()];
        }
    }
}
