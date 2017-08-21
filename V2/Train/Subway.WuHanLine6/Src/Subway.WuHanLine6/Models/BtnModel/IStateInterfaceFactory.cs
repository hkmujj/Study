using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using Microsoft.Practices.ServiceLocation;
using Subway.WuHanLine6.Controller.BtnResponse;
using Subway.WuHanLine6.Interfaces;
using Subway.WuHanLine6.Views.Common;

namespace Subway.WuHanLine6.Models.BtnModel
{
    /// <summary>
    ///
    /// </summary>
    public interface IStateInterfaceFactory
    {
        /// <summary>
        /// 获取或创建
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        IStateInterface GetOrCreat(StateKeys key);
    }

    /// <summary>
    ///
    /// </summary>
    [Export(typeof(IStateInterfaceFactory))]
    public class StateInterfaceFactory : IStateInterfaceFactory
    {
        private Dictionary<StateKeys, IStateInterface> m_StateInterfaces;

        /// <summary>
        ///
        /// </summary>
        public StateInterfaceFactory()
        {
            m_StateInterfaces = new Dictionary<StateKeys, IStateInterface>();
        }

        /// <summary>
        /// 获取或创建
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public IStateInterface GetOrCreat(StateKeys key)
        {
            if (!m_StateInterfaces.ContainsKey(key))
            {
                CreatStateInterface(key);
            }
            return m_StateInterfaces.ContainsKey(key) ? m_StateInterfaces[key] : null;
        }

        private void CreatStateInterface(StateKeys key)
        {
            var confih = GlobalParams.Instance.StateInterfaceUnits.FirstOrDefault(f => f.Key == key.ToString());
            if (confih != null)
            {
                var interfac =
                ServiceLocator.Current.GetInstance<StateInterface>();

                interfac.TitleName = confih.Title;
                interfac.CurrentView = GetFullName(confih.Content);
                interfac.Btn01 = CreatItem(confih.Btn01Content, confih.Btn01ActionKey);
                interfac.Btn02 = CreatItem(confih.Btn02Content, confih.Btn02ActionKey);
                interfac.Btn03 = CreatItem(confih.Btn03Content, confih.Btn03ActionKey);
                interfac.Btn04 = CreatItem(confih.Btn04Content, confih.Btn04ActionKey);
                interfac.Btn05 = CreatItem(confih.Btn05Content, confih.Btn05ActionKey);
                interfac.Btn06 = CreatItem(confih.Btn06Content, confih.Btn06ActionKey);
                interfac.Btn07 = CreatItem(confih.Btn07Content, confih.Btn07ActionKey);
                interfac.Btn08 = CreatItem(confih.Btn08Content, confih.Btn08ActionKey);
                interfac.Btn09 = CreatItem(confih.Btn09Content, confih.Btn09ActionKey);
                interfac.Btn10 = CreatItem(confih.Btn10Content, confih.Btn10ActionKey);
                interfac.Btn11 = CreatItem(confih.Btn11Content, confih.Btn11ActionKey);
                interfac.Btn12 = CreatItem(confih.Btn12Content, confih.Btn12ActionKey);
                m_StateInterfaces.Add(key, interfac);
            }
        }

        private static readonly Style Style = new Style(typeof(TextBlock))
        {
            Setters =
            {
                new Setter(TextBlock.ForegroundProperty,Brushes.Black),
                new Setter(TextBlock.FontSizeProperty,18d),
                new Setter(FrameworkElement.HorizontalAlignmentProperty,HorizontalAlignment.Center),
                new Setter(FrameworkElement.VerticalAlignmentProperty,VerticalAlignment.Center),
            }
        };

        private static string GetFullName(string name)
        {
            if (string.IsNullOrEmpty(name))
            {
                return string.Empty;
            }
            if (m_Type == null || m_Type.Length == 0)
            {
                m_Type = typeof(BtnResponseBase).Assembly.GetTypes();
            }

            var first = m_Type.FirstOrDefault(f => f.Name.Equals(name));

            var fullNme = first == null ? string.Empty : first.FullName;

            return fullNme;
        }

        private static Type[] m_Type;

        private static BtnItem CreatItem(string content, string action)
        {
            var pth = typeof(BtnResponseBase).Namespace + "." + action;

            IBtnActionResponse response = null;

            if (!string.IsNullOrEmpty(action))
            {
                var type1 = typeof(BtnResponseBase).Assembly.GetType(pth, false, false);
                response = ServiceLocator.Current.GetInstance(type1) as IBtnActionResponse;
            }

            object contents;

            if (content.Equals("到站广播"))
            {
                contents = new ArriveStationBorderCasterBtn();
            }
            else if (content.Equals("离站广播"))
            {
                contents = new LeveStationBorderCasterBtn();
            }
            else if (content.Equals("向前跳站"))
            {
                contents = new SkipStationUp();
            }
            else if (content.Equals("向后跳站"))
            {
                contents = new SkipStationDown();
            }
            else
            {
                contents = new TextBlock()
                {
                    Text = content,
                    Style = Style,
                };
            }

            return new BtnItem(contents, response);
        }
    }
}