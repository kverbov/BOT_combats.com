/*
 * Created by Ranorex
 * User: kverbov
 * Date: 03.12.2014
 * Time: 12:02
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;
using System.Drawing;
using System.Threading;
using WinForms = System.Windows.Forms;

using Ranorex;
using Ranorex.Core;
using Ranorex.Core.Testing;

namespace Test
{
	/// <summary>
	/// Description of Func.
	/// </summary>
	public static class Func
	{
		/// <summary>
        /// Holds an instance of the TestRepository repository.
        /// </summary>
        public static TestRepository repo = TestRepository.Instance;
		
		/// <summary>
		/// следующий шаг или конец боя
		/// </summary>
		/// <param name="min"></param>
		/// <param name="max"></param>
		/// <returns>следующий шаг</returns>
		public static bool WaitNextStep(Duration min,  Duration max)
        {
        	while(!Validate_Вперёд())
        	{
        		//	если видна кнопка "Обновить"
        		if(repo.БойцовскийКлуб1.Menu.ОбновитьInfo.Exists() && repo.БойцовскийКлуб1.Menu.Обновить.Visible)
        		{
            		Report.Log(ReportLevel.Info, "Keyboard",  "Процесс боя. Жмем кнопку 'Обновить'.");
            		Func.ClickElement(repo.БойцовскийКлуб1.Menu.Обновить);
            		Wait(min,max);
            		continue;
            	}         
        		else if(repo.БойцовскийКлуб1.Menu.ВернутьсяInfo.Exists() && repo.БойцовскийКлуб1.Menu.Вернуться.Visible)
            	{
            		Report.Log(ReportLevel.Info, "Keyboard", "Процесс боя. Конец боя. Нажатие кнопки 'Вернуться'.");
            		Func.ClickElement(repo.БойцовскийКлуб1.Menu.Вернуться);
            		return false;
            	}   
        		else if(repo.БойцовскийКлуб1.Menu.Combats1Info.Exists() && repo.БойцовскийКлуб1.Menu.Combats1.Visible)
        		{
        			Report.Log(ReportLevel.Info, "Keyboard", "Процесс боя. Появилась кнопка 'Поединки'. Конец боя.");
        			return false;

        		}
        		else 
            	{
        			Keyboard.Press("{F5}");
        			Report.Log(ReportLevel.Info, "Keyboard", "Процесс боя. Ничего не появилось. Жмем 'F5'.");
        			continue;
        			
            		
//            		return false;
            	}
        	}
        	
        	//	появилась кнопка "Вперед"
        	return true;
        	      
        }
        
		/// <summary>
		/// Ждем
		/// </summary>
		/// <param name="min"></param>
		/// <param name="max"></param>
        public static void Wait(Ranorex.Duration min, Ranorex.Duration max)
        {
        	Random R=new Random();
        	int time=R.Next(min,max);
        	Report.Log(ReportLevel.Info, "Keyboard", "Ждем в течение "+time.ToString());
        	Thread.Sleep(time);        	
        	
        }

        public static bool ClickElement(WebElement item)
        {
        	if(item.EnsureVisible() && item.Valid)
        	{
        		Report.Log(ReportLevel.Info, "Клик на элемент "+item.ToString() +". Элемент найден. Делаем клик.");
        		Validate.Exists(item);
        		item.PerformClick();// Mouse.Click( item);
        		Delay.Seconds(2);
        		return true;
        	}
        	else
        	{
        		Report.Log(ReportLevel.Info, "Клик на элемент "+item.ToString() +". Элемент не найден!");
        		return false;
        	}
        	
        }
        
        public static bool WaitElementAppear(Element item)
        {
        	if(item.EnsureVisible() && item.Valid && item.ScreenRectangle.Size.Width!=0)
        	{
        		Report.Log(ReportLevel.Info, "Проверка видимости элемента "+item.ToString() +". Элемент найден.");
        		return true;
        	}
        	else
        	{
        		Report.Log(ReportLevel.Info, "Проверка видимости элемента "+item.ToString() +". Элемент не найден!");
        		return false;
        	}
        	
        }
        
        public static bool Validate_Вперёд()
        {
        	
        	if(repo.БойцовскийКлуб1.Menu.ВперёдInfo.Exists() &&repo.БойцовскийКлуб1.Menu.Вперёд.Visible&& repo.БойцовскийКлуб1.Menu.Вперёд.ScreenRectangle.Size.Width!=0)        	
            {
        		Report.Log(ReportLevel.Info, "Keyboard", "Кнопка 'Вперед' появилась.");
            	return true;
            }
            else
            {
            	Report.Log(ReportLevel.Info, "Keyboard", "Кнопка 'Вперед' не появилась.");
            	return false;
            	
            }            	
        }
	}
}
