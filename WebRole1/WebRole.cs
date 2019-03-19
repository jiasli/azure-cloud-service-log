using Microsoft.WindowsAzure.ServiceRuntime;
using System.Threading;
using TableLog;

namespace WebRole1
{
    public class WebRole : RoleEntryPoint
    {
        public override bool OnStart()
        {
            Log.Trace("onStart() enter");

            int sleepTime = 5000;
            Log.Trace("Sleeping " + sleepTime);
            Thread.Sleep(sleepTime);

            Log.Trace("onStart() exit");
            return base.OnStart();
        }

        public override void Run()
        {
            Log.Trace("Run() enter");

            while (true)
            {
                int sleepTime = 5000;
                Log.Trace("Sleeping " + sleepTime);
                Thread.Sleep(sleepTime);
            }
        }

        public override void OnStop()
        {
            Log.Trace("OnStop() enter");
            Log.Trace("OnStop() exit");
            base.OnStop();
        }
    }
}
