using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Threading;
using ATP200C.ButtonStateControl;
using ATP200C.FunctionButton;
using ATP200C.PopupViews;
using ATP200C.PublicComponents;
using ATP200C.Resource.Images;
using CommonUtil.Util;
using Excel.Interface;
using MMI.Facility.Interface.Attribute;
using MMI.Facility.Interface.Data;

namespace ATP200C.MainView.FunctionButton
{
    /// <summary>
    /// 
    /// </summary>
    [GksDataType(DataType.isMMIObjectClass)]
    public partial class FunctionButtonView : ATPBaseClass
    {

        public int SoudValue
        {
            get { return m_SoudValue; }
            set
            {
                if (m_SoudValue == value)
                {
                    return;
                }
                m_SoudValue = value;
                append_postCmd(CmdType.SetFloatValue, 607, 0, value);
            }
        }

        public bool IsLoad;
        public bool IsSetLoadSound;
        /// <summary>
        /// 所有菜单字典
        /// </summary>
        public Dictionary<int, FunBtnMenu> AllFunBtnState { get; private set; }

        /// <summary>
        /// 所有条件判断字典
        /// </summary>
        public Dictionary<int, Func<bool>> AllConditionals = new Dictionary<int, Func<bool>>();

        private int m_SoudValue;

        /// <summary>
        /// 此类实例增加后需要重构
        /// </summary>
        public static FunctionButtonView Instance { private set; get; }

        public FunctionButtonView()
        {
            AllFunBtnState = new Dictionary<int, FunBtnMenu>();

        }

        public override string GetInfo()
        {
            return "ATP_200C";
        }

        public override void setRunValue(int nParaA, int nParaB, float nParaC)
        {
            base.setRunValue(nParaA, nParaB, nParaC);
            if (nParaA != 2)
            {
                return;
            }
            if (!IsLoad)
            {
                IsLoad = true;
            }
        }

        public override bool Initalize()
        {
            Instance = this;

            //加载坐标
            InitAllRectangles();

            //加载所有条件判断
            InitAllConditionals();

            var config = LoadButtonConfigFile();

            ReflectButtonByConfig(config);

            // 初始化
            RequestNavigate(0);

            return true;
        }

        private void ReflectButtonByConfig(DataTable funMenuInfo)
        {
            foreach (DataRow row in funMenuInfo.Rows)
            {
                int temp;
                if (!int.TryParse(row[1].ToString().Trim(), out temp))
                {
                    continue;
                }
                FunBtnMenu fbm;
                switch (temp)
                {
                    case 11:
                        fbm = new FunBtnMenuDriverId(temp, GetBtnContent(row))
                        {
                            BtnMeans = row[0].ToString().Trim(),
                            NumbKeyboardOpen = string.IsNullOrEmpty(row[2].ToString())
                        };
                        break;
                    case 12:
                        fbm = new FunBtnMenuTrainId(temp, GetBtnContent(row))
                        {
                            BtnMeans = row[0].ToString().Trim(),
                            NumbKeyboardOpen = string.IsNullOrEmpty(row[2].ToString())
                        };
                        break;
                    default:
                        fbm = new FunBtnMenuCommon(temp, GetBtnContent(row))
                        {
                            BtnMeans = row[0].ToString().Trim(),
                            NumbKeyboardOpen = string.IsNullOrEmpty(row[2].ToString())
                        };
                        break;
                }
                AllFunBtnState.Add(temp, fbm);
            }
        }

        private DataTable LoadButtonConfigFile()
        {
            var excel = new ExcelReaderConfig()
            {
                File = Path.Combine(AppPaths.ConfigDirectory, "MessageFormat_FunBtnFormat.xls"),
                Coloumns = new List<ColoumnConfig>() { new ColoumnConfig() { IsPrimaryKey = false, Name = "*" } },
                SheetNames = new List<string>() { "sheet1" },
            };
            return excel.Adapter().Tables[0];
        }

        public override void paint(Graphics dcGs)
        {
            if (IsLoad && !IsSetLoadSound)
            {
                SoudValue = 50;
                IsSetLoadSound = true;
            }
            CurrentFunBtnMenu.Paint(dcGs);
            SendCurrenMenu();
            if (ATPButtonScreen.BtnState != null && !ATPButtonScreen.BtnState.IsPress)
            {
                RequestHandle();
                ATPButtonScreen.BtnState.Press();
            }
        }

