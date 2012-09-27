using System.Collections.Generic;
using System.Linq;
using btswebdoc.Shared.Logging;
using Microsoft.BizTalk.ExplorerOM;

namespace btswebdoc.CmdClient
{
    /// <summary>
    /// Uses ExplorerOM to read from the BizTalk configuration db. Will cache the result with the class instance. Returns ExplorerOM types.
    /// </summary>
    class BtsCatalogReader
    {
        private readonly BtsCatalogExplorer _catalog;
        private IEnumerable<Application> _omApplications;
        private IEnumerable<Pipeline> _omPipelines;
        private IEnumerable<ReceivePort> _omReceivePorts;
        private IEnumerable<SendPort> _omSendPorts;
        private IEnumerable<Transform> _omTransforms;
        private IEnumerable<Schema> _omSchemas;
        private IEnumerable<BtsAssembly> _omAssemblies;
        private List<BtsOrchestration> _omOrchestrations;
        private readonly HashSet<string> _excludedApplications;

        public BtsCatalogReader(string server, string database, HashSet<string> excludedApplications)
        {
            var cs = string.Format("Server={0};Database={1};Integrated Security=SSPI", server, database);

            _excludedApplications = excludedApplications;

            Log.Info("Open BtsCatalogExplorer using following connection string {0}", cs);

            _catalog = new BtsCatalogExplorer
                           {
                               ConnectionString = cs
                           };
        }

        public string GroupName
        {
            get { return _catalog.GroupName; }
        }

        public IEnumerable<Application> Applications
        {
            get
            {
                if (_omApplications == null)
                {
                    Log.Info("Reads applications from BizTalk Config");
                    _omApplications = _catalog.Applications.Cast<Application>().Where(a => !_excludedApplications.Contains(a.Name));
                }

                return _omApplications;
            }
        }

        public IEnumerable<Pipeline> Pipelines
        {

            get
            {
                if (_omPipelines == null)
                {
                    Log.Info("Reads pipelines from BizTalk Config");
                    _omPipelines = _catalog.Pipelines.Cast<Pipeline>().Where(p => !_excludedApplications.Contains(p.Application.Name));
                }

                return _omPipelines;
            }
        }

        public IEnumerable<ReceivePort> ReceivePorts
        {

            get
            {
                if (_omReceivePorts == null)
                {
                    Log.Info("Reads receive ports from BizTalk Config");
                    _omReceivePorts = _catalog.ReceivePorts.Cast<ReceivePort>().Where(rp => !_excludedApplications.Contains(rp.Application.Name));
                }

                return _omReceivePorts;
            }
        }

        public IEnumerable<SendPort> SendPorts
        {

            get
            {
                if (_omSendPorts == null)
                {
                    Log.Info("Reads send ports from BizTalk Configuration DB");
                    _omSendPorts = _catalog.SendPorts.Cast<SendPort>().Where(sp => !_excludedApplications.Contains(sp.Application.Name));

                }

                return _omSendPorts;
            }
        }

        public IEnumerable<Transform> Transforms
        {

            get
            {
                if (_omTransforms == null)
                {
                    Log.Info("Reads transforms from BizTalk Configuration DB");
                    _omTransforms = _catalog.Transforms.Cast<Transform>().Where(t => !_excludedApplications.Contains(t.Application.Name));
                }

                return _omTransforms;
            }
        }

        public IEnumerable<Schema> Schemas
        {

            get
            {
                if (_omSchemas == null)
                {
                    Log.Info("Reads schemas from BizTalk Configuration DB");
                    _omSchemas = _catalog.Schemas.Cast<Schema>().Where(s => !_excludedApplications.Contains(s.Application.Name));
                }

                return _omSchemas;
            }
        }

        public IEnumerable<BtsAssembly> Assemblies
        {

            get
            {
                if (_omAssemblies == null)
                {
                    Log.Info("Reads assemblies from BizTalk Configuration DB");
                    _omAssemblies = _catalog.Assemblies.Cast<BtsAssembly>().Where(a => !_excludedApplications.Contains(a.Application.Name));
                }

                return _omAssemblies;
            }
        }

        public IEnumerable<BtsOrchestration> Orchestrations
        {
            get
            {
                if (_omOrchestrations == null)
                {
                    Log.Info("Reads orchestrations from BizTalk Configuration DB");
                    _omOrchestrations = new List<BtsOrchestration>();
                    foreach (Application app in _catalog.Applications.Cast<Application>().Where(a => !_excludedApplications.Contains(a.Name)))
                    {
                        foreach (BtsOrchestration btsOrchestration in app.Orchestrations)
                        {
                            _omOrchestrations.Add(btsOrchestration);
                        }
                    }
                }

                return _omOrchestrations;
            }
        }
    }

}
