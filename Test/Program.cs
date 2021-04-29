/*
 * Created by Ranorex
 * User: kverbov
 * Date: 01.12.2014
 * Time: 15:49
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */

using System;
using System.Threading;
using System.Drawing;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using WinForms = System.Windows.Forms;

using Ranorex;
using Ranorex.Core;
using Ranorex.Core.Reporting;
using Ranorex.Core.Testing;
using SD=System.Diagnostics;

namespace Test
{
    class Program
    {
        [STAThread]
        public static int Main(string[] args)
        {
            // Uncomment the following 2 lines if you want to automate Windows apps
            // by starting the test executable directly
            //if (Util.IsRestartRequiredForWinAppAccess)
            //    return Util.RestartWithUiAccess();

            Keyboard.AbortKey = System.Windows.Forms.Keys.Pause;
            int error = 0;
			try{
			
			Launch.Start();
			//Launch.Instance.RepairPerson();
			while(true)
            {					
	            Launch.Instance.ChooseFight();
	            System.DateTime start=System.DateTime.Now;
	            
	            //	если групповой бой
	            if(Launch.Instance.fightType!="1")            		
	            {
	            //	если противники не набраны, то бой отменяется автоматически
	            if(!Launch.Instance.WaitGPFight(10000,45000,6*60000))
	            continue;            			
	            }
	            else
	            Launch.Instance.WaitFight(10000,45000);
	            	
	            Fight.Instance.DoFight(false);
	            Report.Log(ReportLevel.Info,"Закрываем браузер.");
	            Host.Local.KillBrowser("ie");
	            Func.Wait(105000,195000);
	            Launch.Start();            		
            }
            				
            error = TestSuiteRunner.Run(typeof(Program), Environment.CommandLine);
            }
            catch (Exception e)
            {
                Report.Error("Unexpected exception occurred: " + e.ToString());
                error = -1;
            }
            return error;
        }
        
        
        
        
    }
}
