using System;
using NetFwTypeLib;

namespace eLib.Utils
{
    public static class FirewallHelper
    {
        /// <summary>
        /// Adds or removes a firewall rule.
        /// </summary>
        /// <param name="path">The path to the executable.</param>
        /// <param name="name">Name of the application</param>
        /// <param name="direction">The affected connection type.</param>
        /// <param name="fwaction">Rule action.</param>
        /// <param name="action">"Add or remove the specified rule."</param>
        public static void AddFirewallRule(string path, string name, NET_FW_RULE_DIRECTION_ direction, NET_FW_ACTION_ fwaction, bool action)
        {
            try
            {
                var firewallRule = (INetFwRule)Activator.CreateInstance(
                Type.GetTypeFromProgID("HNetCfg.FWRule"));
                firewallRule.Action = fwaction;
                firewallRule.Enabled = true;
                firewallRule.InterfaceTypes = "All";
                firewallRule.ApplicationName = path;
                firewallRule.Name = name;
                var firewallPolicy = (INetFwPolicy2)Activator.CreateInstance
                (Type.GetTypeFromProgID("HNetCfg.FwPolicy2"));
                firewallRule.Direction = direction;
                if (action) firewallPolicy.Rules.Add(firewallRule);
                else firewallPolicy.Rules.Remove(firewallRule.Name);
            }
            catch (Exception ex) { ex.Log(); }
        }

        public static void OpenPort(int port, string name)
        {
            try
            {
                var ticfMgr = Type.GetTypeFromProgID("HNetCfg.FwMgr");
                var icfMgr = (INetFwMgr)Activator.CreateInstance(ticfMgr);

                var tportClass = Type.GetTypeFromProgID("HNetCfg.FWOpenPort");
                var portClass = (INetFwOpenPort)Activator.CreateInstance(tportClass);

                // Get the current profile
                var profile = icfMgr.LocalPolicy.CurrentProfile;

                // Set the port properties
                portClass.Scope = NET_FW_SCOPE_.NET_FW_SCOPE_ALL;
                portClass.Enabled = true;
                portClass.Protocol = NET_FW_IP_PROTOCOL_.NET_FW_IP_PROTOCOL_TCP;
                portClass.Name = name;
                portClass.Port = port;

                // Add the port to the ICF Permissions List
                profile.GloballyOpenPorts.Add(portClass);
            }
            catch (Exception ex)
            {
                ex.Log();
            }
        }
    }
}

