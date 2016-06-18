//using PostSharp.Aspects;
//using PostSharp.Extensibility;

namespace eLib.AOP
{
   
    //[Serializable]
    ////[MulticastAttributeUsage(MulticastTargets.Method)]
    //public class Timing //: OnMethodBoundaryAspect
    //{
    //    [NonSerialized]
    //    Stopwatch _stopWatch;
    //    [NonSerialized]
    //    private readonly string _origine;

    //    public Timing(string origine = default(string))
    //    {
    //        _origine = origine;
    //    }

    //    public override void OnEntry(MethodExecutionArgs args)
    //    {
    //        _stopWatch = Stopwatch.StartNew();

    //        base.OnEntry(args);
    //    }

    //    public override void OnExit(MethodExecutionArgs args)
    //    {
    //        if (string.IsNullOrEmpty(_origine))
    //        {
    //            DebugHelper.WriteLine($"[{new StackTrace().GetFrame(1).GetMethod().Name}] took {_stopWatch.ElapsedMilliseconds}ms to execute");

    //            Debug.WriteLine(
    //                $"[{new StackTrace().GetFrame(1).GetMethod().Name}] took {_stopWatch.ElapsedMilliseconds}ms to execute");
    //        }
    //        else
    //        {
    //            DebugHelper.WriteLine($"[{_origine}] took {_stopWatch.ElapsedMilliseconds}ms to execute");

    //            Debug.WriteLine(
    //                $"[{_origine}] took {_stopWatch.ElapsedMilliseconds}ms to execute");
    //        }
            

    //        base.OnExit(args);
    //    }
    //}
}
