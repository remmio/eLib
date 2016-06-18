using System;
using System.Collections;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.InteropServices;
using NetFwTypeLib;

namespace eLib.Utils
{
    /// 

    /// Allows basic access to the windows firewall API.
    /// This can be used to add an exception to the windows firewall
    /// exceptions list, so that our programs can continue to run merrily
    /// even when nasty windows firewall is running.
    ///
    /// Please note: It is not enforced here, but it might be a good idea
    /// to actually prompt the user before messing with their firewall settings,
    /// just as a matter of politeness.
    /// 

    /// 
    /// To allow the installers to authorize idiom products to work through
    /// the Windows Firewall.
    /// 
    public class FirewallHelperAlt1
    {
        #region Variables
        /// 

        /// Hooray! Singleton access.
        /// 

        private static FirewallHelperAlt1 _instance;

        /// 

        /// Interface to the firewall manager COM object
        /// 

        private readonly INetFwMgr _fwMgr;
        #endregion
        #region Properties
        /// 

        /// Singleton access to the firewallhelper object.
        /// Threadsafe.
        /// 

        public static FirewallHelperAlt1 Instance
        {
            get
            {
                lock (typeof (FirewallHelperAlt1))
                    return _instance ?? (_instance = new FirewallHelperAlt1());
            }
        }
        #endregion
        #region Constructivat0r
        /// 

        /// Private Constructor.  If this fails, HasFirewall will return
        /// false;
        /// 

        private FirewallHelperAlt1()
        {
            // Get the type of HNetCfg.FwMgr, or null if an error occurred
            var fwMgrType = Type.GetTypeFromProgID("HNetCfg.FwMgr", false);

            // Assume failed.
            _fwMgr = null;

            if (fwMgrType == null) return;
            try
            {
                _fwMgr = (INetFwMgr)Activator.CreateInstance(fwMgrType);
            }
             // In all other circumnstances, fwMgr is null.
            catch (ArgumentException) { }
            catch (NotSupportedException) { }
            catch (TargetInvocationException) { }
            catch (MissingMethodException) { }
            catch (MethodAccessException) { }
            catch (MemberAccessException) { }
            catch (InvalidComObjectException) { }
            catch (COMException) { }
            catch (TypeLoadException) { }
        }
        #endregion
        #region Helper Methods
        /// 

        /// Gets whether or not the firewall is installed on this computer.       
        public bool IsFirewallInstalled
        {
            get
            {
                if (_fwMgr != null &&
                      _fwMgr.LocalPolicy != null &&
                      _fwMgr.LocalPolicy.CurrentProfile != null)
                    return true;
                return false;
            }
        }

        /// 

        /// Returns whether or not the firewall is enabled.
        /// If the firewall is not installed, this returns false.
        /// 

        public bool IsFirewallEnabled
        {
            get
            {
                if (IsFirewallInstalled && _fwMgr.LocalPolicy.CurrentProfile.FirewallEnabled)
                    return true;
                return false;
            }
        }

        /// 

        /// Returns whether or not the firewall allows Application "Exceptions".
        /// If the firewall is not installed, this returns false.
        /// 

        /// 
        /// Added to allow access to this metho
        /// 
        public bool AppAuthorizationsAllowed => IsFirewallInstalled && !_fwMgr.LocalPolicy.CurrentProfile.ExceptionsNotAllowed;

        /// 

        /// Adds an application to the list of authorized applications.
        /// If the application is already authorized, does nothing.
        /// 

