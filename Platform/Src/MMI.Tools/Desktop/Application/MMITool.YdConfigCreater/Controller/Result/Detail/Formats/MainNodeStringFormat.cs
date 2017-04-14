namespace MMITool.Addin.YdConfigCreater.Controller.Result.Detail.Formats
{
    public class MainNodeStringFormatProvider : ConditionModeStringFormatProviderBase
    {
        // ReSharper disable once InconsistentNaming
        private static readonly string m_Format = @"#库特性参数
[RunParam]
;是否启用TCP异步模式
ASYNC_TCP = {4}

;是否启用UDP异步模式
ASYNC_UDP = {5}

;是否启用Muit异步模式
ASYNC_Mult = {6}


; 网络将会自动连接同一组内1节点，
; 1节点定义为：40001-前两位为组号; 后三位若是001则说明是1节点.
; 自动建立组外连接的系统ID列表如: 50012;60007
[NETWORK]
ConnectAddr = 

;数据库连接，IP地址，端口，数据库名称,用户名，密码
[DATABASE]
DatabaseAddr = {0};{1};{2}

;自身系统ID、是否打印网络信息
[SYSTEM]
SysID = {3}001
CmdData = 0
WndData = 0

";


        protected override string FortmatTemplate
        {
            get { return m_Format; }
        }
    }
}