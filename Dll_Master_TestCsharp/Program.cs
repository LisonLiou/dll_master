using System;
using System.IO;
using System.Reflection;
using System.Runtime.InteropServices;

namespace Dll_Master_TestCsharp
{
    class Program
    {
        [DllImport("kernel32.dll")]
        private extern static IntPtr LoadLibrary(String path);//path 就是dll路径 返回结果为0表示失败。
        [DllImport("kernel32.dll")]
        private extern static IntPtr GetProcAddress(IntPtr lib, String funcName);//lib是LoadLibrary返回的句柄，funcName 是函数名称 返回结果为0标识失败。
        [DllImport("kernel32.dll")]
        private extern static bool FreeLibrary(IntPtr lib);

        //声明委托
        //[UnmanagedFunctionPointerAttribute(CallingConvention.Cdecl)]
        [UnmanagedFunctionPointerAttribute(CallingConvention.StdCall)]
        delegate int divide(int arg);

        static void Main(string[] args)
        {
            //https://blog.csdn.net/weixin_42291376/article/details/111998553
            //https://www.cnblogs.com/cyf-besti/p/5325186.html#:~:text=C%23%E6%97%B6%E5%B8%B8%E9%9C%80%E8%A6%81%E8%B0%83%E7%94%A8C%2FC%2B%2BDLL%EF%BC%8C%E5%BD%93%E4%BC%A0%E9%80%92%E5%8F%82%E6%95%B0%E6%97%B6%E6%97%B6%E5%B8%B8%E9%81%87%E5%88%B0%E9%97%AE%E9%A2%98%EF%BC%8C%E5%B0%A4%E5%85%B6%E6%98%AF%E4%BC%A0%E9%80%92%E5%92%8C%E8%BF%94%E5%9B%9E%E5%AD%97%E7%AC%A6%E4%B8%B2%E6%97%B6%E3%80%82%20VC%2B%2B%E4%B8%AD%E4%B8%BB%E8%A6%81%E5%AD%97%E7%AC%A6%E4%B8%B2%E7%B1%BB%E5%9E%8B%E4%B8%BA%EF%BC%9ALPSTR%2CLPCSTR%2C%20LPCTSTR%2C%20string%2C%20CString%2C%20LPCWSTR%2C%20LPWSTR%E7%AD%89%EF%BC%8C%E4%BD%86%E8%BD%AC%E4%B8%BAC%23%E7%B1%BB%E5%9E%8B%E5%8D%B4%E4%B8%8D%E5%AE%8C%E5%85%A8%E7%9B%B8%E5%90%8C%E3%80%82,CallBack%E5%9B%9E%E8%B0%83%E5%87%BD%E6%95%B0%E9%9C%80%E8%A6%81%E5%B0%81%E8%A3%85%E5%9C%A8%E4%B8%80%E4%B8%AA%E5%A7%94%E6%89%98%E9%87%8C%EF%BC%8Cdelegate%20static%20extern%20int%20FunCallBack%20%28string%20str%29%3B
            //https://blog.csdn.net/qq_33286695/article/details/99968924#:~:text=System.BadImageFormatException%3A%20%E8%AF%95%E5%9B%BE%E5%8A%A0%E8%BD%BD%E6%A0%BC%E5%BC%8F%E4%B8%8D%E6%AD%A3%E7%A1%AE%E7%9A%84%E7%A8%8B%E5%BA%8F%E3%80%82%20%28%E5%BC%82%E5%B8%B8%E6%9D%A5%E8%87%AA%20HRESULT%3A0x8007000B%29,%E7%AC%AC%E4%B8%80%E6%AD%A5%EF%BC%9A%E9%A1%B9%E7%9B%AE%E5%8F%B3%E9%94%AE%E5%B1%9E%E6%80%A7-%3E%E9%A1%B9%E7%9B%AE%E8%AE%BE%E8%AE%A1%E5%99%A8-%3E%E7%94%9F%E6%88%90-%3E%E5%B9%B3%E5%8F%B0-%3E%E6%8A%8A%E2%80%99%E9%BB%98%E8%AE%A4%E8%AE%BE%E7%BD%AE%20%28%E4%BB%BB%E4%BD%95%20CPU%29%27%E6%94%B9%E4%B8%BAx86%E3%80%82%20%E5%9B%A0%E4%B8%BA%E2%80%99%E4%BB%BB%E4%BD%95%20CPU%E2%80%99%E7%9A%84%E7%A8%8B%E5%BA%8F%E5%9C%A864%E4%BD%8D%E7%9A%84%E6%9C%BA%E5%99%A8%E4%B8%8A%E5%B0%B1%E4%BC%9A%E7%94%A8%E8%BF%90%E8%A1%8C%E4%B8%BA64%E4%BD%8D%EF%BC%8C%E8%80%8C64%E7%A8%8B%E5%BA%8F%E6%98%AF%E4%B8%8D%E8%83%BD%E5%8A%A0%E8%BD%BD32%E4%BD%8Ddll%E7%9A%84

            string dllPath = Environment.CurrentDirectory + "\\cdll\\Dll_Master.dll";

            IntPtr hLib = LoadLibrary(dllPath);//加载函数
            IntPtr apiFunction1 = GetProcAddress(hLib, "divide");//获取函数地址

            int i = Marshal.GetLastWin32Error();
            if (apiFunction1.ToInt32() == 0)//0表示函数没找到
                return;

            //获取函数接口，相当于函数指针
            divide div = Marshal.GetDelegateForFunctionPointer(apiFunction1, typeof(divide)) as divide;

            Console.WriteLine(div(22));

            // //释放句柄
            FreeLibrary(hLib);

            Console.Read();
        }
    }
}
