using System.Collections.Generic;
using System.Linq;

namespace ATP200C.FunctionButton
{
    internal class BtnMemento
    {

        public static readonly BtnMemento Instance = new BtnMemento();

        public List<FunBtnMenu> BackupStateList { get; private set; }

        private BtnMemento()
        {
            BackupStateList = new List<FunBtnMenu>();
        }

        public void AddNewObjToMemento(FunBtnMenu funBtnMenu)
        {
            //为空直接加
            if (!BackupStateList.Any())
            {
                BackupStateList.Add(funBtnMenu);
            }
            else if (funBtnMenu != BackupStateList.Last())
            {
                BackupStateList.Add(funBtnMenu);
            }
            if (BackupStateList.Count > 3)
            {
                BackupStateList.RemoveAt(0);
            }
        }
    }
}
