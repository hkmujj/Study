using System;
using System.Collections.Generic;
using ATP200C.MainView.FunctionButton;
using ATP200C.PublicComponents;
using MMI.Facility.Interface.Data;

namespace ATP200C.FunctionButton
{
    /// <summary>
    /// �๦�ܲ˵�(����)
    /// </summary>
    public class FunBtnMenuCommon : FunBtnMenu
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="menuId"></param>
        /// <param name="funNames"></param>
        public FunBtnMenuCommon(int menuId, IList<FunBtnStruct> funNames)
            : base(menuId, funNames)
        {
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="context"></param>
        public override void Handle(FunctionButtonView context)
        {
            var buttonId = ATPButtonScreen.BtnState.BtnId;

            foreach (var buttonInfo in m_BtnInfoArr)
            {
                if (buttonInfo.FunButton.BtnTypeName != (BtnTypeName) buttonId)
                {
                    continue; //��ť��F1~F8֮���
                }
                if (buttonInfo.Locked) continue; //��ťû������


                buttonInfo.SendTheValue(context);


                //�����ж�������ת�˵�
                if (buttonInfo.FunButton.JumpConditional != null)
                {
                    foreach (var func in buttonInfo.FunButton.JumpConditional)
                    {
                        if (func.Value != null)
                        {
                            if (!func.Value())
                            {
                                continue;
                            }
                            context.RequestNavigate(func.Key);
                            break;
                        }
                        context.RequestNavigate(func.Key);
                    }
                }

                if (buttonInfo.ButtonEventHandler != null)
                {
                    buttonInfo.ButtonEventHandler.Handle(context, buttonInfo);
                }
            }
        }

    }
}