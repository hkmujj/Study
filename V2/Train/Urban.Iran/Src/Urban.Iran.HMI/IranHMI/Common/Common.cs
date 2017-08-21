using System.Drawing;

namespace Urban.Iran.HMI.Common
{
    public delegate void OnMouseDown(FjButton btnSender, Point pt);


    public delegate void OnMouseDownEx(FjButtonEx btnSender, Point pt);
    public delegate void OnLostFoucse(FjButtonEx btnSender, Point pt);
}