        /// 
        ///         The full path to the application executable.  This cannot
        ///         be blank, and cannot be a relative path.
        /// 
        /// 
        ///         This is the name of the application, purely for display
        ///         puposes in the Microsoft Security Center.
        /// 
        /// 
        ///         When applicationFullPath is null OR
        ///         When appName is null.
        /// 
        /// 
        ///         When applicationFullPath is blank OR
        ///         When appName is blank OR
        ///         applicationFullPath contains invalid path characters OR
        ///         applicationFullPath is not an absolute path
        /// 
        /// 
        ///         If the firewall is not installed OR
        ///         If the firewall does not allow specific application 'exceptions' OR
        ///         Due to an exception in COM this method could not create the
        ///         necessary COM types
        /// 
        /// 
        ///         If no file exists at the given applicationFullPath
        /// 
        public void GrantAuthorization(string applicationFullPath, string appName)
        {
            #region  Parameter checking
            if (applicationFullPath == null)
                throw new ArgumentNullException(nameof(applicationFullPath));
            if (appName == null)
                throw new ArgumentNullException(nameof(appName));
            if (applicationFullPath.Trim().Length == 0)
                throw new ArgumentException("applicationFullPath must not be blank");
            if (applicationFullPath.Trim().Length == 0)
                throw new ArgumentException("appName must not be blank");
            if (applicationFullPath.IndexOfAny(Path.GetInvalidPathChars()) >= 0)
                throw new ArgumentException("applicationFullPath must not contain invalid path characters");
            if (!Path.IsPathRooted(applicationFullPath))
                throw new ArgumentException("applicationFullPath is not an absolute path");
            if (!File.Exists(applicationFullPath))
                throw new FileNotFoundException("File does not exist", applicationFullPath);
            // State checking
            if (!IsFirewallInstalled)
                throw new FirewallHelperException("Cannot grant authorization: Firewall is not installed.");
            if (!AppAuthorizationsAllowed)
                throw new FirewallHelperException("Application exemptions are not allowed.");
            #endregion

            if (HasAuthorization(applicationFullPath)) return;
            // Get the type of HNetCfg.FwMgr, or null if an error occurred
            var authAppType = Type.GetTypeFromProgID("HNetCfg.FwAuthorizedApplication", false);

            // Assume failed.
            INetFwAuthorizedApplication appInfo = null;

            if (authAppType != null)
            {
                try
                {
                    appInfo = (INetFwAuthorizedApplication)Activator.CreateInstance(authAppType);
                }
                    // In all other circumnstances, appInfo is null.
                catch (ArgumentException) { }
                catch (NotSupportedException) { }
                catch (TargetInvocationException) { }
                catch (MissingMethodException) { }
                catch (MethodAccessException) { }
                catch (MemberAccessException) { }
                catch (InvalidComObjectException) { }
                catch (COMException) { }
                catch (TypeLoadException) { }
            }

            if (appInfo == null)
                throw new FirewallHelperException("Could not grant authorization: can't create INetFwAuthorizedApplication instance.");

            appInfo.Name = appName;
            appInfo.ProcessImageFileName = applicationFullPath;
            // ...
            // Use defaults for other properties of the AuthorizedApplication COM object

            // Authorize this application
            _fwMgr.LocalPolicy.CurrentProfile.AuthorizedApplications.Add(appInfo);
            // otherwise it already has authorization so do nothing
        }
        /// 

        /// Removes an application to the list of authorized applications.
        /// Note that the specified application must exist or a FileNotFound
        /// exception will be thrown.
        /// If the specified application exists but does not current have
        /// authorization, this method will do nothing.
        /// 

        /// 
        ///         The full path to the application executable.  This cannot
        ///         be blank, and cannot be a relative path.
        /// 
        /// 
        ///         When applicationFullPath is null
        /// 
        /// 
        ///         When applicationFullPath is blank OR
        ///         applicationFullPath contains invalid path characters OR
        ///         applicationFullPath is not an absolute path
        /// 
        /// 
        ///         If the firewall is not installed.
        /// 
        /// 
        ///         If the specified application does not exist.
        /// 
        public void RemoveAuthorization(string applicationFullPath)
        {

            #region  Parameter checking
            if (applicationFullPath == null)
                throw new ArgumentNullException(nameof(applicationFullPath));
            if (applicationFullPath.Trim().Length == 0)
                throw new ArgumentException("applicationFullPath must not be blank");
            if (applicationFullPath.IndexOfAny(Path.GetInvalidPathChars()) >= 0)
                throw new ArgumentException("applicationFullPath must not contain invalid path characters");
            if (!Path.IsPathRooted(applicationFullPath))
                throw new ArgumentException("applicationFullPath is not an absolute path");
            if (!File.Exists(applicationFullPath))
                throw new FileNotFoundException("File does not exist", applicationFullPath);
            // State checking
            if (!IsFirewallInstalled)
                throw new FirewallHelperException("Cannot remove authorization: Firewall is not installed.");
            #endregion

            if (HasAuthorization(applicationFullPath))
                _fwMgr.LocalPolicy.CurrentProfile.AuthorizedApplications.Remove(applicationFullPath);
        }
        /// 

