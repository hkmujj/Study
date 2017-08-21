using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using MMI.Facility.Interface;
using MMI.Facility.Interface.Attribute;
using MMI.Facility.Interface.Data;

namespace Subway.ATC.THALES.VOBC
{
    [GksDataType(DataType.isMMIObjectClass)]
    public class V5_C0_FalutList : baseClass
    {
        private Button _deleteBtn;
        private Button _englishBtn;
        private Button _quitBtn;
        private Button _upBtn;
        private Button _downBtn;
        private Image background;
        private int _currentMessageID = 0;
        private List<MessageInfo> _infos = new List<MessageInfo>();
        private int _id = 0;
        private int _upID = 0;
        private int _downID = 0;

        public override string GetInfo()
        {
            return "故障信息列表";
        }

        public override bool init(ref int nErrorObjectIndex)
        {
            using (FileStream fs = new FileStream(Path.Combine(RecPath, UIObj.ParaList[0]), FileMode.Open))
            {
                background = Image.FromStream(fs);
            }

            _deleteBtn = new Button(new Rectangle(17, 479, 97, 41), null, null);
            _deleteBtn.MouseUpEvent += _deleteBtn_MouseUpEvent;

            _englishBtn = new Button(new Rectangle(624, 474, 87, 44), null, null);
            _englishBtn.MouseUpEvent += _englishBtn_MouseUpEvent;

            _quitBtn = new Button(new Rectangle(624, 529, 87, 44), null, null);
            _quitBtn.MouseUpEvent += _quitBtn_MouseUpEvent;

            _upBtn = new Button(new Rectangle(723, 476, 44, 43), null, null);
            _upBtn.MouseUpEvent += _upBtn_MouseUpEvent;

            _downBtn = new Button(new Rectangle(724, 526, 44, 43), null, null);
            _downBtn.MouseUpEvent += _downBtn_MouseUpEvent;

            return true;
        }

        public override void setRunValue(int nParaA, int nParaB, float nParaC)
        {
            if (nParaA == 2)
            {
                if (nParaB == 108)
                {
                    if (VC_C0_GetValue.AllFaluts.Count <= 20)
                    {
                        _downID = 0;
                        _infos.Clear();
                        _infos.AddRange(VC_C0_GetValue.AllFaluts);
                        _upID = VC_C0_GetValue.AllFaluts.Count - 1;
                    }
                    else
                    {
                        MessageInfo[] Faluts = new MessageInfo[20];
                        VC_C0_GetValue.AllFaluts.CopyTo(0, Faluts, 0, 20);
                        _infos = Faluts.ToList();
                        _downID = 0;
                        _upID = 19;
                    }
                    _currentMessageID = 0;
                }
            }
        }

        public override bool mouseUp(Point nPoint)
        {
            _deleteBtn.MouseUp(nPoint);
            _quitBtn.MouseUp(nPoint);
            _upBtn.MouseUp(nPoint);
            _downBtn.MouseUp(nPoint);

            return true;
        }

        void _downBtn_MouseUpEvent(int obj)
        {
            if (_currentMessageID == 19)
            {
                if (VC_C0_GetValue.AllFaluts.Count - 1 > _upID)
                {
                    _upID++;
                    _downID++;
                    _infos.RemoveAt(0);
                    _infos.Add(VC_C0_GetValue.AllFaluts[_upID]);
                }
            }
            else
            {
                if (_currentMessageID < _infos.Count - 1)
                {
                    _currentMessageID++;
                }
            }
        }

        void _upBtn_MouseUpEvent(int obj)
        {
            if (_currentMessageID == 0)
            {
                if (_downID > 0)
                {
                    _downID--;
                    _upID--;
                    _infos.RemoveAt(19);
                    _infos.Insert(0, VC_C0_GetValue.AllFaluts[_downID]);
                }
            }
            else
            {
                if (_currentMessageID > 0)
                {
                    _currentMessageID--;
                }
            }
        }

        void _quitBtn_MouseUpEvent(int obj)
        {
            append_postCmd(CmdType.ChangePage, 104, 0, 0);
        }

        void _englishBtn_MouseUpEvent(int obj)
        {
        }

        void _deleteBtn_MouseUpEvent(int obj)
        {
            if (VC_C0_GetValue.AllFaluts == null || VC_C0_GetValue.AllFaluts.Count == 0)
            {
                return;
            }

            if (VC_C0_GetValue.AllFaluts.Count <= _currentMessageID)
            {
                return;
            }

            int index = _downID + _currentMessageID;
            if (VC_C0_GetValue.AllFaluts.Count <= index)
            {
                return;
            }

            VC_C0_GetValue.AllFaluts.RemoveAt(index);
            _infos.RemoveAt(_currentMessageID);
            _currentMessageID--;

            _downBtn_MouseUpEvent(0);
        }

        public override void paint(Graphics dcGs)
        {
            dcGs.DrawImage(background, new Rectangle(0, 0, 800, 600));

            if (VC_C0_GetValue.AllFaluts.Count == 0)
            {
                return;
            }

            for (int i = 0; i < _infos.Count; i++)
            {
                dcGs.DrawString(
                    _infos[i].Time,
                    new Font("Arial", 13),
                    new SolidBrush(Color.FromArgb(51, 204, 204)),
                    new RectangleF(20 + 10, 34 + 20 * i, 207, 20),
                    new StringFormat() { Alignment = StringAlignment.Near, LineAlignment = StringAlignment.Center }
                    );

                dcGs.DrawString(
                    _infos[i].Description,
                    new Font("Arial", 13),
                    Brushes.Red,
                    new RectangleF(239, 34 + 20 * i, 207, 20),
                    new StringFormat() { Alignment = StringAlignment.Near, LineAlignment = StringAlignment.Center }
                    );
            }

            dcGs.DrawString(
                _infos[_currentMessageID].Description,
                new Font("Arial", 15, FontStyle.Bold),
                Brushes.Red,
                new RectangleF(113, 477, 499, 40),
                new StringFormat() { Alignment = StringAlignment.Center, LineAlignment = StringAlignment.Center }
                );

            
        }
    }
}
