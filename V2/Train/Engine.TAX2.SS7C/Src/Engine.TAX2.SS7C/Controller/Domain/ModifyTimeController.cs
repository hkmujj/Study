using System;
using System.ComponentModel.Composition;
using System.Diagnostics;
using System.Linq;
using DevExpress.Mvvm.Native;
using DevExpress.Mvvm.POCO;
using Engine.TAX2.SS7C.Model.Domain.Details;
using Engine.TAX2.SS7C.Model.Interface;
using Engine.TAX2.SS7C.ViewModel.Domain;
using MMI.Facility.WPFInfrastructure.Interfaces;

namespace Engine.TAX2.SS7C.Controller.Domain
{
    public class ModifyTimeController : ControllerBase<OtherViewModel>, IResetSupport
    {

        [DebuggerStepThrough]
        public ModifyTimeController(OtherController parentController)
        {
            ParentController = parentController;
        }

        public OtherController ParentController { get; private set; }

        public void Reset()
        {
        }

        public void ModifyTimeOk()
        {
            ViewModel.Model.AdjustSpan = ViewModel.Model.ModifyTimeModel.SettingTime - ViewModel.Model.SimTime;
        }

        public void ModifyTimeCaretLeft()
        {
            var tm = ViewModel.Model.ModifyTimeModel;
            if (tm.BindableCaretIndex > 1)
            {
                --tm.BindableCaretIndex;
                if (ModifyTimeModel.ConstantText.Contains(tm.CharOfIndex))
                {
                    --tm.BindableCaretIndex;
                }
            }
        }

        public void ModifyTimeCaretRight()
        {
            var tm = ViewModel.Model.ModifyTimeModel;
            // 去掉 秒
            if (tm.BindableCaretIndex < tm.Text.Length - 1)
            {
                ++tm.BindableCaretIndex;
                if (ModifyTimeModel.ConstantText.Contains(tm.CharOfIndex))
                {
                    ++tm.BindableCaretIndex;
                }
            }
        }

        public void ResetSettingTime()
        {
            ViewModel.Model.ModifyTimeModel.BindableCaretIndex = 0;
            ViewModel.Model.ModifyTimeModel.SettingTime = ViewModel.Model.ShowTime;
        }

        public void ModifyTime()
        {
            var tm = ViewModel.Model.ModifyTimeModel;
            try
            {
                switch (tm.BindableCaretIndex)
                {
                    // 年
                    case 1:
                        tm.SettingTime = new DateTime((tm.SettingTime.Year + 1000)%10000, tm.SettingTime.Month,
                            tm.SettingTime.Day, tm.SettingTime.Hour, tm.SettingTime.Minute, tm.SettingTime.Second,
                            tm.SettingTime.Millisecond);
                        break;
                    case 2:
                        tm.SettingTime =
                            new DateTime(
                                (tm.SettingTime.Year + 100)%1000 + tm.SettingTime.Year - tm.SettingTime.Year%1000,
                                tm.SettingTime.Month,
                                tm.SettingTime.Day, tm.SettingTime.Hour, tm.SettingTime.Minute, tm.SettingTime.Second,
                                tm.SettingTime.Millisecond);
                        break;
                    case 3:
                        tm.SettingTime =
                            new DateTime(
                                (tm.SettingTime.Year + 10)%100 + +tm.SettingTime.Year - tm.SettingTime.Year%100,
                                tm.SettingTime.Month,
                                tm.SettingTime.Day, tm.SettingTime.Hour, tm.SettingTime.Minute, tm.SettingTime.Second,
                                tm.SettingTime.Millisecond);
                        break;
                    case 4:
                        tm.SettingTime =
                            new DateTime((tm.SettingTime.Year + 1)%10 + tm.SettingTime.Year - tm.SettingTime.Year%10,
                                tm.SettingTime.Month,
                                tm.SettingTime.Day, tm.SettingTime.Hour, tm.SettingTime.Minute, tm.SettingTime.Second,
                                tm.SettingTime.Millisecond);
                        break;

                    //月
                    case 6:
                        ModifyMonth10(tm);
                        break;

                    case 7:
                        ModifyMonth1(tm);
                        break;

                    //日
                    case 9:
                        ModifyDay10(tm);
                        break;
                    case 10:
                        ModifyDay1(tm);
                        break;

                    //时
                    case 12:
                        ModifyHour10(tm);
                        break;
                    case 13:
                        ModifyHour1(tm);
                        break;

                    //分
                    case 15:
                        ModifyMinute10(tm);
                        break;
                    case 16:
                        ModifyMinute1(tm);
                        break;

                    //秒
                    case 18:
                        ModifySecond10(tm);
                        break;
                    case 19:
                        ModifySecond1(tm);
                        break;
                }
            }
            catch (Exception e)
            {
                Console.Write(e.ToString());
            }
        }

