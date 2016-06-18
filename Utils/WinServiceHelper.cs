using System;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Security.Principal;
using System.ServiceProcess;
using System.Threading.Tasks;
using eLib.Exceptions;
using NetFwTypeLib;

namespace eLib.Utils
{
    public static class WinServiceHelper
    {
        public static async void Start(string serviceName, string servicePath, string listeningPort, Action<EventArgs> onStartHandler)
        {
            await Task.Run(() =>
           {
               try
               {
                   if (!IsElevated())
                   {
                       onStartHandler.Invoke(new ErrorEventArgs(new CoolException("Please restart application as administrator")));
                       return;
                   }

                   if (ServiceController.GetServices().All(s => s.ServiceName.Equals(serviceName, StringComparison.InvariantCultureIgnoreCase)))
                   {
                       using (var controller = new ServiceController(serviceName))
                       {
                           controller.Start();
                           controller.WaitForStatus(ServiceControllerStatus.Running, TimeSpan.FromSeconds(60));

                           OpenPort(listeningPort, args =>
                           {
                               AddFirewallExeption(servicePath + ".exe", Path.GetFileName(servicePath));

                               onStartHandler.Invoke(EventArgs.Empty);
                           });                           
                           return;
                       }
                   }
                   
                   InstallService(servicePath, args =>
                   {
                       try
                       {
                           using (var controller = new ServiceController(serviceName))
                           {
                               if (controller.Status == ServiceControllerStatus.Stopped)
                               {
                                   controller.Start();
                                   controller.WaitForStatus(ServiceControllerStatus.Running, TimeSpan.FromSeconds(60));
                               }

                               OpenPort(listeningPort, eventArgs =>
                               {
                                   AddFirewallExeption(servicePath + ".exe", Path.GetFileName(servicePath));

                                   onStartHandler.Invoke(EventArgs.Empty);
                               });
                           }
                       }
                       catch (Exception exception)
                       {
                           exception.Log();
                           onStartHandler.Invoke(new ErrorEventArgs(exception));
                       }
                   });                   
               }
               catch (Exception exception)
               {                  
                   exception.Log();
                   onStartHandler.Invoke( new ErrorEventArgs(exception));
               }
           });
        }

        public static async Task<Operation> Stop(string serviceName)
        {
            return await Task.Run(() =>
            {
                try
                {
                    if (!IsElevated())
                        return Operation.Failed("Please restart application as administrator");

                    using (var controller = new ServiceController(serviceName))
                    {
                        controller.Stop();
                        controller.WaitForStatus(ServiceControllerStatus.Stopped, TimeSpan.FromSeconds(60));
                        return Operation.Succes();
                    }
                }
                catch (Exception exception)
                {
                    exception.Log();
                    return Operation.Failed(exception.AsMessage());
                }
            });
        }

        public static Operation ServiceStatus(string serviceName)
        {
            try
            {              
                using (var controller = new ServiceController(serviceName))
                {
                    switch (controller.Status)
                    {
                        case ServiceControllerStatus.Running:
                            return Operation.Succes(ServiceControllerStatus.Running.Description());

                        case ServiceControllerStatus.Stopped:
                            return Operation.Failed(ServiceControllerStatus.Stopped.Description());
                    
                        case ServiceControllerStatus.Paused:
                            return Operation.Failed(ServiceControllerStatus.Paused.Description());
                  
                        case ServiceControllerStatus.StopPending:
                            return Operation.Failed(ServiceControllerStatus.StopPending.Description());
                   
                        case ServiceControllerStatus.StartPending:
                            return Operation.Failed(ServiceControllerStatus.StartPending.Description());
                    
                        case ServiceControllerStatus.ContinuePending:
                            return Operation.Failed(ServiceControllerStatus.ContinuePending.Description());
                   
                        case ServiceControllerStatus.PausePending:
                            return Operation.Failed(ServiceControllerStatus.PausePending.Description());
                   
                        default:
                            return Operation.Failed(ServiceControllerStatus.Stopped.Description());
                     }
                }
            }
            catch (Exception exception)
            {
                exception.Log();
                return Operation.Failed(ServiceControllerStatus.Stopped.Description());
            }
        }

