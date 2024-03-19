using System;
using System.Windows.Forms;
using UnityEngine;
using System.Reflection;
using Application = UnityEngine.Application;
using SysApp = System.Windows.Forms.Application;

public class WindowMinimizer : MonoBehaviour
{
    public void MinimizeLauncher()
    {
        var form = new Form();
        form.WindowState = FormWindowState.Minimized;
    }

    public void MaximizeLauncher()
    {
        var form = new Form();
        form.WindowState = FormWindowState.Maximized;
    }
}
