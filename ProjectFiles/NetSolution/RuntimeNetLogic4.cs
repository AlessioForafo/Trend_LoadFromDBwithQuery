#region Using directives
using System;
using UAManagedCore;
using OpcUa = UAManagedCore.OpcUa;
using FTOptix.SQLiteStore;
using FTOptix.UI;
using FTOptix.DataLogger;
using FTOptix.HMIProject;
using FTOptix.NetLogic;
using FTOptix.NativeUI;
using FTOptix.Store;
using FTOptix.OPCUAClient;
using FTOptix.CODESYS;
using FTOptix.Retentivity;
using FTOptix.CoreBase;
using FTOptix.CommunicationDriver;
using FTOptix.Core;
#endregion

public class RuntimeNetLogic4 : BaseNetLogic
{
    private PeriodicTask myPeriodicTask;

    public override void Start()
    {
        // Insert code to be executed when the user-defined logic is started
        myPeriodicTask = new PeriodicTask(AggiornaTrend, 10000, LogicObject);
        myPeriodicTask.Start();
        AggiornaTrend();
    }

    public override void Stop()
    {
        // Insert code to be executed when the user-defined logic is stopped
        myPeriodicTask.Dispose();
    }

    [ExportMethod]
    public void AggiornaTrend()
    {
        var myTrend = (Trend)Owner;
        myTrend.Query = "SELECT * FROM DataLogger1 WHERE LocalTimestamp BETWEEN \"" + DateTime.Now.AddHours(-1).ToString("yyyy-MM-dd'T'HH:mm:ss.fffffff") + "\" AND \"" +
                         DateTime.Now.ToString("yyyy-MM-dd'T'HH:mm:ss.fffffff") + "\"";
        
        //serve per riposizionare il trend
        myTrend.XAxis.Time = DateTime.Now;
    }
}