        private static void ModifyMonth10(ModifyTimeModel tm)
        {
            try
            {
                tm.SettingTime = new DateTime(tm.SettingTime.Year, tm.SettingTime.Month + 10,
                    tm.SettingTime.Day, tm.SettingTime.Hour, tm.SettingTime.Minute, tm.SettingTime.Second,
                    tm.SettingTime.Millisecond);
            }
            catch (Exception)
            {
                tm.SettingTime = new DateTime(tm.SettingTime.Year, 1,
                    tm.SettingTime.Day, tm.SettingTime.Hour, tm.SettingTime.Minute, tm.SettingTime.Second,
                    tm.SettingTime.Millisecond);
            }
        }

        private static void ModifyMonth1(ModifyTimeModel tm)
        {
            try
            {
                tm.SettingTime = new DateTime(tm.SettingTime.Year,
                    (tm.SettingTime.Month + 1)%13,
                    tm.SettingTime.Day, tm.SettingTime.Hour, tm.SettingTime.Minute, tm.SettingTime.Second,
                    tm.SettingTime.Millisecond);
            }
            catch (Exception)
            {
                tm.SettingTime = new DateTime(tm.SettingTime.Year,
                    1,
                    tm.SettingTime.Day, tm.SettingTime.Hour, tm.SettingTime.Minute, tm.SettingTime.Second,
                    tm.SettingTime.Millisecond);
            }
        }

        private static void ModifyDay10(ModifyTimeModel tm)
        {
            try
            {
                tm.SettingTime = new DateTime(tm.SettingTime.Year, tm.SettingTime.Month,
                    tm.SettingTime.Day + 10, tm.SettingTime.Hour, tm.SettingTime.Minute,
                    tm.SettingTime.Second,
                    tm.SettingTime.Millisecond);
            }
            catch (Exception)
            {
                tm.SettingTime = new DateTime(tm.SettingTime.Year, tm.SettingTime.Month,
                    tm.SettingTime.Day%10, tm.SettingTime.Hour,
                    tm.SettingTime.Minute,
                    tm.SettingTime.Second,
                    tm.SettingTime.Millisecond);
            }
        }

        private static void ModifyDay1(ModifyTimeModel tm)
        {
            try
            {
                tm.SettingTime = new DateTime(tm.SettingTime.Year, tm.SettingTime.Month,
                    tm.SettingTime.Day + 1, tm.SettingTime.Hour,
                    tm.SettingTime.Minute, tm.SettingTime.Second,
                    tm.SettingTime.Millisecond);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                tm.SettingTime = new DateTime(tm.SettingTime.Year, tm.SettingTime.Month,
                    1, tm.SettingTime.Hour,
                    tm.SettingTime.Minute, tm.SettingTime.Second,
                    tm.SettingTime.Millisecond);
            }
        }