        /// Returns whether an application is in the list of authorized applications.
        /// Note if the file does not exist, this throws a FileNotFound exception.
        /// 

        /// 
        ///         The full path to the application executable.  This cannot
        ///         be blank, and cannot be a relative path.
        /// 
        /// 
        ///         The full path to the application executable.  This cannot
        ///         be blank, and cannot be a relative path.
        /// 
        /// 
        ///         When applicationFullPath is null
        /// 
        /// 
        ///         When applicationFullPath is blank OR
        ///         applicationFullPath contains invalid path characters OR
        ///         applicationFullPath is not an absolute path
        /// 
        /// 
        ///         If the firewall is not installed.
        /// 
        /// 
        ///         If the specified application does not exist.
        /// 
        public bool HasAuthorization(string applicationFullPath)
        {
            #region  Parameter checking
            if (applicationFullPath == null)
                throw new ArgumentNullException(nameof(applicationFullPath));
            if (applicationFullPath.Trim().Length == 0)
                throw new ArgumentException("applicationFullPath must not be blank");
            if (applicationFullPath.IndexOfAny(Path.GetInvalidPathChars()) >= 0)
                throw new ArgumentException("applicationFullPath must not contain invalid path characters");
            if (!Path.IsPathRooted(applicationFullPath))
                throw new ArgumentException("applicationFullPath is not an absolute path");
            if (!File.Exists(applicationFullPath))
                throw new FileNotFoundException("File does not exist.", applicationFullPath);
            // State checking
            if (!IsFirewallInstalled)
                throw new FirewallHelperException("Cannot remove authorization: Firewall is not installed.");

            #endregion

            // Locate Authorization for this application
            return GetAuthorizedAppPaths().Cast<string>().Any(appName => appName.ToLower() == applicationFullPath.ToLower());

            // Failed to locate the given app.
        }

        /// Retrieves a collection of paths to applications that are authorized.
        /// 

        /// 
        /// 
        ///         If the Firewall is not installed.
        ///   
        public ICollection GetAuthorizedAppPaths()
        {
            // State checking
            if (!IsFirewallInstalled)
                throw new FirewallHelperException("Cannot remove authorization: Firewall is not installed.");

            var list = new ArrayList();
            //  Collect the paths of all authorized applications
            foreach (INetFwAuthorizedApplication app in _fwMgr.LocalPolicy.CurrentProfile.AuthorizedApplications)
                list.Add(app.ProcessImageFileName);

            return list;
        }
        #endregion
    }

    /// 

    /// Describes a FirewallHelperException.
    /// 

    /// 
    ///
    /// 
    public class FirewallHelperException : Exception
    {
        /// 

        /// Construct a new FirewallHelperException
        /// 

        /// 
        public FirewallHelperException(string message)
          : base(message)
        { }
    }













    
    public class FirewallHelperAlt
    {
            
        private readonly int[] _portsSocket = { 777, 3306 };
        private readonly string[] _portsName = { "AsyncPort", "MySqlPort" };
        private INetFwProfile _fwProfile;

        protected internal void OpenFirewall(string appPath, string appName)
        {
            try
            {
                if (IsAppFound(appName) == false)
                {
                    SetProfile();
                    var authApps = _fwProfile.AuthorizedApplications;
                    var authApp = GetInstance("INetAuthApp") as INetFwAuthorizedApplication;
                    if (authApp != null)
                    {
                        authApp.Name = appName;
                        authApp.ProcessImageFileName = appPath;
                        authApps.Add(authApp);
                    }
                }

                INetFwOpenPorts openPorts;
                INetFwOpenPort openPort;
                if (IsPortFound(_portsSocket[0]) == false)
                {
                    SetProfile();
                    openPorts = _fwProfile.GloballyOpenPorts;
                    openPort = GetInstance("INetOpenPort") as INetFwOpenPort;
                    if (openPort != null)
                    {
                        openPort.Port = _portsSocket[0];
                        openPort.Protocol = NET_FW_IP_PROTOCOL_.NET_FW_IP_PROTOCOL_TCP;
                        openPort.Name = _portsName[0];
                        openPorts.Add(openPort);
                    }
                }

                if (IsPortFound(_portsSocket[1])) return;
                SetProfile();
                openPorts = _fwProfile.GloballyOpenPorts;
                openPort = GetInstance("INetOpenPort") as INetFwOpenPort;
                if (openPort == null) return;
                openPort.Port = _portsSocket[1];
                openPort.Protocol = NET_FW_IP_PROTOCOL_.NET_FW_IP_PROTOCOL_TCP;
                openPort.Name = _portsName[1];
                openPorts.Add(openPort);
            }
            catch (Exception ex)
            {
                ex.Log();
            }
                
        }