        public static void OpenPort(string port, Action<EventArgs> onPortOpened)
        {
            //NT AUTHORITY\NETWORK SERVICE SYSTEM Everyone

            var args = string.Format(
                @"netsh http add urlacl url=http://*:{0}/ user=""SYSTEM"" listen=yes delegate=yes", port);
            RunCmd("cmd.exe" , args, onPortOpened);
        }

        public static void ClosePort(string port, Action<EventArgs> onPortClosed)
        {
            var args = string.Format(
                @"netsh advfirewall firewall delete rule name=rule name protocol=TCP localport={0}", port);
            RunCmd("cmd.exe", args, onPortClosed);
        }

        public static void AddFirewallExeption(string appExePath, string appName)
        {
            //var args1 = @"netsh firewall add allowedprogram " + appExePath + " " + appName + @" ENABLE";

            //var args2 = @"netsh advfirewall firewall add rule name=""" + appName + @""" dir=in program=""" + appExePath +
            //            @""" security=authnoencap action=allow";

            //var args3 = @"netsh advfirewall firewall add rule name=""" + appName + @""" dir=in action=allow program=""" + appExePath +
            //            @""" enable=yes";

            

            //await RunCmd("cmd.exe", args1);
            //await RunCmd("cmd.exe", args2);

            // await RunCmd("cmd.exe", args3);

            //FirewallHelper.Instance.GrantAuthorization(appExePath, appName);
            FirewallHelper.AddFirewallRule(appExePath, appName, NET_FW_RULE_DIRECTION_.NET_FW_RULE_DIR_IN, NET_FW_ACTION_.NET_FW_ACTION_ALLOW, true);
            FirewallHelper.AddFirewallRule(appExePath, appName, NET_FW_RULE_DIRECTION_.NET_FW_RULE_DIR_OUT, NET_FW_ACTION_.NET_FW_ACTION_ALLOW, true);
        }

        public static void RemoveFirewallExeption(string appExePath, string appName)
        {
            //FirewallHelper.Instance.RemoveAuthorization(appExePath);
            FirewallHelper.AddFirewallRule(appExePath, appName,  NET_FW_RULE_DIRECTION_.NET_FW_RULE_DIR_IN, NET_FW_ACTION_.NET_FW_ACTION_BLOCK, false);
            FirewallHelper.AddFirewallRule(appExePath, appName, NET_FW_RULE_DIRECTION_.NET_FW_RULE_DIR_OUT, NET_FW_ACTION_.NET_FW_ACTION_BLOCK, false);
        }

        //public static async Task<Operation> StartService(string path, string args = default(string)) =>await RunCmd(path, string.IsNullOrEmpty(args) ? "start" : args);

        public static void InstallService(string path, Action<EventArgs> onComplete) => RunCmd(path, "Install", args => RunCmd(path, "start", onComplete));

        public static void UnInstallService(string path, Action<EventArgs> onComplete)
        {
            path = path + ".exe";
            RemoveFirewallExeption(path, Path.GetFileName(path));
            RunCmd(path, "uninstall", onComplete);
        }

        public static async void RunCmd(string path, string args, Action<EventArgs> onComplete)
        {
            await Task.Run(() =>
            { 
                try
                {
                    if (!IsElevated())
                    {
                        onComplete.Invoke(new ErrorEventArgs(new CoolException("Please restart application as administrator")));
                        return;
                    }

                    var process = new Process
                    {
                        StartInfo = new ProcessStartInfo
                        {
                            WindowStyle = ProcessWindowStyle.Hidden,
                            FileName = path,
                            Arguments = args,
                            UseShellExecute = false,
                            Verb = "runas",
                            RedirectStandardInput = true,
                            RedirectStandardOutput = true,
                            CreateNoWindow = true
                        }
                    };

                    process.Exited += (sender, eventArgs) => onComplete.Invoke(EventArgs.Empty);

                    process.Start();
                    process.WaitForExit(3 * 1000);
                    
                    onComplete.Invoke(EventArgs.Empty);
                }
                catch (Exception exception)
                {
                    exception.Log();
                    onComplete.Invoke(new ErrorEventArgs(exception));
                }
            });
        }

        public static bool IsElevated()
        {
            var identity = WindowsIdentity.GetCurrent();
            return identity != null && new WindowsPrincipal
                (identity).IsInRole
                (WindowsBuiltInRole.Administrator);
        }
        
    }
}