        private void SendCurrenMenu()
        {
            if (CurrentFunBtnMenu.BtnMeans.Contains("F1司机号"))
            {
                append_postCmd(CmdType.SetBoolValue, 5027, 1, 0);
            }
            else
            {
                append_postCmd(CmdType.SetBoolValue, 5027, 0, 0);
            }
        }
        private FunBtnStruct[] GetBtnContent(DataRow dr)
        {
            var fbs = new FunBtnStruct[8];
            const int step = 5;
            for (int i = 0; i < fbs.Length; i++)
            {
                fbs[i].BtnTypeName = (BtnTypeName)i;//类型名
                int tempInt;
                //F1内容从第3列开始
                string tempStr = dr[3 + i * step].ToString().Trim();
                if (tempStr == "返回")
                {
                    //图片内容
                    fbs[i].BtnImage = ImageResource.返回;
                }
                else
                {
                    //文字内容
                    fbs[i].BtnContent = tempStr.Replace("\\n", "\n");
                }

                //按钮是否能按的判断条件
                tempStr = dr[4 + i * step].ToString().Trim();
                if (!string.IsNullOrEmpty(tempStr) && int.TryParse(tempStr, out tempInt))
                {
                    var tempId = Convert.ToInt32(tempStr);
                    if (AllConditionals.ContainsKey(tempId))
                        fbs[i].LockedConditional = AllConditionals[tempId];
                }
                else
                {
                    try
                    {
                        var type = Type.GetType(tempStr);
                        if (type != null)
                        {
                            fbs[i].ButtonLockedStatePredicate = Activator.CreateInstance(type) as IButtonLockedStatePredicate;
                        }
                    }
                    catch (Exception e)
                    {
                        LogMgr.Warn(string.Format("Can not reflect type {0}. {1}", tempStr, e));
                    }
                }
                //菜单跳转与条件
                tempStr = dr[5 + i * step].ToString().Trim();
                if (!string.IsNullOrEmpty(tempStr))
                {
                    fbs[i].JumpConditional = new Dictionary<int, Func<bool>>();
                    var tmp1 = tempStr.Split(new[] { ';', '；' });
                    foreach (var tmp2 in tmp1.Select(s => s.Split(new[] { '_' })))
                    {
                        fbs[i].JumpConditional.Add(Convert.ToInt32(tmp2[0]),
                            tmp2.Length == 1 ? null : AllConditionals[Convert.ToInt32(tmp2[1])]);
                    }
                }

                //发送位
                tempStr = dr[6 + i * step].ToString().Trim();
                if (!string.IsNullOrEmpty(tempStr))
                {
                    fbs[i].SendValue = new Dictionary<Func<bool>, List<int>>();
                    var tmp1 = tempStr.Split(new[] { ';', '；' });
                    foreach (var s in tmp1)
                    {
                        var tmp2 = s.Split(new[] { '_' });
                        fbs[i].SendValue.Add(AllConditionals[Convert.ToInt32(tmp2[1])], SeparateString.SplitStr(tmp2[0], "[", "]"));
                    }
                }

                fbs[i].ButtonEventHandlerName = dr[7 + i * step].ToString().Trim();
            }
            return fbs;
        }

        /// <summary>
        /// 当前正在显示的button
        /// </summary>
        public FunBtnMenu CurrentFunBtnMenu { get; set; }

        /// <summary>
        /// 对请求做处理，并设置下一个状态
        /// </summary>
        public void RequestHandle()
        {
            CurrentFunBtnMenu.Handle(this);
        }

        /// <summary>
        /// 请求导航到
        /// </summary>
        /// <param name="btnId"></param>
        public void RequestNavigate(int btnId)
        {
            if (AllFunBtnState[btnId] != CurrentFunBtnMenu)
            {
                BtnMemento.Instance.AddNewObjToMemento(CurrentFunBtnMenu);
                CurrentFunBtnMenu = AllFunBtnState[btnId];
            }
        }

        /// <summary>
        /// 请求返回
        /// </summary>
        public void RequestNavigateBack()
        {
            CurrentFunBtnMenu = BtnMemento.Instance.BackupStateList.Last();
            BtnMemento.Instance.BackupStateList.RemoveAt(BtnMemento.Instance.BackupStateList.Count - 1);
        }
    }
}
