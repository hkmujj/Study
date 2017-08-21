using System.Diagnostics;
using Engine.LCDM.HDX2.Entity.Model.Domain;
using Microsoft.Practices.Prism.ViewModel;

namespace Engine.LCDM.HDX2.Entity.Model.BtnStragy
{
    [DebuggerDisplay("Content={Content}")]
    public class BtnItem : NotificationObject, IRaiseResourceChangedProvider
    {
        public BtnItem(string content, string image, IBtnActionResponser actionResponser)
        {
            Content = content;
            Image = image;
            ActionResponser = actionResponser;
        }

        public BtnItem(string content, IBtnActionResponser actionResponser)
        {
            Content = content;
            ActionResponser = actionResponser;
        }

        public string Content { private set; get; }

        public string Image { private set; get; }

        public IBtnActionResponser ActionResponser { private set; get; }

        public void RaiseResourceChanged()
        {
            RaisePropertyChanged(() => Content);
            RaisePropertyChanged(() => Image);
        }
    }
}