        protected internal void CloseFirewall(string appPath, string appName)
        {
            try
            {
                if (IsAppFound(appName))
                {
                    SetProfile();
                    var apps = _fwProfile.AuthorizedApplications;
                    apps.Remove(appPath);
                }

                INetFwOpenPorts ports;
                if (IsPortFound(_portsSocket[0]))
                {
                    SetProfile();
                    ports = _fwProfile.GloballyOpenPorts;
                    ports.Remove(_portsSocket[0], NET_FW_IP_PROTOCOL_.NET_FW_IP_PROTOCOL_TCP);
                }

                if (IsPortFound(_portsSocket[1]) != true) return;
                SetProfile();
                ports = _fwProfile.GloballyOpenPorts;
                ports.Remove(_portsSocket[1], NET_FW_IP_PROTOCOL_.NET_FW_IP_PROTOCOL_TCP);
            }
            catch (Exception ex)
            {
                ex.Log();
            }
                
        }

        protected internal bool IsAppFound(string appName)
        {
            var boolResult = false;
            try
            {
                var progId = Type.GetTypeFromProgID("HNetCfg.FwMgr");
                var firewall = Activator.CreateInstance(progId) as INetFwMgr;
                if (firewall != null && firewall.LocalPolicy.CurrentProfile.FirewallEnabled)
                {
                    var apps = firewall.LocalPolicy.CurrentProfile.AuthorizedApplications;
                    var appEnumerate = apps.GetEnumerator();
                    while ((appEnumerate.MoveNext()))
                    {
                        var app = appEnumerate.Current as INetFwAuthorizedApplication;
                        if (app == null || app.Name != appName) continue;
                        boolResult = true;
                        break;
                    }
                }
            }
            catch (Exception ex)
            {
                ex.Log();
            }
               
            return boolResult;
        }

        protected internal bool IsPortFound(int portNumber)
        {
            var boolResult = false;
            try
            {
                var progId = Type.GetTypeFromProgID("HNetCfg.FwMgr");
                var firewall = Activator.CreateInstance(progId) as INetFwMgr;
                var ports = firewall?.LocalPolicy.CurrentProfile.GloballyOpenPorts;
                var portEnumerate = ports?.GetEnumerator();
                while (portEnumerate != null && portEnumerate.MoveNext())
                {
                    var currentPort = portEnumerate.Current as INetFwOpenPort;
                    if (currentPort != null && currentPort.Port != portNumber) continue;
                    boolResult = true;
                    break;
                }
            }
            catch (Exception ex)
            {
                ex.Log();
                return false;
            }
                
            return boolResult;
        }

        protected internal void SetProfile()
        {
            try
            {
                var fwMgr = GetInstance("INetFwMgr") as INetFwMgr;
                var fwPolicy = fwMgr?.LocalPolicy;
                _fwProfile = fwPolicy?.CurrentProfile;
            }
            catch (Exception ex)
            {
                ex.Log();
            }
               
        }

        protected internal object GetInstance(string typeName)
        {
            Type tpResult;
            switch (typeName)
            {
                case "INetFwMgr":
                    tpResult = Type.GetTypeFromCLSID(new Guid("{304CE942-6E39-40D8-943A-B913C40C9CD4}"));
                    return Activator.CreateInstance(tpResult);
                case "INetAuthApp":
                    tpResult = Type.GetTypeFromCLSID(new Guid("{EC9846B3-2762-4A6B-A214-6ACB603462D2}"));
                    return Activator.CreateInstance(tpResult);
                case "INetOpenPort":
                    tpResult = Type.GetTypeFromCLSID(new Guid("{0CA545C6-37AD-4A6C-BF92-9F7610067EF5}"));
                    return Activator.CreateInstance(tpResult);
                default:
                    return null;
            }
        }

    }
    
}
