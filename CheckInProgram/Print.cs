using System.Windows.Forms;

namespace CheckInProgram
{
    class Print
    {
        public static void show(int errorid)
        {
            switch (errorid)
            {
                case 100:
                case 200:
                case 300:
                case 400:
                case 500:
                case 600:
                case 700:
                case 800:
                case 900:
                    errormsg("指定的操作未完成，操作代码" + errorid + "。\n如有问题，请联系值班员。", errorid);
                    break;
                case 102:
                    errormsg("启动失败，本教室不在允许签到的教室列表中，操作代码" + errorid + "。\n如有问题，请联系值班员。", errorid);
                    Application.Exit();
                    break;
                case 104:
                    errormsg("启动失败，本教室不在当前时间段允许签到的教室列表中，，请稍后再试，操作代码" + errorid + "。\n如有问题，请联系值班员。", errorid);
                    Application.Exit();
                    break;
                case 202:
                case 302:
                    errormsg("签到失败，现在已经不是签到时间，操作代码" + errorid + "。\n如有问题，请联系值班员。", errorid);
                    break;
                case 203:
                    errormsg("签到失败，本教室不在当前时间内允许签到的教室列表中，操作代码" + errorid + "。\n如有问题，请联系值班员。", errorid);
                    Application.Exit();
                    break;
                case 204:
                    errormsg("签到失败，你不是这节课的学生，操作代码" + errorid + "。\n如有问题，请联系值班员。", errorid);
                    break;
                case 303:
                    errormsg("签到失败，你已经有本节课的非正常签到记录，操作代码" + errorid + "。\n如有问题，请联系值班员。", errorid);
                    break;
                case 312:
                    errormsg("换机失败，换机所用时间超时，本节课将被记为旷课，操作代码" + errorid + "。\n如有问题，请联系值班员。", errorid);
                    Application.Exit();
                    break;
                case 313:
                    errormsg("换机失败，你不能在同一台机器上换机上线，操作代码" + errorid + "。\n如有问题，请联系值班员。", errorid);
                    Application.Exit();
                    break;
                case 402:
                    errormsg("保持在线失败，无法找到你的在线记录，未能发送在线心跳，操作代码" + errorid + "。\n如有问题，请联系值班员。", errorid);
                    break;
                case 502:
                    errormsg("提前下线失败，无法找到你的在线记录，操作代码" + errorid + "。\n如有问题，请联系值班员。", errorid);
                    break;
                case 602:
                    errormsg("无操作下线失败，无法找到你的在线记录，操作代码" + errorid + "。\n如有问题，请联系值班员。", errorid);
                    break;
                case 712:
                    errormsg("换机失败，无法找到你的在线记录，操作代码" + errorid + "。\n如有问题，请联系值班员。", errorid);
                    break;
                case 802:
                    errormsg("注销失败，现在不是注销时间，操作代码" + errorid + "。\n如有问题，请联系值班员。", errorid);
                    break;
                case 803:
                    errormsg("注销失败，无法找到你的在线记录，未能签退，操作代码" + errorid + "。\n如有问题，请联系值班员。", errorid);
                    break;
                case 902:
                    errormsg("查询失败，没有你的历史签到记录（每学期第一次签到出现此情况为正常情况），操作代码" + errorid + "。\n如有问题，请联系值班员。", errorid);
                    break;
                default:
                    errormsg("指定的操作失败，未能解析的错误，操作代码" + errorid + "。\n如有问题，请联系值班员。", errorid);
                    break;
            }
        }
        public static void show(string errorcontent)
        {
            errormsg("执行失败，" + errorcontent);
        }

        public static void errormsg(string error,int errorid)
        {
            System.Windows.Forms.MessageBox.Show(error,"错误",System.Windows.Forms.MessageBoxButtons.OK,System.Windows.Forms.MessageBoxIcon.Error);
        }

        public static void errormsg(string error)
        {
            System.Windows.Forms.MessageBox.Show(error, "错误", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
        }

        public static void infomsg(string info,string tittle)
        {
            System.Windows.Forms.MessageBox.Show(info, tittle, System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Information);
        }

        public static string state(int stateid)
        {
            switch (stateid)
            {
                case 0:
                    return "在线";
                case 1:
                    return "正常";
                case 2:
                    return "迟到";
                case 3:
                    return "旷课";
                case 4:
                    return "换机中";
                case 5:
                    return "换机完成";
                default:
                    return "";
            }
        }
    }
}