        private static void ModifyHour10(ModifyTimeModel tm)
        {
            try
            {
                tm.SettingTime = new DateTime(tm.SettingTime.Year, tm.SettingTime.Month,
                    tm.SettingTime.Day, tm.SettingTime.Hour + 10, tm.SettingTime.Minute,
                    tm.SettingTime.Second,
                    tm.SettingTime.Millisecond);
            }
            catch (Exception)
            {
                tm.SettingTime = new DateTime(tm.SettingTime.Year, tm.SettingTime.Month,
                    tm.SettingTime.Day, tm.SettingTime.Hour%10,
                    tm.SettingTime.Minute,
                    tm.SettingTime.Second,
                    tm.SettingTime.Millisecond);
            }
        }

        private static void ModifyHour1(ModifyTimeModel tm)
        {
            try
            {
                tm.SettingTime = new DateTime(tm.SettingTime.Year, tm.SettingTime.Month,
                    tm.SettingTime.Day, tm.SettingTime.Hour + 1,
                    tm.SettingTime.Minute, tm.SettingTime.Second,
                    tm.SettingTime.Millisecond);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                tm.SettingTime = new DateTime(tm.SettingTime.Year, tm.SettingTime.Month,
                    tm.SettingTime.Day, 1,
                    tm.SettingTime.Minute, tm.SettingTime.Second,
                    tm.SettingTime.Millisecond);
            }
        }

        private static void ModifyMinute10(ModifyTimeModel tm)
        {
            try
            {
                tm.SettingTime = new DateTime(tm.SettingTime.Year, tm.SettingTime.Month,
                    tm.SettingTime.Day, tm.SettingTime.Hour, tm.SettingTime.Minute + 10,
                    tm.SettingTime.Second,
                    tm.SettingTime.Millisecond);
            }
            catch (Exception)
            {
                tm.SettingTime = new DateTime(tm.SettingTime.Year, tm.SettingTime.Month,
                    tm.SettingTime.Day, tm.SettingTime.Hour,
                    tm.SettingTime.Minute%10,
                    tm.SettingTime.Second,
                    tm.SettingTime.Millisecond);
            }
        }

        private static void ModifyMinute1(ModifyTimeModel tm)
        {
            try
            {
                tm.SettingTime = new DateTime(tm.SettingTime.Year, tm.SettingTime.Month,
                    tm.SettingTime.Day, tm.SettingTime.Hour,
                    tm.SettingTime.Minute + 1, tm.SettingTime.Second,
                    tm.SettingTime.Millisecond);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                tm.SettingTime = new DateTime(tm.SettingTime.Year, tm.SettingTime.Month,
                    tm.SettingTime.Day, tm.SettingTime.Hour,
                    1, tm.SettingTime.Second,
                    tm.SettingTime.Millisecond);
            }
        }

        private static void ModifySecond10(ModifyTimeModel tm)
        {
            try
            {
                tm.SettingTime = new DateTime(tm.SettingTime.Year, tm.SettingTime.Month,
                    tm.SettingTime.Day, tm.SettingTime.Hour, tm.SettingTime.Minute,
                    tm.SettingTime.Second + 10,
                    tm.SettingTime.Millisecond);
            }
            catch (Exception)
            {
                tm.SettingTime = new DateTime(tm.SettingTime.Year, tm.SettingTime.Month,
                    tm.SettingTime.Day, tm.SettingTime.Hour,
                    tm.SettingTime.Minute,
                    tm.SettingTime.Second%10,
                    tm.SettingTime.Millisecond);
            }
        }

        private static void ModifySecond1(ModifyTimeModel tm)
        {
            try
            {
                tm.SettingTime = new DateTime(tm.SettingTime.Year, tm.SettingTime.Month,
                    tm.SettingTime.Day, tm.SettingTime.Hour,
                    tm.SettingTime.Minute, tm.SettingTime.Second + 1,
                    tm.SettingTime.Millisecond);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                tm.SettingTime = new DateTime(tm.SettingTime.Year, tm.SettingTime.Month,
                    tm.SettingTime.Day, tm.SettingTime.Hour,
                    tm.SettingTime.Minute, 1,
                    tm.SettingTime.Millisecond);
            }
        }

    }
}